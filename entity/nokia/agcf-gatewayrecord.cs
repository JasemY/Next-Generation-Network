using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model.Nokia
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// AGCF Gateway Record Entity Framework class for Next Generation Network (NGN) entity model.
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

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        /// NgcfAgcfGatewayRecordAid (opt for ent,req for ed) LCP Format as GatewayTableId`GatewayRecordId. GatewayRecordId (2048-4047). The value of GatewayTableId should be same as 'TableId'. The value of GatewayRecordId should be same as 'GwId' LCP-CTS R8.0 From CTS R8.0,GatewayRecordId support(2048-18047) LCP-CTS R8.1.1 From CTS R8.1.1,GatewayRecordId support(2048-502047) LCP-CTS R9.1 From CTS R9.1, GatewayRecordId support(2048-5002047)
        /// </summary>
        public string NgcfAgcfGatewayRecordAid { get; set; }
        
        /// <summary>
        /// TableId (req for ent,req for ed) LCP Same value as the aid of Agcf Gateway Table, Integer (1-99)
        /// </summary>
        public int TableId { get; set; }
        
        /// <summary>
        /// GwId (req for ent,req for ed). LCP Integer (2048-4047). cannot be modified. Default 2048. LCP-CTS R8.0 Integer (2048-18047). Default 2048 LCP-CTS R8.1.1 Integer (2048-502047) Default 2048 LCP-CTS R9.1 Integer (2048-5002047) Default 2048
        /// </summary>
        public int GwId { get; set; }
        
        /// <summary>
        /// AgcfSipIaPort (req for ent,opt for ed). LCP String (1-71)
        /// </summary>
        public string AgcfSipIaPort { get; set; }
        
        /// <summary>
        /// ConnectionType (opt for ent,opt for ed). LCP Valid value: UDP/IP TCP/IP Default UDP/IP
        /// </summary>
        public bool ConnectionType { get; set; }
        
        /// <summary>
        /// ContextAudits (opt for ent,opt for ed). LCP Valid value: None Audit Value Based Default Audit Value Based
        /// </summary>
        public bool ContextAudits { get; set; }
        
        /// <summary>
        /// GwDigitMapId (req for ent,opt for ed). LCP Integer (1-5)
        /// </summary>
        public int GwDigitMapId { get; set; }
        
        /// <summary>
        /// EnableChannelStatusAudits (opt for ent,opt for ed). LCP Boolean (true or false) D. LCP Command. 230 Default false LCP-CTS R8.1.1 Deprecated and will be replaced by "Channel Status Audit/Arming Options".
        /// </summary>
        public bool EnableChannelStatusAudits { get; set; }
        
        /// <summary>
        /// GwUserId (opt for ent,opt for ed). LCP String (1-28). Supported since CTS R6.1, could not be empty after CTS R6.1,, cannot be modified 
        /// </summary>
        public string GwUserId { get; set; }
        
        /// <summary>
        /// GwDomain (req for ent,opt for ed). LCP String (1-255), cannot be modified
        /// </summary>
        public string GwDomain { get; set; }
        
        /// <summary>
        /// GwName (req for ent,opt for ed). LCP String (1-32)
        /// </summary>
        public string GwName { get; set; }
        
        /// <summary>
        /// GwPrivId (req for ent,opt for ed). LCP String(1-100) LCP-CTS R6.1 Deprecated and must be empty since CTS R6.1 (R17.18).
        /// </summary>
        public string GwPrivId { get; set; }
        
        /// <summary>
        /// IP1 (req for ent,opt for ed). LCP Format as: ((([0-1]?[0-9]?[0-9])|(2[0-4][0-9])|(25[0-5]))\.){3}((25[0-5])|(2[0-4][0-9])|([0-1]?[0-9]?[0-9])) Default 0.0.0.0
        /// </summary>
        public string IP1 { get; set; }
        
        /// <summary>
        /// IP2 (opt for ent,opt for ed). LCP Format as: ((([0-1]?[0-9]?[0-9])|(2[0-4][0-9])|(25[0-5]))\.){3}((25[0-5])|(2[0-4][0-9])|([0-1]?[0-9]?[0-9])) Default 0.0.0.0
        /// </summary>
        public string IP2 { get; set; }
        
        /// <summary>
        /// LocalTermTypePrefix (opt for ent,opt for ed). LCP String (1-32) Default AL LCP-CTS R6.2 Deprecated in CTS 6.2 and later:
        /// </summary>
        public string LocalTermTypePrefix { get; set; }
        
        /// <summary>
        /// LocalTermTypeAnalogPrefix (opt for ent,opt for ed). LCP-CTS R6.2 String (1-32) Default: AL replaces LocalTermTypePrefix
        /// </summary>
        public string LocalTermTypeAnalogPrefix { get; set; }
        
        /// <summary>
        /// IsLocalTermTypeTDMAnalog (opt for ent,opt for ed). LCP Boolean (true or false) Default true
        /// </summary>
        public bool IsLocalTermTypeTDMAnalog { get; set; }
        
        /// <summary>
        /// NetTermTypePrefix (opt for ent,opt for ed). LCP String (1-32) Default RTP/
        /// </summary>
        public string NetTermTypePrefix { get; set; }
        
        /// <summary>
        /// IsNetTermTypeRTPUDP (opt for ent,opt for ed). LCP Boolean (true or false) default true
        /// </summary>
        public bool IsNetTermTypeRTPUDP { get; set; }
        
        /// <summary>
        /// PhysicalTermIdScheme (opt for ent,opt for ed). LCP-CTS R6.0 Valid value:CTS 6.0 and beyond: a. al/dslam/rack/shelf/slot/port b. al/rack/shelf/slot/port c. al/shelf/slot/port Default al/dslam/rack/shelf/slot/port LCP-CTS R6.1 CTS 6.1 addition d. Flat Term ID LCP-CTS R6.2 Deprecated values in CTS 6.2 and later: al/dslam/rack/shelf/slot/port al/rack/shelf/slot/port al/shelf/slot/port New values for CTS 6.2 and later: prefix/dslam/rack/shelf/slot/port[/channel] D. LCP Command. 231 prefix/rack/shelf/slot/port[/channel] prefix/shelf/slot/port[/channel] LCP Boolean (true or false) Default false
        /// </summary>
        public string PhysicalTermIdScheme { get; set; }
        
        /// <summary>
        /// SendCompactMessages (opt for ent,opt for ed). LCP-CTS R8.1.1 default value changed to true
        /// </summary>
        public bool SendCompactMessages { get; set; }
        
        /// <summary>
        /// SendEphemeralPrefix (opt for ent,opt for ed). LCP Boolean (true or false) Default false
        /// </summary>
        public bool SendEphemeralPrefix { get; set; }
        
        /// <summary>
        /// UdpPort (opt for ent,opt for ed). LCP default value:2944 LCP-CTS R8.1.1 value range: 2944-20287 This field specifies the UDP/IP port used by a gateway. From R24.21, the default value depends on the "Gateway Type" field. - Multiple RGWs/iAGs can share port 2944, so the default port for RGWs/iADs is 2944. - For AGWs, non-2944 port number must be unique, so the default should be a free port, the following formala will most likely identify a free port: ((Gateway ID - 2048)%17242) + 2945. 
        /// </summary>
        public int UdpPort { get; set; }
        
        /// <summary>
        /// GwVariantId (req for ent,opt for ed). LCP Integer (1-17) LCP-CTS R8.0 Integer (1-64)
        /// </summary>
        public int GwVariantId { get; set; }
        
        /// <summary>
        /// AreaId (opt for ent,opt for ed). LCP-ISC R21.1.1 Integer (0-100) Default: 0 - means the Area ID is not provisioned Used by the iAGCF to populate the X-ALU-Area-ID parameter sent to the S-CSCF during gateway registrations.
        /// </summary>
        public int AreaId { get; set; }
        
        /// <summary>
        /// ComfortNoise (opt for ent,opt for ed). LCP-ISC R20.0 Boolean (true or false) Default:false
        /// </summary>
        public bool ComfortNoise { get; set; }
        
        /// <summary>
        /// Dtmf (opt for ent,opt for ed). LCP-ISC R20.0 Boolean (true or false) Default:false
        /// </summary>
        public bool Dtmf { get; set; }
        
        /// <summary>
        /// FaxEvents (opt for ent,opt for ed). LCP-ISC R20.0 Boolean (true or false) Default:false
        /// </summary>
        public bool FaxEvents { get; set; }
        
        /// <summary>
        /// ModemEvents (opt for ent,opt for ed). LCP-ISC R20.0 Boolean (true or false) Default:false
        /// </summary>
        public bool ModemEvents { get; set; }
        
        /// <summary>
        /// TextTelephonyEvents (opt for ent,opt for ed). LCP-ISC R20.0 Boolean (true or false) Default:false
        /// </summary>
        public bool TextTelephonyEvents { get; set; }
        
        /// <summary>
        /// AllCodecDataStr (opt for ent,opt for ed). LCP-CTS R6.2 Format as Type|CodecAlgType|PayloadSize|SilenceSupression Type: Audio CodecAlgType: valid value: G.711 - aLaw, G.711 - uLaw. default G.711 - uLaw D. LCP Command. 232 PayloadSize: Integer 1-6 or a multiple of 5 less than or equal to 80. Default 20 SilenceSupression: Boolean (true or false) default false LCP-CTS R7.0 CodecAlgType: valid value: G.711 - aLaw, G.711 - uLaw, G.723, G.729. LCP-CTS R7.0.1 PayloadSize: The field will actually contain "Packetization Time" in milliseconds. Valid range depends on CodecAlgType, as follows: G.711 - aLaw: 10 to 30 in increments of 10 G.711 - uLaw: 10 to 30 in increments of 10 G.723: 30 to 60 in increments of 30 G.729: 10 to 60 in increments of 10 Default for G.723 is 30ms, and for the other 3 types - 20ms.
        /// </summary>
        public string AllCodecDataStr { get; set; }
        
        /// <summary>
        /// AgwIuaIpAddress (opt for ent,opt for ed). LCP-CTS R6.2 String (0-45)[a-fA-F0-9:.]*
        /// </summary>
        public string AgwIuaIpAddress { get; set; }
        
        /// <summary>
        /// AgwIuaSctpPort (opt for ent,opt for ed). LCP-CTS R6.2 Integer 1-65535 Default:9900
        /// </summary>
        public int AgwIuaSctpPort { get; set; }
        
        /// <summary>
        /// AgcfLocalSctpPort (opt for ent,opt for ed). LCP-CTS R6.2 Integer 1025-65535 Default: GatewayId - 2048 + 9900
        /// </summary>
        public int AgcfLocalSctpPort { get; set; }
        
        /// <summary>
        /// SctpProfile (opt for ent,opt for ed). LCP-CTS R6.2 Integer 0-15 Default:0
        /// </summary>
        public int SctpProfile { get; set; }

        /// <summary>
        /// IuaIIDMapScheme (opt for ent,opt for ed). LCP-CTS R6.2 Range: "", rack/shelf/slot/port/channel, Flat Term ID Default: rack/shelf/slot/port/channel 
        /// </summary>
        public string IuaIIDMapScheme { get; set; }
        
        /// <summary>
        /// LocalTermTypeIsdnPrefix (opt for ent,opt for ed). LCP-CTS R6.2 This field and AgwIuaIpAddress, AgwIuaIpAddress, AgwIuaSctpPort, AgcfLocalSctpPort , SctpProfile, IuaIIDMapScheme, AuditAllActiveIsdnCalls are for ISDN, if one of them is provided with a non null value, then all ISDN fields must be provided, and vice versa. Default values only apply when AgwIuaIpAddress is not empty. string (1-10) Default: BA
        /// </summary>
        public string LocalTermTypeIsdnPrefix { get; set; }
        
        /// <summary>
        /// AuditAllActiveIsdnCalls (opt for ent,opt for ed). LCP-CTS R6.2 Boolean (true or false) Default:false
        /// </summary>
        public bool AuditAllActiveIsdnCalls { get; set; }
        
        /// <summary>
        /// MateSite (opt for ent,opt for ed). LCP-CTS R6.1 A value of 0 indicates that the GW is not geo-redundant. Otherwise, the GW is considered Geo-Redundant and all the HW data including HSS data will be provisioned on primary and protection switches. The Id given here should D. LCP Command. 233 be that of the switch (2-8) as seen under the alternate switch's protection site list. 
        /// </summary>
        public int MateSite { get; set; }
        
        /// <summary>
        /// MateExternalIPAddr (opt for ent,opt for ed). LCP-CTS R6.1 Default IPv4 external address of the AGCF card on the alternate switch. So, on the Primary, it will be the Protection Switch's AGCF Card IP address. And on the Protection, it will be the Primary Switch's AGCF Card IP address.
        /// </summary>
        public string MateExternalIPAddr { get; set; }
        
        /// <summary>
        /// IsPrimary (opt for ent,opt for ed). LCP-CTS R6.1 True indicates to the switch that it is designated as the primary in the geo-redundancy mode. False otherwise.
        /// </summary>
        public bool IsPrimary { get; set; }
        
        /// <summary>
        /// AddtionalDigitMapList (opt for ent,opt for ed). LCP-CTS R7.1 Digit Map Table id list. Format as tableid1^tableid2^...
        /// </summary>
        public string AddtionalDigitMapList { get; set; }
        
        /// <summary>
        /// AuthTimer (opt for ent,opt for ed). LCP-CTS R8.1.1 Integer (1-60) default:10
        /// </summary>
        public int AuthTimer { get; set; }
        
        /// <summary>
        /// LocalSecKey (opt for ent,opt for ed,na for rtrv). LCP-CTS R8.1.1 Integer (2-65535) default:888
        /// </summary>
        public int LocalSecKey { get; set; }
        
        /// <summary>
        /// SharedBasNumber (opt for ent,opt for ed). LCP-CTS R8.1.1 Ulong (2-4294967295) default:101
        /// </summary>
        public long SharedBasNumber { get; set; }
        
        /// <summary>
        /// SharedPriNumber (opt for ent,opt for ed). LCP-CTS R8.1.1 Integer (2-65535) default:523
        /// </summary>
        public int SharedPriNumber { get; set; }
        
        /// <summary>
        /// SharedSecKey (opt for ent,opt for ed,na for rtrv). LCP-CTS R8.1.1 Hex_Digits (0-32) default:null
        /// </summary>
        public string SharedSecKey { get; set; }
        
        /// <summary>
        /// MgID (opt for ent,opt for ed). LCP-CTS R8.1.1 Hex_Digits (0-32) default:null
        /// </summary>
        public string MgID { get; set; }
        
        /// <summary>
        /// EnnableMD5DigAuteh (opt for ent,opt for ed). LCP-CTS R8.1.1 Boolean (true or false) Default false
        /// </summary>
        public bool EnnableMD5DigAuteh { get; set; }
        
        /// <summary>
        /// ChannelAudits (opt for ent,opt for ed). LCP-CTS R8.1.1 0-Audit disabled 1-Arming Modify+AuditValue in one Transaction 2-Arming Modify and AuditValue done one after another sequentially. default value:Arming Modify+AuditValue in one Transaction
        /// </summary>
        public int ChannelAudits { get; set; }
        
        /// <summary>
        /// GroupId (opt for ent,opt for ed). LCP-CTS R8.1.1 value range:0-500 0 for AGWs; For RGW/iAD, if the GW ID is k, the default value of GW group ID is 1 + (k - 2048)/1000 LCP-CTS R9.1 value range:0-500 0 for AGWs; For RGW/iAD, if the GW ID is k, the default value of GW group ID is 1 + (k - 2048)/10000 
        /// </summary>
        public int GroupId { get; set; }
        
        /// <summary>
        /// AgcfGateWayType (opt for ent,opt for ed). LCP-CTS R8.1.1 RGWs/iADs and AGWs
        /// <summary/>
        public int AgcfGateWayType { get; set; }

        /// <summary/>
        public virtual ICollection<AgcfEndpoint> AgcfEndpoints { get; set; }

        public DateTime Created { get; set; }
        /// <summary/>
        public DateTime Updated { get; set; }
        /// <summary/>
        public DateTime Inspected { get; set; }
        /// <summary/>
        public System.Guid UserId { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int AgcfGatewayRecordId(int tableId, int gwId)
        {
            int id;
            string s;

            s = tableId.ToString() + gwId.ToString().PadLeft(6,'0');

            id = int.Parse(s);

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(AgcfGatewayRecord agcfGatewayRecord, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfGatewayRecord.Created = agcfGatewayRecord.Updated = agcfGatewayRecord.Inspected = DateTime.UtcNow.AddHours(3);

                db.AgcfGatewayRecords.Add(agcfGatewayRecord);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static AgcfGatewayRecord Read(int id)
        {
            AgcfGatewayRecord agcfGatewayRecord;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfGatewayRecord = (from q in db.AgcfGatewayRecords where q.Id == id select q).SingleOrDefault();
            }

            return agcfGatewayRecord;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static AgcfGatewayRecord ReadbyIp(string ip)
        {
            AgcfGatewayRecord agcfGatewayRecord;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfGatewayRecord = (from q in db.AgcfGatewayRecords where q.IP1 == ip select q).SingleOrDefault();
            }

            return agcfGatewayRecord;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<AgcfGatewayRecord> ReadList()
        {
            List<AgcfGatewayRecord> agcfGatewayRecordList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfGatewayRecordList = (from q in db.AgcfGatewayRecords select q).ToList();
            }

            return agcfGatewayRecordList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(AgcfGatewayRecord agcfGatewayRecord, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfGatewayRecord = (from q in db.AgcfGatewayRecords where q.Id == agcfGatewayRecord.Id select q).SingleOrDefault();

                agcfGatewayRecord.Updated = DateTime.UtcNow.AddHours(3);

                db.AgcfGatewayRecords.Attach(agcfGatewayRecord);

                db.Entry(agcfGatewayRecord).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Delete(int id, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                var v = (from q in db.AgcfGatewayRecords where q.Id == id select q).FirstOrDefault();

                db.AgcfGatewayRecords.Remove(v);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Equal(AgcfGatewayRecord b)
        {
            // below: this will not check the Id, Created, Updated, or Inspected fields
            bool areEqual;

            //if (this.BatteryBackupAvailable != b.BatteryBackupAvailable) areEqual = false;
            //else areEqual = true;

            areEqual = false;

            return areEqual;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Update(AgcfGatewayRecord updatedAgcfGatewayRecord)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.NgcfAgcfGatewayRecordAid != updatedAgcfGatewayRecord.NgcfAgcfGatewayRecordAid) { this.NgcfAgcfGatewayRecordAid = updatedAgcfGatewayRecord.NgcfAgcfGatewayRecordAid; updated = true; }
            if (this.TableId != updatedAgcfGatewayRecord.TableId) { this.TableId = updatedAgcfGatewayRecord.TableId; updated = true; }
            if (this.GwId != updatedAgcfGatewayRecord.GwId) { this.GwId = updatedAgcfGatewayRecord.GwId; updated = true; }
            if (this.AgcfSipIaPort != updatedAgcfGatewayRecord.AgcfSipIaPort) { this.AgcfSipIaPort = updatedAgcfGatewayRecord.AgcfSipIaPort; updated = true; }
            if (this.ConnectionType != updatedAgcfGatewayRecord.ConnectionType) { this.ConnectionType = updatedAgcfGatewayRecord.ConnectionType; updated = true; }
            if (this.ContextAudits != updatedAgcfGatewayRecord.ContextAudits) { this.ContextAudits = updatedAgcfGatewayRecord.ContextAudits; updated = true; }
            if (this.GwDigitMapId != updatedAgcfGatewayRecord.GwDigitMapId) { this.GwDigitMapId = updatedAgcfGatewayRecord.GwDigitMapId; updated = true; }
            if (this.EnableChannelStatusAudits != updatedAgcfGatewayRecord.EnableChannelStatusAudits) { this.EnableChannelStatusAudits = updatedAgcfGatewayRecord.EnableChannelStatusAudits; updated = true; }
            if (this.GwUserId != updatedAgcfGatewayRecord.GwUserId) { this.GwUserId = updatedAgcfGatewayRecord.GwUserId; updated = true; }
            if (this.GwDomain != updatedAgcfGatewayRecord.GwDomain) { this.GwDomain = updatedAgcfGatewayRecord.GwDomain; updated = true; }
            if (this.GwName != updatedAgcfGatewayRecord.GwName) { this.GwName = updatedAgcfGatewayRecord.GwName; updated = true; }
            if (this.GwPrivId != updatedAgcfGatewayRecord.GwPrivId) { this.GwPrivId = updatedAgcfGatewayRecord.GwPrivId; updated = true; }
            if (this.IP1 != updatedAgcfGatewayRecord.IP1) { this.IP1 = updatedAgcfGatewayRecord.IP1; updated = true; }
            if (this.IP2 != updatedAgcfGatewayRecord.IP2) { this.IP2 = updatedAgcfGatewayRecord.IP2; updated = true; }
            if (this.LocalTermTypePrefix != updatedAgcfGatewayRecord.LocalTermTypePrefix) { this.LocalTermTypePrefix = updatedAgcfGatewayRecord.LocalTermTypePrefix; updated = true; }
            if (this.LocalTermTypeAnalogPrefix != updatedAgcfGatewayRecord.LocalTermTypeAnalogPrefix) { this.LocalTermTypeAnalogPrefix = updatedAgcfGatewayRecord.LocalTermTypeAnalogPrefix; updated = true; }
            if (this.IsLocalTermTypeTDMAnalog != updatedAgcfGatewayRecord.IsLocalTermTypeTDMAnalog) { this.IsLocalTermTypeTDMAnalog = updatedAgcfGatewayRecord.IsLocalTermTypeTDMAnalog; updated = true; }
            if (this.NetTermTypePrefix != updatedAgcfGatewayRecord.NetTermTypePrefix) { this.NetTermTypePrefix = updatedAgcfGatewayRecord.NetTermTypePrefix; updated = true; }
            if (this.IsNetTermTypeRTPUDP != updatedAgcfGatewayRecord.IsNetTermTypeRTPUDP) { this.IsNetTermTypeRTPUDP = updatedAgcfGatewayRecord.IsNetTermTypeRTPUDP; updated = true; }
            if (this.PhysicalTermIdScheme != updatedAgcfGatewayRecord.PhysicalTermIdScheme) { this.PhysicalTermIdScheme = updatedAgcfGatewayRecord.PhysicalTermIdScheme; updated = true; }
            if (this.SendCompactMessages != updatedAgcfGatewayRecord.SendCompactMessages) { this.SendCompactMessages = updatedAgcfGatewayRecord.SendCompactMessages; updated = true; }
            if (this.SendEphemeralPrefix != updatedAgcfGatewayRecord.SendEphemeralPrefix) { this.SendEphemeralPrefix = updatedAgcfGatewayRecord.SendEphemeralPrefix; updated = true; }
            if (this.UdpPort != updatedAgcfGatewayRecord.UdpPort) { this.UdpPort = updatedAgcfGatewayRecord.UdpPort; updated = true; }
            if (this.GwVariantId != updatedAgcfGatewayRecord.GwVariantId) { this.GwVariantId = updatedAgcfGatewayRecord.GwVariantId; updated = true; }
            if (this.AreaId != updatedAgcfGatewayRecord.AreaId) { this.AreaId = updatedAgcfGatewayRecord.AreaId; updated = true; }
            if (this.ComfortNoise != updatedAgcfGatewayRecord.ComfortNoise) { this.ComfortNoise = updatedAgcfGatewayRecord.ComfortNoise; updated = true; }
            if (this.Dtmf != updatedAgcfGatewayRecord.Dtmf) { this.Dtmf = updatedAgcfGatewayRecord.Dtmf; updated = true; }
            if (this.FaxEvents != updatedAgcfGatewayRecord.FaxEvents) { this.FaxEvents = updatedAgcfGatewayRecord.FaxEvents; updated = true; }
            if (this.ModemEvents != updatedAgcfGatewayRecord.ModemEvents) { this.ModemEvents = updatedAgcfGatewayRecord.ModemEvents; updated = true; }
            if (this.TextTelephonyEvents != updatedAgcfGatewayRecord.TextTelephonyEvents) { this.TextTelephonyEvents = updatedAgcfGatewayRecord.TextTelephonyEvents; updated = true; }
            if (this.AllCodecDataStr != updatedAgcfGatewayRecord.AllCodecDataStr) { this.AllCodecDataStr = updatedAgcfGatewayRecord.AllCodecDataStr; updated = true; }
            if (this.AgwIuaIpAddress != updatedAgcfGatewayRecord.AgwIuaIpAddress) { this.AgwIuaIpAddress = updatedAgcfGatewayRecord.AgwIuaIpAddress; updated = true; }
            if (this.AgwIuaSctpPort != updatedAgcfGatewayRecord.AgwIuaSctpPort) { this.AgwIuaSctpPort = updatedAgcfGatewayRecord.AgwIuaSctpPort; updated = true; }
            if (this.AgcfLocalSctpPort != updatedAgcfGatewayRecord.AgcfLocalSctpPort) { this.AgcfLocalSctpPort = updatedAgcfGatewayRecord.AgcfLocalSctpPort; updated = true; }
            if (this.SctpProfile != updatedAgcfGatewayRecord.SctpProfile) { this.SctpProfile = updatedAgcfGatewayRecord.SctpProfile; updated = true; }
            if (this.IuaIIDMapScheme != updatedAgcfGatewayRecord.IuaIIDMapScheme) { this.IuaIIDMapScheme = updatedAgcfGatewayRecord.IuaIIDMapScheme; updated = true; }
            if (this.LocalTermTypeIsdnPrefix != updatedAgcfGatewayRecord.LocalTermTypeIsdnPrefix) { this.LocalTermTypeIsdnPrefix = updatedAgcfGatewayRecord.LocalTermTypeIsdnPrefix; updated = true; }
            if (this.AuditAllActiveIsdnCalls != updatedAgcfGatewayRecord.AuditAllActiveIsdnCalls) { this.AuditAllActiveIsdnCalls = updatedAgcfGatewayRecord.AuditAllActiveIsdnCalls; updated = true; }
            if (this.MateSite != updatedAgcfGatewayRecord.MateSite) { this.MateSite = updatedAgcfGatewayRecord.MateSite; updated = true; }
            if (this.MateExternalIPAddr != updatedAgcfGatewayRecord.MateExternalIPAddr) { this.MateExternalIPAddr = updatedAgcfGatewayRecord.MateExternalIPAddr; updated = true; }
            if (this.IsPrimary != updatedAgcfGatewayRecord.IsPrimary) { this.IsPrimary = updatedAgcfGatewayRecord.IsPrimary; updated = true; }
            if (this.AddtionalDigitMapList != updatedAgcfGatewayRecord.AddtionalDigitMapList) { this.AddtionalDigitMapList = updatedAgcfGatewayRecord.AddtionalDigitMapList; updated = true; }
            if (this.AuthTimer != updatedAgcfGatewayRecord.AuthTimer) { this.AuthTimer = updatedAgcfGatewayRecord.AuthTimer; updated = true; }
            if (this.LocalSecKey != updatedAgcfGatewayRecord.LocalSecKey) { this.LocalSecKey = updatedAgcfGatewayRecord.LocalSecKey; updated = true; }
            if (this.SharedBasNumber != updatedAgcfGatewayRecord.SharedBasNumber) { this.SharedBasNumber = updatedAgcfGatewayRecord.SharedBasNumber; updated = true; }
            if (this.SharedPriNumber != updatedAgcfGatewayRecord.SharedPriNumber) { this.SharedPriNumber = updatedAgcfGatewayRecord.SharedPriNumber; updated = true; }
            if (this.SharedSecKey != updatedAgcfGatewayRecord.SharedSecKey) { this.SharedSecKey = updatedAgcfGatewayRecord.SharedSecKey; updated = true; }
            if (this.MgID != updatedAgcfGatewayRecord.MgID) { this.MgID = updatedAgcfGatewayRecord.MgID; updated = true; }
            if (this.EnnableMD5DigAuteh != updatedAgcfGatewayRecord.EnnableMD5DigAuteh) { this.EnnableMD5DigAuteh = updatedAgcfGatewayRecord.EnnableMD5DigAuteh; updated = true; }
            if (this.ChannelAudits != updatedAgcfGatewayRecord.ChannelAudits) { this.ChannelAudits = updatedAgcfGatewayRecord.ChannelAudits; updated = true; }
            if (this.GroupId != updatedAgcfGatewayRecord.GroupId) { this.GroupId = updatedAgcfGatewayRecord.GroupId; updated = true; }
            if (this.AgcfGateWayType != updatedAgcfGatewayRecord.AgcfGateWayType) { this.AgcfGateWayType = updatedAgcfGatewayRecord.AgcfGateWayType; updated = true; }
            if (this.UserId != updatedAgcfGatewayRecord.UserId) { this.UserId = updatedAgcfGatewayRecord.UserId; updated = true; }

            if(updated) this.Updated = this.Inspected = DateTime.UtcNow.AddHours(3);
            //else this.Inspected = DateTime.UtcNow.AddHours(3);

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}