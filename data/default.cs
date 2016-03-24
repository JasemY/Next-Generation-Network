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

namespace Ia.Ngn.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Default support class for Next Generation Network (NGN) data model.
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
    public partial class Default
    {
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Default() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ui.ServiceCustomerAddressAccessOntNameStatisticalOntName> DifferentOntNameAndStatisticalOntNameList(int siteId)
        {
            string address, level, siteRouterDomainListString;
            Hashtable ht;
            Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Site site;
            Ia.Ngn.Cl.Model.Business.ServiceAddress serviceAddress;
            Ia.Ngn.Cl.Model.Access statisticalAccess;
            Ia.Ngn.Cl.Model.Ui.ServiceCustomerAddressAccessOntNameStatisticalOntName l;
            Ia.Ngn.Cl.Model.Ui.Nokia.AlInitialInstallation alInitialInstallation;

            List<int> siteRouterDomainList, areaIdList;
            List<Ia.Ngn.Cl.Model.Ui.ServiceCustomerAddressAccessOntNameStatisticalOntName> list;
            List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList;
            List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList;
            List<Ia.Ngn.Cl.Model.Access> accessList;
            List<Ia.Ngn.Cl.Model.Ui.Nokia.AlInitialInstallation> alInitialInstallationList;

            level = "";
            areaIdList = new List<int>();

            alInitialInstallationList = Ia.Ngn.Cl.Model.Data.Nokia.Default.NokiaCustomerInitialInstallationData;

            site = (from a in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.SiteList where a.Id == siteId select a).SingleOrDefault();

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                siteRouterDomainList = (from r in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.RouterList where r.Site.Id == siteId select r).SelectMany(z => z.DomainList).ToList();

                siteRouterDomainListString = Ia.Cl.Model.Default.NumberListToCommaSeperatedNumberString(siteRouterDomainList);

                serviceRequestList = (from sr in db.ServiceRequests where siteRouterDomainList.Contains(sr.Number / 10000) select sr).ToList();

                ht = Ia.Ngn.Cl.Model.Data.ServiceRequest.NumberToCustomerAddressHashtable(siteRouterDomainList);

                serviceRequestServiceList = (from srs in db.ServiceRequestServices where siteRouterDomainListString.Contains(srs.Service.Substring(0, 4)) select srs).ToList();

                areaIdList = site.KuwaitNgnAreas.Select(i=>i.Id).ToList();

                accessList = (from a in db.Accesses join ai in areaIdList on a.AreaId equals ai select a).ToList();

                list = new List<Ia.Ngn.Cl.Model.Ui.ServiceCustomerAddressAccessOntNameStatisticalOntName>();

                foreach (Ia.Ngn.Cl.Model.ServiceRequestService srs in serviceRequestServiceList)
                {
                    l = new Ia.Ngn.Cl.Model.Ui.ServiceCustomerAddressAccessOntNameStatisticalOntName();
                    
                    if (ht[srs.Service] != null) address = ht[srs.Service].ToString();
                    else address = null;

                    l.Service = srs.Service;
                    l.CustomerAddress = address;
                    l.Access = srs.Access;

                    if (srs.Access != null)
                    {
                        l.OntAddress = srs.Access.Address;

                        alInitialInstallation = (from a in alInitialInstallationList where a.AccessId == srs.Access.Id select a).SingleOrDefault();

                        if(alInitialInstallation != null)
                        {
                            l.Note = alInitialInstallation.Contact + " " + alInitialInstallation.OwnerName + " " + alInitialInstallation.Remark + " " + alInitialInstallation.BuildingType;
                        }
                    }

                    serviceAddress = Ia.Ngn.Cl.Model.Business.ServiceRequest.StatisticalServiceAddress(srs.Service, address, out level);

                    statisticalAccess = Ia.Ngn.Cl.Model.Data.Access.StatisticalAccess(serviceAddress, ref accessList);

                    if (statisticalAccess != null)
                    {
                        l.Block = statisticalAccess.Block;
                        l.Street = statisticalAccess.Street;
                        l.PremisesOld = statisticalAccess.PremisesOld;
                        l.PremisesNew = statisticalAccess.PremisesNew;
                        l.KuwaitNgnAreaName = (from kna in Ia.Ngn.Cl.Model.Data.Service.KuwaitNgnAreaList where kna.Id == statisticalAccess.AreaId select kna.NameArabicName).SingleOrDefault();
                    }

                    if (statisticalAccess != null) l.StatisticalOntName = statisticalAccess.Name;

                    if (serviceAddress.AreaId != 0)
                    {
                        l.StatisticalAddress = serviceAddress.Address;

                        if (srs.Access != null) l.OntName = l.Access.Name;

                        //if (l.OntName != null && l.OntName == l.StatisticalOntName) l.Note = Ia.Cl.Model.Default.YesNo(true);
                        //else l.Note = Ia.Cl.Model.Default.YesNo(false);

                        //l.Note += " (" + level + ")";
                    }
                    else
                    {
                        //l.Note = Ia.Cl.Model.Default.YesNo(false);

                        //l.Note += " (AreaId zero)";
                    }

                    // below: this will skip Sabah Al-Salem area
                    if (l.CustomerAddress != null && l.CustomerAddress.Contains("صباح")) { }
                    else list.Add(l);
                }
            }

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////    
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
