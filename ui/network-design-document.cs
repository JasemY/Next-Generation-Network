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
using System.Reflection;
using System.Linq;

namespace Ia.Ngn.Cl.Model.Ui
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Network Design Document support class for Next Generation Network (NGN) UI model.
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
    public partial class NetworkDesignDocument
    {
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public NetworkDesignDocument() { }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        /// 
        /// </summary>
        public static string StructureDataTable()
        {
            int siteCount, routerCount, odfCount, oltCount;
            int siteOltCount, routerOltCount, odfOltCount, oltOltCount;
            string htmlTable;

            siteCount = routerCount = odfCount = oltCount = 0;
            siteOltCount = routerOltCount = odfOltCount = oltOltCount = 0;

            htmlTable = @"<div id=""network-design-document-structure"">";

            htmlTable += "<table>";

            siteCount = 1;

            foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Site site in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.SiteList)
            {
                siteOltCount = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList where q.Odf != null && q.Odf.Router != null && q.Odf.Router.Site == site select q).Count();

                if (site.Routers.Count > 0)
                {
                    routerCount = 1;

                    foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Router router in site.Routers)
                    {
                        routerOltCount = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList where q.Odf != null && q.Odf.Router == router select q).Count();

                        if (router.Odfs.Count > 0)
                        {
                            odfCount = 1;

                            foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Odf odf in router.Odfs)
                            {
                                odfOltCount = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList where q.Odf == odf select q).Count();

                                oltCount = 1;

                                foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt in odf.Olts)
                                {
                                    oltOltCount = odf.Olts.Count;

                                    htmlTable += @"<tr class=site" + siteCount + ">";

                                    if (routerCount == 1 && odfCount == 1 && oltCount == 1) htmlTable += "<td class=site rowspan=" + siteOltCount + ">" + SiteStructureDataTable(site) +"</td>";
                                    if (odfCount == 1 && oltCount == 1) htmlTable += "<td class=router rowspan=" + routerOltCount + ">" + RouterStructureDataTable(router) + "</td>";
                                    if (oltCount == 1) htmlTable += "<td class=odf rowspan=" + odfOltCount + ">" + OdfStructureDataTable(odf) + "</td>";

                                    htmlTable += "<td class=olt>" + OltStructureDataTable(olt) + "</td>";

                                    htmlTable += @"</tr>";

                                    oltCount++;
                                }

                                odfCount++;
                            }
                        }

                        routerCount++;
                    }
                }

                siteCount++;
            }
           
            htmlTable += "</table>";

            htmlTable += "</div>";

            return htmlTable;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        /// 
        /// </summary>
        private static string SiteStructureDataTable(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Site site)
        {
            string content, kuwaitNgnAreas;

            kuwaitNgnAreas = "";

            foreach (Ia.Ngn.Cl.Model.Business.Service.KuwaitNgnArea ka in site.KuwaitNgnAreas) kuwaitNgnAreas += ka.NameArabicName + "<br/>";

            kuwaitNgnAreas = kuwaitNgnAreas.Trim(',');

            content = site.NameArabicName;
            content += @"<br/>" + site.AreaSymbolList;
            content += @"<br/>" + kuwaitNgnAreas;

            return content;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        /// 
        /// </summary>
        private static string RouterStructureDataTable(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Router router)
        {
            string content, domainList;

            domainList = "";

            foreach (int i in router.DomainList) domainList += i + ", ";

            domainList = domainList.Trim();
            domainList = domainList.Trim(',');

            content = router.Vendor.Name;
            content += @"<br/>" + domainList;

            return content;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        /// 
        /// </summary>
        private static string OdfStructureDataTable(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Odf odf)
        {
            string content;

            content = odf.Name + @"<br/>" + odf.Vendor.Name + @"<br/>" + odf.PrimaryIp + @"<br/>" + odf.SecondaryIp;

            return content;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        /// 
        /// </summary>
        private static string OltStructureDataTable(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt)
        {
            string content;
            List<int> list;

            list = new List<int>();

            foreach (int i in olt.UsedPonList) if (i != 0) list.Add(i);

            content = olt.Symbol;
            content += @" (" + olt.AmsName + ") ";
            content += @" PONs: " + Ia.Cl.Model.Default.ConvertANumberArrayListToHyphenAndCommaSeperatedNumberString(list);
            content += @" " + olt.EdgeRouter;
            content += @" " + olt.ImsFsdb;
            content += @" " + olt.ImsService;
            content += @" " + olt.MgcIp;
            content += @" " + olt.MgcSecondaryIp;

            return content;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////    
    }
}
