using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model.Data.Nokia
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// AGCF Gateway Records support class for Nokia data model.
    /// </summary>
    /// 
    /// <remarks> 
    /// Copyright © 2014-2015 Jasem Y. Al-Shamlan (info@ia.com.kw), Internet Applications - Kuwait. All Rights Reserved.
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
    public partial class AgcfGatewayRecord
    {
        /// <summary>
        /// 1360 COM WebAPI User Guide 255-400-419R3.X
        /// ngfs-agcfgatewayrecord-v2(rtrv,ent,ed,dlt)
        /// </summary>
        public AgcfGatewayRecord() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> ReadGwIdList()
        {
            List<int> gwIdlist;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                gwIdlist = (from q in db.AgcfGatewayRecords orderby q.GwId ascending select q.GwId).ToList();
            }

            return gwIdlist;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> ReadUnusedGwIdList()
        {
            List<int> gwIdlist, allPossibleGwIdList, unusedGwIdlist;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                gwIdlist = (from q in db.AgcfGatewayRecords orderby q.GwId ascending select q.GwId).ToList();
            }

            allPossibleGwIdList = Ia.Ngn.Cl.Model.Data.Nokia.Ims.AllPossibleGatewayIdList;

            // below: extract the GwId in allPossibleGwIdList that are not in gwIdlist
            unusedGwIdlist = allPossibleGwIdList.Except(gwIdlist).ToList();

            return unusedGwIdlist;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int GwIdFromIp(string ip)
        {
            int gwId;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                gwId = (from q in db.AgcfGatewayRecords where q.IP1 == ip select q.GwId).SingleOrDefault();
            }

            return gwId;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string IpFromGwId(int gwId)
        {
            string ip;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ip = (from q in db.AgcfGatewayRecords where q.GwId == gwId select q.IP1).SingleOrDefault();
            }

            return ip;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> List(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt)
        {
            List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> ngnOntList;
            List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList;

            // below: NGN ONT list
            ngnOntList = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where q.Pon.Olt.Id == olt.Id select q).ToList();

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below: AgcfGatewayRecord list
                agcfGatewayRecordList = (from a in db.AgcfGatewayRecords select a).ToList();
            }

            agcfGatewayRecordList = (from gr in agcfGatewayRecordList join no in ngnOntList on gr.IP1 equals no.Ip select gr).ToList();

            return agcfGatewayRecordList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<string> ReadIpList()
        {
            List<string> iplist;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                iplist = (from q in db.AgcfGatewayRecords orderby q.IP1 ascending select q.IP1).Distinct().ToList();
            }

            return iplist;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<string, int> ReadIpDictionary
        {
            get
            {
                Dictionary<string, int> ipListDictionary;

                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    ipListDictionary = (from a in db.AgcfGatewayRecords select a.IP1).ToDictionary(n => n, n => 1);
                }

                return ipListDictionary;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static SortedList AgcfGatewayRecordIpListNotInNetworkDesignDocument
        {
            get
            {
                SortedList sl;
                Dictionary<string, int> ipDictionary, nddIpDictionary;

                ipDictionary = ReadIpDictionary;
                nddIpDictionary = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntListIpDictionary;

                sl = new SortedList(ipDictionary.Count + nddIpDictionary.Count);

                foreach (KeyValuePair<string, int> kvp in ipDictionary)
                {
                    if (!nddIpDictionary.ContainsKey(kvp.Key))
                    {
                        if (!sl.ContainsKey(kvp.Key)) sl.Add(kvp.Key, 1);
                    }
                }

                return sl;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}