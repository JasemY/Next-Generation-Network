using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data.Entity;
using System.Configuration;

namespace Ia.Ngn.Cl.Model.Data.Huawei
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Optical Fiber Network Management Intranet Portal (OFN) support class for Huawei's Next Generation Network (NGN) data model
    /// </summary>
    /// 
    /// <value>
    /// - Add Service Reference to ims.api.huawei
    /// - Make sure web.config or app.config as:
    ///   <system.serviceModel>
    ///     <bindings>
    ///        <basicHttpBinding>
    ///           <binding name="ATSV100R003C01SPC100Binding" />
    ///        </basicHttpBinding>
    ///      </bindings>
    ///      <client>
    ///        <endpoint address="http://*.*.*.*:8080/spg" binding="basicHttpBinding" bindingConfiguration="ATSV100R003C01SPC100Binding" contract="ims.api.huawei.ATSV100R003C01SPC100" name="ATSV100R003C01SPC100Port" />
    ///      </client>
    ///   </system.serviceModel>
    ///                        
    /// - Add:
    ///  <appSettings>
    ///     <add key="imsHuaweiServerUser" value="*" />
    ///     <add key="imsHuaweiServerUserPassword" value="*" />
    ///   </appSettings>
    /// </value>
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
    public class Ims
    {
        //private static int allPossibleServiceNumbersWithinHuaweiNetworkArrayListIndex;
        //private static List<int> allPossibleServiceNumbersWithinHuaweiNetworkArrayList;

        /// <summary/>
        public enum ResultCode
        {
            OperationSucceeded = 0,
            AtsSystemInternalError = 1,
            TheSubscriberIsNotDefinedInTheHssOrAtsOrServiceDataIsNotConfiguredForTheSubscriber = 101

            #region Result Code Description
            /*
        Result Code Description

List of result code:
Result Code
 Description
 
0 Operation succeeded. 
1 ATS system internal error. 
2 Internal error. 
3 Command execution timeout. 
4 The service cannot be activated or deactivated repeatedly. 
5 The operation is restricted by operator determined barring. 
100 Unknown user, maybe the user does not exist. 
101 The subscriber is not defined in the HSS or ATS,or service data is not configured for the subscriber. 
102 Data cannot be modified. 
103 The sequence number of transparent data is incorrect. 
104 Unknown error from HSS. 
105 HSS message parameters are not found. 
106 The buffer size is too small. 
107 No memory resource of ATS. 
108 No data buffer resource. 
109 Parameter value is out of range. 
110 No basic data. Please run ADD SBR to configure the basic data first. 
111 Unknown data from HSS. 
112 Unknown command. 
113 Invalid URI format or call prefix not configured. 
114 Users with the call forwarding service cannot forward calls to users in the ImplicitIdentitys list. 
115 Users with the call forwarding service cannot forward calls to themselves. 
116 No user data. 
117 No service right. 
118 The UDA message includes unexpected data. 
119 Users with the call forwarding service cannot forward calls to the restricted numbers. 
121 The Centrex group number or Centrex user group number cannot be set for a non-Centrex subscriber. 
122 Time conflict. 
123 The end time is earlier than the start time. 
124 Invalid parameter: Dial profile. 
125 Invalid parameter: Profile name. 
126 The service is not registered. 
127 Parameter conflict. 
128 The data to be registered conflicts with the existing record. 
129 The subscriber has registered this service. 
130 Deleting the MUPT service data fails. 
131 The value of the Name parameter contains illegal characters: " . 
133 The PBX subscriber is not allowed to config the call waiting(CW) service. 
134 The physical IMPU and the subscriber number must be in the same alias group. 
135 The alias group must contain on SIP IMPU and two TEL IMPU at least. 
136 The physical IMPU must be in Tel URI format. 
137 The subscriber data is incomplete. Please remove the subscriber and then add the subscriber again. 
300 The format of the data to be updated to the HSS is incorrect. 
301 The data on the HSS cannot be read or written. Please check the data configuration of the HSS. 
302 The length of the data to be updated to the HSS exceeds the maximum limit of the HSS. 
303 The size of the configured service data exceeds the maximum size supported by the ATS. 
500 Failed to decode or encode the SOAP text. 
501 Failed to decode the SOAP text because a parameter value is out of range. The parameter is: 
502 One or more mandatory parameters are not found. Please specify these mandatory parameters. 
503 The SOAP message is too large. 
504 Failed to decode the SOAP message because the memory is insufficient. 
505 The parameter configuration exceeds the maximum number. As a result, SOAP messages cannot be decoded. The parameter is: 
1000 The user already exists. 
1001 The Local DN set parameter must be specified. 
1002 The Call source code parameter must be specified. 
1003 Invalid parameter: Local DN set. 
1004 Invalid parameter: Call source code. 
1005 Invalid parameter: Pulse charge case. 
1006 Invalid parameter: User category. 
1007 Invalid parameter: Voice group number. 
1008 Invalid parameter: Callee route source code. 
1009 Invalid parameter VoiceMailBox index. 
1010 Invalid parameter VideoMailBox index. 
1011 Prepaid Prefix Index does not exist. 
1012 The CRBT prefix index does not exist. 
1013 The CLIP and CIDCW conflict. 
1014 The RID, NRID, and CLIR conflict or they are registered. 
1015 The COLP and COLPOVR conflict. 
1016 The pulse charging type can not be configured for the AOC_D or AOC_E. 
1017 The SD1D and SD2D cannot co-exist with the SPEED_DIAL. 
1018 The CCW service right can be configured only for a subscriber that has the CW service right. 
1019 The record mapping the DSPIDX parameter must be defined first. 
1020 The call barring group must be configured first. 
1021 Obtaining the call source code failed. Please run ADD NUMSEGSRCMAP to configure the call source code that maps the IMPU. 
1022 Obtaining the local DN set failed. Please run ADD CALLSRC to configure the local DN set that maps the call source code. 
1200 The PBX user cannot be deleted by running RMV SBR. 
1300 No user data. 
1600 The same service data has been registered on the HSS. 
1601 The CFDATA service data does not exist. 
1602 Failed to register the CFDATA service because one parameter is invalid. 
1603 The CFDATA record number reaches the maximum. 
1604 No right to register the CFDATA service. 
1700 No user data. 
1701 One parameter in the RMV CFDATA command is invalid. 
1702 No CFDATA service data. 
1703 The data to be removed is not found. 
1800 No CFDATA service data. 
1900 The user already has the number change service. 
1901 The SOAP data is empty. 
1902 The new number is the same as itself. 
1903 The new number cannot be the same as one in the ImplicitIdentitys set. 
1910 The number invalidation status is incorrect. 
1911 The new number format is incorrect. 
2000 The SOAP data is empty. 
2001 The user does not have the number change service. 
2100 The user status is normal and thus cannot be modified. 
2101 No service data. 
2102 One parameter of the MOD NUMCHG command in the SOAP text is not found. 
2103 New IMPU, Number change mode, and Send short message flag are not specified. 
2104 The new number is the same as itself. 
2105 The modified data is the same as the data from HSS. 
2106 The new number cannot be the same as one in the ImplicitIdentitys set. 
2200 No number change service data. 
2300 The service data already exists. 
2400 No STRATEGY service data. 
2500 No STRATEGY service data. 
2700 The ADD SBR command is not found. 
2801 Barred by the call forwarding barring (CDIVBAR). 
2802 Barred by selective outgoing call (SOC). 
2803 Barred by operator determined barring (ODB). 
2804 Barred by outgoing call barring (CBA). 
2805 Barred by incoming only line (ICO). 
2806 Barred by the owing restricted service. 
2807 Barred by dial number call-out barring service (DNCALLOUTBAR). 
2808 Barred by outgoing call barring except green call (OCBEG). 
2809 Barred by customized call-out authority (CUSTOM). 
2810 Barred by outgoing call barring (OCB). 
2811 Barred by operator determined barring outgoing call (ODBBOC). 
2812 Barred by operator determined barring outgoing call when roaming outside home public land mobile network (ODBBOCROAM). 
2813 Barred by operator determined barring outgoing call except mobile subscriber. 
2814 Barring for international call. 
2815 Barring for prepaid user. 
2816 Barred by the carrier selection on call by call (CBC) restriction right. 
2817 Barred by illegal configuration of carrier selection on call by call (CBC). 
2818 Barred by dial number call-out allowing service (DNCALLOUTALLOW). 
2828 No service data. 
2829 At least one parameter must be specified. 
2830 The number of IMPU in ImplicitIdentitys set(IRS) exceeds the maximum. 
10000 No SOC service data. 
10001 The SOC service already exists. 
10002 The SOC record reaches the maximum. 
10003 The time of the SOC service conflicts with one existing record. 
10004 The start time of the SOC service is invalid. 
10005 The end time of the SOC service is invalid. 
10006 The end time of the SOC service is earlier than the start time. 
10007 The time of the SOC service conflicts. 
10008 The start time and end time of the SOC service are invalid. 
10009 The SOC number cannot be the same as the user number. 
10010 The SOC number cannot be the same as one in the ImplicitIdentitys set. 
10100 No SOC service data. 
10200 The user has not registered the SOC service. 
10201 The user has not registered the SOC service. 
10202 The SOC service data does not exist. 
10203 No SOC service data. 
10300 The SIC record reaches the maximum. 
10301 The start time of the SIC service is invalid. 
10302 The end time of the SIC service is invalid. 
10303 The time and IMPU are the same as those on the HSS and thus a conflict occurs. 
10304 The restricted number cannot be the same as the user number. 
10305 The restricted number cannot be the same as one in the ImplicitIdentitys set. 
10308 Caller IMPU must be specified. 
10400 No SIC service data. 
10500 The user has not registered the SIC service. 
10501 No SIC service data. 
10502 No SIC service data. 
10503 The length of the caller number must be 10 digits. 
10600 The DND service data does not exist. 
10601 The DND service data already exists. 
10602 Failed to download the service data. 
10603 The DND record reaches the maximum. 
10604 The Time parameter is invalid. 
10605 The Mode parameter is invalid. 
10606 The Group number parameter is invalid. 
10607 The Start year parameter is invalid. 
10608 The Start month parameter is invalid. 
10609 The Start day parameter is invalid. 
10610 The Start hour parameter is invalid. 
10611 The End year parameter is invalid. 
10612 The End month parameter is invalid. 
10613 The End day parameter is invalid. 
10614 The End hour parameter is invalid. 
10615 The mode is week, but the weekpattern is not set. 
10616 The mode is month, but the day is not set. 
10617 The mode is year, but the day and month are not set. 
10618 The hour and minute parameters must be specified or must not be specified simultaneously. 
10619 The end hour and end minute parameters must be specified or must not be specified simultaneously. 
10620 The start hour, start minute, end hour, and end minute parameters must be specified or not be specified simultaneously. 
10621 No valid date between start date and end date. 
10622 The end time of the DND is earlier than the start time of the DND. 
10623 The DND access mode can not be configured on the Web portal. 
10624 The start year, month, and day must be specified. 
10700 No service data. 
10800 The user has not registered the DND service. 
10801 No right to register the GOIR service. 
10900 The K value is not defined in the MOD PFXOCR command. 
11000 No CBA service data. 
11300 No GOIR service data. 
11301 No service data. 
11400 The user has not registered the GOIR service. 
11500 The GOIR service is registered and thus the MOIR service cannot be registered. 
11600 No MOIR service data. 
11601 The user has not registered the MOIR service. 
11800 The same number already exists. 
11801 The DN_CALL_OUT_ALLOW record reaches the maximum. 
11802 The number to be registered is a prefix of a number registered on the HSS. 
11900 No DN_CALL_OUT_ALLOW service data. 
12000 The user has not registered the service. 
12001 No DN_CALL_OUT_ALLOW service data. 
12002 Failed to obtain the DN_CALL_OUT_ALLOW service data. 
12003 The user has not registered the service. 
12100 The same number already exists. 
12101 The DN_CALL_OUT_BAR record reaches the maximum. 
12102 The number to be registered is a prefix of an existing number. 
12103 The number to be registered is sip URI. 
12200 No DN_CALL_OUT_BAR service data. 
12300 The user has not registered the service. 
12301 No DN_CALLOUT_BAR service data. 
12302 Failed to obtain the DN_CALL_OUT_BAR service data. 
12303 The user has not registered the service. 
12400 The value of one parameter in the SET_OWSBR command is invalid. 
12401 The parameter indicating the owing status of a user is invalid. 
12500 Failed to obtain the user data. 
12501 No user data. 
12700 The value of one parameter in the PRK_OWSBR command is invalid. 
12900 No user data. 
12901 No response data. 
12902 The REG COMSS command includes unauthorized services. 
12903 The RID, NRID and CLIR conflict. 
12904 The RID,NRID and CLIR rights are not configured. 
12905 The service(s) to be configured is not authorized to subscribers of the customized type (indicated by Customized user category). 
13100 When the TMODE is PERMANENT, the start time and end time can not be specified. 
13101 When the TMODE is TEMP, the start time and end time should be specified. 
13400 Failed to obtain the system time. 
13401 The WAKEUP service cannot be registered near the current system time. 
13402 The same record already exists. 
13403 The WAKEUP service record reaches the maximum. 
13404 The week time parameter is not specified. 
13405 The day time parameter is not specified. 
13406 The value of week is out of range. 
13407 The weeks value is small. 
13408 The WCLAN parameter is invalid. Please first configure LANGDEF on the OMU client. 
13500 No WAKEUP service data. 
13600 The service is not registered. 
13601 The hour and minute must be specified together. 
13602 No record is found. 
13900 No CFTB service data. 
13901 The record to be cancelled by running RPL CFTB is not found. 
14000 All the parameters in REG UINFO are empty. 
14100 No UINFO service data. 
14300 In REG MRINGMODE, IMPUn parameter format error: invalid URI format. 
14301 The CFMODE parameter in REG MRONGMOD must be specified. 
14302 In the multi-ringing service, each IMPU is unique. 
14303 REG MRINGMODE IMPU list is empty. 
14400 No MRINGMODE service data. 
14600 In the ImplicitIdentitys set, there is no SIP IMPU. 
14601 In the ImplicitIdentitys set, there is no TEL IMPU. 
14602 The PC-ONLY subscriber is not a SIP subscriber. 
14603 The PC-ONLY subscriber and the subscriber number are not in the same implicit registration set. 
14604 The user does not have the ImplicitIdentitys list. 
14700 The user has not registered the service. 
14800 No user data. 
14900 There are two identical services in REG SS. 
14901 The service to be activated is not registered. 
14902 The service to be deactivated is not registered. 
14903 The ACT SS command contains services that the subscriber does not have the right to use. 
14904 The DEA SS command contains services that the subscriber does not have the right to use. 
15000 No service data. 
15100 No right to register the CFU service. 
15200 No right to register the CFU service. 
15201 No user data. 
15202 No right to register the service. 
15203 No extended user data. 
15204 The IMPU must be a TEL URI or the alias group to which the IMPU belongs must contain a TEL URI. 
15300 No service data. 
15400 The CFU service is not registered and thus running RPL CFU fails. 
15401 No CFU service data. 
15402 No user data. 
15500 No right to register the CFB service. 
15600 No CFB service data. 
15601 No right to register the CFNR service. 
15602 No CFNR service data. 
15900 No CFNR service data. 
16200 No CFNL service data. 
16500 No CFNRC service data. 
16800 No CWCFNR service data. 
16900 The time parameter in REG CFS is invalid. 
16901 The CFS record reaches the maximum. 
16902 The CFS record reaches the maximum. 
16903 One parameter in REG CFS is invalid. 
16904 The same service has registered on the HSS. 
16905 The caller IMPU and the subscriber number cannot be in the same ImplicitIdentitys list. 
16906 The caller IMPU cannot be the same as the user IMPU. 
16907 Please enter a valid 10-digit caller number. 
17000 No user service data. 
17001 No CFS service data. 
17002 Failed to obtain the service that has the same EP parameter. 
17100 No CFS service data. 
17300 The start time is the same as an existing record. 
17301 The value of the Cycle type parameter is DEFAULT, which is the same as an existing record. 
17302 The CFTB record reaches the maximum. 
17303 The end time is smaller than the start time. 
17304 The start time of the CFTB is invalid. 
17305 The end time of the CFTB is invalid. 
17306 The year, month and date must be specified when cycle type is NORMAL. 
17307 The hour and minute must be specified when cycle type is not DEFAULT. 
17308 The week must be specified when cycle type is WEEK. 
17309 Service type must be specified. 
17400 One parameter in REG CFSB is invalid. 
17401 No CFSB service data. 
17600 No TMP_LIN service data. 
17700 One parameter in REG TMPLIN is invalid. 
17701 No TPMLIN service data. 
17900 One parameter in REG CODEC_CNTRL is invalid. 
18000 No user service data. 
18001 No user service data. 
18002 The user has not registered the service. 
18003 The user has not registered the CODEC CONTROL service. 
18100 No user service data. 
18101 The CODEC_CNTRL service is not registered. 
18102 Failed to obtain the user data. 
18200 Invalid parameter. 
18201 The bind mode DOUBLE is not applicable to Centrex user. 
18202 The bound-to IMPU can not be the IMPU of the service subscriber. 
18203 The bound-to IMPU can not be the IMPU forbidden by service provider. 
18204 The bind mode DOUBLE is used only when Binding IMPU is specified. 
18205 No user data. 
18206 The green call service is not registered. 
18207 The green call data does not exist. 
18208 Registering the green call service failed. 
18209 A mobile phone number can not bound to another mobile phone number. 
18210 The binding number is not a local number. 
18211 The IMPU of the bound-to subscriber must have the same country code as that of the service subscriber. 
18301 No extended subscriber data. 
18302 The service is not registered 
18303 The service data does not exist. 
18401 The hotline IMPU must be in Tel URI format. 
18402 No extended subscriber data. 
18403 The service is not registered. 
18404 The service data does not exist. 
18405 The hotline number cannot be the number of the hotline service subscriber or another IMPU in the same alias group. 
18406 The value of Hotline Delay Time is invalid. 
18501 No extended subscriber data. 
18502 The service is not registered. 
18503 The service data does not exist. 
18601 The nightservice IMPU cannot be the IMPU of the service subscriber. 
18602 No extended subscriber data. 
18603 The service is not registered. 
18604 The service data does not exist. 
18701 The backup IMPU cannot be the IMPU of the service subscriber. 
18702 No extended subscriber data. 
18703 The service is not registered. 
18704 The service data does not exist. 
18801 The console IMPU cannot be the IMPU of the service subscriber. 
18802 No extended subscriber data. 
18803 The service is not registered. 
18804 The service data does not exist. 
18900 IMS subscribers are not allowed to use the following services: 
18901 POTS subscribers are not allowed to use the following services: 
18902 G/U subscribers are not allowed to use the following services: 
18903 CDMA subscribers are not allowed to use the following services: 
18904 PSTN subscribers are not allowed to use the following services: 
19000 The service is not registered. 
19001 The service data does not exist. 
19002 The Multiple Subscriber Number service right cannot be configured for a non-IRS subscriber. 
19003 The Multiple Subscriber Number service right can be configured only for the default IMPU of the IRS. 
19004 The CET mode is not configured. You can run MOD MCIDCFG to configure it. 
19005 Invalid MCID phase parameter. 
19100 The subscriber has not registered the service. 
19101 No service data. 
19102 The Busy Notify, No Answer Notify, and Not Reachable Notify parameters cannot be set to FALSE at the same time. 
19103 The Videotelephony subscriber is not allowed to register the miss call notify(MCN) service. 
19200 The number of records of Distinct Alerting service has reached the maximum. 
19201 The record does not exist. 
19202 Source IMPU must be a valid 10-digit number. 
19300 The alias set must contains at least one SIP URI number. 
19301 The alias set must contains at least one TEL URI number. 
19302 The PC IMPU must be a SIP URI number. 
19303 The PC IMPU is not in the alias set. 
19400 The STB IMPU and the subscriber number cannot be in the same implicit registration set. 
19401 The STB IMPU cannot be the same as the subscriber number. 
19402 The STB IMPU must be a global number. 
19500 The paramer of Priority call number is invalid. 
19501 The record of Priority call number already exists. 
19502 Start time or Stop time is invalid. 
19503 The record of Priority call number is not found. 
19600 The Binding IMPU must be a global number. 
20014 The subscriber has applied for VIMPU. 
20016 The INBOUND and OUTBOUND services are mutually exclusively. 
20019 The system is locked. Thus, you can perform service query only. 
20020 Please enter a valid 10-digit forwarded-to number. 
20021 Please enter a valid 11-digit forwarded-to number that is prefixed with 1. 
20022 Please enter a valid 7-digit forwarded-to number. 
30100 The one number service data records registered for the subscriber have reached the maximum number. 
30101 A one number service data record with the same index or priority already exists. 
30102 The specified one number service data record does not exist. 
30103 The subscriber can register one Caller One Number data record only. 
30104 The service type conflicts with the number type. 
30105 Service type or Ringing type cannot match the association data. 
30106 The association list does not exist. 
30201 The association list data records registered for the subscriber have reached the maximum number. 
30202 The specified association list data record does not exist. 
30203 A association list data record with the same index and type already exists. 
30204 The association type of this record conflicts with that of another record in the same association index. 
30205 The association number of a record must be unique. 
30206 Invalid association number format. 
30207 The displayed number must be a valid global number. 
30208 The primary number must be a valid global number. 
30209 This record is referenced by the One Number service data and thus it cannot be removed. 
30210 Failed to obtain the country code of the asssociation number. 
30107 The Filter criteria is invalid. Only digits, letters, underscores and semicolons are allowed. 
30108 When the Ringing type is set to Call forwarding or Call forwarding to voice mailbox, the Call forwarding mode must be selected. 
30300 The number of registered Filter criteria records has reached the maximum number that a single subscriber can register. 
30301 The Filter criteria name is invalid. Only digits, letters, and underscores are allowed. 
30302 Please configure at least one Filter criteria. 
30303 If Filter by caller information is selected as a Filter criteria, the Caller information must be filled. 
30304 If Filter by presence status is selected as a Filter criteria, the Presence status must be filled. 
30305 If Filter by time is selected as a Filter criteria, the Holiday group index or the Date mode must be filled. 
30306 The specified Holiday group index does not exist. Please run ADD HLDYGRP to configure the Holiday group index first. 
30307 The Date mode is Month+order+day of week. The parameters Month, Order and Day of week must be specified. 
30308 The Date mode is Month+start day+end day. The parameters Month, Start day and End day must be specified. 
30309 The Date mode is Start date+end date. The parameters Start date and End date must be specified. 
30310 The End day cannot be earlier than the Start day. 
30311 The End date cannot be earlier than the Start date. 
30312 The Start date is invalid. 
30313 The End date is invalid. 
30314 A record with the same Filter criteria name already exits. 
30315 The specified record does not exist. 
30316 The Start time is invalid. 
30317 The End time is invalid. 
30318 The End time cannot be earlier than the Start time. 
30319 The Start day is invalid. 
30320 The End day is invalid. 
30211 Please enter a valid 10-digit Association number. 
30212 Please enter a valid 11-digit Association number that is prefixed with 1. 
30213 Please enter a valid 7-digit Association number. 
30400 The format of the Caller number is invalid. 
30401 The Caller number must be a global number. 
30403 The specified record already exists. 
30404 The specified record does not exist. 
30405 The number of registered Caller information records has reached the maximum number that a single subscriber can register. 
35604 The subscriber does not register the One Number service. 
35609 There is a service conflict. 
35610 The service right of the number is restricted. 
35611 The maximum number of secondary numbers has been exceeded. 
35612 The specified number is already associated with another subscriber. 
35616 Invalid domain name. 
35617 Invalid phone number. 
35625 Cannot remove the subscriber data: The input parameters are incorrect. 
35669 Cannot remove the subscriber data: The specified data is associated with other subscribers. 
35799 Other errors. 
90000 In ADD BLKNUM, BIMPU parameter format error: invalid URI format. 
90001 The black or white IMPU already exists in the service data. 
90002 The MCR, DND, or call forwarding service right must be configured. 
90003 No basic user data. 
90004 The record reaches the maximum. 
90005 The black number cannot be the same as the user number. 
90006 The black number cannot be the same as one in the ImplicitIdentitys set. 
90100 The record is not found in the black or white number service data. 
90300 The ACR, ACRTOVM, DND, or call forwarding service right must be configured. 
90301 The white number cannot be the same as the user number. 
90302 The number cannot be the same as one in the ImplicitIdentitys set. 
90600 Green or red number format error: invalid URI format. 
90601 The green or red number already exists. 
90602 The green number cannot be the same as the user number. 
90603 The green number cannot be the same as one in the ImplicitIdentitys set. 
90604 The record reaches the maximum. 
90700 The green or red number is not found. 
90900 The red number cannot be the same as one in the ImplicitIdentitys set. 
90901 The red IMPU cannot be the same as the user IMPU. 
91000 The NPAS state of the subscriber is incorrect. 
91001 The Permissive time is illegal. 
91002 The Announce time is illegal. 
91003 The End time is illegal. 
91004 The Permissive time must be smaller than or equal to the Announce time. 
91005 The Announce time must be smaller than or equal to the End time. 
91006 At least one of the Permissive time, the Announce time, and the End time must be greater than the current time. 
91100 The VIMPU parameter must be set to a TEL URI number. 
92400 The basic service group is not supported. 
92401 The Basic service group in http command is not the same as the one in CFX service data. 
92402 User is not allowed to perform the operation. 
92403 Can not get the home and visited nation code. 
92404 Can not get the forward number nation code. 
92405 Barred by ODBBRACF. 
92406 Barred by ODBBRICF. 
92407 Barred by ODBBRICFEXHC. 
92408 Barred by BAOC. 
92409 Barred by BAIC. 
92410 Barred by BOIC. 
92411 Barred by BOICEXHC. 
92412 Barred by BICROM. 
92413 Activating the CCF service is barred by CFU. 
92414 Failed to obtain the CFU service data. 
92415 Failed to obtain the CFB service data. 
92416 Failed to obtain the CFNR service data. 
92417 Failed to obtain the CFNRC service data. 
92418 Failed to obtain the CCF service data. 
92200 The user is not mobile subscriber. 
92201 Some field does not match usertype. 
92202 The service has not been activated. 
92210 The BAOC service has not been activated. 
92211 The BOIC service has not been activated. 
92212 The BOICEXHC service has not been activated. 
92213 The BAIC service has not been activated. 
92214 The BICROM service has not been activated. 
92224 The CFU service has been activated. The CFU and BAOC services are mutually exclusive. 
92225 The CFB Service has been activated. The CFB and BAOC services are mutually exclusive. 
92226 The CFNR Service has been activated. The CFNR and BAOC services are mutually exclusive. 
92227 The CFNRC Service has been activated. The CFNRC and BAOC services are mutually exclusive. 
92237 The CFU Service has been activated. The CFU and BOIC services are mutually exclusive. 
92238 The CFB Service has been activated. The CFB and BOIC services are mutually exclusive. 
92239 The CFNR Service has been activated. The CFNR and BOIC services are mutually exclusive. 
92240 The CFNRC Service has been activated. The CFNRC and BOIC services are mutually exclusive. 
92250 The CFU Service has been activated. The CFU and BOICExHC services are mutually exclusive. 
92251 The CFB Service has been activated. The CFB and BOICExHC services are mutually exclusive. 
92252 The CFNR Service has been activated. The CFNR and BOICExHC services are mutually exclusive. 
92253 The CFNRC Service has been activated. The CFNRC and BOICExHC services are mutually exclusive. 
92263 The CFU Service has been activated. The CFU and BAIC services are mutually exclusive. 
92264 The CFB Service has been activated. The CFB and BAIC services are mutually exclusive. 
92265 The CFNR Service has been activated. The CFNR and BAIC services are mutually exclusive. 
92266 The CFNRC Service has been activated. The CFNRC and BAIC services are mutually exclusive. 
92276 The CFU Service has been activated. The CFU and BICROM services are mutually exclusive. 
92277 The CFB Service has been activated. The CFB and BICROM services are mutually exclusive. 
92278 The CFNR Service has been activated. The CFNR and BICROM services are mutually exclusive. 
92279 The CFNRC Service has been activated. The CFNRC and BICROM services are mutually exclusive. 
92280 An unknown error occurs during number analysis. 
92281 An internal error occurs during number analysis. 
92300 The length of Abbreviated Dial Number exceed three. 
92301 The abbreviated dial Number contains invalid character. 
92302 The number of SpeedDial codes registered by a subscriber reaches the maximum. 
92303 The number of SpeedDial services registered on a single CCU reaches the maximum. 
92304 The number of subscribers that has registered the SpeedDial service on a single CCU reaches the maximum. 
92305 The destination dial number cannot be the sip URI type. 
92306 The destination dial number cannot be the same as the user IMPU. 
92307 The destination dial number cannot be the same as one in the ImplicitIdentitys set. 
92308 The destination dial number contains invalid character. 
92309 The service data does not exist. 
92310 The specific speed dial data does not exist. 
92311 The destination dial number cannot be configured in ADD SPDLBAR. 
92312 The format of new IMPU is invalid. 
92313 The short number is invalid. 
92314 The short number is smaller than the configured value. 
92315 The length of the destination number reaches the maximum. 
93001 The service is restricted by license. 
94000 Some parameters conflict with those in the template. 
94001 Failed to obtain the template data. 
94002 The subscriber does not have the right to use the service in the template. 
94003 No template data. 
    */
            #endregion
        }

        /// <summary/>
        public static string UserName { get { return ConfigurationManager.AppSettings["imsHuaweiServerUser"].ToString(); ; } }
        /// <summary/>
        public static string Password { get { return ConfigurationManager.AppSettings["imsHuaweiServerUserPassword"].ToString(); ; } }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Ims()
        {
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateSbrWithResultData(string impu, ResultCode resultCode, ref Ia.Ngn.Cl.Model.Huawei.HuSbr newHuSbr)
        {
            bool isOk;
            Ia.Ngn.Cl.Model.Huawei.HuSbr huSbr;

            isOk = false;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                huSbr = (from q in db.HuSbrs where q.IMPU == impu select q).SingleOrDefault();

                switch (resultCode)
                {
                    case Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode.OperationSucceeded:
                        {
                            if (huSbr == null)
                            {
                                newHuSbr.Created = newHuSbr.Updated = newHuSbr.Inspected = DateTime.UtcNow.AddHours(3);

                                db.HuSbrs.Add(newHuSbr);
                            }
                            else
                            {
                                if (huSbr.Update(newHuSbr))
                                {
                                    db.HuSbrs.Attach(huSbr);
                                    db.Entry(huSbr).State = System.Data.Entity.EntityState.Modified;
                                }
                            }

                            break;
                        }
                    case Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode.TheSubscriberIsNotDefinedInTheHssOrAtsOrServiceDataIsNotConfiguredForTheSubscriber:
                        {
                            if (huSbr == null) { }
                            else db.HuSbrs.Remove(huSbr);

                            break;
                        }
                    default:
                        {
                            throw new Exception("Undefined result code seen in Ia.Ngn.Cl.Model.Business.Huawei.Ims.ResultCode");
                        }
                }

                db.SaveChanges();

                isOk = true;
            }

            return isOk;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateServiceForSbr(string impu, Ia.Ngn.Cl.Model.Huawei.HuSbr huSbr)
        {
            bool isOk;
            int i, n, serviceType;
            string serviceId;
            Ia.Ngn.Cl.Model.Service2 service, newService;

            isOk = false;
            serviceType = 1;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                n = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Number(impu);
                serviceId = Ia.Ngn.Cl.Model.Service2.ServiceId(n.ToString(), serviceType);

                if (huSbr != null)
                {
                    newService = new Ia.Ngn.Cl.Model.Service2();

                    newService.Id = serviceId;
                    newService.AreaCode = Ia.Ngn.Cl.Model.Data.Service.CountryCode;
                    newService.Service = n.ToString();
                    newService.ServiceType = serviceType;

                    //newService.ServiceSuspension =;
                    //newService.AlarmCall = ;
                    //newService.InternationalCallingUserControlled =;
                    //newService.InternationalCalling =;
                    newService.AbbriviatedCalling = huSbr.NSABRC;
                    newService.CallerId = huSbr.NSCLIP;
                    newService.CallForwarding = huSbr.NSCFU;
                    newService.CallWaiting = huSbr.NSCW;
                    newService.ConferenceCall = huSbr.NS3PTY;
                    newService.Pin = (int.TryParse(huSbr.COP, out i)) ? i : 0;
                    //newService.LineCard = "Hu?";
                    newService.WakeupCall = huSbr.NSWAKE_UP;

                    // below:
                    //newService.Access = Ia.Ngn.Cl.Model.Access.Read(db, srs.Access.Id);

                    service = (from q in db.Service2s where q.Id == newService.Id select q).SingleOrDefault();

                    if (service == null)
                    {
                        newService.Created = newService.Updated = newService.Inspected = DateTime.UtcNow.AddHours(3);

                        db.Service2s.Add(newService);
                    }
                    else
                    {
                        if (service.Update(newService))
                        {
                            db.Service2s.Attach(service);
                            db.Entry(service).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                }
                else
                {
                    service = (from q in db.Service2s where q.Id == serviceId select q).SingleOrDefault();

                    if (service != null) db.Service2s.Remove(service);
                }

                db.SaveChanges();

                isOk = true;
            }

            return isOk;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool CreateSubscriberAndBlaBlaBla(ims.api.huawei.ATSV100R003C01SPC100Client client, ims.api.huawei.Authentication authentication, string service, Ia.Ngn.Cl.Model.Business.Service.SupplementaryService somethingService, bool supplementaryServiceState, out string result)
        {
            bool b, tempSkip;
            string messageId, meName, impu, impuSipDomain;
            ims.api.huawei.LST_SBRType lstSbrType;
            ims.api.huawei.LST_SBRStruct1 lstSbrStruct1;
            ims.api.huawei.ResultType resultType;
            Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode resultCode;

            b = false;
            messageId = "1";
            meName = "tecats1";

            result = "";

            impu = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Impu(service);
            impuSipDomain = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Impu(service);

            // USE ME:name=techss;
            // ADD HSDAINF:IMPI="+96523715499@ims.moc.kw",HUSERNAME="+96523715499@ims.moc.kw",PWD=admin,REALM="ims.moc.kw";

            /*
ADD HSUB:SUBID="+96523715499@ims.moc.kw",IMPI="+96523715499@ims.moc.kw",IMPU="sip:+96523715499@ims.moc.kw";

ADD HIMPU:IMPI="+96523715499@ims.moc.kw",IMPU="tel:+96523715499";

SET HREGAUTH:IMPU="sip:+96523715499@ims.moc.kw",REGAUTH=TRUE;

SET HVNTPLID: IMPU="sip:+96523715499@ims.moc.kw", VNTPLID=0;

SET HVNTPLID: IMPU="tel:+96523715499", VNTPLID=0;

SET HIRS:IRSID=1,IMPULIST="\"sip:+96523715499@ims.moc.kw\"&\"tel:+96523715499\""; 

SET HDEFIMPU: IRSID=1, IMPU="sip:+96523715499@ims.moc.kw";

SET HSPSHARE:BASEIMPU="sip:+96523715499@ims.moc.kw",IMPU="tel:+96523715499";

SET HALIASPU:ALIASID=1,IMPULIST="\"sip:+96523715499@ims.moc.kw\"&\"tel:+96523715499\"";


ADD HSIFC:IMPU="sip:+96523715499@ims.moc.kw",SIFCID=1;
             */

            // USE ME:name=tecats0; 
            // ADD SBR:IMPU="sip:+96523715499@ims.moc.kw",TEMPLATEIDX=65535,DSPIDX=65534,LP=0,CSC=0,UTYPE=POTS,VCCFLAG=NO,VTFLAG=NO,NSCFU=0,NSCFUVM=0,NSCFB=0,NSCFBVM=0,NSCFNR=0,NSCFNRVM=0,NSCFNL=0,NSCFNLVM=0,NSCD=0,NSCDVM=0,NSCFNRC=0,NSCFNRCVM=0,NSCLIP=0,NSCIDCW=0,NSRIO=0,NSCNIP=0,NSCLIR=0,NSRIP=0,NSCNIR=0,NSRID=0,NSNRID=0,NSRND=0,NSNRND=0,NSCW=0,NSCCW=0,NSOIP=0,NSACRM=0,NSGOIR=0,NSMOIR=0,NSTIP=0,NSTIR=0,NSOTIR=0,NSCLIPNOSCREENING=0,NSCR=0,NSWAKE_UP=0,NSAOC_D=0,NSAOC_E=0,NSXEXH=0,NSXEGJ=0,NSCWCFNR=0,NSIIFC=0,NSDN_CALL_OUT_BAR=0,NSCCBS=0,NSCCNR=0,NSCCBSR=0,NSCCNRR=0,NS3PTY=0,NSNPTY=0,NSDND=0,NSMCR=0,NSCBA=0,NSTMP_LIN=0,NSCODEC_CNTRL=0,NSMWI=0,NSDC=0,NSHOLD=0,NSECT=0,NSCFTB=0,NSDAN=0,NSSTOP_SECRET=0,NSMCID=0,NSEBO=0,NSICO=0,NSOUTG=0,NSINQYH=0,NSUINFO=0,NSDN_CALL_OUT_ALLOW=0,NSSIC=0,NSSOC=0,NSSETCFNRTIME=0,NSCFS=0,NSCFSB=0,NSFAX=0,NSABRC=0,NSACRTOVM=0,NSPREPAID=0,NSCRBT=0,NSICB=0,NSMRINGING=0,NSCIS=0,NSCBEG=0,NSCOLP=0,NSCOLR=0,NSCOLPOVR=0,NSBAOC=0,NSBOIC=0,NSBOICEXHC=0,NSBAIC=0,NSBICROM=0,NSSPEED_DIAL=0,NSSD1D=0,NSSD2D=0,NSGRNCALL=0,NSCPARK=0,NSGAA=0,NSQSNS=0,NSMSN=0,NSHOTLINE=0,NSAOC_S=0,NSNIGHTSRV=0,NSBACKNUM=0,NSAUTOCON=0,NSCAMPON=0,NSCTD=0,NSCLICKHOLD=0,NSQUEUE=0,NSSANSWER=0,NSICENCF=0,NSCFGO=0,NSCECT=0,NSCTGO=0,NSCTIO=0,NSSETBUSY=0,NSOVERSTEP=0,NSABSENT=0,NSMONITOR=0,NSFMONITOR=0,NSDISCNT=0,NSFDISCNT=0,NSINSERT=0,NSFINSERT=0,NSASI=0,NSPWCB=0,NSRD=0,NSLCPS=0,NSNCPS=0,NSICPS=0,NSCBCLOCK=0,NSMINIBAR=0,NSMCN=0,NSDSTR=0,NSOPRREG=0,NSONEKEY=0,NSINBOUND=0,NSOUTBOUND=0,NSCALLERID=0,NSCUN=0,NSIPTVVC=0,NSNP=0,NSSEC=0,NSSECSTA=0,NSHRCN=0,NSSB=0,LCO=1,LC=1,LCT=1,NTT=1,ITT=0,ICTX=1,OCTX=1,INTT=0,IITT=0,ICLT=0,ICDDD=0,ICIDD=0,IOLT=0,CTLCO=1,CTLCT=1,CTLD=1,CTINTNANP=1,CTINTWORLD=1,CTDA=1,CTOSM=1,CTOSP=0,CTOSP1=0,CCO1=0,CCO2=0,CCO3=0,CCO4=0,CCO5=0,CCO6=0,CCO7=0,CCO8=0,CCO9=0,CCO10=0,CCO11=0,CCO12=0,CCO13=0,CCO14=0,CCO15=0,CCO16=0,HIGHENTCO=0,OPERATOR=1,SUPYSRV=1,IDDCI=1,NTCI=1,LTCI=1,RSC=65535,CIG=4294967295,OUTRST=NO,INRST=NO,NOAT=20,VMAIND=65535,VDMAIND=65535,TGRP=65534,TIDHLD=HOLD,TIDCW=PLEASE_WAITING,SCF=NO,LMTGRP=65534,FLBGRP=65535,SLBGRP=65535,COP="0000",G711_64K_A_LAW=1,G711_64K_U_LAW=1,G722=1,G723=1,G726=1,G728=1,G729=1,CODEC_MP4A=1,CODEC2833=1,CODEC2198=1,G726_40=1,G726_32=1,G726_24=1,G726_16=1,AMR=1,CLEARMODE=1,ILBC=1,SPEEX=1,G729EV=1,EVRC=1,EVRCB=1,H261=1,H263=1,CODEC_MP4V=1,H264=1,T38=1,T120=1,G711A_VBD=1,G711U_VBD=1,G726_VBD=1,G726_40_VBD=1,G726_32_VBD=1,G726_24_VBD=1,G726_16_VBD=1,WIND_BAND_AMR=1,GSM610=1,H263_2000=1,BROADVOICE_32=1,UNKNOWN_CODEC=1,ACODEC=NONE,VCODEC=NONE,POLIDX=255,NCPI=255,ICPI=255,EBOCL=NOEBOL,EBOPL=NOEBOL,EBOIT=INVALID,RM=NOTPLAYTONE,CPC=ORDINARY,PCHG=65535,TFPT=NULL,CHT=CCF,MCIDMODE=NSWNA,MCIDCMODE=NOCM,MCIDAMODE=TEMPORARY,PREPAIDIDX=65535,CRBTID=65535,ODBBICTYPE=ODBNOBIC,ODBBOCTYPE=ODBNOBOC,ODBBARTYPE=ODBNOBAR,ODBSS=NO,ODBBRCFTYPE=ODBNOBRCF,PNOTI=NO,MAXPARACALL=1,ATSDTMBUSY=YES,CALLCOUNT=COUNT_CR,CDNOTICALLER=NO,ISCHGFLAG=CHARGE,CHC=NORMAL_USER,CUSER=0,STCF=0,CHARSC=65535,REGUIDX=0,SOCBFUNC=NULL,SOCBPTONEIDX=65535,ADMINCBA=NO,ADCONTROL_DIVERSION=NO,CPCRUS=255,CUSCAT=NORMAL,SPT100REL=YES; 
            resultType = client.ADD_SBR(authentication, meName, ref messageId, impuSipDomain, 65535 /* TEMPLATEIDX */, 65534 /* DSPIDX */, 0 /* LP */, 0 /* CSC */, "" /* UNAME */, "POTS" /* UTYPE */, "NO" /* VCCFLAG */, "NO" /* VTFLAG */, 0 /* NSCFU */, 0 /* NSCFUVM */, 0 /* NSCFB */, 0 /* NSCFBVM */, 0 /* NSCFNR */, 0 /* NSCFNRVM */, 0 /* NSCFNL */, 0 /* NSCFNLVM */, 0 /* NSCD */, 0 /* NSCDVM */, 0 /* NSCFNRC */, 0 /* NSCFNRCVM */, 0 /* NSCLIP */, 0 /* NSCIDCW */, 0 /* NSRIO */, 0 /* NSCNIP */, 0 /* NSCLIR */, 0 /* NSRIP */, 0 /* NSCNIR */, 0 /* NSRID */, 0 /* NSNRID */, 0 /* NSRND */, 0 /* NSNRND */, 0 /* NSCW */, 0 /* NSCCW */, 0 /* NSOIP */, 0 /* NSACRM */, 0 /* NSGOIR */, 0 /* NSMOIR */, 0 /* NSTIP */, 0 /* NSTIR */, 0 /* NSOTIR */, 0 /* NSCLIPNOSCREENING */, 0 /* NSCR */, 0 /* NSWAKE_UP */, 0 /* NSAOC_D */, 0 /* NSAOC_E */, 0 /* NSXEXH */, 0 /* NSXEGJ */, 0 /* NSCWCFNR */, 0 /* NSIIFC */, 0 /* NSDN_CALL_OUT_BAR */, 0 /* NSCCBS */, 0 /* NSCCNR */, 0 /* NSCCBSR */, 0 /* NSCCNRR */, 0 /* NS3PTY */, 0 /* NSNPTY */, 0 /* NSDND */, 0 /* NSMCR */, 0 /* NSCBA */, 0 /* NSTMP_LIN */, 0 /* NSCODEC_CNTRL */, 0 /* NSMWI */, 0 /* NSDC */, 0 /* NSHOLD */, 0 /* NSECT */, 0 /* NSCFTB */, 0 /* NSDAN */, 0 /* NSSTOP_SECRET */, 0 /* NSMCID */, 0 /* NSEBO */, 0 /* NSICO */, 0 /* NSOUTG */, 0 /* NSINQYH */, 0 /* NSUINFO */, 0 /* NSDN_CALL_OUT_ALLOW */, 0 /* NSSIC */, 0 /* NSSOC */, 0 /* NSSETCFNRTIME */, 0 /* NSCFS */, 0 /* NSCFSB */, 0 /* NSFAX */, 0 /* NSABRC */, 0 /* NSACRTOVM */, 0 /* NSPREPAID */, 0 /* NSCRBT */, 0 /* NSICB */, 0 /* NSMRINGING */, 0 /* NSCIS */, 0 /* NSCBEG */, 0 /* NSCOLP */, 0 /* NSCOLR */, 0 /* NSCOLPOVR */, 0 /* NSBAOC */, 0 /* NSBOIC */, 0 /* NSBOICEXHC */, 0 /* NSBAIC */, 0 /* NSBICROM */, 0 /* NSSPEED_DIAL */, 0 /* NSSD1D */, 0 /* NSSD2D */, 0 /* NSGRNCALL */, 0 /* NSCPARK */, 0 /* NSGAA */, 0 /* NSQSNS */, 0 /* NSMSN */, 0 /* NSHOTLINE */, 0 /* NSAOC_S */, 0 /* NSNIGHTSRV */, 0 /* NSBACKNUM */, 0 /* NSAUTOCON */, 0 /* NSCAMPON */, 0 /* NSCTD */, 0 /* NSCLICKHOLD */, 0 /* NSQUEUE */, 0 /* NSSANSWER */, 0 /* NSICENCF */, 0 /* NSCFGO */, 0 /* NSCECT */, 0 /* NSCTGO */, 0 /* NSCTIO */, 0 /* NSSETBUSY */, 0 /* NSOVERSTEP */, 0 /* NSABSENT */, 0 /* NSMONITOR */, 0 /* NSFMONITOR */, 0 /* NSDISCNT */, 0 /* NSFDISCNT */, 0 /* NSINSERT */, 0 /* NSFINSERT */, 0 /* NSASI */, 0 /* NSPWCB */, 0 /* NSRD */, 0 /* NSLCPS */, 0 /* NSNCPS */, 0 /* NSICPS */, 0 /* NSCBCLOCK */, 0 /* NSMINIBAR */, 0 /* NSMCN */, 0 /* NSDSTR */, 0 /* NSOPRREG */, 0 /* NSONEKEY */, 0 /* NSINBOUND */, 0 /* NSOUTBOUND */, 0 /* NSCALLERID */, 0 /* NSCUN */, 0 /* NSIPTVVC */, 0 /* NSNP */, 0 /* NSSEC */, 0 /* NSSECSTA */, 0 /* NSHRCN */, 0 /* NSSB */, 0 /* NSOCCR */, 1 /* LCO */, 1 /* LC */, 1 /* LCT */, 1 /* NTT */, 0 /* ITT */, 1 /* ICTX */, 1 /* OCTX */, 0 /* INTT */, 0 /* IITT */, 0 /* ICLT */, 0 /* ICDDD */, 0 /* ICIDD */, 0 /* IOLT */, 1 /* CTLCO */, 1 /* CTLCT */, 1 /* CTLD */, 1 /* CTINTNANP */, 1 /* CTINTWORLD */, 1 /* CTDA */, 1 /* CTOSM */, 0 /* CTOSP */, 0 /* CTOSP1 */, 0 /* CCO1 */, 0 /* CCO2 */, 0 /* CCO3 */, 0 /* CCO4 */, 0 /* CCO5 */, 0 /* CCO6 */, 0 /* CCO7 */, 0 /* CCO8 */, 0 /* CCO9 */, 0 /* CCO10 */, 0 /* CCO11 */, 0 /* CCO12 */, 0 /* CCO13 */, 0 /* CCO14 */, 0 /* CCO15 */, 0 /* CCO16 */, 0 /* HIGHENTCO */, 1 /* OPERATOR */, 1 /* SUPYSRV */, 1 /* IDDCI */, 1 /* NTCI */, 1 /* LTCI */, 65535 /* RSC */, 4294967295 /* CIG */, "NO" /* OUTRST */, "NO" /* INRST */, 20 /* NOAT */, 0 /* RINGCOUNT */, 65535 /* VMAIND */, 65535 /* VDMAIND */, 65534 /* TGRP */, "HOLD" /* TIDHLD */, "PLEASE_WAITING" /* TIDCW */, "NO" /* SCF */, 65534 /* LMTGRP */, 65535 /* FLBGRP */, 65535 /* SLBGRP */, "0000" /* COP */, 1 /* G711_64K_A_LAW */, 1 /* G711_64K_U_LAW */, 1 /* G722 */, 1 /* G723 */, 1 /* G726 */, 1 /* G728 */, 1 /* G729 */, 1 /* CODEC_MP4A */, 1 /* CODEC2833 */, 1 /* CODEC2198 */, 1 /* G726_40 */, 1 /* G726_32 */, 1 /* G726_24 */, 1 /* G726_16 */, 1 /* AMR */, 1 /* CLEARMODE */, 1 /* ILBC */, 1 /* SPEEX */, 1 /* G729EV */, 1 /* EVRC */, 1 /* EVRCB */, 1 /* H261 */, 1 /* H263 */, 1 /* CODEC_MP4V */, 1 /* H264 */, 1 /* T38 */, 1 /* T120 */, 1 /* G711A_VBD */, 1 /* G711U_VBD */, 1 /* G726_VBD */, 1 /* G726_40_VBD */, 1 /* G726_32_VBD */, 1 /* G726_24_VBD */, 1 /* G726_16_VBD */, 1 /* WIND_BAND_AMR */, 1 /* GSM610 */, 1 /* H263_2000 */, 1 /* BROADVOICE_32 */, 1 /* UNKNOWN_CODEC */, "NONE" /* ACODEC */, "NONE" /* VCODEC */, 255 /* POLIDX */, 255 /* NCPI */, 255 /* ICPI */, "NOEBOL" /* EBOCL */, "NOEBOL" /* EBOPL */, "INVALID" /* EBOIT */, "NOTPLAYTONE" /* RM */, "ORDINARY" /* CPC */, 65535 /* PCHG */, "NULL" /* TFPT */, "CCF" /* CHT */, "NSWNA" /* MCIDMODE */, "NOCM" /* MCIDCMODE */, "TEMPORARY" /* MCIDAMODE */, 65535 /* PREPAIDIDX */, 65535 /* CRBTID */, "ODBNOBIC" /* ODBBICTYPE */, "ODBNOBOC" /* ODBBOCTYPE */, "ODBNOBAR" /* ODBBARTYPE */, "NO" /* ODBSS */, "ODBNOBRCF" /* ODBBRCFTYPE */, "NO" /* PNOTI */, 1 /* MAXPARACALL */, "YES" /* ATSDTMBUSY */, "COUNT_CR" /* CALLCOUNT */, "NO" /* CDNOTICALLER */, "CHARGE" /* ISCHGFLAG */, "NORMAL_USER" /* CHC */, 0 /* CUSER */, "" /* CGRP */, "" /* CUSERGRP */, 0 /* STCF */, 65535 /* CHARSC */, 0 /* REGUIDX */, "NULL" /* SOCBFUNC */, 65535 /* SOCBPTONEIDX */, "NO" /* ADMINCBA */, "NO" /* ADCONTROL_DIVERSION */, "" /* DPR */, "" /* PRON */, 255 /* CPCRUS */, "NORMAL" /* CUSCAT */, "YES" /* SPT100REL */);

            // TEMPLATEIDX: List Subscriber Data Template(LST TP) This command is used to query a Subscriber Data Template to the configuration database. Each Subscriber Data Template is identified by a unique template index. You can use the same template to define subscribers with some common attributes.

            resultCode = (Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode)resultType.ResultCode;

            /*

USE ME:name=tecens; 

ADD DNAPTRREC: NAME="9.9.4.5.1.7.3.2.5.6.9.e164.arpa", ZONENAME="5.6.9.e164.arpa", ORDER=0, PREFERENCE=10, FLAGS="U", SERVICE="E2U+sip", REGEXP="!^.*$!sip:+96523715499@ims.moc.kw!", REPLACEMENT="ims.moc.kw.";
             */



            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool AssingImsSupplementaryService(ims.api.huawei.ATSV100R003C01SPC100Client client, ims.api.huawei.Authentication authentication, string service, Ia.Ngn.Cl.Model.Business.Service.SupplementaryService somethingService, bool supplementaryServiceState, out string result)
        {
            bool b, tempSkip;
            string messageId, meName, impu;
            //ims.api.huawei.LST_SBRType lstSbrType;
            ims.api.huawei.LST_SSType lstSsType;
            //ims.api.huawei.LST_SBRStruct1 lstSbrStruct1;
            ims.api.huawei.LST_SSStruct1 lstSsStruct1;
            ims.api.huawei.ResultType resultType;
            Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode resultCode;

            messageId = "1";
            meName = "tecats1"; //tecats0

            result = "";

            impu = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Impu(service);

            //lstSbrType = client.LST_SBR(authentication, meName, ref messageId, impu, null, null);
            lstSsType = client.LST_SS(authentication, meName, ref messageId, impu, null);

            //resultCode = (Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode)lstSbrType.ResultCode;
            resultCode = (Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode)lstSsType.ResultCode;

            if (resultCode == Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode.OperationSucceeded)
            {
                tempSkip = false;

                //lstSbrStruct1 = lstSbrType.ResultData.Table1[0];
                lstSsStruct1 = lstSsType.ResultData.Table1[0];

                switch (somethingService)
                {
                    //newService.ServiceSuspension =;
                    //newService.AlarmCall = ;
                    //newService.InternationalCallingUserControlled =;
                    //newService.InternationalCalling =;

                    // newService.Pin = (int.TryParse(huSbr.COP, out i)) ? i : 0;

                        /*
                    case (Ia.Ngn.Cl.Model.Business.Service.SupplementaryService.AbbriviatedCalling): lstSbrStruct1.NSABRC = supplementaryServiceState ? 1 : 0; break;
                    case (Ia.Ngn.Cl.Model.Business.Service.SupplementaryService.CallerId): lstSbrStruct1.NSCLIP = supplementaryServiceState ? 1 : 0; break;
                    case (Ia.Ngn.Cl.Model.Business.Service.SupplementaryService.CallForwarding): lstSbrStruct1.NSCFU = supplementaryServiceState ? 1 : 0; break;
                    case (Ia.Ngn.Cl.Model.Business.Service.SupplementaryService.CallWaiting): lstSbrStruct1.NSCW = supplementaryServiceState ? 1 : 0; break;
                    case (Ia.Ngn.Cl.Model.Business.Service.SupplementaryService.ConferenceCall): lstSbrStruct1.NS3PTY = supplementaryServiceState ? 1 : 0; break;
                    case (Ia.Ngn.Cl.Model.Business.Service.SupplementaryService.WakeupCall): lstSbrStruct1.NSWAKE_UP = supplementaryServiceState ? 1 : 0; break;
                         */ 

                    case (Ia.Ngn.Cl.Model.Business.Service.SupplementaryService.AbbriviatedCalling): lstSsStruct1.NSABRC = supplementaryServiceState ? 1 : 0; break;
                    case (Ia.Ngn.Cl.Model.Business.Service.SupplementaryService.CallerId): lstSsStruct1.NSCLIP = supplementaryServiceState ? 1 : 0; break;
                    case (Ia.Ngn.Cl.Model.Business.Service.SupplementaryService.CallForwarding): lstSsStruct1.NSCFU = supplementaryServiceState ? 1 : 0; break;
                    case (Ia.Ngn.Cl.Model.Business.Service.SupplementaryService.CallWaiting): lstSsStruct1.NSCW = supplementaryServiceState ? 1 : 0; break;
                    case (Ia.Ngn.Cl.Model.Business.Service.SupplementaryService.ConferenceCall): lstSsStruct1.NS3PTY = supplementaryServiceState ? 1 : 0; break;
                    case (Ia.Ngn.Cl.Model.Business.Service.SupplementaryService.WakeupCall): lstSsStruct1.NSWAKE_UP = supplementaryServiceState ? 1 : 0; break;

                    default: tempSkip = true; break;
                }

                if (!tempSkip)
                {
                    /*
                    // update IMS with changed values from Sbr:
                    resultType = client.MOD_SBR(authentication, meName, ref messageId,
                        lstSbrStruct1.IMPU,
                        lstSbrStruct1.TEMPLATEIDX,
                        lstSbrStruct1.DSPIDX,
                        lstSbrStruct1.LP,
                        lstSbrStruct1.CSC,
                        lstSbrStruct1.UNAME,
                        lstSbrStruct1.UTYPE,
                        lstSbrStruct1.VCCFLAG,
                        lstSbrStruct1.VTFLAG,
                        lstSbrStruct1.NSCFU,
                        lstSbrStruct1.NSCFUVM,
                        lstSbrStruct1.NSCFB,
                        lstSbrStruct1.NSCFBVM,
                        lstSbrStruct1.NSCFNR,
                        lstSbrStruct1.NSCFNRVM,
                        lstSbrStruct1.NSCFNL,
                        lstSbrStruct1.NSCFNLVM,
                        lstSbrStruct1.NSCD,
                        lstSbrStruct1.NSCDVM,
                        lstSbrStruct1.NSCFNRC,
                        lstSbrStruct1.NSCFNRCVM,
                        lstSbrStruct1.NSCLIP,
                        lstSbrStruct1.NSCIDCW,
                        lstSbrStruct1.NSRIO,
                        lstSbrStruct1.NSCNIP,
                        lstSbrStruct1.NSCLIR,
                        lstSbrStruct1.NSRIP,
                        lstSbrStruct1.NSCNIR,
                        lstSbrStruct1.NSRID,
                        lstSbrStruct1.NSNRID,
                        lstSbrStruct1.NSRND,
                        lstSbrStruct1.NSNRND,
                        lstSbrStruct1.NSCW,
                        lstSbrStruct1.NSCCW,
                        lstSbrStruct1.NSOIP,
                        lstSbrStruct1.NSACRM,
                        lstSbrStruct1.NSGOIR,
                        lstSbrStruct1.NSMOIR,
                        lstSbrStruct1.NSTIP,
                        lstSbrStruct1.NSTIR,
                        lstSbrStruct1.NSOTIR,
                        lstSbrStruct1.NSCLIPNOSCREENING,
                        lstSbrStruct1.NSCR,
                        lstSbrStruct1.NSWAKE_UP,
                        lstSbrStruct1.NSAOC_D,
                        lstSbrStruct1.NSAOC_E,
                        lstSbrStruct1.NSXEXH,
                        lstSbrStruct1.NSXEGJ,
                        lstSbrStruct1.NSCWCFNR,
                        lstSbrStruct1.NSIIFC,
                        lstSbrStruct1.NSDN_CALL_OUT_BAR,
                        lstSbrStruct1.NSCCBS,
                        lstSbrStruct1.NSCCNR,
                        lstSbrStruct1.NSCCBSR,
                        lstSbrStruct1.NSCCNRR,
                        lstSbrStruct1.NS3PTY,
                        lstSbrStruct1.NSNPTY,
                        lstSbrStruct1.NSDND,
                        lstSbrStruct1.NSMCR,
                        lstSbrStruct1.NSCBA,
                        lstSbrStruct1.NSTMP_LIN,
                        lstSbrStruct1.NSCODEC_CNTRL,
                        lstSbrStruct1.NSMWI,
                        lstSbrStruct1.NSDC,
                        lstSbrStruct1.NSHOLD,
                        lstSbrStruct1.NSECT,
                        lstSbrStruct1.NSCFTB,
                        lstSbrStruct1.NSDAN,
                        lstSbrStruct1.NSSTOP_SECRET,
                        lstSbrStruct1.NSMCID,
                        lstSbrStruct1.NSEBO,
                        lstSbrStruct1.NSICO,
                        lstSbrStruct1.NSOUTG,
                        lstSbrStruct1.NSINQYH,
                        lstSbrStruct1.NSUINFO,
                        lstSbrStruct1.NSDN_CALL_OUT_ALLOW,
                        lstSbrStruct1.NSSIC,
                        lstSbrStruct1.NSSOC,
                        lstSbrStruct1.NSSETCFNRTIME,
                        lstSbrStruct1.NSCFS,
                        lstSbrStruct1.NSCFSB,
                        lstSbrStruct1.NSFAX,
                        lstSbrStruct1.NSABRC,
                        lstSbrStruct1.NSACRTOVM,
                        lstSbrStruct1.NSPREPAID,
                        lstSbrStruct1.NSCRBT,
                        lstSbrStruct1.NSICB,
                        lstSbrStruct1.NSMRINGING,
                        lstSbrStruct1.NSCIS,
                        lstSbrStruct1.NSCBEG,
                        lstSbrStruct1.NSCOLP,
                        lstSbrStruct1.NSCOLR,
                        lstSbrStruct1.NSCOLPOVR,
                        lstSbrStruct1.NSBAOC,
                        lstSbrStruct1.NSBOIC,
                        lstSbrStruct1.NSBOICEXHC,
                        lstSbrStruct1.NSBAIC,
                        lstSbrStruct1.NSBICROM,
                        lstSbrStruct1.NSSPEED_DIAL,
                        lstSbrStruct1.NSSD1D,
                        lstSbrStruct1.NSSD2D,
                        lstSbrStruct1.NSGRNCALL,
                        lstSbrStruct1.NSCPARK,
                        lstSbrStruct1.NSGAA,
                        lstSbrStruct1.NSQSNS,
                        lstSbrStruct1.NSMSN,
                        lstSbrStruct1.NSHOTLINE,
                        lstSbrStruct1.NSAOC_S,
                        lstSbrStruct1.NSNIGHTSRV,
                        lstSbrStruct1.NSBACKNUM,
                        lstSbrStruct1.NSAUTOCON,
                        lstSbrStruct1.NSCAMPON,
                        lstSbrStruct1.NSCTD,
                        lstSbrStruct1.NSCLICKHOLD,
                        lstSbrStruct1.NSQUEUE,
                        lstSbrStruct1.NSSANSWER,
                        lstSbrStruct1.NSICENCF,
                        lstSbrStruct1.NSCFGO,
                        lstSbrStruct1.NSCECT,
                        lstSbrStruct1.NSCTGO,
                        lstSbrStruct1.NSCTIO,
                        lstSbrStruct1.NSSETBUSY,
                        lstSbrStruct1.NSOVERSTEP,
                        lstSbrStruct1.NSABSENT,
                        lstSbrStruct1.NSMONITOR,
                        lstSbrStruct1.NSFMONITOR,
                        lstSbrStruct1.NSDISCNT,
                        lstSbrStruct1.NSFDISCNT,
                        lstSbrStruct1.NSINSERT,
                        lstSbrStruct1.NSFINSERT,
                        lstSbrStruct1.NSASI,
                        lstSbrStruct1.NSPWCB,
                        lstSbrStruct1.NSRD,
                        lstSbrStruct1.NSLCPS,
                        lstSbrStruct1.NSNCPS,
                        lstSbrStruct1.NSICPS,
                        lstSbrStruct1.NSCBCLOCK,
                        lstSbrStruct1.NSMINIBAR,
                        lstSbrStruct1.NSMCN,
                        lstSbrStruct1.NSDSTR,
                        lstSbrStruct1.NSOPRREG,
                        lstSbrStruct1.NSONEKEY,
                        lstSbrStruct1.NSINBOUND,
                        lstSbrStruct1.NSOUTBOUND,
                        lstSbrStruct1.NSCALLERID,
                        lstSbrStruct1.NSCUN,
                        lstSbrStruct1.NSIPTVVC,
                        lstSbrStruct1.NSNP,
                        lstSbrStruct1.NSSEC,
                        lstSbrStruct1.NSSECSTA,
                        lstSbrStruct1.NSHRCN,
                        lstSbrStruct1.NSSB,
                        lstSbrStruct1.NSOCCR,
                        lstSbrStruct1.LCO,
                        lstSbrStruct1.LC,
                        lstSbrStruct1.LCT,
                        lstSbrStruct1.NTT,
                        lstSbrStruct1.ITT,
                        lstSbrStruct1.ICTX,
                        lstSbrStruct1.OCTX,
                        lstSbrStruct1.INTT,
                        lstSbrStruct1.IITT,
                        lstSbrStruct1.ICLT,
                        lstSbrStruct1.ICDDD,
                        lstSbrStruct1.ICIDD,
                        lstSbrStruct1.IOLT,
                        lstSbrStruct1.CTLCO,
                        lstSbrStruct1.CTLCT,
                        lstSbrStruct1.CTLD,
                        lstSbrStruct1.CTINTNANP,
                        lstSbrStruct1.CTINTWORLD,
                        lstSbrStruct1.CTDA,
                        lstSbrStruct1.CTOSM,
                        lstSbrStruct1.CTOSP,
                        lstSbrStruct1.CTOSP1,
                        lstSbrStruct1.CCO1,
                        lstSbrStruct1.CCO2,
                        lstSbrStruct1.CCO3,
                        lstSbrStruct1.CCO4,
                        lstSbrStruct1.CCO5,
                        lstSbrStruct1.CCO6,
                        lstSbrStruct1.CCO7,
                        lstSbrStruct1.CCO8,
                        lstSbrStruct1.CCO9,
                        lstSbrStruct1.CCO10,
                        lstSbrStruct1.CCO11,
                        lstSbrStruct1.CCO12,
                        lstSbrStruct1.CCO13,
                        lstSbrStruct1.CCO14,
                        lstSbrStruct1.CCO15,
                        lstSbrStruct1.CCO16,
                        lstSbrStruct1.HIGHENTCO,
                        lstSbrStruct1.OPERATOR,
                        lstSbrStruct1.SUPYSRV,
                        lstSbrStruct1.IDDCI,
                        lstSbrStruct1.NTCI,
                        lstSbrStruct1.LTCI,
                        lstSbrStruct1.RSC,
                        lstSbrStruct1.CIG,
                        lstSbrStruct1.OUTRST,
                        lstSbrStruct1.INRST,
                        lstSbrStruct1.NOAT,
                        lstSbrStruct1.RINGCOUNT,
                        lstSbrStruct1.VMAIND,
                        lstSbrStruct1.VDMAIND,
                        lstSbrStruct1.TGRP,
                        lstSbrStruct1.TIDHLD,
                        lstSbrStruct1.TIDCW,
                        lstSbrStruct1.SCF,
                        lstSbrStruct1.LMTGRP,
                        lstSbrStruct1.FLBGRP,
                        lstSbrStruct1.SLBGRP,
                        lstSbrStruct1.COP,
                        lstSbrStruct1.G711_64K_A_LAW,
                        lstSbrStruct1.G711_64K_U_LAW,
                        lstSbrStruct1.G722,
                        lstSbrStruct1.G723,
                        lstSbrStruct1.G726,
                        lstSbrStruct1.G728,
                        lstSbrStruct1.G729,
                        lstSbrStruct1.CODEC_MP4A,
                        lstSbrStruct1.CODEC2833,
                        lstSbrStruct1.CODEC2198,
                        lstSbrStruct1.G726_40,
                        lstSbrStruct1.G726_32,
                        lstSbrStruct1.G726_24,
                        lstSbrStruct1.G726_16,
                        lstSbrStruct1.AMR,
                        lstSbrStruct1.CLEARMODE,
                        lstSbrStruct1.ILBC,
                        lstSbrStruct1.SPEEX,
                        lstSbrStruct1.G729EV,
                        lstSbrStruct1.EVRC,
                        lstSbrStruct1.EVRCB,
                        lstSbrStruct1.H261,
                        lstSbrStruct1.H263,
                        lstSbrStruct1.CODEC_MP4V,
                        lstSbrStruct1.H264,
                        lstSbrStruct1.T38,
                        lstSbrStruct1.T120,
                        lstSbrStruct1.G711A_VBD,
                        lstSbrStruct1.G711U_VBD,
                        lstSbrStruct1.G726_VBD,
                        lstSbrStruct1.G726_40_VBD,
                        lstSbrStruct1.G726_32_VBD,
                        lstSbrStruct1.G726_24_VBD,
                        lstSbrStruct1.G726_16_VBD,
                        lstSbrStruct1.WIND_BAND_AMR,
                        lstSbrStruct1.GSM610,
                        lstSbrStruct1.H263_2000,
                        lstSbrStruct1.BROADVOICE_32,
                        lstSbrStruct1.UNKNOWN_CODEC,
                        lstSbrStruct1.ACODEC,
                        lstSbrStruct1.VCODEC,
                        lstSbrStruct1.POLIDX,
                        lstSbrStruct1.NCPI,
                        lstSbrStruct1.ICPI,
                        lstSbrStruct1.EBOCL,
                        lstSbrStruct1.EBOPL,
                        lstSbrStruct1.EBOIT,
                        lstSbrStruct1.RM,
                        lstSbrStruct1.CPC,
                        lstSbrStruct1.PCHG,
                        lstSbrStruct1.TFPT,
                        lstSbrStruct1.CHT,
                        lstSbrStruct1.MCIDMODE,
                        lstSbrStruct1.MCIDCMODE,
                        lstSbrStruct1.MCIDAMODE,
                        lstSbrStruct1.PREPAIDIDX,
                        lstSbrStruct1.CRBTID,
                        lstSbrStruct1.ODBBICTYPE,
                        lstSbrStruct1.ODBBOCTYPE,
                        lstSbrStruct1.ODBBARTYPE,
                        lstSbrStruct1.ODBSS,
                        lstSbrStruct1.ODBBRCFTYPE,
                        lstSbrStruct1.PNOTI,
                        lstSbrStruct1.MAXPARACALL,
                        lstSbrStruct1.ATSDTMBUSY,
                        lstSbrStruct1.CALLCOUNT,
                        lstSbrStruct1.CDNOTICALLER,
                        lstSbrStruct1.ISCHGFLAG,
                        lstSbrStruct1.CHC,
                        lstSbrStruct1.CUSER,
                        lstSbrStruct1.CGRP,
                        lstSbrStruct1.CUSERGRP,
                        lstSbrStruct1.STCF,
                        lstSbrStruct1.CHARSC,
                        lstSbrStruct1.REGUIDX,
                        lstSbrStruct1.SOCBFUNC,
                        lstSbrStruct1.SOCBPTONEIDX,
                        lstSbrStruct1.ADMINCBA,
                        lstSbrStruct1.ADCONTROL_DIVERSION,
                        lstSbrStruct1.DPR,
                        lstSbrStruct1.PRON,
                        lstSbrStruct1.CPCRUS,
                        lstSbrStruct1.CUSCAT,
                        lstSbrStruct1.SPT100REL);
                     */ 

                    // update IMS with changed values from Ss:
                    resultType = client.MOD_SS(authentication, meName, ref messageId, impu,
                        lstSsStruct1.NSCFU,
                        lstSsStruct1.NSCFUVM,
                        lstSsStruct1.NSCFB,
                        lstSsStruct1.NSCFBVM,
                        lstSsStruct1.NSCFNR,
                        lstSsStruct1.NSCFNRVM,
                        lstSsStruct1.NSCFNL,
                        lstSsStruct1.NSCFNLVM,
                        lstSsStruct1.NSCD,
                        lstSsStruct1.NSCDVM,
                        lstSsStruct1.NSCFNRC,
                        lstSsStruct1.NSCFNRCVM,
                        lstSsStruct1.NSCIDCW,
                        lstSsStruct1.NSRIP,
                        lstSsStruct1.NSRID,
                        lstSsStruct1.NSNRID,
                        lstSsStruct1.NSRND,
                        lstSsStruct1.NSNRND,
                        lstSsStruct1.NSOIP,
                        lstSsStruct1.NSGOIR,
                        lstSsStruct1.NSMOIR,
                        lstSsStruct1.NSTIP,
                        lstSsStruct1.NSTIR,
                        lstSsStruct1.NSOTIR,
                        lstSsStruct1.NSCLIP,
                        lstSsStruct1.NSCLIPNOSCREENING,
                        lstSsStruct1.NSRIO,
                        lstSsStruct1.NSCNIP,
                        lstSsStruct1.NSCLIR,
                        lstSsStruct1.NSCNIR,
                        lstSsStruct1.NSCW,
                        lstSsStruct1.NSCCW,
                        lstSsStruct1.NSACRM,
                        lstSsStruct1.NSCR,
                        lstSsStruct1.NSWAKE_UP,
                        lstSsStruct1.NSAOC_D,
                        lstSsStruct1.NSAOC_E,
                        lstSsStruct1.NSXEXH,
                        lstSsStruct1.NSXEGJ,
                        lstSsStruct1.NSCWCFNR,
                        lstSsStruct1.NSIIFC,
                        lstSsStruct1.NSDN_CALL_OUT_BAR,
                        lstSsStruct1.NSCCBS,
                        lstSsStruct1.NSCCNR,
                        lstSsStruct1.NSCCBSR,
                        lstSsStruct1.NSCCNRR,
                        lstSsStruct1.NS3PTY,
                        lstSsStruct1.NSNPTY,
                        lstSsStruct1.NSDND,
                        lstSsStruct1.NSMCR,
                        lstSsStruct1.NSCBA,
                        lstSsStruct1.NSTMP_LIN,
                        lstSsStruct1.NSCODEC_CNTRL,
                        lstSsStruct1.NSMWI,
                        lstSsStruct1.NSDC,
                        lstSsStruct1.NSHOLD,
                        lstSsStruct1.NSECT,
                        lstSsStruct1.NSCFTB,
                        lstSsStruct1.NSDAN,
                        lstSsStruct1.NSSTOP_SECRET,
                        lstSsStruct1.NSMCID,
                        lstSsStruct1.NSEBO,
                        lstSsStruct1.NSICO,
                        lstSsStruct1.NSOUTG,
                        lstSsStruct1.NSINQYH,
                        lstSsStruct1.NSUINFO,
                        lstSsStruct1.NSDN_CALL_OUT_ALLOW,
                        lstSsStruct1.NSSIC,
                        lstSsStruct1.NSSOC,
                        lstSsStruct1.NSSETCFNRTIME,
                        lstSsStruct1.NSCFS,
                        lstSsStruct1.NSCFSB,
                        lstSsStruct1.NSFAX,
                        lstSsStruct1.NSABRC,
                        lstSsStruct1.NSACRTOVM,
                        lstSsStruct1.NSPREPAID,
                        lstSsStruct1.NSCRBT,
                        lstSsStruct1.NSICB,
                        lstSsStruct1.NSMRINGING,
                        lstSsStruct1.NSCIS,
                        lstSsStruct1.NSCBEG,
                        lstSsStruct1.NSCOLP,
                        lstSsStruct1.NSCOLR,
                        lstSsStruct1.NSCOLPOVR,
                        lstSsStruct1.NSBAOC,
                        lstSsStruct1.NSBOIC,
                        lstSsStruct1.NSBOICEXHC,
                        lstSsStruct1.NSBAIC,
                        lstSsStruct1.NSBICROM,
                        lstSsStruct1.NSSPEED_DIAL,
                        lstSsStruct1.NSSD1D,
                        lstSsStruct1.NSSD2D,
                        lstSsStruct1.NSGRNCALL,
                        lstSsStruct1.NSCPARK,
                        lstSsStruct1.NSGAA,
                        lstSsStruct1.NSQSNS,
                        lstSsStruct1.NSMSN,
                        lstSsStruct1.NSHOTLINE,
                        lstSsStruct1.NSAOC_S,
                        lstSsStruct1.NSNIGHTSRV,
                        lstSsStruct1.NSBACKNUM,
                        lstSsStruct1.NSAUTOCON,
                        lstSsStruct1.NSCAMPON,
                        lstSsStruct1.NSCTD,
                        lstSsStruct1.NSCLICKHOLD,
                        lstSsStruct1.NSQUEUE,
                        lstSsStruct1.NSSANSWER,
                        lstSsStruct1.NSICENCF,
                        lstSsStruct1.NSCFGO,
                        lstSsStruct1.NSCECT,
                        lstSsStruct1.NSCTGO,
                        lstSsStruct1.NSCTIO,
                        lstSsStruct1.NSSETBUSY,
                        lstSsStruct1.NSOVERSTEP,
                        lstSsStruct1.NSABSENT,
                        lstSsStruct1.NSMONITOR,
                        lstSsStruct1.NSFMONITOR,
                        lstSsStruct1.NSDISCNT,
                        lstSsStruct1.NSFDISCNT,
                        lstSsStruct1.NSINSERT,
                        lstSsStruct1.NSFINSERT,
                        lstSsStruct1.NSASI,
                        lstSsStruct1.NSPWCB,
                        lstSsStruct1.NSRD,
                        lstSsStruct1.NSLCPS,
                        lstSsStruct1.NSNCPS,
                        lstSsStruct1.NSICPS,
                        lstSsStruct1.NSCBCLOCK,
                        lstSsStruct1.NSMINIBAR,
                        lstSsStruct1.NSMCN,
                        lstSsStruct1.NSDSTR,
                        lstSsStruct1.NSOPRREG,
                        lstSsStruct1.NSONEKEY,
                        lstSsStruct1.NSINBOUND,
                        lstSsStruct1.NSOUTBOUND,
                        lstSsStruct1.NSCALLERID,
                        lstSsStruct1.NSCUN,
                        lstSsStruct1.NSIPTVVC,
                        lstSsStruct1.NSNP,
                        lstSsStruct1.NSSEC,
                        lstSsStruct1.NSSECSTA,
                        lstSsStruct1.NSHRCN,
                        lstSsStruct1.NSSB,
                        lstSsStruct1.NSOCCR);

                    resultCode = (Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode)lstSsType.ResultCode;

                    b = (resultCode == Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode.OperationSucceeded);

                    lstSsType = client.LST_SS(authentication, meName, ref messageId, impu, null);

                    /*
                    resultCode = (Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode)lstSbrType.ResultCode;

                    b = (resultCode == Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode.OperationSucceeded);

                    lstSbrType = client.LST_SBR(authentication, meName, ref messageId, impu, null, null);
                     */ 

                    /*
                    if(b)
                    {
                        lstSbrType = client.LST_SBR(authentication, meName, ref messageId, impu, "", ""); // "tel:+96524839366"

                        b = (resultCode == Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode.OperationSucceeded);
                    }
                     */
                }
                else b = false;
            }
            else if (resultCode == Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode.TheSubscriberIsNotDefinedInTheHssOrAtsOrServiceDataIsNotConfiguredForTheSubscriber)
            {
                b = false;
            }
            else b = false;

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}