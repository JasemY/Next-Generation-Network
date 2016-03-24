using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model.Business.AlcatelLucent
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publich="true">
    /// Call-Engine (S12) support class for Alcatel-Lucent's Next Generation Network (NGN) business model.
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
    public class CallEngine
    {
        private bool isConnected;
        private int currentLceidId;
        private const string hostName = "*", userName = "*", password = "*", prompt = "# ";
        private const string hyconHostName = "*", hyconUserName = "*", hyconPassword = "*";
        private string[] hyconPrompt = { "USERID:", "PASSWORD:", ">", "<" };
        private ArrayList al;
        private Ia.Cl.Model.Telnet telnet;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public CallEngine()
        {
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void Connect(out string result)
        {
            int op;
            string s;
            StringBuilder sb;

            result = "";
            sb = new StringBuilder();

            try
            {
                telnet = new Ia.Cl.Model.Telnet();

                op = telnet.Connect(hostName, userName, password, prompt, out result);

                if (op > 0)
                {

                    // below: open a connection according to the constructor
                    if (telnet.IsConnected)
                    {
                        telnet.SendLine("#NGN Server Request: Call Engine Domain Session " + DateTime.UtcNow.AddHours(3).ToString("yyyy-MM-dd HH:mm"), out sb, out result);

                        try
                        {
                            telnet.SendLine("hycon " + hyconHostName, hyconPrompt, out sb, out result);

                            //telnet.SendLine("", out sb, out result);

                            s = sb.ToString();

                            if (s.Contains("USERID:"))
                            {
                                // below: if asked for user_id and password give them.
                                telnet.SendLine(hyconUserName, hyconPrompt, out sb, out result);

                                // password
                                telnet.SendLine(hyconPassword, hyconPrompt, out sb, out result);

                                isConnected = true;
                            }
                            else
                            {
                                result = "Ia.Ngn.Cl.Model(): No login prompt";

                                isConnected = false;
                            }
                        }
                        catch (Exception ex)
                        {
#if DEBUG
                            result = "Ia.Ngn.Cl.Model(): " + ex.ToString();
#else
                            result = "Ia.Ngn.Cl.Model(): " + ex.Message;
#endif
                            isConnected = false;

                            telnet.Disconnect();
                        }
                    }
                    else
                    {
                        result = "Ia.Ngn.Cl.Model(): Not connected";
                        isConnected = false;
                    }
                }
                else
                {
                    isConnected = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                isConnected = false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public int ReadAndUpdateServicesUsingLceidAndLanList(string lceidName, ArrayList lanList, out string result)
        {
            int op;
            //string lanLine;
            StringBuilder rowData;

            op = 0;
            result = "";

            op = ReadServiceUsingLceidAndLanList(lceidName, lanList, out rowData, out result);

            if (op > 0)
            {
                op = ParseAndUpdateServiceListWithinLceidFromRowData(lceidName, lanList, rowData, out result);

                if (op > 0) result = "Read: " + al[currentLceidId].ToString();
            }
            else
            {
                result = "Error: " + al[currentLceidId].ToString() + "; " + result;
            }

            currentLceidId = Ia.Cl.Model.Default.IncrementArrayListIndexOrRestart(al, currentLceidId);

            return op;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public int ReadAndUpdateAllServicesUsingLceidAndLanInSequence(out string result)
        {
            int op;
            string lceidName, lanLine;
            StringBuilder rowData;
            ArrayList lanList;

            op = 0;
            result = "";

            if (al == null)
            {
                currentLceidId = 0;

                al = Ia.Ngn.Cl.Model.Data.Service.ListOfLansFromLogicalCircuitListGroupedInLceidAndLansInTens;
            }

            lceidName = al[currentLceidId].ToString().Substring(0, 4);
            lanLine = al[currentLceidId].ToString().Substring(5);

            lanList = new ArrayList(10);

            foreach (string u in lanLine.Split(',')) lanList.Add(long.Parse(u));

            op = ReadServiceUsingLceidAndLanList(lceidName, lanList, out rowData, out result);

            if (op > 0)
            {
                op = ParseAndUpdateServiceListWithinLceidFromRowData(lceidName, lanList, rowData, out result);

                if (op > 0) result = "Read: " + al[currentLceidId].ToString();
            }
            else
            {
                result = "Error: " + al[currentLceidId].ToString() + "; " + result;
            }

            currentLceidId = Ia.Cl.Model.Default.IncrementArrayListIndexOrRestart(al, currentLceidId);

            return op;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public int ReadAndUpdateAllServicesUsingDnInSequence(out string result)
        {
            int op;
            StringBuilder rowData;
            ArrayList dnList;

            op = 0;
            result = "";

            if (al == null)
            {
                currentLceidId = 0;

                al = Ia.Ngn.Cl.Model.Data.Service.ListOfLansFromLogicalCircuitListGroupedInLceidAndLansInTens;
            }

            dnList = new ArrayList(10);

            //dnList = Ia.Cl.Model.Default.ConvertHyphenAndCommaSeperatedNumberStringToASortedNumberArrayList("22239100-22239189");
            dnList.Add((long)22239100);
            dnList.Add((long)22239101);
            dnList.Add((long)22239102);
            dnList.Add((long)22239200);
            dnList.Add((long)22239201);
            dnList.Add((long)22239202);

            op = ReadServiceUsingDnList(dnList, out rowData, out result);

            if (op > 0)
            {
                op = ParseAndUpdateServiceListFromRowData(rowData, out result);

                if (op > 0) result = "Read: " + al[currentLceidId].ToString();
            }
            else
            {
                result = "Error: " + al[currentLceidId].ToString() + "; " + result;
            }

            currentLceidId = Ia.Cl.Model.Default.IncrementArrayListIndexOrRestart(al, currentLceidId);

            return op;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private int ParseAndUpdateServiceListWithinLceidFromRowData(string lceidName, ArrayList lanList, StringBuilder rowData, out string result)
        {
            int op;
            string rowDataString;
            Dictionary<long, Ia.Ngn.Cl.Model.Service> dnServiceDictionary;

            op = 0;
            result = "";

            if (rowData.Length > 0)
            {
                try
                {
                    rowDataString = rowData.ToString();

                    ParseAndUpdateMgcS12FromRowDataAndReturnServiceList(lceidName, lanList, rowDataString, out dnServiceDictionary);

                    //Ia.Ngn.Cl.Model.Business.AlcatelLucent.Ims.UpdateServiceList(dnServiceDictionary);

                    op = 1;
                }
                catch (Exception ex)
                {
                    result = "Ia.Ngn.Cl.Model.Business.AlcatelLucent.ParseAndUpdateSubscriberListWithinLceidFromRowData(): LCEIDName: " + lceidName + ", lanList[0]:" + lanList[0].ToString() + ". " + ex.ToString();
                    op = -1;
                }
            }
            else
            {
                result = "Ia.Ngn.Cl.Model.Business.AlcatelLucent.ParseAndUpdateSubscriberListWithinLceidFromRowData(): Row data length too short. LCEIDName: " + lceidName + ", lanList[0]:" + lanList[0].ToString();
                op = -1;
            }

            return op;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private int ParseAndUpdateServiceListFromRowData(StringBuilder rowData, out string result)
        {
            int op;
            string rowDataString;
            Dictionary<long, Ia.Ngn.Cl.Model.Service> dnServiceDictionary;

            op = 0;
            result = "";

            if (rowData.Length > 0)
            {
                try
                {
                    rowDataString = rowData.ToString();

                    ParseAndUpdateMgcS12FromRowDataAndReturnServiceList(null, null, rowDataString, out dnServiceDictionary);

                    //Ia.Ngn.Cl.Model.Business.AlcatelLucent.Ims.UpdateServiceList(dnServiceDictionary);

                    op = 1;
                }
                catch (Exception ex)
                {
                    result = "Ia.Ngn.Cl.Model.ParseAndUpdateServiceListFromRowData(): " + ex.ToString();
                    op = -1;
                }
            }
            else
            {
                result = "Ia.Ngn.Cl.Model.ParseAndUpdateServiceListFromRowData():";
                op = -1;
            }

            return op;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private void ParseAndUpdateMgcS12FromRowDataAndReturnServiceList(string lceidName, ArrayList lanList, string rowDataString, out Dictionary<long, Ia.Ngn.Cl.Model.Service> dnServiceDictionary)
        {
            int i, lan; //, op;
            long dn, serviceId;
            string v; //, result;
            Ia.Ngn.Cl.Model.Service service, dataService;
            dnServiceDictionary = new Dictionary<long, Ia.Ngn.Cl.Model.Service>();

            //op = 0;
            //result = "";

            // below: read queried dns
            if (lanList != null)
            {
                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    // below: read all dns for given LCEID and LAN list
                    foreach (long l in lanList)
                    {
                        dn = (from q in db.Services where q.LCEIDName == lceidName && q.LAN == l select q.DN).SingleOrDefault();

                        dnServiceDictionary[dn] = null;
                    }
                }
            }

            // below: update
            foreach (string u in Regex.Split(rowDataString, "DISPLAY-SINGLE-SUBSCR"))
            {
                if (!u.Contains("NOT SUCCESSFUL") && u.Contains("SUCCESSFUL"))
                {
                    //try
                    //{
                        if (long.TryParse(Ia.Cl.Model.Default.Match(u, @"DN\s+=\s+(\d+)\s+"), out dn))
                        {
                            dataService = new Ia.Ngn.Cl.Model.Service();

                            dataService.DN = dn;

                            lan = int.Parse(Ia.Cl.Model.Default.Match(u, @"LAN\s+=\s+(\d{1,5})\s+"));

                            // below: calculate LCEID
                            dataService.LCEID = Ia.Cl.Model.Default.Match(u, @"LCEID\s+=\s+(\S+)\s+");
                            lceidName = dataService.LCEID.Remove(0, 2).ToLower(); // LCEID: H'BB80 to bb80

                            serviceId = Ia.Ngn.Cl.Model.Service.ServiceId(lceidName, lan);

                            dataService.LCEIDName = lceidName;

                            dataService.Id = serviceId;

                            if (int.TryParse(Ia.Cl.Model.Default.Match(u, @"AREACODE\s+=\s+(\d+)\s+"), out i)) dataService.AREACODE = i;

                            if (long.TryParse(Ia.Cl.Model.Default.Match(u, @"IDFDN\s+=\s+(\d+)\s+"), out dn)) dataService.IDFDN = dn;

                            if (long.TryParse(Ia.Cl.Model.Default.Match(u, @"REFDN\s+=\s+(\d+)\s+"), out dn)) dataService.REFDN = dn;

                            if (long.TryParse(Ia.Cl.Model.Default.Match(u, @"TAXDN\s+=\s+(\d+)\s+"), out dn)) dataService.TAXDN = dn;

                            dataService.SUBTYP = Ia.Cl.Model.Default.Match(u, @"SUBTYP\s+=\s+(\w+)\s+");

                            if (int.TryParse(Ia.Cl.Model.Default.Match(u, @"ACCNBR\s+=\s+(\d+)\s+"), out i)) dataService.ACCNBR = i;

                            dataService.ACCTYPE = Ia.Cl.Model.Default.Match(u, @"ACCTYPE\s+=\s+(\w+)\s+");
                            dataService.LINEHNT = Ia.Cl.Model.Default.Match(u, @"LINEHNT\s+:\s+(\w+)\s+");
                            dataService.TCEHNT = Ia.Cl.Model.Default.Match(u, @"TCEHNT\s+:\s+(\w+)\s+");
                            dataService.SCOPE = Ia.Cl.Model.Default.Match(u, @"SCOPE\s+=\s+(\w+)\s+");

                            dataService.RECALL = Regex.IsMatch(u, @"LINECHAR\s+=\s+PABXDEC\s+RECALL\s+");

                            dataService.NA = Ia.Cl.Model.Default.Match(u, @"NA\s+=\s+(\S+)\s+");
                            dataService.LAN = lan;
                            dataService.SUBGNBR = Ia.Cl.Model.Default.Match(u, @"SUBGNBR:\s+(\S+)\s+");

                            if (int.TryParse(Ia.Cl.Model.Default.Match(u, @"ORGCH\s+:\s+(\d+)\s+"), out i)) dataService.ORGCH = i;

                            if (int.TryParse(Ia.Cl.Model.Default.Match(u, @"ORGRTG\s+:\s+(\d+)\s+"), out i)) dataService.ORGRTG = i;

                            dataService.ACB = Ia.Cl.Model.Default.Match(u, @"ACB\s+=\s+(\w+)\s+");
                            dataService.OCBP = Ia.Cl.Model.Default.Match(u, @"OCBP\s+=\s+(\w+)\s+");

                            dataService.OCBUC_ass = Regex.IsMatch(u, @"\s+OCBUC\s+");
                            dataService.OCBUC_act = Regex.IsMatch(u, @"OCBUC\s+=\s+PROV\s+BASIC\s+BASIC\s+ACTIVE");

                            if (dataService.OCBUC_ass && dataService.OCBUC_act) dataService.OCBUC_lev = Ia.Cl.Model.Default.Match(u, @"OCBUC\s+=\s+PROV\s+BASIC\s+BASIC\s+ACTIVE\s+(\w+)\s+");
                            else dataService.OCBUC_lev = null;

                            dataService.CLIP = Regex.IsMatch(u, @"CLIP\s+=\s+PROV\s+");

                            dataService.CLIR_ass = Regex.IsMatch(u, @"CLIR\s+=\s+PROV\s+");

                            dataService.CLIR_perm = Regex.IsMatch(u, @"CLIR\s+=\s+PROV\s+PERCALL\s+");

                            dataService.CLIR_pres = Regex.IsMatch(u, @"CLIR\s+=\s+PROV\s+PERCALL\s+PRES");

                            dataService.TPS_ass = Regex.IsMatch(u, @"TPS\s+=\s+PROV\s+");

                            dataService.TPS_type = Ia.Cl.Model.Default.Match(u, @"TPS\s+=\s+PROV\s+TYPE\s+:\s+(\w+)\s+");

                            dataService.CONF = Regex.IsMatch(u, @"CONF\s+=\s+PROV\s+");

                            dataService.CW_ass = Regex.IsMatch(u, @"CW\s+=\s+PROV\s+");

                            dataService.CW_act = Regex.IsMatch(u, @"CW\s+=\s+PROV\s+ACTIVE\s+");

                            dataService.CW_notcg = Regex.IsMatch(u, @"CW\s+=\s+PROV\s+ACTIVE\s+NOTCG\s+");

                            dataService.CFU_ass = Regex.IsMatch(u, @"CFU\s+=\s+PROV\s+");

                            v = Ia.Cl.Model.Default.Match(u, @"CFU\s+=\s+ACTIVE\s+(\d+)\s+PROV\s+CG-WFTN\s+CLIP");

                            // below: ????
                            if (!dataService.CFU_ass) dataService.CFU_ass = (v != null) ? true : false;
                            dataService.CFU_act = (v != null) ? true : false;

                            if (v != null)
                            {
                                dataService.CFU_F_DN = int.Parse(v);
                                dataService.CFU_WFTN = (v != null) ? true : false;
                            }

                            dataService.PIN_ass = Regex.IsMatch(u, @"PIN\s+=\s+PROV\s+");

                            if (int.TryParse(Ia.Cl.Model.Default.Match(u, @"PIN\s+=\s+PROV\s+(\d+)\s+"), out i)) dataService.PIN_code = i;

                            dataService.ALM_ass = Regex.IsMatch(u, @"\s+ALMCALL\s+");
                            dataService.ALM_once = Regex.IsMatch(u, @"\s+ALMCALL\s+=\.+ONCEPROV\s+");
                            dataService.ALM_days = Regex.IsMatch(u, @"\s+ALMCALL\s+=\.+DAYSPROV\s+");
                            dataService.ALM_daily = Regex.IsMatch(u, @"\s+ALMCALL\s+=\.+DAILYPROV\s+");

                            // ABD assigned:
                            dataService.ABD_ass = Regex.IsMatch(u, @"\s+AN\s+ABD\-LIST\s+IS\s+EXISTING\s+");

                            #region Old Code

                            //m = Regex.Match(u, @"SUBTYP = (\w{5})");
                            //if (m.Groups[1].Captures.Count > 0)
                            //{
                            //    n1 = m.Groups[1].Captures[0].Value;

                            //    if (n1 == "ASUBS") n1 = "1";
                            //    else if (n1 == "DSUBS") n1 = "2";
                            //    else if (n1 == "NDDIPABX") n1 = "3";
                            //    else n1 = "0";

                            //    dr["subtyp"] = n1;
                            //}
                            //else dr["subtyp"] = "0";

                            //if (Regex.IsMatch(u, @"SUBGRP = (SUBGNBR): SUBG03 ORGCH : 001 ORGRTG : 001")) dr["subgrp"] = "";
                            //else dr["subgrp"] = "";

                            //if (Regex.IsMatch(u, @"OCBUC = PROV BASIC BASIC ACTIVE INT"))
                            //{
                            //    dr["ocbuc"] = true;
                            //    dr["ocbuc_active"] = true;
                            //}
                            //else
                            //{
                            //    if (Regex.IsMatch(u, @"OCBUC = PROV BASIC BASIC INACTIVE INT"))
                            //    {
                            //        dr["ocbuc"] = true;
                            //        dr["ocbuc_active"] = false;
                            //    }
                            //    else
                            //    {
                            //        dr["ocbuc"] = false;
                            //        dr["ocbuc_active"] = false;
                            //    }
                            //}

                            //n1 = Ia.Cs.Default.Match(u, @"OCBP = (\w{3})");
                            //if (n1 == "NAT") n1 = "1";
                            //else
                            //{
                            //    n1 = Ia.Cs.Default.Match(u, @"OCBP = ALLSERV (\w{3})");
                            //    if (n1 == "NAT") n1 = "1";
                            //    else n1 = "0";
                            //}
                            //dr["ocbp"] = n1;

                            ///*
                            //m = Regex.Match(u, @"OCBP     = (\w{3})");
                            //if (m.Groups[1].Captures.Count > 0)
                            //{
                            //    n1 = m.Groups[1].Captures[0].Value;

                            //    if (n1 == "NAT") n1 = "1";
                            //    else n1 = "0";

                            //    dr["ocbp"] = n1;
                            //}
                            //else dr["ocbp"] = "0";
                            //*/

                            //m = Regex.Match(u, @"CW = (\w{4}) (\w{6})");
                            //if (m.Groups[1].Captures.Count > 0)
                            //{
                            //    n1 = m.Groups[1].Captures[0].Value;
                            //    n2 = m.Groups[2].Captures[0].Value;

                            //    if (n1 == "PROV") dr["cw"] = true;
                            //    else dr["cw"] = false;

                            //    if (n2 == "ACTIVE") dr["cw_active"] = true;
                            //    else dr["cw_active"] = false;
                            //}
                            //else
                            //{
                            //    m = Regex.Match(u, @"CW = (\w{4})");
                            //    if (m.Groups[1].Captures.Count > 0)
                            //    {
                            //        n1 = m.Groups[1].Captures[0].Value;

                            //        if (n1 == "PROV") b = true;
                            //        else b = false;

                            //        dr["cw"] = b;
                            //    }
                            //    else
                            //    {
                            //        dr["cw"] = false;
                            //        dr["cw_active"] = false;
                            //    }
                            //}

                            //m = Regex.Match(u, @"CLIP = (\w{4})");
                            //if (m.Groups[1].Captures.Count > 0)
                            //{
                            //    n1 = m.Groups[1].Captures[0].Value;

                            //    if (n1 == "PROV") b = true;
                            //    else b = false;

                            //    dr["clip"] = b;
                            //}
                            //else dr["clip"] = false;

                            //m = Regex.Match(u, @"CFU = (\w{6}) (\d{5,8}) (\w{4}) CG-WFTN CLIP");
                            //if (m.Groups[1].Captures.Count > 0)
                            //{
                            //    n1 = m.Groups[1].Captures[0].Value;
                            //    n2 = m.Groups[2].Captures[0].Value;
                            //    n3 = m.Groups[3].Captures[0].Value;

                            //    if (n1 == "ACTIVE") dr["cfu_active"] = true;
                            //    else dr["cfu_active"] = false;

                            //    dr["cfu_dn"] = n2;

                            //    if (n3 == "PROV") dr["cfu"] = true;
                            //    else dr["cfu"] = false;
                            //}
                            //else
                            //{
                            //    m = Regex.Match(u, @"CFU = (\w{4}) CG-WFTN CLIP");
                            //    if (m.Groups[1].Captures.Count > 0)
                            //    {
                            //        n1 = m.Groups[1].Captures[0].Value;

                            //        if (n1 == "PROV") dr["cfu"] = true;
                            //        else dr["cfu"] = false;
                            //    }
                            //    else
                            //    {
                            //        m = Regex.Match(u, @"CFU = (\w{4})");
                            //        if (m.Groups[1].Captures.Count > 0)
                            //        {
                            //            n1 = m.Groups[1].Captures[0].Value;

                            //            if (n1 == "PROV") dr["cfu"] = true;
                            //            else dr["cfu"] = false;
                            //        }
                            //        else
                            //        {
                            //            dr["cfu_active"] = false;
                            //            dr["cfu_dn"] = "";
                            //            dr["cfu"] = false;
                            //        }
                            //    }
                            //}

                            //m = Regex.Match(u, @"TPS = (\w{4}) TYPE : CONF3");
                            //if (m.Groups[1].Captures.Count > 0)
                            //{
                            //    n1 = m.Groups[1].Captures[0].Value;

                            //    if (n1 == "PROV") dr["tps"] = true;
                            //    else dr["tps"] = false;
                            //}
                            //else dr["tps"] = false;

                            //m = Regex.Match(u, @"CONF = (\w{4})");
                            //if (m.Groups[1].Captures.Count > 0)
                            //{
                            //    n1 = m.Groups[1].Captures[0].Value;

                            //    if (n1 == "PROV") dr["conf"] = "1";
                            //    else dr["conf"] = "0";
                            //}
                            //else dr["conf"] = "0";

                            //m = Regex.Match(u, @"PIN = (\w{4}) (\d{4}) PINCAT : 1");
                            //if (m.Groups[1].Captures.Count > 0)
                            //{
                            //    n1 = m.Groups[1].Captures[0].Value;
                            //    n2 = m.Groups[2].Captures[0].Value;

                            //    dr["pin"] = n2;
                            //}
                            //else
                            //{
                            //    if (Regex.IsMatch(u, @"PIN = PINCAT : 1")) dr["pin"] = "";
                            //    else dr["pin"] = "";
                            //}

                            //m = Regex.Match(u, @"ALMCALL = (\w{8})");
                            //if (m.Groups[1].Captures.Count > 0)
                            //{
                            //    n1 = m.Groups[1].Captures[0].Value;

                            //    if (n1 == "ONCEPROV") dr["almcall"] = true;
                            //    else dr["almcall"] = false;
                            //}
                            //else dr["almcall"] = false;

                            //m = Regex.Match(u, @"ACB = (\w{8})");
                            //if (m.Groups[1].Captures.Count > 0)
                            //{
                            //    n1 = m.Groups[1].Captures[0].Value;

                            //    if (n1 == "FULLBADP") dr["acb"] = true;
                            //    else dr["acb"] = false;
                            //}
                            //else dr["acb"] = false;

                            #endregion

                            using (var db = new Ia.Ngn.Cl.Model.Ngn())
                            {
                                service = (from q in db.Services where q.Id == dataService.Id select q).SingleOrDefault();

                                if (service == null)
                                {
                                    dataService.Created = dataService.Updated = dataService.Viewed = DateTime.UtcNow.AddHours(3);

                                    db.Services.Add(dataService);

                                    dnServiceDictionary[dataService.DN] = dataService;
                                }
                                else
                                {
                                    if (service.Update(dataService))
                                    {
                                        db.Services.Attach(service);
                                        db.Entry(service).State = System.Data.Entity.EntityState.Modified;
                                    }

                                    dnServiceDictionary[service.DN] = service;
                                }

                                db.SaveChanges();
                            }
                        }
                        else
                        {
                        }
                    //}
                    //catch (Exception ex)
                    //{
                    //    result = "Ia.Ngn.Cl.Model.Business.AlcatelLucent.CallEngine.ParseAndUpdateMgcS12FromRowDataAndReturnServiceList(): LCEIDName: " + lceidName + ", lanList[0]:" + lanList[0].ToString() + ". " + ex.ToString();
                    //    op = -1;
                    //}
                }
                else
                {
                }
            }

            // below: delete entries not processed
            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                foreach(KeyValuePair<long, Ia.Ngn.Cl.Model.Service> s in dnServiceDictionary)
                {
                    if (s.Value == null)
                    {
                        service = (from q in db.Services where q.DN == s.Key select q).SingleOrDefault();

                        if (service != null) db.Services.Remove(service);
                    }
                }

                db.SaveChanges();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public int ReadServiceUsingDnList(ArrayList al, out StringBuilder data, out string result)
        {
            int op;

            op = SendMMCommand(al, "5291", "DN", "K'", "", out data, out result);

            return op;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public int ReadServiceUsingLceidAndLanList(string lceidName, ArrayList lanList, out StringBuilder data, out string result)
        {
            int op;

            op = SendMMCommand(lanList, "5291", "LAN", "", "LCEID=H'" + lceidName.ToUpper(), out data, out result);

            return op;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public int UpdateServiceUsingDnList(ArrayList dnList, string prop, out StringBuilder data, out string result)
        {
            int op;

            op = SendMMCommand(dnList, "5290", "DN", "K'", prop, out data, out result);

            return op;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public int CreateServiceUsingDnList(ArrayList dnList, string prop, out StringBuilder data, out string result)
        {
            int op;

            if (prop.Length > 0) prop += ",";

            //prop += "SUBGRP=\"SUBG03\",SUBTYP=ASUBS";
            prop += "CTRLOPT=IMMREUSE&TAXDN,SUBGRP=\"SUBG03\",SUBTYP=ASUBS";

            op = SendMMCommand(dnList, "5289", "DN", "K'", prop, out data, out result);

            return op;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public int DeleteServiceUsingDnList(ArrayList dnList, out StringBuilder data, out string result)
        {
            int op;

            op = SendMMCommand(dnList, "5292", "DN", "K'", "", out data, out result);

            return op;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Send MM (Man-Machine) command to the Call Engine and return output in data
        /// </summary>
        private int SendMMCommand(ArrayList list, string opcode, string operand, string operandPrefix, string parameter, out StringBuilder data, out string result)
        {
            int n, op, line_length_max;
            string c;
            StringBuilder sb;
            ArrayList c_al;

            op = 0;
            result = c = "";
            data = new StringBuilder(1000);
            data.Length = 0;
            c_al = new ArrayList(list.Count / 4);
            line_length_max = 80; // maximum length of any command line sent to the MM domain

            // below: build command to send to MGC-CED-MM domain. This will send upto 8 numbers in one command, but will be able to send multiple commands.

            if (telnet.IsConnected)
            {
                if (list.Count > 0 && list.Count <= 100)
                {
                    n = 0;

                    foreach (long l in list)
                    {
                        // below: we will send the command in two lines.
                        if (n % 8 == 0)
                        {
                            if (n != 0)
                            {
                                c = c.Remove(c.Length - 1, 1); // remove last "&"
                                if (parameter.Length > 0) c += "," + parameter + ".";
                                else c += ".";

                                c_al.Add(c);
                            }

                            c = opcode + ":" + operand + "=" + operandPrefix + l + "&";
                        }
                        else if (n % 4 == 0)
                        {
                            c_al.Add(c);
                            c = operandPrefix + l + "&";
                        }
                        else c += operandPrefix + l + "&";

                        n++;
                    }

                    c = c.Remove(c.Length - 1, 1); // remove last "&"
                    if (parameter != "") c += "," + parameter + ".";
                    else c += ".";
                    c_al.Add(c);

                    n = 0;
                    foreach (string u in c_al)
                    {
                        try
                        {
                            if (n % 2 == 0)
                            {
                                // MM command
                                telnet.SendLine("MM", hyconPrompt, out sb, out result);
                                data.Append(sb.ToString());

                                // below: if asked for user_id and password give them.
                                if (data.ToString().Contains("USERID:"))
                                {
                                    // below: if asked for user_id and password give them.
                                    telnet.SendLine(hyconUserName, hyconPrompt, out sb, out result);
                                    data.Append(sb.ToString());

                                    // password
                                    telnet.SendLine(hyconPassword, hyconPrompt, out sb, out result);
                                    data.Append(sb.ToString());
                                }
                                else { }
                            }

                            n++;

                            // below: check length of command before sending
                            if (u.Length > line_length_max * 2)
                            {
                                telnet.SendLine(u.Substring(0, 80), hyconPrompt, out sb, out result);
                                data.Append(sb.ToString());

                                telnet.SendLine(u.Substring(80, 80), hyconPrompt, out sb, out result);
                                data.Append(sb.ToString());

                                telnet.SendLine(u.Substring(160), hyconPrompt, out sb, out result);
                                data.Append(sb.ToString());
                            }
                            else if (u.Length > line_length_max)
                            {
                                telnet.SendLine(u.Substring(0, 80), hyconPrompt, out sb, out result);
                                data.Append(sb.ToString());

                                telnet.SendLine(u.Substring(80), hyconPrompt, out sb, out result);
                                data.Append(sb.ToString());
                            }
                            else
                            {
                                telnet.SendLine(u, hyconPrompt, out sb, out result);
                                data.Append(sb.ToString());
                            }

                            op = 1;
                        }
                        catch (Exception ex)
                        {
                            data.Length = 0;
#if DEBUG
                            result = "Ia.Ngn.Cl.Model.CallEngine.SendMMCommand(): " + ex.ToString();
#else
                            result = "Ia.Ngn.Cs.Telnet.SendMMCommand(): " + ex.Message;
#endif
                            op = -1;
                        }
                    }
                }
                else
                {
                    result = "Ia.Ngn.Cl.Model.CallEngine.SendMMCommand(): Range invalid";
                    op = -1;
                }
            }
            else
            {
                result = "Ia.Ngn.Cl.Model.CallEngine.SendMMCommand(): Not connected";
                op = -1;
            }

            return op;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public void Disconnect()
        {
            // below: properly exit
            string result;
            StringBuilder sb;

            if (telnet.IsConnected)
            {
                // below:
                telnet.SendLine("DACALL", hyconPrompt, out sb, out result);
                telnet.SendLine("QUIT", hyconPrompt, out sb, out result);

                telnet.SendLine("exit" + "\r\n");
                telnet.Disconnect();
            }

            isConnected = false;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            Disconnect();

            telnet.Dispose();
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}