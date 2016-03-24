using System;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ia.Ngn.Cl.Model.Business
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Administration support class of Next Generation Network'a (NGN's) business model.
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
    public class Administration
    {
        /// <summary/>
        public static DateTime OfficialStartOfWorkTime = DateTime.Parse("07:00");
        /// <summary/>
        public static DateTime OfficialEndOfWorkTime = DateTime.Parse("14:00");
        /// <summary/>
        public static DateTime EarliestServiceRequestDate = DateTime.Parse("2000-01-01");

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Statistic
        {
            /// <summary/>
            public Statistic() { }

            /// <summary/>
            public string Id { get; set; }
            /// <summary/>
            public string Name { get; set; }
            /// <summary/>
            public string Site { get; set; }
            /// <summary/>
            public string Symbol { get; set; }
            /// <summary/>
            public string KuwaitAreaNameListString { get; set; }
            /// <summary/>
            public string DomainListString { get; set; }
            /// <summary/>
            public string SymbolListString { get; set; }

            /// <summary/>
            public string Capacity { get; set; }
            /// <summary/>
            public string InstalledContractor { get; set; }
            /// <summary/>
            public string InstalledContractorWithSerial { get; set; }
            /// <summary/>
            public string Provisioned { get; set; }
            /// <summary/>
            public string ReadyForService { get; set; }
            /// <summary/>
            public string DefinedInCustomerDepartment { get; set; }
            /// <summary/>
            public string Utilized { get; set; }


            /// <summary/>
            public string ServiceRequests { get; set; }
            /// <summary/>
            public string ServiceRequestServices { get; set; }
            /// <summary/>
            public string Services { get; set; }
            /// <summary/>
            public string InternationalCalling { get; set; }
            /// <summary/>
            public string InternationalCallingUserControlled { get; set; }
            /// <summary/>
            public string CallWaiting { get; set; }
            /// <summary/>
            public string AlarmCall { get; set; }
            /// <summary/>
            public string CallBarring { get; set; }
            /// <summary/>
            public string CallerId { get; set; }
            /// <summary/>
            public string CallForwarding { get; set; }
            /// <summary/>
            public string ConferenceCall { get; set; }
            /// <summary/>
            public string ServiceSuspension { get; set; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Administration() { }

        /// <summary/>
        public static bool NowIsOfficialWorkingTime
        {
            get
            {
                bool b;
                DateTime now;

                now = DateTime.UtcNow.AddHours(3);

                b = now.DayOfWeek != DayOfWeek.Friday && now.DayOfWeek != DayOfWeek.Saturday && now.TimeOfDay >= OfficialStartOfWorkTime.TimeOfDay && now.TimeOfDay < OfficialEndOfWorkTime.TimeOfDay;

                return b;
            }
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        /// Tests a given Guid value to see if it has a special format used in framework items. Framework ites will have 00000000- prefixed to them
        /// </summary>
        public static bool IsFrameworkGuid(Guid guid)
        {
            bool isFrameworkGuid;

            isFrameworkGuid = (guid.ToString().Substring(0, 9) == "00000000-") ? true : false;

            return isFrameworkGuid;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.Administration.Framework> FrameworkListOfAllowedReportAssignsByStaff(Ia.Ngn.Cl.Model.Staff staff)
        {
            List<Ia.Ngn.Cl.Model.Data.Administration.Framework> frameworkList;

            frameworkList = new List<Ia.Ngn.Cl.Model.Data.Administration.Framework>();

            frameworkList.AddRange(staff.Framework.Children);
            frameworkList.AddRange(staff.Framework.Siblings);

            return frameworkList;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static bool StaffHasOpenPendingReportAndReportHistory(Ia.Ngn.Cl.Model.Staff staff)
        {
            bool hasOpenReport;
            List<Ia.Ngn.Cl.Model.Report> reportList;

            reportList = Ia.Ngn.Cl.Model.Data.Report.ReadOpenStatusAndClosedStatusWithinLast24HourReportList();

            reportList = Ia.Ngn.Cl.Model.Data.Report.ReadOpenStatusAndClosedStatusWithinLast24HourForSpecificUserIdOrHisFrameworkAncestorOrDescendantUserIdWithNoStaffHistoryReportOrHisSubordinateStaffReportList(reportList, staff);

            // I will exclude closed reports
            // <status id="1" name="Open" arabicName="مفتوح"/>
            // <status id="2" name="Closed" arabicName="مغلق"/>

            reportList = (from r in reportList where r.Status == 1 select r).ToList();

            hasOpenReport = (reportList.Count > 0) ? true : false;

            return hasOpenReport;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.Administration.StaffFramework> StaffFrameworkWithOpenStatusAndClosedStatusWithinLast24HourReportList(List<Ia.Ngn.Cl.Model.Report> defaultReportList)
        {
            Hashtable userIdHashtable;
            List<Guid> userIdList;
            List<Ia.Ngn.Cl.Model.Data.Administration.StaffFramework> list;
            List<Ia.Ngn.Cl.Model.Report> reportList;

            userIdHashtable = new Hashtable();
            userIdList = new List<Guid>();

            foreach (Ia.Ngn.Cl.Model.Data.Administration.StaffFramework sf in Ia.Ngn.Cl.Model.Data.Administration.StaffFrameworkList)
            {
                if (sf.IsStaff)
                {
                    reportList = Ia.Ngn.Cl.Model.Data.Report.ReadOpenStatusAndClosedStatusWithinLast24HourForSpecificUserIdOrHisFrameworkAncestorOrDescendantUserIdWithNoStaffHistoryReportOrHisSubordinateStaffReportList(defaultReportList, sf.Id);

                    if (reportList.Count > 0) userIdHashtable[sf.Id] = 1;
                }
                else //if (sf.IsFramework)
                {
                    reportList = Ia.Ngn.Cl.Model.Data.Report.ReadOpenStatusAndClosedStatusWithinLast24HourForSpecificFrameworkUserIdOrItsFrameworkAncestorOrDescendantFrameworkUserIdWithNoStaffHistoryReportList(defaultReportList, sf.Id);

                    if (reportList.Count > 0) userIdHashtable[sf.Id] = 1;
                }
            }

            foreach (Guid userId in userIdHashtable.Keys) userIdList.Add(userId);

            list = (from q in userIdList
                    join sf in Ia.Ngn.Cl.Model.Data.Administration.StaffFrameworkList on q equals sf.Id
                    select sf).OrderByDescending(c => c.IsStaff).ThenBy(c => c.FrameworkId).ToList();

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
