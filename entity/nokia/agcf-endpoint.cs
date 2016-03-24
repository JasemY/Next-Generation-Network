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
    /// AGCF Endpoint Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class AgcfEndpoint
    {
        /// <summary>
        /// 1360 COM WebAPI User Guide 255-400-419R3.X
        /// ngfs-agcfendpoint-v2(rtrv,ent,ed,dlt)
        /// </summary>
        public AgcfEndpoint() { }

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        /// <summary>
        /// PrividUser (req for ent,req for ed). LCP Aid of AGCF Endpoint, same value of PrimaryPUID. String (0-32) [+0-9a-zA-Z:,\-_\.'()]*
        /// </summary>
        public string PrividUser { get; set; }

        /// <summary>
        /// Slot (opt for ent,na for ed). LCP Integer (1-99)
        /// </summary>
        public int Slot { get; set; }

        /// <summary>
        /// Port (opt for ent,na for ed). LCP Integer (1-99), LCP Integer (2048-4047), LCP-CTS R8.0 Integer (2048-18047), LCP-CTS R8.1.1 Integer (2048-502047)
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// GwId (req for ent,na for ed). LCP-CTS R9.1 Integer (2048-5002047)
        /// </summary>
        public int GwId { get; set; }

        /// <summary>
        /// Dn (req for ent,na for ed). LCP String (0-32) [+0-9a-zA-Z:,\-_\.'()]*
        /// </summary>
        public string Dn { get; set; }

        /// <summary>
        /// AdditionalDNs (opt for ent,opt for ed). LCP-CTS R8.1 String, up 9 space separated additional DNs. Each can be up to 32 characters long [0-9a-zA-Z&amp;=\+$,\?/\-_\.!~\*'()]*
        /// </summary>
        public string AdditionalDNs { get; set; }

        /// <summary>
        /// DomainIndex (opt for ent,na for ed). LCP Integer (0-150)
        /// </summary>
        public int DomainIndex { get; set; }

        /// <summary>
        /// FeatureFlag (opt for ent,opt for ed). LCP Valid value: 0, 1, 2, 0 means None, 1 means WarmLine, 2 means HotLine
        /// </summary>
        public int FeatureFlag { get; set; }

        /// <summary>
        /// DslamId (opt for ent,na for ed). LCP String (0-32) [a-zA-Z0-9\*#\.,\-+'~`!@$%^:;=(){}\[\]?&quot;&apos;&amp;&gt;&lt;/_|\\ ]*
        /// </summary>
        public string DslamId { get; set; }

        /// <summary>
        /// Rack (opt for ent,na for ed). LCP Integer (1-9)
        /// </summary>
        public int Rack { get; set; }

        /// <summary>
        /// Shelf (opt for ent,na for ed). LCP Integer (1-99)
        /// </summary>
        public int Shelf { get; set; }

        /// <summary>
        /// FlatTermID (opt for ent,na for ed). LCP-CTS R6.1 Integer (1-32767)
        /// If the GwId corresponding PhysicalTermIDScheme is set to FlatTermID, this FlatTermID field can have D. LCP Command. 317
        /// the value between 1-32767 and the other field shall be set as the following default value:
        /// Slot: 0
        /// Port: 0
        /// DslamId : null
        /// Rack: -1
        /// Shelf: 0
        /// LCP-ISC R21.1.1 Integer (0-32767)
        /// When Shelf/Slot/Port is provisioned, this field will be
        /// set to -1 and value will not be send to NE
        /// </summary>
        public int FlatTermID { get; set; }

        /// <summary>
        /// SubscriberType (opt for ent,na for ed). LCP-CTS R6.2 Range: Analog or ISDNBRA. Default: Analog
        /// </summary>
        public bool SubscriberType { get; set; }

        /// <summary>
        /// ReversePolarity (opt for ent,opt for ed). LCP-CTS R7.1
        /// Enum: NO_POLARITY_REVERSAL, UPON_DIGIT_COLLECTION_ALWAYS, UPON_REMOTE_ANSWER_ALWAYS, UPON_REMOTE_ANSWER_EXCEPT_FREE_CALLS
        /// Default: NO_POLARITY_REVERSAL
        /// </summary>
        public int ReversePolarity { get; set; }

        /// <summary>
        /// PayphoneMetering (opt for ent,opt for ed). LCP-CTS R7.1 
        /// enum: PAYPHONE_REVERSE_POLARITY, PAYPHONE_PULSE_METERING_16_KHZ
        /// default: PAYPHONE_PULSE_METERING_16_KHZ
        /// </summary>
        public int PayphoneMetering { get; set; }

        /// <summary>
        /// DigitMap1st (opt for ent,opt for ed). LCP-CTS R7.1 Integer: 0-64
        /// </summary>
        public int DigitMap1st { get; set; }

        /// <summary>
        /// DigitMap2nd (opt for ent,opt for ed). LCP-CTS R7.1 Integer: 0-64
        /// </summary>
        public int DigitMap2nd { get; set; }

        /// <summary>
        /// DialTone2nd (opt for ent,opt for ed). LCP-CTS R7.1 Integer: 0-128
        /// </summary>
        public int DialTone2nd { get; set; }

        /// <summary>
        /// CallHoldLc (opt for ent,opt for ed). LCP-CTS R8.0 boolean, default true
        /// </summary>
        public bool CallHoldLc { get; set; }

        /// <summary>
        /// CallWaitingLc (opt for ent,opt for ed). LCP-CTS R8.0 boolean, default true
        /// </summary>
        public bool CallWaitingLc { get; set; }

        /// <summary>
        /// CallToggleLc (opt for ent,opt for ed). LCP-CTS R8.0 boolean, default true
        /// </summary>
        public bool CallToggleLc { get; set; }

        /// <summary>
        /// ThreeWayCallLc (opt for ent,opt for ed). LCP-CTS R8.0 boolean, default true
        /// </summary>
        public bool ThreeWayCallLc { get; set; }

        /// <summary>
        /// McidLc (opt for ent,opt for ed). LCP-CTS R8.0 boolean, default false
        /// </summary>
        public bool McidLc { get; set; }

        /// <summary>
        /// Password (opt for ent,opt for ed,na for rtrv). LCP-CTS R8.1 String, max length 32. [+0-9a-zA-Z(),':@&=|-_\.!~*%]*
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// CallTransferLc (opt for ent,opt for ed). LCP-ISC R22.1 boolean, default false
        /// </summary>
        public bool CallTransferLc { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual AgcfGatewayRecord AgcfGatewayRecord { get; set; }

        /// <summary/>
        public virtual ICollection<SubParty> SubParties { get; set; }

        /// <summary/>
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
        public static string AgcfEndpointId(string prividUser)
        {
            return prividUser;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(AgcfEndpoint agcfEndpoint, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfEndpoint.Created = agcfEndpoint.Updated = agcfEndpoint.Inspected = DateTime.UtcNow.AddHours(3);

                db.AgcfEndpoints.Add(agcfEndpoint);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static AgcfEndpoint Read(string id)
        {
            AgcfEndpoint agcfEndpoint;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfEndpoint = (from q in db.AgcfEndpoints where q.Id == id select q).SingleOrDefault();
            }

            return agcfEndpoint;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static AgcfEndpoint ReadbyGwId(int gwId)
        {
            AgcfEndpoint agcfEndpoint;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfEndpoint = (from q in db.AgcfEndpoints where q.GwId == gwId select q).SingleOrDefault();
            }

            return agcfEndpoint;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static AgcfEndpoint ReadWithAgcfGatewayRecord(string id)
        {
            AgcfEndpoint agcfEndpoint;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfEndpoint = (from q in db.AgcfEndpoints where q.Id == id select q).Include(x => x.AgcfGatewayRecord).SingleOrDefault();
            }

            return agcfEndpoint;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<AgcfEndpoint> ReadList()
        {
            List<AgcfEndpoint> agcfEndpointList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfEndpointList = (from q in db.AgcfEndpoints select q).ToList();
            }

            return agcfEndpointList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(AgcfEndpoint agcfEndpoint, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfEndpoint = (from q in db.AgcfEndpoints where q.Id == agcfEndpoint.Id select q).SingleOrDefault();

                agcfEndpoint.Updated = DateTime.UtcNow.AddHours(3);

                db.AgcfEndpoints.Attach(agcfEndpoint);

                db.Entry(agcfEndpoint).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Delete(string id, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                var v = (from q in db.AgcfEndpoints where q.Id == id select q).FirstOrDefault();

                db.AgcfEndpoints.Remove(v);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Equal(AgcfEndpoint b)
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
        public bool Update(AgcfEndpoint updatedAgcfEndpoint)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.PrividUser != updatedAgcfEndpoint.PrividUser) { this.PrividUser = updatedAgcfEndpoint.PrividUser; updated = true; }
            if (this.Slot != updatedAgcfEndpoint.Slot) { this.Slot = updatedAgcfEndpoint.Slot; updated = true; }
            if (this.Port != updatedAgcfEndpoint.Port) { this.Port = updatedAgcfEndpoint.Port; updated = true; }
            if (this.GwId != updatedAgcfEndpoint.GwId) { this.GwId = updatedAgcfEndpoint.GwId; updated = true; }
            if (this.Dn != updatedAgcfEndpoint.Dn) { this.Dn = updatedAgcfEndpoint.Dn; updated = true; }
            if (this.AdditionalDNs != updatedAgcfEndpoint.AdditionalDNs) { this.AdditionalDNs = updatedAgcfEndpoint.AdditionalDNs; updated = true; }
            if (this.DomainIndex != updatedAgcfEndpoint.DomainIndex) { this.DomainIndex = updatedAgcfEndpoint.DomainIndex; updated = true; }
            if (this.FeatureFlag != updatedAgcfEndpoint.FeatureFlag) { this.FeatureFlag = updatedAgcfEndpoint.FeatureFlag; updated = true; }
            if (this.DslamId != updatedAgcfEndpoint.DslamId) { this.DslamId = updatedAgcfEndpoint.DslamId; updated = true; }
            if (this.Rack != updatedAgcfEndpoint.Rack) { this.Rack = updatedAgcfEndpoint.Rack; updated = true; }
            if (this.Shelf != updatedAgcfEndpoint.Shelf) { this.Shelf = updatedAgcfEndpoint.Shelf; updated = true; }
            if (this.FlatTermID != updatedAgcfEndpoint.FlatTermID) { this.FlatTermID = updatedAgcfEndpoint.FlatTermID; updated = true; }
            if (this.SubscriberType != updatedAgcfEndpoint.SubscriberType) { this.SubscriberType = updatedAgcfEndpoint.SubscriberType; updated = true; }
            if (this.ReversePolarity != updatedAgcfEndpoint.ReversePolarity) { this.ReversePolarity = updatedAgcfEndpoint.ReversePolarity; updated = true; }
            if (this.PayphoneMetering != updatedAgcfEndpoint.PayphoneMetering) { this.PayphoneMetering = updatedAgcfEndpoint.PayphoneMetering; updated = true; }
            if (this.DigitMap1st != updatedAgcfEndpoint.DigitMap1st) { this.DigitMap1st = updatedAgcfEndpoint.DigitMap1st; updated = true; }
            if (this.DigitMap2nd != updatedAgcfEndpoint.DigitMap2nd) { this.DigitMap2nd = updatedAgcfEndpoint.DigitMap2nd; updated = true; }
            if (this.DialTone2nd != updatedAgcfEndpoint.DialTone2nd) { this.DialTone2nd = updatedAgcfEndpoint.DialTone2nd; updated = true; }
            if (this.CallHoldLc != updatedAgcfEndpoint.CallHoldLc) { this.CallHoldLc = updatedAgcfEndpoint.CallHoldLc; updated = true; }
            if (this.CallWaitingLc != updatedAgcfEndpoint.CallWaitingLc) { this.CallWaitingLc = updatedAgcfEndpoint.CallWaitingLc; updated = true; }
            if (this.CallToggleLc != updatedAgcfEndpoint.CallToggleLc) { this.CallToggleLc = updatedAgcfEndpoint.CallToggleLc; updated = true; }
            if (this.ThreeWayCallLc != updatedAgcfEndpoint.ThreeWayCallLc) { this.ThreeWayCallLc = updatedAgcfEndpoint.ThreeWayCallLc; updated = true; }
            if (this.McidLc != updatedAgcfEndpoint.McidLc) { this.McidLc = updatedAgcfEndpoint.McidLc; updated = true; }
            if (this.Password != updatedAgcfEndpoint.Password) { this.Password = updatedAgcfEndpoint.Password; updated = true; }
            if (this.CallTransferLc != updatedAgcfEndpoint.CallTransferLc) { this.CallTransferLc = updatedAgcfEndpoint.CallTransferLc; updated = true; }

            if (this.AgcfGatewayRecord != updatedAgcfEndpoint.AgcfGatewayRecord) { this.AgcfGatewayRecord = updatedAgcfEndpoint.AgcfGatewayRecord; updated = true; }

            if (this.UserId != updatedAgcfEndpoint.UserId) { this.UserId = updatedAgcfEndpoint.UserId; updated = true; }

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