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
    /// Service support class for Next Generation Network (NGN) data model.
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
    public partial class Service
    {
        private static XDocument xDocument;
        private static List<Ia.Ngn.Cl.Model.Business.Service.KuwaitNgnArea> kuwaitNgnAreaList;
        private static List<int> fourDigitNumberDomainList;
        //private static List<Ia.Ngn.Cl.Model.Business.Service.Lceid> lceidList;
        //private static List<Ia.Ngn.Cl.Model.Business.Service.LceidLanRange> lceidLanRangeList;

        /// <summary/>
        public static int CountryCode { get { return 965; } }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Service() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Business.Service.KuwaitNgnArea> KuwaitNgnAreaList
        {
            get
            {
                if (kuwaitNgnAreaList == null || kuwaitNgnAreaList.Count == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["kuwaitNgnAreaList"] != null)
                    {
                        kuwaitNgnAreaList = (List<Ia.Ngn.Cl.Model.Business.Service.KuwaitNgnArea>)HttpContext.Current.Application["kuwaitNgnAreaList"];
                    }
                    else
                    {
                        kuwaitNgnAreaList = Ia.Ngn.Cl.Model.Data.Service._KuwaitNgnAreaList;

                        if (HttpContext.Current != null) HttpContext.Current.Application["kuwaitNgnAreaList"] = kuwaitNgnAreaList;
                    }
                }

                return kuwaitNgnAreaList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Business.Service.KuwaitNgnArea> _KuwaitNgnAreaList
        {
            get
            {
                if (kuwaitNgnAreaList == null || kuwaitNgnAreaList.Count == 0)
                {
                    int id;
                    string symbol;
                    Ia.Ngn.Cl.Model.Business.Service.KuwaitNgnArea kuwaitNgnArea;

                    kuwaitNgnAreaList = new List<Ia.Ngn.Cl.Model.Business.Service.KuwaitNgnArea>();

                    foreach (XElement xe in XDocument.Element("service").Elements("areaList").Elements("area"))
                    {
                        kuwaitNgnArea = new Ia.Ngn.Cl.Model.Business.Service.KuwaitNgnArea();
                        kuwaitNgnArea.Site = new Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Site();

                        id = int.Parse(xe.Attribute("id").Value);
                        kuwaitNgnArea.Id = id;

                        symbol = xe.Attribute("symbol").Value;
                        kuwaitNgnArea.Symbol = symbol;

                        kuwaitNgnArea.ServiceRequestAddressProvinceAreaName = xe.Attribute("serviceRequestAddressProvinceAreaName").Value;

                        kuwaitNgnArea.Name = (from q in Ia.Cl.Model.Kuwait.KuwaitAreaList where q.Id == id select q.Name).SingleOrDefault();
                        kuwaitNgnArea.ArabicName = (from q in Ia.Cl.Model.Kuwait.KuwaitAreaList where q.Id == id select q.ArabicName).SingleOrDefault();

                        kuwaitNgnArea.Site = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.SiteList where q.AreaSymbolList.Contains(symbol) select q).SingleOrDefault();
                        
                        kuwaitNgnAreaList.Add(kuwaitNgnArea);
                    }
                }

                return kuwaitNgnAreaList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> FourDigitNumberDomainList
        {
            get
            {
                if (fourDigitNumberDomainList == null || fourDigitNumberDomainList.Count == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["fourDigitNumberDomainList"] != null)
                    {
                        fourDigitNumberDomainList = (List<int>)HttpContext.Current.Application["fourDigitNumberDomainList"];
                    }
                    else
                    {
                        fourDigitNumberDomainList = Ia.Ngn.Cl.Model.Data.Service._FourDigitNumberDomainList;

                        if (HttpContext.Current != null) HttpContext.Current.Application["fourDigitNumberDomainList"] = fourDigitNumberDomainList;
                    }
                }

                return fourDigitNumberDomainList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<int> _FourDigitNumberDomainList
        {
            get
            {
                if (fourDigitNumberDomainList == null || fourDigitNumberDomainList.Count == 0)
                {
                    fourDigitNumberDomainList = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.RouterList select q).SelectMany(r => r.DomainList).Distinct().ToList();

                    fourDigitNumberDomainList.Sort();
                }

                return fourDigitNumberDomainList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> FourDigitNumberDomainListWithinNokiaSwitch
        {
            get
            {
                Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Vendor vendor;

                vendor = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.VendorList where q.ShortName == "No" select q).Single();

                return FourDigitNumberDomainListWithinSwitchVendor(vendor);
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> FourDigitNumberDomainListWithinHuaweiSwitch
        {
            get
            {
                Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Vendor vendor;

                vendor = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.VendorList where q.ShortName == "Hu" select q).Single();

                return FourDigitNumberDomainListWithinSwitchVendor(vendor);
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> FourDigitNumberDomainListWithinNokiaSiemensSwitch
        {
            get
            {
                Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Vendor vendor;

                vendor = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.VendorList where q.ShortName == "NS" select q).Single();

                return FourDigitNumberDomainListWithinSwitchVendor(vendor);
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<int> FourDigitNumberDomainListWithinSwitchVendor(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Vendor vendor)
        {
            List<int> fourDigitNumberDomain;

            fourDigitNumberDomain = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.RouterList where q.Vendor == vendor select q).SelectMany(r => r.DomainList).ToList();

            fourDigitNumberDomain.Sort();

            return fourDigitNumberDomain;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> AllPossibleServiceNumberList
        {
            get
            {
                int n;
                List<int> allPossibleServiceNumberList;

                allPossibleServiceNumberList = new List<int>(10000 * FourDigitNumberDomainList.Count);

                foreach (int o in FourDigitNumberDomainList)
                {
                    for (int p = 0; p < 10000; p++)
                    {
                        n = o * 10000 + p;

                        allPossibleServiceNumberList.Add(n);
                    }
                }

                return allPossibleServiceNumberList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> AllPossibleServiceNumberListWithinDomain(int domain)
        {
            int n;
            List<int> allPossibleServiceNumberListWithinDomain;

            allPossibleServiceNumberListWithinDomain = new List<int>(10000);

            for (int p = 0; p < 10000; p++)
            {
                n = domain * 10000 + p;

                allPossibleServiceNumberListWithinDomain.Add(n);
            }

            return allPossibleServiceNumberListWithinDomain;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> AllPossibleServiceNumberListWithinNokiaSwitch
        {
            get
            {
                Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Vendor nokia;
                List<int> allPossibleServiceNumberListWithinSwitch;

                nokia = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.VendorList where q.ShortName == "No" select q).Single();

                allPossibleServiceNumberListWithinSwitch = AllPossibleServiceNumberListWithinSwitchVendor(nokia);

                return allPossibleServiceNumberListWithinSwitch;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> AllPossibleServiceNumberListWithinHuaweiSwitch
        {
            get
            {
                Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Vendor huawei;
                List<int> allPossibleServiceNumberListWithinSwitch;

                huawei = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.VendorList where q.ShortName == "Hu" select q).Single();

                allPossibleServiceNumberListWithinSwitch = AllPossibleServiceNumberListWithinSwitchVendor(huawei);

                return allPossibleServiceNumberListWithinSwitch;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> AllPossibleServiceNumberListWithinNokiaSiemensSwitch
        {
            get
            {
                Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Vendor nokiaSiemens;
                List<int> allPossibleServiceNumberListWithinSwitch;

                nokiaSiemens = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.VendorList where q.ShortName == "NS" select q).Single();

                allPossibleServiceNumberListWithinSwitch = AllPossibleServiceNumberListWithinSwitchVendor(nokiaSiemens);

                return allPossibleServiceNumberListWithinSwitch;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<int> AllPossibleServiceNumberListWithinSwitchVendor(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Vendor vendor)
        {
            int n;
            List<int> fourDigitNumberDomainListWithinSwitchVendor, allPossibleServiceNumberListWithinSwitchVendor;

            fourDigitNumberDomainListWithinSwitchVendor = FourDigitNumberDomainListWithinSwitchVendor(vendor);

            allPossibleServiceNumberListWithinSwitchVendor = new List<int>(10000 * fourDigitNumberDomainListWithinSwitchVendor.Count);

            foreach (int o in fourDigitNumberDomainListWithinSwitchVendor)
            {
                for (int p = 0; p < 10000; p++)
                {
                    n = o * 10000 + p;

                    allPossibleServiceNumberListWithinSwitchVendor.Add(n);
                }
            }

            return allPossibleServiceNumberListWithinSwitchVendor;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> AllPossibleServiceNumberListWithinTestSwitch
        {
            get
            {
                int n, testDomain;
                List<int> list;

                testDomain = 2497;

                list = new List<int>(10000 * 1);

                for (int p = 0; p < 10000; p++)
                {
                    n = testDomain * 10000 + p;

                    list.Add(n);
                }

                return list;
            }
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static List<string> ActiveServiceNumbersWithinDomainList(string domain)
        {
            List<string> serviceList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceList = (from q in db.ServiceRequestServices
                               where q.ServiceType == 1 && q.Service.Substring(0, 4) == domain
                               select q.Service).ToList();
            }

            return serviceList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<string> InactiveServiceNumbersWithinDomainList(string domain)
        {
            List<int> possibleServiceList;
            List<string> serviceList, inactiveServiceList;

            serviceList = ActiveServiceNumbersWithinDomainList(domain);

            possibleServiceList = Ia.Ngn.Cl.Model.Data.Service.AllPossibleServiceNumberListWithinDomain(int.Parse(domain));

            inactiveServiceList = new List<string>(possibleServiceList.Count - serviceList.Count);

            // below: extract numbers within possible but not in serviceList
            foreach (int i in possibleServiceList)
            {
                if (!serviceList.Contains(i.ToString())) inactiveServiceList.Add(i.ToString());
            }

            return inactiveServiceList;
        }

        /*
        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static List<string> ServiceNumbersWithServiceAdministrativeStateNotNullFromOldNgnDatabase()
        {
            string sql, connectionString;
            DataTable dt;
            List<string> serviceList;
            Ia.Cl.Model.Db.SqlServer sqlServer;

            serviceList = new List<string>(2000);

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionToNgn"].ConnectionString;

            sqlServer = new Ia.Cl.Model.Db.SqlServer(connectionString);

            sql = "select sa.dn,sa.state from ia_service_administrative as sa where sa.state is not null";

            dt = sqlServer.Select(sql);

            foreach (DataRow dr in dt.Rows) serviceList.Add(dr["dn"].ToString());

            return serviceList;
        }
         */ 

        /*
        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static List<string> ServiceNumberWithLastOrderDeactivateIssuedWithinLastSixMonthFromOldNgnDatabaseList()
        {
            string sql, sixMonthsAgo, connectionString;
            Hashtable ht;
            DataTable dt;
            List<string> serviceList;
            Ia.Cl.Model.Db.SqlServer sqlServer;

            // <function id="1" name="Activate" parameter=""/>
            // <function id="2" name="Deactivate" parameter=""/>

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionToNgn"].ConnectionString;

            sixMonthsAgo = DateTime.UtcNow.AddHours(3).AddMonths(-6).ToString("yyyy-MM-dd");

            sqlServer = new Ia.Cl.Model.Db.SqlServer(connectionString);

            sql = @"select id, function_id, ref, created
                    from ia_log
                    where direction_type_id = 1 and system_id = 1 and process_id = 1 and (function_id = 1 or function_id = 2) and created >= '" + sixMonthsAgo + @"'
                    order by created asc";

            dt = sqlServer.Select(sql);

            serviceList = new List<string>(dt.Rows.Count);
            ht = new Hashtable(dt.Rows.Count);

            // below: important: loop will start from oldest records
            foreach (DataRow dr in dt.Rows) ht[dr["ref"].ToString()] = dr["function_id"].ToString();

            foreach (string _ref in ht.Keys)
            {
                // <function id="2" name="Deactivate" parameter=""/>
                if (ht[_ref].ToString() == "2") serviceList.Add(_ref);
            }

            return serviceList;
        }
         */ 

        /*
        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static List<string> ServiceNumberWithLastOrderDeactivateIssuedWithinLastSixMonthsFromOldNgnDatabaseAndExistInServiceRequestServiceList()
        {
            List<string> serviceList, oldServiceList, serviceRequestServiceNumberList;

            oldServiceList = Ia.Ngn.Cl.Model.Data.Service.ServiceNumberWithLastOrderDeactivateIssuedWithinLastSixMonthFromOldNgnDatabaseList();

            serviceRequestServiceNumberList = Ia.Ngn.Cl.Model.Data.ServiceRequestService.ServiceStringList();

            serviceList = new List<string>(oldServiceList.Count);

            // below: extract commons

            if (oldServiceList.Count > 0)
            {
                foreach (string s in oldServiceList)
                {
                    if (serviceRequestServiceNumberList.Contains(s))
                    {
                        serviceList.Add(s);
                    }
                }
            }

            return serviceList;
        }
         */ 

        /*
        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static Hashtable ServiceNumbersWithServiceAdministrativeStateNotNullFromOldNgnDatabaseHashtable()
        {
            string sql, connectionString;
            Hashtable ht;
            DataTable dt;
            Ia.Cl.Model.Db.SqlServer sqlServer;

            ht = new Hashtable(2000);

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionToNgn"].ConnectionString;

            sqlServer = new Ia.Cl.Model.Db.SqlServer(connectionString);

            sql = "select sa.dn,sa.state from ia_service_administrative as sa where sa.state is not null";

            dt = sqlServer.Select(sql);

            foreach (DataRow dr in dt.Rows)
            {
                ht[dr["dn"].ToString()] = int.Parse(dr["state"].ToString());
            }

            return ht;
        }
         */ 

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
                    streamReader = new StreamReader(_assembly.GetManifestResourceStream("Ia.Ngn.Cl.model.data.service.xml"));

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
