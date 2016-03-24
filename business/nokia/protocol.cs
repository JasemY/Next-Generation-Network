using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Ia.Ngn.Cl.Model.Business.AlcatelLucent
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publich="true">
    /// Protocol domain support class for Alcatel-Lucent's Next Generation Network (NGN) business model.
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
    public class Protocol
    {
        private int currentLceidId;
        private Ia.Cl.Model.Telnet telnet;

        // below: access data for AMS
        private const int port = 0;
        private const string host = "*";
        private const string userName = "*";
        private const string password = "*";
        private const string prompt = "*";
        private const string textToSkipInTelnetEndReceive = "IP 0\r\n<";

        /// <summary/>
        public static string Host { get { return host; } }
        /// <summary/>
        public static int Port { get { return port; } }
        /// <summary/>
        public static string UserName { get { return userName; } }
        /// <summary/>
        public static string Password { get { return password; } }
        /// <summary/>
        public static string Prompt { get { return prompt; } }
        /// <summary/>
        public static string TextToSkipInTelnetEndReceive { get { return textToSkipInTelnetEndReceive; } }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Protocol()
        {
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void Connect(out string result)
        {
            StringBuilder sb;

            result = "";
            sb = new StringBuilder();

            try
            {
                telnet = new Ia.Cl.Model.Telnet();

                telnet.Connect(host, userName, password, prompt, out result);

                telnet.SendLine("#NGN Server Request: Protocol Domain Session " + DateTime.UtcNow.AddHours(3).ToString("yyyy-MM-dd HH:mm"), out sb, out result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);

                result = "Exception: " + e.Message;
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
                return (telnet == null) ? false : telnet.IsConnected;
            }
        }

        /*
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public int ReadAndUpdateAllLogicalCircuitsWithinASequencialLceidAndLanRange(out string result)
        {
            int op, startLan, endLan;
            string lceidName;

            op = 0;

            lceidName = Ia.Ngn.Cl.Model.Data.Service.LceidName[currentLceidId].ToString();

            Ia.Ngn.Cl.Model.Data.Service.LansInLceid(lceidName, out startLan, out endLan);

            op = ReadAndUpdateAllLogicalCircuitsWithinLceidAndLanRange(lceidName, startLan, endLan, out result);

            if (currentLceidId < Ia.Ngn.Cl.Model.Data.Service.LceidName.Count - 1) currentLceidId++;
            else currentLceidId = 0;

            return op;
        }
         */ 

        /*
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public int ReadAndUpdateAllLogicalCircuitsWithinLceidAndLanRange(string lceidName, int startLan, int endLan, out string result)
        {
            int op;
            StringBuilder rowData;

            op = 0;
            result = "";

            op = ReadLogicalCircuitListWithinLceidInRowData(startLan, endLan, lceidName, out rowData, out result);

            if (op > 0) op = ParseAndUpdateLogicalCircuitListWithinLceidFromRowData(startLan, endLan, lceidName, rowData, out result);
            else
            {
                result = "Error: " + lceidName + " " + result;
            }

            return op;
        }
         */ 

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private int ReadLogicalCircuitListWithinLceidInRowData(int start, int end, string lceidName, out StringBuilder data, out string result)
        {
            int op, i;
            string c;

            op = 0;
            result = c = "";
            data = new StringBuilder(1000);
            data.Length = 0;

            if (telnet.IsConnected)
            {
                // below: convert numbers in al to a format suitable for SQL WHERE statement. Note that in the UNIX script ngn_gu, lan is defined as "a.LOGICALCKTID-d.LOGCKTTBLINDEX"
                c = "(a.LOGICALCKTID-d.LOGCKTTBLINDEX >= " + start.ToString() + " AND a.LOGICALCKTID-d.LOGCKTTBLINDEX <= " + end.ToString() + ")";

                // below: make sure that ngn_gu is under "usr/local/sbin" and it has permission "chmod 755"

                // below: IMPORTANT if the lan > 8192 then we will take its lceid value and subtract 10 hex (or 16 dec) from it to go back to the lower lceid
                i = Ia.Cl.Model.Default.HexToDec(lceidName);
                if (start > 8192) i -= 16;

                try
                {
                    telnet.SendLine("/usr/local/sbin/ngn_gu \"" + c + "\" " + i, out data, out result);

                    op = 1;
                }
                catch (Exception ex)
                {
                    data.Length = 0;
#if DEBUG
                    result = "Ia.Ngn.Cl.Model.Protocol.Read(): " + ex.ToString();
#else
                    result = "Ia.Ngn.Cl.Model.Protocol.Read(): " + ex.Message;
#endif
                    op = -1;
                }
            }
            else
            {
                result = "Ia.Ngn.Cl.Model.Protocol.Read(): Not connected";

                op = -1;
            }

            return op;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList LogicalCircuitCommandsToReadProtocolLceidWithinLanRangeDataArrayList()
        {
            string c, command;
            ArrayList al;
            List<Ia.Ngn.Cl.Model.Business.Service.LceidLanRange> lceidLanRangeList;

            lceidLanRangeList = Ia.Ngn.Cl.Model.Data.Service.LceidLanRangesGroupedIn128List;

            al = new ArrayList(lceidLanRangeList.Count);

            foreach (Ia.Ngn.Cl.Model.Business.Service.LceidLanRange lceidLanRange in lceidLanRangeList)
            {
                // below: convert numbers in al to a format suitable for SQL WHERE statement. Note that in the UNIX script ngn_gu, lan is defined as "a.LOGICALCKTID-d.LOGCKTTBLINDEX"
                c = "(a.LOGICALCKTID-d.LOGCKTTBLINDEX >= " + lceidLanRange.StartLan + " AND a.LOGICALCKTID-d.LOGCKTTBLINDEX <= " + lceidLanRange.EndLan + ")";

                // below: make sure that ngn_gu is under "usr/local/sbin" and it has permission "chmod 755"

                // below: IMPORTANT if the lan > 8192 then we will take its lceid value and subtract 10 hex (or 16 dec) from it to go back to the lower lceid
                //i = Ia.Cl.Model.Default.HexToDec(lceidName);
                //if (startLan > 8192) i -= 16;

                command = "/usr/local/sbin/ngn_gu \"" + c + "\" " + lceidLanRange.Lceid.LogicalCircuitNumber + "\n";

                al.Add(command);
            }

            return al;
        }

        /*
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private int ParseAndUpdateLogicalCircuitListWithinLceidFromRowData(int start, int end, string lceidName, StringBuilder rowData, out string result)
        {
            int op, count_in, count, logicalCircuitId, tn, lceid;
            string s;
            MatchCollection mc;
            LogicalCircuit logicalCircuit, newLogicalCircuit;
            List<LogicalCircuit> logicalCircuitList;

            op = count_in = count = 0;
            result = "";

            if (rowData.Length > 0)
            {
                s = Regex.Replace(rowData.ToString(), @"\r\n", "\n");

                try
                {
                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        logicalCircuitList = Ia.Ngn.Cl.Model.LogicalCircuit.ReadList(lceidName);

                        foreach (Match m in Regex.Matches(s, @"(.*)\n")) // match all lines
                        {
                            count_in++;

                            // LCKTID     GWY_INDEX   TERMID_INDEX GWY_NAME      TERM_STRING      LCEID      TN
                            // 1          177         142          10.3.144.44   td1              48000      1
                            // 1          2           3            4             5                6          7

                            mc = Regex.Matches(m.Groups[1].Captures[0].Value, @"^\s+(\d{1,6})\s+(\d{1,6})\s+(\d{1,6})\s+(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\s+\w\w(\d{1,2})\s+(.*?)\s+(.*?)$", RegexOptions.Singleline);
                            //                                                      1           2           3           4                                          5           6       7

                            if (mc.Count == 1)
                            {
                                count++;

                                tn = int.Parse(mc[0].Groups[7].Captures[0].Value);
                                logicalCircuitId = Ia.Ngn.Cl.Model.LogicalCircuit.LogicalCircuitId(lceidName, tn);

                                lceid = int.Parse(mc[0].Groups[6].Captures[0].Value.ToString());

                                newLogicalCircuit = new LogicalCircuit()
                                {
                                    Id = logicalCircuitId,
                                    GatewayIndex = int.Parse(mc[0].Groups[2].Captures[0].Value.ToString()),
                                    TerminationIdIndex = int.Parse(mc[0].Groups[3].Captures[0].Value.ToString()),
                                    GatewayName = mc[0].Groups[4].Captures[0].Value.ToString(),
                                    TerminationString = mc[0].Groups[5].Captures[0].Value.ToString(),
                                    Lceid = lceid,
                                    LceidName = (from q in Ia.Ngn.Cl.Model.Data.Service.LceidList where q.LogicalCircuitNumber == lceid && tn >= q.StartLan && tn <= q.EndLan select q.Name).SingleOrDefault(),
                                    Tn = tn
                                };

                                if (logicalCircuitList.Any(q => q.Id == logicalCircuitId))
                                {
                                    // below: object key exists. Now check for updates:

                                    logicalCircuit = logicalCircuitList.SingleOrDefault(q => q.Id == newLogicalCircuit.Id);

                                    if (newLogicalCircuit.GatewayIndex != logicalCircuit.GatewayIndex
                                        || newLogicalCircuit.GatewayName != logicalCircuit.GatewayName
                                        || newLogicalCircuit.Lceid != logicalCircuit.Lceid
                                        || newLogicalCircuit.TerminationIdIndex != logicalCircuit.TerminationIdIndex
                                        || newLogicalCircuit.TerminationString != logicalCircuit.TerminationString
                                        || newLogicalCircuit.Tn != logicalCircuit.Tn)
                                    {
                                        newLogicalCircuit.Created = logicalCircuit.Created;
                                        newLogicalCircuit.Updated = DateTime.UtcNow.AddHours(3);

                                        db.LogicalCircuits.Attach(newLogicalCircuit);

                                        var v = db.Entry(newLogicalCircuit);
                                        v.State = System.Data.Entity.EntityState.Modified;
                                    }

                                    // below: remove the item
                                    logicalCircuitList.Remove(logicalCircuit);
                                }
                                else
                                {
                                    newLogicalCircuit.Created = newLogicalCircuit.Updated = DateTime.UtcNow.AddHours(3);

                                    db.LogicalCircuits.Add(newLogicalCircuit);
                                }

                                op = 1;
                            }
                            else
                            {
                            }
                        }

                        // below: this function will remove values that were not present in the reading

                        if (logicalCircuitList.Count() > 0)
                        {
                            foreach (LogicalCircuit lc in logicalCircuitList)
                            {
                                var v = (from q in db.LogicalCircuits where q.Id == lc.Id select q).FirstOrDefault();

                                db.LogicalCircuits.Remove(v);
                            }
                        }

                        db.SaveChanges();

                        result = lceidName + ":" + start + "-" + end + ".";
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    result = "Ia.Ngn.Cl.Model.ParseLogicalCircuitListFromRowData(): " + ex.ToString();
#else
                    result = "Ia.Ngn.Cl.Model.ParseLogicalCircuitListFromRowData(): " + ex.Message;
#endif
                    op = -1;
                }
            }
            else
            {
                result = "Ia.Ngn.Cl.Model.ParseLogicalCircuitListFromRowData(): Data length too short";
                op = -1;
            }

            return op;
        }
        */

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateLogicalCircuitWithCommandOutput(string rowData)
        {
            bool b;
            int op, count_in, count, logicalCircuitId, lceid, tn, startTn, endTn;
            ArrayList newLogicalCircuitIdArryList;
            Match commandMatch;
            MatchCollection matchCollection;
            LogicalCircuit logicalCircuit, newLogicalCircuit;
            List<LogicalCircuit> logicalCircuitList;

            b = false;
            op = count_in = count = 0;

            /*
            rowData = @"/usr/local/sbin/ngn_gu ""(a.LOGICALCKTID-d.LOGCKTTBLINDEX >= 1036 
9 AND a.LOGICALCKTID-d.LOGCKTTBLINDEX <= 10496)"" 48000
 
****************************** UNIX data reports *************************
   LCKTID GWY_INDEX TERMID_INDEX GWY_NAME      TERM_STRING              LCEID                              TN
";
             */ 

            if (rowData.Length > 0)
            {
                rowData = Regex.Replace(rowData, @"\r\n", "\n");
                rowData = Regex.Replace(rowData, @" \r", ""); // very important because when start lan is, say "10369" it is echoed back "1036 \r9"

                #region Hohoho

                // below: extract lceid, start and end lan from command
                commandMatch = Regex.Match(rowData, @"""\(a.LOGICALCKTID-d.LOGCKTTBLINDEX >= (\d+) .+? a.LOGICALCKTID-d.LOGCKTTBLINDEX <= (\d+)\)"" (\d+)\n", RegexOptions.Singleline);

                if (commandMatch.Success)
                {
                    lceid = int.Parse(commandMatch.Groups[3].Value);
                    startTn = int.Parse(commandMatch.Groups[1].Value);
                    endTn = int.Parse(commandMatch.Groups[2].Value);

                    logicalCircuitList = Ia.Ngn.Cl.Model.LogicalCircuit.ReadList(lceid, startTn, endTn);

                    //amsName = match.Groups[1].Value;

                    // LCKTID     GWY_INDEX   TERMID_INDEX GWY_NAME      TERM_STRING      LCEID      TN
                    // 1          177         142          10.3.144.44   td1              48000      1
                    // 1          2           3            4             5                6          7

                    matchCollection = Regex.Matches(rowData, @"\s+(\d{1,6})\s+(\d{1,6})\s+(\d{1,6})\s+(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\s+(td\d{1,2})\s+(\d{5}?)\s+(\d+)", RegexOptions.Singleline);

                    newLogicalCircuitIdArryList = new ArrayList(matchCollection.Count);

                    if (matchCollection.Count > 0)
                    {
                        using (var db = new Ia.Ngn.Cl.Model.Ngn())
                        {
                            foreach (Match match in matchCollection)
                            {
                                if (match.Success)
                                {
                                    count++;

                                    lceid = int.Parse(match.Groups[6].Value);
                                    tn = int.Parse(match.Groups[7].Value);

                                    logicalCircuitId = Ia.Ngn.Cl.Model.LogicalCircuit.LogicalCircuitId(lceid, tn);

                                    newLogicalCircuitIdArryList.Add(logicalCircuitId);

                                    newLogicalCircuit = new Ia.Ngn.Cl.Model.LogicalCircuit();

                                    newLogicalCircuit.Id = logicalCircuitId;
                                    newLogicalCircuit.Lceid = lceid;
                                    newLogicalCircuit.LceidName = (from q in Ia.Ngn.Cl.Model.Data.Service.LceidList where q.LogicalCircuitNumber == lceid && tn >= q.StartLan && tn <= q.EndLan select q.Name).SingleOrDefault();
                                    newLogicalCircuit.GatewayIndex = int.Parse(match.Groups[2].Value);
                                    newLogicalCircuit.GatewayName = match.Groups[4].Value;
                                    newLogicalCircuit.TerminationIdIndex = int.Parse(match.Groups[3].Value);
                                    newLogicalCircuit.TerminationString = match.Groups[5].Value;
                                    newLogicalCircuit.Tn = tn;

                                    logicalCircuit = (from q in logicalCircuitList where q.Id == newLogicalCircuit.Id select q).SingleOrDefault();

                                    if (logicalCircuit == null)
                                    {
                                        newLogicalCircuit.Created = newLogicalCircuit.Updated = DateTime.UtcNow.AddHours(3);

                                        db.LogicalCircuits.Add(newLogicalCircuit);
                                    }
                                    else
                                    {
                                        // below: copy values from newLogicalCircuit to logicalCircuit

                                        if (logicalCircuit.Update(newLogicalCircuit))
                                        {
                                            db.LogicalCircuits.Attach(logicalCircuit);
                                            db.Entry(logicalCircuit).State = System.Data.Entity.EntityState.Modified;
                                        }
                                    }

                                    b = true;
                                }
                                else
                                {
                                }
                            }

                            // below: this function will remove values that were not present in the reading
                            if (logicalCircuitList.Count > 0)
                            {
                                foreach (LogicalCircuit lc in logicalCircuitList)
                                {
                                    if (!newLogicalCircuitIdArryList.Contains(lc.Id))
                                    {
                                        logicalCircuit = (from q in db.LogicalCircuits where q.Id == lc.Id select q).SingleOrDefault();

                                        db.LogicalCircuits.Remove(logicalCircuit);
                                    }
                                }
                            }

                            db.SaveChanges();
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                }

                #endregion
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public void Disconnect()
        {
            // below: properly exit

            if (telnet.IsConnected) telnet.Disconnect();
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