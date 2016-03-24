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
    /// SubParty Entity Framework class for Next Generation Network (NGN) entity model.
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
        /// <summary>
        /// 1360 COM WebAPI User Guide 255-400-419R3.X
        /// ngfs-subparty-v2(rtrv)
        /// </summary>
        public SubParty() { }

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        /// <summary>
        /// PartyId (req for ent,req for ed). LCP Name for Party record. String (1-27) [+0-9a-zA-Z:@&amp;=!,|-_.!~*()%]* Before CTS R5.0: [+0-9a-zA-Z:@&amp;=!,-_.!~*'()%]* LCP-CTS R6.1 From CTS R6.1: [+0-9a-zA-Z:@&amp;=,|-_.!~*'()%#?/$][]* If the String Ends with '[AutoSelect]', '[AutoSelectSTAS]', or '[AutoSelectDTAS]' The Subscriber will be automatically distributed to the TASs. When [AutoSelect],[AutoSelectDTAS] or [AutoSelectSTAS] is used in ent- request, AssocOtasRealm should be empty. And the OTAS which has fewest subscriber will be chosen as AssocOtasRealm. For non-ICS subscriber request (do not include iHss private id info), [AutoSelect] can be used. For the subscriber request which include iHss Private Id info, [AutoSelectDTAS] and [AutoSelectSTAS] can be used. And the Realm in OTAS must match the "ApplicateionServer" defined in iHss Global IFC and this IFC must be used by iHss Service profile. Otherwise, error will be reported thant no service profile for this OTAS. String length does not include length of '[AutoSelect]', '[AutoSelectSTAS]', and '[AutoSelectDTAS]'. LCP-CTS R6.2.1 The new partyId. Used for changing partyId. Only used for ed command. Range same as PartyId.
        /// </summary>
        public string PartyId { get; set; }
        /// <summary>
        /// DisplayName (opt for ent,opt for ed). LCP DisplayName that is displayed on a Caller ID String (0-15).[+0-9a-zA-Z(),':@&amp;=!\-_\.!~\*%#&gt;&lt;$^\|?{}\[\];/`]*
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Category (req for ent,opt for ed). LCP String containing the Feature Set the party is assigned. The category is the feature bundle.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// NewPartyId (opt for ent,opt for ed). LCP-CTS R9.1 Starting in COM 4.1, deprecated for switches on release CTS R9.1 and later. Use exec-ngfs-subscriber-changekey-v2.
        /// </summary>
        public string NewPartyId { get; set; }

        /// <summary>
        /// PrimaryPUID (req for ent,opt for ed). LCP Primary Phone number of endpoint. String (1-32) [+0-9a-zA-Z:,\-_\.()]* LCP-CTS R6.1 From CTS R6.1: String(1-252) [+0-9a-zA-Z:,-_.()&=?~/$'!*]* LCP-CTS R9.0 From CTS 9.0, format puid@Domain is supported.All features assigned to this PUID also support @domain. puid length (1-32) [0-9a-zA-Z&amp;=\+$,\?/\-_\.!~\*'()]* domain is predefined in subscriber home domain, max length 219
        /// </summary>
        public string PrimaryPUID { get; set; }

        /// <summary>
        /// NewPrimaryPUID (opt for ent,opt for ed). LCP-CTS R6.2.1 The new PrimaryPUId. Used for changing PrimaryPUID. Only used for ed command. Range same as PrimaryPUID. LCP-CTS R9.1 Starting in COM 4.1, deprecated for switches on release CTS R9.1 and later. Use exec-ngfs-subscriber-changekey-v2.
        /// </summary>
        public string NewPrimaryPUID { get; set; }

        /// <summary>
        /// PrimaryPUIDDomainRequired (opt for ent,opt for ed). LCP-CTS R9.0 Boolean (true or false). Default false.
        /// </summary>
        public bool PrimaryPUIDDomainRequired { get; set; }

        /// <summary>
        /// PrimaryPUIDCPEProfileNumber (req for ent,opt for ed). LCP Identifies type of CPE Int (0-255)
        /// </summary>
        public int PrimaryPUIDCPEProfileNumber { get; set; }

        /// <summary>
        /// PrimaryPUIDFlashable (opt for ent,opt for ed). LCP Has flash capabilities. Boolean (true or false)
        /// </summary>
        public bool PrimaryPUIDFlashable { get; set; }

        /// <summary>
        /// AssocOtasRealm (opt for ent,opt for ed). LCP The TAS that supports this party. String (0-63) [0-9a-zA-Z\.\-]*
        /// </summary>
        public string AssocOtasRealm { get; set; }

        /// <summary>
        /// NetworkProfileName (opt for ent,opt for ed). LCP-CTS R7.1.1 Name of the Network Profile String (0-20) [0-9a-zA-Z\-_]* When a subparty is associated with a template, any conflicting field will be silently overridden by the template's value.
        /// </summary>
        public string NetworkProfileName { get; set; }

        /// <summary>
        /// NetworkProfileVersion (opt for ent,opt for ed). LCP-CTS R7.1.1 Version of the Network Profile int (1-99)
        /// </summary>
        public int NetworkProfileVersion { get; set; }

        /// <summary>
        /// ServiceProfileName (opt for ent,opt for ed). LCP-CTS R7.1.1 Name of the Service Profile String (0-20) [0-9a-zA-Z\-_]* When a subparty is associated with a template, any conflicting field will be silently overridden by the template's value.
        /// </summary>
        public string ServiceProfileName { get; set; }

        /// <summary>
        /// ServiceProfileVersion (opt for ent,opt for ed). LCP-CTS R7.1.1 Version of the Service Profile int (1-99)
        /// </summary>
        public int ServiceProfileVersion { get; set; }

        /// <summary>
        /// IsReducedServiceProfile (opt for ent,opt for ed). LCP-CTS R9.0 Boolean (true or false). Default is false. This field indicates whether the Service Profile is a Reduced Service Profile or a regular Service Profile profile type.
        /// </summary>
        public bool IsReducedServiceProfile { get; set; }

        /// <summary>
        /// CallLimit (opt for ent,opt for ed). LCP Maximum number of simultaneous calls permitted for this subscriber. Integer(1-1024) For CTS R9.1 and later, Integer(1-1920) LCP-CTS R10.2 Integer(1-2500)
        /// </summary>
        public int CallLimit { get; set; }

        /// <summary>
        /// ServiceSuspension (opt for ent,opt for ed). LCP Suspend services Boolean (true or false)
        /// </summary>
        public bool ServiceSuspension { get; set; }

        /// <summary>
        /// OriginationSuspension (opt for ent,opt for ed). LCP-CTS R9.2.1 Boolean (true or false). Default false. Only the ability to originate a call is suspended.
        /// </summary>
        public bool OriginationSuspension { get; set; }

        /// <summary>
        /// TerminationSuspension (opt for ent,opt for ed). LCP-CTS R9.2.1 Boolean (true or false). Default false. Only the ability to receive a call is suspended.
        /// </summary>
        public bool TerminationSuspension { get; set; }

        /// <summary>
        /// SuspensionNotification (opt for ent,opt for ed). LCP-CTS R9.2.1 Boolean (true or false). Default false. Neither originating or terminating service is barred or blocked; however any originating attempt results in a announcement being played that is intended to warn the subscriber that their service may soon be suspended due to non-payment.
        /// </summary>
        public bool SuspensionNotification { get; set; }

        /// <summary>
        /// UserOrigSuspension (opt for ent,opt for ed). LCP-CTS R9.2.1 Boolean (true or false). Default false. This indicates that the subscriber requested the suspension of their own service for originations (during which time they pay a reduced fee). 
        /// </summary>
        public bool UserOrigSuspension { get; set; }

        /// <summary>
        /// UserTermSuspension (opt for ent,opt for ed). LCP-CTS R9.2.1 Boolean (true or false). Default false. This indicates that the subscriber requested the suspension of their own service for terminations (during which time they pay a reduced fee).
        /// </summary>
        public bool UserTermSuspension { get; set; }

        /// <summary>
        /// AssocWpifRealm (opt for ent,opt for ed). LCP Web portal interface URI. String(0-63) [0-9a-zA-Z\.\-]*
        /// </summary>
        public string AssocWpifRealm { get; set; }

        /// <summary>
        /// IddPrefix (opt for ent,opt for ed). LCP-CTS R3.0 IDD Prefix. Number string(0-5)
        /// </summary>
        public string IddPrefix { get; set; }

        /// <summary>
        /// AlternateFsdbFqdn (opt for ent,opt for ed). LCP-CTS R3.0 Alternate Fsdb fully qualified domain name.If there is no Primary/Protection Geographic Redundancy this field shall not be populated. String(0-63) [0-9a-zA-Z:-_.]*
        /// </summary>
        public string AlternateFsdbFqdn { get; set; }

        /// <summary>
        /// SharedHssData (opt for ent,opt for ed). LCP-CTS R3.0 Has Shared Hss Data or not. Boolean(true or false)
        /// </summary>
        public bool SharedHssData { get; set; }

        /// <summary>
        /// Pin (opt for ent,opt for ed). LCP-CTS R4.0 String(0 or 4-10) [0-9]* Pin is required for the User Control options under the CallBarring/CallBlocking feature. Note: If you change this pin field you must also change the associated PSI Extension and PSI User pins LCP-CTS R9.0 Deprecated since CTS R9.0.
        /// </summary>
        public string Pin { get; set; }

        /// <summary>
        /// MsnCapability (opt for ent,opt for ed). LCP-CTS R6.1 Boolean (true or false) Indicates that the subscriber is allowed to utilize Multiple Subscriber Number Service. MSN service provides additional service support on a per PUID basis, i.e. ACR will be capable of assigning to each PUID in the party when MsnCapability is set. Default false
        /// </summary>
        public bool MsnCapability { get; set; }

        /// <summary>
        /// VideoProhibit (opt for ent,opt for ed). LCP-CTS R9.0 Boolean(true or false) Default: false
        /// </summary>
        public bool VideoProhibit { get; set; }

        /// <summary>
        /// MaxFwdHops (opt for ent,opt for ed). LCP-CTS R4.1 Maximum number of Call Forwarding Hops Integer (1-20) Default 10
        /// </summary>
        public int MaxFwdHops { get; set; }

        /// <summary>
        /// CsdFlavor (opt for ent,opt for ed). LCP-CTS R5.0 Valid values: TAS_CSD_NONE, TAS_CSD_AIMS_P0, TAS_CSD_ALU_P2
        /// </summary>
        public int CsdFlavor { get; set; }

        /// <summary>
        /// CsdDynamic (opt for ent,opt for ed). LCP-CTS R6.1 Boolean (true or false) Indicates that the subscriber is allowed to utilize the Centralized Subscriber Database. Default false
        /// </summary>
        public bool CsdDynamic { get; set; }

        /// <summary>
        /// SipErrorTableId (opt for ent,opt for ed). LCP-CTS R5.0 SIP error handling table ID which can be assigned on a per Party basis to provide the flexibility to support individual customization for handling SIP error responses. Integer (1- 255) Default: 0: the system table LCP-CTS R7.1 SIP error handling table ID which can be assigned on a per Party basis to provide the flexibility to support individual customization for handling SIP error responses on the originating side. Integer (1- 255) Default: 0: the system table
        /// </summary>
        public int SipErrorTableId { get; set; }

        /// <summary>
        /// TreatmentTableId (opt for ent,opt for ed). LCP-CTS R5.0 Treatment table ID which can be assigned on a per Party basis to provide the flexibility to support individual treatment tables for Wholesaling and VPN type services. Integer (120-1145) Default: 0: the default treatment table 
        /// </summary>
        public int TreatmentTableId { get; set; }

        /// <summary>
        /// Locale (opt for ent,opt for ed). LCP-CTS R5.0 The locale of the subscriber which is used to override the system locale on a per Party basis. ([a-zA-Z]{2}([a-zA-Z]) ?_[a-zA-Z]{2}([a-zA-Z])?)? For example: en_us LCP-CTS R9.2 From CTS R9.2, deprecated.
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// CliPrefixList (opt for ent,opt for ed). LCP-CTS R5.0 A comma separated list of prefixes which are to be stripped off before presenting calling or called numbers. String (0-35) [0-9,]*
        /// </summary>
        public string CliPrefixList { get; set; }

        /// <summary>
        /// IsGroupCPE (opt for ent,opt for ed). LCP-CTS R5.0 Boolean (true or false) A party cannot require Group CPE processing and also be marked as a flashable endpoint.
        /// </summary>
        public bool IsGroupCPE { get; set; }

        /// <summary>
        /// Receive181Mode (opt for ent,opt for ed). LCP-CTS R5.0 Valid value: TAS_181_NONE, TAS_181_WITH_FWINFO Default is TAS_181_NONE
        /// </summary>
        public int Receive181Mode { get; set; }

        /// <summary>
        /// CcNdcLength (opt for ent,opt for ed). LCP-CTS R6.0 0 or integer (3-10) default 0
        /// </summary>
        public int CcNdcLength { get; set; }

        /// <summary>
        /// MaxActiveCalls (opt for ent,opt for ed). LCP-CTS R6.0 Integer (0-2500) default 0 The value should not be greater than Call Limit.
        /// </summary>
        public int MaxActiveCalls { get; set; }

        /// <summary>
        /// CallingPartyCategory (opt for ent,opt for ed). LCP-CTS R6.1 String indicates the Party Type. Valid Values: CPC_ORDINARY, CPC_OPERATOR, CPC_PRIORITY, CPC_DATA, CPC_TEST, CPC_PAYPHONE, CPC_CELLULAR, CPC_PRISON, CPC_HOTEL, CPC_POLICE, CPC_UNKNOWN, CPC_HOSPITAL, CPC_SPARE14, CPC_SPARE15, CPC_SPARE16, CPC_SPARE17, CPC_SPARE18, CPC_SPARE19, CPC_SPARE20, CPC_SPARE21, CPC_SPARE22, CPC_SPARE23, CPC_SPARE24, CPC_SPARE25, CPC_SPARE26, CPC_SPARE27, CPC_SPARE28, CPC_SPARE29, CPC_SPARE30, CPC_SPARE31, Default CPC_ORDINARY
        /// </summary>
        public int CallingPartyCategory { get; set; }

        /// <summary>
        /// PublicUID1 (opt for ent,opt for ed). LCP Secondary Phone number of endpoint. String (1-32) [+0-9a-zA-Z:,\-_\.()]* LCP-CTS R6.1 From CTS R6.1: String(1-252) [+0-9a-zA-Z:,-_.()&=?~/$'!*]* LCP-CTS R9.0 From CTS 9.0, format puid@Domain is supported. All features assigned to this PUID also support @domain. puid length (1-32) [0-9a-zA-Z&amp;=\+$,\?/\-_\.!~\*'()]* domain is predefined in subscriber home domain, max length 219
        /// </summary>
        public string PublicUID1 { get; set; }

        /// <summary>
        /// PublicUID2 (opt for ent,opt for ed). LCP The same as PublicUID1 LCP-CTS R9.0 From CTS 9.0, format puid@Domain is supported.All features assigned to this PUID also support @domain. puid length (1-32) [0-9a-zA-Z&amp;=\+$,\?/\-_\.!~\*'()]* domain is predefined in subscriber home domain, max length 219
        /// </summary>
        public string PublicUID2 { get; set; }

        /// <summary>
        /// PublicUID3 (opt for ent,opt for ed). LCP-CTS R9.0 From CTS 9.0, format puid@Domain is supported.All LCP The same as PublicUID1 features assigned to this PUID also support @domain. puid length (1-32) [0-9a-zA-Z&amp;=\+$,\?/\-_\.!~\*'()]* domain is predefined in subscriber home domain, max length 219
        /// </summary>
        public string PublicUID3 { get; set; }

        /// <summary>
        /// PublicUID4 (opt for ent,opt for ed). LCP-CTS R9.0 From CTS 9.0, format puid@Domain is supported.All LCP The same as PublicUID1 features assigned to this PUID also support @domain. puid length (1-32) [0-9a-zA-Z&amp;=\+$,\?/\-_\.!~\*'()]* domain is predefined in subscriber home domain, max length 219
        /// </summary>
        public string PublicUID4 { get; set; }

        /// <summary>
        /// PublicUID5 (opt for ent,opt for ed). LCP The same as PublicUID1 LCP-CTS R9.0 From CTS 9.0, format puid@Domain is supported.All features assigned to this PUID also support @domain. puid length (1-32) [0-9a-zA-Z&amp;=\+$,\?/\-_\.!~\*'()]* domain is predefined in subscriber home domain, max length 219
        /// </summary>
        public string PublicUID5 { get; set; }

        /// <summary>
        /// PublicUID6 (opt for ent,opt for ed). LCP-CTS R9.0 From CTS 9.0, format puid@Domain is supported.All LCP The same as PublicUID1 features assigned to this PUID also support @domain. puid length (1-32) [0-9a-zA-Z&amp;=\+$,\?/\-_\.!~\*'()]* domain is predefined in subscriber home domain, max length 219
        /// </summary>
        public string PublicUID6 { get; set; }

        /// <summary>
        /// PublicUID7 (opt for ent,opt for ed) LCP The same as PublicUID1 LCP-CTS R9.0 From CTS 9.0, format puid@Domain is supported.All LCP The same as PublicUID1 features assigned to this PUID also support @domain. puid length (1-32) [0-9a-zA-Z&amp;=\+$,\?/\-_\.!~\*'()]* domain is predefined in subscriber home domain, max length 219
        /// </summary>
        public string PublicUID7 { get; set; }

        /// <summary>
        /// PublicUID8 (opt for ent,opt for ed). LCP-CTS R6.0 The same as PublicUID1 LCP-CTS R9.0 From CTS 9.0, format puid@Domain is supported.All features assigned to this PUID also support @domain. puid length (1-32) [0-9a-zA-Z&amp;=\+$,\?/\-_\.!~\*'()]* domain is predefined in subscriber home domain, max length 219
        /// </summary>
        public string PublicUID8 { get; set; }

        /// <summary>
        /// PublicUID9 (opt for ent,opt for ed). LCP-CTS R6.0 The same as PublicUID1 LCP-CTS R9.0 From CTS 9.0, format puid@Domain is supported.All features assigned to this PUID also support @domain. puid length (1-32) [0-9a-zA-Z&amp;=\+$,\?/\-_\.!~\*'()]* domain is predefined in subscriber home domain, max length 219
        /// </summary>
        public string PublicUID9 { get; set; }

        /// <summary>
        /// PublicUID1DomainRequired (opt for ent,opt for ed). LCP-CTS R9.0 Boolean (true or false).Default false.
        /// </summary>
        public bool PublicUID1DomainRequired { get; set; }

        /// <summary>
        /// PublicUID2DomainRequired (opt for ent,opt for ed). LCP-CTS R9.0 Boolean (true or false).Default false.
        /// </summary>
        public bool PublicUID2DomainRequired { get; set; }

        /// <summary>
        /// PublicUID3DomainRequired (opt for ent,opt for ed). LCP-CTS R9.0 Boolean (true or false) Default false.
        /// </summary>
        public bool PublicUID3DomainRequired { get; set; }

        /// <summary>
        /// PublicUID4DomainRequired (opt for ent,opt for ed). LCP-CTS R9.0 Boolean (true or false) Default false.
        /// </summary>
        public bool PublicUID4DomainRequired { get; set; }

        /// <summary>
        /// PublicUID5DomainRequired (opt for ent,opt for ed). LCP-CTS R9.0 Boolean (true or false) Default false.
        /// </summary>
        public bool PublicUID5DomainRequired { get; set; }

        /// <summary>
        /// PublicUID6DomainRequired (opt for ent,opt for ed). LCP-CTS R9.0 Boolean (true or false) Default false.
        /// </summary>
        public bool PublicUID6DomainRequired { get; set; }

        /// <summary>
        /// PublicUID7DomainRequired (opt for ent,opt for ed). LCP-CTS R9.0 Boolean (true or false)Default false.
        /// </summary>
        public bool PublicUID7DomainRequired { get; set; }

        /// <summary>
        /// PublicUID8DomainRequired (opt for ent,opt for ed). LCP-CTS R9.0 Boolean (true or false)Default false.
        /// </summary>
        public bool PublicUID8DomainRequired { get; set; }

        /// <summary>
        /// PublicUID9DomainRequired (opt for ent,opt for ed). LCP-CTS R9.0 Boolean (true or false)Default false.
        /// </summary>
        public bool PublicUID9DomainRequired { get; set; }

        /// <summary>
        /// WildCardPUIDStr (opt for ent,opt for ed). LCP-CTS R6.0 Format as: WildCardPUID1` WildCardPUID2`...... The max number for WildCardPUID is 31. Wild-card Puids have a different format than regular PUID. They are numbers with the wild-card part at the end delimited by '!'. Few examples of wildcard PUIDs: 163000![1-9]! Range 1630001 - 1630009 16300![1-9][0-9]! Range 1630010 - 1630099 1630![1-9][0-9].! Range 1630100 - 1630999 String(3-64) (+)?[0-9]*(!)[0-9-.*[]]+(!)
        /// </summary>
        public string WildCardPUIDStr { get; set; }

        /// <summary>
        /// AllowCustomAnnouncement (opt for ent,opt for ed). LCP-CTS R6.1 Boolean (true or false) Indicates if the subscriber is allowed to use custom announcements. Default false
        /// </summary>
        public bool AllowCustomAnnouncement { get; set; }

        /// <summary>
        /// PtySpareLong1 (opt for ent,opt for ed). LCP-CTS R6.1a (0-4294967295) default 0 The following party level spare fields are not stored in CTS database now.
        /// </summary>
        public long PtySpareLong1 { get; set; }

        /// <summary>
        /// PtySpareString (opt for ent,opt for ed). LCP-CTS R6.1a The length is between 0 and 252. 
        /// </summary>
        public string PtySpareString { get; set; }

        /// <summary>
        /// PtySpareString2 (opt for ent,opt for ed). LCP-CTS R10.0 The length is between 0 and 252.
        /// </summary>
        public string PtySpareString2 { get; set; }

        /// <summary>
        /// PtySpareShort1 (opt for ent,opt for ed). LCP-CTS R6.1a (0-65535) default 0
        /// </summary>
        public int PtySpareShort1 { get; set; }

        /// <summary>
        /// PtySpareShort2 (opt for ent,opt for ed). LCP-CTS R6.1a (0-65535) default 0
        /// </summary>
        public int PtySpareShort2 { get; set; }

        /// <summary>
        /// PtySpareBool1 (opt for ent,opt for ed). LCP-CTS R6.1a Boolean (true or false) default false
        /// </summary>
        public bool PtySpareBool1 { get; set; }

        /// <summary>
        /// PtySpareBool2 (opt for ent,opt for ed). LCP-CTS R6.1a Boolean (true or false) default false
        /// </summary>
        public bool PtySpareBool2 { get; set; }

        /// <summary>
        /// PtySpareBool3 (opt for ent,opt for ed). LCP-CTS R6.1a Boolean (true or false) default false
        /// </summary>
        public bool PtySpareBool3 { get; set; }

        /// <summary>
        /// PtySpareBool4 (opt for ent,opt for ed). LCP-CTS R6.1a Boolean (true or false) default false
        /// </summary>
        public bool PtySpareBool4 { get; set; }

        /// <summary>
        /// PtySpareBool5 (opt for ent,opt for ed). LCP-CTS R6.1a Boolean (true or false) default false
        /// </summary>
        public bool PtySpareBool5 { get; set; }

        /// <summary>
        /// PtySpareBool6 (opt for ent,opt for ed). LCP-CTS R6.1a Boolean (true or false) default false
        /// </summary>
        public bool PtySpareBool6 { get; set; }

        /// <summary>
        /// PtySpareBool7 (opt for ent,opt for ed). LCP-CTS R6.1a Boolean (true or false) default false
        /// </summary>
        public bool PtySpareBool7 { get; set; }

        /// <summary>
        /// PtySpareBool8 (opt for ent,opt for ed). LCP-CTS R6.1a Boolean (true or false) default false
        /// </summary>
        public bool PtySpareBool8 { get; set; }

        /// <summary>
        /// TerminatingTableId (opt for ent,opt for ed). LCP-CTS R7.1 Terminating SIP error handling table ID which can be assigned on a per Party basis to provide the flexibility to support individual customization for handling terminating SIP error responses on the terminating side. Integer (1- 255) Default: 0: the system table
        /// </summary>
        public int TerminatingTableId { get; set; }

        /// <summary>
        /// AllowNonSipTelUri (opt for ent,opt for ed). LCP-CTS R8.0 Boolean (true or false) Default false
        /// </summary>
        public bool AllowNonSipTelUri { get; set; }

        /// <summary>
        /// LocationType (opt for ent,opt for ed). LCP-CTS R8.1.1 One of: - None - EHRPD - LTE - 3G-1X (1) If we set "None" to "LocationType", "RncID", "LteMcc", "LteMnc", "LteTac", "MarketSID" and "SwitchNumber" could be only set null value. (2) If we set "EHRPD" to "LocationType", "RncID" could be inputted with value. Meanwhile, null could only be set to "LteMcc", "LteMnc", "LteTac", "MarketSID" and "SwitchNumber". (3) If we set "LTE" to "LocationType", "LteMcc", "LteMnc" and "LteTac" could be inputed with value. Meanwhile, null could only be set to "MarketSID", "SwitchNumber"and "RncID". (4) If we set "3G-1X" to "LocationType", "MarketSID" and "SwitchNumber" could be inputted with value. Meanwhile, null could only be set to "RncID", "LteMcc", "LteMnc" and "LteTac".
        /// </summary>
        public int LocationType { get; set; }

        /// <summary>
        /// RncID (opt for ent,opt for ed). LCP-CTS R8.1.1 String (3..15) Required when LocationType=EHRPD
        /// </summary>
        public string RncID { get; set; }

        /// <summary>
        /// LteMcc (opt for ent,opt for ed). LCP-CTS R8.1.1 string(3) Required when LocationType=LTE
        /// </summary>
        public string LteMcc { get; set; }

        /// <summary>
        /// LteMnc (opt for ent,opt for ed). LCP-CTS R8.1.1 string(2..3) Required when LocationType=LTE
        /// </summary>
        public string LteMnc { get; set; }

        /// <summary>
        /// LteTac (opt for ent,opt for ed). LCP-CTS R8.1.1 number 0..65535 Required when LocationType=LTE
        /// </summary>
        public int LteTac { get; set; }

        /// <summary>
        /// MarketSID (opt for ent,opt for ed). LCP-CTS R8.1.1 number 0..65535 Required when LocationType=3G-1X
        /// </summary>
        public int MarketSID { get; set; }

        /// <summary>
        /// SwitchNumber (opt for ent,opt for ed). LCP-CTS R8.1.1 Number 0..255 Required when LocationType=3G-1X
        /// </summary>
        public int SwitchNumber { get; set; }

        /// <summary>
        /// CallsToWebUserProhibited (opt for ent,opt for ed). LCP-CTS R9.0 Boolean (true or false). Default false
        /// </summary>
        public bool CallsToWebUserProhibited { get; set; }

        /// <summary>
        /// IMSI (opt for ent,opt for ed). LCP-CTS R10.1 String:blank or 6-15 digits Default blank
        /// </summary>
        public string IMSI { get; set; }

        /// <summary>
        /// IMSNotSupported (opt for ent,opt for ed). LCP-CTS R10.1 Boolean: true or false Default: false
        /// </summary>
        public bool IMSNotSupported { get; set; }

        /// <summary>
        /// ValidateCellID (opt for ent,opt for ed). LCP-CTS R10.1 Boolean(true or false) Default: false
        /// </summary>
        public bool ValidateCellID { get; set; }

        /// <summary>
        /// OperatorID (opt for ent,opt for ed). LCP-CTS R10.1 Integer (0-999) default 0
        /// </summary>
        public int OperatorID { get; set; }

        /// <summary>
        /// HomeMTA (opt for ent,opt for ed). LCP-CTS R10.1 Integer (0-99) default 0
        /// </summary>
        public int HomeMTA { get; set; }

        /// <summary>
        /// ForwardDenyNumbers (opt for ent,opt for ed). LCP-CTS R10.1 Boolean (true or false). Default is false. 
        /// </summary>
        public bool ForwardDenyNumbers { get; set; }

        /// <summary>
        /// PlayAnnoFailNotForward (opt for ent,opt for ed). LCP-CTS R10.1 Boolean (true or false). Default false.
        /// </summary>
        public bool PlayAnnoFailNotForward { get; set; }

        /// <summary>
        /// MrfPoolID (opt for ent,opt for ed). LCP-CTS R10.0 Integer (0-31) default 0 
        /// </summary>
        public int MrfPoolID { get; set; }

        /// <summary>
        /// Custom120x (opt for ent,opt for ed). LCP-CTS R10.1(COM 3.0.X) Boolean (true or false). Default is false.
        /// </summary>
        public bool Custom120x { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual AgcfEndpoint AgcfEndpoint { get; set; }

        /// <summary/>
        public virtual ICollection<Subscriber> Subscribers { get; set; }

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
        public static string SubPartyId(string partyId)
        {
            return partyId;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(SubParty subParty, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                subParty.Created = subParty.Updated = subParty.Inspected = DateTime.UtcNow.AddHours(3);

                db.SubParties.Add(subParty);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static SubParty Read(string id)
        {
            SubParty subParty;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                subParty = (from q in db.SubParties where q.Id == id select q).SingleOrDefault();
            }

            return subParty;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<SubParty> ReadList()
        {
            List<SubParty> subPartyList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                subPartyList = (from q in db.SubParties select q).ToList();
            }

            return subPartyList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(SubParty subParty, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                subParty = (from q in db.SubParties where q.Id == subParty.Id select q).SingleOrDefault();

                subParty.Updated = DateTime.UtcNow.AddHours(3);

                db.SubParties.Attach(subParty);

                db.Entry(subParty).State = System.Data.Entity.EntityState.Modified;
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
                var v = (from q in db.SubParties where q.Id == id select q).FirstOrDefault();

                db.SubParties.Remove(v);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Equal(SubParty b)
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
        public bool Update(SubParty updatedSubParty)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.PartyId != updatedSubParty.PartyId) { this.PartyId = updatedSubParty.PartyId; updated = true; }
            if (this.DisplayName != updatedSubParty.DisplayName) { this.DisplayName = updatedSubParty.DisplayName; updated = true; }
            if (this.Category != updatedSubParty.Category) { this.Category = updatedSubParty.Category; updated = true; }
            if (this.NewPartyId != updatedSubParty.NewPartyId) { this.NewPartyId = updatedSubParty.NewPartyId; updated = true; }
            if (this.PrimaryPUID != updatedSubParty.PrimaryPUID) { this.PrimaryPUID = updatedSubParty.PrimaryPUID; updated = true; }
            if (this.NewPrimaryPUID != updatedSubParty.NewPrimaryPUID) { this.NewPrimaryPUID = updatedSubParty.NewPrimaryPUID; updated = true; }
            if (this.PrimaryPUIDDomainRequired != updatedSubParty.PrimaryPUIDDomainRequired) { this.PrimaryPUIDDomainRequired = updatedSubParty.PrimaryPUIDDomainRequired; updated = true; }
            if (this.PrimaryPUIDCPEProfileNumber != updatedSubParty.PrimaryPUIDCPEProfileNumber) { this.PrimaryPUIDCPEProfileNumber = updatedSubParty.PrimaryPUIDCPEProfileNumber; updated = true; }
            if (this.PrimaryPUIDFlashable != updatedSubParty.PrimaryPUIDFlashable) { this.PrimaryPUIDFlashable = updatedSubParty.PrimaryPUIDFlashable; updated = true; }
            if (this.AssocOtasRealm != updatedSubParty.AssocOtasRealm) { this.AssocOtasRealm = updatedSubParty.AssocOtasRealm; updated = true; }
            if (this.NetworkProfileName != updatedSubParty.NetworkProfileName) { this.NetworkProfileName = updatedSubParty.NetworkProfileName; updated = true; }
            if (this.NetworkProfileVersion != updatedSubParty.NetworkProfileVersion) { this.NetworkProfileVersion = updatedSubParty.NetworkProfileVersion; updated = true; }
            if (this.ServiceProfileName != updatedSubParty.ServiceProfileName) { this.ServiceProfileName = updatedSubParty.ServiceProfileName; updated = true; }
            if (this.ServiceProfileVersion != updatedSubParty.ServiceProfileVersion) { this.ServiceProfileVersion = updatedSubParty.ServiceProfileVersion; updated = true; }
            if (this.IsReducedServiceProfile != updatedSubParty.IsReducedServiceProfile) { this.IsReducedServiceProfile = updatedSubParty.IsReducedServiceProfile; updated = true; }
            if (this.CallLimit != updatedSubParty.CallLimit) { this.CallLimit = updatedSubParty.CallLimit; updated = true; }
            if (this.ServiceSuspension != updatedSubParty.ServiceSuspension) { this.ServiceSuspension = updatedSubParty.ServiceSuspension; updated = true; }
            if (this.OriginationSuspension != updatedSubParty.OriginationSuspension) { this.OriginationSuspension = updatedSubParty.OriginationSuspension; updated = true; }
            if (this.TerminationSuspension != updatedSubParty.TerminationSuspension) { this.TerminationSuspension = updatedSubParty.TerminationSuspension; updated = true; }
            if (this.SuspensionNotification != updatedSubParty.SuspensionNotification) { this.SuspensionNotification = updatedSubParty.SuspensionNotification; updated = true; }
            if (this.UserOrigSuspension != updatedSubParty.UserOrigSuspension) { this.UserOrigSuspension = updatedSubParty.UserOrigSuspension; updated = true; }
            if (this.UserTermSuspension != updatedSubParty.UserTermSuspension) { this.UserTermSuspension = updatedSubParty.UserTermSuspension; updated = true; }
            if (this.AssocWpifRealm != updatedSubParty.AssocWpifRealm) { this.AssocWpifRealm = updatedSubParty.AssocWpifRealm; updated = true; }
            if (this.IddPrefix != updatedSubParty.IddPrefix) { this.IddPrefix = updatedSubParty.IddPrefix; updated = true; }
            if (this.AlternateFsdbFqdn != updatedSubParty.AlternateFsdbFqdn) { this.AlternateFsdbFqdn = updatedSubParty.AlternateFsdbFqdn; updated = true; }
            if (this.SharedHssData != updatedSubParty.SharedHssData) { this.SharedHssData = updatedSubParty.SharedHssData; updated = true; }
            if (this.Pin != updatedSubParty.Pin) { this.Pin = updatedSubParty.Pin; updated = true; }
            if (this.MsnCapability != updatedSubParty.MsnCapability) { this.MsnCapability = updatedSubParty.MsnCapability; updated = true; }
            if (this.VideoProhibit != updatedSubParty.VideoProhibit) { this.VideoProhibit = updatedSubParty.VideoProhibit; updated = true; }
            if (this.MaxFwdHops != updatedSubParty.MaxFwdHops) { this.MaxFwdHops = updatedSubParty.MaxFwdHops; updated = true; }
            if (this.CsdFlavor != updatedSubParty.CsdFlavor) { this.CsdFlavor = updatedSubParty.CsdFlavor; updated = true; }
            if (this.CsdDynamic != updatedSubParty.CsdDynamic) { this.CsdDynamic = updatedSubParty.CsdDynamic; updated = true; }
            if (this.SipErrorTableId != updatedSubParty.SipErrorTableId) { this.SipErrorTableId = updatedSubParty.SipErrorTableId; updated = true; }
            if (this.TreatmentTableId != updatedSubParty.TreatmentTableId) { this.TreatmentTableId = updatedSubParty.TreatmentTableId; updated = true; }
            if (this.Locale != updatedSubParty.Locale) { this.Locale = updatedSubParty.Locale; updated = true; }
            if (this.CliPrefixList != updatedSubParty.CliPrefixList) { this.CliPrefixList = updatedSubParty.CliPrefixList; updated = true; }
            if (this.IsGroupCPE != updatedSubParty.IsGroupCPE) { this.IsGroupCPE = updatedSubParty.IsGroupCPE; updated = true; }
            if (this.Receive181Mode != updatedSubParty.Receive181Mode) { this.Receive181Mode = updatedSubParty.Receive181Mode; updated = true; }
            if (this.CcNdcLength != updatedSubParty.CcNdcLength) { this.CcNdcLength = updatedSubParty.CcNdcLength; updated = true; }
            if (this.MaxActiveCalls != updatedSubParty.MaxActiveCalls) { this.MaxActiveCalls = updatedSubParty.MaxActiveCalls; updated = true; }
            if (this.CallingPartyCategory != updatedSubParty.CallingPartyCategory) { this.CallingPartyCategory = updatedSubParty.CallingPartyCategory; updated = true; }
            if (this.PublicUID1 != updatedSubParty.PublicUID1) { this.PublicUID1 = updatedSubParty.PublicUID1; updated = true; }
            if (this.PublicUID2 != updatedSubParty.PublicUID2) { this.PublicUID2 = updatedSubParty.PublicUID2; updated = true; }
            if (this.PublicUID3 != updatedSubParty.PublicUID3) { this.PublicUID3 = updatedSubParty.PublicUID3; updated = true; }
            if (this.PublicUID4 != updatedSubParty.PublicUID4) { this.PublicUID4 = updatedSubParty.PublicUID4; updated = true; }
            if (this.PublicUID5 != updatedSubParty.PublicUID5) { this.PublicUID5 = updatedSubParty.PublicUID5; updated = true; }
            if (this.PublicUID6 != updatedSubParty.PublicUID6) { this.PublicUID6 = updatedSubParty.PublicUID6; updated = true; }
            if (this.PublicUID7 != updatedSubParty.PublicUID7) { this.PublicUID7 = updatedSubParty.PublicUID7; updated = true; }
            if (this.PublicUID8 != updatedSubParty.PublicUID8) { this.PublicUID8 = updatedSubParty.PublicUID8; updated = true; }
            if (this.PublicUID9 != updatedSubParty.PublicUID9) { this.PublicUID9 = updatedSubParty.PublicUID9; updated = true; }
            if (this.PublicUID1DomainRequired != updatedSubParty.PublicUID1DomainRequired) { this.PublicUID1DomainRequired = updatedSubParty.PublicUID1DomainRequired; updated = true; }
            if (this.PublicUID2DomainRequired != updatedSubParty.PublicUID2DomainRequired) { this.PublicUID2DomainRequired = updatedSubParty.PublicUID2DomainRequired; updated = true; }
            if (this.PublicUID3DomainRequired != updatedSubParty.PublicUID3DomainRequired) { this.PublicUID3DomainRequired = updatedSubParty.PublicUID3DomainRequired; updated = true; }
            if (this.PublicUID4DomainRequired != updatedSubParty.PublicUID4DomainRequired) { this.PublicUID4DomainRequired = updatedSubParty.PublicUID4DomainRequired; updated = true; }
            if (this.PublicUID5DomainRequired != updatedSubParty.PublicUID5DomainRequired) { this.PublicUID5DomainRequired = updatedSubParty.PublicUID5DomainRequired; updated = true; }
            if (this.PublicUID6DomainRequired != updatedSubParty.PublicUID6DomainRequired) { this.PublicUID6DomainRequired = updatedSubParty.PublicUID6DomainRequired; updated = true; }
            if (this.PublicUID7DomainRequired != updatedSubParty.PublicUID7DomainRequired) { this.PublicUID7DomainRequired = updatedSubParty.PublicUID7DomainRequired; updated = true; }
            if (this.PublicUID8DomainRequired != updatedSubParty.PublicUID8DomainRequired) { this.PublicUID8DomainRequired = updatedSubParty.PublicUID8DomainRequired; updated = true; }
            if (this.PublicUID9DomainRequired != updatedSubParty.PublicUID9DomainRequired) { this.PublicUID9DomainRequired = updatedSubParty.PublicUID9DomainRequired; updated = true; }
            if (this.WildCardPUIDStr != updatedSubParty.WildCardPUIDStr) { this.WildCardPUIDStr = updatedSubParty.WildCardPUIDStr; updated = true; }
            if (this.AllowCustomAnnouncement != updatedSubParty.AllowCustomAnnouncement) { this.AllowCustomAnnouncement = updatedSubParty.AllowCustomAnnouncement; updated = true; }
            if (this.PtySpareLong1 != updatedSubParty.PtySpareLong1) { this.PtySpareLong1 = updatedSubParty.PtySpareLong1; updated = true; }
            if (this.PtySpareString != updatedSubParty.PtySpareString) { this.PtySpareString = updatedSubParty.PtySpareString; updated = true; }
            if (this.PtySpareString2 != updatedSubParty.PtySpareString2) { this.PtySpareString2 = updatedSubParty.PtySpareString2; updated = true; }
            if (this.PtySpareShort1 != updatedSubParty.PtySpareShort1) { this.PtySpareShort1 = updatedSubParty.PtySpareShort1; updated = true; }
            if (this.PtySpareShort2 != updatedSubParty.PtySpareShort2) { this.PtySpareShort2 = updatedSubParty.PtySpareShort2; updated = true; }
            if (this.PtySpareBool1 != updatedSubParty.PtySpareBool1) { this.PtySpareBool1 = updatedSubParty.PtySpareBool1; updated = true; }
            if (this.PtySpareBool2 != updatedSubParty.PtySpareBool2) { this.PtySpareBool2 = updatedSubParty.PtySpareBool2; updated = true; }
            if (this.PtySpareBool3 != updatedSubParty.PtySpareBool3) { this.PtySpareBool3 = updatedSubParty.PtySpareBool3; updated = true; }
            if (this.PtySpareBool4 != updatedSubParty.PtySpareBool4) { this.PtySpareBool4 = updatedSubParty.PtySpareBool4; updated = true; }
            if (this.PtySpareBool5 != updatedSubParty.PtySpareBool5) { this.PtySpareBool5 = updatedSubParty.PtySpareBool5; updated = true; }
            if (this.PtySpareBool6 != updatedSubParty.PtySpareBool6) { this.PtySpareBool6 = updatedSubParty.PtySpareBool6; updated = true; }
            if (this.PtySpareBool7 != updatedSubParty.PtySpareBool7) { this.PtySpareBool7 = updatedSubParty.PtySpareBool7; updated = true; }
            if (this.PtySpareBool8 != updatedSubParty.PtySpareBool8) { this.PtySpareBool8 = updatedSubParty.PtySpareBool8; updated = true; }
            if (this.TerminatingTableId != updatedSubParty.TerminatingTableId) { this.TerminatingTableId = updatedSubParty.TerminatingTableId; updated = true; }
            if (this.AllowNonSipTelUri != updatedSubParty.AllowNonSipTelUri) { this.AllowNonSipTelUri = updatedSubParty.AllowNonSipTelUri; updated = true; }
            if (this.LocationType != updatedSubParty.LocationType) { this.LocationType = updatedSubParty.LocationType; updated = true; }
            if (this.RncID != updatedSubParty.RncID) { this.RncID = updatedSubParty.RncID; updated = true; }
            if (this.LteMcc != updatedSubParty.LteMcc) { this.LteMcc = updatedSubParty.LteMcc; updated = true; }
            if (this.LteMnc != updatedSubParty.LteMnc) { this.LteMnc = updatedSubParty.LteMnc; updated = true; }
            if (this.LteTac != updatedSubParty.LteTac) { this.LteTac = updatedSubParty.LteTac; updated = true; }
            if (this.MarketSID != updatedSubParty.MarketSID) { this.MarketSID = updatedSubParty.MarketSID; updated = true; }
            if (this.SwitchNumber != updatedSubParty.SwitchNumber) { this.SwitchNumber = updatedSubParty.SwitchNumber; updated = true; }
            if (this.CallsToWebUserProhibited != updatedSubParty.CallsToWebUserProhibited) { this.CallsToWebUserProhibited = updatedSubParty.CallsToWebUserProhibited; updated = true; }
            if (this.IMSI != updatedSubParty.IMSI) { this.IMSI = updatedSubParty.IMSI; updated = true; }
            if (this.IMSNotSupported != updatedSubParty.IMSNotSupported) { this.IMSNotSupported = updatedSubParty.IMSNotSupported; updated = true; }
            if (this.ValidateCellID != updatedSubParty.ValidateCellID) { this.ValidateCellID = updatedSubParty.ValidateCellID; updated = true; }
            if (this.OperatorID != updatedSubParty.OperatorID) { this.OperatorID = updatedSubParty.OperatorID; updated = true; }
            if (this.HomeMTA != updatedSubParty.HomeMTA) { this.HomeMTA = updatedSubParty.HomeMTA; updated = true; }
            if (this.ForwardDenyNumbers != updatedSubParty.ForwardDenyNumbers) { this.ForwardDenyNumbers = updatedSubParty.ForwardDenyNumbers; updated = true; }
            if (this.PlayAnnoFailNotForward != updatedSubParty.PlayAnnoFailNotForward) { this.PlayAnnoFailNotForward = updatedSubParty.PlayAnnoFailNotForward; updated = true; }
            if (this.MrfPoolID != updatedSubParty.MrfPoolID) { this.MrfPoolID = updatedSubParty.MrfPoolID; updated = true; }
            if (this.Custom120x != updatedSubParty.Custom120x) { this.Custom120x = updatedSubParty.Custom120x; updated = true; }

            if (this.AgcfEndpoint != updatedSubParty.AgcfEndpoint) { this.AgcfEndpoint = updatedSubParty.AgcfEndpoint; updated = true; }

            if (this.UserId != updatedSubParty.UserId) { this.UserId = updatedSubParty.UserId; updated = true; }

            if (updated) this.Updated = this.Inspected = DateTime.UtcNow.AddHours(3);
            //else this.Inspected = DateTime.UtcNow.AddHours(3);

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}