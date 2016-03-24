using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Reflection;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Entity;
using Ia.Ngn.Cl.Model.Business; // Needed for ServerExtension

namespace Ia.Ngn.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Service Request Type support class for Next Generation Network (NGN) data model.
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
    public partial class ServiceRequestType
    {
        /// <summary/>
        public ServiceRequestType() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.ServiceRequestType> ReadListThatHasServiceRequestIdsWithinIdRange(int start, int end)
        {
            List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestTypeList = (from srt in db.ServiceRequestTypes
                                          join sr in db.ServiceRequests
                                          on srt.ServiceRequest.Id equals sr.Id
                                          where sr.Id >= start && sr.Id <= end
                                          select srt).ToList();
            }

            return serviceRequestTypeList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.ServiceRequestType> ReadListThatHaveServiceRequestsWithinGivenDateRange(DateTime startDateTime, DateTime endDateTime)
        {
            List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestTypeList = (from srt in db.ServiceRequestTypes
                                         join sr in db.ServiceRequests 
                                         on srt.ServiceRequest.Id equals sr.Id
                                         where sr.RequestDateTime >= startDateTime && sr.RequestDateTime < endDateTime select srt).ToList();
            }

            return serviceRequestTypeList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Return a list of service request types for service requests that have numbers within the passed number-serial list
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.ServiceRequestType> ReadListRelatedToServiceRequestNumberList(List<Ia.Ngn.Cl.Model.Business.ServiceRequest.NumberSerial> numberSerialList)
        {
            List<long> idList;
            List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList;

            idList = numberSerialList.IdList();

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestTypeList = (from q in db.ServiceRequestTypes
                                          where
                                          //numberSerialList.Contains(q.ServiceRequest.Number, q.ServiceRequest.Serial) 
                                          idList.Contains((long)q.ServiceRequest.Number * 100 + q.ServiceRequest.Serial)
                                          select q).Include(x => x.ServiceRequest).ToList();
            }

            return serviceRequestTypeList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Return a list of service request types for service requests that have numbers within the passed number list
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.ServiceRequestType> RelatedToServiceRequestNumberList(List<int> numberList)
        {
            List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestTypeList = (from q in db.ServiceRequestTypes where numberList.Contains(q.ServiceRequest.Number) select q).Include(x => x.ServiceRequest).ToList();
            }

            return serviceRequestTypeList;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static string OracleSqlCommandForGivenDateTime(DateTime dateTime)
        {
            string sql;

            sql = @"select SRV_REQ_FIPER_TECH.SRV_REQ_ID, SRV_REQ_FIPER_TECH.TECH_TYPE_ID, SRV_REQ_FIPER_TECH.VAL from SRV_REQ_FIPER left outer join SRV_REQ_FIPER_TECH on SRV_REQ_FIPER_TECH.SRV_REQ_ID = SRV_REQ_FIPER.SRV_REQ_ID where REQ_DATE >= '" + dateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + @"' and REQ_DATE < '" + dateTime.AddDays(1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + @"' and SRV_REQ_FIPER_TECH.SRV_REQ_ID is not null and SRV_REQ_FIPER_TECH.TECH_TYPE_ID is not null and SRV_REQ_FIPER_TECH.VAL is not null order by REQ_DATE asc, SRV_REQ_FIPER.SRV_REQ_ID asc";

            return sql;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static string OracleSqlCommandForServiceRequestIdRange(Tuple<int, int> startEndRange)
        {
            return OracleSqlCommandForServiceRequestIdRange(startEndRange.Item1, startEndRange.Item2);
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static string OracleSqlCommandForServiceRequestIdRange(int start, int end)
        {
            string sql;

            sql = @"select SRV_REQ_FIPER_TECH.SRV_REQ_ID, SRV_REQ_FIPER_TECH.TECH_TYPE_ID, SRV_REQ_FIPER_TECH.VAL from SRV_REQ_FIPER left outer join SRV_REQ_FIPER_TECH on SRV_REQ_FIPER_TECH.SRV_REQ_ID = SRV_REQ_FIPER.SRV_REQ_ID where SRV_REQ_FIPER_TECH.SRV_REQ_ID >= " + start + " and SRV_REQ_FIPER_TECH.SRV_REQ_ID <= " + end + " and SRV_REQ_FIPER_TECH.SRV_REQ_ID is not null and SRV_REQ_FIPER_TECH.TECH_TYPE_ID is not null and SRV_REQ_FIPER_TECH.VAL is not null order by REQ_DATE asc, SRV_REQ_FIPER.SRV_REQ_ID asc";

            return sql;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void UpdateForServiceRequestIdRangeWithOutputDataTable(DataTable dataTable, Tuple<int, int> startEndRange, out string result)
        {
            // below: the SQL statement should be within the dataTable.TableName variable
            int serviceRequestId, serviceRequestTypeId, start, end, readItemCount, existingItemCount, insertedItemCount, updatedItemCount, deletedItemCount;
            string sql, r;
            ArrayList newServiceRequestTypeIdArryList;
            Match match;
            Ia.Ngn.Cl.Model.ServiceRequestType serviceRequestType, newServiceRequestType;
            List<int> serviceRequestTypeWithNoServiceRequestIdList;
            List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList;

            readItemCount = existingItemCount = insertedItemCount = updatedItemCount = deletedItemCount = 0;
            result = r = "";
            serviceRequestTypeWithNoServiceRequestIdList = new List<int>();

            if (dataTable != null)
            {
                sql = dataTable.TableName;

                // select SRV_REQ_FIPER_TECH.SRV_REQ_ID, SRV_REQ_FIPER_TECH.TECH_TYPE_ID, SRV_REQ_FIPER_TECH.VAL from SRV_REQ_FIPER left outer join SRV_REQ_FIPER_TECH on SRV_REQ_FIPER_TECH.SRV_REQ_ID = SRV_REQ_FIPER.SRV_REQ_ID where SRV_REQ_FIPER_TECH.SRV_REQ_ID >= 110000 and SRV_REQ_FIPER_TECH.SRV_REQ_ID <= 321203 and SRV_REQ_FIPER_TECH.SRV_REQ_ID is not null and SRV_REQ_FIPER_TECH.TECH_TYPE_ID is not null and SRV_REQ_FIPER_TECH.VAL is not null order by REQ_DATE asc, SRV_REQ_FIPER.SRV_REQ_ID asc
                match = Regex.Match(sql, @"SRV_REQ_FIPER_TECH\.SRV_REQ_ID >= (\d+) and SRV_REQ_FIPER_TECH\.SRV_REQ_ID <= (\d+) ", RegexOptions.Singleline);
                //                                       1                       2

                if (match.Success)
                {
                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        readItemCount = dataTable.Rows.Count;

                        start = int.Parse(match.Groups[1].Value);
                        end = int.Parse(match.Groups[2].Value);

                        serviceRequestTypeList = Ia.Ngn.Cl.Model.Data.ServiceRequestType.ReadListThatHasServiceRequestIdsWithinIdRange(start, end);
                        existingItemCount = serviceRequestTypeList.Count;

                        newServiceRequestTypeIdArryList = new ArrayList(dataTable.Rows.Count + 1);

                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            serviceRequestId = int.Parse(dataRow["SRV_REQ_ID"].ToString());
                            serviceRequestTypeId = int.Parse(serviceRequestId.ToString() + dataRow["TECH_TYPE_ID"].ToString().PadLeft(2, '0'));

                            newServiceRequestType = new Ia.Ngn.Cl.Model.ServiceRequestType();

                            newServiceRequestType.Id = serviceRequestTypeId;

                            newServiceRequestType.ServiceRequest = (from q in db.ServiceRequests where q.Id == serviceRequestId select q).SingleOrDefault();

                            // below: we will not add any type that does not have a service request
                            if (newServiceRequestType.ServiceRequest != null)
                            {
                                // below: this will enable the removal of SRT that don't have a valid SR
                                newServiceRequestTypeIdArryList.Add(serviceRequestTypeId);

                                newServiceRequestType.TypeId = int.Parse(dataRow["TECH_TYPE_ID"].ToString()); ;
                                newServiceRequestType.Value = dataRow["VAL"].ToString();

                                FixCommonMistakesAndCheckValidityOfServiceRequestTypeRecords(ref newServiceRequestType);

                                serviceRequestType = (from q in serviceRequestTypeList where q.Id == newServiceRequestType.Id select q).SingleOrDefault();

                                if (serviceRequestType == null)
                                {
                                    newServiceRequestType.Created = newServiceRequestType.Updated = newServiceRequestType.Inspected = DateTime.UtcNow.AddHours(3);

                                    db.ServiceRequestTypes.Add(newServiceRequestType);

                                    insertedItemCount++;
                                }
                                else
                                {
                                    // below: copy values from newServiceRequestType to serviceRequestType

                                    if (serviceRequestType.Update(newServiceRequestType))
                                    {
                                        db.ServiceRequestTypes.Attach(serviceRequestType);
                                        db.Entry(serviceRequestType).State = System.Data.Entity.EntityState.Modified;

                                        updatedItemCount++;
                                    }
                                }
                            }
                            else
                            {
                                serviceRequestTypeWithNoServiceRequestIdList.Add(newServiceRequestType.Id);
                            }
                        }

                        /*
                        if (serviceRequestTypeWithNoServiceRequestIdList.Count > 0)
                        {
                            r = "SRT with no SR: ";

                            foreach (int n in serviceRequestTypeWithNoServiceRequestIdList) r += n + ",";

                            r = r.Trim(',');
                        }
                        */

                        // below: this function will remove values that were not present in the reading
                        if (serviceRequestTypeList.Count > 0)
                        {
                            foreach (Ia.Ngn.Cl.Model.ServiceRequestType srt in serviceRequestTypeList)
                            {
                                if (!newServiceRequestTypeIdArryList.Contains(srt.Id))
                                {
                                    serviceRequestType = (from q in db.ServiceRequestTypes where q.Id == srt.Id select q).SingleOrDefault();

                                    db.ServiceRequestTypes.Remove(srt);

                                    deletedItemCount++;
                                }
                            }
                        }

                        db.SaveChanges();

                        result = "(" + readItemCount + "/" + existingItemCount + "/" + insertedItemCount + "," + updatedItemCount + "," + deletedItemCount + ") " + r;
                    }
                }
                else
                {
                    result = "(?/?/?: SQL in TableName is unmatched) ";
                }
            }
            else
            {
                result = "(dataTable == null/?/?) ";
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void UpdateForADateTimeRangeWithOutputDataTable(DataTable dataTable, Tuple<int, int> dateTime, out string result)
        {
            // below: the SQL statement should be within the dataTable.TableName variable
            int readItemCount, existingItemCount, insertedItemCount, updatedItemCount, deletedItemCount;
            int serviceRequestId, serviceRequestTypeId;
            string sql, r;
            ArrayList newServiceRequestTypeIdArryList;
            DateTime startDateTime, endDateTime;
            Match match;
            Ia.Ngn.Cl.Model.ServiceRequestType serviceRequestType, newServiceRequestType;
            List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList;

            readItemCount = existingItemCount = insertedItemCount = updatedItemCount = deletedItemCount = 0;
            result = r = "";

            startDateTime = endDateTime = DateTime.MinValue;

            if (dataTable != null)
            {
                sql = dataTable.TableName;

                // select * from SRV_REQ_FIPER LEFT OUTER JOIN SRV_REQ_FIPER_TECH ON SRV_REQ_FIPER_TECH.SRV_REQ_ID = SRV_REQ_FIPER.SRV_REQ_ID where REQ_DATE >= '06/01/2007' and REQ_DATE < '07/01/2007'  order by REQ_DATE asc, SRV_REQ_FIPER.SRV_REQ_ID asc

                match = Regex.Match(sql, @".+'(\d{2})\/(\d{2})\/(\d{4})'.+'(\d{2})\/(\d{2})\/(\d{4})'.+", RegexOptions.Singleline);
                //                             1        2        3        4          5        6

                if (match.Success)
                {
                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        readItemCount = dataTable.Rows.Count;

                        //if (dataTable.Rows.Count > 0)
                        //{
                            startDateTime = DateTime.Parse(match.Groups[3].Value + "-" + match.Groups[2].Value + "-" + match.Groups[1].Value);
                            endDateTime = DateTime.Parse(match.Groups[6].Value + "-" + match.Groups[5].Value + "-" + match.Groups[4].Value);

                            serviceRequestTypeList = Ia.Ngn.Cl.Model.Data.ServiceRequestType.ReadListThatHaveServiceRequestsWithinGivenDateRange(startDateTime, endDateTime);
                            existingItemCount = serviceRequestTypeList.Count;

                            newServiceRequestTypeIdArryList = new ArrayList(dataTable.Rows.Count + 1);

                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                serviceRequestId = int.Parse(dataRow["SRV_REQ_ID"].ToString());
                                serviceRequestTypeId = int.Parse(serviceRequestId.ToString() + dataRow["TECH_TYPE_ID"].ToString().PadLeft(2, '0'));

                                newServiceRequestType = new Ia.Ngn.Cl.Model.ServiceRequestType();

                                newServiceRequestType.Id = serviceRequestTypeId;

                                newServiceRequestType.ServiceRequest = (from q in db.ServiceRequests where q.Id == serviceRequestId select q).SingleOrDefault();

                                // below: we will not add any type that does not have a service request
                                if (newServiceRequestType.ServiceRequest != null)
                                {
                                    // below: this will enable the removal of SRT that don't have a valid SR
                                    newServiceRequestTypeIdArryList.Add(serviceRequestTypeId);

                                    newServiceRequestType.TypeId = int.Parse(dataRow["TECH_TYPE_ID"].ToString()); ;
                                    newServiceRequestType.Value = dataRow["VAL"].ToString();

                                    FixCommonMistakesAndCheckValidityOfServiceRequestTypeRecords(ref newServiceRequestType);

                                    serviceRequestType = (from q in serviceRequestTypeList where q.Id == newServiceRequestType.Id select q).SingleOrDefault();

                                    if (serviceRequestType == null)
                                    {
                                        newServiceRequestType.Created = newServiceRequestType.Updated = newServiceRequestType.Inspected = DateTime.UtcNow.AddHours(3);

                                        db.ServiceRequestTypes.Add(newServiceRequestType);

                                        insertedItemCount++;
                                    }
                                    else
                                    {
                                        // below: copy values from newServiceRequestType to serviceRequestType

                                        if (serviceRequestType.Update(newServiceRequestType))
                                        {
                                            db.ServiceRequestTypes.Attach(serviceRequestType);
                                            db.Entry(serviceRequestType).State = System.Data.Entity.EntityState.Modified;

                                            updatedItemCount++;
                                        }
                                    }
                                }
                                else
                                {
                                    r += "newServiceRequestType.Id: " + newServiceRequestType.Id + " newServiceRequestType.ServiceRequest == null, ";
                                }
                            }

                            // below: this function will remove values that were not present in the reading
                            if (serviceRequestTypeList.Count > 0)
                            {
                                foreach (Ia.Ngn.Cl.Model.ServiceRequestType srt in serviceRequestTypeList)
                                {
                                    if (!newServiceRequestTypeIdArryList.Contains(srt.Id))
                                    {
                                        serviceRequestType = (from q in db.ServiceRequestTypes where q.Id == srt.Id select q).SingleOrDefault();

                                        db.ServiceRequestTypes.Remove(srt);

                                        deletedItemCount++;
                                    }
                                }
                            }

                            db.SaveChanges();

                            result = "(" + readItemCount + "/" + existingItemCount + "/" + insertedItemCount + "," + updatedItemCount + "," + deletedItemCount + ") " + r;
                        //}
                        //else
                        //{
                        //    result = "(" + readItemCount + "/?/?) ";
                        //}
                    }
                }
                else
                {
                    result = "(?/?/?: SQL in TableName is unmatched) ";
                }
            }
            else
            {
                result = "(dataTable == null/?/?) ";
            }
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        private static void FixCommonMistakesAndCheckValidityOfServiceRequestTypeRecords(ref Ia.Ngn.Cl.Model.ServiceRequestType serviceRequestType)
        {
            // below: procedure to fix service request records from the common mistakes

            bool b;
            int number;

            // below: convert 7 digit numbers to 8 digits
            // <type id="11" name="dn" arabicName="dn" oracleFieldName="الرقم الجديد"/>
            if (serviceRequestType.TypeId == 11)
            {
                b = int.TryParse(serviceRequestType.Value.Trim(), out number);

                if (b)
                {
                    number = Ia.Ngn.Cl.Model.Business.Default.ChangeOldSevenDigitNumbersToEightDigitFormat(number);

                    if (Ia.Ngn.Cl.Model.Business.Service.NumberIsWithinAllowedDomainList(number))
                    {
                        serviceRequestType.Value = number.ToString();
                    }
                    else serviceRequestType.Value = null;
                }
                else serviceRequestType.Value = null;
            }
        }

        ////////////////////////////////////////////////////////////////////////////    
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}