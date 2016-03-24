using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ia.Ngn.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Staff support class for Next Generation Network (NGN) data model.
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
    public partial class Staff
    {
        private static List<Ia.Ngn.Cl.Model.Staff> staffList;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Staff> List
        {
            get
            {
                if (staffList == null || staffList.Count == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["staffList"] != null)
                    {
                        staffList = (List<Ia.Ngn.Cl.Model.Staff>)HttpContext.Current.Application["staffList"];
                    }
                    else
                    {
                        staffList = Ia.Ngn.Cl.Model.Data.Staff._List;

                        if (HttpContext.Current != null) HttpContext.Current.Application["staffList"] = staffList;
                    }
                }

                return staffList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Staff> _List
        {
            get
            {
                if (staffList == null || staffList.Count == 0)
                {
                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        staffList = (from q in db.Staff select q).ToList();

                        // below: the order of the multiple foreach loops is imporant

                        foreach (Ia.Ngn.Cl.Model.Staff staff in staffList)
                        {
                            staff.User = (from q in Ia.Cl.Model.Identity.UserList where staff.UserId != Guid.Empty && q.ProviderUserKey == staff.UserId select q).SingleOrDefault();

                            // below: Head & Framework
                            staff.Framework = (from q in Ia.Ngn.Cl.Model.Data.Administration.FrameworkList where q.Id == staff.AdministrativeFrameworkId select q).SingleOrDefault();
                        }

                        foreach (Ia.Ngn.Cl.Model.Staff staff in staffList)
                        {
                            if (staff.Framework != null && staff.Framework.Parent != null)
                            {
                                staff.Head = (from q in staffList
                                              where q.IsHead == true && (
                                              staff.IsHead == true && q.AdministrativeFrameworkId == staff.Framework.Parent.Id
                                              || staff.IsHead == false && q.AdministrativeFrameworkId == staff.Framework.Id)
                                              select q).SingleOrDefault();
                            }
                        }

                        foreach (Ia.Ngn.Cl.Model.Staff staff in staffList)
                        {
                            // below: Subordinates
                            if (staff.IsHead)
                            {
                                staff.Subordinates = (from q in staffList
                                                      where q.Id != staff.Id &&
                                                      (q.Framework == staff.Framework && q.IsHead == false)
                                                      ||
                                                      (
                                                      q.Framework.Parent != null &&
                                                      (q.Framework.Parent == staff.Framework || q.Framework.Parent.Parent != null &&
                                                       (q.Framework.Parent.Parent == staff.Framework || q.Framework.Parent.Parent.Parent != null &&
                                                        (q.Framework.Parent.Parent.Parent == staff.Framework || q.Framework.Parent.Parent.Parent != null &&
                                                         (q.Framework.Parent.Parent.Parent.Parent == staff.Framework)
                                                        )
                                                       )
                                                      )
                                                      )
                                                      
                                                      select q).ToList();
                            }
                            else staff.Subordinates = null;

                            // below: Colleagues
                            staff.Colleagues = (from q in staffList where q.Id != staff.Id && q.Head != null && staff.Head != null && q.Head == staff.Head select q).ToList();

                            // below: Heads
                            if (staff.Head != null)
                            {
                                if (staff.Head.Head != null)
                                {
                                    if (staff.Head.Head.Head != null)
                                    {
                                        if (staff.Head.Head.Head.Head != null) staff.Heads = (from q in staffList where q == staff.Head || q == staff.Head.Head || q == staff.Head.Head.Head || q == staff.Head.Head.Head.Head select q).ToList();
                                        else staff.Heads = (from q in staffList where q == staff.Head || q == staff.Head.Head || q == staff.Head.Head.Head select q).ToList();
                                    }
                                    else staff.Heads = (from q in staffList where q == staff.Head || q == staff.Head.Head select q).ToList();
                                }
                                else staff.Heads = (from q in staffList where q == staff.Head select q).ToList();
                            }
                            else staff.Heads = null;
                        }
                    }

                    staffList = (from q in staffList select q).OrderByDescending(c => c.IsHead).ThenBy(c => c.AdministrativeFrameworkId).ToList();
                }

                return staffList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Staff SessionStateStaff(System.Web.UI.Page page)
        {
            Guid userId;
            Ia.Ngn.Cl.Model.Staff staff;

            if (page.Session["staff"] != null) staff = (Ia.Ngn.Cl.Model.Staff)page.Session["staff"];
            else
            {
                userId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

                staff = (from q in Ia.Ngn.Cl.Model.Data.Staff.List where q.UserId == userId select q).SingleOrDefault();
                page.Session["staff"] = staff;
            }

            return staff;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Staff MembershipUser
        {
            get
            {
                Ia.Ngn.Cl.Model.Staff staffMembershipUser;

                staffMembershipUser = Ia.Ngn.Cl.Model.Data.Staff._MembershipUser;

                return staffMembershipUser;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static Ia.Ngn.Cl.Model.Staff _MembershipUser
        {
            get
            {
                Ia.Ngn.Cl.Model.Staff staffMembershipUser;

                if (Membership.GetUser() != null)
                {
                    staffMembershipUser = (from q in Ia.Ngn.Cl.Model.Data.Staff.List where q.User != null && q.UserId == Guid.Parse(Membership.GetUser().ProviderUserKey.ToString()) select q).SingleOrDefault();
                }
                else staffMembershipUser = null;

                return staffMembershipUser;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Staff MembershipUser2(Guid userId)
        {
            Ia.Ngn.Cl.Model.Staff staff;

            if (userId != Guid.Empty)
            {
                staff = (from q in Ia.Ngn.Cl.Model.Data.Staff.List where q.UserId == userId select q).SingleOrDefault();
            }
            else staff = null;

            return staff;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool NullifyUserId(int staffId)
        {
            bool b;
            Ia.Ngn.Cl.Model.Staff staff;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                staff = (from q in db.Staff where q.Id == staffId select q).SingleOrDefault();

                staff.UserId = Guid.Empty;

                db.Staff.Attach(staff);
                db.Entry(staff).Property(x => x.UserId).IsModified = true;

                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}