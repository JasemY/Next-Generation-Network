using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Data.Entity;
using System.Xml.Linq;

namespace Ia.Ngn.Cl.Model.Business.Nokia
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Subscriber support class for Nokia's Next Generation Network (NGN) business model.
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
    public partial class Subscriber
    {
        public Subscriber() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Nokia.Subscriber ParseFromDictionary(Dictionary<string, string> parameterDictionary)
        {
            string partyId;
            Ia.Ngn.Cl.Model.Nokia.Subscriber subscriber;

            subscriber = new Ia.Ngn.Cl.Model.Nokia.Subscriber();

            partyId = Ia.Cl.Model.Default.Match(parameterDictionary["SubParty"].ToString(), "<PartyId>(.+)</PartyId>");

            subscriber.Id = Ia.Ngn.Cl.Model.Nokia.Subscriber.SubscriberId(partyId);
            subscriber.PartyId = partyId;

            subscriber.AlternateOtasRealm = (parameterDictionary.ContainsKey("AlternateOtasRealm")) ? parameterDictionary["AlternateOtasRealm"].ToString() : null;

            subscriber._AnonymousCallRej = (parameterDictionary.ContainsKey("AnonymousCallRej")) ? parameterDictionary["AnonymousCallRej"].ToString() : null;
            subscriber._SelectiveCallRejct = (parameterDictionary.ContainsKey("SelectiveCallRejct")) ? parameterDictionary["SelectiveCallRejct"].ToString() : null;
            subscriber._SelectiveCallAcpt = (parameterDictionary.ContainsKey("SelectiveCallAcpt")) ? parameterDictionary["SelectiveCallAcpt"].ToString() : null;
            subscriber._AutomaticCallBack = (parameterDictionary.ContainsKey("AutomaticCallBack")) ? parameterDictionary["AutomaticCallBack"].ToString() : null;
            subscriber._AutomaticRecall = (parameterDictionary.ContainsKey("AutomaticRecall")) ? parameterDictionary["AutomaticRecall"].ToString() : null;
            subscriber._CallingLineId = (parameterDictionary.ContainsKey("CallingLineId")) ? parameterDictionary["CallingLineId"].ToString() : null;
            subscriber._RemoteAccessServer = (parameterDictionary.ContainsKey("RemoteAccessServer")) ? parameterDictionary["RemoteAccessServer"].ToString() : null;
            subscriber._CallBarring = (parameterDictionary.ContainsKey("CallBarring")) ? parameterDictionary["CallBarring"].ToString() : null;
            subscriber._CallBlocking = (parameterDictionary.ContainsKey("CallBlocking")) ? parameterDictionary["CallBlocking"].ToString() : null;
            subscriber._CallBlocking = (parameterDictionary.ContainsKey("CallBlocking")) ? parameterDictionary["CallBlocking"].ToString() : null;
            subscriber._CallTransfer = (parameterDictionary.ContainsKey("CallTransfer")) ? parameterDictionary["CallTransfer"].ToString() : null;
            subscriber._CallWaiting = (parameterDictionary.ContainsKey("CallWaiting")) ? parameterDictionary["CallWaiting"].ToString() : null;
            subscriber._CallForwardingVari = (parameterDictionary.ContainsKey("CallForwardingVari")) ? parameterDictionary["CallForwardingVari"].ToString() : null;
            subscriber._CallForwardingBusy = (parameterDictionary.ContainsKey("CallForwardingBusy")) ? parameterDictionary["CallForwardingBusy"].ToString() : null;
            subscriber._CallForwardingNoAns = (parameterDictionary.ContainsKey("CallForwardingNoAns")) ? parameterDictionary["CallForwardingNoAns"].ToString() : null;
            subscriber._SelectiveCallFwd = (parameterDictionary.ContainsKey("SelectiveCallFwd")) ? parameterDictionary["SelectiveCallFwd"].ToString() : null;
            subscriber._CallForwardingUnreg = (parameterDictionary.ContainsKey("CallForwardingUnreg")) ? parameterDictionary["CallForwardingUnreg"].ToString() : null;
            subscriber._CustomerOrigTrace = (parameterDictionary.ContainsKey("CustomerOrigTrace")) ? parameterDictionary["CustomerOrigTrace"].ToString() : null;
            subscriber._NuisanceCallTrace = (parameterDictionary.ContainsKey("NuisanceCallTrace")) ? parameterDictionary["NuisanceCallTrace"].ToString() : null;
            subscriber._DoNotDisturb = (parameterDictionary.ContainsKey("DoNotDisturb")) ? parameterDictionary["DoNotDisturb"].ToString() : null;
            subscriber._MsgWaitingInd = (parameterDictionary.ContainsKey("MsgWaitingInd")) ? parameterDictionary["MsgWaitingInd"].ToString() : null;
            subscriber._SimultaneousRinging = (parameterDictionary.ContainsKey("SimultaneousRinging")) ? parameterDictionary["SimultaneousRinging"].ToString() : null;
            subscriber._WebPortal = (parameterDictionary.ContainsKey("WebPortal")) ? parameterDictionary["WebPortal"].ToString() : null;
            subscriber._ConferenceCalling = (parameterDictionary.ContainsKey("ConferenceCalling")) ? parameterDictionary["ConferenceCalling"].ToString() : null;
            subscriber._FlashOrigServices = (parameterDictionary.ContainsKey("FlashOrigServices")) ? parameterDictionary["FlashOrigServices"].ToString() : null;
            subscriber._CarrierSelection = (parameterDictionary.ContainsKey("CarrierSelection")) ? parameterDictionary["CarrierSelection"].ToString() : null;
            subscriber._RingbackWhenFree = (parameterDictionary.ContainsKey("RingbackWhenFree")) ? parameterDictionary["RingbackWhenFree"].ToString() : null;
            subscriber._MultipleRingPattern = (parameterDictionary.ContainsKey("MultipleRingPattern")) ? parameterDictionary["MultipleRingPattern"].ToString() : null;
            subscriber._CallForwardingLocal = (parameterDictionary.ContainsKey("CallForwardingLocal")) ? parameterDictionary["CallForwardingLocal"].ToString() : null;
            subscriber._RemoteAccessServices = (parameterDictionary.ContainsKey("RemoteAccessServices")) ? parameterDictionary["RemoteAccessServices"].ToString() : null;
            subscriber._VoiceMail = (parameterDictionary.ContainsKey("VoiceMail")) ? parameterDictionary["VoiceMail"].ToString() : null;
            subscriber._InterceptReferral = (parameterDictionary.ContainsKey("InterceptReferral")) ? parameterDictionary["InterceptReferral"].ToString() : null;
            subscriber._DialingPlan = (parameterDictionary.ContainsKey("DialingPlan")) ? parameterDictionary["DialingPlan"].ToString() : null;
            subscriber._HSSPrivateId = (parameterDictionary.ContainsKey("HSSPrivateId")) ? parameterDictionary["HSSPrivateId"].ToString() : null;
            subscriber._HSSPublicIdCustom = (parameterDictionary.ContainsKey("HSSPublicIdCustom")) ? parameterDictionary["HSSPublicIdCustom"].ToString() : null;
            subscriber._AcctCodes = (parameterDictionary.ContainsKey("AcctCodes")) ? parameterDictionary["AcctCodes"].ToString() : null;
            subscriber._SeqRinging = (parameterDictionary.ContainsKey("SeqRinging")) ? parameterDictionary["SeqRinging"].ToString() : null;
            subscriber._AuthCodeService = (parameterDictionary.ContainsKey("AuthCodeService")) ? parameterDictionary["AuthCodeService"].ToString() : null;
            subscriber._MLHGNoHuntMember = (parameterDictionary.ContainsKey("MLHGNoHuntMember")) ? parameterDictionary["MLHGNoHuntMember"].ToString() : null;
            subscriber._MultilineHuntGroup = (parameterDictionary.ContainsKey("MultilineHuntGroup")) ? parameterDictionary["MultilineHuntGroup"].ToString() : null;
            subscriber._CallPickupOrig = (parameterDictionary.ContainsKey("CallPickupOrig")) ? parameterDictionary["CallPickupOrig"].ToString() : null;
            subscriber._CallPickupTerm = (parameterDictionary.ContainsKey("CallPickupTerm")) ? parameterDictionary["CallPickupTerm"].ToString() : null;
            subscriber._Attendant = (parameterDictionary.ContainsKey("Attendant")) ? parameterDictionary["Attendant"].ToString() : null;
            subscriber._AttendantServer = (parameterDictionary.ContainsKey("AttendantServer")) ? parameterDictionary["AttendantServer"].ToString() : null;
            subscriber._CallPark = (parameterDictionary.ContainsKey("CallPark")) ? parameterDictionary["CallPark"].ToString() : null;
            subscriber._DirectedGroup = (parameterDictionary.ContainsKey("DirectedGroup")) ? parameterDictionary["DirectedGroup"].ToString() : null;
            subscriber._RemoteUser = (parameterDictionary.ContainsKey("RemoteUser")) ? parameterDictionary["RemoteUser"].ToString() : null;
            subscriber._TransferToUsersVM = (parameterDictionary.ContainsKey("TransferToUsersVM")) ? parameterDictionary["TransferToUsersVM"].ToString() : null;
            subscriber._FlexCallingLineId = (parameterDictionary.ContainsKey("FlexCallingLineId")) ? parameterDictionary["FlexCallingLineId"].ToString() : null;
            subscriber._ExtensionDevice = (parameterDictionary.ContainsKey("ExtensionDevice")) ? parameterDictionary["ExtensionDevice"].ToString() : null;
            subscriber._PSIServer = (parameterDictionary.ContainsKey("PSIServer")) ? parameterDictionary["PSIServer"].ToString() : null;
            subscriber._ClosedUserGroup = (parameterDictionary.ContainsKey("ClosedUserGroup")) ? parameterDictionary["ClosedUserGroup"].ToString() : null;
            subscriber._OneDigitSpeedDial = (parameterDictionary.ContainsKey("OneDigitSpeedDial")) ? parameterDictionary["OneDigitSpeedDial"].ToString() : null;
            subscriber._TwoDigitSpeedDial = (parameterDictionary.ContainsKey("TwoDigitSpeedDial")) ? parameterDictionary["TwoDigitSpeedDial"].ToString() : null;
            subscriber._ExtensionServer = (parameterDictionary.ContainsKey("ExtensionServer")) ? parameterDictionary["ExtensionServer"].ToString() : null;
            subscriber._SelectiveAlert = (parameterDictionary.ContainsKey("SelectiveAlert")) ? parameterDictionary["SelectiveAlert"].ToString() : null;
            subscriber._ShortCodeTranslate = (parameterDictionary.ContainsKey("ShortCodeTranslate")) ? parameterDictionary["ShortCodeTranslate"].ToString() : null;
            subscriber._ReminderCall = (parameterDictionary.ContainsKey("ReminderCall")) ? parameterDictionary["ReminderCall"].ToString() : null;
            subscriber._SetTZPath = (parameterDictionary.ContainsKey("SetTZPath")) ? parameterDictionary["SetTZPath"].ToString() : null;
            subscriber._InhibitIncomingFwd = (parameterDictionary.ContainsKey("InhibitIncomingFwd")) ? parameterDictionary["InhibitIncomingFwd"].ToString() : null;
            subscriber._BearerBasedCallFwd = (parameterDictionary.ContainsKey("BearerBasedCallFwd")) ? parameterDictionary["BearerBasedCallFwd"].ToString() : null;
            subscriber._TimeOriginatedRC = (parameterDictionary.ContainsKey("TimeOriginatedRC")) ? parameterDictionary["TimeOriginatedRC"].ToString() : null;
            subscriber._MobileDMS = (parameterDictionary.ContainsKey("MobileDMS")) ? parameterDictionary["MobileDMS"].ToString() : null;
            subscriber._MLPP = (parameterDictionary.ContainsKey("MLPP")) ? parameterDictionary["MLPP"].ToString() : null;
            subscriber._MusicOnHold = (parameterDictionary.ContainsKey("MusicOnHold")) ? parameterDictionary["MusicOnHold"].ToString() : null;
            subscriber._MiRingback = (parameterDictionary.ContainsKey("MiRingback")) ? parameterDictionary["MiRingback"].ToString() : null;
            subscriber._SelNomadicBlocking = (parameterDictionary.ContainsKey("SelNomadicBlocking")) ? parameterDictionary["SelNomadicBlocking"].ToString() : null;
            subscriber._AutoDial = (parameterDictionary.ContainsKey("AutoDial")) ? parameterDictionary["AutoDial"].ToString() : null;
            subscriber._CallForwardingDefault = (parameterDictionary.ContainsKey("CallForwardingDefault")) ? parameterDictionary["CallForwardingDefault"].ToString() : null;
            subscriber._CCBS = (parameterDictionary.ContainsKey("CCBS")) ? parameterDictionary["CCBS"].ToString() : null;
            subscriber._BlockCCBS = (parameterDictionary.ContainsKey("BlockCCBS")) ? parameterDictionary["BlockCCBS"].ToString() : null;
            subscriber._AINFeatcodeTrigger = (parameterDictionary.ContainsKey("AINFeatcodeTrigger")) ? parameterDictionary["AINFeatcodeTrigger"].ToString() : null;
            subscriber._AINPDPcodeTrigger = (parameterDictionary.ContainsKey("AINPDPcodeTrigger")) ? parameterDictionary["AINPDPcodeTrigger"].ToString() : null;
            subscriber._AINOHDTrigger = (parameterDictionary.ContainsKey("AINOHDTrigger")) ? parameterDictionary["AINOHDTrigger"].ToString() : null;
            subscriber._AINTATTrigger = (parameterDictionary.ContainsKey("AINTATTrigger")) ? parameterDictionary["AINTATTrigger"].ToString() : null;
            subscriber._GLSAccess = (parameterDictionary.ContainsKey("GLSAccess")) ? parameterDictionary["GLSAccess"].ToString() : null;
            subscriber._SharedNumber = (parameterDictionary.ContainsKey("SharedNumber")) ? parameterDictionary["SharedNumber"].ToString() : null;
            subscriber._CallDeflection = (parameterDictionary.ContainsKey("CallDeflection")) ? parameterDictionary["CallDeflection"].ToString() : null;
            subscriber._AutoAttendant = (parameterDictionary.ContainsKey("AutoAttendant")) ? parameterDictionary["AutoAttendant"].ToString() : null;
            subscriber._AdjunctTerminating = (parameterDictionary.ContainsKey("AdjunctTerminating")) ? parameterDictionary["AdjunctTerminating"].ToString() : null;
            subscriber._OutgoingCallBarring = (parameterDictionary.ContainsKey("OutgoingCallBarring")) ? parameterDictionary["OutgoingCallBarring"].ToString() : null;
            subscriber._RDigitParms = (parameterDictionary.ContainsKey("RDigitParms")) ? parameterDictionary["RDigitParms"].ToString() : null;
            subscriber._TransitRouting = (parameterDictionary.ContainsKey("TransitRouting")) ? parameterDictionary["TransitRouting"].ToString() : null;
            subscriber._LocationNumber = (parameterDictionary.ContainsKey("LocationNumber")) ? parameterDictionary["LocationNumber"].ToString() : null;
            subscriber._FemtoBSR = (parameterDictionary.ContainsKey("FemtoBSR")) ? parameterDictionary["FemtoBSR"].ToString() : null;
            subscriber._GeneralReset = (parameterDictionary.ContainsKey("GeneralReset")) ? parameterDictionary["GeneralReset"].ToString() : null;
            subscriber._Metering = (parameterDictionary.ContainsKey("Metering")) ? parameterDictionary["Metering"].ToString() : null;
            subscriber._PinService = (parameterDictionary.ContainsKey("PinService")) ? parameterDictionary["PinService"].ToString() : null;
            subscriber._WholeSale = (parameterDictionary.ContainsKey("WholeSale")) ? parameterDictionary["WholeSale"].ToString() : null;
            subscriber._SpareService = (parameterDictionary.ContainsKey("SpareService")) ? parameterDictionary["SpareService"].ToString() : null;
            subscriber._UseHoldAnnouncement = (parameterDictionary.ContainsKey("UseHoldAnnouncement")) ? parameterDictionary["UseHoldAnnouncement"].ToString() : null;
            subscriber._VPNDialingAndDisplay = (parameterDictionary.ContainsKey("VPNDialingAndDisplay")) ? parameterDictionary["VPNDialingAndDisplay"].ToString() : null;
            subscriber._PriorityModeCalling = (parameterDictionary.ContainsKey("PriorityModeCalling")) ? parameterDictionary["PriorityModeCalling"].ToString() : null;
            subscriber._OneNumber = (parameterDictionary.ContainsKey("OneNumber")) ? parameterDictionary["OneNumber"].ToString() : null;
            subscriber._AdvancedConference = (parameterDictionary.ContainsKey("AdvancedConference")) ? parameterDictionary["AdvancedConference"].ToString() : null;
            subscriber._AnswConfirm = (parameterDictionary.ContainsKey("AnswConfirm")) ? parameterDictionary["AnswConfirm"].ToString() : null;
            subscriber._OnlineCharging = (parameterDictionary.ContainsKey("OnlineCharging")) ? parameterDictionary["OnlineCharging"].ToString() : null;
            subscriber._VideoCallRouting = (parameterDictionary.ContainsKey("VideoCallRouting")) ? parameterDictionary["VideoCallRouting"].ToString() : null;
            subscriber._LisInVMRescue = (parameterDictionary.ContainsKey("LisInVMRescue")) ? parameterDictionary["LisInVMRescue"].ToString() : null;
            subscriber._EndpointValidation = (parameterDictionary.ContainsKey("EndpointValidation")) ? parameterDictionary["EndpointValidation"].ToString() : null;
            subscriber._ParlayRest = (parameterDictionary.ContainsKey("ParlayRest")) ? parameterDictionary["ParlayRest"].ToString() : null;
            subscriber._AltRtToPBX = (parameterDictionary.ContainsKey("AltRtToPBX")) ? parameterDictionary["AltRtToPBX"].ToString() : null;
            subscriber._WAMAccess = (parameterDictionary.ContainsKey("WAMAccess")) ? parameterDictionary["WAMAccess"].ToString() : null;
            subscriber._ISDN = (parameterDictionary.ContainsKey("ISDN")) ? parameterDictionary["ISDN"].ToString() : null;
            subscriber._LocaleParms = (parameterDictionary.ContainsKey("LocaleParms")) ? parameterDictionary["LocaleParms"].ToString() : null;
            subscriber._VASI = (parameterDictionary.ContainsKey("VASI")) ? parameterDictionary["VASI"].ToString() : null;
            subscriber._LocBasedRedirect = (parameterDictionary.ContainsKey("LocBasedRedirect")) ? parameterDictionary["LocBasedRedirect"].ToString() : null;
            subscriber._CallLimitGroup = (parameterDictionary.ContainsKey("CallLimitGroup")) ? parameterDictionary["CallLimitGroup"].ToString() : null;
            subscriber._AlternateCharging = (parameterDictionary.ContainsKey("AlternateCharging")) ? parameterDictionary["AlternateCharging"].ToString() : null;
            subscriber._CallForwardNotReach = (parameterDictionary.ContainsKey("CallForwardNotReach")) ? parameterDictionary["CallForwardNotReach"].ToString() : null;
            subscriber._MultiDevicesPUID = (parameterDictionary.ContainsKey("MultiDevicesPUID")) ? parameterDictionary["MultiDevicesPUID"].ToString() : null;
            subscriber._ManagedCallRec = (parameterDictionary.ContainsKey("ManagedCallRec")) ? parameterDictionary["ManagedCallRec"].ToString() : null;
            subscriber._GeoRedundancyData = (parameterDictionary.ContainsKey("GeoRedundancyData")) ? parameterDictionary["GeoRedundancyData"].ToString() : null;

            subscriber.Created = DateTime.UtcNow.AddHours(3);
            subscriber.Updated = DateTime.UtcNow.AddHours(3);
            subscriber.Inspected = DateTime.UtcNow.AddHours(3);
            subscriber.UserId = Guid.Empty;

            return subscriber;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool CallingLineIdIsAssigned(string text)
        {
            bool b;
            string s1, s2;

            if (text != null)
            {
                s1 = "<CallingLineIdPresentation>true</CallingLineIdPresentation>";
                s2 = "<ConnectedLinePresentation>true</ConnectedLinePresentation>";

                if (text.Contains(s1) && text.Contains(s2)) b = true;
                else b = false;
            }
            else b = false;

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool OneDigitSpeedDialIsAssigned(string text)
        {
            bool b;
            string s;

            if (text != null)
            {
                s = "<Assigned>true</Assigned>";

                if (text.Contains(s)) b = true;
                else b = false;
            }
            else b = false;

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool CallForwardingIsAssigned(string text)
        {
            bool b;
            string s;

            if (text != null)
            {
                s = "<Assigned>true</Assigned>";

                if (text.Contains(s)) b = true;
                else b = false;
            }
            else b = false;

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool CallWaitingIsAssigned(string subscriberCallWaitingContent, bool agcfEndpointCallWaitingLc)
        {
            bool b;
            string s;

            if (subscriberCallWaitingContent != null)
            {
                s = "<Assigned>true</Assigned>";

                // uncomment later
                if (subscriberCallWaitingContent.Contains(s) /*&& agcfEndpointCallWaitingLc*/) b = true;
                else b = false;
            }
            else b = false;

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool ConferenceCallIsAssigned(string text)
        {
            bool b;
            string s;

            if (text != null)
            {
                s = "<Assigned>true</Assigned>";

                if (text.Contains(s)) b = true;
                else b = false;
            }
            else b = false;

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool InternationalCallingIsAssigned(string text)
        {
            bool b;
            string s;

            // important: true means NOT CallBarring international is not allowed

            if (text != null)
            {
                s = "<International>true</International>";

                if (text.Contains(s)) b = true;
                else b = false;
            }
            else b = false;

            return !b; // see
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool InternationalCallingUserControlledIsAssigned(string text)
        {
            bool b;
            string s;

            if (text != null)
            {
                s = "<Assigned>true</Assigned>";

                if (text.Contains(s)) b = true;
                else b = false;
            }
            else b = false;

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool ReminderCallIsAssigned(string text)
        {
            bool b;
            string s;

            if (text != null)
            {
                s = "<Assigned>true</Assigned>";

                if (text.Contains(s)) b = true;
                else b = false;
            }
            else b = false;

            return b;
        }


        /*
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool ServiceSuspensionIsAssigned(string _ServiceSuspension)
        {
            bool b;
            string s;

            s = "<ServiceSuspension>true</ServiceSuspension>";

            if (_ServiceSuspension.Contains(s)) b = true;
            else b = false;

            return b;
        }
         */

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}