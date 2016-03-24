using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model.Business.Nokia
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// AGCF Gateway Records support class for Nokia's Next Generation Network (NGN) business model.
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
        public AgcfGatewayRecord() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int RandomUnusedGwId()
        {
            int randomGwId;
            Random r;
            List<int> unusedGwIdList;

            r = new Random();
            unusedGwIdList = Ia.Ngn.Cl.Model.Data.Nokia.AgcfGatewayRecord.ReadUnusedGwIdList();

            randomGwId = unusedGwIdList[r.Next(unusedGwIdList.Count)];

            return randomGwId;        
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord ParseFromDictionary(Dictionary<string, string> parameterDictionary)
        {
            int i, gwId, tableId;
            long l;
            Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord agcfGatewayRecord;

            agcfGatewayRecord = new Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord();

            gwId = int.Parse(parameterDictionary["GwId"].ToString());
            tableId = int.Parse(parameterDictionary["TableId"].ToString());

            agcfGatewayRecord.Id = Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord.AgcfGatewayRecordId(tableId, gwId);

            agcfGatewayRecord.NgcfAgcfGatewayRecordAid = parameterDictionary["NgcfAgcfGatewayRecordAid"].ToString();
            agcfGatewayRecord.GwId = gwId;
            agcfGatewayRecord.TableId = tableId;

            agcfGatewayRecord.AgcfSipIaPort = parameterDictionary["AgcfSipIaPort"].ToString();
            agcfGatewayRecord.ConnectionType = (parameterDictionary["ConnectionType"].ToString() == "UDP/IP") ? true : false;
            agcfGatewayRecord.ContextAudits = (parameterDictionary["ContextAudits"].ToString() == "None") ? true : false;
            agcfGatewayRecord.GwDigitMapId = int.TryParse(parameterDictionary["GwDigitMapId"].ToString(), out i) ? i : 0;
            agcfGatewayRecord.EnableChannelStatusAudits = Convert.ToBoolean(parameterDictionary["EnableChannelStatusAudits"].ToString());
            agcfGatewayRecord.GwUserId = parameterDictionary["GwUserId"].ToString();
            agcfGatewayRecord.GwDomain = parameterDictionary["GwDomain"].ToString();
            agcfGatewayRecord.GwName = parameterDictionary["GwName"].ToString();
            agcfGatewayRecord.GwPrivId = parameterDictionary["GwPrivId"].ToString();
            agcfGatewayRecord.IP1 = parameterDictionary["IP1"].ToString();
            agcfGatewayRecord.IP2 = parameterDictionary["IP2"].ToString();
            agcfGatewayRecord.LocalTermTypePrefix = parameterDictionary["LocalTermTypePrefix"].ToString();
            agcfGatewayRecord.LocalTermTypeAnalogPrefix = parameterDictionary["LocalTermTypeAnalogPrefix"].ToString();
            agcfGatewayRecord.IsLocalTermTypeTDMAnalog = Convert.ToBoolean(parameterDictionary["IsLocalTermTypeTDMAnalog"].ToString());
            agcfGatewayRecord.NetTermTypePrefix = parameterDictionary["NetTermTypePrefix"].ToString();
            agcfGatewayRecord.IsNetTermTypeRTPUDP = Convert.ToBoolean(parameterDictionary["IsNetTermTypeRTPUDP"].ToString());
            agcfGatewayRecord.PhysicalTermIdScheme = parameterDictionary["PhysicalTermIdScheme"].ToString();
            agcfGatewayRecord.SendCompactMessages = Convert.ToBoolean(parameterDictionary["SendCompactMessages"].ToString());
            agcfGatewayRecord.SendEphemeralPrefix = Convert.ToBoolean(parameterDictionary["SendEphemeralPrefix"].ToString());
            agcfGatewayRecord.UdpPort = int.TryParse(parameterDictionary["UdpPort"].ToString(), out i) ? i : 0;
            agcfGatewayRecord.GwVariantId = int.TryParse(parameterDictionary["GwVariantId"].ToString(), out i) ? i : 0;
            agcfGatewayRecord.AreaId = int.TryParse(parameterDictionary["AreaId"].ToString(), out i) ? i : 0;
            agcfGatewayRecord.ComfortNoise = Convert.ToBoolean(parameterDictionary["ComfortNoise"].ToString());
            agcfGatewayRecord.Dtmf = Convert.ToBoolean(parameterDictionary["Dtmf"].ToString());
            agcfGatewayRecord.FaxEvents = Convert.ToBoolean(parameterDictionary["FaxEvents"].ToString());
            agcfGatewayRecord.ModemEvents = Convert.ToBoolean(parameterDictionary["ModemEvents"].ToString());
            agcfGatewayRecord.TextTelephonyEvents = Convert.ToBoolean(parameterDictionary["TextTelephonyEvents"].ToString());
            agcfGatewayRecord.AllCodecDataStr = parameterDictionary["AllCodecDataStr"].ToString();
            agcfGatewayRecord.AgwIuaIpAddress = parameterDictionary["AgwIuaIpAddress"].ToString();
            agcfGatewayRecord.AgwIuaSctpPort = int.TryParse(parameterDictionary["AgwIuaSctpPort"].ToString(), out i) ? i : 0;
            agcfGatewayRecord.AgcfLocalSctpPort = int.TryParse(parameterDictionary["AgcfLocalSctpPort"].ToString(), out i) ? i : 0;
            agcfGatewayRecord.SctpProfile = int.TryParse(parameterDictionary["SctpProfile"].ToString(), out i) ? i : 0;
            agcfGatewayRecord.IuaIIDMapScheme = parameterDictionary["IuaIIDMapScheme"].ToString();
            agcfGatewayRecord.LocalTermTypeIsdnPrefix = parameterDictionary["LocalTermTypeIsdnPrefix"].ToString();
            agcfGatewayRecord.AuditAllActiveIsdnCalls = Convert.ToBoolean(parameterDictionary["AuditAllActiveIsdnCalls"].ToString());
            agcfGatewayRecord.MateSite = int.TryParse(parameterDictionary["MateSite"].ToString(), out i) ? i : 0;
            agcfGatewayRecord.MateExternalIPAddr = parameterDictionary["MateExternalIPAddr"].ToString();
            agcfGatewayRecord.IsPrimary = Convert.ToBoolean(parameterDictionary["IsPrimary"].ToString());
            agcfGatewayRecord.AddtionalDigitMapList = parameterDictionary["AddtionalDigitMapList"].ToString();
            agcfGatewayRecord.AuthTimer = int.TryParse(parameterDictionary["AuthTimer"].ToString(), out i) ? i : 0;
            agcfGatewayRecord.SharedBasNumber = long.TryParse(parameterDictionary["SharedBasNumber"].ToString(), out l) ? l : 0;
            agcfGatewayRecord.SharedPriNumber = int.TryParse(parameterDictionary["SharedPriNumber"].ToString(), out i) ? i : 0;
            agcfGatewayRecord.MgID = parameterDictionary["MgID"].ToString();
            agcfGatewayRecord.EnnableMD5DigAuteh = Convert.ToBoolean(parameterDictionary["EnnableMD5DigAuteh"].ToString());
            agcfGatewayRecord.ChannelAudits = int.TryParse(parameterDictionary["ChannelAudits"].ToString(), out i) ? i : 0;
            agcfGatewayRecord.GroupId = int.TryParse(parameterDictionary["GroupId"].ToString(), out i) ? i : 0;
            agcfGatewayRecord.AgcfGateWayType = int.TryParse(parameterDictionary["AgcfGateWayType"].ToString(), out i) ? i : 0;

            agcfGatewayRecord.Created = DateTime.UtcNow.AddHours(3);
            agcfGatewayRecord.Updated = DateTime.UtcNow.AddHours(3);
            agcfGatewayRecord.Inspected = DateTime.UtcNow.AddHours(3);
            agcfGatewayRecord.UserId = Guid.Empty;

            return agcfGatewayRecord;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}