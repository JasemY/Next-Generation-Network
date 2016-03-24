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

namespace Ia.Ngn.Cl.Model.Data.Nokia
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Optical Fiber Network Management Intranet Portal (OFN) support class for Nokia's Next Generation Network (NGN) data model.
    /// </summary>
    /// 
    /// <value>
    ///   <appSettings>
    ///     <add key="imsServerHost" value="https://*" />
    ///     <add key="imsServerPort" value="*" />
    ///     <add key="imsServerServiceUrl" value="*" />
    ///     <add key="imsServerUser" value="*" />
    ///     <add key="imsServerUserPassword" value="*" />
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
    public partial class Ims
    {
        /// <summary/>
        public static string BaseAddress { get { return ConfigurationManager.AppSettings["imsServerHost"].ToString() + ":" + ConfigurationManager.AppSettings["imsServerPort"].ToString(); } }
        /// <summary/>
        public static string ServiceUrl { get { return ConfigurationManager.AppSettings["imsServerServiceUrl"].ToString(); } }
        /// <summary/>
        public static string UserName { get { return ConfigurationManager.AppSettings["imsServerUser"].ToString(); } }
        /// <summary/>
        public static string Password { get { return ConfigurationManager.AppSettings["imsServerUserPassword"].ToString(); } }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Ims() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void CreateSubscriberAndAgcfEndpoint(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, int gwId, string service, int flatTermId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out string result)
        {
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode entNgfsSubscriberResultCode, entNgfsAgcfEndpointResultCode;

            alIms.EntNgfsSubscriberV2_EntNgfsAgcfEndpointV2(service, gwId, flatTermId, nddOnt, out entNgfsSubscriberResultCode, out entNgfsAgcfEndpointResultCode);

            result = service + "," + gwId + "," + flatTermId + "," + nddOnt.Access.Name + "," + entNgfsSubscriberResultCode.ToString() + "," + entNgfsAgcfEndpointResultCode.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void DeleteAgcfEndpointAndSubscriber(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out string result)
        {
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode entNgfsSubscriberResultCode, entNgfsAgcfEndpointResultCode;

            alIms.DltNgfsAgcfEndpointV2_DltNgfsSubscriberV2(service, nddOnt, out entNgfsSubscriberResultCode, out entNgfsAgcfEndpointResultCode);

            result = service + "," + nddOnt.Access.Name + "," + entNgfsSubscriberResultCode.ToString() + "," + entNgfsAgcfEndpointResultCode.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////






        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void AssignCallingLineIdStateToServiceSubscriber(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool callingLineIdState, out string result)
        {
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode;

            alIms.EdNgfsSubscriberV2_CallingLineId(service, nddOnt, callingLineIdState, out resultCode);

            result = service + "," + nddOnt.Access.Name + ",calling line id: " + callingLineIdState.ToString().ToLower() + "," + resultCode.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void AssignAbbriviatedCallingStateToServiceSubscriber(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool abbriviatedCallingState, out string result)
        {
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode;

            alIms.EdNgfsSubscriberV2_OneDigitSpeedDial(service, nddOnt, abbriviatedCallingState, out resultCode);

            result = service + "," + nddOnt.Access.Name + ",abbriviated calling: " + abbriviatedCallingState.ToString().ToLower() + "," + resultCode.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void AssignCallForwardingStateToServiceSubscriber(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool callForwardingState, out string result)
        {
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode;

            alIms.EdNgfsSubscriberV2_CallForwardingVari(service, nddOnt, callForwardingState, out resultCode);

            result = service + "," + nddOnt.Access.Name + ",call forwarding vari: " + callForwardingState.ToString().ToLower() + "," + resultCode.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void AssignCallWaitingStateToServiceSubscriberAndAgcfEndpoint(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool callWaitingState, out string result)
        {
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode subscriberResultCode, agcfEndpointResultCode;

            alIms.EdNgfsSubscriberV2_CallWaiting(service, nddOnt, callWaitingState, out subscriberResultCode);

            alIms.EdNgfsAgcfEndpointV2_CallWaiting(service, nddOnt, callWaitingState, out agcfEndpointResultCode);

            result = service + "," + nddOnt.Access.Name + ",call waiting: " + callWaitingState.ToString().ToLower() + ", subscriber: " + subscriberResultCode.ToString() + ", agcfEndpoint: " + agcfEndpointResultCode.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void AssignConferenceCallStateToServiceSubscriber(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool conferenceCallState, out string result)
        {
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode;

            alIms.EdNgfsSubscriberV2_ConferenceCall(service, nddOnt, conferenceCallState, out resultCode);

            result = service + "," + nddOnt.Access.Name + ",conference call: " + conferenceCallState.ToString().ToLower() + "," + resultCode.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void AssignReminderCallStateToServiceSubscriber(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool reminderCallState, out string result)
        {
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode;

            alIms.EdNgfsSubscriberV2_ReminderCall(service, nddOnt, reminderCallState, out resultCode);

            result = service + "," + nddOnt.Access.Name + ",reminder call: " + reminderCallState.ToString().ToLower() + "," + resultCode.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void AssignInternationalCallingStateToServiceSubscriber(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool internationalCallingState, out string result)
        {
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode;

            alIms.EdNgfsSubscriberV2_InternationalCalling(service, nddOnt, internationalCallingState, out resultCode);

            result = service + "," + nddOnt.Access.Name + ",international calling: " + internationalCallingState.ToString().ToLower() + "," + resultCode.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void AssignInternationalCallingUserControlledStateToServiceSubscriber(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool internationalCallingUserControlledState, out string result)
        {
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode;

            alIms.EdNgfsSubscriberV2_InternationalCallingUserControlled(service, nddOnt, internationalCallingUserControlledState, out resultCode);

            result = service + "," + nddOnt.Access.Name + ",international calling user controlled: " + internationalCallingUserControlledState.ToString().ToLower() + "," + resultCode.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void AssignServiceSuspensionStateToServiceSubscriber(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool serviceSuspensionState, out string result)
        {
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode;

            alIms.EdNgfsSubscriberV2_ServiceSuspension(service, nddOnt, serviceSuspensionState, out resultCode);

            result = service + "," + nddOnt.Access.Name + ",service suspension: " + serviceSuspensionState.ToString().ToLower() + "," + resultCode.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord ReadAgcfGatewayRecordForGwId(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, int gwId, out string result)
        {
            bool b;
            Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord agcfGatewayRecord;
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode;

            agcfGatewayRecord = alIms.RtrvNgfsAgcfGatewayRecordV2(gwId, out resultCode);

            if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful || resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.PLX_SERVICE_OBJECT_DOES_NOT_EXIST)
            {
                b = Ia.Ngn.Cl.Model.Data.Nokia.Ims.UpdateAgcfGatewayRecord(gwId, resultCode, agcfGatewayRecord);
            }

            result = "GatewayRecord: " + gwId + ": " + resultCode.ToString();

            return agcfGatewayRecord;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void ReadAgcfEndpointListForGwId(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, int gwId, out string result)
        {
            bool b;
            int agcfGatewayRecordId;
            Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt;
            Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord agcfGatewayRecord;
            List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList;
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode;

            // below: read the AgcfGatewayRecord
            agcfGatewayRecordId = Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord.AgcfGatewayRecordId(Ia.Ngn.Cl.Model.Business.Nokia.AgcfGatewayTable.NgfsAgcfGatewayTableAid, gwId);
            agcfGatewayRecord = Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord.Read(agcfGatewayRecordId);

            if (agcfGatewayRecord != null)
            {
                nddOnt = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where q.Ip == agcfGatewayRecord.IP1 select q).SingleOrDefault();

                if (nddOnt != null)
                {
                    agcfEndpointList = alIms.RtrvNgfsAgcfEndpointV2List(agcfGatewayRecord.GwId, nddOnt, out resultCode);

                    if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful || resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.PLX_SERVICE_OBJECT_DOES_NOT_EXIST || resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.SuccessfulButNoParameters)
                    {
                        b = Ia.Ngn.Cl.Model.Data.Nokia.Ims.UpdateNgfsAgcfEndpointList(gwId, agcfGatewayRecordId, resultCode, agcfEndpointList);
                    }
                    else if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.SESSION_INVALID) { }
                    else throw new Exception(@"Undefined result code """ + resultCode + @""" seen in Ia.Ngn.Cl.Model.Data.Nokia.Ims.ResultCode");

                    result = "Endpoint: GwId:" + gwId + ", " + resultCode.ToString();
                }
                else
                {
                    result = "Endpoint: " + "Error: nddOnt == null, GwId:" + gwId;
                }
            }
            else
            {
                result = "Endpoint: " + "GwId:" + gwId + ", Exception: " + @"agcfGatewayRecord is null, agcfGatewayRecordsId=" + agcfGatewayRecordId;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void ReadAgcfGatewayRecordAndAgcfEndpointListAndSubPartyAndSubscriberForGwId(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, int gwId, out string result)
        {
            bool agcfGatewayRecordUpdated, agcfEndpointListUpdated, ngfsSubPartyUpdated, ngfsSubscriberUpdated;
            int agcfGatewayRecordId, imsService;
            string service, edgeRouter, partyId, prividUser, subPartyId;
            Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt;
            Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord agcfGatewayRecord;
            Ia.Ngn.Cl.Model.Nokia.SubParty subParty;
            Ia.Ngn.Cl.Model.Nokia.Subscriber subscriber;
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode;
            List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList;

            // Read GwId->Ep(s)->Sp->Sub, and ask about non FSDB sub search on XML

            result = "gwId: " + gwId;

            agcfGatewayRecordId = Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord.AgcfGatewayRecordId(Ia.Ngn.Cl.Model.Business.Nokia.AgcfGatewayTable.NgfsAgcfGatewayTableAid, gwId);
            agcfGatewayRecord = alIms.RtrvNgfsAgcfGatewayRecordV2(gwId, out resultCode);

            if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful || resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.PLX_SERVICE_OBJECT_DOES_NOT_EXIST)
            {
                agcfGatewayRecordUpdated = Ia.Ngn.Cl.Model.Data.Nokia.Ims.UpdateAgcfGatewayRecord(gwId, resultCode, agcfGatewayRecord);

                if (agcfGatewayRecord != null)
                {
                    nddOnt = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where q.Ip == agcfGatewayRecord.IP1 select q).SingleOrDefault();
                    imsService = Ia.Ngn.Cl.Model.Business.Nokia.Ims.ImsServiceFromAgcfSipIaPort(agcfGatewayRecord.AgcfSipIaPort);
                    edgeRouter = Ia.Ngn.Cl.Model.Business.Nokia.Ims.EdgeRouterFromAgcfGatewayRecord(agcfGatewayRecord.IsPrimary);

                    if (nddOnt != null) result += " " + nddOnt.Access.Name;
                    else result += " warning: nddOnt is null for IP: " + agcfGatewayRecord.IP1;

                    agcfEndpointList = alIms.RtrvNgfsAgcfEndpointV2List(agcfGatewayRecord.GwId, edgeRouter, imsService, out resultCode);

                    if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful || resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.PLX_SERVICE_OBJECT_DOES_NOT_EXIST || resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.SuccessfulButNoParameters)
                    {
                        agcfEndpointListUpdated = Ia.Ngn.Cl.Model.Data.Nokia.Ims.UpdateNgfsAgcfEndpointList(gwId, agcfGatewayRecordId, resultCode, agcfEndpointList);

                        agcfEndpointList = Ia.Ngn.Cl.Model.Data.Nokia.AgcfEndpoint.List(gwId);

                        if (agcfEndpointList != null)
                        {
                            foreach (Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint agcfEndpoint in agcfEndpointList)
                            {
                                prividUser = agcfEndpoint.PrividUser;
                                partyId = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(prividUser);

                                subParty = alIms.RtrvNgfsSubPartyV2(partyId, edgeRouter, imsService, out resultCode);

                                if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful || resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.PLX_SERVICE_OBJECT_DOES_NOT_EXIST)
                                {
                                    ngfsSubPartyUpdated = Ia.Ngn.Cl.Model.Data.Nokia.Ims.UpdateNgfsSubParty(partyId, agcfEndpoint.Id, resultCode, subParty);

                                    // below: read the SubParty
                                    subPartyId = Ia.Ngn.Cl.Model.Nokia.SubParty.SubPartyId(partyId);
                                    subParty = Ia.Ngn.Cl.Model.Nokia.SubParty.Read(subPartyId);

                                    if (subParty != null)
                                    {
                                        subscriber = alIms.RtrvNgfsSubscriberV2(partyId, edgeRouter, imsService, out resultCode);

                                        if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful || resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.PLX_SERVICE_OBJECT_DOES_NOT_EXIST)
                                        {
                                            service = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Service(partyId);

                                            ngfsSubscriberUpdated = Ia.Ngn.Cl.Model.Data.Nokia.Ims.UpdateNgfsSubscriber(partyId, subPartyId, resultCode, subscriber);

                                            Ia.Ngn.Cl.Model.Data.Nokia.Ims.UpdateServiceFromAgcfGatewayRecordAndAgcfEndpointAndSubPartyAndSubscriber(service, agcfGatewayRecord, agcfEndpoint, subParty, subscriber);

                                            result += "," + service;
                                        }
                                        else
                                        {
                                            result += " error: subscriber: " + resultCode.ToString();
                                        }
                                    }
                                    else
                                    {
                                        result += " subParty is null";
                                    }
                                }
                                else
                                {
                                    result += " error: subParty: " + resultCode.ToString();
                                }
                            }
                        }
                        else
                        {
                            result += " agcfEndpointList is null";
                        }
                    }
                    else
                    {
                        result += " error: agcfEndpointList: " + resultCode.ToString();
                    }
                }
                else
                {
                    result += " agcfGatewayRecord is null";
                }
            }
            else
            {
                result += " error: agcfGatewayRecord: " + resultCode.ToString();
            }
        }

        /*
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void ReadAgcfEndpointAndSubPartyAndSubscriberForService(Ia.Ngn.Cl.Model.Client.Nokia.Ims alIms, int gwId, string service, out string result)
        {
            bool ngfsSubPartyUpdated, ngfsSubscriberUpdated;
            int imsService;
            string edgeRouter, partyId, prividUser, subPartyId;
            Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt;
            Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord agcfGatewayRecord;
            Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint agcfEndpoint;
            Ia.Ngn.Cl.Model.Nokia.SubParty subParty;
            Ia.Ngn.Cl.Model.Nokia.Subscriber subscriber;
            Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode;

            result = "service: " + service + ", gwId: " + gwId;

            prividUser = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PrividUser(service);
            partyId = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(service);

            Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord agcfGatewayRecord;

            agcfEndpoint = alIms.RtrvNgfsAgcfEndpointV2(prividUser, edgeRouter, imsService, out resultCode);

            if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful || resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.PLX_SERVICE_OBJECT_DOES_NOT_EXIST || resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.SuccessfulButNoParameters)
            {
                agcfEndpoint = Ia.Ngn.Cl.Model.Data.Nokia.AgcfEndpoint.Read(prividUser);

                if (agcfEndpoint != null)
                {
                    nddOnt = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where q.Ip == agcfEndpoint.AgcfGatewayRecord.IP1 select q).SingleOrDefault();
                    imsService = Ia.Ngn.Cl.Model.Business.Nokia.Ims.ImsServiceFromAgcfSipIaPort(agcfEndpoint.AgcfGatewayRecord.AgcfSipIaPort);
                    edgeRouter = Ia.Ngn.Cl.Model.Business.Nokia.Ims.EdgeRouterFromAgcfGatewayRecord(agcfEndpoint.AgcfGatewayRecord.IsPrimary);

                    if (nddOnt != null) result += " " + nddOnt.Access.Name;
                    else result += " warning: nddOnt is null for IP: " + agcfEndpoint.AgcfGatewayRecord.IP1;

                    subParty = alIms.RtrvNgfsSubPartyV2(partyId, edgeRouter, imsService, out resultCode);

                    if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful || resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.PLX_SERVICE_OBJECT_DOES_NOT_EXIST)
                    {
                        ngfsSubPartyUpdated = Ia.Ngn.Cl.Model.Data.Nokia.Ims.UpdateNgfsSubParty(partyId, agcfEndpoint.Id, resultCode, subParty);

                        // below: read the SubParty
                        subPartyId = Ia.Ngn.Cl.Model.Nokia.SubParty.SubPartyId(partyId);
                        subParty = Ia.Ngn.Cl.Model.Nokia.SubParty.Read(subPartyId);

                        if (subParty != null)
                        {
                            subscriber = alIms.RtrvNgfsSubscriberV2(partyId, edgeRouter, imsService, out resultCode);

                            if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful || resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.PLX_SERVICE_OBJECT_DOES_NOT_EXIST)
                            {
                                service = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Service(partyId);

                                ngfsSubscriberUpdated = Ia.Ngn.Cl.Model.Data.Nokia.Ims.UpdateNgfsSubscriber(partyId, subPartyId, resultCode, subscriber);

                                Ia.Ngn.Cl.Model.Data.Nokia.Ims.UpdateServiceFromAgcfEndpointAndSubPartyAndSubscriber(service, agcfEndpoint, subParty, subscriber);

                                result += "," + service;
                            }
                            else
                            {
                                result += " error: subscriber: " + resultCode.ToString();
                            }
                        }
                        else
                        {
                            result += " subParty is null";
                        }
                    }
                    else
                    {
                        result += " error: subParty: " + resultCode.ToString();
                    }
                }
                else
                {
                    result += " agcfEndpoint is null";
                }
            }
            else
            {
                result += " error: agcfEndpointList: " + resultCode.ToString();
            }
        }
        */


        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateAgcfGatewayRecord(int gwId, Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode, Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord newAgcfGatewayRecord)
        {
            bool updated;
            int id, tableId;
            Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord agcfGatewayRecord;

            tableId = Ia.Ngn.Cl.Model.Business.Nokia.AgcfGatewayTable.NgfsAgcfGatewayTableAid;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                id = Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord.AgcfGatewayRecordId(tableId, gwId);

                agcfGatewayRecord = (from q in db.AgcfGatewayRecords where q.Id == id select q).SingleOrDefault();

                switch (resultCode)
                {
                    case Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful:
                        {
                            if (agcfGatewayRecord == null)
                            {
                                newAgcfGatewayRecord.Created = newAgcfGatewayRecord.Updated = newAgcfGatewayRecord.Inspected = DateTime.UtcNow.AddHours(3);
                                db.AgcfGatewayRecords.Add(newAgcfGatewayRecord);

                                updated = true;
                            }
                            else
                            {
                                if (agcfGatewayRecord.Update(newAgcfGatewayRecord))
                                {
                                    db.AgcfGatewayRecords.Attach(agcfGatewayRecord);
                                    db.Entry(agcfGatewayRecord).State = System.Data.Entity.EntityState.Modified;

                                    updated = true;
                                }
                                else updated = false;
                            }

                            break;
                        }
                    case Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.PLX_SERVICE_OBJECT_DOES_NOT_EXIST:
                        {
                            if (agcfGatewayRecord != null)
                            {
                                // below: delete if foreign key is not null or zero
                                if (agcfGatewayRecord.AgcfEndpoints == null || agcfGatewayRecord.AgcfEndpoints.Count == 0)
                                {
                                    db.AgcfGatewayRecords.Remove(agcfGatewayRecord);

                                    updated = true;
                                }
                                else updated = false;
                            }
                            else updated = false;

                            break;
                        }
                    default: updated = false; break;
                }

                db.SaveChanges();
            }

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateNgfsAgcfEndpoint(string prividUser, Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode, Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint newAgcfEndpoint)
        {
            bool isOk;
            int agcfGatewayRecordsId;
            string id;
            Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint agcfEndpoint;

            isOk = false;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                id = Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint.AgcfEndpointId(prividUser);

                agcfEndpoint = (from q in db.AgcfEndpoints where q.Id == id select q).SingleOrDefault();

                switch (resultCode)
                {
                    case Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful:
                        {
                            if (agcfEndpoint == null)
                            {
                                newAgcfEndpoint.Created = newAgcfEndpoint.Updated = newAgcfEndpoint.Inspected = DateTime.UtcNow.AddHours(3);
                                db.AgcfEndpoints.Add(newAgcfEndpoint);
                            }
                            else
                            {
                                // below: reference
                                agcfGatewayRecordsId = Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord.AgcfGatewayRecordId(Ia.Ngn.Cl.Model.Business.Nokia.AgcfGatewayTable.NgfsAgcfGatewayTableAid, newAgcfEndpoint.GwId);
                                newAgcfEndpoint.AgcfGatewayRecord = (from q in db.AgcfGatewayRecords where q.Id == agcfGatewayRecordsId select q).SingleOrDefault();

                                if (agcfEndpoint.Update(newAgcfEndpoint))
                                {
                                    db.AgcfEndpoints.Attach(agcfEndpoint);
                                    db.Entry(agcfEndpoint).State = System.Data.Entity.EntityState.Modified;
                                }
                            }

                            break;
                        }
                    case Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.PLX_SERVICE_OBJECT_DOES_NOT_EXIST:
                        {
                            if (agcfEndpoint != null)
                            {
                                // below: skip delete if the foreign key is not null
                                if (agcfEndpoint.SubParties == null)
                                {
                                    db.AgcfEndpoints.Remove(agcfEndpoint);
                                }
                            }
                            break;
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
        public static bool UpdateNgfsAgcfEndpointList(int gwId, int agcfGatewayRecordId, Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode, List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> newAgcfEndpointList)
        {
            bool updated;
            Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord agcfGatewayRecord;
            Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint agcfEndpoint;
            List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList;

            updated = true;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfEndpointList = (from q in db.AgcfEndpoints where q.GwId == gwId select q).ToList();

                switch (resultCode)
                {
                    case Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful:
                        {
                            // below: add or update read endpoints
                            foreach (Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint newAgcfEndpoint in newAgcfEndpointList)
                            {
                                // below: reference
                                agcfGatewayRecord = (from q in db.AgcfGatewayRecords where q.Id == agcfGatewayRecordId select q).SingleOrDefault();
                                agcfEndpoint = (from q in db.AgcfEndpoints where q.Id == newAgcfEndpoint.Id select q).SingleOrDefault();
                                newAgcfEndpoint.AgcfGatewayRecord = agcfGatewayRecord;

                                if (agcfEndpoint == null)
                                {
                                    newAgcfEndpoint.Created = newAgcfEndpoint.Updated = newAgcfEndpoint.Inspected = DateTime.UtcNow.AddHours(3);
                                    db.AgcfEndpoints.Add(newAgcfEndpoint);

                                    // updated = updated && true;
                                }
                                else
                                {
                                    if (agcfEndpoint.Update(newAgcfEndpoint))
                                    {
                                        db.AgcfEndpoints.Attach(agcfEndpoint);
                                        db.Entry(agcfEndpoint).State = System.Data.Entity.EntityState.Modified;

                                        //updated = updated && true;
                                    }
                                    else updated = false;
                                }
                            }

                            // below: remove stored endpoint that do not exist in read
                            if (agcfEndpointList != null && agcfEndpointList.Count > 0)
                            {
                                foreach (Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint storedAgcfEndpoint in agcfEndpointList)
                                {
                                    agcfEndpoint = (from a in newAgcfEndpointList where a.Id == storedAgcfEndpoint.Id select a).SingleOrDefault();

                                    if (agcfEndpoint == null && (storedAgcfEndpoint.SubParties == null || storedAgcfEndpoint.SubParties.Count == 0))
                                    {
                                        db.AgcfEndpoints.Remove(storedAgcfEndpoint);

                                        updated = true;
                                    }
                                }
                            }

                            break;
                        }
                    case Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.SuccessfulButNoParameters:
                    case Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.PLX_SERVICE_OBJECT_DOES_NOT_EXIST:
                        {
                            if (agcfEndpointList != null && agcfEndpointList.Count > 0)
                            {
                                foreach (Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint deletedAgcfEndpoint in agcfEndpointList)
                                {
                                    // below: delete if foreign key is not null or zero
                                    if (deletedAgcfEndpoint.SubParties == null || deletedAgcfEndpoint.SubParties.Count == 0)
                                    {
                                        db.AgcfEndpoints.Remove(deletedAgcfEndpoint);

                                        //updated = updated && true;
                                    }
                                    else updated = false;// updated && false;
                                }
                            }
                            break;
                        }
                    default: updated = false; break;
                }

                db.SaveChanges();
            }

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateNgfsSubParty(string partyId, string agcfEndpointId, Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode, Ia.Ngn.Cl.Model.Nokia.SubParty newSubParty)
        {
            bool updated;
            string id;
            Ia.Ngn.Cl.Model.Nokia.SubParty subParty;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                id = Ia.Ngn.Cl.Model.Nokia.SubParty.SubPartyId(partyId);

                subParty = (from q in db.SubParties where q.Id == id select q).SingleOrDefault();

                switch (resultCode)
                {
                    case Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful:
                        {
                            // below: reference
                            agcfEndpointId = Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint.AgcfEndpointId(agcfEndpointId);
                            newSubParty.AgcfEndpoint = (from q in db.AgcfEndpoints where q.Id == agcfEndpointId select q).SingleOrDefault();

                            if (subParty == null)
                            {
                                newSubParty.Created = newSubParty.Updated = newSubParty.Inspected = DateTime.UtcNow.AddHours(3);
                                db.SubParties.Add(newSubParty);

                                updated = true;
                            }
                            else
                            {
                                // below: they don't
                                // <PrimaryPUID>icsSubAgcfpuid</PrimaryPUID> in <SubParty> element of ent-ngfs-subscriber-v2 has to match <Dn>icsSubAgcfpuid</Dn> in ent-ngfs-agcfendpoint-v2

                                if (subParty.Update(newSubParty))
                                {
                                    db.SubParties.Attach(subParty);
                                    db.Entry(subParty).State = System.Data.Entity.EntityState.Modified;

                                    updated = true;
                                }
                                else updated = false;
                            }

                            break;
                        }
                    case Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.PLX_SERVICE_OBJECT_DOES_NOT_EXIST:
                        {
                            if (subParty != null)
                            {
                                // below: delete if foreign key is not null or zero
                                if (subParty.Subscribers == null || subParty.Subscribers.Count == 0)
                                {
                                    db.SubParties.Remove(subParty);

                                    updated = true;
                                }
                                else updated = false;
                            }
                            else updated = false;
                            break;
                        }
                    default: updated = false; break;
                }

                db.SaveChanges();
            }

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateNgfsSubscriber(string partyId, string subPartyId, Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode, Ia.Ngn.Cl.Model.Nokia.Subscriber newSubscriber)
        {
            bool updated;
            string id;
            Ia.Ngn.Cl.Model.Nokia.Subscriber subscriber;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                id = Ia.Ngn.Cl.Model.Nokia.Subscriber.SubscriberId(partyId);

                subscriber = (from q in db.Subscribers where q.Id == id select q).SingleOrDefault();

                switch (resultCode)
                {
                    case Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful:
                        {
                            // below: reference
                            newSubscriber.SubParty = (from q in db.SubParties where q.Id == subPartyId select q).SingleOrDefault();

                            if (subscriber == null)
                            {
                                newSubscriber.Created = newSubscriber.Updated = newSubscriber.Inspected = DateTime.UtcNow.AddHours(3);
                                db.Subscribers.Add(newSubscriber);

                                updated = true;
                            }
                            else
                            {
                                if (subscriber.Update(newSubscriber))
                                {
                                    db.Subscribers.Attach(subscriber);
                                    db.Entry(subscriber).State = System.Data.Entity.EntityState.Modified;

                                    updated = true;
                                }
                                else updated = false;
                            }

                            break;
                        }
                    case Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.PLX_SERVICE_OBJECT_DOES_NOT_EXIST:
                        {
                            if (subscriber != null)
                            {
                                db.Subscribers.Remove(subscriber);

                                updated = true;
                            }
                            else updated = false;

                            break;
                        }
                    default: updated = false; break;
                }

                db.SaveChanges();
            }

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateServiceFromAgcfGatewayRecordAndAgcfEndpointAndSubPartyAndSubscriber(string service, Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord agcfGatewayRecord, Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint agcfEndpoint, Ia.Ngn.Cl.Model.Nokia.SubParty subParty, Ia.Ngn.Cl.Model.Nokia.Subscriber subscriber)
        {
            bool isOk;
            int i, serviceType;
            string serviceId;
            Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt;
            Ia.Ngn.Cl.Model.Service2 service2, newService;

            isOk = false;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceType = 1; // serviceType = 1 for phones
                serviceId = Ia.Ngn.Cl.Model.Service2.ServiceId(service, serviceType);

                // below:
                if (agcfGatewayRecord != null && agcfEndpoint != null && subParty != null && subscriber != null)
                {
                    newService = new Ia.Ngn.Cl.Model.Service2();

                    newService.Id = serviceId;
                    newService.AreaCode = Ia.Ngn.Cl.Model.Data.Service.CountryCode;
                    newService.Service = service;
                    newService.ServiceType = serviceType;

                    newService.CallerId = Ia.Ngn.Cl.Model.Business.Nokia.Subscriber.CallingLineIdIsAssigned(subscriber._CallingLineId);
                    newService.AbbriviatedCalling = Ia.Ngn.Cl.Model.Business.Nokia.Subscriber.OneDigitSpeedDialIsAssigned(subscriber._OneDigitSpeedDial);
                    newService.CallForwarding = Ia.Ngn.Cl.Model.Business.Nokia.Subscriber.CallForwardingIsAssigned(subscriber._CallForwardingVari);
                    newService.CallWaiting = Ia.Ngn.Cl.Model.Business.Nokia.Subscriber.CallWaitingIsAssigned(subscriber._CallWaiting, agcfEndpoint.CallWaitingLc);
                    newService.ConferenceCall = Ia.Ngn.Cl.Model.Business.Nokia.Subscriber.ConferenceCallIsAssigned(subscriber._ConferenceCalling);
                    newService.InternationalCalling = Ia.Ngn.Cl.Model.Business.Nokia.Subscriber.InternationalCallingIsAssigned(subscriber._CallBarring);
                    newService.InternationalCallingUserControlled = Ia.Ngn.Cl.Model.Business.Nokia.Subscriber.InternationalCallingUserControlledIsAssigned(subscriber._OutgoingCallBarring);
                    newService.WakeupCall = Ia.Ngn.Cl.Model.Business.Nokia.Subscriber.ReminderCallIsAssigned(subscriber._ReminderCall);

                    newService.ServiceSuspension = subParty.ServiceSuspension;

                    newService.Pin = int.TryParse(subParty.Pin, out i) ? i : 0;

                    nddOnt = (from n in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where n.Ip == agcfGatewayRecord.IP1 select n).SingleOrDefault();

                    if (nddOnt != null) newService.Access = (from a in db.Accesses where a.Id == nddOnt.Access.Id select a).SingleOrDefault();
                    else newService.Access = null;
 
                    service2 = (from q in db.Service2s where q.Id == newService.Id select q).SingleOrDefault();

                    if (service2 == null)
                    {
                        newService.Created = newService.Updated = newService.Inspected = DateTime.UtcNow.AddHours(3);
                        db.Service2s.Add(newService);
                    }
                    else
                    {
                        if (service2.Update(newService))
                        {
                            db.Service2s.Attach(service2);
                            db.Entry(service2).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                }
                else
                {
                    service2 = (from q in db.Service2s where q.Id == serviceId select q).SingleOrDefault();

                    if (service2 != null) db.Service2s.Remove(service2);
                }

                db.SaveChanges();

                isOk = true;
            }

            return isOk;
        }

        /*
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateServiceList(Dictionary<long, Ia.Ngn.Cl.Model.Service> dnServiceDictionary)
        {
            bool isOk;
            int serviceType;
            long dn;
            string serviceId;
            Ia.Ngn.Cl.Model.Service service;
            Ia.Ngn.Cl.Model.Service2 service2, newService2;

            isOk = false;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below:
                foreach (KeyValuePair<long, Ia.Ngn.Cl.Model.Service> u in dnServiceDictionary)
                {
                    dn = u.Key;
                    service = u.Value;

                    serviceType = 1; // serviceType = 1 for phones
                    serviceId = Ia.Ngn.Cl.Model.Service2.ServiceId(dn.ToString().Remove(0, 3), serviceType); // temp: remove 3 digits

                    // below:
                    if (service != null)
                    {
                        newService2 = new Ia.Ngn.Cl.Model.Service2();

                        newService2.Id = serviceId;
                        newService2.AreaCode = Ia.Ngn.Cl.Model.Data.Service.CountryCode;
                        newService2.Service = dn.ToString();
                        newService2.ServiceType = serviceType;

                        //newService2.ServiceSuspension = ??
                        newService2.CallBarring = service.OCBUC_ass;
                        newService2.CallForwarding = service.CFU_ass;
                        newService2.ConferenceCall = service.CONF;
                        //newService2.ConferenceCall = service.TPS_ass;

                        newService2.AlarmCall = service.ALM_ass;
                        newService2.CallWaiting = service.CW_ass;
                        newService2.Pin = service.PIN_code;

                        newService2.CallerId = service.CLIP;

                        service2 = (from q in db.Service2s where q.Id == newService2.Id select q).SingleOrDefault();

                        if (service2 == null)
                        {
                            newService2.Created = newService2.Updated = newService2.Inspected = DateTime.UtcNow.AddHours(3);
                            db.Service2s.Add(newService2);
                        }
                        else
                        {
                            if (service2.Update(newService2))
                            {
                                db.Service2s.Attach(service2);
                                db.Entry(service2).State = System.Data.Entity.EntityState.Modified;
                            }
                        }
                    }
                    else
                    {
                        service2 = (from q in db.Service2s where q.Id == serviceId select q).SingleOrDefault();

                        if (service2 != null) db.Service2s.Remove(service2);
                    }
                }

                db.SaveChanges();

                isOk = true;
            }

            return isOk;
        }
         */

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> AllPossibleGatewayIdList
        {
            get
            {
                // below: GwId are defined by Nokia

                List<int> allPossibleGatewayIdList;

                allPossibleGatewayIdList = new List<int>(Ia.Ngn.Cl.Model.Business.Nokia.Ims.LastGatewayId - Ia.Ngn.Cl.Model.Business.Nokia.Ims.FirstGatewayId);

                //allPossibleGatewayIdList = Ia.Ngn.Cl.Model.Data.Nokia.Default.GatewayIdList;

                for (int i = Ia.Ngn.Cl.Model.Business.Nokia.Ims.FirstGatewayId; i <= Ia.Ngn.Cl.Model.Business.Nokia.Ims.LastGatewayId; i++)
                {
                    allPossibleGatewayIdList.Add(i);
                }

                return allPossibleGatewayIdList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ont> AllPossibleOntNotInAgcfGatewayRecordList(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt)
        {
            List<Ia.Ngn.Cl.Model.Ont> ontsNotInAgcfGatewayRecordsList;
            List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordsNoInOntsList;

            DiscrepancyBetweenOntListAndAgctGatewayRecordList(olt, out ontsNotInAgcfGatewayRecordsList, out agcfGatewayRecordsNoInOntsList);

            return ontsNotInAgcfGatewayRecordsList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> AllPossibleNddOntNotInAgcfGatewayRecordList(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt)
        {
            List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> ngnOntList;
            List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> ngnOntsNotInAgcfGatewayRecordsList;
            List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList;

            // below: NGN ONT list
            ngnOntList = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where q.Pon.Olt.Id == olt.Id select q).ToList();

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below: AgcfGatewayRecord list
                agcfGatewayRecordList = (from a in db.AgcfGatewayRecords select a).ToList();
            }

            // below: ONTs not in AgcfGatewayRecord list
            ngnOntsNotInAgcfGatewayRecordsList = (from no in ngnOntList
                                                  join a in agcfGatewayRecordList on no.Ip equals a.IP1
                                                  into gj
                                                  from sub in gj.DefaultIfEmpty()
                                                  where sub == null
                                                  select no).ToList();

            return ngnOntsNotInAgcfGatewayRecordsList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> AllPossibleAgcfGatewayRecordsNoInOntsList(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt)
        {
            List<Ia.Ngn.Cl.Model.Ont> ontsNotInAgcfGatewayRecordsList;
            List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordsNoInOntsList;

            DiscrepancyBetweenOntListAndAgctGatewayRecordList(olt, out ontsNotInAgcfGatewayRecordsList, out agcfGatewayRecordsNoInOntsList);

            return agcfGatewayRecordsNoInOntsList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static void DiscrepancyBetweenOntListAndAgctGatewayRecordList(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt, out List<Ia.Ngn.Cl.Model.Ont> ontsNotInAgcfGatewayRecordsList, out List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordsNoInOntsList)
        {
            List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> ngnOntList;
            List<Ia.Ngn.Cl.Model.Ont> ontList;
            List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList;

            // below: NGN ONT list
            ngnOntList = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where q.Pon.Olt.Id == olt.Id select q).ToList();

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below: ONT list
                ontList = (from o in db.Onts where o.Access != null && o.Access.Olt == olt.Id select o).ToList();

                // below: AgcfGatewayRecord list
                agcfGatewayRecordList = (from a in db.AgcfGatewayRecords select a).ToList();
            }

            // below: ONTs not in AgcfGatewayRecord list
            ontsNotInAgcfGatewayRecordsList = (from o in ontList
                                               join no in ngnOntList on o.Id equals no.Id
                                               join a in agcfGatewayRecordList on no.Ip equals a.IP1
                                               into gj
                                               from sub in gj.DefaultIfEmpty()
                                               where sub == null
                                               select o).ToList();

            // below: AgcfGatewayRecords with IPs that are not in NGN ONT list
            agcfGatewayRecordsNoInOntsList = (from a in agcfGatewayRecordList
                                              join no in ngnOntList on a.IP1 equals no.Ip
                                              into gj
                                              from sub in gj.DefaultIfEmpty()
                                              where sub == null
                                              select a).ToList();

        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> AgcfEndpointsWithNoSubPartyReferenceList
        {
            get
            {
                List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList;

                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    agcfEndpointList = (from e in db.AgcfEndpoints join s in db.SubParties on e equals s.AgcfEndpoint into gj from u in gj.DefaultIfEmpty() where u == null select e).ToList();
                }

                return agcfEndpointList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
