using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data.Entity;
using System.Net;
using System.IO;

namespace Ia.Ngn.Cl.Model.Client.Nokia
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Optical Fiber Network Management Intranet Portal (OFN) client support class for Nokia's Next Generation Network (NGN) client model.
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
    public class Ims
    {
        private static bool success;
        private static string sessionId, commandInLower, requestId, switchName;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 1360 COM WebAPI User Guide
        /// </summary>
        public enum ResultCode
        {
            Successful = 0, // added by me
            SuccessfulButNoParameters = 100000, // added by me
            DocumentElementIsNull = 100001, // added by me
            PLX_SERVICE_NO_CONNECTION_TO_SWITCH = 1,
            PLX_SERVICE_INVALID_OBJECT_ID = 2,
            PLX_SERVICE_CONNECT_FAILURE = 3,
            PLX_SERVICE_OBJECT_DOES_NOT_EXIST = 4,
            PLX_NE_RESPONSE_FAILURE = 5,
            PLX_INVALID_OPERATION = 6,
            PLX_INVALID_TID = 7,
            PLX_SWITCH_CONNECT_FAILED = 8,
            PLX_SWITCH_NOT_MANAGED = 9,
            PLX_SWITCH_NO_ACTIVE_CONNECTION = 10,
            PLX_INVALID_TL1_META_PROCESSING = 11,
            PLX_SWITCH_VERSION_MISMATCH = 13,
            PLX_INVALID_REGULAR_EXPRESSION = 14,
            PLX_INVALID_RANGE = 15,
            PLX_FILE_IO_ERROR = 17,
            PLX_XML_ERROR = 18,
            PLX_TRANSIENT_STATE = 19,
            PLX_UNABLE_TO_PERFORM_OPERATION = 21,
            PLX_MAX_CONFIGURATION_EXCEEDED = 22,
            PLX_NOT_SUPPORTED = 23,
            PLX_REQ_PARAMETER_NOT_SET = 24,
            PLX_CONGESTION_CONTROL_REQUEST_TIMEOUT = 26,
            PLX_CONGESTION_CONTROL_CONGESTION_DETECTED = 27,
            PLX_DB_REQUEST_FAILURE = 10001,
            PLX_DB_LOGIN_FAILURE = 10002,
            PLX_USER_PRIV_VIOLATION = 10202,
            PLX_INVALID_IP = 20002,
            PLX_INVALID_NETWORK_IP = 20003,
            PLX_DUPLICATE_NAME = 20006,
            PLX_INVALID_PORT_ID = 20008,
            PLX_UNABLE_TO_ADD_DUPLICATE = 20011,
            PLX_UNABLE_TO_DELETE_CHILDREN_EXIST = 20012,
            PLX_MUST_DELETE_ACL_FIRST = 20013,
            PLX_FAILURE = 20014,
            PLX_SYNC_FAILURE = 20015,
            PLX_OE_NOT_SUPPORTED = 20016,
            PLX_INVALID_CHARS_IN_NAME = 20017,
            PLX_SWITCH_EM_USERNAME_INVALID_CHARS = 20108,
            PLX_SWITCH_EM_PASSWORD_INVALID_CHARS = 20110,
            PLX_SWITCH_CANNOT_MODIFY_NAME = 20121,
            PLX_ROUTE_INVALID_NAME_LEN = 20804,
            PLX_ROUTE_INVALID_NAME_CHARS = 20805,
            PLX_ROUTE_INVALID_ROUTE_ID = 20806,
            PLX_ROUTE_INVALID_ROUTE_PRIORITY = 20807,
            PLX_NUMBER_INVALID_NPA = 21901,
            PLX_NUMBER_INVALID_NXX = 21902,
            PLX_INVALID_SUBSCRIBER_ID = 22200,
            PLX_INVALID_SUBSCRIBER_PIC_CODE = 22201,
            PLX_INVALID_SUBSCRIBER_STATUS = 22204,
            WEB_API_FAILURE = 30000,
            SESSION_INVALID = 30001,
            SESSION_TIMED_OUT = 30002,
            REQUEST_TIMED_OUT = 30003,
            PLX_CTS_UNKNOWN_ERROR = 50000,
            PLX_CTS_ERROR_DBUNAVAIL = 50001,
            PLX_CTS_ERROR_DBERROR = 50002,
            PLX_CTS_ERROR_DATAINVALID = 50003,
            PLX_CTS_ERROR_DATAREPLICATION = 50004,
            PLX_CTS_ERROR_NODATAFOUND = 50005,
            PLX_CTS_ERROR_UNAUTHORIZED = 50006,
            PLX_CTS_ERROR_RETRYLATER = 50007,
            PLX_CTS_ERROR_OVERLOAD = 50008,
            PLX_CTS_ERROR_DUPLICATEKEY = 50009,
            PLX_CTS_ERROR_VERSIONMISMATCH = 50010,
            PLX_CTS_ERROR_UPDATELOGOVERRUN = 50011,
            PLX_CTS_ERROR_UNEXPECTEDSTATE = 50012
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Ims()
        {
            // below: trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public string SessionId
        {
            get { return sessionId; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void ActUser(out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            XmlDocument soapResultEnvelopeXmlDocument;

            SendSoapRequestAndReadResponse(ActUserSoapEnvelopeXml(sessionId), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void CancUser(out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            XmlDocument soapResultEnvelopeXmlDocument;

            SendSoapRequestAndReadResponse(CancUserSoapEnvelopeXml(sessionId), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EntNgfsAgcfGatewayRecordV2(int gwId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            int tableId;
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            tableId = Ia.Ngn.Cl.Model.Business.Nokia.AgcfGatewayTable.NgfsAgcfGatewayTableAid;

            SendSoapRequestAndReadResponse(EntNgcfAgcfGatewayRecordV2SoapEnvelopeXml(tableId, gwId, nddOnt, sessionId), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord RtrvNgfsAgcfGatewayRecordV2(int gwId, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            int tableId;
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;
            Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord agcfGatewayRecord;

            tableId = Ia.Ngn.Cl.Model.Business.Nokia.AgcfGatewayTable.NgfsAgcfGatewayTableAid;

            SendSoapRequestAndReadResponse(RtrvNgcfAgcfGatewayRecordV2SoapEnvelopeXml(tableId, gwId, sessionId), out soapResultEnvelopeXmlDocument);

            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);

            if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful)
            {
                agcfGatewayRecord = Ia.Ngn.Cl.Model.Business.Nokia.AgcfGatewayRecord.ParseFromDictionary(parameterDictionaryList[0]);
            }
            else agcfGatewayRecord = null;

            return agcfGatewayRecord;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void DltNgfsAgcfGatewayRecordV2(int gwId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            int tableId;
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            tableId = Ia.Ngn.Cl.Model.Business.Nokia.AgcfGatewayTable.NgfsAgcfGatewayTableAid;

            SendSoapRequestAndReadResponse(DltNgcfAgcfGatewayRecordV2SoapEnvelopeXml(tableId, gwId, nddOnt, sessionId), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EntNgfsAgcfGwCombinedRecV2(int gwId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont ont, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            int tableId;
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            tableId = Ia.Ngn.Cl.Model.Business.Nokia.AgcfGatewayTable.NgfsAgcfGatewayTableAid;

            SendSoapRequestAndReadResponse(EntNgfsAgcfGwCombinedRecV2SoapEnvelopeXml(tableId, gwId, ont, sessionId), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void DltNgfsAgcfGwCombinedRecV2(int gwId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            int tableId;
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            tableId = Ia.Ngn.Cl.Model.Business.Nokia.AgcfGatewayTable.NgfsAgcfGatewayTableAid;

            SendSoapRequestAndReadResponse(DltNgfsAgcfGwCombinedRecV2SoapEnvelopeXml(tableId, gwId, nddOnt, sessionId), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EntNgfsAgcfEndpointV2(string service, int gwId, int flatTermId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            XmlDocument soapResultEnvelopeXmlDocument;
            List<Dictionary<string, string>> parameterDictionaryList;

            SendSoapRequestAndReadResponse(EntNgfsAgcfEndpointV2SoapEnvelopeXml(service, gwId, flatTermId, nddOnt, sessionId), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);

            //resultCode = Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> RtrvNgfsAgcfEndpointV2(string prividUser, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            XmlDocument soapResultEnvelopeXmlDocument;
            List<Dictionary<string, string>> parameterDictionaryList;
            List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList;

            SendSoapRequestAndReadResponse(RtrvNgfsAgcfEndpointV2SoapEnvelopeXml(prividUser, nddOnt, sessionId), out soapResultEnvelopeXmlDocument);

            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);

            if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful)
            {
                agcfEndpointList = Ia.Ngn.Cl.Model.Business.Nokia.AgcfEndpoint.ParseFromDictionary(parameterDictionaryList);
            }
            else agcfEndpointList = null;

            return agcfEndpointList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> RtrvNgfsAgcfEndpointV2List(int gwId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            XmlDocument soapResultEnvelopeXmlDocument;
            List<Dictionary<string, string>> parameterDictionaryList;
            List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList;

            SendSoapRequestAndReadResponse(RtrvNgfsAgcfEndpointV2SoapEnvelopeXml(gwId, nddOnt, sessionId), out soapResultEnvelopeXmlDocument);

            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);

            if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful)
            {
                agcfEndpointList = Ia.Ngn.Cl.Model.Business.Nokia.AgcfEndpoint.ParseFromDictionary(parameterDictionaryList);
            }
            else agcfEndpointList = null;

            return agcfEndpointList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> RtrvNgfsAgcfEndpointV2List(int gwId, string edgeRouter, int imsService, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            XmlDocument soapResultEnvelopeXmlDocument;
            List<Dictionary<string, string>> parameterDictionaryList;
            List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList;

            SendSoapRequestAndReadResponse(RtrvNgfsAgcfEndpointV2SoapEnvelopeXml(gwId, edgeRouter, imsService, sessionId), out soapResultEnvelopeXmlDocument);

            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);

            if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful)
            {
                agcfEndpointList = Ia.Ngn.Cl.Model.Business.Nokia.AgcfEndpoint.ParseFromDictionary(parameterDictionaryList);
            }
            else agcfEndpointList = null;

            return agcfEndpointList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EdNgfsAgcfEndpointV2_CallWaiting(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool serviceAssigned, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            XmlDocument soapResultEnvelopeXmlDocument;
            List<Dictionary<string, string>> parameterDictionaryList;

            SendSoapRequestAndReadResponse(EdNgfsAgcfEndpointV2SoapEnvelopeXml_CallWaiting(service, nddOnt, serviceAssigned, sessionId), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void DltNgfsAgcfEndpointV2(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            SendSoapRequestAndReadResponse(DltNgfsAgcfEndpointV2SoapEnvelopeXml(service, nddOnt, sessionId), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EntNgfsSubPartyV2(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            SendSoapRequestAndReadResponse(EntNgfsSubPartyV2SoapEnvelopeXml(service, nddOnt, sessionId), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);

            //resultCode = Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Ia.Ngn.Cl.Model.Nokia.SubParty RtrvNgfsSubPartyV2(string partyId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            XmlDocument soapResultEnvelopeXmlDocument;
            List<Dictionary<string, string>> parameterDictionaryList;
            Ia.Ngn.Cl.Model.Nokia.SubParty subParty;

            SendSoapRequestAndReadResponse(RtrvNgfsSubPartyV2SoapEnvelopeXml(partyId, nddOnt, sessionId), out soapResultEnvelopeXmlDocument);

            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);

            if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful)
            {
                subParty = Ia.Ngn.Cl.Model.Business.Nokia.SubParty.ParseFromDictionary(parameterDictionaryList[0]);
            }
            else subParty = null;

            return subParty;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Ia.Ngn.Cl.Model.Nokia.SubParty RtrvNgfsSubPartyV2(string partyId, string edgeRouter, int imsService, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            XmlDocument soapResultEnvelopeXmlDocument;
            List<Dictionary<string, string>> parameterDictionaryList;
            Ia.Ngn.Cl.Model.Nokia.SubParty subParty;

            SendSoapRequestAndReadResponse(RtrvNgfsSubPartyV2SoapEnvelopeXml(partyId, edgeRouter, imsService, sessionId), out soapResultEnvelopeXmlDocument);

            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);

            if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful)
            {
                subParty = Ia.Ngn.Cl.Model.Business.Nokia.SubParty.ParseFromDictionary(parameterDictionaryList[0]);
            }
            else subParty = null;

            return subParty;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void DltNgfsSubPartyV2(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            SendSoapRequestAndReadResponse(DltNgfsSubPartyV2SoapEnvelopeXml(service, nddOnt, sessionId), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EntNgfsSubscriberV2(string service, int gwId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            SendSoapRequestAndReadResponse(EntNgfsSubscriberV2SoapEnvelopeXml(service, gwId, nddOnt, sessionId), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);

            //resultCode = Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Ia.Ngn.Cl.Model.Nokia.Subscriber RtrvNgfsSubscriberV2(string aid, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            XmlDocument soapResultEnvelopeXmlDocument;
            List<Dictionary<string, string>> parameterDictionaryList;
            Ia.Ngn.Cl.Model.Nokia.Subscriber subscriber;

            SendSoapRequestAndReadResponse(RtrvNgfsSubscriberV2SoapEnvelopeXml(aid, nddOnt, sessionId), out soapResultEnvelopeXmlDocument);

            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);

            if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful)
            {
                subscriber = Ia.Ngn.Cl.Model.Business.Nokia.Subscriber.ParseFromDictionary(parameterDictionaryList[0]);
            }
            else subscriber = null;

            return subscriber;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Ia.Ngn.Cl.Model.Nokia.Subscriber RtrvNgfsSubscriberV2(string aid, string edgeRouter, int imsService, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            XmlDocument soapResultEnvelopeXmlDocument;
            List<Dictionary<string, string>> parameterDictionaryList;
            Ia.Ngn.Cl.Model.Nokia.Subscriber subscriber;

            SendSoapRequestAndReadResponse(RtrvNgfsSubscriberV2SoapEnvelopeXml(aid, edgeRouter, imsService, sessionId), out soapResultEnvelopeXmlDocument);

            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);

            if (resultCode == Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful)
            {
                subscriber = Ia.Ngn.Cl.Model.Business.Nokia.Subscriber.ParseFromDictionary(parameterDictionaryList[0]);
            }
            else subscriber = null;

            return subscriber;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void DltNgfsSubscriberV2(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            SendSoapRequestAndReadResponse(DltNgfsSubscriberV2SoapEnvelopeXml(service, nddOnt, sessionId), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EdNgfsSubscriberV2_CallingLineId(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool callingLineIdState, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            string xml;
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            xml = CallingLineIdXml(service, callingLineIdState);

            SendSoapRequestAndReadResponse(EdNgfsSubscriberV2SoapEnvelopeXml(service, nddOnt, sessionId, xml), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EdNgfsSubscriberV2_OneDigitSpeedDial(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool oneDigitSpeedDialState, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            string xml;
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            xml = OneDigitSpeedDial(service, oneDigitSpeedDialState);

            SendSoapRequestAndReadResponse(EdNgfsSubscriberV2SoapEnvelopeXml(service, nddOnt, sessionId, xml), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EdNgfsSubscriberV2_CallForwardingVari(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool callForwardingState, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            string xml;
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            xml = CallForwardingVariXml(service, callForwardingState);

            SendSoapRequestAndReadResponse(EdNgfsSubscriberV2SoapEnvelopeXml(service, nddOnt, sessionId, xml), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EdNgfsSubscriberV2_CallWaiting(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool callWaitingState, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            string xml;
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            xml = CallWaitingXml(service, callWaitingState);

            SendSoapRequestAndReadResponse(EdNgfsSubscriberV2SoapEnvelopeXml(service, nddOnt, sessionId, xml), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EdNgfsSubscriberV2_ConferenceCall(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool conferenceCallState, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            string xml;
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            xml = ConferenceCallingXml(service, conferenceCallState);

            SendSoapRequestAndReadResponse(EdNgfsSubscriberV2SoapEnvelopeXml(service, nddOnt, sessionId, xml), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EdNgfsSubscriberV2_ReminderCall(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool reminderCallState, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            string xml;
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            xml = ReminderCallXml(service, reminderCallState);

            SendSoapRequestAndReadResponse(EdNgfsSubscriberV2SoapEnvelopeXml(service, nddOnt, sessionId, xml), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EdNgfsSubscriberV2_InternationalCalling(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool internationalCallingState, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            string xml;
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            xml = InternationalCallingXml(service, internationalCallingState);

            SendSoapRequestAndReadResponse(EdNgfsSubscriberV2SoapEnvelopeXml(service, nddOnt, sessionId, xml), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EdNgfsSubscriberV2_InternationalCallingUserControlled(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool internationalCallBarringState, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            string xml;
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            xml = InternationalCallingUserControlledXml(service, internationalCallBarringState);
            
            SendSoapRequestAndReadResponse(EdNgfsSubscriberV2SoapEnvelopeXml(service, nddOnt, sessionId, xml), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EdNgfsSubscriberV2_ServiceSuspension(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool serviceSuspensionState, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            string xml;
            List<Dictionary<string, string>> parameterDictionaryList;
            XmlDocument soapResultEnvelopeXmlDocument;

            xml = ServiceSuspension(serviceSuspensionState);

            SendSoapRequestAndReadResponse(EdNgfsSubscriberV2WithinSubPartySoapEnvelopeXml(service, nddOnt, sessionId, xml), out soapResultEnvelopeXmlDocument);
            ParseSoapResultXml(soapResultEnvelopeXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////







        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void EntNgfsSubscriberV2_EntNgfsAgcfEndpointV2(string service, int gwId, int flatTermId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode entNgfsSubscriberResultCode, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode entNgfsAgcfEndpointResultCode)
        {
            EntNgfsSubscriberV2(service, gwId, nddOnt, out entNgfsSubscriberResultCode);

            // below: not needed
            //EntNgfsSubPartyV2(service, nddOnt, out entNgfsSubPartyResultCode);

            EntNgfsAgcfEndpointV2(service, gwId, flatTermId, nddOnt, out entNgfsAgcfEndpointResultCode);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void DltNgfsAgcfEndpointV2_DltNgfsSubscriberV2(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode dltNgfsSubscriberResultCode, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode dltNgfsAgcfEndpointResultCode)
        {
            DltNgfsAgcfEndpointV2(service, nddOnt, out dltNgfsAgcfEndpointResultCode);

            // below: not needed
            //DltNgfsSubPartyV2(service, nddOnt, out dltNgfsSubPartyResultCode);

            DltNgfsSubscriberV2(service, nddOnt, out dltNgfsSubscriberResultCode);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////







        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static XmlDocument ActUserSoapEnvelopeXml(string sessionId)
        {
            XmlDocument soapEnvelopeXml;
            Dictionary<string, string> param;

            param = new Dictionary<string, string>();

            param.Add("UserName", Ia.Ngn.Cl.Model.Data.Nokia.Ims.UserName);
            param.Add("PassWord", Ia.Ngn.Cl.Model.Data.Nokia.Ims.Password);

            soapEnvelopeXml = SoapEnvelopeXml("act-user", param, sessionId, "", "TECICS01", "fsdb0");

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        ///   <PlexViewRequest Command="canc-user" SwitchName="" RequestId="" SessionId="test:2012917080">
        ///   </PlexViewRequest>
        /// </remarks>
        /// </summary>
        private static XmlDocument CancUserSoapEnvelopeXml(string sessionId)
        {
            XmlDocument soapEnvelopeXml;

            soapEnvelopeXml = SoapEnvelopeXml("canc-user", "", sessionId, "", "TECICS01", "fsdb0");

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        /// </remarks>
        /// </summary>
        private static XmlDocument EntNgcfAgcfGatewayRecordV2SoapEnvelopeXml(int tableId, int gwId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId)
        {
            int serviceProfileNumber;
            string xmlContent, puid, ip1, ip2, pridUser, privateId, prsetName, prsetNumber, prsetNum, gwUserId, gwName, agcfSipIaPort;
            XmlDocument soapEnvelopeXml;

            puid = "sip:rgw" + gwId;
            pridUser = "rgw" + gwId;
            privateId = "rgw" + gwId;
            prsetName = "rgw" + gwId;
            prsetNumber = gwId.ToString();
            prsetNum = gwId.ToString();
            gwUserId = "rgw" + gwId;
            gwName = "rgw" + gwId;
            ip1 = nddOnt.Ip; // "1.4.150.2";
            ip2 = "0.0.0.0";

            serviceProfileNumber = Ia.Ngn.Cl.Model.Business.Nokia.Ims.ServiceProfileNumber(nddOnt.Pon.Olt.ImsService);
            agcfSipIaPort = Ia.Ngn.Cl.Model.Business.Nokia.Ims.AgcfSipIaPort(nddOnt.Pon.Olt.ImsService);

            xmlContent = @"
    <AgcfGatewayRecord>
    <TableId>" + tableId + @"</TableId>
	<GwId>" + gwId + @"</GwId>
	<GwUserId>" + gwUserId + @"</GwUserId>
	<GwName>" + gwName + @"</GwName>
    <IP1>" + ip1 + @"</IP1>
	<IP2>" + ip2 + @"</IP2>
    <AgcfSipIaPort>" + agcfSipIaPort + @"</AgcfSipIaPort>
	<ConnectionType>UDP/IP</ConnectionType>
	<ContextAudits>Audit Value Based</ContextAudits>
	<GwDigitMapId>1</GwDigitMapId>
	<EnableChannelStatusAudits>false</EnableChannelStatusAudits>
	<GwDomain>ims.moc1.kw</GwDomain>
	<GwPrivId></GwPrivId>
    <LocalTermTypePrefix>td</LocalTermTypePrefix>
    <LocalTermTypeAnalogPrefix>td</LocalTermTypeAnalogPrefix>
    <IsLocalTermTypeTDMAnalog>true</IsLocalTermTypeTDMAnalog>
    <NetTermTypePrefix>RTP/</NetTermTypePrefix>
    <IsNetTermTypeRTPUDP>true</IsNetTermTypeRTPUDP>
    <PhysicalTermIdScheme>Flat Term ID</PhysicalTermIdScheme>
    <SendCompactMessages>true</SendCompactMessages>
    <SendEphemeralPrefix>false</SendEphemeralPrefix>
    <UdpPort>2944</UdpPort>
    <GwVariantId>1</GwVariantId>
    <AreaId>0</AreaId>
    <ComfortNoise>false</ComfortNoise>
    <Dtmf>false</Dtmf>
    <FaxEvents>false</FaxEvents>
    <ModemEvents>false</ModemEvents>
    <TextTelephonyEvents>false</TextTelephonyEvents>
    <AllCodecDataStr>Audio|G.711 - uLaw|20|false</AllCodecDataStr>
    <AgwIuaIpAddress></AgwIuaIpAddress>
    <AgwIuaSctpPort></AgwIuaSctpPort>
    <AgcfLocalSctpPort></AgcfLocalSctpPort>
    <SctpProfile></SctpProfile>
    <IuaIIDMapScheme></IuaIIDMapScheme>
    <LocalTermTypeIsdnPrefix></LocalTermTypeIsdnPrefix>
    <AuditAllActiveIsdnCalls>false</AuditAllActiveIsdnCalls>
    <MateSite>2</MateSite>
    <MateExternalIPAddr>10.16.5.31</MateExternalIPAddr>
    <IsPrimary>true</IsPrimary>
    <AddtionalDigitMapList></AddtionalDigitMapList>
    <AuthTimer>10</AuthTimer>
    <SharedBasNumber>101</SharedBasNumber>
    <SharedPriNumber>523</SharedPriNumber>
    <MgID></MgID>
    <EnnableMD5DigAuteh>false</EnnableMD5DigAuteh>
    <ChannelAudits>Arming Modify and AuditValue done one after another sequentially</ChannelAudits>
    <GroupId>1</GroupId>
    <AgcfGateWayType>RGW/iAD</AgcfGateWayType>
    </AgcfGatewayRecord>

	<HSSPrivateId>
		<PridUser>" + pridUser + @"</PridUser>
		<PrsetNumber>" + prsetNumber + @"</PrsetNumber>
		<PuidUserList>
			<HSSPublicId>
				<Puid>" + puid + @"</Puid>
                <ServiceProfileNumber>" + serviceProfileNumber + @"</ServiceProfileNumber>
			</HSSPublicId>
		</PuidUserList>
	</HSSPrivateId>

";

            soapEnvelopeXml = SoapEnvelopeXml("ent-ngfs-agcfgatewayrecord-v2", xmlContent, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        /// </remarks>
        /// </summary>
        private static XmlDocument EntNgfsAgcfGwCombinedRecV2SoapEnvelopeXml(int tableId, int gwId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId)
        {
            int serviceProfileNumber;
            string xmlContent, puid, ip1, ip2, mgcIp, mgcSecondaryIp, pridUser, privateId, prsetName, prsetNumber, prsetNum, gwUserId, gwName, agcfSipIaPort;
            XmlDocument soapEnvelopeXml;

            puid = "sip:rgw" + gwId;
            pridUser = "rgw" + gwId;
            privateId = "rgw" + gwId;
            prsetName = "rgw" + gwId;
            prsetNumber = gwId.ToString();
            prsetNum = gwId.ToString();
            gwUserId = "rgw" + gwId;
            gwName = "rgw" + gwId;
            ip1 = nddOnt.Ip; // "1.4.150.2";
            ip2 = "0.0.0.0";
            mgcIp = nddOnt.Pon.Olt.MgcIp;
            mgcSecondaryIp = nddOnt.Pon.Olt.MgcSecondaryIp;

            agcfSipIaPort = Ia.Ngn.Cl.Model.Business.Nokia.Ims.AgcfSipIaPort(nddOnt.Pon.Olt.ImsService);
            serviceProfileNumber = Ia.Ngn.Cl.Model.Business.Nokia.Ims.ServiceProfileNumber(nddOnt.Pon.Olt.ImsService);

            xmlContent = @"
	<AgcfGatewayRecord>
		<NgcfAgcfGatewayRecordAid>" + tableId + "`" + gwId + @"</NgcfAgcfGatewayRecordAid>
		<TableId>" + tableId + @"</TableId>
		<GwId>" + gwId + @"</GwId>
		<AgcfSipIaPort>" + agcfSipIaPort + @"</AgcfSipIaPort>
		<GwUserId>" + gwUserId + @"</GwUserId>
		<GwName>" + gwName + @"</GwName>
		<IP1>" + ip1 + @"</IP1>
		<IP2>" + ip2 + @"</IP2>
		<MateExternalIPAddr>" + mgcIp + @"</MateExternalIPAddr>
		<ConnectionType>UDP/IP</ConnectionType>
		<ContextAudits>Audit Value Based</ContextAudits>
		<GwDigitMapId>1</GwDigitMapId>
		<EnableChannelStatusAudits>false</EnableChannelStatusAudits>
		<GwDomain>ims.moc1.kw</GwDomain>
		<GwPrivId></GwPrivId>
		<LocalTermTypePrefix>td</LocalTermTypePrefix>
		<LocalTermTypeAnalogPrefix>td</LocalTermTypeAnalogPrefix>
		<IsLocalTermTypeTDMAnalog>true</IsLocalTermTypeTDMAnalog>
		<NetTermTypePrefix>RTP</NetTermTypePrefix>
		<IsNetTermTypeRTPUDP>true</IsNetTermTypeRTPUDP>
		<PhysicalTermIdScheme>Flat Term ID</PhysicalTermIdScheme>
		<SendCompactMessages>true</SendCompactMessages>
		<SendEphemeralPrefix>false</SendEphemeralPrefix>
		<UdpPort>2944</UdpPort>
		<GwVariantId>1</GwVariantId>
		<AreaId>0</AreaId>
		<ComfortNoise>false</ComfortNoise>
		<Dtmf>false</Dtmf>
		<FaxEvents>false</FaxEvents>
		<ModemEvents>false</ModemEvents>
		<TextTelephonyEvents>false</TextTelephonyEvents>
		<AllCodecDataStr>Audio|G.711 - uLaw|20|false</AllCodecDataStr>
		<AgwIuaIpAddress></AgwIuaIpAddress>
		<AgwIuaSctpPort></AgwIuaSctpPort>
		<AgcfLocalSctpPort></AgcfLocalSctpPort>
		<SctpProfile></SctpProfile>
		<IuaIIDMapScheme></IuaIIDMapScheme>
		<LocalTermTypeIsdnPrefix></LocalTermTypeIsdnPrefix>
		<AuditAllActiveIsdnCalls>false</AuditAllActiveIsdnCalls>
		<MateSite>2</MateSite>
		<IsPrimary>true</IsPrimary>
		<AddtionalDigitMapList></AddtionalDigitMapList>
		<AuthTimer>10</AuthTimer>
		<SharedBasNumber>101</SharedBasNumber>
		<SharedPriNumber>523</SharedPriNumber>
		<MgID></MgID>
		<EnnableMD5DigAuteh>false</EnnableMD5DigAuteh>
		<ChannelAudits>Arming Modify and AuditValue done one after another sequentially</ChannelAudits>
		<GroupId>1</GroupId>
		<AgcfGateWayType>RGW/iAD</AgcfGateWayType>
	</AgcfGatewayRecord>

	<HSSPrivateId>
		<PridUser>" + pridUser + @"</PridUser>
		<PrsetNumber>" + prsetNumber + @"</PrsetNumber>
		<PrivPassword>1234</PrivPassword>
		<PridDomainId>0</PridDomainId>
		<PridDomainName>ims.moc1.kw</PridDomainName>
		<ServerCapNumber>0</ServerCapNumber>
		<ProtectionGroupNumber>1</ProtectionGroupNumber>
		<ChargeInfoNum>0</ChargeInfoNum>
		<MediaProfileId>0</MediaProfileId>
		<PuidUserList>
			<HSSPublicId>
				<Puid>" + puid + @"</Puid>
				<ServiceProfileNumber>" + serviceProfileNumber + @"</ServiceProfileNumber>
				<PrivateId>" + privateId + @"</PrivateId>
				<PuidDomainName>ims.moc1.kw</PuidDomainName>
				<PridDomainName>ims.moc1.kw</PridDomainName>
				<IsWildcardPuid>false</IsWildcardPuid>
				<Barring>true</Barring>
				<ImplRegSet>1</ImplRegSet>
				<Authorized>true</Authorized>
				<SendTelURI>false</SendTelURI>
			</HSSPublicId>
		</PuidUserList>
	</HSSPrivateId>

	<HSSPrset>
		<PrsetNum>" + prsetNum + @"</PrsetNum>
		<PridUser>" + pridUser + @"</PridUser>
		<PrsetName>" + prsetName + @"</PrsetName>
		<PridDomain>ims.moc1.kw</PridDomain>
		<ServerCapNum>0</ServerCapNum>
	</HSSPrset>

	<GeoRedundancyData>
		<AgcfGatewayRecord>
			<NgcfAgcfGatewayRecordAid>" + tableId + "`" + gwId + @"</NgcfAgcfGatewayRecordAid>
			<TableId>" + tableId + @"</TableId>
			<GwId>" + gwId + @"</GwId>
            <AgcfSipIaPort>" + agcfSipIaPort + @"</AgcfSipIaPort>
			<GwUserId>" + gwUserId + @"</GwUserId>
			<GwName>" + gwName + @"</GwName>
			<IP1>" + ip1 + @"</IP1>
			<IP2>" + ip2 + @"</IP2>
     		<MateExternalIPAddr>" + mgcSecondaryIp + @"</MateExternalIPAddr>
			<ConnectionType>UDP/IP</ConnectionType>
			<ContextAudits>Audit Value Based</ContextAudits>
			<GwDigitMapId>1</GwDigitMapId>
			<EnableChannelStatusAudits>false</EnableChannelStatusAudits>
			<GwDomain>ims.moc1.kw</GwDomain>
			<GwPrivId></GwPrivId>
			<LocalTermTypePrefix>td</LocalTermTypePrefix>
			<LocalTermTypeAnalogPrefix>td</LocalTermTypeAnalogPrefix>
			<IsLocalTermTypeTDMAnalog>true</IsLocalTermTypeTDMAnalog>
			<NetTermTypePrefix>RTP</NetTermTypePrefix>
			<IsNetTermTypeRTPUDP>true</IsNetTermTypeRTPUDP>
			<PhysicalTermIdScheme>Flat Term ID</PhysicalTermIdScheme>
			<SendCompactMessages>true</SendCompactMessages>
			<SendEphemeralPrefix>false</SendEphemeralPrefix>
			<UdpPort>2944</UdpPort>
			<GwVariantId>1</GwVariantId>
			<AreaId>0</AreaId>
			<ComfortNoise>false</ComfortNoise>
			<Dtmf>false</Dtmf>
			<FaxEvents>false</FaxEvents>
			<ModemEvents>false</ModemEvents>
			<TextTelephonyEvents>false</TextTelephonyEvents>
			<AllCodecDataStr>Audio|G.711 - uLaw|20|false</AllCodecDataStr>
			<AgwIuaIpAddress></AgwIuaIpAddress>
			<AgwIuaSctpPort></AgwIuaSctpPort>
			<AgcfLocalSctpPort></AgcfLocalSctpPort>
			<SctpProfile></SctpProfile>
			<IuaIIDMapScheme></IuaIIDMapScheme>
			<LocalTermTypeIsdnPrefix></LocalTermTypeIsdnPrefix>
			<AuditAllActiveIsdnCalls>false</AuditAllActiveIsdnCalls>
			<MateSite>2</MateSite>
			<IsPrimary>false</IsPrimary>
			<AddtionalDigitMapList></AddtionalDigitMapList>
			<AuthTimer>10</AuthTimer>
			<SharedBasNumber>101</SharedBasNumber>
			<SharedPriNumber>523</SharedPriNumber>
			<MgID></MgID>
			<EnnableMD5DigAuteh>false</EnnableMD5DigAuteh>
			<ChannelAudits>Arming Modify and AuditValue done one after another sequentially</ChannelAudits>
			<GroupId>1</GroupId>
			<AgcfGateWayType>RGW/iAD</AgcfGateWayType>
		</AgcfGatewayRecord>
		<HSSPrivateId>
			<PridUser>" + pridUser + @"</PridUser>
			<PrsetNumber>" + prsetNumber + @"</PrsetNumber>
			<PrivPassword>1234</PrivPassword>
			<PridDomainId>0</PridDomainId>
			<PridDomainName>ims.moc1.kw</PridDomainName>
			<ServerCapNumber>0</ServerCapNumber>
			<ProtectionGroupNumber>2</ProtectionGroupNumber>
			<ChargeInfoNum>0</ChargeInfoNum>
			<MediaProfileId>0</MediaProfileId>
			<PuidUserList>
				<HSSPublicId>
					<Puid>" + puid + @"</Puid>
					<PrivateId>" + privateId + @"</PrivateId>
                    <ServiceProfileNumber>" + serviceProfileNumber + @"</ServiceProfileNumber>
					<PuidDomainName>ims.moc1.kw</PuidDomainName>
					<PridDomainName>ims.moc1.kw</PridDomainName>
					<IsWildcardPuid>false</IsWildcardPuid>
					<Barring>true</Barring>
					<ImplRegSet>1</ImplRegSet>
					<Authorized>true</Authorized>
					<SendTelURI>false</SendTelURI>
				</HSSPublicId>
			</PuidUserList>
		</HSSPrivateId>
		<HSSPrset>
			<PrsetNum>" + prsetNum + @"</PrsetNum>
			<PridUser>" + pridUser + @"</PridUser>
			<PrsetName>" + prsetName + @"</PrsetName>
			<PridDomain>ims.moc1.kw</PridDomain>
			<ServerCapNum>0</ServerCapNum>
		</HSSPrset>
	</GeoRedundancyData>";

            soapEnvelopeXml = SoapEnvelopeXml("ent-ngfs-agcfgwcombinedrec-v2", xmlContent, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        /// </remarks>
        /// </summary>
        private static XmlDocument DltNgfsAgcfGwCombinedRecV2SoapEnvelopeXml(int tableId, int gwId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId)
        {
            string xmlContent;
            XmlDocument soapEnvelopeXml;

            xmlContent = @"
	<AgcfGatewayRecord>
		<NgcfAgcfGatewayRecordAid>" + tableId + "`" + gwId + @"</NgcfAgcfGatewayRecordAid>
		<TableId>" + tableId + @"</TableId>
		<GwId>" + gwId + @"</GwId>
	</AgcfGatewayRecord>";

            soapEnvelopeXml = SoapEnvelopeXml("dlt-ngfs-agcfgwcombinedrec-v2", xmlContent, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        ///   <PlexViewRequest Command="rtrv-ngfs-agcfgatewayrecord-v2" SwitchName="TECICS01" RequestId="" SessionId="ngnAPI:1086840178" MaxRows="-1">
        ///     <NgcfAgcfGatewayRecordAid>6`2101</NgcfAgcfGatewayRecordAid>
        ///   </PlexViewRequest>
        /// </remarks>
        /// </summary>
        private static XmlDocument RtrvNgcfAgcfGatewayRecordV2SoapEnvelopeXml(int tableId, int gwId, string sessionId)
        {
            XmlDocument soapEnvelopeXml;
            Dictionary<string, string> param;

            param = new Dictionary<string, string>();

            param.Add("NgcfAgcfGatewayRecordAid", tableId + "`" + gwId);

            soapEnvelopeXml = SoapEnvelopeXml("rtrv-ngfs-agcfgatewayrecord-v2", param, sessionId, "", Ia.Ngn.Cl.Model.Business.Nokia.Ims.AnyEdgeRouter);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        ///   <PlexViewRequest Command="dlt-ngfs-agcfgatewayrecord-v2" SwitchName="TECICS01" RequestId="" SessionId="ngnAPI:1086840178" MaxRows="-1">
        ///     <NgcfAgcfGatewayRecordAid>6`2101</NgcfAgcfGatewayRecordAid>
        ///   </PlexViewRequest>
        /// </remarks>
        /// </summary>
        private static XmlDocument DltNgcfAgcfGatewayRecordV2SoapEnvelopeXml(int tableId, int gwId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId)
        {
            XmlDocument soapEnvelopeXml;
            Dictionary<string, string> param;

            param = new Dictionary<string, string>();

            param.Add("NgcfAgcfGatewayRecordAid", tableId + "`" + gwId);

            soapEnvelopeXml = SoapEnvelopeXml("dlt-ngfs-agcfgatewayrecord-v2", param, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        /// </remarks>
        /// </summary>
        private static XmlDocument EntNgfsAgcfEndpointV2SoapEnvelopeXml(string service, int gwId, int flatTermId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId)
        {
            string xmlContent, prividUser, dn;
            XmlDocument soapEnvelopeXml;

            prividUser = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PrividUser(service);
            dn = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Dn(service);

            xmlContent = @"
	<PrividUser>" + prividUser + @"</PrividUser>
	<GwId>" + gwId + @"</GwId>
	<Dn>" + dn + @"</Dn>
	<FlatTermID>" + flatTermId + @"</FlatTermID>
	<Slot>0</Slot>
	<Port>0</Port>
	<AdditionalDNs></AdditionalDNs>
	<DomainIndex>0</DomainIndex>
	<FeatureFlag>0</FeatureFlag>
	<DslamId></DslamId>
	<Rack>-1</Rack>
	<Shelf>0</Shelf>
	<SubscriberType>Analog</SubscriberType>
	<ReversePolarity>UPON_DIGIT_COLLECTION_ALWAYS</ReversePolarity>
	<PayphoneMetering>PAYPHONE_PULSE_METERING_16_KHZ</PayphoneMetering>
	<DigitMap1st>0</DigitMap1st>
	<DigitMap2nd>0</DigitMap2nd>
	<DialTone2nd>0</DialTone2nd>
	<CallHoldLc>true</CallHoldLc>
	<CallWaitingLc>false</CallWaitingLc>
	<CallToggleLc>true</CallToggleLc>
	<ThreeWayCallLc>true</ThreeWayCallLc>
	<McidLc>false</McidLc>
	<CallTransferLc>false</CallTransferLc>
";

            soapEnvelopeXml = SoapEnvelopeXml("ent-ngfs-agcfendpoint-v2", xmlContent, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static XmlDocument EdNgfsAgcfEndpointV2SoapEnvelopeXml_CallWaiting(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, bool serviceAssigned, string sessionId)
        {
            string xmlContent, prividUser, value;
            XmlDocument soapEnvelopeXml;

            prividUser = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PrividUser(service);
            value = Convert.ToString(serviceAssigned).ToLower();

            xmlContent = @"
	<PrividUser>" + prividUser + @"</PrividUser>
	<CallWaitingLc>" + value + @"</CallWaitingLc>
";

            soapEnvelopeXml = SoapEnvelopeXml("ed-ngfs-agcfendpoint-v2", xmlContent, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        ///   <PlexViewRequest Command="rtrv-ngfs-agcfendpoint-v2" SwitchName="TECICS01" Fsdb="fsdb0" RequestId="" SessionId="ngnAPI:1946199540" MaxRows="-1">
        ///   <PrividUser>priv_96522239100</PrividUser>
        ///   </PlexViewRequest>
        /// </remarks>
        /// </summary>
        private static XmlDocument RtrvNgfsAgcfEndpointV2SoapEnvelopeXml(string prividUser, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId)
        {
            XmlDocument soapEnvelopeXml;
            Dictionary<string, string> param;

            param = new Dictionary<string, string>();

            param.Add("PrividUser", prividUser);

            soapEnvelopeXml = SoapEnvelopeXml("rtrv-ngfs-agcfendpoint-v2", param, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        /// <PlexViewRequest Command="rtrv-ngfs-agcfendpoint-v2" SwitchName="TECICS01" Fsdb="fsdb2" RequestId="" SessionId="ngnAPI:1313903573" MaxRows="4">
        ///   <filter>
        ///     <search>
        ///       <searchby>GwId</searchby>
        ///       <searchstring>2463</searchstring>
        ///       <operation>Equals</operation>
        ///       <caseSensitive>false</caseSensitive>
        ///     </search>
        ///   </filter>
        /// </PlexViewRequest>
        /// </remarks>
        /// </summary>
        private static XmlDocument RtrvNgfsAgcfEndpointV2SoapEnvelopeXml(int gwId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId)
        {
            string xmlContent;
            XmlDocument soapEnvelopeXml;

            xmlContent = @"
	<filter>
		<search>
			<searchby>GwId</searchby>
			<searchstring>" + gwId + @"</searchstring>
			<operation>Equals</operation>
			<caseSensitive>false</caseSensitive>
		</search>
	</filter>
";

            soapEnvelopeXml = SoapEnvelopeXml("rtrv-ngfs-agcfendpoint-v2", xmlContent, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        /// <PlexViewRequest Command="rtrv-ngfs-agcfendpoint-v2" SwitchName="TECICS01" Fsdb="fsdb2" RequestId="" SessionId="ngnAPI:1313903573" MaxRows="4">
        ///   <filter>
        ///     <search>
        ///       <searchby>GwId</searchby>
        ///       <searchstring>2463</searchstring>
        ///       <operation>Equals</operation>
        ///       <caseSensitive>false</caseSensitive>
        ///     </search>
        ///   </filter>
        /// </PlexViewRequest>
        /// </remarks>
        /// </summary>
        private static XmlDocument RtrvNgfsAgcfEndpointV2SoapEnvelopeXml(int gwId, string edgeRouter, int imsService, string sessionId)
        {
            string xmlContent, imsFsdb;
            XmlDocument soapEnvelopeXml;

            imsFsdb = Ia.Ngn.Cl.Model.Business.Nokia.Ims.ImsFsdb(imsService);

            xmlContent = @"
	<filter>
		<search>
			<searchby>GwId</searchby>
			<searchstring>" + gwId + @"</searchstring>
			<operation>Equals</operation>
			<caseSensitive>false</caseSensitive>
		</search>
	</filter>
";

            soapEnvelopeXml = SoapEnvelopeXml("rtrv-ngfs-agcfendpoint-v2", xmlContent, sessionId, "", edgeRouter, imsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        ///   <PlexViewRequest Command="dlt-ngfs-agcfendpoint-v2" SwitchName="TECICS01" Fsdb="fsdb0" RequestId="" SessionId="ngnAPI:1946199540" MaxRows="-1">
        ///   <PrividUser>priv_96522239100</PrividUser>
        ///   </PlexViewRequest>
        /// </remarks>
        /// </summary>
        private static XmlDocument DltNgfsAgcfEndpointV2SoapEnvelopeXml(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId)
        {
            string prividUser;
            XmlDocument soapEnvelopeXml;
            Dictionary<string, string> param;

            prividUser = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PridUser(service);

            param = new Dictionary<string, string>();

            param.Add("PrividUser", prividUser);

            soapEnvelopeXml = SoapEnvelopeXml("dlt-ngfs-agcfendpoint-v2", param, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        /// </remarks>
        /// </summary>
        private static XmlDocument EntNgfsSubPartyV2SoapEnvelopeXml(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId)
        {
            string xmlContent, partyId, primaryPuid, alternateFsdbFqdn, assocOtasRealm, alternateOtasRealm;
            XmlDocument soapEnvelopeXml;

            partyId = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(service);
            primaryPuid = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PrimaryPuid(service);

            assocOtasRealm = Ia.Ngn.Cl.Model.Business.Nokia.Ims.AssocOtasRealm(nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsService);
            alternateOtasRealm = Ia.Ngn.Cl.Model.Business.Nokia.Ims.AlternateOtasRealm(nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsService);

            alternateFsdbFqdn = Ia.Ngn.Cl.Model.Business.Nokia.Ims.AlternateFsdbFqdn(nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            xmlContent = @"
	<PartyId>" + partyId + @"</PartyId>
	<PrimaryPUID>" + primaryPuid + @"</PrimaryPUID>
	<DisplayName>" + service + @"</DisplayName>
    <AssocOtasRealm>" + assocOtasRealm + @"</AssocOtasRealm>
    <AlternateFsdbFqdn>" + alternateFsdbFqdn + @"</AlternateFsdbFqdn>
	<Category>RESIDENTIALSUBSCRIBER_R2</Category>
	<PrimaryPUIDDomainRequired>false</PrimaryPUIDDomainRequired>
	<PrimaryPUIDCPEProfileNumber>0</PrimaryPUIDCPEProfileNumber>
	<PrimaryPUIDFlashable>false</PrimaryPUIDFlashable>
	<NetworkProfileName></NetworkProfileName>
	<NetworkProfileVersion>0</NetworkProfileVersion>
	<ServiceProfileName></ServiceProfileName>
	<ServiceProfileVersion>0</ServiceProfileVersion>
	<IsReducedServiceProfile>false</IsReducedServiceProfile>
	<CallLimit>2</CallLimit>
	<ServiceSuspension>false</ServiceSuspension>
	<OriginationSuspension>false</OriginationSuspension>
	<TerminationSuspension>false</TerminationSuspension>
	<SuspensionNotification>false</SuspensionNotification>
	<UserOrigSuspension>false</UserOrigSuspension>
	<UserTermSuspension>false</UserTermSuspension>
	<AssocWpifRealm></AssocWpifRealm>
	<IddPrefix></IddPrefix>
	<SharedHssData>false</SharedHssData>
	<Pin></Pin>
	<MsnCapability>false</MsnCapability>
	<VideoProhibit>false</VideoProhibit>
	<MaxFwdHops>10</MaxFwdHops>
	<CsdFlavor>TAS_CSD_NONE</CsdFlavor>
	<CsdDynamic>false</CsdDynamic>
	<SipErrorTableId>0</SipErrorTableId>
	<TreatmentTableId>0</TreatmentTableId>
	<Locale></Locale>
	<CliPrefixList></CliPrefixList>
	<IsGroupCPE>false</IsGroupCPE>
	<Receive181Mode>TAS_181_NONE</Receive181Mode>
	<CcNdcLength>0</CcNdcLength>
	<MaxActiveCalls>0</MaxActiveCalls>
	<CallingPartyCategory>CPC_ORDINARY</CallingPartyCategory>
	<PublicUID1></PublicUID1>
	<PublicUID2></PublicUID2>
	<PublicUID3></PublicUID3>
	<PublicUID4></PublicUID4>
	<PublicUID5></PublicUID5>
	<PublicUID6></PublicUID6>
	<PublicUID7></PublicUID7>
	<PublicUID8></PublicUID8>
	<PublicUID9></PublicUID9>
	<PublicUID1DomainRequired>false</PublicUID1DomainRequired>
	<PublicUID2DomainRequired>false</PublicUID2DomainRequired>
	<PublicUID3DomainRequired>false</PublicUID3DomainRequired>
	<PublicUID4DomainRequired>false</PublicUID4DomainRequired>
	<PublicUID5DomainRequired>false</PublicUID5DomainRequired>
	<PublicUID6DomainRequired>false</PublicUID6DomainRequired>
	<PublicUID7DomainRequired>false</PublicUID7DomainRequired>
	<PublicUID8DomainRequired>false</PublicUID8DomainRequired>
	<PublicUID9DomainRequired>false</PublicUID9DomainRequired>
	<WildCardPUIDStr></WildCardPUIDStr>
	<AllowCustomAnnouncement>false</AllowCustomAnnouncement>
	<PtySpareLong1>0</PtySpareLong1>
	<PtySpareString></PtySpareString>
	<PtySpareString2></PtySpareString2>
	<PtySpareShort1>0</PtySpareShort1>
	<PtySpareShort2>0</PtySpareShort2>
	<PtySpareBool1>false</PtySpareBool1>
	<PtySpareBool2>false</PtySpareBool2>
	<PtySpareBool3>false</PtySpareBool3>
	<PtySpareBool4>false</PtySpareBool4>
	<PtySpareBool5>false</PtySpareBool5>
	<PtySpareBool6>false</PtySpareBool6>
	<PtySpareBool7>false</PtySpareBool7>
	<PtySpareBool8>false</PtySpareBool8>
	<TerminatingTableId>0</TerminatingTableId>
	<AllowNonSipTelUri>false</AllowNonSipTelUri>
	<LocationType>None</LocationType>
	<RncID></RncID>
	<LteMcc></LteMcc>
	<LteMnc></LteMnc>
	<LteTac></LteTac>
	<MarketSID></MarketSID>
	<SwitchNumber></SwitchNumber>
	<CallsToWebUserProhibited>false</CallsToWebUserProhibited>
	<IMSI></IMSI>
	<IMSNotSupported>false</IMSNotSupported>
	<ValidateCellID>false</ValidateCellID>
	<OperatorID>0</OperatorID>
	<HomeMTA>0</HomeMTA>
	<ForwardDenyNumbers>false</ForwardDenyNumbers>
	<PlayAnnoFailNotForward>false</PlayAnnoFailNotForward>
	<MrfPoolID>0</MrfPoolID>
	<Custom120x>false</Custom120x>
";

            soapEnvelopeXml = SoapEnvelopeXml("ent-ngfs-subparty-v2", xmlContent, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        ///   <PlexViewRequest Command="rtrv-ngfs-subscriber-v2" SwitchName="TECICS01" Fsdb="fsdb0" RequestId="" SessionId="ngnAPI:1642424089" MaxRows="-1">
        ///   	<SubParty>
        ///   			<PartyId>+96522239100</PartyId>
        ///     </SubParty>
        ///   </PlexViewRequest>
        /// </remarks>
        /// </summary>
        private static XmlDocument RtrvNgfsSubPartyV2SoapEnvelopeXml(string partyId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId)
        {
            string xmlContent;
            XmlDocument soapEnvelopeXml;

            xmlContent = @"
	<Aid>" + partyId + @"</Aid>
";

            soapEnvelopeXml = SoapEnvelopeXml("rtrv-ngfs-subparty-v2", xmlContent, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        ///   <PlexViewRequest Command="rtrv-ngfs-subscriber-v2" SwitchName="TECICS01" Fsdb="fsdb0" RequestId="" SessionId="ngnAPI:1642424089" MaxRows="-1">
        ///   	<SubParty>
        ///   			<PartyId>+96522239100</PartyId>
        ///     </SubParty>
        ///   </PlexViewRequest>
        /// </remarks>
        /// </summary>
        private static XmlDocument RtrvNgfsSubPartyV2SoapEnvelopeXml(string partyId, string edgeRouter, int imsService, string sessionId)
        {
            string xmlContent, imsFsdb;
            XmlDocument soapEnvelopeXml;

            imsFsdb = Ia.Ngn.Cl.Model.Business.Nokia.Ims.ImsFsdb(imsService);

            xmlContent = @"
	<Aid>" + partyId + @"</Aid>
";

            soapEnvelopeXml = SoapEnvelopeXml("rtrv-ngfs-subparty-v2", xmlContent, sessionId, "", edgeRouter, imsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        ///   <PlexViewRequest Command="dlt-ngfs-subparty-v2" SwitchName="TECICS01" RequestId="" SessionId="ngnAPI:1086840178" MaxRows="-1">
        ///     <PartyId>+96522239100</PartyId>
        ///   </PlexViewRequest>
        /// </remarks>
        /// </summary>
        private static XmlDocument DltNgfsSubPartyV2SoapEnvelopeXml(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId)
        {
            string xmlContent, partyId;
            XmlDocument soapEnvelopeXml;

            partyId = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(service);

            xmlContent = @"
	<Aid>" + partyId + @"</Aid>
";

            soapEnvelopeXml = SoapEnvelopeXml("dlt-ngfs-subparty-v2", xmlContent, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        /// </remarks>
        /// </summary>
        private static XmlDocument EntNgfsSubscriberV2SoapEnvelopeXml(string service, int gwId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId)
        {
            int prsetNumber, serviceProfileNumber;
            string xmlContent, partyId, primaryPuid, domainName, alternateFsdbFqdn, assocOtasRealm, alternateOtasRealm;
            XmlDocument soapEnvelopeXml;

            prsetNumber = gwId;
            domainName = "ims.moc1.kw";

            partyId = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(service);
            primaryPuid = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PrimaryPuid(service);

            serviceProfileNumber = Ia.Ngn.Cl.Model.Business.Nokia.Ims.ServiceProfileNumber(nddOnt.Pon.Olt.ImsService);

            assocOtasRealm = Ia.Ngn.Cl.Model.Business.Nokia.Ims.AssocOtasRealm(nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsService);
            alternateOtasRealm = Ia.Ngn.Cl.Model.Business.Nokia.Ims.AlternateOtasRealm(nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsService);

            alternateFsdbFqdn = Ia.Ngn.Cl.Model.Business.Nokia.Ims.AlternateFsdbFqdn(nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            /*
<SubParty>
	<PartyId>" + partyId + @"</PartyId>
	<PrimaryPUID>" + primaryPuid + @"</PrimaryPUID>
    <AssocOtasRealm>" + assocOtasRealm + @"</AssocOtasRealm>
	<Category>RESIDENTIALSUBSCRIBER_R2</Category>
</SubParty>
            */

            xmlContent = @"	
    <SubParty>
    	<PartyId>" + partyId + @"</PartyId>
    	<PrimaryPUID>" + primaryPuid + @"</PrimaryPUID>
    	<DisplayName>" + service + @"</DisplayName>
        <AssocOtasRealm>" + assocOtasRealm + @"</AssocOtasRealm>
        <AlternateFsdbFqdn>" + alternateFsdbFqdn + @"</AlternateFsdbFqdn>
        <PrimaryPUIDCPEProfileNumber>35</PrimaryPUIDCPEProfileNumber>
    	<Category>RESIDENTIALSUBSCRIBER_R2</Category>
		<CallLimit>3</CallLimit>
		<MaxFwdHops>5</MaxFwdHops>
		<MaxActiveCalls>3</MaxActiveCalls>
		<CliPrefixList>965</CliPrefixList>
    </SubParty>
    <AlternateOtasRealm>" + alternateOtasRealm + @"</AlternateOtasRealm>"
                                              + DialingPlanXml(service)
                                              + HSSPrivateIdXml(service, prsetNumber, domainName, serviceProfileNumber)
                                              + GeoRedundancyDataXml(service, prsetNumber, domainName, serviceProfileNumber)
                                              ;

            soapEnvelopeXml = SoapEnvelopeXml("ent-ngfs-subscriber-v2", xmlContent, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        /// </remarks>
        /// </summary>
        private static XmlDocument EdNgfsSubscriberV2SoapEnvelopeXml(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId, string xml)
        {
            string partyId, xmlContent;
            XmlDocument soapEnvelopeXml;

            partyId = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(service);

            xmlContent = @"
	<SubParty>
		<PartyId>" + partyId + @"</PartyId>
	</SubParty>
"
                                              + xml
                                              ;

            soapEnvelopeXml = SoapEnvelopeXml("ed-ngfs-subscriber-v2", xmlContent, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        /// </remarks>
        /// </summary>
        private static XmlDocument EdNgfsSubscriberV2WithinSubPartySoapEnvelopeXml(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId, string xml)
        {
            string partyId, xmlContent;
            XmlDocument soapEnvelopeXml;

            partyId = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(service);

            xmlContent = @"
	<SubParty>
		<PartyId>" + partyId + @"</PartyId>"
                   + xml + @"
	</SubParty>";

            soapEnvelopeXml = SoapEnvelopeXml("ed-ngfs-subscriber-v2", xmlContent, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        /// <PlexViewRequest Command="rtrv-ngfs-subscriber-v2" SwitchName="TECICS01" RequestId="" SessionId="ngnAPI:148964391" MaxRows="-1">
        ///  <aid>+96522239501</aid>
        /// </PlexViewRequest>
        /// </remarks>
        /// </summary>
        private static XmlDocument RtrvNgfsSubscriberV2SoapEnvelopeXml(string partyId, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId)
        {
            string xmlContent;
            XmlDocument soapEnvelopeXml;

            xmlContent = @"
	<SubParty>
		<PartyId>" + partyId + @"</PartyId>
	</SubParty>
";

            soapEnvelopeXml = SoapEnvelopeXml("rtrv-ngfs-subscriber-v2", xmlContent, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        /// <PlexViewRequest Command="rtrv-ngfs-subscriber-v2" SwitchName="TECICS01" RequestId="" SessionId="ngnAPI:148964391" MaxRows="-1">
        ///  <aid>+96522239501</aid>
        /// </PlexViewRequest>
        /// </remarks>
        /// </summary>
        private static XmlDocument RtrvNgfsSubscriberV2SoapEnvelopeXml(string partyId, string edgeRouter, int imsService, string sessionId)
        {
            string xmlContent, imsFsdb;
            XmlDocument soapEnvelopeXml;

            imsFsdb = Ia.Ngn.Cl.Model.Business.Nokia.Ims.ImsFsdb(imsService);

            xmlContent = @"
	<SubParty>
		<PartyId>" + partyId + @"</PartyId>
	</SubParty>
";

            soapEnvelopeXml = SoapEnvelopeXml("rtrv-ngfs-subscriber-v2", xmlContent, sessionId, "", edgeRouter, imsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>
        ///   <PlexViewRequest Command="dlt-ngfs-subscriber-v2" SwitchName="TECICS01" RequestId="" SessionId="ngnAPI:1086840178" MaxRows="-1">
        ///   <SubParty>
        ///     <PartyId>+96522239100</PartyId>
        ///    </SubParty>
        ///   </PlexViewRequest>
        /// </remarks>
        /// </summary>
        private static XmlDocument DltNgfsSubscriberV2SoapEnvelopeXml(string service, Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt, string sessionId)
        {
            string partyId, xmlContent;
            XmlDocument soapEnvelopeXml;

            partyId = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(service);

            xmlContent = @"
	<SubParty>
		<PartyId>" + partyId + @"</PartyId>
	</SubParty>
";

            soapEnvelopeXml = SoapEnvelopeXml("dlt-ngfs-subscriber-v2", xmlContent, sessionId, "", nddOnt.Pon.Olt.EdgeRouter, nddOnt.Pon.Olt.ImsFsdb);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////







        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// DialingPlan XML
        /// <param name="publicUid">+96522239100@ims.moc1.kw</param>
        /// </summary>
        private static string DialingPlanXml(string service)
        {
            string publicUid, xml;

            publicUid = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PublicUid(service);

            xml = @"
	<DialingPlan>
	<PublicUID>" + publicUid + @"</PublicUID>
	<Assigned>true</Assigned>
	<PerPuid>false</PerPuid>
	<PrefixandFeatureCode>1000</PrefixandFeatureCode>
	<E164NormAndCodeConv>1001</E164NormAndCodeConv>
		<CallBarringLocal>0</CallBarringLocal>
		<ESRN1></ESRN1>
		<ESRN2></ESRN2>
		<ESRN3></ESRN3>
		<ESRN4></ESRN4>
		<ESRN5></ESRN5>
		<PrivateDialingPlan>0</PrivateDialingPlan>
	</DialingPlan>
";
            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// SetTZPath XML
        /// <param name="publicUid">+96522239100@ims.moc1.kw</param>
        /// </summary>
        private static string SetTZPathXml(string publicUid)
        {
            string xml;

            xml = @"
	<SetTZPath>
		<PublicUID>" + publicUid + @"</PublicUID>
		<Assigned>true</Assigned>
		<PerPuid>false</PerPuid>
		<TZPath>Asia/Kuwait</TZPath>
	</SetTZPath>
";
            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// HSSPrivateId XML
        /// <param name="service">22239100</param>
        /// <param name="prsetNumber">2101</param>
        /// <param name="domainName">ims.moc1.kw</param>
        /// </summary>
        private static string HSSPrivateIdXml(string service, int prsetNumber, string domainName, int serviceProfileNumber)
        {
            string xml, pridUser, puid;

            pridUser = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PridUser(service);
            puid = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Puid(service);

            // <PridDomainId>0</PridDomainId>

            xml = @"
	<HSSPrivateId>
		<PridDomainName>" + domainName + @"</PridDomainName>
		<PridUser>" + pridUser + @"</PridUser>
		<PrsetNumber>" + prsetNumber + @"</PrsetNumber>
		<PrivPassword>1234</PrivPassword>
		<ServerCapNumber>0</ServerCapNumber>
		<ProtectionGroupNumber>0</ProtectionGroupNumber>
		<ChargeInfoNum>0</ChargeInfoNum>
		<MediaProfileId>0</MediaProfileId>
		<PuidUserList>
			<HSSPublicId>
				<PuidDomainName>" + domainName + @"</PuidDomainName>
				<PridDomainName>" + domainName + @"</PridDomainName>
				<PrivateId>" + pridUser + @"</PrivateId>
				<Puid>" + puid + @"</Puid>
				<IsWildcardPuid>false</IsWildcardPuid>
				<Barring>false</Barring>
				<ImplRegSet>1</ImplRegSet>
                <ServiceProfileNumber>" + serviceProfileNumber + @"</ServiceProfileNumber>
				<Authorized>true</Authorized>
				<SendTelURI>true</SendTelURI>
			</HSSPublicId>
		</PuidUserList>
	</HSSPrivateId>
";
            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// GeoRedundancyData XML
        /// <param name="service">22239100</param>
        /// <param name="prsetNumber">2101</param>
        /// <param name="domainName">ims.moc1.kw</param>
        /// </summary>
        private static string GeoRedundancyDataXml(string service, int prsetNumber, string domainName, int serviceProfileNumber)
        {
            string xml, pridUser, puid;

            pridUser = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PridUser(service);
            puid = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Puid(service);

            xml = @"
	<GeoRedundancyData>
		<HSSPrivateId>
			<PridDomainName>" + domainName + @"</PridDomainName>
			<PridUser>" + pridUser + @"</PridUser>
			<PrsetNumber>" + prsetNumber + @"</PrsetNumber>
			<PrivPassword>1234</PrivPassword>
			<ServerCapNumber>0</ServerCapNumber>
			<ProtectionGroupNumber>0</ProtectionGroupNumber>
			<ChargeInfoNum>0</ChargeInfoNum>
			<MediaProfileId>0</MediaProfileId>
			<PuidUserList>
				<HSSPublicId>
					<PuidDomainName>" + domainName + @"</PuidDomainName>
					<PridDomainName>" + domainName + @"</PridDomainName>
					<PrivateId>" + pridUser + @"</PrivateId>
					<Puid>" + puid + @"</Puid>
					<IsWildcardPuid>false</IsWildcardPuid>
					<Barring>false</Barring>
					<ImplRegSet>1</ImplRegSet>
                    <ServiceProfileNumber>" + serviceProfileNumber + @"</ServiceProfileNumber>
					<Authorized>true</Authorized>
					<SendTelURI>true</SendTelURI>
				</HSSPublicId>
			</PuidUserList>
		</HSSPrivateId>
	</GeoRedundancyData>
";
            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// OneDigitSpeedDial XML
        /// <param name="publicUid">+96522239100@ims.moc1.kw</param>
        /// </summary>
        private static string OneDigitSpeedDialXml(string publicUid)
        {
            string xml;

            xml = @"
	<OneDigitSpeedDial>
		<PublicUID>" + publicUid + @"</PublicUID>
		<Assigned>true</Assigned>
		<PerPuid>false</PerPuid>
		<DialCodesEntries>2^^^^^^^</DialCodesEntries>
		<DNEntries>22334455^^^^^^^</DNEntries>
	</OneDigitSpeedDial>
";
            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// CallingLineId XML
        /// <param name="service">22239100</param>
        /// <param name="serviceAssigned">true</param>
        /// </summary>
        private static string CallingLineIdXml(string service, bool serviceAssigned)
        {
            string publicUid, xml, value;

            publicUid = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PublicUid(service);

            value = Convert.ToString(serviceAssigned).ToLower();

            xml = @"
	<CallingLineId>
		<PublicUID>" + publicUid + @"</PublicUID>
		<CallingLineIdPresentation>" + value + @"</CallingLineIdPresentation>
		<ConnectedLinePresentation>" + value + @"</ConnectedLinePresentation>
		<Assigned>true</Assigned>
		<PerPuid>false</PerPuid>
		<CallingLineIdRestriction>PERM_PUBLIC</CallingLineIdRestriction>
		<CLIREditAllowed>false</CLIREditAllowed>
		<CallingNamePresentation>false</CallingNamePresentation>
		<RestrictionOverride>false</RestrictionOverride>
		<ConnectedLineRestriction>false</ConnectedLineRestriction>
		<ConnectedLineRestrictionOverride>false</ConnectedLineRestrictionOverride>
		<CallingNumScreen>0</CallingNumScreen>
		<ConnectedNumScreen>0</ConnectedNumScreen>
		<PDPExtensionDisplay>false</PDPExtensionDisplay>
		<COLREditAllowed>false</COLREditAllowed>
		<OrigLineIdRestrictionLevel>NONE</OrigLineIdRestrictionLevel>
		<OIPEditAllowed>false</OIPEditAllowed>
		<BlockPerCallOverride>false</BlockPerCallOverride>
		<SuppressCLIPonCallWaiting>false</SuppressCLIPonCallWaiting>
		<QueCalNameSer>OMITTED</QueCalNameSer>
		<Typ2CanSer>false</Typ2CanSer>
		<CalNameSerUrl>0</CalNameSerUrl>
		<CompanyNameQuery>false</CompanyNameQuery>
		<OrigCallingNameQuery>false</OrigCallingNameQuery>
	</CallingLineId>
";
            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// CallForwardingVari XML
        /// <param name="service">22239100</param>
        /// <param name="serviceAssigned">true</param>
        /// </summary>
        private static string CallForwardingVariXml(string service, bool serviceAssigned)
        {
            string publicUid, xml, value;

            publicUid = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PublicUid(service);

            value = Convert.ToString(serviceAssigned).ToLower();

            xml = @"
	<CallForwardingVari>
		<PublicUID>" + publicUid + @"</PublicUID>
		<Assigned>" + value + @"</Assigned>
		<PerPuid>false</PerPuid>
		<ForwardToDN></ForwardToDN>
		<PingRing>false</PingRing>
		<ForwardToType>FORWARD_TO_DN</ForwardToType>
		<EditPermission>EDIT_FULL</EditPermission>
		<ForwardVoiceCalls>true</ForwardVoiceCalls>
		<ForwardDataCalls>true</ForwardDataCalls>
		<ReceiveNotify>false</ReceiveNotify>
		<PlayAnnouncement>false</PlayAnnouncement>
		<PinRequired>false</PinRequired>
		<Send181Mode>TAS_181_NONE</Send181Mode>
		<RestrictIdForward>false</RestrictIdForward>
		<RestrictIdBackward>false</RestrictIdBackward>
		<DataForwardToType>USE_VOICE</DataForwardToType>
		<DataForwardToDN></DataForwardToDN>
		<Activated>false</Activated>
	</CallForwardingVari>
";
            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// CallWaiting XML
        /// <param name="service">22239100</param>
        /// <param name="serviceAssigned">true</param>
        /// </summary>
        private static string CallWaitingXml(string service, bool serviceAssigned)
        {
            string publicUid, xml, value;

            publicUid = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PublicUid(service);

            value = Convert.ToString(serviceAssigned).ToLower();

            xml = @"
	<CallWaiting>
		<PublicUID>" + publicUid + @"</PublicUID>
		<Assigned>" + value + @"</Assigned>
		<PerPuid>false</PerPuid>
		<PlayAnnouncement>false</PlayAnnouncement>
		<IsAlternateCallWaiting>false</IsAlternateCallWaiting>
		<Programmable>false</Programmable>
		<Activated>false</Activated>
	</CallWaiting>
";
            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// ConferenceCalling XML
        /// <param name="service">22239100</param>
        /// <param name="serviceAssigned">true</param>
        /// </summary>
        private static string ConferenceCallingXml(string service, bool serviceAssigned)
        {
            string publicUid, xml, value;

            publicUid = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PublicUid(service);

            value = Convert.ToString(serviceAssigned).ToLower();

            xml = @"
	<ConferenceCalling>
		<PublicUID>" + publicUid + @"</PublicUID>
		<Assigned>" + value + @"</Assigned>
		<PerPuid>false</PerPuid>
		<Transfer>false</Transfer>
		<ConfSize>3</ConfSize>
	</ConferenceCalling>
";
            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// ReminderCall XML
        /// <param name="service">22239100</param>
        /// <param name="serviceAssigned">true</param>
        /// </summary>
        private static string ReminderCallXml(string service, bool serviceAssigned)
        {
            string publicUid, xml, value;

            publicUid = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PublicUid(service);

            value = Convert.ToString(serviceAssigned).ToLower();

            xml = @"
	<ReminderCall>
		<PublicUID>" + publicUid + @"</PublicUID>
		<Assigned>" + value + @"</Assigned>
		<PerPuid>false</PerPuid>
		<RetryCounter>5</RetryCounter>
		<NoAnswerTimer>0</NoAnswerTimer>
	</ReminderCall>
";
            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// OneDigitSpeedDial XML
        /// <param name="service">22239100</param>
        /// <param name="serviceAssigned">true</param>
        /// </summary>
        private static string OneDigitSpeedDial(string service, bool serviceAssigned)
        {
            string publicUid, xml, value;

            publicUid = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PublicUid(service);

            value = Convert.ToString(serviceAssigned).ToLower();

            xml = @"
	<OneDigitSpeedDial>
		<PublicUID>" + publicUid + @"</PublicUID>
		<Assigned>" + value + @"</Assigned>
		<Activated>false</Activated>
	</OneDigitSpeedDial>
";
            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// InternationalCalling XML
        /// <param name="service">22239100</param>
        /// <param name="serviceAssigned">true</param>
        /// </summary>
        private static string InternationalCallingXml(string service, bool serviceAssigned)
        {
            string publicUid, xml, value;

            publicUid = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PublicUid(service);

            value = Convert.ToString(!serviceAssigned).ToLower(); // see !

            xml = @"
	<CallBarring>
		<PublicUID>" + publicUid + @"</PublicUID>
		<International>" + value + @"</International>
		<Assigned>true</Assigned>
		<PerPuid>false</PerPuid>
		<CallBarringAll>false</CallBarringAll>
		<Local>false</Local>
		<IntraLataToll>false</IntraLataToll>
		<InterLataToll>false</InterLataToll>
		<National>false</National>
		<PremiumServ>false</PremiumServ>
		<Emergency>false</Emergency>
		<TollFree>false</TollFree>
		<FGDDialedCarrier>false</FGDDialedCarrier>
		<FGBDialedCarrier>false</FGBDialedCarrier>
		<DirectoryAssist>false</DirectoryAssist>
		<NXXCalls>false</NXXCalls>
		<User1>false</User1>
		<User2>false</User2>
		<User3>false</User3>
		<User4>false</User4>
		<User5>false</User5>
		<User6>false</User6>
		<User7>false</User7>
		<User8>false</User8>
		<User9>false</User9>
		<User10>false</User10>
		<User11>false</User11>
		<User12>false</User12>
		<User13>false</User13>
		<NonE164>false</NonE164>
		<Mobile>false</Mobile>
		<CntrlCallBarringAll>false</CntrlCallBarringAll>
		<CntrlDirectoryAssist>false</CntrlDirectoryAssist>
		<CntrlEmergency>false</CntrlEmergency>
		<CntrlFGBDialedCarrier>false</CntrlFGBDialedCarrier>
		<CntrlFGDDialedCarrier>false</CntrlFGDDialedCarrier>
		<CntrlInterLataToll>false</CntrlInterLataToll>
		<CntrlInternational>false</CntrlInternational>
		<CntrlIntraLataToll>false</CntrlIntraLataToll>
		<CntrlLocal>false</CntrlLocal>
		<CntrlNational>false</CntrlNational>
		<CntrlNXXCalls>false</CntrlNXXCalls>
		<CntrlPremiumServ>false</CntrlPremiumServ>
		<CntrlTollFree>false</CntrlTollFree>
		<CntrlUser1>false</CntrlUser1>
		<CntrlUser2>false</CntrlUser2>
		<CntrlUser3>false</CntrlUser3>
		<CntrlUser4>false</CntrlUser4>
		<CntrlUser5>false</CntrlUser5>
		<CntrlUser6>false</CntrlUser6>
		<CntrlUser7>false</CntrlUser7>
		<CntrlUser8>false</CntrlUser8>
		<CntrlUser9>false</CntrlUser9>
		<CntrlUser10>false</CntrlUser10>
		<CntrlUser11>false</CntrlUser11>
		<CntrlUser12>false</CntrlUser12>
		<CntrlUser13>false</CntrlUser13>
		<ReceiveNotify>false</ReceiveNotify>
		<CntrlUserCallLock>false</CntrlUserCallLock>
		<LockSet>0</LockSet>
		<CntrlNonE164>false</CntrlNonE164>
		<CntrlMobile>false</CntrlMobile>
	</CallBarring>";

            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// InternationalCallingUserControlled XML
        /// <param name="service">22239100</param>
        /// <param name="serviceAssigned">true</param>
        /// </summary>
        private static string InternationalCallingUserControlledXml(string service, bool serviceAssigned)
        {
            string publicUid, xml, value;

            publicUid = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PublicUid(service);

            value = Convert.ToString(serviceAssigned).ToLower();

            xml = @"
    <OutgoingCallBarring>
		<PublicUID>" + publicUid + @"</PublicUID>
        <Assigned>" + value + @"</Assigned>
        <AdminActive>false</AdminActive>
        <AdminProgram>0</AdminProgram>
        <UserCtrl>true</UserCtrl>
        <UserActive>true</UserActive>
        <UserProgram>1</UserProgram>
        <ReceiveNotify>false</ReceiveNotify>
        <PerPuid>false</PerPuid>
    </OutgoingCallBarring>";

            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// ServiceSuspension XML
        /// <param name="serviceSuspensionState"
        /// </summary>
        private static string ServiceSuspension(bool serviceSuspensionState)
        {
            string xml, value;

            value = Convert.ToString(serviceSuspensionState).ToLower();

            xml = @"
    <ServiceSuspension>" + value + @"</ServiceSuspension>
";
            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// PinService XML
        /// <param name="publicUid">+96522239100@ims.moc1.kw</param>
        /// </summary>
        private static string PinServiceXml(string publicUid)
        {
            string xml;

            xml = @"
	<PinService>
		<PublicUID>" + publicUid + @"</PublicUID>
		<Assigned>true</Assigned>
		<PerPuid>false</PerPuid>
		<Pin>0000</Pin>
		<PinFrozen>false</PinFrozen>
	</PinService>
";
            return xml;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////










        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static XmlDocument SoapEnvelopeXml(string command, string xmlContent, string sessionId, string requestId, string switchName, string fsdb)
        {
            string s;
            XmlDocument soapEnvelopeXml = new XmlDocument();

            s = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soap-env:Envelope xmlns:soap-env=""http://schemas.xmlsoap.org/soap/envelope/"">
	<soap-env:Header />
	<soap-env:Body>
		<PlexViewRequest Command=""" + command + @""" SwitchName=""" + switchName + @""" Fsdb=""" + fsdb + @""" RequestId=""" + requestId + @""" SessionId=""" + sessionId + @""" MaxRows=""32"">";

            s += xmlContent;

            s += @"
		</PlexViewRequest>
	</soap-env:Body>
</soap-env:Envelope>";

            soapEnvelopeXml.LoadXml(s);

            // below: To avoid XML shorthand closing tag you can set the IsEmpty property to false for all elements not having any child nodes:
            //foreach (XmlElement el in soapEnvelopeXml.SelectNodes("descendant::*[not(node())]")) el.IsEmpty = false;

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static XmlDocument SoapEnvelopeXml(string command, string xmlContent, string sessionId, string requestId, string switchName)
        {
            string s;
            XmlDocument soapEnvelopeXml = new XmlDocument();

            s = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soap-env:Envelope xmlns:soap-env=""http://schemas.xmlsoap.org/soap/envelope/"">
	<soap-env:Header />
	<soap-env:Body>
		<PlexViewRequest Command=""" + command + @""" SwitchName=""" + switchName + @""" RequestId=""" + requestId + @""" SessionId=""" + sessionId + @""" MaxRows=""32"">";

            s += xmlContent;

            s += @"
		</PlexViewRequest>
	</soap-env:Body>
</soap-env:Envelope>";

            soapEnvelopeXml.LoadXml(s);

            // below: To avoid XML shorthand closing tag you can set the IsEmpty property to false for all elements not having any child nodes:
            //foreach (XmlElement el in soapEnvelopeXml.SelectNodes("descendant::*[not(node())]")) el.IsEmpty = false;

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static XmlDocument SoapEnvelopeXml(string command, Dictionary<string, string> param, string sessionId, string requestId, string switchName)
        {
            string s, name, value;
            XmlDocument soapEnvelopeXml = new XmlDocument();

            s = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soap-env:Envelope xmlns:soap-env=""http://schemas.xmlsoap.org/soap/envelope/"">
	<soap-env:Header />
	<soap-env:Body>
		<PlexViewRequest Command=""" + command + @""" SwitchName=""" + switchName + @""" RequestId=""" + requestId + @""" SessionId=""" + sessionId + @""" MaxRows=""-1"">";

            if (param != null)
            {
                foreach (KeyValuePair<string, string> u in param)
                {
                    name = u.Key;
                    value = u.Value;

                    s += @"			<" + name + @">" + value + @"</" + name + @">";
                }
            }

            s += @"
		</PlexViewRequest>
	</soap-env:Body>
</soap-env:Envelope>";

            soapEnvelopeXml.LoadXml(s);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static XmlDocument SoapEnvelopeXml(string command, Dictionary<string, string> param, string sessionId, string requestId, string switchName, string fsdb)
        {
            string s, name, value;
            XmlDocument soapEnvelopeXml = new XmlDocument();

            s = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soap-env:Envelope xmlns:soap-env=""http://schemas.xmlsoap.org/soap/envelope/"">
	<soap-env:Header />
	<soap-env:Body>
		<PlexViewRequest Command=""" + command + @""" SwitchName=""" + switchName + @""" Fsdb=""" + fsdb + @""" RequestId=""" + requestId + @""" SessionId=""" + sessionId + @""" MaxRows=""-1"">";

            if (param != null)
            {
                foreach (KeyValuePair<string, string> u in param)
                {
                    name = u.Key;
                    value = u.Value;

                    s += @"			<" + name + @">" + value + @"</" + name + @">";
                }
            }

            s += @"
		</PlexViewRequest>
	</soap-env:Body>
</soap-env:Envelope>";

            soapEnvelopeXml.LoadXml(s);

            return soapEnvelopeXml;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private void SendSoapRequestAndReadResponse(XmlDocument soapEnvelopeXml, out XmlDocument soapResultXmlDocument)
        {
            string url, action;
            string soapResult;
            HttpWebRequest request;

            sessionId = "";
            soapResultXmlDocument = new XmlDocument();

            url = Ia.Ngn.Cl.Model.Data.Nokia.Ims.BaseAddress + "/" + Ia.Ngn.Cl.Model.Data.Nokia.Ims.ServiceUrl;
            action = "PlexViewRequest";

            request = CreateWebRequest(url, action);

            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(soapEnvelopeXml.OuterXml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";

            try
            {
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                    //soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                        soapResultXmlDocument.LoadXml(soapResult);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private void ParseSoapResultXml(XmlDocument soapResultXmlDocument, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode)
        {
            List<Dictionary<string, string>> parameterDictionaryList;

            ParseSoapResultXml(soapResultXmlDocument, out resultCode, out parameterDictionaryList);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private void ParseSoapResultXml(XmlDocument soapResultXmlDocument, out Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode resultCode, out List<Dictionary<string, string>> parameterDictionaryList)
        {
            string failureReason, failureCode;
            Dictionary<string, string> parameterDictionary;
            XmlNode xn;

            parameterDictionary = new Dictionary<string, string>();
            parameterDictionaryList = new List<Dictionary<string, string>>(32);

            if (soapResultXmlDocument.DocumentElement != null)
            {
                xn = soapResultXmlDocument.DocumentElement.ChildNodes[1].FirstChild;

                commandInLower = xn.Attributes["Command"].Value.ToLower();
                switchName = xn.Attributes["SwitchName"].Value;
                //fsdb = (xn.Attributes["Fsdb"] != null) ? xn.Attributes["Fsdb"].Value : null;
                requestId = xn.Attributes["RequestId"].Value;
                sessionId = xn.Attributes["SessionId"].Value;
                success = xn.Attributes["Status"].Value == "SUCCESS" ? true : false;

                if (success)
                {
                    if (commandInLower == "rtrv-ngfs-agcfendpoint-v2" || commandInLower == "dlt-ngfs-agcfendpoint-v2" ||
                        commandInLower == "rtrv-ngfs-agcfgatewayrecord-v2" || commandInLower == "dlt-ngfs-agcfgatewayrecord-v2" ||
                        commandInLower == "rtrv-ngfs-subparty-v2" || commandInLower == "dlt-ngfs-subparty-v2"
                        )
                    {
                        if (soapResultXmlDocument.DocumentElement.ChildNodes[1].FirstChild.FirstChild != null)
                        {
                            foreach (XmlNode xn2 in soapResultXmlDocument.DocumentElement.ChildNodes[1].FirstChild.ChildNodes)
                            {
                                parameterDictionary = CollectXmlNodeChildNodesValuesIntoDictionary(xn2);

                                parameterDictionaryList.Add(parameterDictionary);
                            }

                            resultCode = Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful;
                        }
                        else
                        {
                            resultCode = Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.SuccessfulButNoParameters;
                        }
                    }
                    else /*if(commandInLower == "rtrv-ngfs-subscriber-v2" || commandInLower == "dlt-ngfs-subscriber-v2" )*/
                    {
                        if (soapResultXmlDocument.DocumentElement.ChildNodes[1].FirstChild != null)
                        {
                            parameterDictionary = CollectXmlNodeChildNodesValuesIntoDictionary(soapResultXmlDocument.DocumentElement.ChildNodes[1].FirstChild);

                            parameterDictionaryList.Add(parameterDictionary);

                            resultCode = Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.Successful;
                        }
                        else
                        {
                            resultCode = Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.SuccessfulButNoParameters;
                        }
                    }

                    // below: if command is act-user we will read the sessionId from the parameters
                    if (commandInLower == "act-user") sessionId = parameterDictionary["SessionId"].ToString();
                    // below: if command is canc-user we will null the sessionId
                    else if (commandInLower == "canc-user" && success) sessionId = "";
                }
                else
                {
                    failureReason = xn.Attributes["FailureReason"].Value;
                    failureCode = xn.Attributes["FailureCode"].Value;

                    // below:
                    resultCode = (Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode)int.Parse(failureCode);
                }
            }
            else
            {
                resultCode = Ia.Ngn.Cl.Model.Client.Nokia.Ims.ResultCode.DocumentElementIsNull;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> CollectXmlNodeChildNodesValuesIntoDictionary(XmlNode xn)
        {
            Dictionary<string, string> parameterDictionary;

            parameterDictionary = new Dictionary<string, string>();

            foreach (XmlElement xe in xn.ChildNodes)
            {
                // below: if the node has children(?) we will treat it as an XDocument and add its name to a top level tag
                if (xe.ChildNodes.Count > 1) parameterDictionary.Add(xe.Name, xe.OuterXml);
                else parameterDictionary.Add(xe.Name, xe.InnerText);
            }

            return parameterDictionary;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Create a soap webrequest to [Url]
        /// </summary>
        /// <see cref="http://www.roelvanlisdonk.nl/?p=1893"/>
        /// <returns></returns>
        private HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.Headers.Add(@"SOAP:" + action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";

            return webRequest;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}