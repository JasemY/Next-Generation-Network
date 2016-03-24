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

namespace Ia.Ngn.Cl.Model.Nokia
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Subscriber Entity Framework class for Next Generation Network (NGN) entity model.
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
        /// <summary>
        /// 1360 COM WebAPI User Guide 255-400-419R3.X
        /// ngfs-subscriber-v2 (rtrv,ent,ed,dlt)
        /// The ngfs-subscriber is a composite of the following groups. Each group is further defined in the subsections
        /// below. PartyID is a unique identifier for a subscriber. PublicID, or PUID, is the public telephone number for the
        /// telephone device. All PUIDs are E.164 numbers, including the leading +.
        /// </summary>
        public Subscriber() { }

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        /// <summary>
        /// PartyId (req for ent,req for ed). LCP Name for Party record. String (1-27) [+0-9a-zA-Z:@&amp;=!,|-_.!~*()%]* Before CTS R5.0: [+0-9a-zA-Z:@&amp;=!,-_.!~*'()%]* LCP-CTS R6.1 From CTS R6.1: [+0-9a-zA-Z:@&amp;=,|-_.!~*'()%#?/$][]* If the String Ends with '[AutoSelect]', '[AutoSelectSTAS]', or '[AutoSelectDTAS]' The Subscriber will be automatically distributed to the TASs. When [AutoSelect],[AutoSelectDTAS] or [AutoSelectSTAS] is used in ent- request, AssocOtasRealm should be empty. And the OTAS which has fewest subscriber will be chosen as AssocOtasRealm. For non-ICS subscriber request (do not include iHss private id info), [AutoSelect] can be used. For the subscriber request which include iHss Private Id info, [AutoSelectDTAS] and [AutoSelectSTAS] can be used. And the Realm in OTAS must match the "ApplicateionServer" defined in iHss Global IFC and this IFC must be used by iHss Service profile. Otherwise, error will be reported thant no service profile for this OTAS. String length does not include length of '[AutoSelect]', '[AutoSelectSTAS]', and '[AutoSelectDTAS]'. LCP-CTS R6.2.1 The new partyId. Used for changing partyId. Only used for ed command. Range same as PartyId.
        /// </summary>
        public string PartyId { get; set; }

        /// <summary>
        /// AlternateOtasRealm (opt for ent/ed). Alternate TAS that supports this party. String (0-63) [0-9a-zA-Z\.\-]* If the string equals to '[AutoSelect]', the protection subscriber will be automatically distributed to the TASs.
        /// </summary>
        public string AlternateOtasRealm { get; set; }

        /// <summary>
        /// AnonymousCallRej
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AnonymousCallRej { get; set; }
        [NotMapped]
        public XElement AnonymousCallRej
        {
            get { return XElement.Parse(_AnonymousCallRej); }
            set { _AnonymousCallRej = value.ToString(); }
        }

        /// <summary>
        /// SelectiveCallRejct
        /// </summary>
        [Column(TypeName = "xml")]
        public string _SelectiveCallRejct { get; set; }

        [NotMapped]
        public XElement SelectiveCallRejct
        {
            get { return XElement.Parse(_SelectiveCallRejct); }
            set { _SelectiveCallRejct = value.ToString(); }
        }

        /// <summary>
        /// SelectiveCallAcpt
        /// </summary>
        [Column(TypeName = "xml")]
        public string _SelectiveCallAcpt { get; set; }

        [NotMapped]
        public XElement SelectiveCallAcpt
        {
            get { return XElement.Parse(_SelectiveCallAcpt); }
            set { _SelectiveCallAcpt = value.ToString(); }
        }

        /// <summary>
        /// AutomaticCallBack
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AutomaticCallBack { get; set; }

        [NotMapped]
        public XElement AutomaticCallBack
        {
            get { return XElement.Parse(_AutomaticCallBack); }
            set { _AutomaticCallBack = value.ToString(); }
        }

        /// <summary>
        /// AutomaticRecall
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AutomaticRecall { get; set; }

        [NotMapped]
        public XElement AutomaticRecall
        {
            get { return XElement.Parse(_AutomaticRecall); }
            set { _AutomaticRecall = value.ToString(); }
        }

        /// <summary>
        /// CallingLineId
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallingLineId { get; set; }

        [NotMapped]
        public XElement CallingLineId
        {
            get { return XElement.Parse(_CallingLineId); }
            set { _CallingLineId = value.ToString(); }
        }

        /// <summary>
        /// RemoteAccessServer
        /// </summary>
        [Column(TypeName = "xml")]
        public string _RemoteAccessServer { get; set; }

        [NotMapped]
        public XElement RemoteAccessServer
        {
            get { return XElement.Parse(_RemoteAccessServer); }
            set { _RemoteAccessServer = value.ToString(); }
        }

        /// <summary>
        /// CallBarring
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallBarring { get; set; }

        [NotMapped]
        public XElement CallBarring
        {
            get { return XElement.Parse(_CallBarring); }
            set { _CallBarring = value.ToString(); }
        }

        /// <summary>
        /// CallBlocking
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallBlocking { get; set; }

        [NotMapped]
        public XElement CallBlocking
        {
            get { return XElement.Parse(_CallBlocking); }
            set { _CallBlocking = value.ToString(); }
        }

        /// <summary>
        /// CallTransfer
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallTransfer { get; set; }

        [NotMapped]
        public XElement CallTransfer
        {
            get { return XElement.Parse(_CallTransfer); }
            set { _CallTransfer = value.ToString(); }
        }

        /// <summary>
        /// CallWaiting
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallWaiting { get; set; }

        [NotMapped]
        public XElement CallWaiting
        {
            get { return XElement.Parse(_CallWaiting); }
            set { _CallWaiting = value.ToString(); }
        }

        /// <summary>
        /// CallForwardingVari
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallForwardingVari { get; set; }

        [NotMapped]
        public XElement CallForwardingVari
        {
            get { return XElement.Parse(_CallForwardingVari); }
            set { _CallForwardingVari = value.ToString(); }
        }

        /// <summary>
        /// CallForwardingBusy
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallForwardingBusy { get; set; }

        [NotMapped]
        public XElement CallForwardingBusy
        {
            get { return XElement.Parse(_CallForwardingBusy); }
            set { _CallForwardingBusy = value.ToString(); }
        }

        /// <summary>
        /// CallForwardingNoAns
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallForwardingNoAns { get; set; }

        [NotMapped]
        public XElement CallForwardingNoAns
        {
            get { return XElement.Parse(_CallForwardingNoAns); }
            set { _CallForwardingNoAns = value.ToString(); }
        }

        /// <summary>
        /// SelectiveCallFwd
        /// </summary>
        [Column(TypeName = "xml")]
        public string _SelectiveCallFwd { get; set; }

        [NotMapped]
        public XElement SelectiveCallFwd
        {
            get { return XElement.Parse(_SelectiveCallFwd); }
            set { _SelectiveCallFwd = value.ToString(); }
        }

        /// <summary>
        /// CallForwardingUnreg
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallForwardingUnreg { get; set; }

        [NotMapped]
        public XElement CallForwardingUnreg
        {
            get { return XElement.Parse(_CallForwardingUnreg); }
            set { _CallForwardingUnreg = value.ToString(); }
        }

        /// <summary>
        /// CustomerOrigTrace
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CustomerOrigTrace { get; set; }

        [NotMapped]
        public XElement CustomerOrigTrace
        {
            get { return XElement.Parse(_CustomerOrigTrace); }
            set { _CustomerOrigTrace = value.ToString(); }
        }

        /// <summary>
        /// NuisanceCallTrace
        /// </summary>
        [Column(TypeName = "xml")]
        public string _NuisanceCallTrace { get; set; }

        [NotMapped]
        public XElement NuisanceCallTrace
        {
            get { return XElement.Parse(_NuisanceCallTrace); }
            set { _NuisanceCallTrace = value.ToString(); }
        }

        /// <summary>
        /// DoNotDisturb
        /// </summary>
        [Column(TypeName = "xml")]
        public string _DoNotDisturb { get; set; }

        [NotMapped]
        public XElement DoNotDisturb
        {
            get { return XElement.Parse(_DoNotDisturb); }
            set { _DoNotDisturb = value.ToString(); }
        }

        /// <summary>
        /// MsgWaitingInd
        /// </summary>
        [Column(TypeName = "xml")]
        public string _MsgWaitingInd { get; set; }

        [NotMapped]
        public XElement MsgWaitingInd
        {
            get { return XElement.Parse(_MsgWaitingInd); }
            set { _MsgWaitingInd = value.ToString(); }
        }

        /// <summary>
        /// SimultaneousRinging
        /// </summary>
        [Column(TypeName = "xml")]
        public string _SimultaneousRinging { get; set; }

        [NotMapped]
        public XElement SimultaneousRinging
        {
            get { return XElement.Parse(_SimultaneousRinging); }
            set { _SimultaneousRinging = value.ToString(); }
        }

        /// <summary>
        /// WebPortal
        /// </summary>
        [Column(TypeName = "xml")]
        public string _WebPortal { get; set; }

        [NotMapped]
        public XElement WebPortal
        {
            get { return XElement.Parse(_WebPortal); }
            set { _WebPortal = value.ToString(); }
        }

        /// <summary>
        /// ConferenceCalling
        /// </summary>
        [Column(TypeName = "xml")]
        public string _ConferenceCalling { get; set; }

        [NotMapped]
        public XElement ConferenceCalling
        {
            get { return XElement.Parse(_ConferenceCalling); }
            set { _ConferenceCalling = value.ToString(); }
        }

        /// <summary>
        /// FlashOrigServices
        /// </summary>
        [Column(TypeName = "xml")]
        public string _FlashOrigServices { get; set; }

        [NotMapped]
        public XElement FlashOrigServices
        {
            get { return XElement.Parse(_FlashOrigServices); }
            set { _FlashOrigServices = value.ToString(); }
        }

        /// <summary>
        /// CarrierSelection
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CarrierSelection { get; set; }

        [NotMapped]
        public XElement CarrierSelection
        {
            get { return XElement.Parse(_CarrierSelection); }
            set { _CarrierSelection = value.ToString(); }
        }

        /// <summary>
        /// RingbackWhenFree
        /// </summary>
        [Column(TypeName = "xml")]
        public string _RingbackWhenFree { get; set; }

        [NotMapped]
        public XElement RingbackWhenFree
        {
            get { return XElement.Parse(_RingbackWhenFree); }
            set { _RingbackWhenFree = value.ToString(); }
        }

        /// <summary>
        /// MultipleRingPattern
        /// </summary>
        [Column(TypeName = "xml")]
        public string _MultipleRingPattern { get; set; }

        [NotMapped]
        public XElement MultipleRingPattern
        {
            get { return XElement.Parse(_MultipleRingPattern); }
            set { _MultipleRingPattern = value.ToString(); }
        }

        /// <summary>
        /// CallForwardingLocal
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallForwardingLocal { get; set; }

        [NotMapped]
        public XElement CallForwardingLocal
        {
            get { return XElement.Parse(_CallForwardingLocal); }
            set { _CallForwardingLocal = value.ToString(); }
        }

        /// <summary>
        /// RemoteAccessServices
        /// </summary>
        [Column(TypeName = "xml")]
        public string _RemoteAccessServices { get; set; }

        [NotMapped]
        public XElement RemoteAccessServices
        {
            get { return XElement.Parse(_RemoteAccessServices); }
            set { _RemoteAccessServices = value.ToString(); }
        }

        /// <summary>
        /// VoiceMail
        /// </summary>
        [Column(TypeName = "xml")]
        public string _VoiceMail { get; set; }

        [NotMapped]
        public XElement VoiceMail
        {
            get { return XElement.Parse(_VoiceMail); }
            set { _VoiceMail = value.ToString(); }
        }

        /// <summary>
        /// InterceptReferral
        /// </summary>
        [Column(TypeName = "xml")]
        public string _InterceptReferral { get; set; }

        [NotMapped]
        public XElement InterceptReferral
        {
            get { return XElement.Parse(_InterceptReferral); }
            set { _InterceptReferral = value.ToString(); }
        }

        /// <summary>
        /// DialingPlan
        /// </summary>
        [Column(TypeName = "xml")]
        public string _DialingPlan { get; set; }

        [NotMapped]
        public XElement DialingPlan
        {
            get { return XElement.Parse(_DialingPlan); }
            set { _DialingPlan = value.ToString(); }
        }

        /// <summary>
        /// HSSPrivateId
        /// </summary>
        [Column(TypeName = "xml")]
        public string _HSSPrivateId { get; set; }

        [NotMapped]
        public XElement HSSPrivateId
        {
            get { return XElement.Parse(_HSSPrivateId); }
            set { _HSSPrivateId = value.ToString(); }
        }

        /// <summary>
        /// HSSPublicIdCustom
        /// </summary>
        [Column(TypeName = "xml")]
        public string _HSSPublicIdCustom { get; set; }

        [NotMapped]
        public XElement HSSPublicIdCustom
        {
            get { return XElement.Parse(_HSSPublicIdCustom); }
            set { _HSSPublicIdCustom = value.ToString(); }
        }

        /// <summary>
        /// AcctCodes
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AcctCodes { get; set; }

        [NotMapped]
        public XElement AcctCodes
        {
            get { return XElement.Parse(_AcctCodes); }
            set { _AcctCodes = value.ToString(); }
        }

        /// <summary>
        /// SeqRinging
        /// </summary>
        [Column(TypeName = "xml")]
        public string _SeqRinging { get; set; }

        [NotMapped]
        public XElement SeqRinging
        {
            get { return XElement.Parse(_SeqRinging); }
            set { _SeqRinging = value.ToString(); }
        }

        /// <summary>
        /// AuthCodeService
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AuthCodeService { get; set; }

        [NotMapped]
        public XElement AuthCodeService
        {
            get { return XElement.Parse(_AuthCodeService); }
            set { _AuthCodeService = value.ToString(); }
        }

        /// <summary>
        /// MLHGNoHuntMember
        /// </summary>
        [Column(TypeName = "xml")]
        public string _MLHGNoHuntMember { get; set; }

        [NotMapped]
        public XElement MLHGNoHuntMember
        {
            get { return XElement.Parse(_MLHGNoHuntMember); }
            set { _MLHGNoHuntMember = value.ToString(); }
        }

        /// <summary>
        /// MultilineHuntGroup
        /// </summary>
        [Column(TypeName = "xml")]
        public string _MultilineHuntGroup { get; set; }

        [NotMapped]
        public XElement MultilineHuntGroup
        {
            get { return XElement.Parse(_MultilineHuntGroup); }
            set { _MultilineHuntGroup = value.ToString(); }
        }

        /// <summary>
        /// CallPickupOrig
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallPickupOrig { get; set; }

        [NotMapped]
        public XElement CallPickupOrig
        {
            get { return XElement.Parse(_CallPickupOrig); }
            set { _CallPickupOrig = value.ToString(); }
        }

        /// <summary>
        /// CallPickupTerm
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallPickupTerm { get; set; }

        [NotMapped]
        public XElement CallPickupTerm
        {
            get { return XElement.Parse(_CallPickupTerm); }
            set { _CallPickupTerm = value.ToString(); }
        }

        /// <summary>
        /// Attendant
        /// </summary>
        [Column(TypeName = "xml")]
        public string _Attendant { get; set; }

        [NotMapped]
        public XElement Attendant
        {
            get { return XElement.Parse(_Attendant); }
            set { _Attendant = value.ToString(); }
        }

        /// <summary>
        /// AttendantServer
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AttendantServer { get; set; }

        [NotMapped]
        public XElement AttendantServer
        {
            get { return XElement.Parse(_AttendantServer); }
            set { _AttendantServer = value.ToString(); }
        }

        /// <summary>
        /// CallPark
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallPark { get; set; }

        [NotMapped]
        public XElement CallPark
        {
            get { return XElement.Parse(_CallPark); }
            set { _CallPark = value.ToString(); }
        }

        /// <summary>
        /// DirectedGroup
        /// </summary>
        [Column(TypeName = "xml")]
        public string _DirectedGroup { get; set; }

        [NotMapped]
        public XElement DirectedGroup
        {
            get { return XElement.Parse(_DirectedGroup); }
            set { _DirectedGroup = value.ToString(); }
        }

        /// <summary>
        /// RemoteUser
        /// </summary>
        [Column(TypeName = "xml")]
        public string _RemoteUser { get; set; }

        [NotMapped]
        public XElement RemoteUser
        {
            get { return XElement.Parse(_RemoteUser); }
            set { _RemoteUser = value.ToString(); }
        }

        /// <summary>
        /// TransferToUsersVM
        /// </summary>
        [Column(TypeName = "xml")]
        public string _TransferToUsersVM { get; set; }

        [NotMapped]
        public XElement TransferToUsersVM
        {
            get { return XElement.Parse(_TransferToUsersVM); }
            set { _TransferToUsersVM = value.ToString(); }
        }

        /// <summary>
        /// FlexCallingLineId
        /// </summary>
        [Column(TypeName = "xml")]
        public string _FlexCallingLineId { get; set; }

        [NotMapped]
        public XElement FlexCallingLineId
        {
            get { return XElement.Parse(_FlexCallingLineId); }
            set { _FlexCallingLineId = value.ToString(); }
        }

        /// <summary>
        /// ExtensionDevice
        /// </summary>
        [Column(TypeName = "xml")]
        public string _ExtensionDevice { get; set; }

        [NotMapped]
        public XElement ExtensionDevice
        {
            get { return XElement.Parse(_ExtensionDevice); }
            set { _ExtensionDevice = value.ToString(); }
        }

        /// <summary>
        /// PSIServer
        /// </summary>
        [Column(TypeName = "xml")]
        public string _PSIServer { get; set; }

        [NotMapped]
        public XElement PSIServer
        {
            get { return XElement.Parse(_PSIServer); }
            set { _PSIServer = value.ToString(); }
        }

        /// <summary>
        /// ClosedUserGroup
        /// </summary>
        [Column(TypeName = "xml")]
        public string _ClosedUserGroup { get; set; }

        [NotMapped]
        public XElement ClosedUserGroup
        {
            get { return XElement.Parse(_ClosedUserGroup); }
            set { _ClosedUserGroup = value.ToString(); }
        }

        /// <summary>
        /// OneDigitSpeedDial
        /// </summary>
        [Column(TypeName = "xml")]
        public string _OneDigitSpeedDial { get; set; }

        [NotMapped]
        public XElement OneDigitSpeedDial
        {
            get { return XElement.Parse(_OneDigitSpeedDial); }
            set { _OneDigitSpeedDial = value.ToString(); }
        }

        /// <summary>
        /// TwoDigitSpeedDial
        /// </summary>
        [Column(TypeName = "xml")]
        public string _TwoDigitSpeedDial { get; set; }

        [NotMapped]
        public XElement TwoDigitSpeedDial
        {
            get { return XElement.Parse(_TwoDigitSpeedDial); }
            set { _TwoDigitSpeedDial = value.ToString(); }
        }

        /// <summary>
        /// ExtensionServer
        /// </summary>
        [Column(TypeName = "xml")]
        public string _ExtensionServer { get; set; }

        [NotMapped]
        public XElement ExtensionServer
        {
            get { return XElement.Parse(_ExtensionServer); }
            set { _ExtensionServer = value.ToString(); }
        }

        /// <summary>
        /// SelectiveAlert
        /// </summary>
        [Column(TypeName = "xml")]
        public string _SelectiveAlert { get; set; }

        [NotMapped]
        public XElement SelectiveAlert
        {
            get { return XElement.Parse(_SelectiveAlert); }
            set { _SelectiveAlert = value.ToString(); }
        }

        /// <summary>
        /// ShortCodeTranslate
        /// </summary>
        [Column(TypeName = "xml")]
        public string _ShortCodeTranslate { get; set; }

        [NotMapped]
        public XElement ShortCodeTranslate
        {
            get { return XElement.Parse(_ShortCodeTranslate); }
            set { _ShortCodeTranslate = value.ToString(); }
        }

        /// <summary>
        /// ReminderCall
        /// </summary>
        [Column(TypeName = "xml")]
        public string _ReminderCall { get; set; }

        [NotMapped]
        public XElement ReminderCall
        {
            get { return XElement.Parse(_ReminderCall); }
            set { _ReminderCall = value.ToString(); }
        }

        /// <summary>
        /// SetTZPath
        /// </summary>
        [Column(TypeName = "xml")]
        public string _SetTZPath { get; set; }

        [NotMapped]
        public XElement SetTZPath
        {
            get { return XElement.Parse(_SetTZPath); }
            set { _SetTZPath = value.ToString(); }
        }

        /// <summary>
        /// InhibitIncomingFwd
        /// </summary>
        [Column(TypeName = "xml")]
        public string _InhibitIncomingFwd { get; set; }

        [NotMapped]
        public XElement InhibitIncomingFwd
        {
            get { return XElement.Parse(_InhibitIncomingFwd); }
            set { _InhibitIncomingFwd = value.ToString(); }
        }

        /// <summary>
        /// BearerBasedCallFwd
        /// </summary>
        [Column(TypeName = "xml")]
        public string _BearerBasedCallFwd { get; set; }

        [NotMapped]
        public XElement BearerBasedCallFwd
        {
            get { return XElement.Parse(_BearerBasedCallFwd); }
            set { _BearerBasedCallFwd = value.ToString(); }
        }

        /// <summary>
        /// TimeOriginatedRC
        /// </summary>
        [Column(TypeName = "xml")]
        public string _TimeOriginatedRC { get; set; }

        [NotMapped]
        public XElement TimeOriginatedRC
        {
            get { return XElement.Parse(_TimeOriginatedRC); }
            set { _TimeOriginatedRC = value.ToString(); }
        }

        /// <summary>
        /// MobileDMS
        /// </summary>
        [Column(TypeName = "xml")]
        public string _MobileDMS { get; set; }

        [NotMapped]
        public XElement MobileDMS
        {
            get { return XElement.Parse(_MobileDMS); }
            set { _MobileDMS = value.ToString(); }
        }

        /// <summary>
        /// MLPP
        /// </summary>
        [Column(TypeName = "xml")]
        public string _MLPP { get; set; }

        [NotMapped]
        public XElement MLPP
        {
            get { return XElement.Parse(_MLPP); }
            set { _MLPP = value.ToString(); }
        }

        /// <summary>
        /// MusicOnHold
        /// </summary>
        [Column(TypeName = "xml")]
        public string _MusicOnHold { get; set; }

        [NotMapped]
        public XElement MusicOnHold
        {
            get { return XElement.Parse(_MusicOnHold); }
            set { _MusicOnHold = value.ToString(); }
        }

        /// <summary>
        /// MiRingback
        /// </summary>
        [Column(TypeName = "xml")]
        public string _MiRingback { get; set; }

        [NotMapped]
        public XElement MiRingback
        {
            get { return XElement.Parse(_MiRingback); }
            set { _MiRingback = value.ToString(); }
        }

        /// <summary>
        /// SelNomadicBlocking
        /// </summary>
        [Column(TypeName = "xml")]
        public string _SelNomadicBlocking { get; set; }

        [NotMapped]
        public XElement SelNomadicBlocking
        {
            get { return XElement.Parse(_SelNomadicBlocking); }
            set { _SelNomadicBlocking = value.ToString(); }
        }

        /// <summary>
        /// AutoDial
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AutoDial { get; set; }

        [NotMapped]
        public XElement AutoDial
        {
            get { return XElement.Parse(_AutoDial); }
            set { _AutoDial = value.ToString(); }
        }

        /// <summary>
        /// CallForwardingDefault
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallForwardingDefault { get; set; }

        [NotMapped]
        public XElement CallForwardingDefault
        {
            get { return XElement.Parse(_CallForwardingDefault); }
            set { _CallForwardingDefault = value.ToString(); }
        }

        /// <summary>
        /// CCBS
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CCBS { get; set; }

        [NotMapped]
        public XElement CCBS
        {
            get { return XElement.Parse(_CCBS); }
            set { _CCBS = value.ToString(); }
        }

        /// <summary>
        /// BlockCCBS
        /// </summary>
        [Column(TypeName = "xml")]
        public string _BlockCCBS { get; set; }

        [NotMapped]
        public XElement BlockCCBS
        {
            get { return XElement.Parse(_BlockCCBS); }
            set { _BlockCCBS = value.ToString(); }
        }

        /// <summary>
        /// AINFeatcodeTrigger
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AINFeatcodeTrigger { get; set; }

        [NotMapped]
        public XElement AINFeatcodeTrigger
        {
            get { return XElement.Parse(_AINFeatcodeTrigger); }
            set { _AINFeatcodeTrigger = value.ToString(); }
        }

        /// <summary>
        /// AINPDPcodeTrigger
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AINPDPcodeTrigger { get; set; }

        [NotMapped]
        public XElement AINPDPcodeTrigger
        {
            get { return XElement.Parse(_AINPDPcodeTrigger); }
            set { _AINPDPcodeTrigger = value.ToString(); }
        }

        /// <summary>
        /// AINOHDTrigger
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AINOHDTrigger { get; set; }

        [NotMapped]
        public XElement AINOHDTrigger
        {
            get { return XElement.Parse(_AINOHDTrigger); }
            set { _AINOHDTrigger = value.ToString(); }
        }

        /// <summary>
        /// AINTATTrigger
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AINTATTrigger { get; set; }

        [NotMapped]
        public XElement AINTATTrigger
        {
            get { return XElement.Parse(_AINTATTrigger); }
            set { _AINTATTrigger = value.ToString(); }
        }

        /// <summary>
        /// GLSAccess
        /// </summary>
        [Column(TypeName = "xml")]
        public string _GLSAccess { get; set; }

        [NotMapped]
        public XElement GLSAccess
        {
            get { return XElement.Parse(_GLSAccess); }
            set { _GLSAccess = value.ToString(); }
        }

        /// <summary>
        /// SharedNumber
        /// </summary>
        [Column(TypeName = "xml")]
        public string _SharedNumber { get; set; }

        [NotMapped]
        public XElement SharedNumber
        {
            get { return XElement.Parse(_SharedNumber); }
            set { _SharedNumber = value.ToString(); }
        }

        /// <summary>
        /// CallDeflection
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallDeflection { get; set; }

        [NotMapped]
        public XElement CallDeflection
        {
            get { return XElement.Parse(_CallDeflection); }
            set { _CallDeflection = value.ToString(); }
        }

        /// <summary>
        /// AutoAttendant
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AutoAttendant { get; set; }

        [NotMapped]
        public XElement AutoAttendant
        {
            get { return XElement.Parse(_AutoAttendant); }
            set { _AutoAttendant = value.ToString(); }
        }

        /// <summary>
        /// AdjunctTerminating
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AdjunctTerminating { get; set; }

        [NotMapped]
        public XElement AdjunctTerminating
        {
            get { return XElement.Parse(_AdjunctTerminating); }
            set { _AdjunctTerminating = value.ToString(); }
        }

        /// <summary>
        /// OutgoingCallBarring
        /// </summary>
        [Column(TypeName = "xml")]
        public string _OutgoingCallBarring { get; set; }

        [NotMapped]
        public XElement OutgoingCallBarring
        {
            get { return XElement.Parse(_OutgoingCallBarring); }
            set { _OutgoingCallBarring = value.ToString(); }
        }

        /// <summary>
        /// RDigitParms
        /// </summary>
        [Column(TypeName = "xml")]
        public string _RDigitParms { get; set; }

        [NotMapped]
        public XElement RDigitParms
        {
            get { return XElement.Parse(_RDigitParms); }
            set { _RDigitParms = value.ToString(); }
        }

        /// <summary>
        /// TransitRouting
        /// </summary>
        [Column(TypeName = "xml")]
        public string _TransitRouting { get; set; }

        [NotMapped]
        public XElement TransitRouting
        {
            get { return XElement.Parse(_TransitRouting); }
            set { _TransitRouting = value.ToString(); }
        }

        /// <summary>
        /// LocationNumber
        /// </summary>
        [Column(TypeName = "xml")]
        public string _LocationNumber { get; set; }

        [NotMapped]
        public XElement LocationNumber
        {
            get { return XElement.Parse(_LocationNumber); }
            set { _LocationNumber = value.ToString(); }
        }

        /// <summary>
        /// FemtoBSR
        /// </summary>
        [Column(TypeName = "xml")]
        public string _FemtoBSR { get; set; }

        [NotMapped]
        public XElement FemtoBSR
        {
            get { return XElement.Parse(_FemtoBSR); }
            set { _FemtoBSR = value.ToString(); }
        }

        /// <summary>
        /// GeneralReset
        /// </summary>
        [Column(TypeName = "xml")]
        public string _GeneralReset { get; set; }

        [NotMapped]
        public XElement GeneralReset
        {
            get { return XElement.Parse(_GeneralReset); }
            set { _GeneralReset = value.ToString(); }
        }

        /// <summary>
        /// Metering
        /// </summary>
        [Column(TypeName = "xml")]
        public string _Metering { get; set; }

        [NotMapped]
        public XElement Metering
        {
            get { return XElement.Parse(_Metering); }
            set { _Metering = value.ToString(); }
        }

        /// <summary>
        /// PinService
        /// </summary>
        [Column(TypeName = "xml")]
        public string _PinService { get; set; }

        [NotMapped]
        public XElement PinService
        {
            get { return XElement.Parse(_PinService); }
            set { _PinService = value.ToString(); }
        }

        /// <summary>
        /// WholeSale
        /// </summary>
        [Column(TypeName = "xml")]
        public string _WholeSale { get; set; }

        [NotMapped]
        public XElement WholeSale
        {
            get { return XElement.Parse(_WholeSale); }
            set { _WholeSale = value.ToString(); }
        }

        /// <summary>
        /// SpareService
        /// </summary>
        [Column(TypeName = "xml")]
        public string _SpareService { get; set; }

        [NotMapped]
        public XElement SpareService
        {
            get { return XElement.Parse(_SpareService); }
            set { _SpareService = value.ToString(); }
        }

        /// <summary>
        /// UseHoldAnnouncement
        /// </summary>
        [Column(TypeName = "xml")]
        public string _UseHoldAnnouncement { get; set; }

        [NotMapped]
        public XElement UseHoldAnnouncement
        {
            get { return XElement.Parse(_UseHoldAnnouncement); }
            set { _UseHoldAnnouncement = value.ToString(); }
        }

        /// <summary>
        /// VPNDialingAndDisplay
        /// </summary>
        [Column(TypeName = "xml")]
        public string _VPNDialingAndDisplay { get; set; }

        [NotMapped]
        public XElement VPNDialingAndDisplay
        {
            get { return XElement.Parse(_VPNDialingAndDisplay); }
            set { _VPNDialingAndDisplay = value.ToString(); }
        }

        /// <summary>
        /// PriorityModeCalling
        /// </summary>
        [Column(TypeName = "xml")]
        public string _PriorityModeCalling { get; set; }

        [NotMapped]
        public XElement PriorityModeCalling
        {
            get { return XElement.Parse(_PriorityModeCalling); }
            set { _PriorityModeCalling = value.ToString(); }
        }

        /// <summary>
        /// OneNumber
        /// </summary>
        [Column(TypeName = "xml")]
        public string _OneNumber { get; set; }

        [NotMapped]
        public XElement OneNumber
        {
            get { return XElement.Parse(_OneNumber); }
            set { _OneNumber = value.ToString(); }
        }

        /// <summary>
        /// AdvancedConference
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AdvancedConference { get; set; }

        [NotMapped]
        public XElement AdvancedConference
        {
            get { return XElement.Parse(_AdvancedConference); }
            set { _AdvancedConference = value.ToString(); }
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AnswConfirm { get; set; }

        [NotMapped]
        public XElement AnswConfirm
        {
            get { return XElement.Parse(_AnswConfirm); }
            set { _AnswConfirm = value.ToString(); }
        }

        /// <summary>
        /// OnlineCharging
        /// </summary>
        [Column(TypeName = "xml")]
        public string _OnlineCharging { get; set; }

        [NotMapped]
        public XElement OnlineCharging
        {
            get { return XElement.Parse(_OnlineCharging); }
            set { _OnlineCharging = value.ToString(); }
        }

        /// <summary>
        /// VideoCallRouting
        /// </summary>
        [Column(TypeName = "xml")]
        public string _VideoCallRouting { get; set; }

        [NotMapped]
        public XElement VideoCallRouting
        {
            get { return XElement.Parse(_VideoCallRouting); }
            set { _VideoCallRouting = value.ToString(); }
        }

        /// <summary>
        /// LisInVMRescue
        /// </summary>
        [Column(TypeName = "xml")]
        public string _LisInVMRescue { get; set; }

        [NotMapped]
        public XElement LisInVMRescue
        {
            get { return XElement.Parse(_LisInVMRescue); }
            set { _LisInVMRescue = value.ToString(); }
        }

        /// <summary>
        /// EndpointValidation
        /// </summary>
        [Column(TypeName = "xml")]
        public string _EndpointValidation { get; set; }

        [NotMapped]
        public XElement EndpointValidation
        {
            get { return XElement.Parse(_EndpointValidation); }
            set { _EndpointValidation = value.ToString(); }
        }

        /// <summary>
        /// ParlayRest
        /// </summary>
        [Column(TypeName = "xml")]
        public string _ParlayRest { get; set; }

        [NotMapped]
        public XElement ParlayRest
        {
            get { return XElement.Parse(_ParlayRest); }
            set { _ParlayRest = value.ToString(); }
        }

        /// <summary>
        /// AltRtToPBX
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AltRtToPBX { get; set; }

        [NotMapped]
        public XElement AltRtToPBX
        {
            get { return XElement.Parse(_AltRtToPBX); }
            set { _AltRtToPBX = value.ToString(); }
        }

        /// <summary>
        /// WAMAccess
        /// </summary>
        [Column(TypeName = "xml")]
        public string _WAMAccess { get; set; }

        [NotMapped]
        public XElement WAMAccess
        {
            get { return XElement.Parse(_WAMAccess); }
            set { _WAMAccess = value.ToString(); }
        }

        /// <summary>
        /// ISDN
        /// </summary>
        [Column(TypeName = "xml")]
        public string _ISDN { get; set; }

        [NotMapped]
        public XElement ISDN
        {
            get { return XElement.Parse(_ISDN); }
            set { _ISDN = value.ToString(); }
        }

        /// <summary>
        /// LocaleParms
        /// </summary>
        [Column(TypeName = "xml")]
        public string _LocaleParms { get; set; }

        [NotMapped]
        public XElement LocaleParms
        {
            get { return XElement.Parse(_LocaleParms); }
            set { _LocaleParms = value.ToString(); }
        }

        /// <summary>
        /// VASI
        /// </summary>
        [Column(TypeName = "xml")]
        public string _VASI { get; set; }

        [NotMapped]
        public XElement VASI
        {
            get { return XElement.Parse(_VASI); }
            set { _VASI = value.ToString(); }
        }

        /// <summary>
        /// LocBasedRedirect
        /// </summary>
        [Column(TypeName = "xml")]
        public string _LocBasedRedirect { get; set; }

        [NotMapped]
        public XElement LocBasedRedirect
        {
            get { return XElement.Parse(_LocBasedRedirect); }
            set { _LocBasedRedirect = value.ToString(); }
        }

        /// <summary>
        /// CallLimitGroup
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallLimitGroup { get; set; }

        [NotMapped]
        public XElement CallLimitGroup
        {
            get { return XElement.Parse(_CallLimitGroup); }
            set { _CallLimitGroup = value.ToString(); }
        }

        /// <summary>
        /// AlternateCharging
        /// </summary>
        [Column(TypeName = "xml")]
        public string _AlternateCharging { get; set; }

        [NotMapped]
        public XElement AlternateCharging
        {
            get { return XElement.Parse(_AlternateCharging); }
            set { _AlternateCharging = value.ToString(); }
        }

        /// <summary>
        /// CallForwardNotReach
        /// </summary>
        [Column(TypeName = "xml")]
        public string _CallForwardNotReach { get; set; }

        [NotMapped]
        public XElement CallForwardNotReach
        {
            get { return XElement.Parse(_CallForwardNotReach); }
            set { _CallForwardNotReach = value.ToString(); }
        }

        /// <summary>
        /// MultiDevicesPUID
        /// </summary>
        [Column(TypeName = "xml")]
        public string _MultiDevicesPUID { get; set; }

        [NotMapped]
        public XElement MultiDevicesPUID
        {
            get { return XElement.Parse(_MultiDevicesPUID); }
            set { _MultiDevicesPUID = value.ToString(); }
        }

        /// <summary>
        /// ManagedCallRec
        /// </summary>
        [Column(TypeName = "xml")]
        public string _ManagedCallRec { get; set; }

        [NotMapped]
        public XElement ManagedCallRec
        {
            get { return XElement.Parse(_ManagedCallRec); }
            set { _ManagedCallRec = value.ToString(); }
        }

        /// <summary>
        /// GeoRedundancyData
        /// </summary>
        [Column(TypeName = "xml")]
        public string _GeoRedundancyData { get; set; }

        [NotMapped]
        public XElement GeoRedundancyData
        {
            get { return XElement.Parse(_GeoRedundancyData); }
            set { _GeoRedundancyData = value.ToString(); }
        }
        

        /// <summary>
        /// SubParty (Req for ent/ed)
        /// </summary>
        public virtual SubParty SubParty { get; set; }

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
        public static string SubscriberId(string partyId)
        {
            return partyId;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static bool Create(Subscriber subscriber, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                subscriber.Created = subscriber.Updated = subscriber.Inspected = DateTime.UtcNow.AddHours(3);

                db.Subscribers.Add(subscriber);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static Subscriber Read(string id)
        {
            Subscriber subscriber;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                subscriber = (from q in db.Subscribers where q.Id == id select q).SingleOrDefault();
            }

            return subscriber;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static List<Subscriber> ReadList()
        {
            List<Subscriber> subscriberList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                subscriberList = (from q in db.Subscribers select q).ToList();
            }

            return subscriberList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static bool Update(Subscriber subscriber, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                subscriber = (from q in db.Subscribers where q.Id == subscriber.Id select q).SingleOrDefault();

                subscriber.Updated = DateTime.UtcNow.AddHours(3);

                db.Subscribers.Attach(subscriber);

                db.Entry(subscriber).State = System.Data.Entity.EntityState.Modified;
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
                var v = (from q in db.Subscribers where q.Id == id select q).FirstOrDefault();

                db.Subscribers.Remove(v);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public bool Equal(Subscriber b)
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
        public bool Update(Subscriber updatedNgfsSubscriber)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.PartyId != updatedNgfsSubscriber.PartyId) { this.PartyId = updatedNgfsSubscriber.PartyId; updated = true; }

            if (this.AlternateOtasRealm != updatedNgfsSubscriber.AlternateOtasRealm) { this.AlternateOtasRealm = updatedNgfsSubscriber.AlternateOtasRealm; updated = true; }

            if (this._AnonymousCallRej != updatedNgfsSubscriber._AnonymousCallRej) { this._AnonymousCallRej = updatedNgfsSubscriber._AnonymousCallRej; updated = true; }
            if (this._SelectiveCallRejct != updatedNgfsSubscriber._SelectiveCallRejct) { this._SelectiveCallRejct = updatedNgfsSubscriber._SelectiveCallRejct; updated = true; }
            if (this._SelectiveCallAcpt != updatedNgfsSubscriber._SelectiveCallAcpt) { this._SelectiveCallAcpt = updatedNgfsSubscriber._SelectiveCallAcpt; updated = true; }
            if (this._AutomaticCallBack != updatedNgfsSubscriber._AutomaticCallBack) { this._AutomaticCallBack = updatedNgfsSubscriber._AutomaticCallBack; updated = true; }
            if (this._AutomaticRecall != updatedNgfsSubscriber._AutomaticRecall) { this._AutomaticRecall = updatedNgfsSubscriber._AutomaticRecall; updated = true; }
            if (this._CallingLineId != updatedNgfsSubscriber._CallingLineId) { this._CallingLineId = updatedNgfsSubscriber._CallingLineId; updated = true; }
            if (this._RemoteAccessServer != updatedNgfsSubscriber._RemoteAccessServer) { this._RemoteAccessServer = updatedNgfsSubscriber._RemoteAccessServer; updated = true; }
            if (this._CallBarring != updatedNgfsSubscriber._CallBarring) { this._CallBarring = updatedNgfsSubscriber._CallBarring; updated = true; }
            if (this._CallBlocking != updatedNgfsSubscriber._CallBlocking) { this._CallBlocking = updatedNgfsSubscriber._CallBlocking; updated = true; }
            if (this._CallTransfer != updatedNgfsSubscriber._CallTransfer) { this._CallTransfer = updatedNgfsSubscriber._CallTransfer; updated = true; }
            if (this._CallWaiting != updatedNgfsSubscriber._CallWaiting) { this._CallWaiting = updatedNgfsSubscriber._CallWaiting; updated = true; }
            if (this._CallForwardingVari != updatedNgfsSubscriber._CallForwardingVari) { this._CallForwardingVari = updatedNgfsSubscriber._CallForwardingVari; updated = true; }
            if (this._CallForwardingBusy != updatedNgfsSubscriber._CallForwardingBusy) { this._CallForwardingBusy = updatedNgfsSubscriber._CallForwardingBusy; updated = true; }
            if (this._CallForwardingNoAns != updatedNgfsSubscriber._CallForwardingNoAns) { this._CallForwardingNoAns = updatedNgfsSubscriber._CallForwardingNoAns; updated = true; }
            if (this._SelectiveCallFwd != updatedNgfsSubscriber._SelectiveCallFwd) { this._SelectiveCallFwd = updatedNgfsSubscriber._SelectiveCallFwd; updated = true; }
            if (this._CallForwardingUnreg != updatedNgfsSubscriber._CallForwardingUnreg) { this._CallForwardingUnreg = updatedNgfsSubscriber._CallForwardingUnreg; updated = true; }
            if (this._CustomerOrigTrace != updatedNgfsSubscriber._CustomerOrigTrace) { this._CustomerOrigTrace = updatedNgfsSubscriber._CustomerOrigTrace; updated = true; }
            if (this._NuisanceCallTrace != updatedNgfsSubscriber._NuisanceCallTrace) { this._NuisanceCallTrace = updatedNgfsSubscriber._NuisanceCallTrace; updated = true; }
            if (this._DoNotDisturb != updatedNgfsSubscriber._DoNotDisturb) { this._DoNotDisturb = updatedNgfsSubscriber._DoNotDisturb; updated = true; }
            if (this._MsgWaitingInd != updatedNgfsSubscriber._MsgWaitingInd) { this._MsgWaitingInd = updatedNgfsSubscriber._MsgWaitingInd; updated = true; }
            if (this._SimultaneousRinging != updatedNgfsSubscriber._SimultaneousRinging) { this._SimultaneousRinging = updatedNgfsSubscriber._SimultaneousRinging; updated = true; }
            if (this._WebPortal != updatedNgfsSubscriber._WebPortal) { this._WebPortal = updatedNgfsSubscriber._WebPortal; updated = true; }
            if (this._ConferenceCalling != updatedNgfsSubscriber._ConferenceCalling) { this._ConferenceCalling = updatedNgfsSubscriber._ConferenceCalling; updated = true; }
            if (this._FlashOrigServices != updatedNgfsSubscriber._FlashOrigServices) { this._FlashOrigServices = updatedNgfsSubscriber._FlashOrigServices; updated = true; }
            if (this._CarrierSelection != updatedNgfsSubscriber._CarrierSelection) { this._CarrierSelection = updatedNgfsSubscriber._CarrierSelection; updated = true; }
            if (this._RingbackWhenFree != updatedNgfsSubscriber._RingbackWhenFree) { this._RingbackWhenFree = updatedNgfsSubscriber._RingbackWhenFree; updated = true; }
            if (this._MultipleRingPattern != updatedNgfsSubscriber._MultipleRingPattern) { this._MultipleRingPattern = updatedNgfsSubscriber._MultipleRingPattern; updated = true; }
            if (this._CallForwardingLocal != updatedNgfsSubscriber._CallForwardingLocal) { this._CallForwardingLocal = updatedNgfsSubscriber._CallForwardingLocal; updated = true; }
            if (this._RemoteAccessServices != updatedNgfsSubscriber._RemoteAccessServices) { this._RemoteAccessServices = updatedNgfsSubscriber._RemoteAccessServices; updated = true; }
            if (this._VoiceMail != updatedNgfsSubscriber._VoiceMail) { this._VoiceMail = updatedNgfsSubscriber._VoiceMail; updated = true; }
            if (this._InterceptReferral != updatedNgfsSubscriber._InterceptReferral) { this._InterceptReferral = updatedNgfsSubscriber._InterceptReferral; updated = true; }
            if (this._DialingPlan != updatedNgfsSubscriber._DialingPlan) { this._DialingPlan = updatedNgfsSubscriber._DialingPlan; updated = true; }
            if (this._HSSPrivateId != updatedNgfsSubscriber._HSSPrivateId) { this._HSSPrivateId = updatedNgfsSubscriber._HSSPrivateId; updated = true; }
            if (this._HSSPublicIdCustom != updatedNgfsSubscriber._HSSPublicIdCustom) { this._HSSPublicIdCustom = updatedNgfsSubscriber._HSSPublicIdCustom; updated = true; }
            if (this._AcctCodes != updatedNgfsSubscriber._AcctCodes) { this._AcctCodes = updatedNgfsSubscriber._AcctCodes; updated = true; }
            if (this._SeqRinging != updatedNgfsSubscriber._SeqRinging) { this._SeqRinging = updatedNgfsSubscriber._SeqRinging; updated = true; }
            if (this._AuthCodeService != updatedNgfsSubscriber._AuthCodeService) { this._AuthCodeService = updatedNgfsSubscriber._AuthCodeService; updated = true; }
            if (this._MLHGNoHuntMember != updatedNgfsSubscriber._MLHGNoHuntMember) { this._MLHGNoHuntMember = updatedNgfsSubscriber._MLHGNoHuntMember; updated = true; }
            if (this._MultilineHuntGroup != updatedNgfsSubscriber._MultilineHuntGroup) { this._MultilineHuntGroup = updatedNgfsSubscriber._MultilineHuntGroup; updated = true; }
            if (this._CallPickupOrig != updatedNgfsSubscriber._CallPickupOrig) { this._CallPickupOrig = updatedNgfsSubscriber._CallPickupOrig; updated = true; }
            if (this._CallPickupTerm != updatedNgfsSubscriber._CallPickupTerm) { this._CallPickupTerm = updatedNgfsSubscriber._CallPickupTerm; updated = true; }
            if (this._Attendant != updatedNgfsSubscriber._Attendant) { this._Attendant = updatedNgfsSubscriber._Attendant; updated = true; }
            if (this._AttendantServer != updatedNgfsSubscriber._AttendantServer) { this._AttendantServer = updatedNgfsSubscriber._AttendantServer; updated = true; }
            if (this._CallPark != updatedNgfsSubscriber._CallPark) { this._CallPark = updatedNgfsSubscriber._CallPark; updated = true; }
            if (this._DirectedGroup != updatedNgfsSubscriber._DirectedGroup) { this._DirectedGroup = updatedNgfsSubscriber._DirectedGroup; updated = true; }
            if (this._RemoteUser != updatedNgfsSubscriber._RemoteUser) { this._RemoteUser = updatedNgfsSubscriber._RemoteUser; updated = true; }
            if (this._TransferToUsersVM != updatedNgfsSubscriber._TransferToUsersVM) { this._TransferToUsersVM = updatedNgfsSubscriber._TransferToUsersVM; updated = true; }
            if (this._FlexCallingLineId != updatedNgfsSubscriber._FlexCallingLineId) { this._FlexCallingLineId = updatedNgfsSubscriber._FlexCallingLineId; updated = true; }
            if (this._ExtensionDevice != updatedNgfsSubscriber._ExtensionDevice) { this._ExtensionDevice = updatedNgfsSubscriber._ExtensionDevice; updated = true; }
            if (this._PSIServer != updatedNgfsSubscriber._PSIServer) { this._PSIServer = updatedNgfsSubscriber._PSIServer; updated = true; }
            if (this._ClosedUserGroup != updatedNgfsSubscriber._ClosedUserGroup) { this._ClosedUserGroup = updatedNgfsSubscriber._ClosedUserGroup; updated = true; }
            if (this._OneDigitSpeedDial != updatedNgfsSubscriber._OneDigitSpeedDial) { this._OneDigitSpeedDial = updatedNgfsSubscriber._OneDigitSpeedDial; updated = true; }
            if (this._TwoDigitSpeedDial != updatedNgfsSubscriber._TwoDigitSpeedDial) { this._TwoDigitSpeedDial = updatedNgfsSubscriber._TwoDigitSpeedDial; updated = true; }
            if (this._ExtensionServer != updatedNgfsSubscriber._ExtensionServer) { this._ExtensionServer = updatedNgfsSubscriber._ExtensionServer; updated = true; }
            if (this._SelectiveAlert != updatedNgfsSubscriber._SelectiveAlert) { this._SelectiveAlert = updatedNgfsSubscriber._SelectiveAlert; updated = true; }
            if (this._ShortCodeTranslate != updatedNgfsSubscriber._ShortCodeTranslate) { this._ShortCodeTranslate = updatedNgfsSubscriber._ShortCodeTranslate; updated = true; }
            if (this._ReminderCall != updatedNgfsSubscriber._ReminderCall) { this._ReminderCall = updatedNgfsSubscriber._ReminderCall; updated = true; }
            if (this._SetTZPath != updatedNgfsSubscriber._SetTZPath) { this._SetTZPath = updatedNgfsSubscriber._SetTZPath; updated = true; }
            if (this._InhibitIncomingFwd != updatedNgfsSubscriber._InhibitIncomingFwd) { this._InhibitIncomingFwd = updatedNgfsSubscriber._InhibitIncomingFwd; updated = true; }
            if (this._BearerBasedCallFwd != updatedNgfsSubscriber._BearerBasedCallFwd) { this._BearerBasedCallFwd = updatedNgfsSubscriber._BearerBasedCallFwd; updated = true; }
            if (this._TimeOriginatedRC != updatedNgfsSubscriber._TimeOriginatedRC) { this._TimeOriginatedRC = updatedNgfsSubscriber._TimeOriginatedRC; updated = true; }
            if (this._MobileDMS != updatedNgfsSubscriber._MobileDMS) { this._MobileDMS = updatedNgfsSubscriber._MobileDMS; updated = true; }
            if (this._MLPP != updatedNgfsSubscriber._MLPP) { this._MLPP = updatedNgfsSubscriber._MLPP; updated = true; }
            if (this._MusicOnHold != updatedNgfsSubscriber._MusicOnHold) { this._MusicOnHold = updatedNgfsSubscriber._MusicOnHold; updated = true; }
            if (this._MiRingback != updatedNgfsSubscriber._MiRingback) { this._MiRingback = updatedNgfsSubscriber._MiRingback; updated = true; }
            if (this._SelNomadicBlocking != updatedNgfsSubscriber._SelNomadicBlocking) { this._SelNomadicBlocking = updatedNgfsSubscriber._SelNomadicBlocking; updated = true; }
            if (this._AutoDial != updatedNgfsSubscriber._AutoDial) { this._AutoDial = updatedNgfsSubscriber._AutoDial; updated = true; }
            if (this._CallForwardingDefault != updatedNgfsSubscriber._CallForwardingDefault) { this._CallForwardingDefault = updatedNgfsSubscriber._CallForwardingDefault; updated = true; }
            if (this._CCBS != updatedNgfsSubscriber._CCBS) { this._CCBS = updatedNgfsSubscriber._CCBS; updated = true; }
            if (this._BlockCCBS != updatedNgfsSubscriber._BlockCCBS) { this._BlockCCBS = updatedNgfsSubscriber._BlockCCBS; updated = true; }
            if (this._AINFeatcodeTrigger != updatedNgfsSubscriber._AINFeatcodeTrigger) { this._AINFeatcodeTrigger = updatedNgfsSubscriber._AINFeatcodeTrigger; updated = true; }
            if (this._AINPDPcodeTrigger != updatedNgfsSubscriber._AINPDPcodeTrigger) { this._AINPDPcodeTrigger = updatedNgfsSubscriber._AINPDPcodeTrigger; updated = true; }
            if (this._AINOHDTrigger != updatedNgfsSubscriber._AINOHDTrigger) { this._AINOHDTrigger = updatedNgfsSubscriber._AINOHDTrigger; updated = true; }
            if (this._AINTATTrigger != updatedNgfsSubscriber._AINTATTrigger) { this._AINTATTrigger = updatedNgfsSubscriber._AINTATTrigger; updated = true; }
            if (this._GLSAccess != updatedNgfsSubscriber._GLSAccess) { this._GLSAccess = updatedNgfsSubscriber._GLSAccess; updated = true; }
            if (this._SharedNumber != updatedNgfsSubscriber._SharedNumber) { this._SharedNumber = updatedNgfsSubscriber._SharedNumber; updated = true; }
            if (this._CallDeflection != updatedNgfsSubscriber._CallDeflection) { this._CallDeflection = updatedNgfsSubscriber._CallDeflection; updated = true; }
            if (this._AutoAttendant != updatedNgfsSubscriber._AutoAttendant) { this._AutoAttendant = updatedNgfsSubscriber._AutoAttendant; updated = true; }
            if (this._AdjunctTerminating != updatedNgfsSubscriber._AdjunctTerminating) { this._AdjunctTerminating = updatedNgfsSubscriber._AdjunctTerminating; updated = true; }
            if (this._OutgoingCallBarring != updatedNgfsSubscriber._OutgoingCallBarring) { this._OutgoingCallBarring = updatedNgfsSubscriber._OutgoingCallBarring; updated = true; }
            if (this._RDigitParms != updatedNgfsSubscriber._RDigitParms) { this._RDigitParms = updatedNgfsSubscriber._RDigitParms; updated = true; }
            if (this._TransitRouting != updatedNgfsSubscriber._TransitRouting) { this._TransitRouting = updatedNgfsSubscriber._TransitRouting; updated = true; }
            if (this._LocationNumber != updatedNgfsSubscriber._LocationNumber) { this._LocationNumber = updatedNgfsSubscriber._LocationNumber; updated = true; }
            if (this._FemtoBSR != updatedNgfsSubscriber._FemtoBSR) { this._FemtoBSR = updatedNgfsSubscriber._FemtoBSR; updated = true; }
            if (this._GeneralReset != updatedNgfsSubscriber._GeneralReset) { this._GeneralReset = updatedNgfsSubscriber._GeneralReset; updated = true; }
            if (this._Metering != updatedNgfsSubscriber._Metering) { this._Metering = updatedNgfsSubscriber._Metering; updated = true; }
            if (this._PinService != updatedNgfsSubscriber._PinService) { this._PinService = updatedNgfsSubscriber._PinService; updated = true; }
            if (this._WholeSale != updatedNgfsSubscriber._WholeSale) { this._WholeSale = updatedNgfsSubscriber._WholeSale; updated = true; }
            if (this._SpareService != updatedNgfsSubscriber._SpareService) { this._SpareService = updatedNgfsSubscriber._SpareService; updated = true; }
            if (this._UseHoldAnnouncement != updatedNgfsSubscriber._UseHoldAnnouncement) { this._UseHoldAnnouncement = updatedNgfsSubscriber._UseHoldAnnouncement; updated = true; }
            if (this._VPNDialingAndDisplay != updatedNgfsSubscriber._VPNDialingAndDisplay) { this._VPNDialingAndDisplay = updatedNgfsSubscriber._VPNDialingAndDisplay; updated = true; }
            if (this._PriorityModeCalling != updatedNgfsSubscriber._PriorityModeCalling) { this._PriorityModeCalling = updatedNgfsSubscriber._PriorityModeCalling; updated = true; }
            if (this._OneNumber != updatedNgfsSubscriber._OneNumber) { this._OneNumber = updatedNgfsSubscriber._OneNumber; updated = true; }
            if (this._AdvancedConference != updatedNgfsSubscriber._AdvancedConference) { this._AdvancedConference = updatedNgfsSubscriber._AdvancedConference; updated = true; }
            if (this._AnswConfirm != updatedNgfsSubscriber._AnswConfirm) { this._AnswConfirm = updatedNgfsSubscriber._AnswConfirm; updated = true; }
            if (this._OnlineCharging != updatedNgfsSubscriber._OnlineCharging) { this._OnlineCharging = updatedNgfsSubscriber._OnlineCharging; updated = true; }
            if (this._VideoCallRouting != updatedNgfsSubscriber._VideoCallRouting) { this._VideoCallRouting = updatedNgfsSubscriber._VideoCallRouting; updated = true; }
            if (this._LisInVMRescue != updatedNgfsSubscriber._LisInVMRescue) { this._LisInVMRescue = updatedNgfsSubscriber._LisInVMRescue; updated = true; }
            if (this._EndpointValidation != updatedNgfsSubscriber._EndpointValidation) { this._EndpointValidation = updatedNgfsSubscriber._EndpointValidation; updated = true; }
            if (this._ParlayRest != updatedNgfsSubscriber._ParlayRest) { this._ParlayRest = updatedNgfsSubscriber._ParlayRest; updated = true; }
            if (this._AltRtToPBX != updatedNgfsSubscriber._AltRtToPBX) { this._AltRtToPBX = updatedNgfsSubscriber._AltRtToPBX; updated = true; }
            if (this._WAMAccess != updatedNgfsSubscriber._WAMAccess) { this._WAMAccess = updatedNgfsSubscriber._WAMAccess; updated = true; }
            if (this._ISDN != updatedNgfsSubscriber._ISDN) { this._ISDN = updatedNgfsSubscriber._ISDN; updated = true; }
            if (this._LocaleParms != updatedNgfsSubscriber._LocaleParms) { this._LocaleParms = updatedNgfsSubscriber._LocaleParms; updated = true; }
            if (this._VASI != updatedNgfsSubscriber._VASI) { this._VASI = updatedNgfsSubscriber._VASI; updated = true; }
            if (this._LocBasedRedirect != updatedNgfsSubscriber._LocBasedRedirect) { this._LocBasedRedirect = updatedNgfsSubscriber._LocBasedRedirect; updated = true; }
            if (this._CallLimitGroup != updatedNgfsSubscriber._CallLimitGroup) { this._CallLimitGroup = updatedNgfsSubscriber._CallLimitGroup; updated = true; }
            if (this._AlternateCharging != updatedNgfsSubscriber._AlternateCharging) { this._AlternateCharging = updatedNgfsSubscriber._AlternateCharging; updated = true; }
            if (this._CallForwardNotReach != updatedNgfsSubscriber._CallForwardNotReach) { this._CallForwardNotReach = updatedNgfsSubscriber._CallForwardNotReach; updated = true; }
            if (this._MultiDevicesPUID != updatedNgfsSubscriber._MultiDevicesPUID) { this._MultiDevicesPUID = updatedNgfsSubscriber._MultiDevicesPUID; updated = true; }
            if (this._ManagedCallRec != updatedNgfsSubscriber._ManagedCallRec) { this._ManagedCallRec = updatedNgfsSubscriber._ManagedCallRec; updated = true; }
            if (this._GeoRedundancyData != updatedNgfsSubscriber._GeoRedundancyData) { this._GeoRedundancyData = updatedNgfsSubscriber._GeoRedundancyData; updated = true; }            

            if (this.SubParty != updatedNgfsSubscriber.SubParty) { this.SubParty = updatedNgfsSubscriber.SubParty; updated = true; }
            
            if (this.UserId != updatedNgfsSubscriber.UserId) { this.UserId = updatedNgfsSubscriber.UserId; updated = true; }

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