using System;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Ia.Ngn.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Report support class for Next Generation Network (NGN) data model.
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
    public partial class Report
    {
        private static XDocument xDocument;
        private static List<Category> categoryList;
        private static List<Area> areaList;
        private static List<Indication> indicationList;
        private static List<Action> actionList;
        private static List<Resolution> resolutionList;

        /*
        private static List<Severity> severityList;
        private static List<Priority> priorityList;
        private static List<Status> statusList;
        private static List<Estimate> estimateList;
        private static List<ServiceType> serviceTypeList;
        private static List<CustomerCare> customerCare;
         */ 

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Report() { }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public class Category
        {
            public Category() { }

            public int Id { get; set; }
            public string Name { get; set; }
            public string ArabicName { get; set; }
            public virtual ICollection<Area> Areas
            {
                get
                {
                    return (from q in AreaList where q.Category.Id == this.Id select q).ToList();
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Area
        {
            public Area() { }

            public int Id { get; set; }
            public int XmlId { get; set; }
            public string Name { get; set; }
            public string ArabicName { get; set; }
            public List<string> Frameworks { get; set; }
            public virtual Category Category { get; set; }
            public virtual ICollection<Indication> Indications
            {
                get { return (from q in IndicationList where q.Area.Id == this.Id select q).ToList(); }
            }
            public virtual ICollection<Action> Actions
            {
                get { return (from q in ActionList where q.Area.Id == this.Id select q).ToList(); }
            }
            public virtual ICollection<Resolution> Resolutions
            {
                get { return (from q in ResolutionList where q.Area.Id == this.Id select q).ToList(); }
            }

            public int AreaId(int categoryId, int areaId)
            {
                return categoryId * 100 + areaId;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Indication
        {
            public Indication() { }

            public int Id { get; set; }
            public int XmlId { get; set; }
            public bool Obsolete { get; set; }
            public bool CanInsert { get; set; }
            public string Name { get; set; }
            public string ArabicName { get; set; }
            public string ColoredName { get; set; }
            public string ColoredArabicName { get; set; }
            public string EnglishAndArabicName { get; set; }
            public string ColoredEnglishAndArabicName { get; set; }
            public string Color { get; set; }
            public List<string> Frameworks { get; set; }
            public virtual Area Area { get; set; }

            public int IndicationId(int areaId, int indicationId) { return areaId * 10000 + indicationId; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Action
        {
            public Action() { }

            public int Id { get; set; }
            public int XmlId { get; set; }
            public bool Obsolete { get; set; }
            public bool CanInsert { get; set; }
            public string Name { get; set; }
            public string ArabicName { get; set; }
            public string ColoredName { get; set; }
            public string ColoredArabicName { get; set; }
            public string EnglishAndArabicName { get; set; }
            public string ColoredEnglishAndArabicName { get; set; }
            public string Color { get; set; }
            public List<string> Frameworks { get; set; }
            public virtual Area Area { get; set; }
            public int ActionId(int areaId, int actionId) { return areaId * 10000 + actionId; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Resolution
        {
            public Resolution() { }

            public int Id { get; set; }
            public int XmlId { get; set; }
            public bool Obsolete { get; set; }
            public bool CanInsert { get; set; }
            public string Name { get; set; }
            public string ArabicName { get; set; }
            public string ColoredName { get; set; }
            public string ColoredArabicName { get; set; }
            public string EnglishAndArabicName { get; set; }
            public string ColoredEnglishAndArabicName { get; set; }
            public string Color { get; set; }
            public virtual Area Area { get; set; }
            public int ResolutionId(int areaId, int resolutionId) { return areaId * 10000 + resolutionId; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.Report.Category> CategoryList
        {
            get
            {
                if (categoryList == null || categoryList.Count == 0)
                {
                    int id;
                    Category category;

                    categoryList = new List<Category>();

                    foreach (XElement x in XDocument.Element("report").Elements("category"))
                    {
                        category = new Category();

                        id = int.Parse(x.Attribute("id").Value);

                        category.Id = id;
                        category.Name = x.Attribute("name").Value;
                        category.ArabicName = (x.Attribute("arabicName") != null) ? x.Attribute("arabicName").Value : string.Empty;

                        categoryList.Add(category);
                    }
                }

                return categoryList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.Report.Area> AreaList
        {
            get
            {
                if (areaList == null || areaList.Count == 0)
                {
                    int categoryId, id;
                    Area area;

                    areaList = new List<Area>();

                    foreach (XElement x in XDocument.Element("report").Elements("category").Elements("area"))
                    {
                        area = new Area();
                        area.Category = new Category();

                        categoryId = int.Parse(x.Parent.Attribute("id").Value);
                        id = int.Parse(x.Attribute("id").Value);

                        area.Id = area.AreaId(categoryId, id);
                        area.XmlId = id;
                        area.Category = (from q in CategoryList where q.Id == categoryId select q).SingleOrDefault();
                        area.Name = x.Attribute("name").Value;
                        area.ArabicName = (x.Attribute("arabicName") != null) ? x.Attribute("arabicName").Value : string.Empty;

                        if (x.Attribute("framework") != null)
                        {
                            area.Frameworks = new List<string>(100);

                            foreach (string s in x.Attribute("framework").Value.Split(',')) area.Frameworks.Add(s);
                        }

                        areaList.Add(area);
                    }
                }

                return areaList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.Report.Indication> IndicationList
        {
            get
            {
                if (indicationList == null || indicationList.Count == 0)
                {
                    int categoryId, areaId, id;
                    Indication indication;

                    indicationList = new List<Indication>();

                    foreach (XElement x in XDocument.Element("report").Elements("category").Elements("area").Elements("indicationList").Elements("indication"))
                    {
                        indication = new Indication();
                        indication.Area = new Area();

                        categoryId = int.Parse(x.Parent.Parent.Parent.Attribute("id").Value);
                        areaId = int.Parse(x.Parent.Parent.Attribute("id").Value);
                        id = int.Parse(x.Attribute("id").Value);

                        areaId = indication.Area.AreaId(categoryId, areaId);

                        indication.Id = indication.IndicationId(areaId, id);
                        indication.XmlId = id;
                        indication.Area = (from q in AreaList where q.Id == areaId select q).SingleOrDefault();

                        // below: obsolete indicates weather the attribute is still used as a selection, but it must remain as a value of int in the storage
                        if (x.Attribute("obsolete") != null) indication.Obsolete = (x.Attribute("obsolete").Value == "true") ? true : false;
                        else indication.Obsolete = false;

                        // below: canInsert indicates weather the attribute is used as a selection, but it must remain as a value of int in the storage
                        if (x.Attribute("canInsert") != null) indication.CanInsert = (x.Attribute("canInsert").Value == "true") ? true : false;
                        else indication.CanInsert = true;

                        // below: replace spaces ' ' with HTML fixed space "&nbsp;"
                        indication.Name = x.Attribute("name").Value.Replace(" ", "&nbsp;");
                        indication.ColoredName = ColoredName(id, indication.Name);

                        if (x.Attribute("arabicName") != null && x.Attribute("arabicName").Value != string.Empty)
                        {
                            indication.ArabicName = x.Attribute("arabicName").Value.Replace(" ", "&nbsp;");
                            indication.ColoredArabicName = ColoredName(id, indication.ArabicName);
                        }
                        else
                        {
                            indication.ArabicName = string.Empty;
                            indication.ColoredArabicName = string.Empty;
                        }

                        if (indication.ArabicName != string.Empty) indication.EnglishAndArabicName = indication.Name + "&nbsp;(" + indication.ArabicName + ")";
                        else indication.EnglishAndArabicName = indication.Name;

                        if (indication.ColoredArabicName != string.Empty) indication.ColoredEnglishAndArabicName = indication.ColoredName + "&nbsp;(" + indication.ColoredArabicName + ")";
                        else indication.ColoredEnglishAndArabicName = indication.ColoredName;

                        indication.Color = (x.Attribute("color") != null) ? x.Attribute("color").Value : string.Empty;

                        if (x.Parent.Attribute("framework") != null)
                        {
                            indication.Frameworks = new List<string>(100);

                            foreach (string s in x.Parent.Attribute("framework").Value.Split(',')) indication.Frameworks.Add(s);
                        }

                        indicationList.Add(indication);
                    }

                    // below: add the general indications to all areas
                    foreach (XElement x in XDocument.Element("report").Elements("category").Elements("area"))
                    {
                        foreach (XElement y in XDocument.Element("report").Elements("indication"))
                        {
                            indication = new Indication();
                            indication.Area = new Area();

                            categoryId = int.Parse(x.Parent.Attribute("id").Value);
                            areaId = int.Parse(x.Attribute("id").Value);
                            id = int.Parse(y.Attribute("id").Value); // y

                            areaId = indication.Area.AreaId(categoryId, areaId);

                            indication.Id = indication.IndicationId(areaId, id);
                            indication.XmlId = id;
                            indication.Area = null;// (from q in AreaList where q.Id == areaId select q).SingleOrDefault();

                            // below: obsolete indicates weather the attribute is still used as a selection, but it must remain as a value of int in the storage
                            if (y.Attribute("obsolete") != null) indication.Obsolete = (y.Attribute("obsolete").Value == "true") ? true : false;
                            else indication.Obsolete = false;

                            // below: canInsert indicates weather the attribute is used as a selection, but it must remain as a value of int in the storage
                            if (y.Attribute("canInsert") != null) indication.CanInsert = (y.Attribute("canInsert").Value == "true") ? true : false;
                            else indication.CanInsert = true;

                            // below: replace spaces ' ' with HTML fixed space "&nbsp;"
                            indication.Name = y.Attribute("name").Value.Replace(" ", "&nbsp;");
                            indication.ColoredName = ColoredName(id, indication.Name);

                            if (y.Attribute("arabicName") != null && y.Attribute("arabicName").Value != string.Empty)
                            {
                                indication.ArabicName = y.Attribute("arabicName").Value.Replace(" ", "&nbsp;");
                                indication.ColoredArabicName = ColoredName(id, indication.ArabicName);
                            }
                            else
                            {
                                indication.ArabicName = string.Empty;
                                indication.ColoredArabicName = string.Empty;
                            }

                            if (indication.ArabicName != string.Empty) indication.EnglishAndArabicName = indication.Name + "&nbsp;(" + indication.ArabicName + ")";
                            else indication.EnglishAndArabicName = indication.Name;

                            if (indication.ColoredArabicName != string.Empty) indication.ColoredEnglishAndArabicName = indication.ColoredName + "&nbsp;(" + indication.ColoredArabicName + ")";
                            else indication.ColoredEnglishAndArabicName = indication.ColoredName;

                            indication.Color = (y.Attribute("color") != null) ? y.Attribute("color").Value : string.Empty;

                            indicationList.Add(indication);
                        }
                    }
                }

                return indicationList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.Report.Action> ActionList
        {
            get
            {
                if (actionList == null || actionList.Count == 0)
                {
                    int categoryId, areaId, id;
                    Action action;

                    actionList = new List<Action>();

                    foreach (XElement x in XDocument.Element("report").Elements("category").Elements("area").Elements("actionList").Elements("action"))
                    {
                        action = new Action();
                        action.Area = new Area();

                        categoryId = int.Parse(x.Parent.Parent.Parent.Attribute("id").Value);
                        areaId = int.Parse(x.Parent.Parent.Attribute("id").Value);
                        id = int.Parse(x.Attribute("id").Value);

                        areaId = action.Area.AreaId(categoryId, areaId);

                        action.Id = action.ActionId(areaId, id);
                        action.XmlId = id;
                        action.Area = (from q in AreaList where q.Id == areaId select q).SingleOrDefault();

                        // below: obsolete indicates weather the attribute is still used as a selection, but it must remain as a value of int in the storage
                        if (x.Attribute("obsolete") != null) action.Obsolete = (x.Attribute("obsolete").Value == "true") ? true : false;
                        else action.Obsolete = false;

                        // below: canInsert indicates weather the attribute is used as a selection, but it must remain as a value of int in the storage
                        if (x.Attribute("canInsert") != null) action.CanInsert = (x.Attribute("canInsert").Value == "true") ? true : false;
                        else action.CanInsert = true;

                        // below: replace spaces ' ' with HTML fixed space "&nbsp;"
                        action.Name = x.Attribute("name").Value.Replace(" ", "&nbsp;");
                        action.ColoredName = ColoredName(id, action.Name);

                        if (x.Attribute("arabicName") != null && x.Attribute("arabicName").Value != string.Empty)
                        {
                            action.ArabicName = x.Attribute("arabicName").Value.Replace(" ", "&nbsp;");
                            action.ColoredArabicName = ColoredName(id, action.ArabicName);
                        }
                        else
                        {
                            action.ArabicName = string.Empty;
                            action.ColoredArabicName = string.Empty;
                        }

                        if (action.ArabicName != string.Empty) action.EnglishAndArabicName = action.Name + "&nbsp;(" + action.ArabicName + ")";
                        else action.EnglishAndArabicName = action.Name;

                        if (action.ColoredArabicName != string.Empty) action.ColoredEnglishAndArabicName = action.ColoredName + "&nbsp;(" + action.ColoredArabicName + ")";
                        else action.ColoredEnglishAndArabicName = action.ColoredName;

                        action.Color = (x.Attribute("color") != null) ? x.Attribute("color").Value : string.Empty;

                        if (x.Parent.Attribute("framework") != null)
                        {
                            action.Frameworks = new List<string>(100);

                            foreach (string s in x.Parent.Attribute("framework").Value.Split(',')) action.Frameworks.Add(s);
                        }

                        actionList.Add(action);
                    }

                    // below: add the general actions to all areas
                    foreach (XElement x in XDocument.Element("report").Elements("category").Elements("area"))
                    {
                        foreach (XElement y in XDocument.Element("report").Elements("action"))
                        {
                            action = new Action();
                            action.Area = new Area();

                            categoryId = int.Parse(x.Parent.Attribute("id").Value);
                            areaId = int.Parse(x.Attribute("id").Value);
                            id = int.Parse(y.Attribute("id").Value); // y

                            areaId = action.Area.AreaId(categoryId, areaId);

                            action.Id = action.ActionId(areaId, id);
                            action.XmlId = id;
                            action.Area = null; // (from q in AreaList where q.Id == areaId select q).SingleOrDefault();

                            // below: obsolete indicates weather the attribute is still used as a selection, but it must remain as a value of int in the storage
                            if (y.Attribute("obsolete") != null) action.Obsolete = (y.Attribute("obsolete").Value == "true") ? true : false;
                            else action.Obsolete = false;

                            // below: canInsert indicates weather the attribute is used as a selection, but it must remain as a value of int in the storage
                            if (y.Attribute("canInsert") != null) action.CanInsert = (y.Attribute("canInsert").Value == "true") ? true : false;
                            else action.CanInsert = true;

                            // below: replace spaces ' ' with HTML fixed space "&nbsp;"
                            action.Name = y.Attribute("name").Value.Replace(" ", "&nbsp;");
                            action.ColoredName = ColoredName(id, action.Name);

                            if (y.Attribute("arabicName") != null && y.Attribute("arabicName").Value != string.Empty)
                            {
                                action.ArabicName = y.Attribute("arabicName").Value.Replace(" ", "&nbsp;");
                                action.ColoredArabicName = ColoredName(id, action.ArabicName);
                            }
                            else
                            {
                                action.ArabicName = string.Empty;
                                action.ColoredArabicName = string.Empty;
                            }

                            if (action.ArabicName != string.Empty) action.EnglishAndArabicName = action.Name + "&nbsp;(" + action.ArabicName + ")";
                            else action.EnglishAndArabicName = action.Name;

                            if (action.ColoredArabicName != string.Empty) action.ColoredEnglishAndArabicName = action.ColoredName + "&nbsp;(" + action.ColoredArabicName + ")";
                            else action.ColoredEnglishAndArabicName = action.ColoredName;

                            action.Color = (y.Attribute("color") != null) ? y.Attribute("color").Value : string.Empty;

                            actionList.Add(action);
                        }
                    }
                }

                return actionList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.Report.Resolution> ResolutionList
        {
            get
            {
                if (resolutionList == null || resolutionList.Count == 0)
                {
                    int categoryId, areaId, id;
                    Resolution resolution;

                    resolutionList = new List<Resolution>();

                    foreach (XElement x in XDocument.Element("report").Elements("category").Elements("area").Elements("resolutionList").Elements("resolution"))
                    {
                        resolution = new Resolution();
                        resolution.Area = new Area();

                        categoryId = int.Parse(x.Parent.Parent.Parent.Attribute("id").Value);
                        areaId = int.Parse(x.Parent.Parent.Attribute("id").Value);
                        id = int.Parse(x.Attribute("id").Value);

                        areaId = resolution.Area.AreaId(categoryId, areaId);

                        resolution.Id = resolution.ResolutionId(areaId, id);
                        resolution.XmlId = id;
                        resolution.Area = (from q in AreaList where q.Id == areaId select q).SingleOrDefault();

                        // below: obsolete indicates weather the attribute is still used as a selection, but it must remain as a value of int in the storage
                        if (x.Attribute("obsolete") != null) resolution.Obsolete = (x.Attribute("obsolete").Value == "true") ? true : false;
                        else resolution.Obsolete = false;

                        // below: canInsert indicates weather the attribute is used as a selection, but it must remain as a value of int in the storage
                        if (x.Attribute("canInsert") != null) resolution.CanInsert = (x.Attribute("canInsert").Value == "true") ? true : false;
                        else resolution.CanInsert = true;

                        // below: replace spaces ' ' with HTML fixed space "&nbsp;"
                        resolution.Name = x.Attribute("name").Value.Replace(" ", "&nbsp;");
                        resolution.ColoredName = ColoredName(id, resolution.Name);

                        if (x.Attribute("arabicName") != null && x.Attribute("arabicName").Value != string.Empty)
                        {
                            resolution.ArabicName = x.Attribute("arabicName").Value.Replace(" ", "&nbsp;");
                            resolution.ColoredArabicName = ColoredName(id, resolution.ArabicName);
                        }
                        else
                        {
                            resolution.ArabicName = string.Empty;
                            resolution.ColoredArabicName = string.Empty;
                        }

                        if (resolution.ArabicName != string.Empty) resolution.EnglishAndArabicName = resolution.Name + "&nbsp;(" + resolution.ArabicName + ")";
                        else resolution.EnglishAndArabicName = resolution.Name;

                        if (resolution.ColoredArabicName != string.Empty) resolution.ColoredEnglishAndArabicName = resolution.ColoredName + "&nbsp;(" + resolution.ColoredArabicName + ")";
                        else resolution.ColoredEnglishAndArabicName = resolution.ColoredName;

                        resolution.Color = (x.Attribute("color") != null) ? x.Attribute("color").Value : string.Empty;

                        resolutionList.Add(resolution);
                    }

                    // below: add the general resolutions to all areas
                    foreach (XElement x in XDocument.Element("report").Elements("category").Elements("area"))
                    {
                        foreach (XElement y in XDocument.Element("report").Elements("resolution"))
                        {
                            resolution = new Resolution();
                            resolution.Area = new Area();

                            categoryId = int.Parse(x.Parent.Attribute("id").Value);
                            areaId = int.Parse(x.Attribute("id").Value);
                            id = int.Parse(y.Attribute("id").Value); // y

                            areaId = resolution.Area.AreaId(categoryId, areaId);

                            resolution.Id = resolution.ResolutionId(areaId, id);
                            resolution.XmlId = id;
                            resolution.Area = (from q in AreaList where q.Id == areaId select q).SingleOrDefault();

                            // below: obsolete indicates weather the attribute is still used as a selection, but it must remain as a value of int in the storage
                            if (y.Attribute("obsolete") != null) resolution.Obsolete = (y.Attribute("obsolete").Value == "true") ? true : false;
                            else resolution.Obsolete = false;

                            // below: canInsert indicates weather the attribute is used as a selection, but it must remain as a value of int in the storage
                            if (y.Attribute("canInsert") != null) resolution.CanInsert = (y.Attribute("canInsert").Value == "true") ? true : false;
                            else resolution.CanInsert = true;

                            // below: replace spaces ' ' with HTML fixed space "&nbsp;"
                            resolution.Name = y.Attribute("name").Value.Replace(" ", "&nbsp;");
                            resolution.ColoredName = ColoredName(id, resolution.Name);

                            if (y.Attribute("arabicName") != null && y.Attribute("arabicName").Value != string.Empty)
                            {
                                resolution.ArabicName = y.Attribute("arabicName").Value.Replace(" ", "&nbsp;");
                                resolution.ColoredArabicName = ColoredName(id, resolution.ArabicName);
                            }
                            else
                            {
                                resolution.ArabicName = string.Empty;
                                resolution.ColoredArabicName = string.Empty;
                            }

                            if (resolution.ArabicName != string.Empty) resolution.EnglishAndArabicName = resolution.Name + "&nbsp;(" + resolution.ArabicName + ")";
                            else resolution.EnglishAndArabicName = resolution.Name;

                            if (resolution.ColoredArabicName != string.Empty) resolution.ColoredEnglishAndArabicName = resolution.ColoredName + "&nbsp;(" + resolution.ColoredArabicName + ")";
                            else resolution.ColoredEnglishAndArabicName = resolution.ColoredName;

                            resolution.Color = (y.Attribute("color") != null) ? y.Attribute("color").Value : string.Empty;

                            resolutionList.Add(resolution);
                        }
                    }
                }

                return resolutionList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ui.ReportAccessServiceRequest> ReadSpecificUserIdAndAreaIdReportWithReportOpenStatusList(Guid userId, int areaId)
        {
            List<Ia.Ngn.Cl.Model.Ui.ReportAccessServiceRequest> reportAccessServiceRequestList;

            if (areaId == 0) reportAccessServiceRequestList = ReadSpecificUserIdReportWithReportOpenStatusList_(userId);
            else reportAccessServiceRequestList = (from q in ReadSpecificUserIdReportWithReportOpenStatusList_(userId) where q.Access != null && q.Access.AreaId == areaId select q).ToList();

            return reportAccessServiceRequestList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Report> ReadOpenStatusReportList()
        {
            List<Ia.Ngn.Cl.Model.Report> reportList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // <status id="1" name="Open" arabicName="مفتوح"/>
                // <status id="2" name="Closed" arabicName="مغلق"/>

                reportList = (from q in db.Reports where q.Status == 1 orderby q.Created select q).Include(x => x.ReportHistories).ToList();
            }

            return reportList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Report> ReadOpenStatusAndClosedStatusWithinLast24HourForSpecificUserIdOrHisFrameworkAncestorOrDescendantUserIdWithNoStaffHistoryReportOrHisSubordinateStaffReportList(List<Ia.Ngn.Cl.Model.Report> reportList, Guid userId)
        {
            Ia.Ngn.Cl.Model.Staff staff;
            staff = Ia.Ngn.Cl.Model.Data.Staff.MembershipUser2(userId);

            return ReadOpenStatusAndClosedStatusWithinLast24HourForSpecificUserIdOrHisFrameworkAncestorOrDescendantUserIdWithNoStaffHistoryReportOrHisSubordinateStaffReportList(reportList, staff);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Report> ReadOpenStatusAndClosedStatusWithinLast24HourForSpecificUserIdOrHisFrameworkAncestorOrDescendantUserIdWithNoStaffHistoryReportOrHisSubordinateStaffReportList(List<Ia.Ngn.Cl.Model.Report> reportList, Ia.Ngn.Cl.Model.Staff staff)
        {
            List<Guid> staffFrameworkAncestorAndDescendantUserIdList;
            List<Guid> staffSubordinatesUserIdList;

            if (staff != null)
            {
                staffFrameworkAncestorAndDescendantUserIdList = new List<Guid>();
                staffSubordinatesUserIdList = new List<Guid>();

                // below: add self framework
                staffFrameworkAncestorAndDescendantUserIdList.Add(staff.Framework.Guid);

                // below: add ancestor framework
                foreach (Ia.Ngn.Cl.Model.Data.Administration.Framework ancestor in staff.Framework.Ancestors) staffFrameworkAncestorAndDescendantUserIdList.Add(ancestor.Guid);

                // below: add decendant framework
                foreach (Ia.Ngn.Cl.Model.Data.Administration.Framework descendant in staff.Framework.Descendants) staffFrameworkAncestorAndDescendantUserIdList.Add(descendant.Guid);

                // below: add children staff
                if(staff.Subordinates != null) foreach (Ia.Ngn.Cl.Model.Staff subordinate in staff.Subordinates) staffSubordinatesUserIdList.Add(subordinate.UserId);

                reportList = (from r in reportList
                              where r.LastReportHistory == null
                              || r.LastReportHistory == null && r.UserId == staff.UserId
                              || r.LastReportHistory == null && staffSubordinatesUserIdList.Contains(r.UserId)
                              || r.LastReportHistory == null && staffFrameworkAncestorAndDescendantUserIdList.Contains(r.UserId)
                              || r.LastReportHistory != null && r.LastReportHistory.UserId == staff.UserId
                              || r.LastReportHistory != null && staffSubordinatesUserIdList.Contains(r.LastReportHistory.UserId)
                              || r.LastReportHistory != null && staffFrameworkAncestorAndDescendantUserIdList.Contains(r.LastReportHistory.UserId)
                              select r).ToList();
            }
            else reportList = null;

            return reportList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Report> ReadOpenStatusAndClosedStatusWithinLast24HourForSpecificFrameworkUserIdOrItsFrameworkAncestorOrDescendantFrameworkUserIdWithNoStaffHistoryReportList(List<Ia.Ngn.Cl.Model.Report> reportList, Guid frameworkUserId)
        {
            Ia.Ngn.Cl.Model.Data.Administration.Framework framework;
            List<Guid> staffFrameworkAncestorAndDescendantUserIdList;

            staffFrameworkAncestorAndDescendantUserIdList = new List<Guid>();

            framework = (from f in Ia.Ngn.Cl.Model.Data.Administration.FrameworkList where f.Guid == frameworkUserId select f).SingleOrDefault();

            // below: add self framework
            staffFrameworkAncestorAndDescendantUserIdList.Add(framework.Guid);

            // below: add ancestor framework
            foreach (Ia.Ngn.Cl.Model.Data.Administration.Framework ancestor in framework.Ancestors) staffFrameworkAncestorAndDescendantUserIdList.Add(ancestor.Guid);

            // below: add decendant
            foreach (Ia.Ngn.Cl.Model.Data.Administration.Framework descendant in framework.Descendants) staffFrameworkAncestorAndDescendantUserIdList.Add(descendant.Guid);

            reportList = (from r in reportList
                          where r.LastReportHistory == null
                          || r.LastReportHistory == null && r.UserId == framework.Guid
                          || r.LastReportHistory == null && staffFrameworkAncestorAndDescendantUserIdList.Contains(r.UserId)
                          || r.LastReportHistory != null && r.LastReportHistory.UserId == framework.Guid
                          || r.LastReportHistory != null && staffFrameworkAncestorAndDescendantUserIdList.Contains(r.LastReportHistory.UserId)
                          select r).ToList();

            return reportList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Ui.ReportAccessServiceRequest> ReadSpecificUserIdReportWithReportOpenStatusList_(Guid userId)
        {
            List<string> serviceIdList;
            Ia.Ngn.Cl.Model.Ui.ReportAccessServiceRequest reportAccessServiceRequest;
            List<Ia.Ngn.Cl.Model.Ui.ReportAccessServiceRequest> reportAccessServiceRequestList;
            List<Ia.Ngn.Cl.Model.Report> reportList;
            List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList;

            reportAccessServiceRequestList = null;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // <status id="1" name="Open" arabicName="مفتوح"/>
                // <status id="2" name="Closed" arabicName="مغلق"/>

                if (userId != Guid.Empty)
                {
                    reportList = (from r in db.Reports
                                  join srs in db.ServiceRequestServices on r.Service equals srs.Service into gj
                                  where (r.Status == 1 && (r.ReportHistories.Any(y => y.UserId == userId)))
                                  orderby r.Created
                                  from sub in gj.DefaultIfEmpty()
                                  select r).Include(x => x.ReportHistories).ToList();

                    if (reportList.Count > 0)
                    {
                        serviceIdList = new List<string>();
                        foreach (var v in reportList) serviceIdList.Add(v.Service);

                        serviceRequestServiceList = (from srs in db.ServiceRequestServices
                                                     where serviceIdList.Contains(srs.Service)
                                                     select srs).ToList();

                        reportAccessServiceRequestList = new List<Ia.Ngn.Cl.Model.Ui.ReportAccessServiceRequest>();

                        foreach (Ia.Ngn.Cl.Model.Report report in reportList)
                        {
                            // below: the aggregate function will check the last entry
                            if (report.ReportHistories.Aggregate((i, j) => i.Id > j.Id ? i : j).UserId == userId)
                            {
                                reportAccessServiceRequest = new Ia.Ngn.Cl.Model.Ui.ReportAccessServiceRequest();

                                reportAccessServiceRequest.Report = report;
                                reportAccessServiceRequest.Report.ReportHistories = report.ReportHistories; // this to force inclusion of ReportHistories

                                reportAccessServiceRequest.Access = (from srs in serviceRequestServiceList where srs.Service == report.Service select srs.Access).FirstOrDefault();

                                reportAccessServiceRequestList.Add(reportAccessServiceRequest);
                            }
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }

            return reportAccessServiceRequestList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Report> ReadOpenStatusAndClosedStatusWithinLast24HourReportList()
        {
            DateTime before24Hour;
            List<Ia.Ngn.Cl.Model.Report> reportList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                before24Hour = DateTime.UtcNow.AddHours(3).AddDays(-1);

                // <status id="1" name="Open" arabicName="مفتوح"/>
                // <status id="2" name="Closed" arabicName="مغلق"/>

                reportList = (from q in db.Reports
                              where q.Status == 1 || (q.Status == 2 && q.Updated >= before24Hour)
                              orderby q.Created
                              select q).Include(x => x.ReportHistories).ToList();
            }

            return reportList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Report> ReadSingleAsList(int reportId)
        {
            List<Ia.Ngn.Cl.Model.Report> reportList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                try
                {
                    reportList = (from q in db.Reports where q.Id == reportId select q).Include(x => x.ReportHistories).ToList();
                }
                catch
                {
                    reportList = null;
                }
            }

            return reportList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static DateTime LastUpdatedDateTime()
        {
            DateTime lastUpdatedDateTime;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                try
                {
                    lastUpdatedDateTime = (from q in db.Reports orderby q.Updated descending select q.Updated).Take(1).Single();
                }
                catch
                {
                    lastUpdatedDateTime = new DateTime(1900, 1, 1);
                }
            }

            return lastUpdatedDateTime;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static DateTime LastUpdatedDateTimeOfHistory()
        {
            DateTime lastUpdatedDateTime;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                try
                {
                    lastUpdatedDateTime = (from q in db.ReportHistories orderby q.Updated descending select q.Updated).Take(1).Single();
                }
                catch
                {
                    lastUpdatedDateTime = new DateTime(1900, 1, 1);
                }
            }

            return lastUpdatedDateTime;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> CategoryDictionary
        {
            get
            {
                Dictionary<int, string> dictionary;

                dictionary = new Dictionary<int, string>(100);

                dictionary = (from q in XDocument.Elements("report").Elements("category")
                              select new { Id = int.Parse(q.Attribute("id").Value), Name = q.Attribute("name").Value }).ToDictionary(r => r.Id, r => r.Name);

                return dictionary;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> CategoryAreaIndicationDictionary(int categoryId, int areaId)
        {
            Dictionary<int, string> dictionary;

            dictionary = new Dictionary<int, string>(100);

            dictionary = (from q in XDocument.Elements("report").Elements("category").Elements("area").Elements("indicationList")
                 where q.Parent.Parent.Attribute("id").Value == categoryId.ToString() && q.Parent.Attribute("id").Value == areaId.ToString()
                 select new { Id = int.Parse(q.Attribute("id").Value), Name = q.Attribute("id").Value }).ToDictionary(r => r.Id, r => r.Name);

            return dictionary;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> IndicationColoredDictionary()
        {
            return ColoredDictionary(false, true, "category", "area", "indicationList", "indication").Concat(GeneralIndicationColoredDictionary).ToDictionary(q => q.Key, q => q.Value);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> IndicationColoredDictionary(int categoryId, int categoryAreaId)
        {
            return ColoredDictionary("category", "area", "indicationList", "indication", categoryId, categoryAreaId, true).Concat(GeneralIndicationColoredDictionary).ToDictionary(q => q.Key, q => q.Value);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> ActionDictionary()
        {
            return ColoredDictionary(false, false, "category", "area", "actionList", "action").Concat(GeneralActionColoredDictionary).ToDictionary(q => q.Key, q => q.Value);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> ActionColoredDictionary()
        {
            return ColoredDictionary(false, true, "category", "area", "actionList", "action").Concat(GeneralActionColoredDictionary).ToDictionary(q => q.Key, q => q.Value);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> ActionColoredDictionary(int categoryId, int categoryAreaId)
        {
            return ColoredDictionary("category", "area", "actionList", "action", categoryId, categoryAreaId, true).Concat(GeneralActionColoredDictionary).ToDictionary(q => q.Key, q => q.Value);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> ResolutionColoredDictionary
        {
            get
            {
                return ColoredDictionary(false, true, "resolution");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> ResolutionEnglishAndArabicColoredDictionary
        {
            get
            {
                return ColoredDictionary(true, true, "resolution");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> ServiceTypeDictionary
        {
            get
            {
                return ColoredDictionary(false, false, "service", "typeList", "type");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> ResolutionDictionary
        {
            get
            {
                return ColoredDictionary(false, false, "resolution");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> GeneralActionDictionary
        {
            get
            {
                return ColoredDictionary(false, false, "action");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> GeneralActionColoredDictionary
        {
            get
            {
                return ColoredDictionary(false, true, "action");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> GeneralIndicationDictionary
        {
            get
            {
                return ColoredDictionary(false, false, "indication");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> GeneralIndicationColoredDictionary
        {
            get
            {
                return ColoredDictionary(false, true, "indication");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> EstimateDictionary
        {
            get
            {
                return ColoredDictionary(false, false, "estimate");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> EstimateColoredDictionary
        {
            get
            {
                return ColoredDictionary(false, true, "estimate");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> StatusDictionary
        {
            get
            {
                return ColoredDictionary(false, false, "status");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> StatusColoredDictionary
        {
            get
            {
                return ColoredDictionary(false, true, "status");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> PriorityDictionary
        {
            get
            {
                return ColoredDictionary(false, false, "priority");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> PriorityColoredDictionary
        {
            get
            {
                return ColoredDictionary(false, true, "priority");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> SeverityDictionary
        {
            get
            {
                return ColoredDictionary(false, false, "severity");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> SeverityColoredDictionary
        {
            get
            {
                return ColoredDictionary(false, true, "severity");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> CategoryAreaDictionary()
        {
            return ColoredDictionary(false, false, "category", "area");
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> CategoryAreaDictionary(int categoryId)
        {
            return ColoredDictionary("category", "area", categoryId, false);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> CategoryAreaColoredDictionary
        {
            get
            {
                return ColoredDictionary(false, true, "category", "area");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns a Dictionary<int, string> dictionary of elements. If parameter isColored is true the dictionary will contain HTML formatted colored list
        /// </summary>
        private static Dictionary<int, string> ColoredDictionary(bool englishAndArabicName, bool isColored, params string[] elementList)
        {
            int id;
            string name, color;
            Dictionary<int, string> dictionary;
            IEnumerable<XElement> xElementIenumerable;

            dictionary = new Dictionary<int, string>(10);
            xElementIenumerable = null;

            switch (elementList.Length)
            {
                case 1: xElementIenumerable = XDocument.Element("report").Elements(elementList[0]); break;
                case 2: xElementIenumerable = XDocument.Element("report").Elements(elementList[0]).Elements(elementList[1]); break;
                case 3: xElementIenumerable = XDocument.Element("report").Elements(elementList[0]).Elements(elementList[1]).Elements(elementList[2]); break;
                case 4: xElementIenumerable = XDocument.Element("report").Elements(elementList[0]).Elements(elementList[1]).Elements(elementList[2]).Elements(elementList[3]); break;
                default: break;
            }

            foreach (XElement x in xElementIenumerable)
            {
                id = int.Parse(x.Attribute("id").Value);

                if(englishAndArabicName)
                {
                    if (x.Attribute("arabicName") != null)
                    {
                        name = x.Attribute("name").Value + " (" + x.Attribute("arabicName").Value + ")";
                    }
                    else name = x.Attribute("name").Value;
                }
                else name = x.Attribute("name").Value;

                color = (x.Attribute("color") != null)? x.Attribute("color").Value:null;

                // below: replace spaces ' ' with HTML fixed space "&nbsp;"
                name = name.Replace(" ", "&nbsp;");

                ColoredDictionaryItem(ref dictionary, isColored, id, name, color);
            }

            return dictionary;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns a Dictionary<int, string> dictionary of elements. If parameter isColored is true the dictionary will contain HTML formatted colored list
        /// </summary>
        private static Dictionary<int, string> ColoredDictionary(string element, string secondElement, int categoryId, bool isColored)
        {
            int id;
            string name, color;
            Dictionary<int, string> dictionary;

            dictionary = new Dictionary<int, string>(100);

            foreach (XElement x in XDocument.Element("report").Elements(element).Elements(secondElement))
            {
                if (x.Parent.Attribute("id").Value == categoryId.ToString())
                {
                    id = int.Parse(x.Attribute("id").Value);
                    name = x.Attribute("name").Value;
                    color = (x.Attribute("color") != null) ? x.Attribute("color").Value : null;

                    ColoredDictionaryItem(ref dictionary, isColored, id, name, color);
                }
            }

            return dictionary;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns a Dictionary<int, string> dictionary of elements. If parameter isColored is true the dictionary will contain HTML formatted colored list
        /// </summary>
        private static Dictionary<int, string> ColoredDictionary(string element, string secondElement, string thirdElement, string fourthElement, int categoryId, int categoryAreaId, bool isColored)
        {
            int id;
            string name, color;
            Dictionary<int, string> dictionary;

            dictionary = new Dictionary<int, string>(100);

            foreach (XElement x in XDocument.Element("report").Elements(element).Elements(secondElement).Elements(thirdElement).Elements(fourthElement))
            {
                if (x.Parent.Parent.Parent.Attribute("id").Value == categoryId.ToString() && x.Parent.Parent.Attribute("id").Value == categoryAreaId.ToString())
                {
                    id = int.Parse(x.Attribute("id").Value);
                    name = x.Attribute("name").Value;
                    color = (x.Attribute("color") != null) ? x.Attribute("color").Value : null;

                    ColoredDictionaryItem(ref dictionary, isColored, id, name, color);
                }
            }

            return dictionary;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static void ColoredDictionaryItem(ref Dictionary<int, string> dictionary, bool isColored, int id, string name, string color)
        {
            if (isColored)
            {
                if (color != null)
                {
                    dictionary.Add(id, @"<span style=""color:" + color + @""">" + name + "</span>");
                }
                else
                {
                    dictionary.Add(id, @"<span style=""color:" + Ia.Ngn.Cl.Model.Ui.Default.LightBackgroundColorList[id % Ia.Ngn.Cl.Model.Ui.Default.LightBackgroundColorList.Count].ToString() + @""">" + name + "</span>");
                }
            }
            else
            {
                dictionary.Add(id, name);
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static string ColoredName(int id, string name)
        {
           return ColoredName(id, name, null);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static string ColoredName(int id, string name, string color)
        {
            string coloredName;

            if (color != null) coloredName = @"<span style=""color:" + color + @""">" + name + "</span>";
            else coloredName = @"<span style=""color:" + Ia.Ngn.Cl.Model.Ui.Default.LightBackgroundColorList[id % Ia.Ngn.Cl.Model.Ui.Default.LightBackgroundColorList.Count].ToString() + @""">" + name + "</span>";

            return coloredName;
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

        public static XDocument XDocument
        {
            get
            {
                Assembly _assembly;
                StreamReader streamReader;

                if (xDocument == null)
                {
                    _assembly = Assembly.GetExecutingAssembly();
                    streamReader = new StreamReader(_assembly.GetManifestResourceStream("Ia.Ngn.Cl.model.data.report.xml"));

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
