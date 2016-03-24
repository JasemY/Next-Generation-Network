using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;

namespace Ia.Ngn.Cl.Model.Business
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Service Request Service support class of Next Generation Network'a (NGN's) business model.
    /// </summary>
    /// 
    /// <remarks> 
    /// Copyright © 2006-2015 Jasem Y. Al-Shamlan (info@ia.com.kw), Internet Applications - Kuwait. All Rights Reserved.
    ///
    /// This library is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by
    /// the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
    ///
    /// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
    /// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
    /// 
    /// You should have received a copy of the GNU General Public License along with this library. If not, see http://www.gnu.org/licenses.
    /// 
    /// Copyright notice: This notice may not be removed or altered from any source distribution.
    /// </remarks> 
    public partial class ServiceRequestService
    {
        private static StringBuilder log = new StringBuilder();

        /*
        internal sealed class Tuple<TFirst, TSecond>
        {
            public Tuple(TFirst first, TSecond second) { First = first; Second = second; }

            public TFirst First { get; private set; }
            public TSecond Second { get; private set; }

            public override bool Equals(object value)
            {
                return value != null && Equals(value as Tuple<TFirst, TSecond>);
            }

            private bool Equals(Tuple<TFirst, TSecond> type)
            {
                return type != null
                       && EqualityComparer<TFirst>.Default.Equals(type.First, First)
                       && EqualityComparer<TSecond>.Default.Equals(type.Second, Second);
            }

            public override int GetHashCode() { throw new NotImplementedException(); }

            public override string ToString()
            {
                return "{ First = " + First + ", Second = " + Second + " }";
            }
        }
         */

        /// <summary/>
        public ServiceRequestService() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Update service-request-service table with data from service requests
        /// </summary>
        public static void UpdateForServiceRequestIdRange(Tuple<int, int> startEndRange, out string result)
        {
            bool recordExisted, initialRecordCreated;
            int serviceType;
            string service, serviceRequestServiceId, r0, r1, r2;
            Hashtable serviceNumberChangeHashtable;
            Ia.Ngn.Cl.Model.ServiceRequestService serviceRequestService;
            List<int> numberList, changedNumberList;
            List<string> serviceList;
            List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList;
            List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList;
            List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList;

            recordExisted = false;
            r0 = r1 = r2 = "";
            serviceNumberChangeHashtable = new Hashtable(10);
            // above: serviceNumberChangeHashtable will contain serviceRequestServiceId as keys and the new number as value

            // 1. Read all SRs for this range
            serviceRequestList = Ia.Ngn.Cl.Model.Data.ServiceRequest.ReadListWithinIdRange(startEndRange.Item1, startEndRange.Item2);

            // 2. Collect numbers in int format into int list
            numberList = serviceRequestList.DistinctNumberList();
            //numberList = new List<int>();
            //numberList.Add(24848450);

            // 3. Read all SRs related to collected numbers
            serviceRequestList = Ia.Ngn.Cl.Model.Data.ServiceRequest.ReadList(numberList);

            // 4. Read all SRTs related to collected numbers
            serviceRequestTypeList = Ia.Ngn.Cl.Model.Data.ServiceRequestType.RelatedToServiceRequestNumberList(numberList);

            // 5 Read all possible changed numbers from all SRT and add them to service list
            changedNumberList = Ia.Ngn.Cl.Model.Business.ServiceRequestType.PossibleChangedNumberList(serviceRequestTypeList);

            if (changedNumberList.Count > 0)
            {
                numberList.AddRange(changedNumberList);
                numberList = numberList.Distinct().ToList();

                // 6 Read all SRs related to collected numbers
                serviceRequestList = Ia.Ngn.Cl.Model.Data.ServiceRequest.ReadList(numberList);

                // 7. Read all SRTs related to collected numbers
                serviceRequestTypeList = Ia.Ngn.Cl.Model.Data.ServiceRequestType.RelatedToServiceRequestNumberList(numberList);
            }

            // 8. Collect numbers in string format into int list (add changed number list)
            serviceList = (from q in numberList select q.ToString()).ToList<string>();

            // 9. Initialize SRS empty list
            serviceRequestServiceList = new List<Ia.Ngn.Cl.Model.ServiceRequestService>(serviceRequestList.Count);

            serviceType = 1; // (?) <type id="1" name="Dn"

            // 10. Loop through all sr records by request id ascending
            foreach (Ia.Ngn.Cl.Model.ServiceRequest sr in serviceRequestList.OrderBy(p => p.Id))
            {
                // below: we will only process service requests that have status = 2003 or status = 2005
                if (sr.Status == 2003 || sr.Status == 2005)
                {
                    // <status id="2003" arabicName="قيد التنفيذ" />
                    // <status id="2005" arabicName="تم التنفيذ" />
                    // below: first check if there is already a SRS record within this loop

                    initialRecordCreated = false;

                    service = sr.Number.ToString();

                    serviceRequestServiceId = Ia.Ngn.Cl.Model.ServiceRequestService.ServiceRequestServiceId(service, serviceType);

                    serviceRequestService = (from q in serviceRequestServiceList where q.Id == serviceRequestServiceId select q).SingleOrDefault();

                    if (serviceRequestService == null)
                    {
                        serviceRequestService = new Ia.Ngn.Cl.Model.ServiceRequestService();

                        serviceRequestService.Id = serviceRequestServiceId;
                        serviceRequestService.Service = service;
                        serviceRequestService.AreaCode = Ia.Ngn.Cl.Model.Data.Service.CountryCode;
                        serviceRequestService.ServiceType = serviceType;

                        // below: We had some problems with Huawei numbers not having complete service orders. this will enable some services initially. If an installation order comes it will override this. If not, this will enable services
                        // for incomplete service orders
                        serviceRequestService.CallerId = true;
                        //serviceRequestService.WakeupCall = true;
                        //serviceRequestService.InternationalCallingUserControlled = true;
                        serviceRequestService.CallWaiting = true;
                        //serviceRequestService.CallForwarding = true;
                        //serviceRequestService.ConferenceCall = true;

                        recordExisted = false;
                    }
                    else recordExisted = true;

                    if (sr.ServiceCategoryId == 3)
                    {
                        // <category id="1" arabicName="الخدمات الهاتفية" />

                        TelephonyService(sr, serviceRequestTypeList, ref serviceRequestService, recordExisted, out initialRecordCreated);
                    }
                    else if (sr.ServiceCategoryId == 49)
                    {
                        // <category id="49" arabicName="إنترنت ألياف ضوئية - مشترك" />

                        HsiService(sr, serviceRequestTypeList, ref serviceRequestService, recordExisted);
                    }
                    else
                    {
                        //r0 += "service category id=" + sr.ServiceCategoryId + " was not recognized. \r\n";
                    }

                    // below: important, only update if it is null
                    //if (srs.Access != null) newServiceRequestService.Access = Ia.Ngn.Cl.Model.Access.Read(db, srs.Access.Id);

                    //if(serviceRequestService != null) recordExisted = serviceRequestServiceList.Any(srs => srs.Id == serviceRequestService.Id);

                    //if (initialRecordCreated)
                    //{
                    //    serviceRequestServiceList.Add(serviceRequestService);
                    //    sr.ServiceRequestService = serviceRequestService;
                    //}
                    //else
                    //{
                    if (!recordExisted)
                    {
                        if (serviceRequestService != null)
                        {
                            serviceRequestServiceList.Add(serviceRequestService);

                            sr.ServiceRequestService = serviceRequestService;
                        }
                    }
                    else if (recordExisted)
                    {
                        if (serviceRequestService == null)
                        {
                            // below: remove entry from list
                            var itemToRemove = serviceRequestServiceList.SingleOrDefault(r => r.Id == serviceRequestServiceId);

                            if (itemToRemove != null) serviceRequestServiceList.Remove(itemToRemove);

                            sr.ServiceRequestService = null;
                        }
                        else
                        {
                            sr.ServiceRequestService = serviceRequestService;
                        }
                    }
                    //}
                }
                else
                {
                    //r0 += "status=" + sr.Status + " unprocessed. \r\n";
                }
            }

            // 11. Remove duplicates
            serviceRequestServiceList = serviceRequestServiceList.OrderByDescending(u => u.Updated).GroupBy(u => u.Id).Select(u => u.First()).ToList();

            // 12. Update service request services
            Ia.Ngn.Cl.Model.Data.ServiceRequestService.UpdateWithServiceList(serviceList, serviceRequestServiceList, out r1);

            // 13. Update service requests with SRS foreign keys
            Ia.Ngn.Cl.Model.Data.ServiceRequest.UpdateWithServiceList(serviceList, serviceRequestList, out r2);

            result = r1 + " " + r2;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static void TelephonyService(Ia.Ngn.Cl.Model.ServiceRequest serviceRequest, List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList, ref Ia.Ngn.Cl.Model.ServiceRequestService serviceRequestService, bool recordExists, out bool initialRecordCreated)
        {
            int serviceType;
            string changedServiceNumber;
            Ia.Ngn.Cl.Model.Access access;

            /*
             * <service id="15" arabicName="خدمة المحاسبة" />
             * <service id="54" arabicName="اعادة تركيب" />
             * <service id="42" arabicName="نقل داخلي" />
             * <service id="44" arabicName="تغيير نوع إشتراك" />
             * <service id="63" arabicName="ايقاف التحويل الآلي/بدالة فرع" />
             * <service id="64" arabicName="ايقاف التحويل الآلي/بدالة رئيس" />
             * <service id="65" arabicName="إيقاف المحاسبة الذاتية" />
             * <service id="271" arabicName="تغيير اسم"/>
             */

            initialRecordCreated = false;
            serviceType = 1; // (?) <type id="1" name="Dn"

            // below: we will process only telephone service numbers that fall within network specs.
            if (Ia.Ngn.Cl.Model.Business.Service.NumberIsWithinAllowedDomainList(serviceRequest.Number))
            {
                if (serviceRequest.ServiceId == 1 || serviceRequest.ServiceId == 129 || serviceRequest.ServiceId == 39 || serviceRequest.ServiceId == 41)
                {
                    // <service id="1" arabicName="خط هاتف" />
                    // <service id="129" arabicName="خط هاتف مع نداء آلي"/>
                    // <service id="39" arabicName="نقل خارجى" />
                    // <service id="41" arabicName="تغيير رقم" />

                    if (serviceRequest.ServiceId == 1 || serviceRequest.ServiceId == 129 || serviceRequest.ServiceId == 39)
                    {
                        // <service id="1" arabicName="خط هاتف" />
                        // <service id="129" arabicName="خط هاتف مع نداء آلي"/>
                        // <service id="39" arabicName="نقل خارجى" />

                        using (var db = new Ia.Ngn.Cl.Model.Ngn())
                        {
                            // important: ServiceRequestService.Update() will only update stored.Access if it is null, or (stored.userId == Guid.Empty && update.Id > stored.Id)
                            access = Ia.Ngn.Cl.Model.Business.ServiceRequestType.ExtractAccess(serviceRequest.Id, serviceRequestTypeList);

                            if (access != null) serviceRequestService.Access = (from a in db.Accesses where a.Id == access.Id select a).SingleOrDefault();
                            else serviceRequestService.Access = null;
                        }

                        if (/*!recordExists &&*/ (serviceRequest.ServiceId == 1 || serviceRequest.ServiceId == 129))
                        {
                            // <service id="1" arabicName="خط هاتف" />
                            // <service id="129" arabicName="خط هاتف مع نداء آلي"/>

                            serviceRequestService.CallerId = false;
                            serviceRequestService.WakeupCall = false;
                            ///serviceRequestService.InternationalCallingUserControlled = true; // as requested by MOC NGN management
                            serviceRequestService.CallWaiting = false;
                            serviceRequestService.CallForwarding = false;
                            serviceRequestService.ConferenceCall = false;

                            if (serviceRequest.ServiceId == 1) serviceRequestService.InternationalCalling = false;
                            else if (serviceRequest.ServiceId == 129) serviceRequestService.InternationalCalling = true;

                            initialRecordCreated = true;
                        }
                        else if (/*recordExists &&*/ serviceRequest.ServiceId == 39)
                        {
                            // <service id="39" arabicName="نقل خارجى" />
                        }
                    }
                    else if (/*recordExists &&*/ serviceRequest.ServiceId == 41)
                    {
                        // <service id="41" arabicName="تغيير رقم" />

                        // below: <type id="11" name="dn" arabicName="dn" oracleFieldName="الرقم الجديد"/>
                        changedServiceNumber = (from q in serviceRequestTypeList where q.ServiceRequest.Id == serviceRequest.Id && q.TypeId == 11 select q.Value).SingleOrDefault();

                        if (Ia.Ngn.Cl.Model.Business.Service.NumberIsWithinAllowedDomainList(changedServiceNumber))
                        {
                            serviceRequestService.Id = Ia.Ngn.Cl.Model.ServiceRequestService.ServiceRequestServiceId(changedServiceNumber, serviceType);
                            serviceRequestService.Service = changedServiceNumber;
                        }
                        else
                        {

                        }
                    }
                }
                else if (serviceRequest.ServiceId == 43)
                {
                    // <service id="43" arabicName="تنازل" />

                    // Id	Number	Serial	Status	RequestDateTime	CustomerId	CustomerName	CustomerCategoryId	CustomerAddress	ServiceId	ServiceCategoryId	Balance	Created	Updated	Inspected	UserId	ServiceRequestService_Id
                    // 1821798	24573325	1	2005	2015-08-20 00:00:00.000	1500989	محمد مهاوش سويد السعيدي	1	الجهراء الجهراء,قطعة 5 شارع 1 جادة قسائم العثمان قسيمة الا زرق 29, منزل 529	43	3	0	2015-08-20 20:41:04.403	2015-08-20 20:41:04.403	2015-08-20 20:41:04.403	00000000-0000-0000-0000-000000000000	NULL
                    // 1821312	25234142	0	2005	2015-08-19 00:00:00.000	1476656	رنا فيصل عبدالرزاق الخالد	1	حولى الشهداء,قطعة 1 شارع 3, منزل 501	43	3	0	2015-08-19 11:13:32.827	2015-08-19 11:13:32.827	2015-08-19 11:13:32.827	00000000-0000-0000-0000-000000000000	NULL

                    // No SRT records. Nothing to do.
                }
                else
                {
                    //if (recordExists)
                    //{
                    switch (serviceRequest.ServiceId)
                    {
                        // below: <service id="40" arabicName="رفع خط" />
                        case 40: { serviceRequestService = null; break; }

                        // below: <service id="12" arabicName="خدمة الإنتظار" />
                        case 12: { serviceRequestService.CallWaiting = true; break; }

                        // below: <service id="68" arabicName="إيقاف خدمة الإنتظار" />
                        case 68: { serviceRequestService.CallWaiting = false; break; }

                        // below: <service id="13" arabicName="التحكم بالصفر الدولي" />
                        case 13: { serviceRequestService.InternationalCallingUserControlled = true; break; }

                        // below: <service id="67" arabicName="وقف التحكم بالصفر" />
                        case 67: { serviceRequestService.InternationalCallingUserControlled = false; break; }

                        // below: <service id="14" arabicName="كاشف رقم" />
                        case 14: { serviceRequestService.CallerId = true; break; }

                        // below: <service id="66" arabicName="إيقاف كاشف" />
                        case 66: { serviceRequestService.CallerId = false; break; }

                        // below: <service id="5" arabicName="تحويل مكالمات" />
                        case 5: { serviceRequestService.CallForwarding = true; break; }

                        // below: <service id="75" arabicName="إيقاف التحويل" />
                        case 75: { serviceRequestService.CallForwarding = false; break; }

                        // below: <service id="20" arabicName="النداء الآلي" />
                        case 20: { serviceRequestService.InternationalCalling = true; break; }

                        // below: <service id="19" arabicName="قطع النداء الآلي" />
                        case 19: { serviceRequestService.InternationalCalling = false; break; }

                        // below: <service id="38" arabicName="مجموعة الخدمات" />
                        case 38:
                            {
                                serviceRequestService.CallerId = true;
                                serviceRequestService.WakeupCall = true;
                                //serviceRequestService.InternationalCallingUserControlled = true;
                                serviceRequestService.CallWaiting = true;
                                serviceRequestService.CallForwarding = true;
                                serviceRequestService.ConferenceCall = true;
                                break;
                            }

                        // below: <service id="62" arabicName="إيقاف مجموعة الخدمات" />
                        case 62:
                            {
                                serviceRequestService.CallerId = false;
                                serviceRequestService.WakeupCall = false;
                                //serviceRequestService.InternationalCallingUserControlled = false;
                                serviceRequestService.CallWaiting = false;
                                serviceRequestService.CallForwarding = false;
                                serviceRequestService.ConferenceCall = false;
                                break;
                            }

                        // below: <service id="52" arabicName="قطع حرارة" />
                        case 52: { serviceRequestService.CallBarring = true; break; }

                        // below: <service id="53" arabicName="اعادة حرارة" />
                        case 53: { serviceRequestService.CallBarring = false; break; }

                        // below: <service id="7" arabicName="منع الإتصال" />
                        case 7: { serviceRequestService.BarringOfAllOutgoingCalls = true; break; }

                        // below: <service id="73" arabicName="إيقاف منع الإتصال" />
                        case 73: { serviceRequestService.BarringOfAllOutgoingCalls = false; break; }

                        // below: <service id="8" arabicName="ايقاف استقبال" />
                        case 8: { serviceRequestService.BarringOfAllIncomingCalls = true; break; }

                        // below: <service id="72" arabicName="رد استقبال" />
                        case 72: { serviceRequestService.BarringOfAllIncomingCalls = false; break; }

                        // below: <service id="10" arabicName="خدمة الإيقاظ" />
                        case 10: { serviceRequestService.WakeupCall = true; break; }

                        // below: <service id="70" arabicName="إيقاف خدمة الإيقاظ" />
                        case 70: { serviceRequestService.WakeupCall = false; break; }

                        // below: <service id="6" arabicName="استشارة" />
                        case 6: { serviceRequestService.ConferenceCall = true; break; }

                        // below: <service id="74" arabicName="إيقاف استشارة" />
                        case 74: { serviceRequestService.ConferenceCall = false; break; }

                        // below: <service id="11" arabicName="إختصار الرقم" />
                        case 11: { serviceRequestService.AbbriviatedCalling = true; break; }

                        // below: <service id="69" arabicName="إيقاف إختصار الرقم" />
                        case 69: { serviceRequestService.AbbriviatedCalling = false; break; }

                        // ???
                        // below: <service id="55" arabicName="مفتاح بدالة" />
                        //case 55: { srs.?? = false; break; }

                        // below: <if id="60" name_ar="إيقاف مفتاح بدالة"...
                        //case 60: { dr["pbx_dn_key"] = DBNull.Value; break; }

                        // below: <if id="56" name_ar="فرع بدالة"...
                        //case 56:
                        //    {
                        //        u = r["pbx_dn"].ToString();
                        //        u = Regex.Replace(u, @"\/\d", ""); // remove that last /0
                        //        pbx_dn = int.Parse(Ia.Ngn.Cs.This.Digit_7_to_8(u));
                        //        dr["pbx_dn_key"] = pbx_dn;
                        //        break;
                        //    }

                        // below: <if id="59" name_ar="إيقاف فرع بدالة"...
                        //case 59: { dr["pbx_dn_key"] = DBNull.Value; break; }

                        default:
                            {
                                log.AppendLine("TelephonyService(): For ServiceRequest.Number=" + serviceRequest.Number + " ServiceId=" + serviceRequest.ServiceId + " was not recognized. ");
                                break;
                            }
                    }
                    //}
                    //else
                    //{
                    //    log.AppendLine("TelephonyService(): For ServiceRequest.Number=" + serviceRequest.Number + " ServiceId=" + serviceRequest.ServiceId + " record did not exist before this step. ");
                    //}
                }
            }
            else
            {
                // below: delete record
                serviceRequestService = null;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static void HsiService(Ia.Ngn.Cl.Model.ServiceRequest sr, List<Ia.Ngn.Cl.Model.ServiceRequestType> srtList, ref Ia.Ngn.Cl.Model.ServiceRequestService serviceRequestService, bool recordExists)
        {
            if (sr.ServiceId == 406)
            {
                /*
                // <if id="406" name_ar="إنترنت ألياف ضوئية - مشترك"

                try
                {
                    u = Ia.Ngn.Cs.This.Digit_7_to_8(r["hsi_dn"].ToString());

                    //if (u == "25220203") { }

                    if (u.Length == 8)
                    {
                        dr = srs_dt.Rows.Find(u);

                        if (dr != null)
                        {
                            xn = hsi_xd.SelectSingleNode("hsi/isp/isp[@service_request_customer_id='" + r["customer_id"].ToString() + "']");

                            if (xn != null)
                            {
                                isp = int.Parse(xn.Attributes["id"].Value);

                                //dr["hsi_dn"] = Ia.Ngn.Cs.This.Digit_7_to_8(r["dn"].ToString());
                                dr["hsi_dn"] = r["dn"].ToString();
                                dr["hsi_isp"] = isp;
                                dr["hsi_profile"] = 0;

                                // below: store the relationship between sr.dn and srs.dn for use later if the HSI service is removed
                                hsi_ht[r["dn"].ToString()] = u; // +":" + isp;
                            }
                            else
                            {
                                log.Append("(hsi error: id=" + r["id"].ToString() + " []),");
                            }
                        }
                        else
                        {
                            log.Append("(hsi error srs_dt rows not found for dn = " + u + "; id=" + r["id"].ToString() + "),");
                        }
                    }
                    else
                    {
                        log.Append("(hsi dn.Length != 8; dn = " + u + "; sr_id=" + r["id"].ToString() + " []),");
                    }
                }
                catch (Exception ex)
                {
                    log.Append("(hsi exception error: id=" + r["id"].ToString() + ex.ToString() + "),");
                }
                 */
            }
            else if (sr.ServiceId == 407)
            {
                /*
                // <if id="407" name_ar="إيقاف"

                // stopping an HSI service is tricky, the work order will only have the dn value of the HSI service to be stopped. Here I will read the actualy dn number that correspondes to this hsi_dn value
                // and find the numbers record in srs, then delete HSI records associated with it (making sure the hsi_dn value is consistance)

                u = r["dn"].ToString();

                if (hsi_ht.ContainsKey(u))
                {
                    dr = srs_dt.Rows.Find(hsi_ht[u].ToString());

                    if (dr != null)
                    {
                        xn = hsi_xd.SelectSingleNode("hsi/isp/isp[@service_request_customer_id='" + r["customer_id"].ToString() + "']");

                        if (xn != null)
                        {
                            isp = int.Parse(xn.Attributes["id"].Value);

                            // below: only delete if srs.hsi_dn == sr.dn
                            if (dr["hsi_dn"].ToString() == u) // + ":" + isp)
                            {
                                dr["hsi_dn"] = DBNull.Value;
                                dr["hsi_isp"] = DBNull.Value;
                                dr["hsi_profile"] = DBNull.Value;

                                // below: delete the hasttable entry
                                hsi_ht.Remove(u);
                            }
                            else
                            {
                                log.Append("(hsi remove error srs_dt hsi_dn value does not match sr.dn sr.dn= " + u + "; id=" + r["id"].ToString() + "),");
                            }
                        }
                        else
                        {
                            log.Append("(hsi remove error: id=" + r["id"].ToString() + " []),");
                        }
                    }
                    else
                    {
                        log.Append("(hsi remove error srs_dt rows not found for dn = " + u + "; id=" + r["id"].ToString() + "),");
                    }
                }
                else
                {
                                 
                 log.Append("(hsi remove error hashtable does not contain sr.dn as key sr.dn = " + u + "; id=" + r["id"].ToString() + "),");
                }
                 */
            }
            else
            {
                log.AppendLine("HsiService(): For ServiceRequest.Number=" + sr.Number + " ServiceId=" + sr.ServiceId + " was not recognized. ");
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}