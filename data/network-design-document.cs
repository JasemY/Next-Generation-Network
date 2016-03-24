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
    /// Network Design Document support class for Next Generation Network (NGN) data model.
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
        private static XDocument xDocument;
        private static List<Network> networkList;
        private static List<Vendor> vendorList;
        private static List<Site> siteList;
        private static List<Router> routerList;
        private static List<Oam> oamList;
        private static List<Odf> odfList;
        private static List<Msan> msanList;
        private static List<Olt> oltList;
        private static List<Pon> ponList;
        private static List<Ont> ontList;

        /*
        protected void Application_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Application.Clear();
            ... 
        }
         */ 


        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public NetworkDesignDocument() { }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Vendor
        {
            public Vendor() { }

            public int Id { get; set; }
            public string Name { get; set; }
            public string ShortName { get; set; }
            public string ArabicName { get; set; }
            public string ImageUrl { get; set; }

            public virtual ICollection<Router> Routers
            {
                get
                {
                    return (from q in RouterList where q.Vendor == this select q).ToList();
                }
            }

            public virtual ICollection<Odf> Odfs
            {
                get
                {
                    return (from q in odfList where q.Vendor == this select q).ToList();
                }
            }

            public virtual ICollection<Msan> Msans
            {
                get
                {
                    return (from q in MsanList where q.Vendor == this select q).ToList();
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////    

        public class Network
        {
            public Network() { }

            public int Id { get; set; }
            public string Name { get; set; }
            public virtual ICollection<Site> Sites
            {
                get 
                {
                    return (from q in SiteList where q.Network == this select q).ToList();
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Site
        {
            public Site() { }

            public int Id { get; set; }
            public string Name { get; set; }
            public string ArabicName { get; set; }
            public string NameArabicName 
            {
                get
                {
                    return Name + " (" + ArabicName + ")";
                }
            }
            public string AreaSymbolList { get; set; }
            public virtual Network Network { get; set; }

            public virtual ICollection<Router> Routers
            {
                get
                {
                    return (from q in RouterList where q.Site == this select q).ToList();
                }
            }

            public virtual ICollection<Ia.Ngn.Cl.Model.Business.Service.KuwaitNgnArea> KuwaitNgnAreas
            {
                get
                {
                    return (from q in Ia.Ngn.Cl.Model.Data.Service.KuwaitNgnAreaList where this.AreaSymbolList.Contains(q.Symbol) select q).ToList();
                }
            }

            public int SiteId(int networkId, int siteId)
            {
                return networkId * 100 + siteId;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Router
        {
            public Router() { }

            public int Id { get; set; }
            public string Name { get; set; }
            public string DomainListString { get; set; }
            public List<int> DomainList { get; set; }
            public virtual Vendor Vendor { get; set; }
            public virtual Site Site { get; set; }
            public virtual ICollection<Oam> Oams
            {
                get
                {
                    return (from q in OamList where q.Router == this select q).ToList();
                }
            }

            public virtual ICollection<Odf> Odfs
            {
                get
                {
                    return (from q in OdfList where q.Router == this select q).ToList();
                }
            }

            public virtual ICollection<Msan> Msans
            {
                get
                {
                    return (from q in MsanList where q.Router == this select q).ToList();
                }
            }

            public int RouterId(int siteId, int routerId)
            {
                return siteId * 100 + routerId;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Oam
        {
            public Oam()  { }

            public int Id { get; set; }
            public string Network { get; set; }
            public string SubnetMask { get; set; }
            public string Gateway { get; set; }
            public int Vlan { get; set; }
            public int Vpls { get; set; }
            public string FtpIp { get; set; }
            public string ConfigFile { get; set; }
            public virtual Router Router { get; set; }

            public int OamId(int routerId, int oamId)
            {
                return routerId * 100 + oamId;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Odf
        {
            public Odf() { }

            public int Id { get; set; }
            public string Name { get; set; }
            public string PrimaryIp { get; set; }
            public string SecondaryIp { get; set; }
            public string SubnetMask { get; set; }
            public virtual Vendor Vendor { get; set; }
            public virtual Router Router { get; set; }
            public virtual ICollection<Olt> Olts
            {
                get
                {
                    return (from q in OltList where q.Odf == this select q).ToList();
                }
            }

            public int OdfId(int routerId, int odfId)
            {
                return routerId * 100 + odfId;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Msan
        {
            public Msan() { }

            public int Id { get; set; }
            public string Name { get; set; }
            public string Symbol { get; set; }
            public virtual Vendor Vendor { get; set; }
            public virtual Router Router { get; set; }
            public int MsanId(int routerId, int msanId)
            {
                return routerId * 100 + msanId;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Olt
        {
            public Olt()  { }

            public int Id { get; set; }
            public string Type { get; set; }
            public int Rack { get; set; }
            public int Sub { get; set; }
            public string Name { get; set; }
            public string AmsName { get; set; }
            public string Symbol { get; set; }
            public int NumberOfPons { get; set; }
            public List<int> UsedPonList { get; set; }
            public string MgcIp { get; set; }
            public string MgcSecondaryIp { get; set; }
            public int Vlan { get; set; }
            public string NetworkNumber { get; set; }
            public string GatewayIp { get; set; }
            public int ImsService { get; set; }
            public string ImsFsdb { get; set; }
            public string EdgeRouter { get; set; }
            public virtual Odf Odf { get; set; }
            public virtual ICollection<Pon> PonList
            {
                get
                {
                    return (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.PonList where q.Olt == this select q).ToList();
                }
            }

            public int OltId(int odfId, int oltId)
            {
                return odfId * 100 + oltId;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Pon
        {
            public Pon()  { }

            public long Id { get; set; }
            public int Index { get; set; }
            public int Rack { get; set; }
            public int Sub { get; set; }
            public int Card { get; set; }
            public int Port { get; set; }
            public int Number { get; set; }
            public string Position { get; set; }
            public string Name { get; set; }
            public virtual Olt Olt { get; set; }
            public virtual ICollection<Ont> Onts { get; set; }

            public long PonId(long oltId, int ponId)
            {
                return oltId * 100 + ponId;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Ont
        {
            public Ont() { }

            public string Id { get; set; }
            public int Rack { get; set; }
            public int Sub { get; set; }
            public int Card { get; set; }
            public int Port { get; set; }
            public int Number { get; set; }
            public string Position { get; set; }
            public string Ip { get; set; }
            //public int GatewayId { get; set; }
            public virtual Pon Pon { get; set; }
            public virtual Access Access { get; set; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Access
        {
            public Access() { }

            public string Id { get; set; }
            public string Name { get; set; }
            //public virtual Pon Pon { get; set; }

            public string AccessId(int oltId, int ponNumber, int ontNumber)
            {
                return Ia.Ngn.Cl.Model.Access.AccessId(oltId, ponNumber, ontNumber);
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> OntList
        {
            get
            {
                if (ontList == null || ontList.Count == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["ontList"] != null)
                    {
                        ontList = (List<Ont>)HttpContext.Current.Application["ontList"];
                    }
                    else
                    {
                        ontList = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument._OntList;

                        if (HttpContext.Current != null) HttpContext.Current.Application["ontList"] = ontList;
                    }
                }

                return ontList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> _OntList
        {
            get
            {
                if (ontList == null || ontList.Count == 0)
                {
                    long firstIpLong, ipLong, diff, u;
                    Ont ont;
                    Access access;

                    ontList = new List<Ont>(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList.Count * /*32*/ 256 * 32); // 256 is the bigger number

                    foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Pon pon in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.PonList)
                    {
                        firstIpLong = Ia.Cl.Model.Default.IpToDec(pon.Olt.NetworkNumber);

                        for (int ontNumber = 1; ontNumber <= 32; ontNumber++)
                        {
                            ont = new Ont();
                            access = new Access();

                            ont.Pon = pon;

                            ont.Rack = ont.Pon.Olt.Rack;
                            ont.Sub = ont.Pon.Olt.Sub;
                            ont.Card = ont.Pon.Card;
                            ont.Port = ont.Pon.Port;

                            ont.Number = ontNumber;

                            ont.Position = ont.Pon.Olt.AmsName + "-" + ont.Card + "-" + ont.Port + "-" + ont.Number;

                            access.Id = access.AccessId(ont.Pon.Olt.Id, ont.Pon.Number, ont.Number);
                            //access.Pon = pon;
                            access.Name = ont.Pon.Olt.Symbol + "." + ont.Pon.Number + "." + ont.Number;
                            ont.Access = access;

                            ipLong = firstIpLong + pon.Index * 32 + ont.Number;
                            diff = ipLong - firstIpLong;

                            // below: skip *.*.*.0
                            if (diff >= 1021) u = 4;
                            else if (diff >= 766) u = 3;
                            else if (diff >= 511) u = 2;
                            else if (diff >= 256) u = 1;
                            else u = 0;

                            ipLong += u;

                            ont.Ip = Ia.Cl.Model.Default.DecToIp((int)ipLong);

                            //ont.GatewayId = (Ia.Ngn.Cl.Model.Data.Nokia.Default.IpGatewayIdHashtable[ont.Ip] != null) ? (int)Ia.Ngn.Cl.Model.Data.Nokia.Default.IpGatewayIdHashtable[ont.Ip] : 0;

                            ont.Id = Ia.Ngn.Cl.Model.Business.Ont.OntId(ont.Pon.Olt.Id, ont.Rack, ont.Sub, ont.Card, ont.Port, ont.Number);

                            ontList.Add(ont);
                        }
                    }
                }

                return ontList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// A Hashtable "faster" version of OntList for frequent use
        /// </summary>
        public static Hashtable OntListAccessIdToAccessVendorHashtable
        {
            get
            {
                Hashtable ht;

                ht = new Hashtable(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList.Count);

                foreach(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont ont in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList)
                {
                    ht[ont.Access.Id] = ont.Pon.Olt.Odf.Vendor;
                }

                return ht;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// A Hashtable "faster" version of OntList for frequent use
        /// </summary>
        public static Hashtable OntListAccessIdToOntHashtable
        {
            get
            {
                Hashtable ht;

                ht = new Hashtable(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList.Count);

                foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont ont in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList)
                {
                    ht[ont.Access.Id] = ont;
                }

                return ht;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// A Hashtable "faster" version of OntList for frequent use
        /// </summary>
        public static Hashtable OntListIdToAccessNameHashtable
        {
            get
            {
                Hashtable ht;

                ht = new Hashtable(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList.Count);

                foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont ont in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList)
                {
                    ht[ont.Id] = ont.Access.Name;
                }

                return ht;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// A Hashtable "faster" version for frequent use
        /// </summary>
        public static Hashtable OntListAccessIdToAccessNameHashtable
        {
            get
            {
                Hashtable ht;

                ht = new Hashtable(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList.Count);

                foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont ont in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList)
                {
                    if (ont.Access != null) ht[ont.Access.Id] = ont.Access.Name;
                }

                return ht;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// A Hashtable "faster" version for frequent use
        /// </summary>
        public static Hashtable OntListIpToAccessNameHashtable
        {
            get
            {
                Hashtable ht;

                ht = new Hashtable(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList.Count);

                foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont ont in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList)
                {
                    if (ont.Access != null) ht[ont.Ip] = ont.Access.Name;
                }

                return ht;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<string, int> OntListIpDictionary
        {
            get
            {
                Dictionary<string, int> ipDictionary;

                ipDictionary = (from o in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList select o.Ip).Distinct().ToDictionary(n => n, n => 1);

                return ipDictionary;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool AccessNameIsWithinAllowedOntList(string accessName)
        {
            return OntNameIsWithinAllowedOntList(accessName);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool OntNameIsWithinAllowedOntList(string ontName)
        {
            // below: this checks that this ontName is within the standard possible values in network
            
            bool isWithinAllowedOnts;

            isWithinAllowedOnts = (from q in OntList where q.Access.Name == ontName select q).SingleOrDefault() != null;

            return isWithinAllowedOnts;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool PonNameIsWithinAllowedOntList(string ontAreaPon)
        {
            // below: this checks that this ontName is within the standard possible values in network
            
            bool isWithinAllowedOnts;

            if (ontAreaPon == null || ontAreaPon.Length == 0) isWithinAllowedOnts = false;
            else
            {
                isWithinAllowedOnts = (from q in OntList where q.Access.Name.Contains(ontAreaPon + ".") select q).FirstOrDefault() != null;
            }

            return isWithinAllowedOnts;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Pon> PonList
        {
            get
            {
                if (ponList == null || ponList.Count == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["ponList"] != null)
                    {
                        ponList = (List<Pon>)HttpContext.Current.Application["ponList"];
                    }
                    else
                    {
                        ponList = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument._PonList;

                        if (HttpContext.Current != null) HttpContext.Current.Application["ponList"] = ponList;
                    }
                }

                return ponList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Pon> _PonList
        {
            get
            {
                if (ponList == null || ponList.Count == 0)
                {
                    int ponNumber;
                    Pon pon;

                    ponList = new List<Pon>(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList.Count * /*32 56*/ 256); // 256 is the bigger number

                    foreach (Olt olt in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList)
                    {
                        if (olt.UsedPonList.Count == olt.NumberOfPons)
                        {
                            // below: Note that I will use the INDEX of the PON number in the UsedPonList to construct the PON Id, this will make it possible 
                            // to match this index with that from the Ip, position list, because that list does not recognize PONs.
                            for (int i = 0; i < olt.UsedPonList.Count; i++)
                            {
                                if ((int)olt.UsedPonList[i] != 0)
                                {
                                    pon = new Pon();

                                    ponNumber = (int)olt.UsedPonList[i];

                                    pon.Id = pon.PonId(olt.Id, i);
                                    pon.Index = i;

                                    pon.Olt = (from q in OltList where q.Id == olt.Id select q).SingleOrDefault();

                                    pon.Rack = pon.Olt.Rack;
                                    pon.Sub = pon.Olt.Sub;

                                    pon.Card = pon.Index / (olt.UsedPonList.Count / 16) + 1;
                                    pon.Port = pon.Index % (olt.UsedPonList.Count / 16) + 1;

                                    pon.Number = ponNumber;
                                    pon.Position = pon.Olt.AmsName + "-" + pon.Card + "-" + pon.Port;

                                    pon.Name = pon.Olt.Symbol + "." + pon.Number;

                                    ponList.Add(pon);
                                }
                            }
                        }
                        else throw new ArgumentOutOfRangeException(@"olt.UsedPonList.Count != olt.NumberOfPons");
                    }
                }

                return ponList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt> OltList
        {
            get
            {
                if (oltList == null || oltList.Count == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["oltList"] != null)
                    {
                        oltList = (List<Olt>)HttpContext.Current.Application["oltList"];
                    }
                    else
                    {
                        oltList = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument._OltList;

                        if (HttpContext.Current != null) HttpContext.Current.Application["oltList"] = oltList;
                    }
                }

                return oltList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt> _OltList
        {
            get
            {
                if (oltList == null || oltList.Count == 0)
                {
                    int networkId, siteId, routerId, odfId, id, rack, sub, i;
                    Olt olt;

                    oltList = new List<Olt>(100);

                    foreach (XElement x in XDocument.Element("networkDesignDocument").Elements("network").Elements("site").Elements("router").Elements("odf").Elements("olt"))
                    {
                        olt = new Olt();
                        olt.Odf = new Odf();
                        olt.Odf.Router = new Router();
                        olt.Odf.Router.Site = new Site();

                        networkId = int.Parse(x.Parent.Parent.Parent.Parent.Attribute("id").Value);
                        siteId = int.Parse(x.Parent.Parent.Parent.Attribute("id").Value);
                        routerId = int.Parse(x.Parent.Parent.Attribute("id").Value);
                        odfId = int.Parse(x.Parent.Attribute("id").Value);
                        id = int.Parse(x.Attribute("id").Value);

                        siteId = olt.Odf.Router.Site.SiteId(networkId, siteId);
                        routerId = olt.Odf.Router.RouterId(siteId, routerId);
                        odfId = olt.Odf.OdfId(routerId, odfId);
                        olt.Id = olt.OltId(odfId, id);

                        olt.Odf = (from q in OdfList where q.Id == odfId select q).SingleOrDefault();

                        olt.Name = x.Attribute("name").Value;
                        olt.AmsName = x.Attribute("amsName").Value;

                        olt.Type = x.Attribute("type").Value;

                        Ia.Ngn.Cl.Model.Business.Nokia.Ams.RackSubFromAmsName(olt.AmsName, out rack, out sub);
                        olt.Rack = rack;
                        olt.Sub = sub;
                        
                        olt.Symbol = x.Attribute("symbol").Value;
                        olt.NetworkNumber = x.Attribute("networkNumber").Value;

                        // below: the number of possible PONs differs between Nokia OLT types and Huawei
                        if (olt.Odf.Vendor.ShortName == "No")
                        {
                            if (olt.Type == "7342") olt.NumberOfPons = 32;
                            else if (olt.Type == "7360") olt.NumberOfPons = 256;
                            else olt.NumberOfPons = 0;
                        }
                        else if (olt.Odf.Vendor.ShortName == "Hu") olt.NumberOfPons = 32;
                        else if (olt.Odf.Vendor.ShortName == "NS") olt.NumberOfPons = 56;
                        else olt.NumberOfPons = 0;

                        // below: pass ponList and change "*" to "0"
                        olt.UsedPonList = Ia.Cl.Model.Default.ConvertHyphenAndCommaSeperatedNumberStringToASortedNumberArrayList(x.Attribute("ponList").Value.Replace("*", "0"));

                        olt.MgcIp = x.Attribute("mgcIp").Value;
                        olt.MgcSecondaryIp = x.Attribute("mgcSecondaryIp").Value;

                        olt.Vlan = int.TryParse(x.Attribute("vlan").Value, out i) ? i : 0;
                        olt.GatewayIp = x.Attribute("gatewayIp").Value;

                        olt.ImsService = int.TryParse(x.Attribute("imsService").Value, out i) ? i : 0;
                        olt.ImsFsdb = Ia.Ngn.Cl.Model.Business.Nokia.Ims.ImsFsdb(olt.ImsService);

                        olt.EdgeRouter = x.Attribute("edgeRouter").Value;

                        oltList.Add(olt);
                    }
                }

                return oltList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Msan> MsanList
        {
            get
            {
                if (msanList == null || msanList.Count == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["msanList"] != null)
                    {
                        msanList = (List<Msan>)HttpContext.Current.Application["msanList"];
                    }
                    else
                    {
                        msanList = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument._MsanList;

                        if (HttpContext.Current != null) HttpContext.Current.Application["msanList"] = msanList;
                    }
                }

                return msanList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Msan> _MsanList
        {
            get
            {
                if (msanList == null || msanList.Count == 0)
                {
                    int networkId, siteId, routerId, id;
                    string vendorShortName;
                    Msan msan;

                    msanList = new List<Msan>();

                    foreach (XElement x in XDocument.Element("networkDesignDocument").Element("network").Elements("site").Elements("router").Elements("msan"))
                    {
                        msan = new Msan();
                        msan.Router = new Router();
                        msan.Router.Site = new Site();

                        networkId = int.Parse(x.Parent.Parent.Parent.Attribute("id").Value);
                        siteId = int.Parse(x.Parent.Parent.Attribute("id").Value);
                        routerId = int.Parse(x.Parent.Attribute("id").Value);
                        id = int.Parse(x.Attribute("id").Value);

                        siteId = msan.Router.Site.SiteId(networkId, siteId);
                        routerId = msan.Router.RouterId(siteId, routerId);
                        msan.Id = msan.MsanId(routerId, id);

                        msan.Name = x.Attribute("name").Value;
                        msan.Symbol = x.Attribute("symbol").Value;

                        vendorShortName = x.Attribute("vendorShortName").Value;
                        msan.Vendor = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.VendorList where q.ShortName == vendorShortName select q).SingleOrDefault();

                        msan.Router = (from q in RouterList where q.Id == routerId select q).SingleOrDefault();

                        msanList.Add(msan);
                    }
                }

                return msanList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Odf> OdfList
        {
            get
            {
                if (odfList == null || odfList.Count == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["odfList"] != null)
                    {
                        odfList = (List<Odf>)HttpContext.Current.Application["odfList"];
                    }
                    else
                    {
                        odfList = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument._OdfList;

                        if (HttpContext.Current != null) HttpContext.Current.Application["odfList"] = odfList;
                    }
                }

                return odfList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Odf> _OdfList
        {
            get
            {
                if (odfList == null || odfList.Count == 0)
                {
                    int networkId, siteId, routerId, id;
                    string vendorShortName;
                    Odf odf;

                    odfList = new List<Odf>();

                    foreach (XElement x in XDocument.Element("networkDesignDocument").Element("network").Elements("site").Elements("router").Elements("odf"))
                    {
                        odf = new Odf();
                        odf.Router = new Router();
                        odf.Router.Site = new Site();

                        networkId = int.Parse(x.Parent.Parent.Parent.Attribute("id").Value);
                        siteId = int.Parse(x.Parent.Parent.Attribute("id").Value);
                        routerId = int.Parse(x.Parent.Attribute("id").Value);
                        id = int.Parse(x.Attribute("id").Value);

                        siteId = odf.Router.Site.SiteId(networkId, siteId);
                        routerId = odf.Router.RouterId(siteId, routerId);
                        odf.Id = odf.OdfId(routerId, id);

                        odf.Name = x.Attribute("name").Value;

                        odf.PrimaryIp = x.Attribute("primaryIp").Value;
                        odf.SecondaryIp = x.Attribute("secondaryIp").Value;
                        odf.SubnetMask = x.Attribute("subnetMask").Value;

                        vendorShortName = x.Attribute("vendorShortName").Value;
                        odf.Vendor = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.VendorList where q.ShortName == vendorShortName select q).SingleOrDefault();

                        odf.Router = (from q in RouterList where q.Id == routerId select q).SingleOrDefault();

                        odfList.Add(odf);
                    }
                }

                return odfList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Oam> OamList
        {
            get
            {
                if (oamList == null || oamList.Count == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["oamList"] != null)
                    {
                        oamList = (List<Oam>)HttpContext.Current.Application["oamList"];
                    }
                    else
                    {
                        oamList = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument._OamList;

                        if (HttpContext.Current != null) HttpContext.Current.Application["oamList"] = oamList;
                    }
                }

                return oamList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Oam> _OamList
        {
            get
            {
                if (oamList == null || oamList.Count == 0)
                {
                    int networkId, siteId, routerId, id;
                    Oam oam;

                    oamList = new List<Oam>();

                    foreach (XElement x in XDocument.Element("networkDesignDocument").Element("network").Elements("site").Elements("router").Elements("oam"))
                    {
                        oam = new Oam();
                        oam.Router = new Router();
                        oam.Router.Site = new Site();

                        networkId = int.Parse(x.Parent.Parent.Parent.Attribute("id").Value);
                        siteId = int.Parse(x.Parent.Parent.Attribute("id").Value);
                        routerId = int.Parse(x.Parent.Attribute("id").Value);
                        id = int.Parse(x.Attribute("id").Value);

                        siteId = oam.Router.Site.SiteId(networkId, siteId);
                        routerId = oam.Router.RouterId(siteId, routerId);
                        oam.Id = oam.OamId(routerId, id);

                        oam.Network = x.Attribute("network").Value;
                        oam.SubnetMask = x.Attribute("subnetMask").Value;
                        oam.Gateway = x.Attribute("gateway").Value;
                        oam.Vlan = int.Parse(x.Attribute("vlan").Value);
                        oam.Vpls = int.Parse(x.Attribute("vpls").Value);
                        oam.FtpIp = x.Attribute("ftpIp").Value;
                        oam.ConfigFile = x.Attribute("configFile").Value;

                        oam.Router = (from q in RouterList where q.Id == routerId select q).SingleOrDefault();

                        oamList.Add(oam);
                    }
                }

                return oamList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Router> RouterList
        {
            get
            {
                if (routerList == null || routerList.Count == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["routerList"] != null)
                    {
                        routerList = (List<Router>)HttpContext.Current.Application["routerList"];
                    }
                    else
                    {
                        routerList = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument._RouterList;

                        if (HttpContext.Current != null) HttpContext.Current.Application["routerList"] = routerList;
                    }
                }

                return routerList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Router> _RouterList
        {
            get
            {
                if (routerList == null || routerList.Count == 0)
                {
                    int networkId, siteId, id;
                    string vendorShortName;
                    Router router;

                    routerList = new List<Router>();

                    foreach (XElement x in XDocument.Element("networkDesignDocument").Element("network").Elements("site").Elements("router"))
                    {
                        router = new Router();
                        router.Site = new Site();

                        networkId = int.Parse(x.Parent.Parent.Attribute("id").Value);
                        siteId = int.Parse(x.Parent.Attribute("id").Value);
                        id = int.Parse(x.Attribute("id").Value);

                        siteId = router.Site.SiteId(networkId, siteId);
                        router.Id = router.RouterId(siteId, id);
                        router.Name = x.Attribute("name").Value;

                        router.DomainListString = x.Attribute("domainList").Value;

                        router.DomainList = Ia.Cl.Model.Default.CommaSeperatedNumberStringToNumberList(router.DomainListString);

                        vendorShortName = x.Attribute("vendorShortName").Value;
                        router.Vendor = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.VendorList where q.ShortName == vendorShortName select q).SingleOrDefault();

                        router.Site = (from q in SiteList where q.Id == siteId select q).SingleOrDefault();

                        routerList.Add(router);
                    }
                }

                return routerList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Site> SiteList
        {
            get
            {
                if (siteList == null || siteList.Count == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["siteList"] != null)
                    {
                        siteList = (List<Site>)HttpContext.Current.Application["siteList"];
                    }
                    else
                    {
                        siteList = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument._SiteList;

                        if (HttpContext.Current != null) HttpContext.Current.Application["siteList"] = siteList;
                    }
                }

                return siteList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Site> _SiteList
        {
            get
            {
                if (siteList == null || siteList.Count == 0)
                {
                    int networkId, id;
                    Site site;

                    siteList = new List<Site>();

                    foreach (XElement x in XDocument.Element("networkDesignDocument").Element("network").Elements("site"))
                    {
                        site = new Site();
                        networkId = int.Parse(x.Parent.Attribute("id").Value);
                        id = int.Parse(x.Attribute("id").Value);

                        site.Id = site.SiteId(networkId, id);
                        site.Name = x.Attribute("name").Value;
                        site.ArabicName = x.Attribute("arabicName").Value;
                        site.AreaSymbolList = x.Attribute("areaSymbolList").Value;

                        site.Network = (from q in NetworkList where q.Id == networkId select q).SingleOrDefault();

                        siteList.Add(site);
                    }
                }

                return siteList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Network> NetworkList
        {
            get
            {
                if (networkList == null || networkList.Count == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["networkList"] != null)
                    {
                        networkList = (List<Network>)HttpContext.Current.Application["networkList"];
                    }
                    else
                    {
                        networkList = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument._NetworkList;

                        if (HttpContext.Current != null) HttpContext.Current.Application["networkList"] = networkList;
                    }
                }

                return networkList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Network> _NetworkList
        {
            get
            {
                if (networkList == null || networkList.Count == 0)
                {
                    int id;
                    Network network;

                    networkList = new List<Network>();

                    foreach (XElement x in XDocument.Element("networkDesignDocument").Elements("network"))
                    {
                        network = new Network();

                        id = int.Parse(x.Attribute("id").Value);

                        network.Id = id;
                        network.Name = x.Attribute("name").Value;

                        networkList.Add(network);
                    }
                }

                return networkList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Vendor> VendorList
        {
            get
            {
                if (vendorList == null || vendorList.Count == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["vendorList"] != null)
                    {
                        vendorList = (List<Vendor>)HttpContext.Current.Application["vendorList"];
                    }
                    else
                    {
                        vendorList = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument._VendorList;

                        if (HttpContext.Current != null) HttpContext.Current.Application["vendorList"] = vendorList;
                    }
                }

                return vendorList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Vendor> _VendorList
        {
            get
            {
                int id;
                Vendor vendor;

                if (vendorList == null || vendorList.Count == 0)
                {
                    vendorList = new List<Vendor>();

                    foreach (XElement x in XDocument.Element("networkDesignDocument").Element("vendorList").Elements("vendor"))
                    {
                        vendor = new Vendor();

                        id = int.Parse(x.Attribute("id").Value);

                        vendor.Name = x.Attribute("name").Value;
                        vendor.ShortName = x.Attribute("shortName").Value;
                        vendor.ArabicName = x.Attribute("arabicName").Value;
                        vendor.ImageUrl = x.Attribute("imageUrl").Value;

                        vendorList.Add(vendor);
                    }
                }

                return vendorList.ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string AmsNameFromOltId(int oltId)
        {
            string amsName;

            try
            {
                amsName = (from q in OltList where q.Id == oltId select q.AmsName).FirstOrDefault();
            }
            catch (Exception)
            {
                amsName = null;
            }

            return amsName;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool CheckIntegrityOfOntList()
        {
            bool isValid;
            string v;
            Hashtable ht, a, b;

            ht = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntListIdToAccessNameHashtable;
            a = new Hashtable();
            b = new Hashtable();

            isValid = true;

            foreach (string s in ht.Keys)
            {
                v = ht[s].ToString();

                if (!a.ContainsKey(v)) a[v] = 1;
                else if (a.ContainsKey(v))
                {
                    if (!v.Contains("RSL"))
                    {
                        b[v] = 1;
                        isValid = false;
                    }
                }
            }

            return isValid;
        }

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
                    streamReader = new StreamReader(_assembly.GetManifestResourceStream("Ia.Ngn.Cl.model.data.network-design-document.xml"));

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
}
