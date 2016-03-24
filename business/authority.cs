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
    /// Authority support class of Next Generation Network'a (NGN's) business model.
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
    public class Authority
    {
        public enum PersistentStorageFunction { Create = 1, Read, Update, Delete };

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Authority() { }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.Administration.StaffFramework> StaffFrameworkListOfAllowedReportAssignsByStaff(Ia.Ngn.Cl.Model.Staff staff)
        {
            List<Ia.Ngn.Cl.Model.Data.Administration.StaffFramework> staffList, frameworkList, list;

            // staff list:
            if (staff.Subordinates != null && staff.Colleagues != null)
            {
                staffList = (from q in staff.Colleagues.Union(staff.Subordinates).Concat(new[] { staff.Head })
                             select new Ia.Ngn.Cl.Model.Data.Administration.StaffFramework
                             {
                                 Id = q.UserId,
                                 IsStaff = true,
                                 Name = q.FirstAndMiddleName
                             }
                ).ToList();
            }
            else if (staff.Colleagues != null)
            {
                staffList = (from q in staff.Colleagues.Concat(new[] { staff.Head })
                             select new Ia.Ngn.Cl.Model.Data.Administration.StaffFramework
                             {
                                 Id = q.UserId,
                                 IsStaff = true,
                                 Name = q.FirstAndMiddleName
                             }
                ).ToList();
            }
            else
            {
                staffList = (from q in (new[] { staff.Head })
                             select new Ia.Ngn.Cl.Model.Data.Administration.StaffFramework
                             {
                                 Id = q.UserId,
                                 IsStaff = true,
                                 Name = q.FirstAndMiddleName
                             }
                             ).ToList();
            }
            
            // framework list: framework decscendants, siblings and uncles (for head staff only)
            if (staff.Framework.Descendants != null && staff.Framework.Siblings != null)
            {
                if (staff.Framework.Parent.Siblings != null && staff.IsHead)
                {
                    frameworkList = (from q in staff.Framework.Siblings.Union(staff.Framework.Descendants).Union(staff.Framework.Parent.Siblings).Concat(new[] { staff.Framework.Parent })
                                     select new Ia.Ngn.Cl.Model.Data.Administration.StaffFramework
                                     {
                                         Id = q.Guid,
                                         IsFramework = true,
                                         Name = q.ArabicName
                                     }
                                     ).OrderBy(c => c.Id).ToList();
                }
                else
                {
                    frameworkList = (from q in staff.Framework.Siblings.Union(staff.Framework.Descendants).Concat(new[] { staff.Framework.Parent })
                                     select new Ia.Ngn.Cl.Model.Data.Administration.StaffFramework
                                     {
                                         Id = q.Guid,
                                         IsFramework = true,
                                         Name = q.ArabicName
                                     }
                                     ).OrderBy(c => c.Id).ToList();
                }
            }
            else if (staff.Framework.Descendants != null)
            {
                if (staff.Framework.Parent.Siblings != null && staff.IsHead)
                {
                    frameworkList = (from q in staff.Framework.Descendants.Union(staff.Framework.Parent.Siblings).Concat(new[] { staff.Framework.Parent })
                                     select new Ia.Ngn.Cl.Model.Data.Administration.StaffFramework
                                     {
                                         Id = q.Guid,
                                         IsFramework = true,
                                         Name = q.ArabicName
                                     }
                                     ).OrderBy(c => c.Id).ToList();
                }
                else
                {
                    frameworkList = (from q in staff.Framework.Descendants.Concat(new[] { staff.Framework.Parent })
                                     select new Ia.Ngn.Cl.Model.Data.Administration.StaffFramework
                                     {
                                         Id = q.Guid,
                                         IsFramework = true,
                                         Name = q.ArabicName
                                     }
                                     ).OrderBy(c => c.Id).ToList();
                }
            }
            else
            {
                if (staff.Framework.Parent.Siblings != null && staff.IsHead)
                {
                    frameworkList = (from q in staff.Framework.Parent.Siblings.Concat(new[] { staff.Framework.Parent })
                                     select new Ia.Ngn.Cl.Model.Data.Administration.StaffFramework
                                     {
                                         Id = q.Guid,
                                         IsFramework = true,
                                         Name = q.ArabicName
                                     }
                                     ).OrderBy(c => c.Id).ToList();
                }
                else
                {
                    frameworkList = (from q in (new[] { staff.Framework.Parent })
                                     select new Ia.Ngn.Cl.Model.Data.Administration.StaffFramework
                                     {
                                         Id = q.Guid,
                                         IsFramework = true,
                                         Name = q.ArabicName
                                     }
                                     ).OrderBy(c => c.Id).ToList();
                }
            }

            list = staffList.Union(frameworkList).ToList();

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static bool StaffCanCreateReadUpdateDeleteReport(Ia.Ngn.Cl.Model.Business.Authority.PersistentStorageFunction function, Ia.Ngn.Cl.Model.Report report, Ia.Ngn.Cl.Model.Staff staff)
        {
            bool isAllowed;
            
            if (function == PersistentStorageFunction.Delete)
            {
                // below: a report can only by:
                // - one of the heads of the reporter.
                // - the reporter if he is a head

                if (report.LastReportHistory == null && staff.UserId == report.UserId && staff.IsHead) isAllowed = true;
                else if (report.LastReportHistory != null && staff.UserId == report.LastReportHistory.UserId && staff.IsHead) isAllowed = true;
                else if (report.LastReportHistory == null && staff.IsHead) isAllowed = true;
                else if (staff.Subordinates != null)
                {
                    if (report.LastReportHistory != null) isAllowed = staff.Subordinates.Any(i => i.UserId == report.LastReportHistory.UserId);
                    else isAllowed = staff.Subordinates.Any(i => i.UserId == report.UserId);
                }
                else isAllowed = false;
            }
            else
            {
                // below: a report can be CRUD if
                // - last report does not exist
                // - last report exists and its UserId is same as staffs
                // - report owner is subordinate of staff
                // - report framework is within users frameworks

                if (report.ReportHistories.Count == 0)
                {
                    isAllowed = report.LastReportHistory.UserId == staff.UserId || staff.Subordinates != null && staff.Subordinates.Any(i => i.UserId == report.LastReportHistory.UserId);

                    if(Ia.Ngn.Cl.Model.Business.Administration.IsFrameworkGuid(report.LastReportHistory.UserId))
                    {
                        isAllowed = staff.Framework.Guid == report.LastReportHistory.UserId 
                            || staff.Framework.Descendants != null && staff.Framework.Descendants.Any(i => i.Guid == report.LastReportHistory.UserId);
                    }
                }
                else
                {
                    isAllowed = true; // report.UserId == staff.UserId || staff.Subordinates != null && staff.Subordinates.Any(i => i.UserId == report.UserId);

                    /*
                    if (Ia.Ngn.Cl.Model.Business.Administration.IsFrameworkGuid(report.UserId))
                    {
                        isAllowed = staff.Framework.Guid == report.UserId 
                            || staff.Framework.Descendants != null && staff.Framework.Descendants.Any(i => i.Guid == report.UserId);
                    }
                     */ 
                }
            }

            return isAllowed;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static bool StaffCanCreateReadUpdateDeleteReportHistory(Ia.Ngn.Cl.Model.Business.Authority.PersistentStorageFunction function, Ia.Ngn.Cl.Model.Report report, Ia.Ngn.Cl.Model.ReportHistory reportHistory, Ia.Ngn.Cl.Model.Staff staff)
        {
            // below: I have to pass both Report and ReportHistory objects seperatly

            bool isAllowed;

            if (function == PersistentStorageFunction.Delete)
            {
                // below: a report history can only be deleted by:
                // - one of the heads of the history user
                // - the history user if he is a head

                if (reportHistory != null)
                {
                    if (reportHistory.Report.ReportHistories.Max(r=>r.Id) != reportHistory.Id) isAllowed = false;
                    else 
                    {
                        if (staff.UserId == reportHistory.UserId && staff.IsHead) isAllowed = true;
                        else if (staff.Subordinates != null) isAllowed = staff.Subordinates.Any(i => i.UserId == reportHistory.UserId);
                        else isAllowed = false;
                    }
                }
                else isAllowed = false;
            }
            else if (function == PersistentStorageFunction.Create)
            {
                // below: for create reportHistory must be null

                // below: a report history can be created if:
                // - report userid is subordinate of staff
                // - report history userid framework is within staff frameworks

                if (report != null && reportHistory == null)
                {
                    if (report.ReportHistories.Count == 0 && staff.UserId == report.UserId) isAllowed = true;
                    else if (report.LastReportHistory != null && staff.UserId == report.LastReportHistory.UserId) isAllowed = true;
                    else if (report.ReportHistories.Count == 0 && report.StatusIsOpen) isAllowed = true;
                    else if (report.LastReportHistory != null && staff.Subordinates != null && staff.Subordinates.Any(i => i.UserId == report.LastReportHistory.UserId)) isAllowed = true;
                    else if (Ia.Ngn.Cl.Model.Business.Administration.IsFrameworkGuid(report.UserId) && (staff.Framework.Guid == report.UserId || staff.Framework.Descendants != null && staff.Framework.Descendants.Any(i => i.Guid == report.UserId))) isAllowed = true;
                    else if (Ia.Ngn.Cl.Model.Business.Administration.IsFrameworkGuid(report.LastReportHistory.UserId) && report.LastReportHistory != null && (staff.Framework.Guid == report.LastReportHistory.UserId || staff.Framework.Descendants != null && staff.Framework.Descendants.Any(i => i.Guid == report.LastReportHistory.UserId) || staff.Framework.Ancestors != null && staff.Framework.Ancestors.Any(i => i.Guid == report.LastReportHistory.UserId))) isAllowed = true;
                    else isAllowed = false;
                }
                else isAllowed = false;
            }
            else
            {
                // below: a report history can be read, and updated if:
                // - report history UserId is same as staff's
                // - report userid is subordinate of staff
                // - report history userid framework is within staff frameworks

                if (reportHistory != null)
                {
                    if (staff.UserId == reportHistory.UserId) isAllowed = true;
                    else if (staff.Subordinates != null && staff.Subordinates.Any(i => i.UserId == reportHistory.UserId)) isAllowed = true;
                    else if (Ia.Ngn.Cl.Model.Business.Administration.IsFrameworkGuid(reportHistory.UserId))
                    {
                        if (staff.Framework.Guid == reportHistory.UserId || staff.Framework.Descendants != null && staff.Framework.Descendants.Any(i => i.Guid == reportHistory.UserId)) isAllowed = true;
                        else isAllowed = false;
                    }
                    else isAllowed = false;
                }
                else isAllowed = false;
            }

            return isAllowed;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static bool StaffCanReopenClosedReport(Ia.Ngn.Cl.Model.Report report, Ia.Ngn.Cl.Model.Staff staff)
        {
            return StaffCanCloseReport(report, staff);
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static bool StaffCanCloseReport(Ia.Ngn.Cl.Model.Report report, Ia.Ngn.Cl.Model.Staff staff)
        {
            bool canClose;

            // below: a report can be closed if
            // - there must be a history
            // - the closer staff is himself, is the head or one of the heads of the last report staff

            if (report.LastReportHistory != null && staff.Subordinates != null)
            {
                if (staff.UserId == report.LastReportHistory.UserId) canClose = true;
                else canClose = staff.Subordinates.Any(i => i.UserId == report.LastReportHistory.UserId);
            }
            else canClose = false;

            return canClose;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool StaffCanCreateReadUpdateDeleteServiceLineCard(Ia.Ngn.Cl.Model.Business.Authority.PersistentStorageFunction function, Ia.Ngn.Cl.Model.Staff staff)
        {
            bool isAllowed;

            if (function == PersistentStorageFunction.Create)
            {
                if (staff.IsHead) isAllowed = true;
                else isAllowed = false;
            }
            else if (function == PersistentStorageFunction.Read)
            {
                isAllowed = true;
            }
            else if (function == PersistentStorageFunction.Update)
            {
                if (staff.IsHead) isAllowed = true;
                else isAllowed = false;
            }
            else if (function == PersistentStorageFunction.Delete)
            {
                if (staff.IsHead) isAllowed = true;
                else isAllowed = false;
            }
            else
            {
                isAllowed = false;
            }

            return isAllowed;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool StaffCanReadUpdateServiceSuspension(Ia.Ngn.Cl.Model.Business.Authority.PersistentStorageFunction function, Ia.Ngn.Cl.Model.Staff staff)
        {
            bool isAllowed;

            if (function == PersistentStorageFunction.Read)
            {
                isAllowed = true;
            }
            else if (function == PersistentStorageFunction.Update)
            {
                /*if (staff.IsHead) isAllowed = true;
                else*/ isAllowed = false;
            }
            else
            {
                isAllowed = false;
            }

            return isAllowed;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool StaffCanReadUpdateServiceRequestServiceAccess(Ia.Ngn.Cl.Model.Staff staff)
        {
            bool isAllowed;

            if (staff.IsHead && staff.FirstName == "جاسم") isAllowed = true;
            else isAllowed = false;

            return isAllowed;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
