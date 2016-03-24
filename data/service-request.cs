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
    /// Service Request support class for Next Generation Network (NGN) data model.
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
    public partial class ServiceRequest
    {
        private static int serviceRequestIdStartEndRangeBufferedListIndex;
        private static SortedList serviceCategorySortedList, serviceSortedList, customerCategorySortedList, statusSortedList;
        private static XDocument xDocument;
        private static List<Tuple<int,int>> serviceRequestIdStartEndRangeBufferedList;

        /// <summary/>
        public ServiceRequest() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static SortedList ServiceCategorySortedList
        {
            get
            {
                int id;

                if (serviceCategorySortedList == null)
                {
                    serviceCategorySortedList = new SortedList(10);

                    foreach (XElement x in XDocument.Element("serviceRequest").Elements("service").Elements("categoryList").Elements("category"))
                    {
                        id = int.Parse(x.Attribute("id").Value);

                        serviceCategorySortedList[id] = x.Attribute("arabicName").Value;
                    }
                }

                return serviceCategorySortedList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static SortedList ServiceSortedList
        {
            get
            {
                int id;

                if (serviceSortedList == null)
                {
                    serviceSortedList = new SortedList(10);

                    foreach (XElement x in XDocument.Element("serviceRequest").Elements("service").Elements("serviceList").Elements("service"))
                    {
                        id = int.Parse(x.Attribute("id").Value);

                        serviceSortedList[id] = x.Attribute("arabicName").Value;
                    }
                }

                return serviceSortedList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static SortedList CustomerCategorySortedList
        {
            get
            {
                int id;

                if (customerCategorySortedList == null)
                {
                    customerCategorySortedList = new SortedList(10);

                    foreach (XElement x in XDocument.Element("serviceRequest").Elements("customer").Elements("categoryList").Elements("category"))
                    {
                        id = int.Parse(x.Attribute("id").Value);

                        customerCategorySortedList[id] = x.Attribute("arabicName").Value;
                    }
                }

                return customerCategorySortedList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static SortedList StatusSortedList
        {
            get
            {
                int id;

                if (statusSortedList == null)
                {
                    statusSortedList = new SortedList(10);

                    foreach (XElement x in XDocument.Element("serviceRequest").Elements("statusList").Elements("status"))
                    {
                        id = int.Parse(x.Attribute("id").Value);

                        statusSortedList[id] = x.Attribute("arabicName").Value;
                    }
                }

                return statusSortedList;
            }
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
        private static string OracleSqlCommandForServiceRequestIdRange(int start, int end)
        {
            string sql;

            // select * from SRV_REQ_FIPER where SRV_REQ_ID >= 110000 and SRV_REQ_ID <= 321203 order by REQ_DATE asc, SRV_REQ_ID asc
            sql = @"select * from SRV_REQ_FIPER where SRV_REQ_ID >= " + start + " and SRV_REQ_ID <= " + end + " order by REQ_DATE asc, SRV_REQ_ID asc";

            return sql;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        private static string OracleSqlCommandForGivenDateTime(DateTime dateTime)
        {
            string sql;

            //sql = @"select * from SRV_REQ_FIPER LEFT OUTER JOIN SRV_REQ_FIPER_TECH ON SRV_REQ_FIPER_TECH.SRV_REQ_ID = SRV_REQ_FIPER.SRV_REQ_ID where REQ_DATE >= '" + dateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "' and REQ_DATE < '" + dateTime.AddDays(1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "' order by REQ_DATE asc, SRV_REQ_ID asc";
            sql = @"select * from SRV_REQ_FIPER where REQ_DATE >= '" + dateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "' and REQ_DATE < '" + dateTime.AddDays(1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "' order by REQ_DATE asc, SRV_REQ_ID asc";

            return sql;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        private static string OracleSqlCommandForSingleRandomDateTimeWithinTheLastNDays(int rangeOfPastDays, out DateTime selectedDate)
        {
            // below:
            int i;
            string sql;

            i = Ia.Cl.Model.Default.Random(rangeOfPastDays);

            selectedDate = DateTime.UtcNow.AddDays(-i);

            sql = OracleSqlCommandForGivenDateTime(selectedDate);

            return sql;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        private static string OracleSqlCommandForSingleDateTimeUsingDayIndexBetweenNowAndEarliestDate(ref int index, out DateTime selectedDate)
        {
            // below: select a date between now and the earliest date using an variable index value
            string sql;
            DateTime now;

            now = DateTime.UtcNow.AddHours(3);

            // below: check that inIndex is an index of a day between earliest day and now, and reset it to 0 if it is bigger than now
            if (DateTime.Compare(Ia.Ngn.Cl.Model.Business.Administration.EarliestServiceRequestDate.AddDays(index), now) < 0)
            {
                // below: within range
            }
            else index = 0;

            selectedDate = Ia.Ngn.Cl.Model.Business.Administration.EarliestServiceRequestDate.AddDays(index++);

            sql = OracleSqlCommandForGivenDateTime(selectedDate);

            return sql;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static Tuple<int, int> ServiceRequestIdStartEndRangeManager(ref int index, out string result)
        {
            int i, count, edgeBufferRange, lastestRangeList;
            Tuple<int, int> tuple;
            List<int> list;

            count = 50;
            edgeBufferRange = 5000;
            lastestRangeList = 21;

            if (serviceRequestIdStartEndRangeBufferedList == null || serviceRequestIdStartEndRangeBufferedListIndex == 0)
            {
                serviceRequestIdStartEndRangeBufferedListIndex = index;

                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    list = (from q in db.ServiceRequests orderby q.Id ascending select q.Id).ToList();
                }

                serviceRequestIdStartEndRangeBufferedList = Ia.Cl.Model.Default.OptimizedStartEndRangeBufferedList(list, count, edgeBufferRange);

                serviceRequestIdStartEndRangeBufferedListIndex = 0;
            }

            if (serviceRequestIdStartEndRangeBufferedList.Count > 0)
            {
                if (Ia.Ngn.Cl.Model.Business.Administration.NowIsOfficialWorkingTime)
                {
                    i = serviceRequestIdStartEndRangeBufferedList.Count - Ia.Cl.Model.Default.Random(lastestRangeList) - 1;

                    if (i < 0) i = 0;

                    tuple = serviceRequestIdStartEndRangeBufferedList[i];

                    if (i == serviceRequestIdStartEndRangeBufferedList.Count - 1)
                    {
                        tuple = new Tuple<int, int>(tuple.Item1, tuple.Item2 + edgeBufferRange);
                    }

                    result = "(latest: " + tuple.Item1 + "-" + tuple.Item2 + " " + serviceRequestIdStartEndRangeBufferedListIndex + "/" + serviceRequestIdStartEndRangeBufferedList.Count + ")";
                }
                else
                {
                    tuple = serviceRequestIdStartEndRangeBufferedList[serviceRequestIdStartEndRangeBufferedListIndex];

                    result = "(historic: " + tuple.Item1 + "-" + tuple.Item2 + " " + serviceRequestIdStartEndRangeBufferedListIndex + "/" + serviceRequestIdStartEndRangeBufferedList.Count + ")";
                }

                serviceRequestIdStartEndRangeBufferedListIndex = Ia.Cl.Model.Default.IncrementListIndexOrRestart(serviceRequestIdStartEndRangeBufferedList, serviceRequestIdStartEndRangeBufferedListIndex);
            }
            else
            {
                result = "(0-0 0/0)";

                tuple = null;
            }

            index = serviceRequestIdStartEndRangeBufferedListIndex;

            return tuple;
        }

        /*
        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        private static ArrayList SqlCommandsForSingleDaysArrayList
        {
            get
            {
                string sql;
                DateTime startDateTime, endDateTime;
                Ia.Ngn.Cl.Model.ServiceRequest serviceRequest;

                if (sqlCommandsForSingleDaysArrayList == null || sqlCommandsForSingleDaysArrayList.Count == 0)
                {
                    // below: start from the date with the oldest inspected time
                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        serviceRequest = (from q in db.ServiceRequests orderby q.Inspected select q).Take(1).SingleOrDefault();

                        if (serviceRequest != null)
                        {
                            startDateTime = serviceRequest.RequestDateTime;

                            endDateTime = DateTime.UtcNow.AddHours(3);

                            // below: the date format on each database to ensuer they are the same
                            //sql = @"select * from nls_session_parameters";

                            // sql = @"SELECT * FROM SRV_REQ_FIPER WHERE ROWNUM = 1"; // @"DESCRIBE SRV_REQ_FIPER;";

                            sqlCommandsForSingleDaysArrayList = new ArrayList();

                            for (DateTime dateTime = startDateTime; dateTime < endDateTime; dateTime = dateTime.AddDays(1))
                            {
                                sql = OracleSqlCommandForGivenDateTime(dateTime);

                                sqlCommandsForSingleDaysArrayList.Add(sql);
                            }
                        }
                    }
                }

                return sqlCommandsForSingleDaysArrayList;
            }
        }*/

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static string AlterSessionOfCustomerDepartmentOracleDatabase
        {
            get
            {
                return @"alter session set nls_date_format = 'DD/MM/YYYY HH24:MI:SS'";
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void UpdateWithServiceList(List<string> serviceList, List<Ia.Ngn.Cl.Model.ServiceRequest> newServiceRequestList, out string result)
        {
            int readItemCount, existingItemCount, insertedItemCount, updatedItemCount, deletedItemCount, serviceRequestId;
            string serviceRequestServiceId;
            Ia.Ngn.Cl.Model.ServiceRequest serviceRequest;

            readItemCount = existingItemCount = insertedItemCount = updatedItemCount = deletedItemCount = 0;
            result = "";

            readItemCount = newServiceRequestList.Count;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                foreach (Ia.Ngn.Cl.Model.ServiceRequest newServiceRequest in newServiceRequestList)
                {
                    serviceRequestId = newServiceRequest.Id;

                    serviceRequest = (from q in db.ServiceRequests where q.Id == serviceRequestId select q).SingleOrDefault();

                    if (newServiceRequest.ServiceRequestService != null)
                    {
                        serviceRequestServiceId = newServiceRequest.ServiceRequestService.Id;

                        newServiceRequest.ServiceRequestService = (from q in db.ServiceRequestServices where q.Id == serviceRequestServiceId select q).SingleOrDefault();
                    }

                    if (serviceRequest.Update(newServiceRequest))
                    {
                        db.ServiceRequests.Attach(serviceRequest);
                        db.Entry(serviceRequest).State = System.Data.Entity.EntityState.Modified;

                        updatedItemCount++;
                    }
                }

                db.SaveChanges();

                result = "(" + readItemCount + "/?/" + insertedItemCount + "," + updatedItemCount + "," + deletedItemCount + ") ";
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateServiceRequestService(Ia.Ngn.Cl.Model.ServiceRequest serviceRequest, Ia.Ngn.Cl.Model.ServiceRequestService serviceRequestService, out string result)
        {
            bool b;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequest = (from q in db.ServiceRequests where q.Id == serviceRequest.Id select q).SingleOrDefault();

                if (serviceRequest.ServiceRequestService != serviceRequestService)
                {
                    serviceRequest.ServiceRequestService = (from q in db.ServiceRequestServices where q.Id == serviceRequestService.Id select q).SingleOrDefault();

                    db.ServiceRequests.Attach(serviceRequest);
                    db.Entry(serviceRequest).Property(x => x.ServiceRequestService).IsModified = true;

                    db.SaveChanges();

                    result = "Success: ServiceRequests ServiceRequestService updated. ";
                    b = true;
                }
                else
                {
                    result = "Warning: ServiceRequests ServiceRequestService value was not updated because its the same. ";

                    b = false;
                }
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void UpdateForADateTimeRangeWithOutputDataTable(DataTable dataTable, DateTime dateTime, out string result)
        {
            // below: the SQL statement should be within the dataTable.TableName variable
            int number, readItemCount, existingItemCount, insertedItemCount, updatedItemCount, deletedItemCount;
            int serviceRequestId;
            string sql, r, level;
            ArrayList newServiceRequestIdArryList;
            DateTime startDateTime, endDateTime;
            Match match;
            Ia.Ngn.Cl.Model.Business.ServiceAddress serviceAddress;
            Ia.Ngn.Cl.Model.ServiceRequest serviceRequest, newServiceRequest;
            List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList;
            List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList;

            readItemCount = existingItemCount = insertedItemCount = updatedItemCount = deletedItemCount = 0;
            result = r = "";

            startDateTime = DateTime.MinValue;

            if (dataTable != null)
            {
                sql = dataTable.TableName;

                // select * from SRV_REQ_FIPER where REQ_DATE >= 'dd/MM/yyyy' and REQ_DATE < 'dd/MM/yyyy' order by REQ_DATE ASC, SRV_REQ_ID ASC;
                // select * from SRV_REQ_FIPER where REQ_DATE >= '01-10-2006' and REQ_DATE < '02-10-2006' order by REQ_DATE asc, SRV_REQ_ID asc;

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

                        serviceRequestList = Ia.Ngn.Cl.Model.Data.ServiceRequest.ReadListWithinDateTimeRange(startDateTime, endDateTime);
                        existingItemCount = serviceRequestList.Count;

                        newServiceRequestIdArryList = new ArrayList(dataTable.Rows.Count + 1);

                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            number = int.Parse(dataRow["SRV_NO"].ToString()); ;

                            if (Ia.Ngn.Cl.Model.Business.Service.NumberIsWithinAllowedDomainList(number))
                            {
                                serviceRequestId = int.Parse(dataRow["SRV_REQ_ID"].ToString());

                                newServiceRequestIdArryList.Add(serviceRequestId);

                                newServiceRequest = new Ia.Ngn.Cl.Model.ServiceRequest();

                                newServiceRequest.Id = serviceRequestId;
                                newServiceRequest.Number = number;

                                newServiceRequest.CustomerAddress = dataRow["ADDRESS"].ToString();
                                serviceAddress = Ia.Ngn.Cl.Model.Business.ServiceRequest.StatisticalServiceAddress(number.ToString(), newServiceRequest.CustomerAddress, out level);
                                newServiceRequest.AreaId = serviceAddress.AreaId;

                                newServiceRequest.CustomerCategoryId = int.Parse(dataRow["CUST_CAT_ID"].ToString());
                                newServiceRequest.CustomerId = int.Parse(dataRow["ACCOUNT_NO"].ToString()); ;
                                newServiceRequest.CustomerName = Ia.Ngn.Cl.Model.Business.Default.CorrectCustomerName(dataRow["NAME"].ToString());
                                newServiceRequest.RequestDateTime = DateTime.Parse(dataRow["REQ_DATE"].ToString());
                                newServiceRequest.Serial = int.Parse(dataRow["SRV_SER_NO"].ToString()); ;
                                newServiceRequest.ServiceCategoryId = int.Parse(dataRow["SRV_CAT_ID"].ToString()); ;
                                newServiceRequest.ServiceId = int.Parse(dataRow["SRV_ID"].ToString());
                                newServiceRequest.Balance = double.Parse(dataRow["BALANCE"].ToString());
                                // newServiceRequest.ServiceRequestOnts = ;
                                // newServiceRequest.ServiceRequestTypes = ;
                                newServiceRequest.Status = int.Parse(dataRow["STATUS"].ToString()); ;

                                serviceRequest = (from q in serviceRequestList where q.Id == newServiceRequest.Id select q).SingleOrDefault();

                                if (serviceRequest == null)
                                {
                                    newServiceRequest.Created = newServiceRequest.Updated = newServiceRequest.Inspected = DateTime.UtcNow.AddHours(3);

                                    db.ServiceRequests.Add(newServiceRequest);

                                    insertedItemCount++;
                                }
                                else
                                {
                                    // below: copy values from newServiceRequest to serviceRequest

                                    if (serviceRequest.Update(newServiceRequest))
                                    {
                                        db.ServiceRequests.Attach(serviceRequest);
                                        db.Entry(serviceRequest).State = System.Data.Entity.EntityState.Modified;

                                        updatedItemCount++;
                                    }
                                }
                            }
                            else
                            {
                                r += "Number: " + number + " is not within allowed domain list, ";
                            }
                        }

                        // below: this function will remove values that were not present in the reading
                        if (serviceRequestList.Count > 0)
                        {
                            foreach (Ia.Ngn.Cl.Model.ServiceRequest sr in serviceRequestList)
                            {
                                if (!newServiceRequestIdArryList.Contains(sr.Id))
                                {
                                    serviceRequest = (from q in db.ServiceRequests where q.Id == sr.Id select q).SingleOrDefault();

                                    db.ServiceRequests.Remove(serviceRequest);

                                    // below: we will also remove SRT records referensing this SR
                                    serviceRequestTypeList = (from q in db.ServiceRequestTypes where q.ServiceRequest.Id == sr.Id select q).ToList();

                                    foreach (Ia.Ngn.Cl.Model.ServiceRequestType srt in serviceRequestTypeList) db.ServiceRequestTypes.Remove(srt);

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
        public static void UpdateForServiceRequetIdRangeWithOutputDataTable(DataTable dataTable, Tuple<int, int> startEndRange, out string result)
        {
            // below: the SQL statement should be within the dataTable.TableName variable
            int number, serviceRequestId, start, end, readItemCount, existingItemCount, insertedItemCount, updatedItemCount, deletedItemCount;
            string sql, r, level;
            ArrayList newServiceRequestIdArryList;
            Match match;
            Ia.Ngn.Cl.Model.Business.ServiceAddress serviceAddress;
            Ia.Ngn.Cl.Model.ServiceRequest serviceRequest, newServiceRequest;
            List<int> numbersNotWithinAllowedDomainList;
            List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList;
            List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList;

            readItemCount = existingItemCount = insertedItemCount = updatedItemCount = deletedItemCount = 0;
            result = r = "";
            numbersNotWithinAllowedDomainList = new List<int>();

            if (dataTable != null)
            {
                sql = dataTable.TableName;

                // select * from SRV_REQ_FIPER where SRV_REQ_ID >= 110000 and SRV_REQ_ID <= 321203 order by REQ_DATE asc, SRV_REQ_ID asc
                match = Regex.Match(sql, @"SRV_REQ_ID >= (\d+) and SRV_REQ_ID <= (\d+) ", RegexOptions.Singleline);
                //                                       1                       2

                if (match.Success)
                {
                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        readItemCount = dataTable.Rows.Count;

                        start = int.Parse(match.Groups[1].Value);
                        end = int.Parse(match.Groups[2].Value);

                        serviceRequestList = Ia.Ngn.Cl.Model.Data.ServiceRequest.ReadListWithinIdRange(start, end);
                        existingItemCount = serviceRequestList.Count;

                        newServiceRequestIdArryList = new ArrayList(dataTable.Rows.Count + 1);

                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            number = int.Parse(dataRow["SRV_NO"].ToString());

                            if (Ia.Ngn.Cl.Model.Business.Service.NumberIsWithinAllowedDomainList(number))
                            {
                                serviceRequestId = int.Parse(dataRow["SRV_REQ_ID"].ToString());

                                newServiceRequestIdArryList.Add(serviceRequestId);

                                serviceAddress = new ServiceAddress();
                                newServiceRequest = new Ia.Ngn.Cl.Model.ServiceRequest();

                                newServiceRequest.Id = serviceRequestId;
                                newServiceRequest.Number = number;

                                newServiceRequest.CustomerAddress = dataRow["ADDRESS"].ToString();
                                serviceAddress = Ia.Ngn.Cl.Model.Business.ServiceRequest.StatisticalServiceAddress(number.ToString(), newServiceRequest.CustomerAddress, out level);
                                newServiceRequest.AreaId = serviceAddress.AreaId;

                                newServiceRequest.CustomerCategoryId = int.Parse(dataRow["CUST_CAT_ID"].ToString());
                                newServiceRequest.CustomerId = int.Parse(dataRow["ACCOUNT_NO"].ToString()); ;
                                newServiceRequest.CustomerName = Ia.Ngn.Cl.Model.Business.Default.CorrectCustomerName(dataRow["NAME"].ToString());
                                newServiceRequest.RequestDateTime = DateTime.Parse(dataRow["REQ_DATE"].ToString());
                                newServiceRequest.Serial = int.Parse(dataRow["SRV_SER_NO"].ToString()); ;
                                newServiceRequest.ServiceCategoryId = int.Parse(dataRow["SRV_CAT_ID"].ToString()); ;
                                newServiceRequest.ServiceId = int.Parse(dataRow["SRV_ID"].ToString());
                                newServiceRequest.Balance = double.Parse(dataRow["BALANCE"].ToString());

                                // newServiceRequest.ServiceRequestOnts = ;
                                // newServiceRequest.ServiceRequestTypes = ;
                                newServiceRequest.Status = int.Parse(dataRow["STATUS"].ToString()); ;

                                serviceRequest = (from q in serviceRequestList where q.Id == newServiceRequest.Id select q).SingleOrDefault();

                                if (serviceRequest == null)
                                {
                                    newServiceRequest.Created = newServiceRequest.Updated = newServiceRequest.Inspected = DateTime.UtcNow.AddHours(3);

                                    db.ServiceRequests.Add(newServiceRequest);

                                    insertedItemCount++;
                                }
                                else
                                {
                                    // below: copy values from newServiceRequest to serviceRequest

                                    if (serviceRequest.UpdateSkipServiceRequestService(newServiceRequest))
                                    {
                                        db.ServiceRequests.Attach(serviceRequest);
                                        db.Entry(serviceRequest).State = System.Data.Entity.EntityState.Modified;

                                        updatedItemCount++;
                                    }
                                }
                            }
                            else
                            {
                                numbersNotWithinAllowedDomainList.Add(number);
                            }
                        }

                        /*
                        if (numbersNotWithinAllowedDomainList.Count > 0)
                        {
                            r = "Numbers not within allowed domain list: ";

                            foreach (int n in numbersNotWithinAllowedDomainList) r += n + ",";

                            r = r.Trim(',');
                        }
                        */

                        // below: this function will remove values that were not present in the reading
                        if (serviceRequestList.Count > 0)
                        {
                            foreach (Ia.Ngn.Cl.Model.ServiceRequest sr in serviceRequestList)
                            {
                                if (!newServiceRequestIdArryList.Contains(sr.Id))
                                {
                                    serviceRequest = (from q in db.ServiceRequests where q.Id == sr.Id select q).SingleOrDefault();

                                    db.ServiceRequests.Remove(serviceRequest);

                                    // below: we will also remove SRT records referensing this SR
                                    serviceRequestTypeList = (from q in db.ServiceRequestTypes where q.ServiceRequest.Id == sr.Id select q).ToList();

                                    foreach (Ia.Ngn.Cl.Model.ServiceRequestType srt in serviceRequestTypeList) db.ServiceRequestTypes.Remove(srt);

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
        /// Return a list of service requests that have numbers-serials within the passed list
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.ServiceRequest> ReadList(List<Ia.Ngn.Cl.Model.Business.ServiceRequest.NumberSerial> numberSerialList)
        {
            List<long> idList;
            List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList;

            idList = numberSerialList.IdList();

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestList = (from q in db.ServiceRequests.Include(a => a.ServiceRequestService)
                                      where
                                          //numberSerialList.Contains(q.Number, q.Serial) does not work
                                          //((from r in numberSerialList where r.Number == q.Number && r.Serial == q.Serial select r) != null) does not work
                                          //numberList.Any<int>(i=> i == q.Number)  does not work
                                      idList.Contains((long)q.Number * 100 + q.Serial)
                                      select q).ToList();
            }

            return serviceRequestList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Return a list of service requests that have numbers within the passed list
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.ServiceRequest> ReadList(List<int> numberList)
        {
            List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestList = (from q in db.ServiceRequests.Include(a => a.ServiceRequestService) where numberList.Contains(q.Number) select q).ToList();
            }

            return serviceRequestList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.ServiceRequest> ReadListWithinDateTimeRange(DateTime startDateTime, DateTime endDateTime)
        {
            List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestList = (from q in db.ServiceRequests.Include(a => a.ServiceRequestService) where q.RequestDateTime >= startDateTime && q.RequestDateTime < endDateTime select q).ToList();
            }

            return serviceRequestList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.ServiceRequest> ReadListWithinIdRange(int start, int end)
        {
            List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestList = (from q in db.ServiceRequests.Include(a => a.ServiceRequestService) where q.Id >= start && q.Id <= end select q).ToList();
            }

            return serviceRequestList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Hashtable NumberToCustomerAddressHashtable(List<int> domainList)
        {
            Hashtable ht;
            List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestList = (from sr in db.ServiceRequests where domainList.Contains(sr.Number/10000) select sr).ToList();

                ht = new Hashtable(serviceRequestList.Count);

                foreach (Ia.Ngn.Cl.Model.ServiceRequest sr in serviceRequestList)
                {
                    ht[sr.Number.ToString()] = sr.CustomerAddress;
                }
            }

            return ht;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// How to embed and access resources by using Visual C# http://support.microsoft.com/kb/319292/en-us
        /// 
        /// 1. Change the "Build Action" property of your XML file from "Content" to "Embedded Resource".
        /// 2. Add "using System.Reflection".
        /// 3. See sample below.
        /// 
        /// </summary>

        private static XDocument XDocument
        {
            get
            {
                if (xDocument == null)
                {
                    Assembly _assembly;
                    StreamReader streamReader;

                    _assembly = Assembly.GetExecutingAssembly();
                    streamReader = new StreamReader(_assembly.GetManifestResourceStream("Ia.Ngn.Cl.model.data.service-request.xml"));

                    try
                    {
                        if (streamReader.Peek() != -1)
                        {
                            xDocument = System.Xml.Linq.XDocument.Load(streamReader);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                    }
                }

                return xDocument;
            }
        }

        ////////////////////////////////////////////////////////////////////////////    
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}