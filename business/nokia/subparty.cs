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
    /// SubParty support class for Nokia's Next Generation Network (NGN) business model.
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
    public partial class SubParty
    {
        public SubParty() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Nokia.SubParty ParseFromDictionary(Dictionary<string, string> parameterDictionary)
        {
            int i;
            long l;
            string partyId;
            Ia.Ngn.Cl.Model.Nokia.SubParty subParty;

            subParty = new Ia.Ngn.Cl.Model.Nokia.SubParty();

            partyId = parameterDictionary["PartyId"].ToString();

            subParty.Id = Ia.Ngn.Cl.Model.Nokia.SubParty.SubPartyId(partyId);

            subParty.PartyId = partyId;
            subParty.DisplayName = parameterDictionary["DisplayName"].ToString();
            subParty.Category = parameterDictionary["Category"].ToString();
            //subparty.NewPartyId = param["NewPartyId"].ToString();
            subParty.PrimaryPUID = parameterDictionary["PrimaryPUID"].ToString();
            //subparty.NewPrimaryPUID = param["NewPrimaryPUID"].ToString();
            subParty.PrimaryPUIDDomainRequired = Convert.ToBoolean(parameterDictionary["PrimaryPUIDDomainRequired"].ToString());
            subParty.PrimaryPUIDCPEProfileNumber = int.TryParse(parameterDictionary["PrimaryPUIDCPEProfileNumber"].ToString(), out i) ? i : 0;
            subParty.PrimaryPUIDFlashable = Convert.ToBoolean(parameterDictionary["PrimaryPUIDFlashable"].ToString());
            subParty.AssocOtasRealm = parameterDictionary["AssocOtasRealm"].ToString();
            subParty.NetworkProfileName = parameterDictionary["NetworkProfileName"].ToString();
            subParty.NetworkProfileVersion = int.TryParse(parameterDictionary["NetworkProfileVersion"].ToString(), out i) ? i : 0;
            subParty.ServiceProfileName = parameterDictionary["ServiceProfileName"].ToString();
            subParty.ServiceProfileVersion = int.TryParse(parameterDictionary["ServiceProfileVersion"].ToString(), out i) ? i : 0;
            subParty.IsReducedServiceProfile = Convert.ToBoolean(parameterDictionary["IsReducedServiceProfile"].ToString());
            subParty.CallLimit = int.TryParse(parameterDictionary["CallLimit"].ToString(), out i) ? i : 0;
            subParty.ServiceSuspension = Convert.ToBoolean(parameterDictionary["ServiceSuspension"].ToString());
            subParty.OriginationSuspension = Convert.ToBoolean(parameterDictionary["OriginationSuspension"].ToString());
            subParty.TerminationSuspension = Convert.ToBoolean(parameterDictionary["TerminationSuspension"].ToString());
            subParty.SuspensionNotification = Convert.ToBoolean(parameterDictionary["SuspensionNotification"].ToString());
            subParty.UserOrigSuspension = Convert.ToBoolean(parameterDictionary["UserOrigSuspension"].ToString());
            subParty.UserTermSuspension = Convert.ToBoolean(parameterDictionary["UserTermSuspension"].ToString());
            subParty.AssocWpifRealm = parameterDictionary["AssocWpifRealm"].ToString();
            subParty.IddPrefix = parameterDictionary["IddPrefix"].ToString();
            subParty.AlternateFsdbFqdn = parameterDictionary["AlternateFsdbFqdn"].ToString();
            subParty.SharedHssData = Convert.ToBoolean(parameterDictionary["SharedHssData"].ToString());
            subParty.Pin = parameterDictionary["Pin"].ToString();
            subParty.MsnCapability = Convert.ToBoolean(parameterDictionary["MsnCapability"].ToString());
            subParty.VideoProhibit = Convert.ToBoolean(parameterDictionary["VideoProhibit"].ToString());
            subParty.MaxFwdHops = int.TryParse(parameterDictionary["MaxFwdHops"].ToString(), out i) ? i : 0;
            subParty.CsdFlavor = int.TryParse(parameterDictionary["CsdFlavor"].ToString(), out i) ? i : 0;
            subParty.CsdDynamic = Convert.ToBoolean(parameterDictionary["CsdDynamic"].ToString());
            subParty.SipErrorTableId = int.TryParse(parameterDictionary["SipErrorTableId"].ToString(), out i) ? i : 0;
            subParty.TreatmentTableId = int.TryParse(parameterDictionary["TreatmentTableId"].ToString(), out i) ? i : 0;
            subParty.Locale = parameterDictionary["Locale"].ToString();
            subParty.CliPrefixList = parameterDictionary["CliPrefixList"].ToString();
            subParty.IsGroupCPE = Convert.ToBoolean(parameterDictionary["IsGroupCPE"].ToString());
            subParty.Receive181Mode = int.TryParse(parameterDictionary["Receive181Mode"].ToString(), out i) ? i : 0;
            subParty.CcNdcLength = int.TryParse(parameterDictionary["CcNdcLength"].ToString(), out i) ? i : 0;
            subParty.MaxActiveCalls = int.TryParse(parameterDictionary["MaxActiveCalls"].ToString(), out i) ? i : 0;
            subParty.CallingPartyCategory = int.TryParse(parameterDictionary["CallingPartyCategory"].ToString(), out i) ? i : 0;
            subParty.PublicUID1 = parameterDictionary["PublicUID1"].ToString();
            subParty.PublicUID2 = parameterDictionary["PublicUID2"].ToString();
            subParty.PublicUID3 = parameterDictionary["PublicUID3"].ToString();
            subParty.PublicUID4 = parameterDictionary["PublicUID4"].ToString();
            subParty.PublicUID5 = parameterDictionary["PublicUID5"].ToString();
            subParty.PublicUID6 = parameterDictionary["PublicUID6"].ToString();
            subParty.PublicUID7 = parameterDictionary["PublicUID7"].ToString();
            subParty.PublicUID8 = parameterDictionary["PublicUID8"].ToString();
            subParty.PublicUID9 = parameterDictionary["PublicUID9"].ToString();
            subParty.PublicUID1DomainRequired = Convert.ToBoolean(parameterDictionary["PublicUID1DomainRequired"].ToString());
            subParty.PublicUID2DomainRequired = Convert.ToBoolean(parameterDictionary["PublicUID2DomainRequired"].ToString());
            subParty.PublicUID3DomainRequired = Convert.ToBoolean(parameterDictionary["PublicUID3DomainRequired"].ToString());
            subParty.PublicUID4DomainRequired = Convert.ToBoolean(parameterDictionary["PublicUID4DomainRequired"].ToString());
            subParty.PublicUID5DomainRequired = Convert.ToBoolean(parameterDictionary["PublicUID5DomainRequired"].ToString());
            subParty.PublicUID6DomainRequired = Convert.ToBoolean(parameterDictionary["PublicUID6DomainRequired"].ToString());
            subParty.PublicUID7DomainRequired = Convert.ToBoolean(parameterDictionary["PublicUID7DomainRequired"].ToString());
            subParty.PublicUID8DomainRequired = Convert.ToBoolean(parameterDictionary["PublicUID8DomainRequired"].ToString());
            subParty.PublicUID9DomainRequired = Convert.ToBoolean(parameterDictionary["PublicUID9DomainRequired"].ToString());
            subParty.WildCardPUIDStr = parameterDictionary["WildCardPUIDStr"].ToString();
            subParty.AllowCustomAnnouncement = Convert.ToBoolean(parameterDictionary["AllowCustomAnnouncement"].ToString());
            subParty.PtySpareLong1 = long.TryParse(parameterDictionary["PtySpareLong1"].ToString(), out l) ? l : 0;
            subParty.PtySpareString = parameterDictionary["PtySpareString"].ToString();
            subParty.PtySpareString2 = parameterDictionary["PtySpareString2"].ToString();
            subParty.PtySpareShort1 = int.TryParse(parameterDictionary["PtySpareShort1"].ToString(), out i) ? i : 0;
            subParty.PtySpareShort2 = int.TryParse(parameterDictionary["PtySpareShort2"].ToString(), out i) ? i : 0;
            subParty.PtySpareBool1 = Convert.ToBoolean(parameterDictionary["PtySpareBool1"].ToString());
            subParty.PtySpareBool2 = Convert.ToBoolean(parameterDictionary["PtySpareBool2"].ToString());
            subParty.PtySpareBool3 = Convert.ToBoolean(parameterDictionary["PtySpareBool3"].ToString());
            subParty.PtySpareBool4 = Convert.ToBoolean(parameterDictionary["PtySpareBool4"].ToString());
            subParty.PtySpareBool5 = Convert.ToBoolean(parameterDictionary["PtySpareBool5"].ToString());
            subParty.PtySpareBool6 = Convert.ToBoolean(parameterDictionary["PtySpareBool6"].ToString());
            subParty.PtySpareBool7 = Convert.ToBoolean(parameterDictionary["PtySpareBool7"].ToString());
            subParty.PtySpareBool8 = Convert.ToBoolean(parameterDictionary["PtySpareBool8"].ToString());
            subParty.TerminatingTableId = int.TryParse(parameterDictionary["TerminatingTableId"].ToString(), out i) ? i : 0;
            subParty.AllowNonSipTelUri = Convert.ToBoolean(parameterDictionary["AllowNonSipTelUri"].ToString());
            subParty.LocationType = int.TryParse(parameterDictionary["LocationType"].ToString(), out i) ? i : 0;
            subParty.RncID = parameterDictionary["RncID"].ToString();
            subParty.LteMcc = parameterDictionary["LteMcc"].ToString();
            subParty.LteMnc = parameterDictionary["LteMnc"].ToString();
            subParty.LteTac = int.TryParse(parameterDictionary["LteTac"].ToString(), out i) ? i : 0;
            subParty.MarketSID = int.TryParse(parameterDictionary["MarketSID"].ToString(), out i) ? i : 0;
            subParty.SwitchNumber = int.TryParse(parameterDictionary["SwitchNumber"].ToString(), out i) ? i : 0;
            subParty.CallsToWebUserProhibited = Convert.ToBoolean(parameterDictionary["CallsToWebUserProhibited"].ToString());
            subParty.IMSI = parameterDictionary["IMSI"].ToString();
            subParty.IMSNotSupported = Convert.ToBoolean(parameterDictionary["IMSNotSupported"].ToString());
            subParty.ValidateCellID = Convert.ToBoolean(parameterDictionary["ValidateCellID"].ToString());
            subParty.OperatorID = int.TryParse(parameterDictionary["OperatorID"].ToString(), out i) ? i : 0;
            subParty.HomeMTA = int.TryParse(parameterDictionary["HomeMTA"].ToString(), out i) ? i : 0;
            subParty.ForwardDenyNumbers = Convert.ToBoolean(parameterDictionary["ForwardDenyNumbers"].ToString());
            subParty.PlayAnnoFailNotForward = Convert.ToBoolean(parameterDictionary["PlayAnnoFailNotForward"].ToString());
            subParty.MrfPoolID = int.TryParse(parameterDictionary["MrfPoolID"].ToString(), out i) ? i : 0;
            subParty.Custom120x = Convert.ToBoolean(parameterDictionary["Custom120x"].ToString());

            subParty.Created = DateTime.UtcNow.AddHours(3);
            subParty.Updated = DateTime.UtcNow.AddHours(3);
            subParty.Inspected = DateTime.UtcNow.AddHours(3);
            subParty.UserId = Guid.Empty;

            return subParty;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}