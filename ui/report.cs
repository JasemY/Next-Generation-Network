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

namespace Ia.Ngn.Cl.Model.Ui
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Report support class for Next Generation Network (NGN) ui model.
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
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Report() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static DataTable ReadSpecificUserIdAndAreaIdReportWithReportOpenStatusListDataTable(Guid userId, int areaId)
        {
            // below:
            DataRow dr;
            DataTable dt;
            List<Ia.Ngn.Cl.Model.Ui.ReportAccessServiceRequest> reportAccessServiceRequestList;

            dt = new DataTable("");
            dt.Columns.Add("Service");
            dt.Columns.Add("Action");
            dt.Columns.Add("Access/ONT Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("Contact");

            reportAccessServiceRequestList = Ia.Ngn.Cl.Model.Data.Report.ReadSpecificUserIdAndAreaIdReportWithReportOpenStatusList(userId, areaId);

            foreach (Ia.Ngn.Cl.Model.Ui.ReportAccessServiceRequest reportAccessServiceRequest in reportAccessServiceRequestList)
            {
                dr = dt.NewRow();

                try
                {
                    dr["Service"] = Ia.Ngn.Cl.Model.Business.Service.ServiceName(reportAccessServiceRequest.Report.Service, reportAccessServiceRequest.Report.ServiceType);
                }
                catch (Exception)
                {

                }
                try
                {
                    dr["Action"] = (reportAccessServiceRequest.Report.LastReportHistory != null) ? Ia.Ngn.Cl.Model.Data.Report.ActionDictionary()[reportAccessServiceRequest.Report.LastReportHistory.Action].ToString() : "";
                }
                catch (Exception)
                {

                }
                try
                {
                    dr["Access/ONT Name"] = reportAccessServiceRequest.Access.Name;
                }
                catch (Exception)
                {

                }
                try
                {
                    dr["Address"] = Ia.Cl.Model.Html.Decode(reportAccessServiceRequest.Access.Address);
                }
                catch (Exception)
                {

                }
                try
                {
                    dr["Contact"] = Ia.Cl.Model.Html.Decode(reportAccessServiceRequest.Report.Contact);

                    dt.Rows.Add(dr);
                }
                catch (Exception)
                {

                }
            }

            return dt;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
