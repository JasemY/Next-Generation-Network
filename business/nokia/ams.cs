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
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model.Business.Nokia
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Access Management System (AMS) support class for Nokia's Next Generation Network (NGN) business model.
    /// </summary>
    /// 
    /// <value>
    ///   <appSettings>
    ///       <add key="amsServerHost" value="*" />
    ///       <add key="amsServerPort" value="*" />
    ///       <add key="amsServerActUser" value="ACT-USER:{amsName}:*:::*;" />
    ///       <add key="amsServerCancUser" value="CANC-USER:{amsName}:*:;" />
    ///   </appSettings>
    /// </value>
    /// 
    /// <remarks> 
    /// Copyright © 2007-2015 Jasem Y. Al-Shamlan (info@ia.com.kw), Internet Applications - Kuwait. All Rights Reserved.
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
    public partial class Ams
    {
        // below: access data for AMS
        private const string textToSkipInTelnetEndReceive = "IP 0\r\n<";

        private static int amsCommandArrayListIndex, amsUpdateCommandArrayListIndex, amsUpdatePotsCommandArrayListIndex;
        private static long amsCommandOltId;
        private static ArrayList amsCommandArrayList, amsUpdateCommandArrayList, amsUpdatePotsCommandArrayList;

        /// <summary/>
        public enum AmsOpcode
        {
            RtrvHdr, RtrvOnt, RtrvServiceHsi, RtrvServiceVoip, RtrvOntPots, EdServiceHsi, RtrvAlmPon, EdOntDesc1, EdOntPotsCustinfo
        }

        /// <summary/>
        public enum BellcoreState { Undefined = 0, IsNr = 1, OosAu, OosMa, OosAuma };

        /// <summary/>
        public static string Host { get { return ConfigurationManager.AppSettings["amsServerHost"].ToString(); } }
        /// <summary/>
        public static int Port { get { return int.Parse(ConfigurationManager.AppSettings["amsServerPort"].ToString()); } }

        /// <summary/>
        public static string ActUser(string amsName)
        {
            return ConfigurationManager.AppSettings["amsServerActUser"].ToString().Replace("{amsName}", amsName);
        }
        /// <summary/>
        public static string CancUser(string amsName)
        {
            return ConfigurationManager.AppSettings["amsServerCancUser"].ToString().Replace("{amsName}", amsName);
        }

        /// <summary/>
        public static string TextToSkipInTelnetEndReceive { get { return textToSkipInTelnetEndReceive; } }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Ams() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string ProperlySelectedSingleAmsCommandToManageOntVoipPotsHsiNetworkElements()
        {
            return ProperlySelectedSingleAmsCommandToManageOntVoipPotsHsiNetworkElements(0);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string AmsCommandOfARandomAmsKeepAlive()
        {
            string command, amsName;

            amsName = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList where q.Odf.Vendor.ShortName == "No" select q.AmsName).ToList().PickRandom();

            command = FormatAmsCommand(AmsOpcode.RtrvHdr, amsName);

            return command;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string ProperlySelectedSingleAmsCommandToManageOntVoipPotsHsiNetworkElements(int oltId)
        {
            string amsCommand;
            DateTime now;

            now = DateTime.UtcNow.AddHours(3);
            amsCommand = null;// +now.ToString("yyyy-MM-dd HH:mm:ss") + ";";

            /*
            else if (now.Second % 58 == 0)
            {
                if (amsElementProperStateCommandArrayList == null || amsElementProperStateCommandArrayList.Count == 0 || amsElementProperStateCommandArrayListIndex >= (amsElementProperStateCommandArrayList.Count - 1))
                {
                    amsElementProperStateCommandArrayListIndex = 0;

                    amsElementProperStateCommandOltId = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList select q.Id).ToList().PickRandom();

                    amsElementProperStateCommandArrayList = AmsCommandsToEditOntEntriesInAmsToAddProperDesc1ValueAsPositionOntIdToAllThatAreMissingTheCorrectValue();
                }

                if (amsElementProperStateCommandArrayList.Count > 0)
                {
                    amsCommand = amsElementProperStateCommandArrayList[amsElementProperStateCommandArrayListIndex++].ToString();
                }
            }
             */
            if (now.Second % 9 == 0)
            {
                if (amsUpdatePotsCommandArrayList == null || amsUpdatePotsCommandArrayList.Count == 0 || amsUpdatePotsCommandArrayListIndex >= (amsUpdatePotsCommandArrayList.Count - 1))
                {
                    amsUpdatePotsCommandArrayListIndex = 0;

                    amsUpdatePotsCommandArrayList = AmsCommandsToUpdateOntOntPotsCustomerWithItsConnectedServiceNumberArrayList();
                }

                if (amsUpdatePotsCommandArrayList.Count > 0) amsCommand = amsUpdatePotsCommandArrayList[amsUpdatePotsCommandArrayListIndex++].ToString();
            }
            else if (now.Second % 51 == 0)
            {
                if (amsUpdateCommandArrayList == null || amsUpdateCommandArrayList.Count == 0 || amsUpdateCommandArrayListIndex >= (amsUpdateCommandArrayList.Count - 1))
                {
                    amsUpdateCommandArrayListIndex = 0;

                    amsUpdateCommandArrayList = AmsCommandsToUpdateAndDisplayOntDescriptionWithItsAccessNameArrayList();
                }

                if (amsUpdateCommandArrayList.Count > 0) amsCommand = amsUpdateCommandArrayList[amsUpdateCommandArrayListIndex++].ToString();
            }
            else if (now.Second % 50 == 0)
            {
                amsCommand = AmsCommandOfARandomAmsKeepAlive();
            }
            else
            {
                if (amsCommandArrayList == null || amsCommandArrayList.Count == 0 || amsCommandArrayListIndex >= (amsCommandArrayList.Count - 1))
                {
                    amsCommandArrayListIndex = 0;

                    if (oltId != 0) amsCommandOltId = oltId;
                    else amsCommandOltId = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList where q.Odf.Vendor.ShortName == "No" select q.Id).ToList().PickRandom();

                    amsCommandArrayList = AmsCommandsToDisplayOntVoipPotsHsiForOntsWithDefinedFamilyTypeAndForOtherOntsDefinedInNddDocumentArrayList(amsCommandOltId);
                }

                if (amsCommandArrayList.Count > 0) amsCommand = amsCommandArrayList[amsCommandArrayListIndex++].ToString();
            }

            if (amsCommand == null) amsCommand = AmsCommandOfARandomAmsKeepAlive();

            return amsCommand;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static ArrayList FormatAmsRtrvCommand(AmsOpcode amsOpcode, string position)
        {
            return FormatAmsRtrvCommand(amsOpcode, 0, position);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static ArrayList FormatAmsRtrvCommand(AmsOpcode amsOpcode, Ia.Ngn.Cl.Model.Business.Ont.FamilyType familyType, string position)
        {
            // Use this command with variable number of parameters
            string amsCommand, amsName, pon;
            ArrayList amsCommandArrayList;

            amsCommand = null;

            amsCommandArrayList = new ArrayList(10);

            if (!string.IsNullOrEmpty(position))
            {
                AmsNameAndCardPortOntSquenceFromOntPosition(position, out amsName, out pon);

                if (amsOpcode == AmsOpcode.RtrvOnt)
                {
                    amsCommand = "RTRV-ONT:" + amsName + ":ONT-1-1-" + pon + ";";
                    amsCommandArrayList.Add(amsCommand);
                }
                else if (amsOpcode == AmsOpcode.RtrvServiceHsi)
                {
                    foreach (string ontHsiCardPortService in Ia.Ngn.Cl.Model.Business.Nokia.Ams.PossibleHsiCardPortServiceConfigurationForOntFamilyTypeArrayList(familyType))
                    {
                        amsCommand = "RTRV-SERVICE-HSI:" + amsName + ":HSI-1-1-" + pon + "-" + ontHsiCardPortService + ":" + ";";
                        amsCommandArrayList.Add(amsCommand);
                    }
                }
                else if (amsOpcode == AmsOpcode.RtrvServiceVoip)
                {
                    amsCommand = "RTRV-SERVICE-VOIP:" + amsName + ":VOIP-1-1-" + pon + "-1;";
                    amsCommandArrayList.Add(amsCommand);
                }
                else if (amsOpcode == AmsOpcode.RtrvOntPots)
                {
                    foreach (string ontPotsCardPort in Ia.Ngn.Cl.Model.Business.Nokia.Ams.PossiblePotsCardPortConfigurationForOntFamilyTypeArrayList(familyType))
                    {
                        amsCommand = "RTRV-ONTPOTS:" + amsName + ":ONTPOTS-1-1-" + pon + "-" + ontPotsCardPort + ";";
                        amsCommandArrayList.Add(amsCommand);
                    }
                }
            }

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static ArrayList FormatAmsOntCommand(AmsOpcode amsOpcode, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont ont, string ontDescription)
        {
            string amsCommand, amsName, pon;
            ArrayList amsCommandArrayList;

            amsCommand = null;

            amsCommandArrayList = new ArrayList(10);

            if (!string.IsNullOrEmpty(ont.Position))
            {
                AmsNameAndCardPortOntSquenceFromOntPosition(ont.Position, out amsName, out pon);

                if (amsOpcode == AmsOpcode.EdOntDesc1)
                {
                    amsCommand = "ED-ONT:" + amsName + @":ONT-1-1-" + pon + "::::DESC1=" + ont.Access.Name + @":;";
                    // ED-ONT:ESH-1-1:ONT-1-1-2-2-26::::DESC1=ESH.4.26:;

                    amsCommandArrayList.Add(amsCommand);
                }
            }

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static ArrayList FormatAmsOntOntPotsCommand(AmsOpcode amsOpcode, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont ont, string termination, string customer)
        {
            string amsCommand, amsName, pon;
            ArrayList amsCommandArrayList;

            amsCommand = null;

            amsCommandArrayList = new ArrayList(10);

            if (!string.IsNullOrEmpty(ont.Position))
            {
                AmsNameAndCardPortOntSquenceFromOntPosition(ont.Position, out amsName, out pon);

                termination = termination.Replace("td", "");

                if (amsOpcode == AmsOpcode.EdOntPotsCustinfo)
                {
                    amsCommand = "ED-ONTPOTS:" + amsName + @":ONTPOTS-1-1-" + pon + "-2-" + termination + "::::CUSTINFO=" + customer + @":;";
                    // ED-ONTPOTS:ESH-1-1:ONTPOTS-1-1-1-1-1-2-1::::CUSTINFO=23632222:;\n";

                    amsCommandArrayList.Add(amsCommand);
                }
            }

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static string FormatAmsCommand(AmsOpcode amsOpcode, string amsName)
        {
            string amsCommand;

            if (amsOpcode == AmsOpcode.RtrvAlmPon)
            {
                amsCommand = "RTRV-ALM-PON:" + amsName + ":ALL:::MN,,NSA;";
            }
            else if (amsOpcode == AmsOpcode.RtrvHdr)
            {
                amsCommand = "RTRV-HDR:" + amsName + "::" + Ia.Cl.Model.Default.Random(10000) + ";";
            }
            else amsCommand = null;

            return amsCommand;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static ArrayList FormatAmsCommand(AmsOpcode amsOpcode, string ontServiceHsiPosition, Ia.Ngn.Cl.Model.Data.Hsi.ProfileName downloadProfile, Ia.Ngn.Cl.Model.Data.Hsi.ProfileName uploadProfile)
        {
            string amsCommand, amsName, cardPortOntCardPortServiceSequence;
            ArrayList amsCommandArrayList;

            amsCommand = null;

            amsCommandArrayList = new ArrayList(10);

            AmsNameAndCardPortOntSquenceFromOntServiceHsiPosition(ontServiceHsiPosition, out amsName, out cardPortOntCardPortServiceSequence);

            if (amsOpcode == AmsOpcode.EdServiceHsi)
            {
                amsCommand = "ED-SERVICE-HSI:" + amsName + ":HSI-1-1-" + cardPortOntCardPortServiceSequence + "::::BWPROFUPID=" + (int)uploadProfile + ",BWPROFDNID=" + (int)downloadProfile + ":;\n";

                // ED-SERVICE-HSI:SUR-1-1:HSI-1-1-1-1-5-1-1-1::::BWPROFUPID=50,BWPROFDNID=50:;

                // below: maybe use this
                // ed-service-hsi::hsi-1-1-7-2-6-1-1-1::::BWPROFUPID=,BWPROFUPNM=,BWPROFDNID=,BWPROFDNNM=,LABEL=:oos;
                // dlt-service-hsi::hsi-1-1-7-2-6-1-1-1::;
                // ent-service-hsi::hsi-1-1-7-2-6-1-1-1::::BWPROFUPID=50,BWPROFDNID=50,PQPROFID=2,SVLAN=305:IS;

                amsCommandArrayList.Add(amsCommand);
            }

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void AmsNameAndCardPortOntSquenceFromOntServiceHsiPosition(string ontPosition, out string amsName, out string cardPortOntCardPortServiceSequence)
        {
            Match match;

            // SUR-1-1-1-1-1-1-1-1;
            match = Regex.Match(ontPosition, @"(\w{3}\-\d{1,2}\-\d{1,2})\-(\d{1,2}\-\d{1,2}\-\d{1,2}\-\d{1,2}\-\d{1,2}\-\d{1,2})");

            amsName = match.Groups[1].Value;
            cardPortOntCardPortServiceSequence = match.Groups[2].Value;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void AmsNameAndCardPortOntSquenceFromOntPosition(string ontPosition, out string amsName, out string pon)
        {
            Match match;

            if (!string.IsNullOrEmpty(ontPosition))
            {
                // SUR-1-1-1-1-1;
                match = Regex.Match(ontPosition, @"(\w{3}\-\d{1,2}\-\d{1,2})\-(\d{1,2}\-\d{1,2}\-\d{1,2})");

                amsName = match.Groups[1].Value;
                pon = match.Groups[2].Value;
            }
            else
            {
                amsName = string.Empty;
                pon = string.Empty;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Extract OLT Id, Rack, Sub, Card, Port, and ONT from ONT Position
        /// </summary>
        public static void OltIdRackSubCardPortOntFromOntPosition(string ontPosition, out int oltId, out int rack, out int sub, out int card, out int port, out int ont)
        {
            string amsName;
            Match match;

            // SUR-1-1-1-1-1;
            match = Regex.Match(ontPosition, @"(\w{3}\-(\d{1,2})\-(\d{1,2}))\-(\d{1,2})\-(\d{1,2})\-(\d{1,2})");

            amsName = match.Groups[1].Value;
            rack = int.Parse(match.Groups[2].Value);
            sub = int.Parse(match.Groups[3].Value);
            card = int.Parse(match.Groups[4].Value);
            port = int.Parse(match.Groups[5].Value);
            ont = int.Parse(match.Groups[6].Value);

            oltId = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList where q.AmsName == amsName select q.Id).FirstOrDefault();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Extract rack, sub from AmsName
        /// </summary>
        public static void RackSubFromAmsName(string amsName, out int rack, out int sub)
        {
            Match match;

            // SUR-1-1;
            match = Regex.Match(amsName, @"(\w{3}\-(\d{1,2})\-(\d{1,2}))");

            rack = int.Parse(match.Groups[2].Value);
            sub = int.Parse(match.Groups[3].Value);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string OntPositionFromAmsNameAndCardPortOntSquence(string amsName, string pon)
        {
            string ontPosition;

            ontPosition = amsName + "-" + pon;

            return ontPosition;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToInsertAndDisplayCorrectDescriptionIntoOntArrayList()
        {
            return AmsCommandsToInsertAndDisplayCorrectDescriptionIntoOntArrayList(true);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToInsertCorrectDescriptionIntoOntArrayList()
        {
            return AmsCommandsToInsertAndDisplayCorrectDescriptionIntoOntArrayList(false);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static ArrayList AmsCommandsToInsertAndDisplayCorrectDescriptionIntoOntArrayList(bool includeRetrieveCommand)
        {
            string accessName;
            ArrayList amsCommandArrayList;
            Hashtable ontListIdToAccessNameHashtable;
            Ia.Ngn.Cl.Model.Business.Ont.FamilyType familyType;
            Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt;
            List<Ia.Ngn.Cl.Model.Ont> ontList;

            ontListIdToAccessNameHashtable = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntListIdToAccessNameHashtable;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ontList = (from o in db.Onts where o.Access != null select o).ToList();

                amsCommandArrayList = new ArrayList(ontList.Count);

                foreach (Ia.Ngn.Cl.Model.Ont ont in ontList)
                {
                    if (ontListIdToAccessNameHashtable[ont.Id] != null) accessName = ontListIdToAccessNameHashtable[ont.Id].ToString();
                    else accessName = string.Empty;

                    if (ont.Description1 != accessName)
                    {
                        nddOnt = (from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where nddo.Id == ont.Id select nddo).SingleOrDefault();

                        if (nddOnt != null)
                        {
                            familyType = (Ia.Ngn.Cl.Model.Business.Ont.FamilyType)ont.FamilyTypeId;

                            amsCommandArrayList.AddRange(Ia.Ngn.Cl.Model.Business.Nokia.Ams.FormatAmsOntCommand(AmsOpcode.EdOntDesc1, nddOnt, nddOnt.Access.Name));

                            if (includeRetrieveCommand) amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvOnt, familyType, nddOnt.Position));
                        }
                    }
                    else
                    {
                    }
                }
            }

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToInsertAndDisplayCorrectCustomerIntoOntPotsArrayList()
        {
            return AmsCommandsToInsertAndOptionallyDisplayCorrectCustomerIntoOntPotsArrayList(true);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToInsertCorrectCustomerIntoOntPotsArrayList()
        {
            return AmsCommandsToInsertAndOptionallyDisplayCorrectCustomerIntoOntPotsArrayList(false);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static ArrayList AmsCommandsToInsertAndOptionallyDisplayCorrectCustomerIntoOntPotsArrayList(bool includeRetrieveCommand)
        {
            ArrayList amsCommandArrayList;
            Ia.Ngn.Cl.Model.Business.Ont.FamilyType familyType;
            Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                var ontOntPotsServiceList = (from sp in db.SubParties
                                             join e in db.AgcfEndpoints on sp.AgcfEndpoint.Id equals e.Id
                                             join gr in db.AgcfGatewayRecords on e.GwId equals gr.GwId
                                             join osv in db.OntServiceVoips on gr.IP1 equals osv.Ip
                                             join o in db.Onts on osv.Ont.Id equals o.Id
                                             join oop in db.OntOntPotses on o.Id equals oop.Ont.Id
                                             where oop.Customer != sp.DisplayName && oop.Termination == "td" + e.FlatTermID.ToString()
                                             select new
                                             {
                                                 Service = sp.DisplayName,
                                                 Termination = oop.Termination,
                                                 OntId = o.Id,
                                                 OntFamilyTypeId = o.FamilyTypeId
                                             }).ToList();

                amsCommandArrayList = new ArrayList(ontOntPotsServiceList.Count);

                foreach (var ontOntPotsService in ontOntPotsServiceList)
                {
                    nddOnt = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where q.Id == ontOntPotsService.OntId select q).SingleOrDefault();

                    if (nddOnt != null)
                    {
                        familyType = (Ia.Ngn.Cl.Model.Business.Ont.FamilyType)ontOntPotsService.OntFamilyTypeId;

                        amsCommandArrayList.AddRange(Ia.Ngn.Cl.Model.Business.Nokia.Ams.FormatAmsOntOntPotsCommand(AmsOpcode.EdOntPotsCustinfo, nddOnt, ontOntPotsService.Termination, ontOntPotsService.Service));

                        if(includeRetrieveCommand) amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvOntPots, familyType, nddOnt.Position));
                    }
                    else
                    {
                    }
                }
            }

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToEditOntServiceHsiArrayList()
        {
            string ispName, amsName, ontServiceHsiPosition;
            ArrayList amsCommandArrayList;
            List<Ia.Ngn.Cl.Model.OntServiceHsi> oshList;

            amsCommandArrayList = null;

            try
            {
                oshList = Ia.Ngn.Cl.Model.Business.OntServiceHsi.ReturnOntServiceHsiAndOntAndAccessListForIspAndBandwidth(Ia.Ngn.Cl.Model.Data.Hsi.IspName.QualityNet, Ia.Ngn.Cl.Model.Data.Hsi.ProfileName.bw50Mbs);

                if (oshList != null && oshList.Count > 0)
                {
                    amsCommandArrayList = new ArrayList(oshList.Count);

                    ispName = Ia.Ngn.Cl.Model.Data.Hsi.IspList.Find(s => s.Id == (int)Ia.Ngn.Cl.Model.Data.Hsi.IspName.QualityNet).Name;

                    foreach (Ia.Ngn.Cl.Model.OntServiceHsi osh in oshList)
                    {
                        amsName = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.AmsNameFromOltId(osh.Ont.Access.Olt);

                        //ontServiceHsiPosition = amsName + "-" + osh.Ont.Access.Card + "-" + osh.Ont.Access.Port + "-" + osh.Ont.Access.Ont + "-" + osh.Card + "-" + osh.Port + "-" + osh.Service;
                        ontServiceHsiPosition = osh.Ont.Access.Position + "-" + osh.Card + "-" + osh.Port + "-" + osh.Service;

                        // below: set bandwidth to 20Mb
                        amsCommandArrayList.AddRange(Ia.Ngn.Cl.Model.Business.Nokia.Ams.FormatAmsCommand(AmsOpcode.EdServiceHsi, ontServiceHsiPosition, Ia.Ngn.Cl.Model.Data.Hsi.ProfileName.bw20Mbs, Ia.Ngn.Cl.Model.Data.Hsi.ProfileName.bw20Mbs));
                    }
                }
                else
                {
                    //result = @"Error: OntServiceHsi query result is null or empty. ";
                }
            }
            catch (Exception)
            {
                //result = @"Exception: " + ex.ToString();
            }

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToDisplayOntVoipPotsHsiForOntsWithDefinedFamilyTypeAndForOtherOntsDefinedInNddDocumentArrayList(long oltId)
        {
            int totalCount;
            Random r;
            ArrayList al1, al2, al;

            r = new Random();

            al1 = AmsCommandsToDisplayOntsDefinedInNddDocumentArrayList(oltId);

            al2 = AmsCommandsToDisplayOntVoipPotsHsiForOntsWithDefinedFamilyTypeArrayList(oltId);

            totalCount = al1.Count + al2.Count;

            al = new ArrayList(totalCount);

            foreach (var item in al1) al.Add(item);
            foreach (var item in al2) al.Add(item);

            return Ia.Cl.Model.Default.ShuffleArrayList(al);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToDisplayOntsDefinedInNddDocumentArrayList(string accessName)
        {
            ArrayList amsCommandArrayList;
            Ia.Ngn.Cl.Model.Business.Ont.FamilyType familyType;
            Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt;
            Ia.Ngn.Cl.Model.Ont ont;

            amsCommandArrayList = new ArrayList(5); // 5 is max number of commands from this function

            if (!string.IsNullOrEmpty(accessName))
            {
                nddOnt = (from o in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where o.Access != null && o.Access.Name == accessName select o).SingleOrDefault();

                if (nddOnt != null)
                {
                    familyType = Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Sfu;

                    amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvOnt, familyType, nddOnt.Position));
                    amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvServiceHsi, familyType, nddOnt.Position));
                    amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvServiceVoip, familyType, nddOnt.Position));
                    amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvOntPots, familyType, nddOnt.Position));
                }
            }

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToDisplayOntsDefinedInNddDocumentArrayList(long oltId)
        {
            ArrayList amsCommandArrayList;
            Ia.Ngn.Cl.Model.Business.Ont.FamilyType familyType;
            List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> ontList;

            ontList = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where q.Pon.Olt.Id == oltId select q).ToList();

            familyType = Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Sfu;

            amsCommandArrayList = new ArrayList(ontList.Count);

            foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont ont in ontList)
            {
                amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvOnt, familyType, ont.Position));
            }

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToDisplayOntVoipPotsHsiForOntsWithDefinedFamilyTypeArrayList(string accessName)
        {
            ArrayList amsCommandArrayList;
            Ia.Ngn.Cl.Model.Business.Ont.FamilyType familyType;
            Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt;
            Ia.Ngn.Cl.Model.Ont ont;

            amsCommandArrayList = new ArrayList(5); // 5 is max number of commands from this function

            if (!string.IsNullOrEmpty(accessName))
            {
                nddOnt = (from o in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where o.Access != null && o.Access.Name == accessName select o).SingleOrDefault();

                if (nddOnt != null)
                {
                    ont = Ia.Ngn.Cl.Model.Data.Ont.Read(nddOnt.Id);

                    if (ont != null)
                    {
                        familyType = (Ia.Ngn.Cl.Model.Business.Ont.FamilyType)ont.FamilyTypeId;

                        amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvOnt, familyType, nddOnt.Position));
                        amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvServiceHsi, familyType, nddOnt.Position));
                        amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvServiceVoip, familyType, nddOnt.Position));
                        amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvOntPots, familyType, nddOnt.Position));
                    }
                }
            }

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToDisplayOntVoipPotsHsiForOntsWithDefinedFamilyTypeArrayList(long oltId)
        {
            Ia.Ngn.Cl.Model.Business.Ont.FamilyType familyType;
            ArrayList amsCommandArrayList;
            List<Ia.Ngn.Cl.Model.Ont> ontList;

            ontList = Ia.Ngn.Cl.Model.Data.Ont.ReadListIncludeAccess(oltId);

            amsCommandArrayList = new ArrayList(ontList.Count);

            foreach (Ia.Ngn.Cl.Model.Ont ont in ontList)
            {
                familyType = (Ia.Ngn.Cl.Model.Business.Ont.FamilyType)ont.FamilyTypeId;

                if (ont.Access != null)
                {
                    amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvOnt, familyType, ont.Access.Position));
                    amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvServiceHsi, familyType, ont.Access.Position));
                    amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvServiceVoip, familyType, ont.Access.Position));
                    amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvOntPots, familyType, ont.Access.Position));
                }
            }

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToRetrieveNewOntAlarmsForOltArrayList()
        {
            ArrayList amsCommandArrayList;

            amsCommandArrayList = new ArrayList(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList.Count);

            foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList)
            {
                amsCommandArrayList.Add(FormatAmsCommand(AmsOpcode.RtrvAlmPon, olt.AmsName));
            }

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToDowngradeOntServiceHsiForQualityNetBandwidthRateFrom50MbTo20MbThenDisplayArrayList()
        {
            string ontPosition;
            Ia.Ngn.Cl.Model.Business.Ont.FamilyType familyType;
            ArrayList amsCommandArrayList;
            List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList;

            ontServiceHsiList = null; // Ia.Ngn.Cl.Model.Business.OntServiceHsi.ReturnOntServiceHsiListForIspAndBandwidthAndTakeN(Ia.Ngn.Cl.Model.Data.Hsi.IspName.QualityNet, Ia.Ngn.Cl.Model.Data.Hsi.ProfileName.bw50Mbs, 1000);

            amsCommandArrayList = new ArrayList(ontServiceHsiList.Count);

            foreach (Ia.Ngn.Cl.Model.OntServiceHsi ontServiceHsi in ontServiceHsiList)
            {
                ontPosition = Ia.Ngn.Cl.Model.Business.Ont.OntPositionFromOntId(ontServiceHsi.Id);

                familyType = (Ia.Ngn.Cl.Model.Business.Ont.FamilyType)ontServiceHsi.Ont.FamilyTypeId;

                amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvOnt, familyType, ontPosition));
                amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvServiceHsi, familyType, ontPosition));
                amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvServiceVoip, familyType, ontPosition));
                amsCommandArrayList.AddRange(FormatAmsRtrvCommand(AmsOpcode.RtrvOntPots, familyType, ontPosition));
            }

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToUpdateAndDisplayOntDescriptionWithItsAccessNameArrayList()
        {
            ArrayList amsCommandArrayList;

            amsCommandArrayList = Ia.Ngn.Cl.Model.Business.Nokia.Ams.AmsCommandsToInsertAndDisplayCorrectDescriptionIntoOntArrayList();

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToUpdateOntOntPotsCustomerWithItsConnectedServiceNumberArrayList()
        {
            ArrayList amsCommandArrayList;

            amsCommandArrayList = Ia.Ngn.Cl.Model.Business.Nokia.Ams.AmsCommandsToInsertCorrectCustomerIntoOntPotsArrayList();

            return Ia.Cl.Model.Default.ShuffleArrayList(amsCommandArrayList);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ArrayList AmsCommandsToUpdateOntServiceHsiDescriptionWithItsIspNameArrayList()
        {
            ArrayList amsCommandArrayList;

            amsCommandArrayList = new ArrayList(100);

            return amsCommandArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static int PossibleNumberOfTdForOntFamilyType(Ia.Ngn.Cl.Model.Business.Ont.FamilyType familyType)
        {
            int number;

            switch (familyType)
            {
                case Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Sfu: number = 4; break;
                case Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Soho: number = 8; break;
                case Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Mdu: number = 24; break;
                default: number = 0; break;
            }

            return number;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Get the number of card-port for a parameter of familyType and td
        /// </summary>
        public static void ReturnOntPotsCardAndPortFromFamilyTypeAndTd(Ia.Ngn.Cl.Model.Business.Ont.FamilyType familyType, int td, out int card, out int port)
        {
            card = port = 0;

            if (familyType == Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Sfu)
            {
                if (td >= 1 && td <= 4)
                {
                    card = 2; port = td;
                }
            }
            else if (familyType == Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Soho)
            {
                if (td >= 1 && td <= 8)
                {
                    card = 2; port = td;
                }
            }
            else if (familyType == Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Mdu)
            {
                if (td >= 1 && td <= 24)
                {
                    if (td >= 1 && td <= 4 || td >= 13 && td <= 16)
                    {
                        card = 1;
                        if (td <= 4) port = td;
                        else port = td - 8;
                    }
                    else if (td >= 5 && td <= 8 || td >= 17 && td <= 20)
                    {
                        card = 2;
                        if (td <= 8) port = td - 4;
                        else port = td - 12;
                    }
                    else if (td >= 9 && td <= 12 || td >= 21 && td <= 24)
                    {
                        card = 3;
                        if (td <= 12) port = td - 8;
                        else port = td - 16;
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static ArrayList PossiblePotsCardPortConfigurationForOntFamilyTypeArrayList(Ia.Ngn.Cl.Model.Business.Ont.FamilyType familyType)
        {
            ArrayList al;

            al = new ArrayList(100);

            // 1-1

            if (familyType == Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Sfu)
            {
                al.Add("2-1");
                al.Add("2-2");
                al.Add("2-3");
                al.Add("2-4");
            }
            else if (familyType == Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Soho)
            {
                al.Add("2-1");
                al.Add("2-2");
                al.Add("2-3");
                al.Add("2-4");
                al.Add("2-5");
                al.Add("2-6");
                al.Add("2-7");
                al.Add("2-8");
            }
            else if (familyType == Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Mdu)
            {
                al.Add("1-1");
                al.Add("1-2");
                al.Add("1-3");
                al.Add("1-4");
                al.Add("1-5");
                al.Add("1-6");
                al.Add("1-7");
                al.Add("1-8");

                al.Add("2-1");
                al.Add("2-2");
                al.Add("2-3");
                al.Add("2-4");
                al.Add("2-5");
                al.Add("2-6");
                al.Add("2-7");
                al.Add("2-8");

                al.Add("3-1");
                al.Add("3-2");
                al.Add("3-3");
                al.Add("3-4");
                al.Add("3-5");
                al.Add("3-6");
                al.Add("3-7");
                al.Add("3-8");
            }

            return al;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static ArrayList PossibleHsiCardPortServiceConfigurationForOntFamilyTypeArrayList(Ia.Ngn.Cl.Model.Business.Ont.FamilyType familyType)
        {
            ArrayList al;

            al = new ArrayList(100);

            // 1-1-1

            if (familyType == Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Sfu || familyType == Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Soho)
            {
                al.Add("1-1-1");
                al.Add("1-2-1");
            }
            else if (familyType == Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Mdu)
            {
                // ???
                al.Add("1-1-1");
                al.Add("1-2-1");
                al.Add("1-3-1");
                al.Add("1-4-1");
                al.Add("1-5-1");
                al.Add("1-6-1");
                al.Add("1-7-1");
                al.Add("1-8-1");
                al.Add("1-9-1");
                al.Add("1-10-1");
                al.Add("1-11-1");
                al.Add("1-12-1");
            }

            return al;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Return the ONT family type from the software version
        /// </summary>
        public static Ia.Ngn.Cl.Model.Business.Ont.FamilyType FamilyType(string activeSoftware, string plannedSoftware)
        {
            Ia.Ngn.Cl.Model.Business.Ont.FamilyType familyType;

            if (activeSoftware != null)
            {
                if (activeSoftware == plannedSoftware)
                {
                    if (activeSoftware.Contains("3FE508")) familyType = Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Sfu;
                    else if (activeSoftware.Contains("3FE511")) familyType = Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Soho;
                    else if (activeSoftware.Contains("3FE514")) familyType = Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Mdu;
                    else familyType = Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Undefined;
                }
                else familyType = Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Undefined;
            }
            else familyType = Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Undefined;

            return familyType;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateDatabaseWithAmsCommandOutput(string rowData)
        {
            bool b;
            string accessId, ontId;
            string line, amsName, pon;
            DateTime eventTime;
            Match match;
            MatchCollection matchCollection;

            Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState state;

            b = false;

            // below: remove all '\' characters from rowData and reset NULL comments to ""
            rowData = rowData.Replace(@"\", "");
            rowData = rowData.Replace(@"NULL", "");

            if (rowData.Contains("RTRV-ONT:"))
            {
                #region RTRV-ONT
                Ia.Ngn.Cl.Model.Ont ont, dataOnt;

                /*
       SUR-1-1 08-07-10 09:35:07
    M  0 COMPLD
       / * RTRV-ONT:SUR-1-1:ONT-1-1-1-1-1&ONT-1-1-1-1-10&ONT-1-1-1-1-2&ONT-1-1-1 * /
       / * -1-3&ONT-1-1-1-1-4&ONT-1-1-1-1-5&ONT-1-1-1-1-6&ONT-1-1-1-1-7&ONT-1-1- * /
       / * 1-1-8&ONT-1-1-1-1-9 * /
       "ONT-1-1-1-1-1::BTRYBKUP=NO,BERINT=8000,DESC1="SLA.1.1",
       DESC2="all is well",PROVVERSION="*",SERNUM=ALCLA0A1A5F8,
       SUBSLOCID="WILDCARD",SWVERPLND="3FE50853AFMA07",FECUP=DISABLE,
       SCHEDPROFID=1,SCHEDPROFNM="defSubSchedProf",POWERSHEDPROFID=0,
       POWERSHEDPROFNM="NULL",ONTENABLE=AUTO,EQPTVERNUM=3FE50683ADAA01,
       SWVERACT=3FE50853AFMA07,SWVERPSV=3FE50853AFKA03,VENDORID=ALCL,
       EQUIPID=BVM3G00CRAO420EB    ,NUMSLOTS=2,NUMTCONT=22,NUMTRFSCH=22,NUMPQ=22:
       IS-NR"
       "ONT-1-1-1-1-10::BTRYBKUP=NO,BERINT=8000,DESC1="NULL",DESC2="NULL",
       PROVVERSION="*",SERNUM=ALCLA0A1A479,SUBSLOCID="WILDCARD",
       SWVERPLND="3FE50853AFMA07",FECUP=DISABLE,SCHEDPROFID=1,
       SCHEDPROFNM="defSubSchedProf",POWERSHEDPROFID=0,POWERSHEDPROFNM="NULL",
       ONTENABLE=AUTO,EQPTVERNUM=3FE50683ADAA01,SWVERACT=3FE50853AFMA07,
       SWVERPSV=3FE50853AFKA03,VENDORID=ALCL,EQUIPID=BVM3G00CRAO420EB    ,
       NUMSLOTS=2,NUMTCONT=22,NUMTRFSCH=22,NUMPQ=22:IS-NR"
       "ONT-1-1-1-1-2::BTRYBKUP=NO,BERINT=8000,DESC1="NULL",DESC2="NULL",
       PROVVERSION="*",SERNUM=ALCLA0A261BB,SUBSLOCID="WILDCARD",
       SWVERPLND="3FE50853AFMA07",FECUP=DISABLE,SCHEDPROFID=1,
       SCHEDPROFNM="defSubSchedProf",POWERSHEDPROFID=0,POWERSHEDPROFNM="NULL",
       ONTENABLE=AUTO,EQPTVERNUM=3FE50683ADAB01,SWVERACT=3FE50853AFMA07,
       SWVERPSV=3FE50853AAAA22,VENDORID=ALCL,EQUIPID=BVM3G00CRAO420EB    ,
       NUMSLOTS=2,NUMTCONT=22,NUMTRFSCH=22,NUMPQ=22:IS-NR"
                 */

                // below: information from the definition of "RTRV-ONT" in "AMS TL1 Commands Reference"

                /*
IP 0
<

   SUR-1-1 08-07-10 09:35:07
M  0 COMPLD
   / * RTRV-ONT:SUR-1-1:ONT-1-1-1-1-1&ONT-1-1-1-1-10&ONT-1-1-1-1-2&ONT-1-1-1 * /
   / * -1-3&ONT-1-1-1-1-4&ONT-1-1-1-1-5&ONT-1-1-1-1-6&ONT-1-1-1-1-7&ONT-1-1- * /
   / * 1-1-8&ONT-1-1-1-1-9 * /
   "ONT-1-1-1-1-1::BTRYBKUP=NO,BERINT=8000,DESC1="SLA.1.1",
   DESC2="all is well",PROVVERSION="*",SERNUM=ALCLA0A1A5F8,
   SUBSLOCID="WILDCARD",SWVERPLND="3FE50853AFMA07",FECUP=DISABLE,
   SCHEDPROFID=1,SCHEDPROFNM="defSubSchedProf",POWERSHEDPROFID=0,
   POWERSHEDPROFNM="NULL",ONTENABLE=AUTO,EQPTVERNUM=3FE50683ADAA01,
   SWVERACT=3FE50853AFMA07,SWVERPSV=3FE50853AFKA03,VENDORID=ALCL,
   EQUIPID=BVM3G00CRAO420EB    ,NUMSLOTS=2,NUMTCONT=22,NUMTRFSCH=22,NUMPQ=22:
   IS-NR"
   "ONT-1-1-1-1-10::BTRYBKUP=NO,BERINT=8000,DESC1="NULL",DESC2="NULL",
   PROVVERSION="*",SERNUM=ALCLA0A1A479,SUBSLOCID="WILDCARD",
   SWVERPLND="3FE50853AFMA07",FECUP=DISABLE,SCHEDPROFID=1,
   SCHEDPROFNM="defSubSchedProf",POWERSHEDPROFID=0,POWERSHEDPROFNM="NULL",
   ONTENABLE=AUTO,EQPTVERNUM=3FE50683ADAA01,SWVERACT=3FE50853AFMA07,
   SWVERPSV=3FE50853AFKA03,VENDORID=ALCL,EQUIPID=BVM3G00CRAO420EB    ,
   NUMSLOTS=2,NUMTCONT=22,NUMTRFSCH=22,NUMPQ=22:IS-NR"
   "ONT-1-1-1-1-2::BTRYBKUP=NO,BERINT=8000,DESC1="NULL",DESC2="NULL",
   PROVVERSION="*",SERNUM=ALCLA0A261BB,SUBSLOCID="WILDCARD",
   SWVERPLND="3FE50853AFMA07",FECUP=DISABLE,SCHEDPROFID=1,
   SCHEDPROFNM="defSubSchedProf",POWERSHEDPROFID=0,POWERSHEDPROFNM="NULL",
   ONTENABLE=AUTO,EQPTVERNUM=3FE50683ADAB01,SWVERACT=3FE50853AFMA07,
   SWVERPSV=3FE50853AAAA22,VENDORID=ALCL,EQUIPID=BVM3G00CRAO420EB    ,
   NUMSLOTS=2,NUMTCONT=22,NUMTRFSCH=22,NUMPQ=22:IS-NR"

   / * More Output Follows * /
>

   SUR-1-1 08-07-10 09:35:07
M  0 COMPLD
   / * RTRV-ONT:SUR-1-1:ONT-1-1-1-1-1&ONT-1-1-1-1-10&ONT-1-1-1-1-2&ONT-1-1-1-1-3&ONT-1-1-1-1-4&ONT-1-1-1-1-5&ONT-1-1-1-1-6&ONT-1-1-1-1-7&ONT-1-1-1-1-8&ONT-1-1-1-1-9 * /
   "ONT-1-1-1-1-3::BTRYBKUP=NO,BERINT=8000,DESC1="NULL",DESC2="NULL",
   PROVVERSION="*",SERNUM=ALCLA0A1C9BC,SUBSLOCID="WILDCARD",
   SWVERPLND="3FE50853AFMA07",FECUP=DISABLE,SCHEDPROFID=1,
   SCHEDPROFNM="defSubSchedProf",POWERSHEDPROFID=0,POWERSHEDPROFNM="NULL",
   ONTENABLE=AUTO,EQPTVERNUM=3FE50683ADAB01,SWVERACT=3FE50853AFMA07,
   SWVERPSV=3FE50853AFKA03,VENDORID=ALCL,EQUIPID=BVM3G00CRAO420EB    ,
   NUMSLOTS=2,NUMTCONT=22,NUMTRFSCH=22,NUMPQ=22:IS-NR"
   "ONT-1-1-1-1-4::BTRYBKUP=NO,BERINT=8000,DESC1="NULL",DESC2="NULL",
   PROVVERSION="*",SERNUM=ALCLA0A1A47A,SUBSLOCID="WILDCARD",
   SWVERPLND="3FE50853AFMA07",FECUP=DISABLE,SCHEDPROFID=1,
   SCHEDPROFNM="defSubSchedProf",POWERSHEDPROFID=0,POWERSHEDPROFNM="NULL",
   ONTENABLE=AUTO,EQPTVERNUM=3FE50683ADAA01,SWVERACT=3FE50853AFMA07,
   SWVERPSV=3FE50853AFKA03,VENDORID=ALCL,EQUIPID=BVM3G00CRAO420EB    ,
   NUMSLOTS=2,NUMTCONT=22,NUMTRFSCH=22,NUMPQ=22:IS-NR"
   "ONT-1-1-1-1-5::BTRYBKUP=NO,BERINT=8000,DESC1="NULL",DESC2="NULL",
   PROVVERSION="*",SERNUM=ALCLA0A1AE44,SUBSLOCID="WILDCARD",
   SWVERPLND="3FE50853AFMA07",FECUP=DISABLE,SCHEDPROFID=1,
   SCHEDPROFNM="defSubSchedProf",POWERSHEDPROFID=0,POWERSHEDPROFNM="NULL",
   ONTENABLE=AUTO,EQPTVERNUM=3FE50683ADAB01,SWVERACT=3FE50853AFMA07,
   SWVERPSV=3FE50853AFKA03,VENDORID=ALCL,EQUIPID=BVM3G00CRAO420EB    ,
   NUMSLOTS=2,NUMTCONT=22,NUMTRFSCH=22,NUMPQ=22:IS-NR"

   / * More Output Follows * /
>

   SUR-1-1 08-07-10 09:35:08
M  0 COMPLD
   / * RTRV-ONT:SUR-1-1:ONT-1-1-1-1-1&ONT-1-1-1-1-10&ONT-1-1-1-1-2&ONT-1-1-1-1-3&ONT-1-1-1-1-4&ONT-1-1-1-1-5&ONT-1-1-1-1-6&ONT-1-1-1-1-7&ONT-1-1-1-1-8&ONT-1-1-1-1-9 * /
   "ONT-1-1-1-1-6::BTRYBKUP=NO,BERINT=8000,DESC1="NULL",DESC2="NULL",
   PROVVERSION="*",SERNUM=ALCLA0A1BE26,SUBSLOCID="WILDCARD",
   SWVERPLND="3FE50853AFMA07",FECUP=DISABLE,SCHEDPROFID=1,
   SCHEDPROFNM="defSubSchedProf",POWERSHEDPROFID=0,POWERSHEDPROFNM="NULL",
   ONTENABLE=AUTO,EQPTVERNUM=3FE50683ADAB01,SWVERACT=3FE50853AFMA07,
   SWVERPSV=3FE50853AFKA03,VENDORID=ALCL,EQUIPID=BVM3G00CRAO420EB    ,
   NUMSLOTS=2,NUMTCONT=22,NUMTRFSCH=22,NUMPQ=22:IS-NR"
   "ONT-1-1-1-1-7::BTRYBKUP=NO,BERINT=8000,DESC1="NULL",DESC2="NULL",
   PROVVERSION="*",SERNUM=ALCLA0A1C94B,SUBSLOCID="WILDCARD",
   SWVERPLND="3FE50853AFMA07",FECUP=DISABLE,SCHEDPROFID=1,
   SCHEDPROFNM="defSubSchedProf",POWERSHEDPROFID=0,POWERSHEDPROFNM="NULL",
   ONTENABLE=AUTO,EQPTVERNUM=3FE50683ADAB01,SWVERACT=3FE50853AFMA07,
   SWVERPSV=3FE50853AFKA03,VENDORID=ALCL,EQUIPID=BVM3G00CRAO420EB    ,
   NUMSLOTS=2,NUMTCONT=22,NUMTRFSCH=22,NUMPQ=22:IS-NR"
   "ONT-1-1-1-1-8::BTRYBKUP=NO,BERINT=8000,DESC1="NULL",DESC2="NULL",
   PROVVERSION="*",SERNUM=ALCLA0A1A484,SUBSLOCID="WILDCARD",
   SWVERPLND="3FE50853AFMA07",FECUP=DISABLE,SCHEDPROFID=1,
   SCHEDPROFNM="defSubSchedProf",POWERSHEDPROFID=0,POWERSHEDPROFNM="NULL",
   ONTENABLE=AUTO,EQPTVERNUM=3FE50683ADAA01,SWVERACT=3FE50853AFMA07,
   SWVERPSV=3FE50853AFKA03,VENDORID=ALCL,EQUIPID=BVM3G00CRAO420EB    ,
   NUMSLOTS=2,NUMTCONT=22,NUMTRFSCH=22,NUMPQ=22:IS-NR"

   / * More Output Follows * /
>

   SUR-1-1 08-07-10 09:35:08
M  0 COMPLD
   / * RTRV-ONT:SUR-1-1:ONT-1-1-1-1-1&ONT-1-1-1-1-10&ONT-1-1-1-1-2&ONT-1-1-1-1-3&ONT-1-1-1-1-4&ONT-1-1-1-1-5&ONT-1-1-1-1-6&ONT-1-1-1-1-7&ONT-1-1-1-1-8&ONT-1-1-1-1-9 * /
   "ONT-1-1-1-1-9::BTRYBKUP=NO,BERINT=8000,DESC1="NULL",DESC2="NULL",
   PROVVERSION="*",SERNUM=ALCLA0A1A43E,SUBSLOCID="WILDCARD",
   SWVERPLND="3FE50853AFMA07",FECUP=DISABLE,SCHEDPROFID=1,
   SCHEDPROFNM="defSubSchedProf",POWERSHEDPROFID=0,POWERSHEDPROFNM="NULL",
   ONTENABLE=AUTO,EQPTVERNUM=3FE50683ADAA01,SWVERACT=3FE50853AFMA07,
   SWVERPSV=3FE50853AFKA03,VENDORID=ALCL,EQUIPID=BVM3G00CRAO420EB    ,
   NUMSLOTS=2,NUMTCONT=22,NUMTRFSCH=22,NUMPQ=22:IS-NR"
;
                */

                // below: read OntPosition
                match = Regex.Match(rowData, @"RTRV-ONT:(\w{3}-\d{1,2}-\d{1,2})", RegexOptions.Singleline);

                amsName = match.Groups[1].Value;

                match = Regex.Match(rowData, @"ONT-1-1-(\d{1,2}-\d{1,2}-\d{1,2})::(.+?):\s*(IS-NR|OOS-AU|OOS-MA|OOS-AUMA)", RegexOptions.Singleline);

                if (match.Success)
                {
                    pon = match.Groups[1].Value;

                    line = match.Groups[2].Value;

                    ontId = Ia.Ngn.Cl.Model.Business.Ont.OntId(amsName, pon);

                    dataOnt = new Ia.Ngn.Cl.Model.Ont();

                    dataOnt.Id = ontId;
                    dataOnt.BatteryBackupAvailable = (Ia.Cl.Model.Default.Match(line, @"BTRYBKUP=(\w+)") == "YES") ? true : false;
                    dataOnt.Description1 = Ia.Cl.Model.Default.Match(line, @"DESC1=""([^""]{1,64})""");
                    dataOnt.Description2 = Ia.Cl.Model.Default.Match(line, @"DESC2=""([^""]{1,64})""");
                    dataOnt.Serial = Ia.Cl.Model.Default.Match(line, @"SERNUM=(\w{12})");
                    dataOnt.PlannedSoftware = Ia.Cl.Model.Default.Match(line, @"SWVERPLND=""(\w{14})""");
                    dataOnt.ActiveSoftware = Ia.Cl.Model.Default.Match(line, @"SWVERACT=(\w{14})");
                    dataOnt.PassiveSoftware = Ia.Cl.Model.Default.Match(line, @"SWVERPSV=(\w{14})");
                    dataOnt.FamilyTypeId = (int)FamilyType(dataOnt.ActiveSoftware, dataOnt.PlannedSoftware);
                    dataOnt.VendorId = Ia.Ngn.Cl.Model.Business.Default.VendorIdFromName(Ia.Cl.Model.Default.Match(line, @"VENDORID=(\w+)"));
                    dataOnt.EquipmentId = Ia.Cl.Model.Default.Match(line, @"EQUIPID=(\w{16})");

                    switch (match.Groups[3].Value)
                    {
                        case "IS-NR": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.IsNr; break;
                        case "OOS-AU": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosAu; break;
                        case "OOS-MA": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosMa; break;
                        case "OOS-AUMA": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosAuma; break;
                        default: state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.Undefined; break;
                    }

                    dataOnt.StateId = (int)state;

                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        accessId = Ia.Ngn.Cl.Model.Access.AccessId(ontId);

                        if (accessId != null) dataOnt.Access = (from q in db.Accesses where q.Id == accessId select q).SingleOrDefault();

                        ont = (from q in db.Onts where q.Id == dataOnt.Id select q).SingleOrDefault();

                        if (ont == null)
                        {
                            dataOnt.Created = dataOnt.Updated = dataOnt.Inspected = DateTime.UtcNow.AddHours(3);

                            db.Onts.Add(dataOnt);
                        }
                        else
                        {
                            // below: copy values from dataOnt to ont

                            if (ont.Update(dataOnt))
                            {
                                db.Onts.Attach(ont);
                                db.Entry(ont).State = System.Data.Entity.EntityState.Modified;
                            }
                        }

                        db.SaveChanges();

                        b = true;
                    }
                }
                #endregion
            }
            else if (rowData.Contains("ED-ONT:"))
            {
                #region ED-ONT
                Ia.Ngn.Cl.Model.Ont ont, dataOnt;

                /*
   SUR-5-2 14-11-23 05:22:08
M  0 COMPLD
   / * ED-ONT:SUR-5-2:ONT-1-1-1-2-6::::DESC1=ZAH.2.6: * /
;
                */

                // below: read OntPosition
                match = Regex.Match(rowData, @"ED-ONT:(\w{3}-\d{1,2}-\d{1,2})", RegexOptions.Singleline);

                amsName = match.Groups[1].Value;

                match = Regex.Match(rowData, @"ONT-1-1-(\d{1,2}-\d{1,2}-\d{1,2})::::(.+?):", RegexOptions.Singleline);

                if (match.Success)
                {
                    pon = match.Groups[1].Value;

                    line = match.Groups[2].Value;

                    ontId = Ia.Ngn.Cl.Model.Business.Ont.OntId(amsName, pon);

                    dataOnt = new Ia.Ngn.Cl.Model.Ont();

                    dataOnt.Id = ontId;
                    dataOnt.Description1 = Ia.Cl.Model.Default.Match(line, @"DESC1=(.{1,64})");
                    dataOnt.Description2 = Ia.Cl.Model.Default.Match(line, @"DESC2=(.{1,64})");

                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        ont = (from q in db.Onts where q.Id == dataOnt.Id select q).SingleOrDefault();

                        if (ont == null)
                        {
                            // below: Don't create a new ONT in this ED-ONT function
                            //dataOnt.Created = dataOnt.Updated = dataOnt.Inspected = DateTime.UtcNow.AddHours(3);

                            //db.Onts.Add(dataOnt);
                        }
                        else
                        {
                            // below: copy values from dataOnt to ont

                            if (ont.Update(dataOnt))
                            {
                                db.Onts.Attach(ont);
                                db.Entry(ont).State = System.Data.Entity.EntityState.Modified;
                            }
                        }

                        db.SaveChanges();

                        b = true;
                    }
                }
                #endregion
            }
            else if (rowData.Contains("RTRV-SERVICE-VOIP:"))
            {
                #region RTRV-SERVICE-VOIP

                string ontServiceVoipId;
                int card;
                Ia.Ngn.Cl.Model.OntServiceVoip ontServiceVoip, dataOntServiceVoip;

                // below: information from the definition of "RTRV-SERVICE-VOIP" in "AMS TL1 Commands Reference"

                /*

   SUR-1-1 08-07-09 09:43:13
M  0 COMPLD
   / * RTRV-SERVICE-VOIP:SUR-1-1:VOIP-1-1-1-1-1-1&VOIP-1-1-1-1-10-1&VOIP-1-1 * /
   / * -1-1-2-1&VOIP-1-1-1-1-3-1&VOIP-1-1-1-1-4-1&VOIP-1-1-1-1-5-1&VOIP-1-1- * /
   / * 1-1-6-1&VOIP-1-1-1-1-7-1&VOIP-1-1-1-1-8-1&VOIP-1-1-1-1-9-1 * /
   "VOIP-1-1-1-1-1-1::BWPROFUPID=1,BWPROFUPNM=VOIP,BWPROFDNID=1,
   BWPROFDNNM=VOIP,PQPROFID=1,PQPROFNM=VOIPPQ,AESENABLE=DISABLE,SVLAN=10,
   IPADDRLOC=10.3.144.1,NETMASKLOC=255.255.248.0,DEFROUTER=10.3.151.254,
   IPADDRMGC=10.255.251.5,IPADDRFTP=0.0.0.0,DHCP=DISABLE,PORTMGC=2944,
   VOIPDSCP=24,VOIPMODE=SSH248,CONFIGFILE=kuwait.xml,CLIENTID="",
   CUSTOMERID="",SECRETID=,SECRETK=,IPSECENABLE=DISABLED,CONFMETH=FTPSERVER,
   SPGPROFID=0,SPGPROFNM="",SPGUNAME="",SPGPWD="",SPGREALM="",
   SRCVLANID=0:IS-NR"
   "VOIP-1-1-1-1-10-1::BWPROFUPID=1,BWPROFUPNM=VOIP,BWPROFDNID=1,
   BWPROFDNNM=VOIP,PQPROFID=1,PQPROFNM=VOIPPQ,AESENABLE=DISABLE,SVLAN=10,
   IPADDRLOC=10.3.144.10,NETMASKLOC=255.255.248.0,DEFROUTER=10.3.151.254,
   IPADDRMGC=10.255.251.5,IPADDRFTP=0.0.0.0,DHCP=DISABLE,PORTMGC=2944,
   VOIPDSCP=24,VOIPMODE=SSH248,CONFIGFILE=kuwait.xml,CLIENTID="",
   CUSTOMERID="",SECRETID=,SECRETK=,IPSECENABLE=DISABLED,CONFMETH=FTPSERVER,
   SPGPROFID=0,SPGPROFNM="",SPGUNAME="",SPGPWD="",SPGREALM="",
   SRCVLANID=0:IS-NR"
   "VOIP-1-1-1-1-2-1::BWPROFUPID=1,BWPROFUPNM=VOIP,BWPROFDNID=1,
   BWPROFDNNM=VOIP,PQPROFID=1,PQPROFNM=VOIPPQ,AESENABLE=DISABLE,SVLAN=10,
   IPADDRLOC=10.3.144.2,NETMASKLOC=255.255.248.0,DEFROUTER=10.3.151.254,
   IPADDRMGC=10.255.251.5,IPADDRFTP=0.0.0.0,DHCP=DISABLE,PORTMGC=2944,
   VOIPDSCP=24,VOIPMODE=SSH248,CONFIGFILE=kuwait.xml,CLIENTID="",
   CUSTOMERID="",SECRETID=,SECRETK=,IPSECENABLE=DISABLED,CONFMETH=FTPSERVER,
   SPGPROFID=0,SPGPROFNM="",SPGUNAME="",SPGPWD="",SPGREALM="",
   SRCVLANID=0:IS-NR"

   / * More Output Follows * /
>
                */

                // below: read OntPosition
                match = Regex.Match(rowData, @"RTRV-SERVICE-VOIP:(\w{3}-\d{1,2}-\d{1,2})", RegexOptions.Singleline);

                amsName = match.Groups[1].Value;

                match = Regex.Match(rowData, @"VOIP-1-1-(\d{1,2}-\d{1,2}-\d{1,2})-(\d{1,2})::(.+?):\s*(IS-NR|OOS-AU|OOS-MA|OOS-AUMA)", RegexOptions.Singleline);

                if (match.Success)
                {
                    pon = match.Groups[1].Value;
                    card = int.Parse(match.Groups[2].Value);

                    line = match.Groups[3].Value;

                    ontId = Ia.Ngn.Cl.Model.Business.Ont.OntId(amsName, pon);
                    ontServiceVoipId = Ia.Ngn.Cl.Model.OntServiceVoip.OntServiceVoipId(ontId, card);

                    dataOntServiceVoip = new Ia.Ngn.Cl.Model.OntServiceVoip();

                    dataOntServiceVoip.Id = ontServiceVoipId;

                    dataOntServiceVoip.ConfiguratinFile = Ia.Cl.Model.Default.Match(line, @"CONFIGFILE=(\w{1,}\.xml)");
                    dataOntServiceVoip.Customer = Ia.Cl.Model.Default.Match(line, @"CUSTOMERID=""([^""]{1,62})""");
                    dataOntServiceVoip.FtpIp = Ia.Cl.Model.Default.Match(line, @"IPADDRFTP=(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})");
                    dataOntServiceVoip.Ip = Ia.Cl.Model.Default.Match(line, @"IPADDRLOC=(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})");
                    dataOntServiceVoip.Label = Ia.Cl.Model.Default.Match(line, @"LABEL=""([^""]{1,80})""");
                    dataOntServiceVoip.MgcIp = Ia.Cl.Model.Default.Match(line, @"IPADDRMGC=(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})");
                    dataOntServiceVoip.MgcSecondaryIp = Ia.Cl.Model.Default.Match(line, @"IPADDRMGCSEC=(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})");

                    switch (match.Groups[4].Value)
                    {
                        case "IS-NR": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.IsNr; break;
                        case "OOS-AU": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosAu; break;
                        case "OOS-MA": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosMa; break;
                        case "OOS-AUMA": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosAuma; break;
                        default: state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.Undefined; break;
                    }

                    dataOntServiceVoip.StateId = (int)state;

                    dataOntServiceVoip.Svlan = int.Parse(Ia.Cl.Model.Default.Match(line, @"SVLAN=(\d{1,3})"));

                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        dataOntServiceVoip.Ont = (from q in db.Onts where q.Id == ontId select q).SingleOrDefault();

                        ontServiceVoip = (from q in db.OntServiceVoips where q.Id == dataOntServiceVoip.Id select q).SingleOrDefault();

                        if (ontServiceVoip == null)
                        {
                            dataOntServiceVoip.Created = dataOntServiceVoip.Updated = dataOntServiceVoip.Inspected = DateTime.UtcNow.AddHours(3);

                            db.OntServiceVoips.Add(dataOntServiceVoip);
                        }
                        else
                        {
                            // below: copy values from dataOnt to ont

                            if (ontServiceVoip.Update(dataOntServiceVoip))
                            {
                                db.OntServiceVoips.Attach(ontServiceVoip);
                                db.Entry(ontServiceVoip).State = System.Data.Entity.EntityState.Modified;
                            }
                        }

                        db.SaveChanges();

                        b = true;
                    }
                }
                #endregion
            }
            else if (rowData.Contains("RTRV-ONTPOTS:"))
            {
                #region RTRV-ONTPOTS

                string ontOntPotsId;
                int card, port, svlan;
                Ia.Ngn.Cl.Model.OntOntPots ontOntPots, dataOntOntPots;

                // below: information from the definition of "RTRV-ONTPOTS" in "AMS TL1 Commands Reference"

                /*
                 * ;RTRV-ONTPOTS:SUR-1-1:ONTPOTS-1-1-1-1-1-2-1;

   SUR-1-1 14-03-13 08:15:46
M  0 COMPLD
   /* RTRV-ONTPOTS:SUR-1-1:ONTPOTS-1-1-1-1-1-2-1 * /
   "ONTPOTS-1-1-1-1-1-2-1::VOIPSERV=1,TERMID=td1,POTSDSCP=46,POTSPWR=0,
   CALLHIST=DISABLED,PWROVERRIDE=FALSE,SIPMSGTOTH=0,BRRPKTLOSSTH=0,XJTTRTH=0,
   RXGAIN=0,TXGAIN=0:IS-NR"
;
*/

                // below: read OntPosition
                match = Regex.Match(rowData, @"RTRV-ONTPOTS:(\w{3}-\d{1,2}-\d{1,2})", RegexOptions.Singleline);

                amsName = match.Groups[1].Value;

                match = Regex.Match(rowData, @"ONTPOTS-1-1-(\d{1,2}-\d{1,2}-\d{1,2})-(\d{1,2})-(\d{1,2})::(.+?):\s*(IS-NR|OOS-AU|OOS-MA|OOS-AUMA)", RegexOptions.Singleline);

                if (match.Success)
                {
                    pon = match.Groups[1].Value;
                    card = int.Parse(match.Groups[2].Value);
                    port = int.Parse(match.Groups[3].Value);

                    line = match.Groups[4].Value;

                    ontId = Ia.Ngn.Cl.Model.Business.Ont.OntId(amsName, pon);
                    ontOntPotsId = Ia.Ngn.Cl.Model.OntOntPots.OntOntPotsId(ontId, card, port);

                    dataOntOntPots = new Ia.Ngn.Cl.Model.OntOntPots();

                    dataOntOntPots.Id = ontOntPotsId;

                    dataOntOntPots.Card = card;
                    dataOntOntPots.Customer = Ia.Cl.Model.Default.Match(line, @"CUSTINFO=(\w{1,80})");
                    dataOntOntPots.Port = port;

                    switch (match.Groups[5].Value)
                    {
                        case "IS-NR": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.IsNr; break;
                        case "OOS-AU": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosAu; break;
                        case "OOS-MA": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosMa; break;
                        case "OOS-AUMA": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosAuma; break;
                        default: state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.Undefined; break;
                    }

                    dataOntOntPots.StateId = (int)state;

                    if (int.TryParse(Ia.Cl.Model.Default.Match(line, @"SVLAN=(\d{1,3})"), out svlan)) dataOntOntPots.Svlan = svlan;

                    dataOntOntPots.Termination = Ia.Cl.Model.Default.Match(line, @"TERMID=(\w{1,20})");
                    dataOntOntPots.Tn = Ia.Cl.Model.Default.Match(line, @"TN=(\w{1,16})");
                    dataOntOntPots.VoipClientIp = Ia.Cl.Model.Default.Match(line, @"VOIPCLIENTADDR=(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})");

                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        dataOntOntPots.Ont = (from q in db.Onts where q.Id == ontId select q).SingleOrDefault();

                        ontOntPots = (from q in db.OntOntPotses where q.Id == dataOntOntPots.Id select q).SingleOrDefault();

                        if (ontOntPots == null)
                        {
                            dataOntOntPots.Created = dataOntOntPots.Updated = dataOntOntPots.Inspected = DateTime.UtcNow.AddHours(3);

                            db.OntOntPotses.Add(dataOntOntPots);
                        }
                        else
                        {
                            // below: copy values from dataOnt to ont

                            if (ontOntPots.Update(dataOntOntPots))
                            {
                                db.OntOntPotses.Attach(ontOntPots);
                                db.Entry(ontOntPots).State = System.Data.Entity.EntityState.Modified;
                            }
                        }

                        db.SaveChanges();

                        b = true;
                    }
                }

                #endregion
            }
            else if (rowData.Contains("RTRV-SERVICE-HSI:"))
            {
                #region RTRV-SERVICE-HSI

                string ontServiceHsiId;
                int hsiCard, hsiPort, hsiService;
                Ia.Ngn.Cl.Model.OntServiceHsi ontServiceHsi, dataOntServiceHsi;

                /*
               / * RTRV-SERVICE-HSI:JHR-3-2:ALL * /
               ""HSI-1-1-2-1-19-1-1-1::BWPROFUPID=6,BWPROFUPNM=BW_1024KB_MOC,BWPROFDNID=6,
               BWPROFDNNM=BW_1024KB_MOC,PQPROFID=2,PQPROFNM=PQ_HSI_MOC,AESENABLE=DISABLE,
               LABEL=\""qnet\"",SVLAN=301,CUSTOMERID=\""24530145\"",ETHERTYPE=UNUSED,
               UNISIDEVLAN=0,NETWORKSIDEVLAN=0:OOS-AU""

               ""HSI-1-1-8-2-26-1-1-1::BWPROFUPID=6,BWPROFUPNM=BW_1024KB_MOC,BWPROFDNID=6,
               BWPROFDNNM=BW_1024KB_MOC,PQPROFID=2,PQPROFNM=PQ_HSI_MOC,AESENABLE=DISABLE,
               LABEL=\""qnet\"",SVLAN=301,CUSTOMERID=\""24531014\"",ETHERTYPE=UNUSED,
               UNISIDEVLAN=0,NETWORKSIDEVLAN=0:OOS-AU""

               ""HSI-1-1-10-2-22-1-1-1::BWPROFUPID=6,BWPROFUPNM=BW_1024KB_MOC,BWPROFDNID=6,
               BWPROFDNNM=BW_1024KB_MOC,PQPROFID=2,PQPROFNM=PQ_HSI_MOC,AESENABLE=DISABLE,
               LABEL=\""fast\"",SVLAN=302,CUSTOMERID=\""24530630\"",ETHERTYPE=UNUSED,
               UNISIDEVLAN=0,NETWORKSIDEVLAN=0:OOS-AU""

               ""HSI-1-1-15-2-1-1-1-1::BWPROFUPID=6,BWPROFUPNM=BW_1024KB_MOC,BWPROFDNID=6,
               BWPROFDNNM=BW_1024KB_MOC,PQPROFID=2,PQPROFNM=PQ_HSI_MOC,AESENABLE=DISABLE,
               LABEL=\""Qnet\"",SVLAN=301,CUSTOMERID=\""\"",ETHERTYPE=UNUSED,UNISIDEVLAN=0,
               NETWORKSIDEVLAN=0:IS-NR""

               ""HSI-1-1-16-2-1-1-1-1::BWPROFUPID=7,BWPROFUPNM=BW_8192KB_MOC,BWPROFDNID=7,
               BWPROFDNNM=BW_8192KB_MOC,PQPROFID=2,PQPROFNM=PQ_HSI_MOC,AESENABLE=DISABLE,
               SVLAN=301,CUSTOMERID=\""\"",ETHERTYPE=UNUSED,UNISIDEVLAN=0,NETWORKSIDEVLAN=0:
               OOS-AU""
                 */

                // below: read OntPosition
                match = Regex.Match(rowData, @"RTRV-SERVICE-HSI:(\w{3}-\d{1,2}-\d{1,2})", RegexOptions.Singleline);

                amsName = match.Groups[1].Value;

                match = Regex.Match(rowData, @"HSI-1-1-(\d{1,2}-\d{1,2}-\d{1,2})-(\d{1,2})-(\d{1,2})-(\d{1,2})::(.+?):\s*(IS-NR|OOS-AU|OOS-MA|OOS-AUMA)", RegexOptions.Singleline);

                if (match.Success)
                {
                    pon = match.Groups[1].Value;

                    hsiCard = int.Parse(match.Groups[2].Value);
                    hsiPort = int.Parse(match.Groups[3].Value);
                    hsiService = int.Parse(match.Groups[4].Value);

                    line = match.Groups[5].Value;

                    ontId = Ia.Ngn.Cl.Model.Business.Ont.OntId(amsName, pon);
                    ontServiceHsiId = Ia.Ngn.Cl.Model.OntServiceHsi.OntServiceHsiId(ontId, hsiCard, hsiPort, hsiService);

                    dataOntServiceHsi = new Ia.Ngn.Cl.Model.OntServiceHsi();

                    dataOntServiceHsi.Id = ontServiceHsiId;

                    dataOntServiceHsi.Aes = (Ia.Cl.Model.Default.Match(line, @"AESENABLE=(\w+)") == "ENABLE") ? true : false;

                    dataOntServiceHsi.Card = hsiCard;
                    dataOntServiceHsi.Customer = Ia.Cl.Model.Default.Match(line, @"CUSTOMERID=""([^""]{1,64})""");
                    dataOntServiceHsi.DownstreamBandwidthProfileId = int.Parse(Ia.Cl.Model.Default.Match(line, @"BWPROFDNID=(\d{1,2})"));
                    dataOntServiceHsi.Label = Ia.Cl.Model.Default.Match(line, @"LABEL=""([^""]{1,20})""");

                    dataOntServiceHsi.Port = hsiPort;
                    dataOntServiceHsi.PriorityQueueProfileId = int.Parse(Ia.Cl.Model.Default.Match(line, @"PQPROFID=(\d{1,2})"));
                    dataOntServiceHsi.Service = hsiService;

                    switch (match.Groups[6].Value)
                    {
                        case "IS-NR": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.IsNr; break;
                        case "OOS-AU": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosAu; break;
                        case "OOS-MA": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosMa; break;
                        case "OOS-AUMA": state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosAuma; break;
                        default: state = Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.Undefined; break;
                    }

                    dataOntServiceHsi.StateId = (int)state;

                    dataOntServiceHsi.Svlan = int.Parse(Ia.Cl.Model.Default.Match(line, @"SVLAN=(\d{1,3})"));
                    dataOntServiceHsi.UpstreamBandwidthProfileId = int.Parse(Ia.Cl.Model.Default.Match(line, @"BWPROFUPID=(\d{1,2})"));

                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        dataOntServiceHsi.Ont = (from q in db.Onts where q.Id == ontId select q).SingleOrDefault();

                        ontServiceHsi = (from q in db.OntServiceHsis where q.Id == dataOntServiceHsi.Id select q).SingleOrDefault();

                        if (ontServiceHsi == null)
                        {
                            dataOntServiceHsi.Created = dataOntServiceHsi.Updated = dataOntServiceHsi.Inspected = DateTime.UtcNow.AddHours(3);

                            db.OntServiceHsis.Add(dataOntServiceHsi);
                        }
                        else
                        {
                            // below: copy values from dataOnt to ont

                            if (ontServiceHsi.Update(dataOntServiceHsi))
                            {
                                db.OntServiceHsis.Attach(ontServiceHsi);
                                db.Entry(ontServiceHsi).State = System.Data.Entity.EntityState.Modified;
                            }
                        }

                        db.SaveChanges();

                        b = true;
                    }
                }
                #endregion
            }
            else if (rowData.Contains("RTRV-ALM-PON:"))
            {
                #region RTRV-ALM-PON

                Ia.Ngn.Cl.Model.Event @event;

                // below: important, remove all ''' char from string
                rowData = rowData.Replace(@"'", "");

                // below: read eventTime and amsName
                // QRW-1-1 14-03-31 06:15:20
                match = Regex.Match(rowData, @"(\w{3}-\d{1,2}-\d{1,2}) ((\d{2})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2}))", RegexOptions.Singleline);

                amsName = match.Groups[1].Value;
                eventTime = DateTime.ParseExact(match.Groups[2].Captures[0].Value, "yy-MM-dd HH:mm:ss", null);

                // "PON-1-1-9-2,PON:MN,NEWONT,NSA,,,,: \"SERNUM =ALCLA0A2D17A, SLID =DEFAULT,\""
                matchCollection = Regex.Matches(rowData, @"""(PON-\d{1,2}-\d{1,2}-\d{1,2}-\d{1,2}),PON:MN,NEWONT,NSA,,,,:\s*\r\n\s*""(SERNUM\s*=\s*(ALCL\w{8}), SLID\s*=\s*DEFAULT,)""""", RegexOptions.Singleline);

                if (matchCollection.Count > 0)
                {
                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        foreach (Match m in matchCollection)
                        {
                            if (m.Success)
                            {
                                @event = new Ia.Ngn.Cl.Model.Event();

                                @event.Aid = m.Groups[1].Value;
                                @event.Cause = "NEWONT";
                                @event.Class = "ONT";
                                @event.Detail = m.Groups[2].Value;
                                @event.EventTime = eventTime;
                                @event.NodeTime = null;
                                @event.Number = 0;
                                @event.SeverityEffect = "NSA";
                                @event.Severity = "MN";
                                @event.System = amsName;
                                @event.TypeId = 0;

                                @event.Created = @event.Updated = @event.Inspected = DateTime.UtcNow.AddHours(3);
                                db.Events.Add(@event);

                                b = true;
                            }
                        }

                        db.SaveChanges();
                    }
                }

                #endregion
            }
            else if (rowData.Contains("REPT ALM"))
            {
                #region REPT ALM

                Ia.Ngn.Cl.Model.Event @event;

                // below: important, remove all ''' char from string
                rowData = rowData.Replace(@"'", "");

                // SUR-1-2 08-07-18 11:24:06 * 491 REPT ALM ONT "ONT-1-1-9-2-5:MN,INACT,SA,7-18,11-24-6:\"ONT is inactive\"" ;
                match = Regex.Match(rowData, @"(\w{3}-\d{1,2}-\d{1,2}) ((\d{2})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2}))\r\n(.{1,10})\s+(\d{1,6}) REPT (ALM|EVT|ALM ENV|SW) (\w{1,20})\r\n\s+""(\w{1,20})-(\d{1,2})-(\d{1,2})-([^:]+?):(CR|MJ|MN|CL),(\w{1,20}),(SA|NSA|NR|Ind NR),((\d{1,2})-(\d{1,2})),((\d{1,2})-(\d{1,2})-(\d{1,2})).*:[\r\n\s]*""(.+?)""""\r\n;", RegexOptions.Singleline);

                if (match.Success)
                {
                    @event = new Ia.Ngn.Cl.Model.Event();

                    @event.Aid = match.Groups[13].Captures[0].Value + "-" + match.Groups[14].Captures[0].Value + "-" + match.Groups[15].Captures[0].Value + "-" + match.Groups[16].Captures[0].Value;
                    @event.Cause = match.Groups[18].Captures[0].Value;
                    @event.Class = match.Groups[12].Captures[0].Value;
                    @event.Detail = match.Groups[27].Captures[0].Value.Replace(@"""", "");
                    @event.EventTime = DateTime.ParseExact(match.Groups[2].Captures[0].Value, "yy-MM-dd HH:mm:ss", null);
                    @event.NodeTime = DateTime.ParseExact(match.Groups[3].Captures[0].Value + "-" + match.Groups[21].Captures[0].Value.PadLeft(2, '0') + "-" + match.Groups[22].Captures[0].Value.PadLeft(2, '0') + " " + match.Groups[24].Captures[0].Value.PadLeft(2, '0') + ":" + match.Groups[25].Captures[0].Value.PadLeft(2, '0') + ":" + match.Groups[26].Captures[0].Value.PadLeft(2, '0'), "yy-MM-dd HH:mm:ss", null);
                    @event.Number = int.Parse(match.Groups[10].Captures[0].Value);
                    @event.SeverityEffect = match.Groups[19].Captures[0].Value;
                    @event.Severity = match.Groups[17].Captures[0].Value;
                    @event.System = match.Groups[1].Captures[0].Value;
                    @event.TypeId = 0;

                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        @event.Created = @event.Updated = @event.Inspected = DateTime.UtcNow.AddHours(3);
                        db.Events.Add(@event);
                        db.SaveChanges();

                        b = true;
                    }
                }

                #endregion
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool AddPot(string td)
        {
            bool b;

            b = true;


            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string CommandsToDeleteAndCreateServiceVoipUsingNdd(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt, Ia.Ngn.Cl.Model.Ont ont, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool edServiceVoipIsOos)
        {
            string sa, cardPortOnt, voipServiceState;

            cardPortOnt = nddOnt.Card + "-" + nddOnt.Port + "-" + nddOnt.Number;

            if (edServiceVoipIsOos) voipServiceState = "OOS";
            else voipServiceState = "IS";

            sa = @"
# Delete then create VOIP and associated ONTPOTS: " + nddOnt.Access.Name + @" " + nddOnt.Position;

            if (ont != null)
            {
                if (ont.FamilyTypeId == (int)Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Sfu || ont.FamilyTypeId == (int)Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Undefined)
                {
                    sa += @"

# Delete ONTPOTS
ED-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1&&-4:::::OOS;
DLT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1&&-4::;

# Delete VOIP
ED-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1:::::OOS;
DLT-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1;

# Create VOIP
ENT-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1::::BWPROFUPID=1,BWPROFDNID=1,PQPROFID=1,LABEL=" + nddOnt.Access.Name + @",SVLAN=" + olt.Vlan + @",IPADDRLOC=" + nddOnt.Ip + @",NETMASKLOC=" + olt.Odf.SubnetMask + @",DEFROUTER=" + olt.GatewayIp + @",IPADDRMGC=" + olt.MgcIp + @",IPADDRMGCSEC=" + olt.MgcSecondaryIp + @",VOIPMODE=SSH248,CONFIGFILE=" + olt.Odf.Router.Oams.FirstOrDefault().ConfigFile + @":IS;

# Create ONTPOTS
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1::::VOIPSERV=1,TERMID=td1:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-2::::VOIPSERV=1,TERMID=td2:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-3::::VOIPSERV=1,TERMID=td3:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-4::::VOIPSERV=1,TERMID=td4:IS;

# VOIP service state
ED-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1:::::" + voipServiceState + @";


";
                }
                else if (ont.FamilyTypeId == (int)Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Soho)
                {
                    sa += @"

# Delete ONTPOTS
ED-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1&&-8:::::OOS;
DLT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1&&-8::;

# Delete VOIP
ED-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1:::::OOS;
DLT-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1;

# Create VOIP
ENT-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1::::BWPROFUPID=1,BWPROFDNID=1,PQPROFID=1,LABEL=" + nddOnt.Access.Name + @",SVLAN=" + olt.Vlan + @",IPADDRLOC=" + nddOnt.Ip + @",NETMASKLOC=" + olt.Odf.SubnetMask + @",DEFROUTER=" + olt.GatewayIp + @",IPADDRMGC=" + olt.MgcIp + @",IPADDRMGCSEC=" + olt.MgcSecondaryIp + @",VOIPMODE=SSH248,CONFIGFILE=" + olt.Odf.Router.Oams.FirstOrDefault().ConfigFile + @":IS;

# Create ONTPOTS
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1::::VOIPSERV=1,TERMID=td1:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-2::::VOIPSERV=1,TERMID=td2:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-3::::VOIPSERV=1,TERMID=td3:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-4::::VOIPSERV=1,TERMID=td4:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-5::::VOIPSERV=1,TERMID=td5:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-6::::VOIPSERV=1,TERMID=td6:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-7::::VOIPSERV=1,TERMID=td7:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-8::::VOIPSERV=1,TERMID=td8:IS;

# VOIP service state
ED-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1:::::" + voipServiceState + @";


";
                }
                else if (ont.FamilyTypeId == (int)Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Mdu)
                {
                    sa += @"

# Delete ONTPOTS
ED-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-1&&-8:::::OOS;
ED-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1&&-8:::::OOS;
ED-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-1&&-8:::::OOS;
DLT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-1&&-8::;
DLT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1&&-8::;
DLT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-1&&-8::;

# Delete VOIP
ED-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1:::::OOS;
DLT-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1;

# Create VOIP
ENT-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1::::BWPROFUPID=8,BWPROFDNID=8,PQPROFID=1,AESENABLE=DISABLE,LABEL=" + nddOnt.Access.Name + @",SVLAN=" + olt.Vlan + @",IPADDRLOC=" + nddOnt.Ip + @",NETMASKLOC=" + olt.Odf.SubnetMask + @",DEFROUTER=" + olt.GatewayIp + @",IPADDRMGC=" + olt.MgcIp + @",IPADDRMGCSEC=" + olt.MgcSecondaryIp + @",IPADDRFTP=0.0.0.0,DHCP=DISABLE,PORTMGC=2944,VOIPDSCP=24,VOIPMODE=SSH248,CONFIGFILE=" + olt.Odf.Router.Oams.FirstOrDefault().ConfigFile + @":IS;

# Create ONTPOTS
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-1::::VOIPSERV=1,TERMID=td1:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-2::::VOIPSERV=1,TERMID=td2:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-3::::VOIPSERV=1,TERMID=td3:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-4::::VOIPSERV=1,TERMID=td4:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-5::::VOIPSERV=1,TERMID=td13:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-6::::VOIPSERV=1,TERMID=td14:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-7::::VOIPSERV=1,TERMID=td15:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-8::::VOIPSERV=1,TERMID=td16:IS;

ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1::::VOIPSERV=1,TERMID=td5:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-2::::VOIPSERV=1,TERMID=td6:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-3::::VOIPSERV=1,TERMID=td7:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-4::::VOIPSERV=1,TERMID=td8:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-5::::VOIPSERV=1,TERMID=td17:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-6::::VOIPSERV=1,TERMID=td18:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-7::::VOIPSERV=1,TERMID=td19:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-8::::VOIPSERV=1,TERMID=td20:IS;

ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-1::::VOIPSERV=1,TERMID=td9:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-2::::VOIPSERV=1,TERMID=td10:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-3::::VOIPSERV=1,TERMID=td11:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-4::::VOIPSERV=1,TERMID=td12:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-5::::VOIPSERV=1,TERMID=td21:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-6::::VOIPSERV=1,TERMID=td22:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-7::::VOIPSERV=1,TERMID=td23:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-8::::VOIPSERV=1,TERMID=td24:IS;

# VOIP service state
ED-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1:::::" + voipServiceState + @";


";
                }
                else
                {
                    throw new Exception("Unknown familyType") { };
                }
            }
            else
            {

                sa = @"
# Delete then create VOIP and associated ONTPOTS: " + nddOnt.Access.Name + @" " + nddOnt.Position;

                sa += @"
# ONT does not have an associated Access to it.";

                sa += @"

# Delete ONTPOTS
ED-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1&&-4:::::OOS;
DLT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1&&-4::;

# Delete VOIP
ED-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1:::::OOS;
DLT-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1;

# Create VOIP
ENT-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1::::BWPROFUPID=1,BWPROFDNID=1,PQPROFID=1,LABEL=" + nddOnt.Access.Name + @",SVLAN=" + olt.Vlan + @",IPADDRLOC=" + nddOnt.Ip + @",NETMASKLOC=" + olt.Odf.SubnetMask + @",DEFROUTER=" + olt.GatewayIp + @",IPADDRMGC=" + olt.MgcIp + @",IPADDRMGCSEC=" + olt.MgcSecondaryIp + @",VOIPMODE=SSH248,CONFIGFILE=" + olt.Odf.Router.Oams.FirstOrDefault().ConfigFile + @":IS;

# Create ONTPOTS
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1::::VOIPSERV=1,TERMID=td1:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-2::::VOIPSERV=1,TERMID=td2:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-3::::VOIPSERV=1,TERMID=td3:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-4::::VOIPSERV=1,TERMID=td4:IS;

# VOIP service state
ED-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1:::::" + voipServiceState + @";


";
            }

            return sa;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string CommandsToCreateOntPots(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt, Ia.Ngn.Cl.Model.Ont ont, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool edServiceVoipIsOos)
        {
            string sa, cardPortOnt, voipServiceState;

            cardPortOnt = nddOnt.Card + "-" + nddOnt.Port + "-" + nddOnt.Number;

            if (edServiceVoipIsOos) voipServiceState = "OOS";
            else voipServiceState = "IS";

            sa = @"
# Create ONTPOTS: " + ont.Access.Name + @" " + ont.Access.Position;

            if (ont.FamilyTypeId == (int)Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Sfu)
            {
                sa += @"

# Create ONTPOTS
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1::::VOIPSERV=1,TERMID=td1:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-2::::VOIPSERV=1,TERMID=td2:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-3::::VOIPSERV=1,TERMID=td3:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-4::::VOIPSERV=1,TERMID=td4:IS;

# VOIP service state
ED-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1:::::" + voipServiceState + @";


";
            }
            else if (ont.FamilyTypeId == (int)Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Soho)
            {
                sa += @"

# Create ONTPOTS
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1::::VOIPSERV=1,TERMID=td1:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-2::::VOIPSERV=1,TERMID=td2:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-3::::VOIPSERV=1,TERMID=td3:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-4::::VOIPSERV=1,TERMID=td4:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-5::::VOIPSERV=1,TERMID=td5:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-6::::VOIPSERV=1,TERMID=td6:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-7::::VOIPSERV=1,TERMID=td7:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-8::::VOIPSERV=1,TERMID=td8:IS;

# VOIP service state
ED-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1:::::" + voipServiceState + @";


";
            }
            else if (ont.FamilyTypeId == (int)Ia.Ngn.Cl.Model.Business.Ont.FamilyType.Mdu)
            {
                sa += @"

# Create ONTPOTS
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-1::::VOIPSERV=1,TERMID=td1:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-2::::VOIPSERV=1,TERMID=td2:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-3::::VOIPSERV=1,TERMID=td3:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-4::::VOIPSERV=1,TERMID=td4:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-5::::VOIPSERV=1,TERMID=td13:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-6::::VOIPSERV=1,TERMID=td14:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-7::::VOIPSERV=1,TERMID=td15:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-1-8::::VOIPSERV=1,TERMID=td16:IS;

ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1::::VOIPSERV=1,TERMID=td5:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-2::::VOIPSERV=1,TERMID=td6:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-3::::VOIPSERV=1,TERMID=td7:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-4::::VOIPSERV=1,TERMID=td8:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-5::::VOIPSERV=1,TERMID=td17:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-6::::VOIPSERV=1,TERMID=td18:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-7::::VOIPSERV=1,TERMID=td19:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-8::::VOIPSERV=1,TERMID=td20:IS;

ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-1::::VOIPSERV=1,TERMID=td9:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-2::::VOIPSERV=1,TERMID=td10:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-3::::VOIPSERV=1,TERMID=td11:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-4::::VOIPSERV=1,TERMID=td12:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-5::::VOIPSERV=1,TERMID=td21:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-6::::VOIPSERV=1,TERMID=td22:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-7::::VOIPSERV=1,TERMID=td23:IS;
ENT-ONTPOTS:" + olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-3-8::::VOIPSERV=1,TERMID=td24:IS;

# VOIP service state
ED-SERVICE-VOIP:" + olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1:::::" + voipServiceState + @";


";
            }
            else
            {
                sa += @"

# Unknown FamilyType


";
            }

            return sa;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string CommandsToPreprovisionOntWithinOlt(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool edServiceVoipIsOos)
        {
            string sa, cardPortOnt, voipServiceState;

            if (edServiceVoipIsOos) voipServiceState = "OOS";
            else voipServiceState = "IS";

            cardPortOnt = nddOnt.Card + "-" + nddOnt.Port + "-" + nddOnt.Number;

            sa = @"
# Preprovision an empty ONT on the network: " + nddOnt.Access.Name + @" " + nddOnt.Position + " (" + nddOnt.Ip + @");
ENT-ONT:" + nddOnt.Pon.Olt.AmsName + @":ONT-1-1-" + cardPortOnt + @"::::SWVERPLND=" + Ia.Ngn.Cl.Model.Data.Nokia.Ams.PlannedSoftware + @",DESC1=" + nddOnt.Access.Name + @",DESC2=""NULL"":OOS;

# Provision the VOIP service;
ENT-ONTCARD:" + nddOnt.Pon.Olt.AmsName + @":ONTCARD-1-1-" + cardPortOnt + @"-2:::POTS::IS;
ENT-ONTCARD:" + nddOnt.Pon.Olt.AmsName + @":ONTCARD-1-1-" + cardPortOnt + @"-1:::10_100BASET::IS;
ENT-ONTENET:" + nddOnt.Pon.Olt.AmsName + @":ONTENET-1-1-" + cardPortOnt + @"-1-1::::SESSPROFID=4,MAXMACNUM=4:IS;
ENT-ONTENET:" + nddOnt.Pon.Olt.AmsName + @":ONTENET-1-1-" + cardPortOnt + @"-1-2::::SESSPROFID=4,MAXMACNUM=4:IS;
ENT-SERVICE-VOIP:" + nddOnt.Pon.Olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1::::BWPROFUPID=1,BWPROFDNID=1,PQPROFID=1,SVLAN=" + nddOnt.Pon.Olt.Vlan + @",IPADDRLOC=" + nddOnt.Ip + @",NETMASKLOC=" + nddOnt.Pon.Olt.Odf.SubnetMask + @",DEFROUTER=" + nddOnt.Pon.Olt.GatewayIp + @",IPADDRMGC=" + nddOnt.Pon.Olt.Odf.PrimaryIp + @",IPADDRMGCSEC=" + nddOnt.Pon.Olt.Odf.SecondaryIp + @",VOIPMODE=SSH248,CONFIGFILE=kuwait.xml:IS;
ENT-ONTPOTS:" + nddOnt.Pon.Olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-1::::VOIPSERV=1,TERMID=td1:IS;
ENT-ONTPOTS:" + nddOnt.Pon.Olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-2::::VOIPSERV=1,TERMID=td2:IS;
ENT-ONTPOTS:" + nddOnt.Pon.Olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-3::::VOIPSERV=1,TERMID=td3:IS;
ENT-ONTPOTS:" + nddOnt.Pon.Olt.AmsName + @":ONTPOTS-1-1-" + cardPortOnt + @"-2-4::::VOIPSERV=1,TERMID=td4:IS;

# Enter PON ARP;
ENT-PONARPENT:" + nddOnt.Pon.Olt.AmsName + @":PONVLAN-" + nddOnt.Pon.Olt.Vlan + @":::" + nddOnt.Ip + @",VOIPBRGPORT-1-1-" + cardPortOnt + @"-1:CVLAN=0;

# VOIP service state
ED-SERVICE-VOIP:" + nddOnt.Pon.Olt.AmsName + @":VOIP-1-1-" + cardPortOnt + @"-1:::::" + voipServiceState + @";

";

            return sa;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string CommandsToPreprovisionOntWithinOltUsingConfigureCommand(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool edServiceVoipIsOos)
        {
            string sa, rackSubCardPortOnt, voipServiceState;

            if (edServiceVoipIsOos) voipServiceState = "OOS";
            else voipServiceState = "IS";

            rackSubCardPortOnt = nddOnt.Rack + "/" + nddOnt.Sub + "/" + nddOnt.Card + "/" + nddOnt.Port + "/" + nddOnt.Number;

            sa = @"
configure equipment ont slot " + rackSubCardPortOnt + @"/1 planned-card-type 10_100base plndnumdataports 2 plndnumvoiceports 0
configure equipment ont slot " + rackSubCardPortOnt + @"/2 planned-card-type pots plndnumdataports 0 plndnumvoiceports 4
configure qos interface ont:" + rackSubCardPortOnt + @" us-num-queue 8
configure qos interface " + rackSubCardPortOnt + @"/voip upstream-queue 5 bandwidth-profile name:VOICE bandwidth-sharing ont-sharing 
configure bridge port " + rackSubCardPortOnt + @"/voip max-unicast-mac 8    
configure bridge port " + rackSubCardPortOnt + @"/voip vlan-id " + nddOnt.Pon.Olt.Vlan + @"
configure bridge port " + rackSubCardPortOnt + @"/voip pvid " + nddOnt.Pon.Olt.Vlan + @"
configure voice ont service " + rackSubCardPortOnt + @"/1 voip-mode softswitch-h248 mgc-ip-addr " + nddOnt.Pon.Olt.MgcIp + @" sec-mgc-ip-addr " + nddOnt.Pon.Olt.MgcSecondaryIp + @" conf-file-name " + nddOnt.Pon.Olt.Odf.Router.Oams.First().ConfigFile + @" ip-address " + nddOnt.Ip + @" net-mask 255.255.248.0 default-router " + nddOnt.Pon.Olt.GatewayIp + @" vlan " + nddOnt.Pon.Olt.Vlan + @"

";

            return sa;
        }

        ////////////////////////////////////////////////////////////////////////////    
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
