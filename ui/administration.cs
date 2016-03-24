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

namespace Ia.Ngn.Cl.Model.Ui
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Administration support class for Next Generation Network (NGN) ui model.
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
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Administration() { }

        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool EmailDailyOfnStatusReport(string name, string email, out string result)
        {
            bool b;
            string content, subject;
            DateTime now;

            now = DateTime.UtcNow.AddHours(3);

            // subject can't have \r\n
            subject = "Daily Optical Fiber Network (OFN) Status Report (" + now.ToString("yyyy-MM-dd HH:mm") + ")";

            content = "Daily Optical Fiber Network (OFN) Status Report: " + now.ToString("yyyy-MM-dd HH:mm") + "\r\n"
                + "Status: undefined.\r\n"
                + @"For help send ""help"" in an email." + "\r\n";

            b = Ia.Ngn.Cl.Model.Ui.Mail.SendPlainMail(name, email, subject, content, out result);

            return b;
        }

        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool EmailListWithOntListProvisionedAndReadyButDoNotExistInCustomerDepartmentDatabase(string name, string email, out string result)
        {
            bool b;
            string content, subject;
            DataTable accessDataTable;
            DateTime now;

            now = DateTime.UtcNow.AddHours(3);

            subject = "Table of ONTs that are provisioned and ready within NGN access network but do not exist in the Customer Department's database (" + now.ToString("yyyy-MM-dd HH:mm") + ")";

            accessDataTable = Ia.Ngn.Cl.Model.Ui.Access.ReadListWithOntListProvisionedAndReadyButDoNotExistInCustomerDepartmentDatabaseDataTable();

            content = "Table of ONTs that are provisioned and ready within NGN access network but do not exist in the Customer Department's database (" + now.ToString("yyyy-MM-dd HH:mm") + ")";

            content += "\r\n";
            content += "\r\n";

            content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(accessDataTable);

            b = Ia.Ngn.Cl.Model.Ui.Mail.SendPlainMail(name, email, subject, content, out result);

            return b;
        }

        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool EmailStatistics(string name, string email, out string result)
        {
            bool b;
            string content, subject;
            DataTable accessDataTable, phoneDataTable, hsiDataTable;
            DateTime now;

            now = DateTime.UtcNow.AddHours(3);

            subject = "Statistics of Access, Phone, and HSI usage within the Optical Fiber Network (OFN) (" + now.ToString("yyyy-MM-dd HH:mm") + ")";

            Ia.Ngn.Cl.Model.Ui.Administration.StatisticsOfAccessAndPhoneAndHsiUsage(out accessDataTable, out phoneDataTable, out hsiDataTable);

            content = "Statistics of Access, Phone, and HSI usage within the Optical Fiber Network (OFN) (" + now.ToString("yyyy-MM-dd HH:mm") + ")";

            content += "\r\n";
            content += "\r\n";

            content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(accessDataTable);

            content += "\r\n";
            content += "\r\n";

            content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(phoneDataTable);

            content += "\r\n";
            content += "\r\n";

            content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(hsiDataTable);

            b = Ia.Ngn.Cl.Model.Ui.Mail.SendPlainMail(name, email, subject, content, out result);

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void StatisticsOfAccessAndPhoneAndHsiUsage(out DataTable accessDataTable, out DataTable phoneDataTable, out DataTable hsiDataTable)
        {
            // below:
            List<Ia.Ngn.Cl.Model.Business.Administration.Statistic> accessList, phoneList;

            accessList = Ia.Ngn.Cl.Model.Data.Administration.OltStatistic();
            phoneList = Ia.Ngn.Cl.Model.Data.Administration.PhoneStatistic(null);
            hsiDataTable = Ia.Ngn.Cl.Model.Data.Administration.HsiStatistics();

            accessDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.Business.Administration.Statistic>(accessList);
            phoneDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.Business.Administration.Statistic>(phoneList);
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        /// 
        /// </summary>
        public static string FrameworkStructureDataTable()
        {
            string htmlTable;
            List<Ia.Ngn.Cl.Model.Data.Administration.Framework> frameworkList;

            frameworkList = (from q in Ia.Ngn.Cl.Model.Data.Administration.FrameworkList where q.Type == "ministry" || q.Type == "supplier" select q).ToList();

            htmlTable = @"<div id=""framework-structure"">";

            foreach (Ia.Ngn.Cl.Model.Data.Administration.Framework framework in frameworkList)
            {
                htmlTable += FrameworkStructureDataTableIteration(framework);
            }

            htmlTable += "</div>";

            return htmlTable;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        /// 
        /// </summary>
        private static string FrameworkStructureDataTableIteration(Ia.Ngn.Cl.Model.Data.Administration.Framework framework)
        {
            string htmlTable;
            Ia.Ngn.Cl.Model.Staff headStaff;
            List<Ia.Ngn.Cl.Model.Staff> staffList;

            htmlTable = null;

            if (framework != null)
            {
                headStaff = (from s in Ia.Ngn.Cl.Model.Data.Staff.List where s.Framework != null && s.Framework.Id == framework.Id && s.IsHead select s).SingleOrDefault();

                htmlTable = "<table>";

                if (framework.Children != null && framework.Children.Count > 0)
                {
                    htmlTable += "<tr>";

                    htmlTable += @"<td class=""" + framework.Type + @""" colspan=""" + framework.Children.Count + @""">" + framework.ArabicName;
                    if (headStaff != null) htmlTable += @"<span class=""head"">" + headStaff.FirstAndMiddleName + "</span>";
                    htmlTable += @"</td>";

                    htmlTable += "</tr>";

                    htmlTable += "<tr>";

                    foreach (Ia.Ngn.Cl.Model.Data.Administration.Framework f in framework.Children)
                    {
                        htmlTable += @"<td class=""" + f.Type + @""">" + FrameworkStructureDataTableIteration(f) + "</td>";
                    }

                    htmlTable += "</tr>";
                }
                else
                {
                    staffList = (from s in Ia.Ngn.Cl.Model.Data.Staff.List where s.Framework != null && s.Framework.Id == framework.Id && !s.IsHead select s).ToList();

                    htmlTable += "<tr>";

                    htmlTable += @"<td class=""" + framework.Type + @""">" + framework.ArabicName;
                    if (headStaff != null) htmlTable += @"<span class=""head"">" + headStaff.FirstAndMiddleName + "</span>";

                    foreach(Ia.Ngn.Cl.Model.Staff staff in staffList)
                    {
                        htmlTable += @"<span class=""staff"">" + staff.FirstAndMiddleName + "</span>";
                    }

                    htmlTable += @"</td>";

                    htmlTable += "</tr>";
                }

                htmlTable += "</table>";
            }

            return htmlTable;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
