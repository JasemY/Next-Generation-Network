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
using System.Web.Security;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ia.Ngn.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Administration support class for Next Generation Network (NGN) data model.
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
        private static XDocument xDocument;
        private static List<Framework> frameworkList;
        private static List<Category> categoryList;
        private static List<StaffFramework> staffFrameworkList;
        private static List<Authority> authorityList;
        private static Ia.Cl.Model.Db.SqlServer sqlServer;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Administration() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Framework
        {
            public Framework() { }

            /// <summary/>
            public int Id { get; set; }
            /// <summary/>
            public int Level { get; set; }
            /// <summary/>
            public Guid Guid { get; set; }
            /// <summary/>
            public string Type { get; set; }
            /// <summary/>
            public string Name { get; set; }
            /// <summary/>
            public string ArabicName { get; set; }
            /// <summary/>
            public string ColoredArabicName { get; set; }
            /// <summary/>
            public Framework Parent { get; set; }
            /// <summary/>
            public List<Framework> Ancestors { get; set; }
            /// <summary/>
            public List<Framework> Siblings { get; set; }
            /// <summary/>
            public List<Framework> Children { get; set; }
            /// <summary/>
            public List<Framework> Descendants { get; set; }
            /// <summary/>
            public List<Authority> Authorities { get; set; }

            /// <summary/>
            //public Ia.Cl.Model.Identity.User Role { get; set; }

            /// <summary/>
            public static bool operator ==(Framework fa, Framework fb)
            {
                bool b;

                // if both are null, or both are same instance, return true.
                if (System.Object.ReferenceEquals(fa, fb)) b = true;
                // if one is null, but not both, return false.
                else if (((object)fa == null) || ((object)fb == null)) b = false;
                // true if the fields match:
                else b = fa.Id == fb.Id;

                return b;
            }

            /// <summary/>
            public static bool operator !=(Framework fa, Framework fb)
            {
                return !(fa == fb);
            }

            /// <summary/>
            public int FrameworkId(int parentId, int frameworkId)
            {
                return parentId * 10 + frameworkId;
            }

            /// <summary/>
            public int ParentId(long frameworkId)
            {
                int i;
                string s;

                s = frameworkId.ToString();

                if (s.Length > 2)
                {
                    s = s.Substring(0, s.Length - 2);

                    i = (int.TryParse(s, out i)) ? i : 0;
                }
                else i = 0;

                return i;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class StaffContact
        {
            /// <summary/>
            public StaffContact() { }

            /// <summary/>
            public string Name { get; set; }
            /// <summary/>
            public string Email { get; set; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Category
        {
            /// <summary/>
            public Category() { }
            /// <summary/>
            public string Name { get; set; }
            /// <summary/>
            public string Description { get; set; }
            /// <summary/>
            public string Regex { get; set; }
            /// <summary/>
            public string Color { get; set; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class StaffFramework
        {
            private bool isStaff;

            /// <summary/>
            public StaffFramework() { }
            /// <summary/>
            public Guid Id { get; set; }
            /// <summary/>
            public long FrameworkId { get; set; }
            /// <summary/>
            public string Name { get; set; }
            /// <summary/>
            public bool IsStaff
            {
                get { return isStaff; }
                set { isStaff = true; }
            }
            /// <summary/>
            public bool IsFramework
            {
                get { return !isStaff; }
                set { isStaff = false; }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Authority
        {
            public Authority() { }

            /// <summary/>
            public int Id { get; set; }
            /// <summary/>
            public string Name { get; set; }
            /// <summary/>
            public string ArabicName { get; set; }
            /// <summary/>
            public string AllowedFrameworkArabicName { get; set; }
            /// <summary/>
            public bool HeadOnly { get; set; }
            /// <summary/>
            public string Medium { get; set; }
            /// <summary/>
            public string System { get; set; }
            /// <summary/>
            public string Process { get; set; }
            /// <summary/>
            public string Function { get; set; }
            /// <summary/>
            public string ParameterRegex { get; set; }
            /// <summary/>
            public string ResponseRegex { get; set; }
            /// <summary/>
            public string Help { get; set; }

            /// <summary/>
            public int AuthorityId(int parentId, int authorityId)
            {
                return parentId * 10 + authorityId;
            }

            /// <summary/>
            public int ParentId(long authorityId)
            {
                int i;
                string s;

                s = authorityId.ToString();

                if (s.Length > 2)
                {
                    s = s.Substring(0, s.Length - 2);

                    i = (int.TryParse(s, out i)) ? i : 0;
                }
                else i = 0;

                return i;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.Administration.Framework> FrameworkList
        {
            get
            {
                if (frameworkList == null || frameworkList.Count == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["frameworkList"] != null)
                    {
                        frameworkList = (List<Ia.Ngn.Cl.Model.Data.Administration.Framework>)HttpContext.Current.Application["frameworkList"];
                    }
                    else
                    {
                        frameworkList = Ia.Ngn.Cl.Model.Data.Administration._FrameworkList;

                        if (HttpContext.Current != null) HttpContext.Current.Application["frameworkList"] = frameworkList;
                    }
                }

                return frameworkList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Data.Administration.Framework> _FrameworkList
        {
            get
            {
                if (frameworkList == null || frameworkList.Count == 0)
                {
                    int parentId;
                    Framework framework;

                    frameworkList = new List<Framework>();

                    foreach (XElement xe in XDocument.Element("administration").Elements("frameworkList").Descendants("framework"))
                    {
                        framework = new Framework();

                        framework.Id = int.Parse(XmlBasedTwoDigitPerId(xe));
                        framework.Level = xe.Ancestors().Count();
                        framework.Guid = Guid.Parse(xe.Attribute("guid").Value);

                        parentId = framework.ParentId(framework.Id);
                        framework.Parent = (from q in frameworkList where q.Id == parentId select q).SingleOrDefault();

                        framework.Type = xe.Attribute("type").Value;
                        framework.Name = xe.Attribute("name").Value;
                        framework.ArabicName = xe.Attribute("arabicName").Value;
                        framework.ColoredArabicName = @"<span style=""color:" + Ia.Ngn.Cl.Model.Ui.Default.LightBackgroundColorList[framework.Id % Ia.Ngn.Cl.Model.Ui.Default.LightBackgroundColorList.Count].ToString() + @""">" + framework.ArabicName + "</span>";

                        framework.Authorities = new List<Ia.Ngn.Cl.Model.Data.Administration.Authority>();
                        framework.Authorities = (from q in AuthorityList where q.AllowedFrameworkArabicName == framework.ArabicName select q).ToList();

                        framework.Children = new List<Ia.Ngn.Cl.Model.Data.Administration.Framework>();
                        if (framework.Parent != null) framework.Parent.Children.Add(framework);

                        frameworkList.Add(framework);
                    }

                    // below: Siblings
                    foreach(Framework f in frameworkList)
                    {
                        f.Siblings = new List<Ia.Ngn.Cl.Model.Data.Administration.Framework>();

                        f.Siblings = (from q in frameworkList where q.Parent == f.Parent && q.Id != f.Id select q).ToList();
                    }

                    // below: Descendants
                    foreach (Framework f in frameworkList)
                    {
                        f.Descendants = new List<Ia.Ngn.Cl.Model.Data.Administration.Framework>();

                        f.Descendants = (from q in frameworkList
                                         where q.Id != f.Id && q.Id > f.Id &&
                                             (q.Parent != null && (q.Parent == f
                                             || q.Parent.Parent != null && (q.Parent.Parent == f
                                             || q.Parent.Parent.Parent != null && (q.Parent.Parent.Parent == f
                                             || q.Parent.Parent.Parent.Parent != null && q.Parent.Parent.Parent.Parent == f)))
                                             )
                                         select q).ToList();
                    }

                    // below: Ancestors
                    foreach (Framework f in frameworkList)
                    {
                        f.Ancestors = new List<Ia.Ngn.Cl.Model.Data.Administration.Framework>();

                        f.Ancestors = (from q in frameworkList
                                         where q.Id != f.Id && q.Id < f.Id &&
                                             (f.Parent != null && (f.Parent == q
                                             || f.Parent.Parent != null && (f.Parent.Parent == q
                                             || f.Parent.Parent.Parent != null && (f.Parent.Parent.Parent == q
                                             || f.Parent.Parent.Parent.Parent != null && f.Parent.Parent.Parent.Parent == q)))
                                             )
                                         select q).ToList();
                    }
                }

                //list = (from q in list select q).OrderByDescending(c => c.IsHead).ThenBy(c => c.AdministrativeFrameworkId).ToList();

                //frameworkList = (from q in frameworkList select q).OrderBy(c => c.Id).ToList(); //.ThenBy(c => c.ParentId).ToList();

                return frameworkList.ToList();
            }
        }

        private static string XmlBasedOneDigitPerId(XElement xeIn)
        {
            return XmlBasedId(xeIn, 1);
        }

        private static string XmlBasedTwoDigitPerId(XElement xeIn)
        {
            return XmlBasedId(xeIn, 2);
        }

        private static string XmlBasedId(XElement xe, int digit)
        {
            string id;

            id = "";

            while (xe.HasAttributes && xe.Attribute("id") != null)
            {
                id = xe.Attribute("id").Value.PadLeft(digit, '0') + id;
                xe = xe.Parent;
            }

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.Administration.Category> CategoryList
        {
            get
            {
                if (categoryList == null || categoryList.Count == 0)
                {
                    Category category;

                    categoryList = new List<Category>();

                    foreach (XElement xe in XDocument.Element("administration").Elements("category"))
                    {
                        category = new Category();

                        category.Name = xe.Attribute("name").Value;
                        category.Regex = xe.Attribute("regex").Value;

                        category.Description = (xe.Attribute("description") != null) ? xe.Attribute("description").Value : string.Empty;
                        category.Color = (xe.Attribute("color") != null) ? xe.Attribute("color").Value : string.Empty;

                        categoryList.Add(category);
                    }
                }

                return categoryList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.Administration.StaffContact> StaffContactWithEmailList
        {
            get
            {
                StaffContact staffContact;
                List<StaffContact> staffContactList;

                staffContactList = new List<StaffContact>();

                foreach(Ia.Ngn.Cl.Model.Staff staff in Ia.Ngn.Cl.Model.Data.Staff.List)
                {
                    if (staff.User != null && staff.User.Email != null)
                    {
                        staffContact = new StaffContact();

                        staffContact.Name = staff.FullName;
                        staffContact.Email = staff.User.Email;

                        staffContactList.Add(staffContact);
                    }
                }

                foreach (Ia.Ngn.Cl.Model.Contact contact in Ia.Ngn.Cl.Model.Contact.List())
                {
                    if (contact.Email != null)
                    {
                        staffContact = new StaffContact();

                        staffContact.Name = contact.FullName;
                        staffContact.Email = contact.Email;

                        staffContactList.Add(staffContact);
                    }
                }

                return staffContactList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.Administration.StaffFramework> StaffFrameworkList
        {
            get
            {
                if (staffFrameworkList == null || staffFrameworkList.Count == 0)
                {
                    List<Ia.Ngn.Cl.Model.Data.Administration.StaffFramework> staffList, frameworkList;

                    staffList = (from q in Ia.Ngn.Cl.Model.Data.Staff.List
                                 select new Ia.Ngn.Cl.Model.Data.Administration.StaffFramework
                                 {
                                     Id = q.UserId,
                                     FrameworkId = q.Framework.Id,
                                     IsStaff = true,
                                     Name = q.FirstAndMiddleName
                                 }
                    ).ToList();

                    frameworkList = (from q in Ia.Ngn.Cl.Model.Data.Administration.FrameworkList
                                     select new Ia.Ngn.Cl.Model.Data.Administration.StaffFramework
                                     {
                                         Id = q.Guid,
                                         FrameworkId = q.Id,
                                         IsFramework = true,
                                         Name = q.ArabicName
                                     }
                    ).ToList();

                    staffFrameworkList = staffList.Union(frameworkList).ToList();
                }

                return staffFrameworkList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.Administration.Authority> AuthorityList
        {
            get
            {
                if (authorityList == null || authorityList.Count == 0)
                {
                    Authority authority;

                    authorityList = new List<Authority>();

                    foreach (XElement xe in XDocument.Element("administration").Element("authorization").Element("authorityList").Descendants("authority"))
                    {
                        authority = new Authority();

                        authority.Id = int.Parse(XmlBasedTwoDigitPerId(xe));
                        authority.Name = xe.Attribute("name").Value;
                        authority.ArabicName = xe.Attribute("arabicName").Value;
                        authority.AllowedFrameworkArabicName = xe.Attribute("allowedFrameworkArabicName").Value;
                        authority.HeadOnly = (xe.Attribute("allowedHeadOnly").Value == "true")? true:false;
                        authority.Medium = xe.Attribute("medium").Value;
                        authority.System = xe.Attribute("system").Value;
                        authority.Process = xe.Attribute("process").Value;
                        authority.Function = xe.Attribute("function").Value;

                        authority.ParameterRegex = xe.Element("parameterRegex").Value;
                        authority.ResponseRegex = xe.Element("responseRegex").Value;

                        authority.Help = xe.Element("help").Value;

                        authorityList.Add(authority);
                    }
                }

                return authorityList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int ServiceRequestServicesWithNullAccessCount()
        {
            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                return (from srs in db.ServiceRequestServices where srs.Access == null select srs.Id).Count();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int ServicesWithNullAccessCount()
        {
            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                return (from s in db.Service2s where s.Access == null select s.Id).Count();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Business.Administration.Statistic> SiteStatistic()
        {
            int siteId;
            Ia.Ngn.Cl.Model.Business.Administration.Statistic statistic;
            List<Ia.Ngn.Cl.Model.Access> mainAccessList, accessList;
            List<Ia.Ngn.Cl.Model.Ont> mainOntList, ontList;
            List<Ia.Ngn.Cl.Model.Business.Administration.Statistic> statisticList;

            statisticList = new List<Ia.Ngn.Cl.Model.Business.Administration.Statistic>();

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                mainAccessList = (from a in db.Accesses select a).ToList();
                mainOntList = (from o in db.Onts select o).ToList();

                // site
                foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Site site in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.SiteList)
                {
                    statistic = new Ia.Ngn.Cl.Model.Business.Administration.Statistic();

                    siteId = site.Id;
                    statistic.Id = site.Id.ToString();
                    statistic.Name = site.NameArabicName;

                    foreach (Ia.Ngn.Cl.Model.Business.Service.KuwaitNgnArea kuwaitNgnArea in site.KuwaitNgnAreas) statistic.KuwaitAreaNameListString += kuwaitNgnArea.ArabicName + ", ";
                    if (statistic.KuwaitAreaNameListString != null && statistic.KuwaitAreaNameListString.Length > 0) statistic.KuwaitAreaNameListString = statistic.KuwaitAreaNameListString.Trim(',', ' ');

                    foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Router router in site.Routers) statistic.DomainListString += router.DomainListString + ",";
                    if (statistic.DomainListString != null && statistic.DomainListString.Length > 0) statistic.DomainListString = statistic.DomainListString.Trim(',', ' ');

                    foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList)
                    {
                        if(olt.Odf.Router.Site.Id == siteId)
                        {
                            if (string.IsNullOrEmpty(statistic.SymbolListString) || !statistic.SymbolListString.Contains(olt.Symbol)) statistic.SymbolListString += olt.Symbol + ", ";
                        }
                    }
                    
                    if (statistic.SymbolListString != null && statistic.SymbolListString.Length > 0) statistic.SymbolListString = statistic.SymbolListString.Trim(',', ' ');

                    statistic.Capacity = ((from o in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList where o.Odf.Router.Site.Id == siteId select o).Count() * 1024).ToString();

                    //accessList = (from a in mainAccessList where site.Routers.SelectMany(u => u.Odfs.Contains(a.Odf)) select a).ToList();
                    //ontList = (from o in mainOntList where o.Access != null && o.Access.Odf == odf.Id.ToString() select o).ToList();

                    /*
                    foreach (Ia.Ngn.Cl.Model.Access access in accessList)
                    {
                    }
                     */

                    statisticList.Add(statistic);
                }

                // kuwait area
                // odf
                // olt

            }

            return statisticList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Business.Administration.Statistic> OdfStatistic()
        {
            Ia.Ngn.Cl.Model.Business.Administration.Statistic statistic;
            List<Ia.Ngn.Cl.Model.Access> mainAccessList, accessList;
            List<Ia.Ngn.Cl.Model.Ont> mainOntList, ontList;
            List<Ia.Ngn.Cl.Model.Business.Administration.Statistic> statisticList;

            statisticList = new List<Ia.Ngn.Cl.Model.Business.Administration.Statistic>();

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                mainAccessList = (from a in db.Accesses select a).ToList();
                mainOntList = (from o in db.Onts select o).ToList();

                foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Odf odf in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OdfList)
                {
                    statistic = new Ia.Ngn.Cl.Model.Business.Administration.Statistic();

                    statistic.Id = odf.Id.ToString();
                    statistic.Name = odf.Name;
                    statistic.Site = odf.Router.Site.NameArabicName;
                    statistic.Symbol = odf.Name;

                    accessList = (from a in mainAccessList where a.Odf == odf.Id.ToString() select a).ToList();
                    ontList = (from o in mainOntList where o.Access != null && o.Access.Odf == odf.Id.ToString() select o).ToList();

                    foreach (Ia.Ngn.Cl.Model.Access access in accessList)
                    {
                    }

                    statistic.Capacity = "1024";
                    statistic.InstalledContractor = accessList.Count().ToString();
                    statistic.InstalledContractorWithSerial = (from o in ontList where o.Serial != null select o).Count().ToString();

                    statisticList.Add(statistic);
                }

                /*
                accessStatisticList = (from a in Ia.Ngn.Cl.Model.Data.Service.KuwaitNgnAreaList
                     group a by a.Id into grp
                     orderby grp.Key
                     select new Ia.Ngn.Cl.Model.Business.Administration.Statistic()
                     {
                         Id = grp.Key.ToString(),
                         Name = grp.SingleOrDefault().NameArabicName,
                         --InstalledContractor = (from a in db.Accesses where a.AreaId == grp.Key select a.Id).Count().ToString(),
                         InstalledContractorWithSerial = (from a in db.Accesses join o in db.Onts on a.Id equals o.Access.Id where a.AreaId == grp.Key && o.Serial != null select a.Id).Count().ToString(),
                         Provisioned = (from a in db.Accesses join o in db.Onts on a.Id equals o.Access.Id where a.AreaId == grp.Key && o.Serial != "ALCL00000000" && o.StateId != 0 && o.ActiveSoftware == o.PlannedSoftware select a.Id).Count().ToString(),
                         ReadyForService = (from a in db.Accesses join o in db.Onts on a.Id equals o.Access.Id where a.AreaId == grp.Key && o.Serial != "ALCL00000000" && o.StateId == 1 && o.ActiveSoftware == o.PlannedSoftware select a.Id).Count().ToString(),
                         DefinedInCustomerDepartment = (from a in db.Accesses join o in db.Onts on a.Id equals o.Access.Id join sro in db.ServiceRequestOnts on a.Id equals sro.Access.Id where a.AreaId == grp.Key && o.Serial != "ALCL00000000" && o.StateId == 1 && o.ActiveSoftware == o.PlannedSoftware select a.Id).Count().ToString(),
                         Utilized = (from a in db.Accesses join o in db.Onts on a.Id equals o.Access.Id 
                                       join sro in db.ServiceRequestOnts on a.Id equals sro.Access.Id 
                                       join srs in db.ServiceRequestServices on a.Id equals srs.Access.Id
                                       where a.AreaId == grp.Key && o.Serial != "ALCL00000000" && o.StateId == 1 && o.ActiveSoftware == o.PlannedSoftware select a.Id).Count().ToString()
                     }).ToList();
                 */

                /*
                if (dt != null)
                {
                    // below: the totals rows

                    dr = dt.NewRow();
                    dr["capacity"] = dt.Compute("SUM (capacity)", "").ToString();
                    dr["installed_contractor"] = dt.Compute("SUM (installed_contractor)", "").ToString();
                    dr["installed_contractor_with_serial"] = dt.Compute("SUM (installed_contractor_with_serial)", "").ToString();
                    dr["provisioned"] = dt.Compute("SUM (provisioned)", "").ToString();
                    dr["ont_ready_for_service"] = dt.Compute("SUM (ont_ready_for_service)", "").ToString();
                    dr["ont_defined_cs"] = dt.Compute("SUM (ont_defined_cs)", "").ToString();
                    dt.Rows.Add(dr);
                }
                 */
            }

            return statisticList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Business.Administration.Statistic> OltStatistic()
        {
            Ia.Ngn.Cl.Model.Business.Administration.Statistic statistic;
            List<Ia.Ngn.Cl.Model.Access> mainAccessList, accessList;
            List<Ia.Ngn.Cl.Model.Ont> mainOntList, ontList;
            List<Ia.Ngn.Cl.Model.Business.Administration.Statistic> statisticList;

            statisticList = new List<Ia.Ngn.Cl.Model.Business.Administration.Statistic>();

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                /*
                var v =
                    (from a in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.SiteList
                     group a by a.Id into grp
                     orderby grp.Key
                     select new
                     {
                         Id = grp.Key,
                         Name = grp.SingleOrDefault().NameArabicName,
                         Capacity = grp.Sum(w => w.Routers.SelectMany(x => x.Odfs.SelectMany(y => y.Olts)).Count()) * 1024,
                     })
                    .ToList();
                 */

                mainAccessList = (from a in db.Accesses select a).ToList();
                mainOntList = (from o in db.Onts select o).ToList();

                foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList)
                {
                    statistic = new Ia.Ngn.Cl.Model.Business.Administration.Statistic();

                    statistic.Id = olt.Id.ToString();
                    statistic.Name = olt.Name;
                    statistic.Site = olt.Odf.Router.Site.NameArabicName;
                    statistic.Symbol = olt.Symbol;

                    accessList = (from a in mainAccessList where a.Olt == olt.Id select a).ToList();
                    ontList = (from o in mainOntList where o.Access != null && o.Access.Olt == olt.Id select o).ToList();

                    foreach (Ia.Ngn.Cl.Model.Access access in accessList)
                    {
                    }

                    statistic.Capacity = "1024";
                    statistic.InstalledContractor = accessList.Count().ToString();
                    statistic.InstalledContractorWithSerial = (from o in ontList where o.Serial != null select o).Count().ToString();

                    statisticList.Add(statistic);
                }

                /*
                accessStatisticList = (from a in Ia.Ngn.Cl.Model.Data.Service.KuwaitNgnAreaList
                     group a by a.Id into grp
                     orderby grp.Key
                     select new Ia.Ngn.Cl.Model.Business.Administration.Statistic()
                     {
                         Id = grp.Key.ToString(),
                         Name = grp.SingleOrDefault().NameArabicName,
                         --InstalledContractor = (from a in db.Accesses where a.AreaId == grp.Key select a.Id).Count().ToString(),
                         InstalledContractorWithSerial = (from a in db.Accesses join o in db.Onts on a.Id equals o.Access.Id where a.AreaId == grp.Key && o.Serial != null select a.Id).Count().ToString(),
                         Provisioned = (from a in db.Accesses join o in db.Onts on a.Id equals o.Access.Id where a.AreaId == grp.Key && o.Serial != "ALCL00000000" && o.StateId != 0 && o.ActiveSoftware == o.PlannedSoftware select a.Id).Count().ToString(),
                         ReadyForService = (from a in db.Accesses join o in db.Onts on a.Id equals o.Access.Id where a.AreaId == grp.Key && o.Serial != "ALCL00000000" && o.StateId == 1 && o.ActiveSoftware == o.PlannedSoftware select a.Id).Count().ToString(),
                         DefinedInCustomerDepartment = (from a in db.Accesses join o in db.Onts on a.Id equals o.Access.Id join sro in db.ServiceRequestOnts on a.Id equals sro.Access.Id where a.AreaId == grp.Key && o.Serial != "ALCL00000000" && o.StateId == 1 && o.ActiveSoftware == o.PlannedSoftware select a.Id).Count().ToString(),
                         Utilized = (from a in db.Accesses join o in db.Onts on a.Id equals o.Access.Id 
                                       join sro in db.ServiceRequestOnts on a.Id equals sro.Access.Id 
                                       join srs in db.ServiceRequestServices on a.Id equals srs.Access.Id
                                       where a.AreaId == grp.Key && o.Serial != "ALCL00000000" && o.StateId == 1 && o.ActiveSoftware == o.PlannedSoftware select a.Id).Count().ToString()
                     }).ToList();
                 */ 

                /*
                if (dt != null)
                {
                    // below: the totals rows

                    dr = dt.NewRow();
                    dr["capacity"] = dt.Compute("SUM (capacity)", "").ToString();
                    dr["installed_contractor"] = dt.Compute("SUM (installed_contractor)", "").ToString();
                    dr["installed_contractor_with_serial"] = dt.Compute("SUM (installed_contractor_with_serial)", "").ToString();
                    dr["provisioned"] = dt.Compute("SUM (provisioned)", "").ToString();
                    dr["ont_ready_for_service"] = dt.Compute("SUM (ont_ready_for_service)", "").ToString();
                    dr["ont_defined_cs"] = dt.Compute("SUM (ont_defined_cs)", "").ToString();
                    dt.Rows.Add(dr);
                }
                 */
            }

            return statisticList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void QuickStatistic(out int al, out int hu)
        {
            List<string> isl, jsl;

            isl = new List<string>();
            jsl = new List<string>();

            foreach (int i in Ia.Ngn.Cl.Model.Data.Service.FourDigitNumberDomainListWithinNokiaSwitch) isl.Add(i.ToString());
            foreach (int i in Ia.Ngn.Cl.Model.Data.Service.FourDigitNumberDomainListWithinHuaweiSwitch) jsl.Add(i.ToString());

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                al = (from s in db.ServiceRequestServices where isl.Contains(s.Service.Substring(0, 4)) select s).Count();
                hu = (from s in db.ServiceRequestServices where jsl.Contains(s.Service.Substring(0, 4)) select s).Count();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Business.Administration.Statistic> PhoneStatistic(string timePeriod)
        {
            List<Ia.Ngn.Cl.Model.Business.Administration.Statistic> phoneStatisticList;
            
            /*
            string s, where;
            DateTime from, to;
            DataTable dt;

            if (timePeriod != null)
            {
                from = DateTime.Parse(timePeriod);
                to = DateTime.Parse(timePeriod);
                to = to.AddMonths(1);

                where = " AND (sr.request_time >= '" + sqlserver.SmallDateTime(from) + "' AND sr.request_time < '" + sqlserver.SmallDateTime(to) + "') ";
            }
            else where = "";
             */
            
            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                phoneStatisticList = (from a in Ia.Ngn.Cl.Model.Data.Service.KuwaitNgnAreaList
                     group a by a.Id into grp
                     orderby grp.Key
                     select new Ia.Ngn.Cl.Model.Business.Administration.Statistic()
                     {
                         Id = grp.Key.ToString(),
                         Name = grp.SingleOrDefault().NameArabicName,
                         //ServiceRequests = (from sr in db.ServiceRequests where sr.AreaId == grp.Key && sr.ServiceRequestService != null select sr.Id).Count().ToString(),
                         ServiceRequestServices = (from srs in db.ServiceRequestServices where srs.Access != null && srs.Access.AreaId == grp.Key select srs.Id).Count().ToString(),
                         Services = (from s in db.Service2s where s.Access != null && s.Access.AreaId == grp.Key select s.Id).Count().ToString(),
                         InternationalCalling = (from srs in db.ServiceRequestServices where srs.Access != null && srs.Access.AreaId == grp.Key && srs.InternationalCalling == true select srs.Id).Count() + " (" + (from s in db.Service2s where s.Access != null && s.Access.AreaId == grp.Key && s.InternationalCalling == true select s.Id).Count() + ")",
                         InternationalCallingUserControlled = (from srs in db.ServiceRequestServices where srs.Access != null && srs.Access.AreaId == grp.Key && srs.InternationalCallingUserControlled == true select srs.Id).Count() + " (" + (from s in db.Service2s where s.Access != null && s.Access.AreaId == grp.Key && s.InternationalCallingUserControlled == true select s.Id).Count() + ")",
                         CallWaiting = (from srs in db.ServiceRequestServices where srs.Access != null && srs.Access.AreaId == grp.Key && srs.CallWaiting == true select srs.Id).Count() + " (" + (from s in db.Service2s where s.Access != null && s.Access.AreaId == grp.Key && s.CallWaiting == true select s.Id).Count() + ")",
                         AlarmCall = (from srs in db.ServiceRequestServices where srs.Access != null && srs.Access.AreaId == grp.Key && srs.AlarmCall == true select srs.Id).Count() + " (" + (from s in db.Service2s where s.Access != null && s.Access.AreaId == grp.Key && s.AlarmCall == true select s.Id).Count() + ")",
                         CallBarring = (from srs in db.ServiceRequestServices where srs.Access != null && srs.Access.AreaId == grp.Key && srs.CallBarring == true select srs.Id).Count() + " (" + (from s in db.Service2s where s.Access != null && s.Access.AreaId == grp.Key && s.CallBarring == true select s.Id).Count() + ")",
                         CallerId = (from srs in db.ServiceRequestServices where srs.Access != null && srs.Access.AreaId == grp.Key && srs.CallerId == true select srs.Id).Count() + " (" + (from s in db.Service2s where s.Access != null && s.Access.AreaId == grp.Key && s.CallerId == true select s.Id).Count() + ")",
                         CallForwarding = (from srs in db.ServiceRequestServices where srs.Access != null && srs.Access.AreaId == grp.Key && srs.CallForwarding == true select srs.Id).Count() + " (" + (from s in db.Service2s where s.Access != null && s.Access.AreaId == grp.Key && s.CallForwarding == true select s.Id).Count() + ")",
                         ConferenceCall = (from srs in db.ServiceRequestServices where srs.Access != null && srs.Access.AreaId == grp.Key && srs.ConferenceCall == true select srs.Id).Count() + " (" + (from s in db.Service2s where s.Access != null && s.Access.AreaId == grp.Key && s.ConferenceCall == true select s.Id).Count() + ")",
                         ServiceSuspension = (from srs in db.ServiceRequestServices where srs.Access != null && srs.Access.AreaId == grp.Key && srs.ServiceSuspension == true select srs.Id).Count() + " (" + (from s in db.Service2s where s.Access != null && s.Access.AreaId == grp.Key && s.ServiceSuspension == true select s.Id).Count() + ")"
                     }).ToList();

                /*
                if (dt != null)
                {
                    // below: the totals rows
                    dr = dt.NewRow();

                    dr["SRS_AccessIdNotNull"] = dt.Compute("SUM (SRS_AccessIdNotNull)", "").ToString();
                    dr["IMS_AccessIdNotNull"] = dt.Compute("SUM (IMS_AccessIdNotNull)", "").ToString();
                    dr["SRS_AccordingToAreaIdFromDomain"] = dt.Compute("SUM (SRS_AccordingToAreaIdFromDomain)", "").ToString();

                    dr["InternationalCalling"] = dt.Compute("SUM (InternationalCalling)", "").ToString();
                    dr["InternationalCallingUserControlled"] = dt.Compute("SUM (InternationalCallingUserControlled)", "").ToString();
                    dr["CallWaiting"] = dt.Compute("SUM (CallWaiting)", "").ToString();
                    dr["AlarmCall"] = dt.Compute("SUM (AlarmCall)", "").ToString();
                    dr["CallBarring"] = dt.Compute("SUM (CallBarring)", "").ToString();
                    dr["CallerId"] = dt.Compute("SUM (CallerId)", "").ToString();
                    dr["CallForwarding"] = dt.Compute("SUM (CallForwarding)", "").ToString();
                    dr["ConferenceCall"] = dt.Compute("SUM (ConferenceCall)", "").ToString();
                    dr["ServiceSuspension"] = dt.Compute("SUM (ServiceSuspension)", "").ToString();

                    dt.Rows.Add(dr);
                }
                 */
            }

            return phoneStatisticList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static DataTable HsiStatistics()
        {
            DataRow dr;
            DataTable dt;

            sqlServer = new Ia.Cl.Model.Db.SqlServer();

            dt = sqlServer.Select(@"
select a.AreaId as areaId, 
(select count(1) from Onts as o left outer join Accesses as a2 on o.Access_Id = a2.Id left outer join OntServiceHsis as osh on osh.Ont_Id = o.Id where osh.StateId <> 0 and a2.AreaId = a.AreaId and osh.Svlan = 301) as Qualitynet,
(select count(1) from Onts as o left outer join Accesses as a2 on o.Access_Id = a2.Id left outer join OntServiceHsis as osh on osh.Ont_Id = o.Id where osh.StateId <> 0 and a2.AreaId = a.AreaId and osh.Svlan = 302) as Fasttelco,
(select count(1) from Onts as o left outer join Accesses as a2 on o.Access_Id = a2.Id left outer join OntServiceHsis as osh on osh.Ont_Id = o.Id where osh.StateId <> 0 and a2.AreaId = a.AreaId and osh.Svlan = 303) as UnitedNetworks,
(select count(1) from Onts as o left outer join Accesses as a2 on o.Access_Id = a2.Id left outer join OntServiceHsis as osh on osh.Ont_Id = o.Id where osh.StateId <> 0 and a2.AreaId = a.AreaId and osh.Svlan = 304) as KEMSZajel,
(select count(1) from Onts as o left outer join Accesses as a2 on o.Access_Id = a2.Id left outer join OntServiceHsis as osh on osh.Ont_Id = o.Id where osh.StateId <> 0 and a2.AreaId = a.AreaId and osh.Svlan = 402) as FasttelcoData,
(select count(1) from Onts as o left outer join Accesses as a2 on o.Access_Id = a2.Id left outer join OntServiceHsis as osh on osh.Ont_Id = o.Id where osh.StateId <> 0 and a2.AreaId = a.AreaId and osh.Svlan = 131) as SIP,
(select count(1) from Onts as o left outer join Accesses as a2 on o.Access_Id = a2.Id left outer join OntServiceHsis as osh on osh.Ont_Id = o.Id where osh.StateId <> 0 and a2.AreaId = a.AreaId and osh.Svlan = 305) as MOC1,
(select count(1) from Onts as o left outer join Accesses as a2 on o.Access_Id = a2.Id left outer join OntServiceHsis as osh on osh.Ont_Id = o.Id where osh.StateId <> 0 and a2.AreaId = a.AreaId and osh.Svlan = 306) as MOC2,
(select count(1) from Onts as o left outer join Accesses as a2 on o.Access_Id = a2.Id left outer join OntServiceHsis as osh on osh.Ont_Id = o.Id where osh.StateId <> 0 and a2.AreaId = a.AreaId and osh.Svlan = 307) as MOC3,
(select count(1) from Onts as o left outer join Accesses as a2 on o.Access_Id = a2.Id left outer join OntServiceHsis as osh on osh.Ont_Id = o.Id where osh.StateId <> 0 and a2.AreaId = a.AreaId and osh.Svlan = 403) as UnitedNetworksData,
(select count(1) from Onts as o left outer join Accesses as a2 on o.Access_Id = a2.Id left outer join OntServiceHsis as osh on osh.Ont_Id = o.Id where osh.StateId <> 0 and a2.AreaId = a.AreaId and osh.Svlan = 401) as QualitynetData,
(select count(1) from Onts as o left outer join Accesses as a2 on o.Access_Id = a2.Id left outer join OntServiceHsis as osh on osh.Ont_Id = o.Id where osh.StateId <> 0 and a2.AreaId = a.AreaId and osh.Svlan = 100) as Huawei,
(select count(1) from Onts as o left outer join Accesses as a2 on o.Access_Id = a2.Id left outer join OntServiceHsis as osh on osh.Ont_Id = o.Id where osh.StateId <> 0 and a2.AreaId = a.AreaId and (osh.Svlan <> 301 and osh.Svlan <> 302 and osh.Svlan <> 303 and osh.Svlan <> 304 and osh.Svlan <> 402 and osh.Svlan <> 131 and osh.Svlan <> 305 and osh.Svlan <> 306 and osh.Svlan <> 307 and osh.Svlan <> 403 and osh.Svlan <> 401 and osh.Svlan <> 100) ) as Unknown,
(select count(1) from Onts as o left outer join Accesses as a2 on o.Access_Id = a2.Id left outer join OntServiceHsis as osh on osh.Ont_Id = o.Id where osh.StateId <> 0 and a2.AreaId = a.AreaId ) as Total
from Accesses as a
group by a.AreaId
order by a.AreaId
    ");

            if (dt != null)
            {
                // below: the totals rows
                dr = dt.NewRow();

                dr["Qualitynet"] = dt.Compute("SUM (Qualitynet)", "").ToString();
                dr["Fasttelco"] = dt.Compute("SUM (Fasttelco)", "").ToString();
                dr["UnitedNetworks"] = dt.Compute("SUM (UnitedNetworks)", "").ToString();
                dr["KEMSZajel"] = dt.Compute("SUM (KEMSZajel)", "").ToString();
                dr["FasttelcoData"] = dt.Compute("SUM (FasttelcoData)", "").ToString();
                dr["SIP"] = dt.Compute("SUM (SIP)", "").ToString();
                dr["MOC1"] = dt.Compute("SUM (MOC1)", "").ToString();
                dr["MOC2"] = dt.Compute("SUM (MOC2)", "").ToString();
                dr["MOC3"] = dt.Compute("SUM (MOC3)", "").ToString();
                dr["UnitedNetworksData"] = dt.Compute("SUM (UnitedNetworksData)", "").ToString();
                dr["QualitynetData"] = dt.Compute("SUM (QualitynetData)", "").ToString();
                dr["Huawei"] = dt.Compute("SUM (Huawei)", "").ToString();
                dr["Unknown"] = dt.Compute("SUM (Unknown)", "").ToString();
                dr["Total"] = dt.Compute("SUM (Total)", "").ToString();

                dt.Rows.Add(dr);
            }

            return dt;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ui.Performance> StaffAndFrameworkPerformanceReport()
        {
            List<Ia.Ngn.Cl.Model.Ui.Performance> performanceList;

            // after 2015-06-01 user report closer inserts a last historic report
            // I should designate last report as CLOSED and add it to resolution list to be accessed by HEAD only.

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                performanceList =
                    (
                    from staff in (from s in db.Staff group s.UserId by s.UserId into g select new { UserId = g.Key, Count = g.Count() })

                    join resolved in (from r in db.ReportHistories where r.Resolution == 1020 group r.UserId by r.UserId into g select new { UserId = g.Key, Count = g.Count() })
                    on staff.UserId equals resolved.UserId into resolved_gj
                    from re in resolved_gj.DefaultIfEmpty()

                    join attempted in (from r in db.ReportHistories where r.Resolution != 1020 group r.UserId by r.UserId into g select new { UserId = g.Key, Count = g.Count() })
                    on staff.UserId equals attempted.UserId into attempted_gj
                    from at in attempted_gj.DefaultIfEmpty()

                    join inserted in
                        (from r in db.Reports group r.UserId by r.UserId into g select new { UserId = g.Key, Count = g.Count() })
                    on staff.UserId equals inserted.UserId into inserted_gj
                    from ins in inserted_gj.DefaultIfEmpty()

                    join open in
                        (from r in db.Reports group r.UserId by r.UserId into g select new { UserId = g.Key, Count = g.Count() })
                    on staff.UserId equals open.UserId into open_gj
                    from opn in open_gj.DefaultIfEmpty()

                    // below: the 20, 10, 1 give weight to the field
                    orderby re.Count descending, at.Count descending, ins.Count descending

                    select new Ia.Ngn.Cl.Model.Ui.Performance
                    {
                        UserId = staff.UserId,
                        Resolved = (re == null ? 0 : re.Count),
                        Attempted = (at == null ? 0 : at.Count),
                        Inserted = (ins == null ? 0 : ins.Count),
                        Open = (ins == null ? 0 : opn.Count),
                        AverageReportsPerDay = 0
                    }).ToList();

                /*
    select users.UserId, resolved.count,attempted.count,inserted.count from
    (
    (select count(*) as count, UserId from Users group by UserId) as users
    left outer join
    (select count(*) as count, rh.UserId from ReportHistories as rh where rh.Resolution = 1020 group by rh.UserId) as resolved
    on users.UserId = resolved.UserId
    left outer join
    (select count(*) as count, rh.UserId from ReportHistories as rh where rh.Resolution <> 1020 group by rh.UserId) as attempted
    on users.UserId = attempted.UserId
    left outer join
    (select count(*) as count, UserId from Reports group by UserId) as inserted
    on users.UserId = inserted.UserId
    )
    order by resolved.count*20+attempted.count*10+inserted.count desc
            */
            }

            return performanceList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ui.Performance> StatisticsOfResolvedAndAttemptedAndInsertedStaffReport2(Guid userId)
        {
            List<Ia.Ngn.Cl.Model.Ui.Performance> performanceList;

            // after 2015-06-01 user report closer inserts a last historic report
            // I should designate last report as CLOSED and add it to resolution list to be accessed by HEAD only.

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                performanceList =
                    (
                    from staff in
                        (from s in db.Staff group s.UserId by s.UserId into g select new { UserId = g.Key, Count = g.Count() })
                    join resolved in
                        (from r in db.ReportHistories where r.Resolution == 1020 group r.UserId by r.UserId into g select new { UserId = g.Key, Count = g.Count() })
                    on staff.UserId equals resolved.UserId into resolved_gj
                    from re in resolved_gj.DefaultIfEmpty()

                    join attempted in
                        (from r in db.ReportHistories where r.Resolution != 1020 group r.UserId by r.UserId into g select new { UserId = g.Key, Count = g.Count() })
                    on staff.UserId equals attempted.UserId into attempted_gj
                    from at in attempted_gj.DefaultIfEmpty()

                    join inserted in
                        (from r in db.Reports group r.UserId by r.UserId into g select new { UserId = g.Key, Count = g.Count() })
                    on staff.UserId equals inserted.UserId into inserted_gj
                    from ins in inserted_gj.DefaultIfEmpty()
                    // below: the 20, 10, 1 give weight to the field
                    orderby re.Count descending, at.Count descending, ins.Count descending
                    select new Ia.Ngn.Cl.Model.Ui.Performance
                    {
                        UserId = staff.UserId,
                        Resolved = (re == null ? 0 : re.Count),
                        Attempted = (at == null ? 0 : at.Count),
                        Inserted = (ins == null ? 0 : ins.Count),
                        AverageReportsPerDay = 0
                    }).ToList();

                /*
    select users.UserId, resolved.count,attempted.count,inserted.count from
    (
    (select count(*) as count, UserId from Users group by UserId) as users
    left outer join
    (select count(*) as count, rh.UserId from ReportHistories as rh where rh.Resolution = 1020 group by rh.UserId) as resolved
    on users.UserId = resolved.UserId
    left outer join
    (select count(*) as count, rh.UserId from ReportHistories as rh where rh.Resolution <> 1020 group by rh.UserId) as attempted
    on users.UserId = attempted.UserId
    left outer join
    (select count(*) as count, UserId from Reports group by UserId) as inserted
    on users.UserId = inserted.UserId
    )
    order by resolved.count*20+attempted.count*10+inserted.count desc
            */
            }

            return performanceList.ToList();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public DataTable CountOfActiveNumbersInArea()
        {
            return CountOfActiveNumbersInAreaByTimePeriod(null);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static DataTable CountOfActiveNumbersInAreaByTimePeriod(string timePeriod)
        {
            string s, where;
            DateTime from, to;
            DataTable dt;

            if (timePeriod != null)
            {
                from = DateTime.Parse(timePeriod);
                to = DateTime.Parse(timePeriod);
                to = to.AddMonths(1);

                where = null; // " AND (sr.request_time >= '" + sqlserver.SmallDateTime(from) + "' AND sr.request_time < '" + sqlserver.SmallDateTime(to) + "') ";
            }
            else where = "";

            s = @"SELECT COUNT(1) AS count, f.area
FROM         ia_system AS s INNER JOIN
                      ia_protocol AS p ON s.lceid = p.lceid AND s.lan = p.lan INNER JOIN
                      ia_standard AS st ON st.ip = p.ip INNER JOIN
                      ia_field AS f ON f.id = st.id LEFT OUTER JOIN
                      ia_service_request_service AS srs ON srs.dn = s.dn LEFT OUTER JOIN
                      ia_service_request AS sr ON sr.id = srs.ia_service_request_id
WHERE f.area != 0 " + where + @" GROUP BY f.area ";

            dt = null; // sqlserver.Select(s);

            return dt;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<string, int> DateTimesWithAvailableData()
        {
            Dictionary<string, int> dic;


            dic = new Dictionary<string, int>(100);


            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                //dic = (from q in db.ServiceRequests orderby q.RequestDateTime select q.RequestDateTime).Distinct().ToDictionary(r => r.CustomerName, r => r.Id);

                // dictionary = (from q in ReportXDocument.Elements("report").Elements("category") select new { Id = int.Parse(q.Attribute("id").Value), Name = q.Attribute("name").Value }).ToDictionary(r => r.Id, r => r.Name);

            }

            /*
SELECT DISTINCT CONVERT(varchar(7), RequestDateTime, 102) AS date, COUNT(1) AS count
FROM [Ia_Ngn].[dbo].[ServiceRequests]
GROUP BY CONVERT(varchar(7), RequestDateTime, 102)
ORDER BY date
             */

            return dic;
        }

        ////////////////////////////////////////////////////////////////////////////
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
                Assembly _assembly;
                StreamReader streamReader;

                if (xDocument == null)
                {
                    _assembly = Assembly.GetExecutingAssembly();
                    streamReader = new StreamReader(_assembly.GetManifestResourceStream("Ia.Ngn.Cl.model.data.administration.xml"));

                    try
                    {
                        if (streamReader.Peek() != -1) xDocument = System.Xml.Linq.XDocument.Load(streamReader);
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
