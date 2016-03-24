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
using System.Data.Entity;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ia.Ngn.Cl.Model.Business.Maintenance
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Find subscriber and network information support class for the Next Generation Network business model
    /// </summary>
    /// 
    /// <remarks> 
    /// Copyright © 2006-2015 Jasem Y. Al-Shamlan (info@ia.com.kw), Internet Applications - Kuwait. All Rights Reserved.
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
    public class Find
    {
        private static int number, oltId, rack, sub, card, port, ponNumber, ontNumber, gatewayId, block, street, premisesOld, premisesNew;
        private static long numberWithCountryCode;
        private static string numberString, ontName, ponName, partyId, prividUser, impu;
        private static Ia.Ngn.Cl.Model.Business.Service.KuwaitNgnArea kuwaitNgnArea;
        private static Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont nddOnt;
        private static List<int> serviceRequestIdList, serviceRequestOntIdList, gatewayRecordIdList, reportIdList, numberList, oltIdList;
        private static List<long> serviceLongList;
        private static List<string> serviceRequestServiceIdList, serviceIdList, impuList, accessIdList, ontIdList, agcfEndpointIdList, subPartyIdList, subscriberIdList, numberStringList, partyIdList, prividUserList, nddOntIdList, ipList;
        private static List<string> eventSystemList, eventAidPonList, eventAidOntList, eventAidOntPotsList, eventAidOntCardList, eventAidOntVoipList, eventAidOntEnetList, eventAidOntHsiList, eventAidBrgPortList, eventAidOntVdslList, eventAidOntVidolList;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Find() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static void Initialize()
        {
            serviceRequestIdList = new List<int>();
            serviceRequestOntIdList = new List<int>();
            serviceRequestServiceIdList = new List<string>();
            serviceIdList = new List<string>();
            accessIdList = new List<string>();
            ontIdList = new List<string>();
            agcfEndpointIdList = new List<string>();
            subscriberIdList = new List<string>();
            gatewayRecordIdList = new List<int>();
            reportIdList = new List<int>();
            oltIdList = new List<int>();
            ipList = new List<string>();
            gatewayRecordIdList = new List<int>();
            numberList = new List<int>();
            serviceLongList = new List<long>();
            impuList = new List<string>();
            numberStringList = new List<string>();
            partyIdList = new List<string>();
            prividUserList = new List<string>();
            nddOntIdList = new List<string>();
            subPartyIdList = new List<string>();

            eventSystemList = new List<string>();
            eventAidPonList = new List<string>();
            eventAidOntList = new List<string>();
            eventAidOntPotsList = new List<string>();
            eventAidOntCardList = new List<string>();
            eventAidOntVoipList = new List<string>();
            eventAidOntEnetList = new List<string>();
            eventAidOntHsiList = new List<string>();
            eventAidBrgPortList = new List<string>();
            eventAidOntVdslList = new List<string>();
            eventAidOntVidolList = new List<string>();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void Number(string input,
            out List<Ia.Ngn.Cl.Model.Service2> serviceList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList,
            out List<Ia.Ngn.Cl.Model.Nokia.SubParty> subPartyList,
            out List<Ia.Ngn.Cl.Model.Nokia.Subscriber> subscriberList,
            out List<Ia.Ngn.Cl.Model.Huawei.HuSbr> huSbrList,
            out List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> nddOntList,
            out List<Ia.Ngn.Cl.Model.Ont> ontList,
            out List<Ia.Ngn.Cl.Model.OntServiceVoip> ontServiceVoipList,
            out List<Ia.Ngn.Cl.Model.OntOntPots> ontOntPotsList,
            out List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList,
            out List<Ia.Ngn.Cl.Model.Event> eventList,
            out List<Ia.Ngn.Cl.Model.Access> accessList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList,
            out List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestOnt> serviceRequestOntList,
            out List<Ia.Ngn.Cl.Model.Report> reportList,
            out List<Ia.Ngn.Cl.Model.ReportHistory> reportHistoryList,
            out Ia.Cl.Model.Result result)
        {
            Initialize();

            result = new Ia.Cl.Model.Result();

            serviceList = null;

            agcfGatewayRecordList = null;
            agcfEndpointList = null;
            subPartyList = null;
            subscriberList = null;

            huSbrList = null;
            nddOntList = null;
            ontList = null;
            ontServiceVoipList = null;
            ontOntPotsList = null;
            ontServiceHsiList = null;
            eventList = null;
            accessList = null;
            serviceRequestServiceList = null;
            serviceRequestList = null;
            serviceRequestTypeList = null;
            serviceRequestOntList = null;
            reportList = null;
            reportHistoryList = null;

            // below: remove country code
            numberString = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Service(input);

            number = int.Parse(numberString);

            if (Ia.Ngn.Cl.Model.Business.Service.NumberIsWithinAllowedDomainList(number))
            {
                try
                {
                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        numberWithCountryCode = long.Parse(Ia.Ngn.Cl.Model.Data.Service.CountryCode.ToString() + number);
                        impu = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Impu(number);
                        numberString = number.ToString();
                        partyId = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(numberString);
                        prividUser = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PrividUser(numberString);

                        //serviceIdList.Add(dnString);
                        //impuIdList.Add(impu);

                        // tier 1
                        serviceRequestServiceList = (from srs in db.ServiceRequestServices where srs.Service == numberString select srs).ToList();
                        foreach (var v in serviceRequestServiceList) serviceRequestServiceIdList.Add(v.Id);
                        serviceRequestServiceIdList = serviceRequestServiceIdList.Distinct().ToList();

                        serviceRequestList = (from sr in db.ServiceRequests where sr.Number == number select sr).ToList();
                        foreach (var v in serviceRequestList) serviceRequestIdList.Add(v.Id);
                        serviceRequestIdList = serviceRequestIdList.Distinct().ToList();

                        serviceList = (from s in db.Service2s where s.Service == numberString select s).ToList();
                        foreach (var v in serviceList) serviceIdList.Add(v.Id);
                        serviceIdList = serviceIdList.Distinct().ToList();

                        subPartyList = (from sp in db.SubParties where sp.PartyId == partyId select sp)
                            .Union(from sp in db.SubParties where sp.DisplayName == numberString select sp).ToList();
                        foreach (var v in subPartyList) subPartyIdList.Add(v.Id);
                        subPartyIdList = subPartyIdList.Distinct().ToList();

                        subscriberList = (from s in db.Subscribers where s.PartyId == partyId select s).ToList();

                        huSbrList = (from h in db.HuSbrs where h.IMPU == impu select h).ToList();

                        reportList = (from r in db.Reports where r.Service == numberString select r).Include(x => x.ReportHistories).ToList();
                        foreach (var v in reportList) reportIdList.Add(v.Id);
                        reportIdList = reportIdList.Distinct().ToList();

                        agcfEndpointList = (from e in db.AgcfEndpoints where e.PrividUser == prividUser select e).ToList();
                        //foreach (var v in agcfEndpointList) agcfEndpointIdList.Add(v.Id); not needed here see below

                        // tier 2
                        serviceRequestList = serviceRequestList.Union(from sr in db.ServiceRequests join srsid in serviceRequestServiceIdList on sr.ServiceRequestService.Id equals srsid select sr).ToList();
                        foreach (var v in serviceRequestList) serviceRequestIdList.Add(v.Id);
                        serviceRequestIdList = serviceRequestIdList.Distinct().ToList();

                        serviceRequestTypeList = (from srt in db.ServiceRequestTypes join srid in serviceRequestIdList on srt.ServiceRequest.Id equals srid select srt).ToList();

                        serviceRequestOntList = (from sro in db.ServiceRequestOnts join srid in serviceRequestIdList on sro.ServiceRequest.Id equals srid select sro).ToList();
                        foreach (var v in serviceRequestOntList) serviceRequestOntIdList.Add(v.Id);
                        serviceRequestOntIdList = serviceRequestOntIdList.Distinct().ToList();

                        accessList = (from a in db.Accesses join srs in db.ServiceRequestServices on a equals srs.Access join srsid in serviceRequestServiceIdList on srs.Id equals srsid select a)
                            .Union(from a in db.Accesses join s in db.Service2s on a equals s.Access join sid in serviceIdList on s.Id equals sid select a)
                            .Union(from a in db.Accesses join sro in db.ServiceRequestOnts on a equals sro.Access join sroid in serviceRequestOntIdList on sro.Id equals sroid select a)
                            .ToList();
                        foreach (var v in accessList) accessIdList.Add(v.Id);
                        accessIdList = accessIdList.Distinct().ToList();

                        subscriberList = subscriberList.Union(from s in db.Subscribers join spid in subPartyIdList on s.SubParty.Id equals spid select s).ToList();

                        agcfEndpointList = agcfEndpointList.Union(from e in db.AgcfEndpoints join sp in db.SubParties on e equals sp.AgcfEndpoint join spid in subPartyIdList on sp.Id equals spid select e).ToList();
                        foreach (var v in agcfEndpointList) agcfEndpointIdList.Add(v.Id);
                        agcfEndpointIdList = agcfEndpointIdList.Distinct().ToList();

                        nddOntList = (from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList join aid in accessIdList on nddo.Access.Id equals aid select nddo)
                                      .Union(from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList join oid in ontIdList on nddo.Id equals oid select nddo)
                                      .ToList();
                        foreach (var v in nddOntList)
                        {
                            ontIdList.Add(v.Id);

                            eventSystemList.Add(v.Pon.Olt.AmsName);
                            eventAidOntList.Add("ONT-1-1-" + v.Card + "-" + v.Port + "-" + v.Number);
                            eventAidOntVoipList.Add("ONTVOIP-1-1-" + v.Card + "-" + v.Port + "-1");
                        }
                        ontIdList = ontIdList.Distinct().ToList();
                        eventSystemList = eventSystemList.Distinct().ToList();
                        eventAidOntList = eventAidOntList.Distinct().ToList();
                        eventAidOntVoipList = eventAidOntVoipList.Distinct().ToList();

                        // tier 3
                        ontList = (from o in db.Onts join oid in ontIdList on o.Id equals oid select o).ToList();
                        foreach (var v in ontList) ontIdList.Add(v.Id);
                        ontIdList = ontIdList.Distinct().ToList();

                        ontOntPotsList = (from op in db.OntOntPotses join oid in ontIdList on op.Ont.Id equals oid select op).ToList();
                        ontServiceVoipList = (from ov in db.OntServiceVoips join oid in ontIdList on ov.Ont.Id equals oid select ov).ToList();
                        ontServiceHsiList = (from oh in db.OntServiceHsis join oid in ontIdList on oh.Ont.Id equals oid select oh).ToList();
                        
                        foreach (var v in nddOntList)
                        {
                            ontIdList.Add(v.Id);

                            eventSystemList.Add(v.Pon.Olt.AmsName);
                            eventAidOntList.Add("ONT-1-1-" + v.Card + "-" + v.Port + "-" + v.Number);
                            eventAidOntVoipList.Add("ONTVOIP-1-1-" + v.Card + "-" + v.Port + "-1");

                            foreach (var w in ontOntPotsList) eventAidOntPotsList.Add("ONTPOTS-1-1-" + v.Card + "-" + v.Port + "-" + w.Card + "-" + w.Port);
                        }
                        ontIdList = ontIdList.Distinct().ToList();
                        eventSystemList = eventSystemList.Distinct().ToList();
                        eventAidOntList = eventAidOntList.Distinct().ToList();
                        eventAidOntVoipList = eventAidOntVoipList.Distinct().ToList();

                        /*
                        eventList = (from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao select e)
                                    .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaov in eventAidOntVoipList on e.Aid equals eaov select e)
                                    .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaop in eventAidOntPotsList on e.Aid equals eaop select e)
                                    .ToList();
                         */

                        agcfGatewayRecordList = (from gr in db.AgcfGatewayRecords join e in db.AgcfEndpoints on gr equals e.AgcfGatewayRecord join eid in agcfEndpointIdList on e.Id equals eid select gr).ToList();
                        foreach (var v in agcfGatewayRecordList) gatewayRecordIdList.Add(v.Id);
                        gatewayRecordIdList = gatewayRecordIdList.Distinct().ToList();
                    }
                }
                catch (Exception ex)
                {
                    result.AddError("Exception: Retrieval error of data for \"" + number + "\": " + ex.ToString());
                }
            }
            else
            {
                result.AddError("The number \"" + number + "\" does not belong to the network (الرقم لا ينتمي للشبكة). ");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void OntName(string input,
            out List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> nddOntList,
            out List<Ia.Ngn.Cl.Model.Ont> ontList,
            out Ia.Cl.Model.Result result)
        {
            List<Ia.Ngn.Cl.Model.Service2> serviceList;

            List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList;
            List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList;
            List<Ia.Ngn.Cl.Model.Nokia.SubParty> subPartyList;
            List<Ia.Ngn.Cl.Model.Nokia.Subscriber> subscriberList;

            List<Ia.Ngn.Cl.Model.Huawei.HuSbr> huSbrList;
            //List<Ia.Ngn.Cl.Model.Ont> ontList;
            //List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> nddOntList;
            List<Ia.Ngn.Cl.Model.OntServiceVoip> ontServiceVoipList;
            List<Ia.Ngn.Cl.Model.OntOntPots> ontOntPotsList;
            List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList;
            List<Ia.Ngn.Cl.Model.Event> eventList;
            List<Ia.Ngn.Cl.Model.Access> accessList;
            List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList;
            List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList;
            List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList;
            List<Ia.Ngn.Cl.Model.ServiceRequestOnt> serviceRequestOntList;
            List<Ia.Ngn.Cl.Model.Report> reportList;
            List<Ia.Ngn.Cl.Model.ReportHistory> reportHistoryList;

            Ia.Ngn.Cl.Model.Business.Maintenance.Find.OntName(input, out serviceList, out agcfGatewayRecordList, out agcfEndpointList, out subPartyList, out subscriberList, out huSbrList, out nddOntList, out ontList, out ontServiceVoipList, out ontOntPotsList, out ontServiceHsiList, out eventList, out accessList, out serviceRequestServiceList, out serviceRequestList, out serviceRequestTypeList, out serviceRequestOntList, out reportList, out reportHistoryList, out result);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void OntName(string input,
            out List<Ia.Ngn.Cl.Model.Service2> serviceList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList,
            out List<Ia.Ngn.Cl.Model.Nokia.SubParty> subPartyList,
            out List<Ia.Ngn.Cl.Model.Nokia.Subscriber> subscriberList,
            out List<Ia.Ngn.Cl.Model.Huawei.HuSbr> huSbrList,
            out List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> nddOntList,
            out List<Ia.Ngn.Cl.Model.Ont> ontList,
            out List<Ia.Ngn.Cl.Model.OntServiceVoip> ontServiceVoipList,
            out List<Ia.Ngn.Cl.Model.OntOntPots> ontOntPotsList,
            out List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList,
            out List<Ia.Ngn.Cl.Model.Event> eventList,
            out List<Ia.Ngn.Cl.Model.Access> accessList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList,
            out List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestOnt> serviceRequestOntList,
            out List<Ia.Ngn.Cl.Model.Report> reportList,
            out List<Ia.Ngn.Cl.Model.ReportHistory> reportHistoryList,
            out Ia.Cl.Model.Result result)
        {
            Initialize();

            ipList = new List<string>();

            result = new Ia.Cl.Model.Result();

            serviceList = null;
            agcfGatewayRecordList = null;
            agcfEndpointList = null;
            subPartyList = null;
            subscriberList = null;
            huSbrList = null;
            nddOntList = null;
            ontList = null;
            ontServiceVoipList = null;
            ontOntPotsList = null;
            ontServiceHsiList = null;
            eventList = null;
            accessList = null;
            serviceRequestServiceList = null;
            serviceRequestList = null;
            serviceRequestTypeList = null;
            serviceRequestOntList = null;
            reportList = null;
            reportHistoryList = null;

            // below: get olt, pon and ont
            ontName = Ia.Ngn.Cl.Model.Business.Ont.ExtractOntNameWithValidSymbolAndLegalFormatForPonAndOntFromValue(input);

            if (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntNameIsWithinAllowedOntList(ontName))
            {
                Ia.Ngn.Cl.Model.Data.Access.ExtractOltIdAndPonNumberAndOntNumberFromOntName(ontName, out oltId, out ponNumber, out ontNumber);

                try
                {
                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        // tier 1
                        nddOntList = (from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList
                                      where nddo.Pon.Olt.Id == oltId && nddo.Pon.Number == ponNumber && nddo.Number == ontNumber
                                      select nddo).ToList();
                        foreach (var v in nddOntList) nddOntIdList.Add(v.Id);
                        nddOntIdList = nddOntIdList.Distinct().ToList();

                        accessList = (from a in db.Accesses where a.Olt == oltId && a.Pon == ponNumber && a.Ont == ontNumber select a).ToList();
                        foreach (var v in accessList) accessIdList.Add(v.Id);
                        accessIdList = accessIdList.Distinct().ToList();

                        reportList = (from r in db.Reports where r.Service == ontName && r.ServiceType == 2 select r).ToList();
                        // <type id="2" name="Ont Name" arabicName=""/>

                        serviceRequestServiceList = (from srs in db.ServiceRequestServices where srs.Service == ontName && srs.ServiceType == 2 select srs).ToList();
                        // <type id="2" name="Ont Name" arabicName=""/>

                        serviceList = (from s in db.Service2s where s.Service == ontName && s.ServiceType == 2 select s).ToList();
                        // <type id="2" name="Ont Name" arabicName=""/>
                        foreach (var v in serviceList) serviceIdList.Add(v.Id);
                        serviceIdList = serviceIdList.Distinct().ToList();

                        // tier 2
                        ontList = (from o in db.Onts join aid in accessIdList on o.Access.Id equals aid select o)
                                   .Union(from o in db.Onts join nid in nddOntIdList on o.Id equals nid select o)
                                   .ToList();
                        foreach (var v in ontList) ontIdList.Add(v.Id);
                        ontIdList = ontIdList.Distinct().ToList();

                        ontOntPotsList = (from op in db.OntOntPotses join oid in ontIdList on op.Ont.Id equals oid select op).ToList();
                        ontServiceVoipList = (from ov in db.OntServiceVoips join oid in ontIdList on ov.Ont.Id equals oid select ov).ToList();
                        foreach (var v in ontServiceVoipList) ipList.Add(v.Ip);
                        ipList = ipList.Distinct().ToList();

                        ontServiceHsiList = (from oh in db.OntServiceHsis join oid in ontIdList on oh.Ont.Id equals oid select oh).ToList();

                        nddOntList = nddOntList.Union(from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList join aid in accessIdList on nddo.Access.Id equals aid select nddo)
                                      .Union(from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList join oid in ontIdList on nddo.Id equals oid select nddo).ToList();
                        foreach (var v in nddOntList)
                        {
                            ontIdList.Add(v.Id);

                            eventSystemList.Add(v.Pon.Olt.AmsName);
                            eventAidOntList.Add("ONT-1-1-" + v.Card + "-" + v.Port + "-" + v.Number);
                            eventAidOntVoipList.Add("ONTVOIP-1-1-" + v.Card + "-" + v.Port + "-1");

                            foreach (var w in ontOntPotsList) eventAidOntPotsList.Add("ONTPOTS-1-1-" + v.Card + "-" + v.Port + "-" + w.Card + "-" + w.Port);
                        }
                        ontIdList = ontIdList.Distinct().ToList();
                        eventSystemList = eventSystemList.Distinct().ToList();
                        eventAidOntList = eventAidOntList.Distinct().ToList();
                        eventAidOntVoipList = eventAidOntVoipList.Distinct().ToList();

                        /*
                        eventList = (from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao select e)
                                    .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaov in eventAidOntVoipList on e.Aid equals eaov select e)
                                    .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaop in eventAidOntPotsList on e.Aid equals eaop select e)
                                    .ToList();
                                    */

                        // tier 3
                        agcfGatewayRecordList = (from gr in db.AgcfGatewayRecords join ip in ipList on gr.IP1 equals ip select gr).ToList();
                        foreach (var v in agcfGatewayRecordList) gatewayRecordIdList.Add(v.Id);
                        gatewayRecordIdList = gatewayRecordIdList.Distinct().ToList();

                        agcfEndpointList = (from e in db.AgcfEndpoints join grid in gatewayRecordIdList on e.AgcfGatewayRecord.Id equals grid where e.AgcfGatewayRecord != null select e).ToList();
                        foreach (var v in agcfEndpointList) agcfEndpointIdList.Add(v.Id);
                        agcfEndpointIdList = agcfEndpointIdList.Distinct().ToList();
                        foreach (var v in agcfEndpointList) prividUserList.Add(v.PrividUser);
                        prividUserList = prividUserList.Distinct().ToList();

                        foreach (var v in prividUserList) partyIdList.Add(Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(v));
                        partyIdList = partyIdList.Distinct().ToList();

                        subPartyList = (from sp in db.SubParties join e in agcfEndpointIdList on sp.AgcfEndpoint.Id equals e where sp.AgcfEndpoint != null select sp)
                            .Union(from sp in db.SubParties join p in partyIdList on sp.PartyId equals p select sp)
                            .ToList();
                        foreach (var v in subPartyList) subPartyIdList.Add(v.Id);
                        subPartyIdList = subPartyIdList.Distinct().ToList();

                        // below: db.Subscribers contains XML fields which can not be evaluated in a Union() function.
                        subscriberList = (from s in db.Subscribers join pid in partyIdList on s.PartyId equals pid select s).ToList();
                        foreach (var v in subscriberList) subscriberIdList.Add(v.Id);
                        subscriberIdList = subscriberIdList.Distinct().ToList();
                        subscriberList = (from s in db.Subscribers join spid in subPartyIdList on s.SubParty.Id equals spid select s).ToList();
                        foreach (var v in subscriberList) subscriberIdList.Add(v.Id);
                        subscriberIdList = subscriberIdList.Distinct().ToList();
                        subscriberList = (from s in db.Subscribers join sid in subscriberIdList on s.Id equals sid select s).ToList();

                        foreach (var v in partyIdList)
                        {
                            numberStringList.Add(Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Service(v));
                            numberList.Add(Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Number(v));
                            impuList.Add(Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Impu(v));
                        }

                        serviceRequestServiceList = serviceRequestServiceList.Union(from srs in db.ServiceRequestServices join ns in numberStringList on srs.Service equals ns select srs).ToList();
                        foreach (var v in serviceRequestServiceList) serviceRequestServiceIdList.Add(v.Id);
                        serviceRequestServiceIdList = serviceRequestServiceIdList.Distinct().ToList();

                        serviceList = serviceList.Union(from s in db.Service2s join ns in numberStringList on s.Service equals ns select s).ToList();

                        huSbrList = (from h in db.HuSbrs join i in impuList on h.IMPU equals i select h).ToList();

                        serviceRequestList = (from sr in db.ServiceRequests join n in numberList on sr.Number equals n select sr).ToList();
                        serviceRequestList = serviceRequestList.Union(from sr in db.ServiceRequests join srsid in serviceRequestServiceIdList on sr.ServiceRequestService.Id equals srsid where sr.ServiceRequestService != null select sr).ToList();
                        foreach (var v in serviceRequestList) serviceRequestIdList.Add(v.Id);
                        serviceRequestIdList = serviceRequestIdList.Distinct().ToList();

                        serviceRequestTypeList = (from srt in db.ServiceRequestTypes join srid in serviceRequestIdList on srt.ServiceRequest.Id equals srid where srt.ServiceRequest != null select srt).ToList();

                        serviceRequestOntList = (from sro in db.ServiceRequestOnts join srid in serviceRequestIdList on sro.ServiceRequest.Id equals srid where sro.ServiceRequest != null select sro).ToList();
                        foreach (var v in serviceRequestOntList) serviceRequestOntIdList.Add(v.Id);
                        serviceRequestOntIdList = serviceRequestOntIdList.Distinct().ToList();

                        reportList = reportList.Union((from r in db.Reports join n in numberStringList on r.Service equals n select r).Include(x => x.ReportHistories)).ToList();
                        foreach (var v in reportList) reportIdList.Add(v.Id);
                        reportIdList = reportIdList.Distinct().ToList();
                    }
                }
                catch (Exception ex)
                {
                    result.AddError("Exception: Retrieval error of data for \"" + ontName + "\": " + ex.ToString());
                }
            }
            else
            {
                result.AddError("The ONT \"" + input + "\" does not belong to the network (الصندوق لا ينتمي للشبكة). ");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void Serial(string input,
            out List<Ia.Ngn.Cl.Model.Service2> serviceList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList,
            out List<Ia.Ngn.Cl.Model.Nokia.SubParty> subPartyList,
            out List<Ia.Ngn.Cl.Model.Nokia.Subscriber> subscriberList,
            out List<Ia.Ngn.Cl.Model.Huawei.HuSbr> huSbrList,
            out List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> nddOntList,
            out List<Ia.Ngn.Cl.Model.Ont> ontList,
            out List<Ia.Ngn.Cl.Model.OntServiceVoip> ontServiceVoipList,
            out List<Ia.Ngn.Cl.Model.OntOntPots> ontOntPotsList,
            out List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList,
            out List<Ia.Ngn.Cl.Model.Event> eventList,
            out List<Ia.Ngn.Cl.Model.Access> accessList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList,
            out List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestOnt> serviceRequestOntList,
            out List<Ia.Ngn.Cl.Model.Report> reportList,
            out List<Ia.Ngn.Cl.Model.ReportHistory> reportHistoryList,
            out Ia.Cl.Model.Result result)
        {
            Initialize();

            result = new Ia.Cl.Model.Result();

            serviceList = null;

            agcfGatewayRecordList = null;
            agcfEndpointList = null;
            subPartyList = null;
            subscriberList = null;

            huSbrList = null;
            nddOntList = null;
            ontList = null;
            ontServiceVoipList = null;
            ontOntPotsList = null;
            ontServiceHsiList = null;
            eventList = null;
            accessList = null;
            serviceRequestServiceList = null;
            serviceRequestList = null;
            serviceRequestTypeList = null;
            serviceRequestOntList = null;
            reportList = null;
            reportHistoryList = null;

            try
            {
                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    ontList = (from o in db.Onts where o.Serial == input select o).ToList();
                    foreach (var v in ontList) ontIdList.Add(v.Id);
                    ontIdList = ontIdList.Distinct().ToList();

                    foreach (var v in ontList) accessIdList.Add(v.Access.Id);
                    accessIdList = accessIdList.Distinct().ToList();

                    ontOntPotsList = (from op in db.OntOntPotses join oid in ontIdList on op.Ont.Id equals oid select op).ToList();
                    ontServiceVoipList = (from ov in db.OntServiceVoips join oid in ontIdList on ov.Ont.Id equals oid select ov).ToList();
                    ontServiceHsiList = (from oh in db.OntServiceHsis join oid in ontIdList on oh.Ont.Id equals oid select oh).ToList();

                    nddOntList = (from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList
                                  join aid in accessIdList on nddo.Access.Id equals aid
                                  select nddo)
                                  .Union(
                                  from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList
                                  join oid in ontIdList on nddo.Id equals oid
                                  select nddo
                                  ).ToList();
                    foreach (var v in nddOntList)
                    {
                        ontIdList.Add(v.Id);

                        eventSystemList.Add(v.Pon.Olt.AmsName);
                        eventAidOntList.Add("ONT-1-1-" + v.Card + "-" + v.Port + "-" + v.Number);
                        eventAidOntVoipList.Add("ONTVOIP-1-1-" + v.Card + "-" + v.Port + "-1");

                        foreach (var w in ontOntPotsList) eventAidOntPotsList.Add("ONTPOTS-1-1-" + v.Card + "-" + v.Port + "-" + w.Card + "-" + w.Port);
                    }
                    ontIdList = ontIdList.Distinct().ToList();
                    eventSystemList = eventSystemList.Distinct().ToList();
                    eventAidOntList = eventAidOntList.Distinct().ToList();
                    eventAidOntVoipList = eventAidOntVoipList.Distinct().ToList();

                    /*
                    eventList = (from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao select e)
                                .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaov in eventAidOntVoipList on e.Aid equals eaov select e)
                                .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaop in eventAidOntPotsList on e.Aid equals eaop select e)
                                .ToList();
                                */

                    accessList = (from a in db.Accesses join aid in accessIdList on a.Id equals aid select a).ToList();

                    serviceRequestServiceList = (from srs in db.ServiceRequestServices
                                                 join aid in accessIdList on srs.Access.Id equals aid
                                                 select srs).ToList();

                    foreach (var v in serviceRequestServiceList)
                    {
                        number = int.Parse(v.Service);
                        numberWithCountryCode = long.Parse(Ia.Ngn.Cl.Model.Data.Service.CountryCode.ToString() + number);
                        numberString = v.Service;
                        partyId = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(numberString);

                        numberList.Add(number);
                        serviceIdList.Add(v.Service);
                        impuList.Add(Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Impu(number));
                        numberStringList.Add(numberString);
                        serviceLongList.Add(numberWithCountryCode);
                        partyIdList.Add(partyId);
                    }
                    numberList = numberList.Distinct().ToList();
                    serviceIdList = serviceIdList.Distinct().ToList();
                    impuList = impuList.Distinct().ToList();
                    numberStringList = numberStringList.Distinct().ToList();
                    serviceLongList = serviceLongList.Distinct().ToList();
                    partyIdList = partyIdList.Distinct().ToList();


                    serviceList = (from se in db.Service2s join ns in numberStringList on se.Service equals ns select se).ToList();

                    agcfGatewayRecordList = (from gr in db.AgcfGatewayRecords join e in db.AgcfEndpoints on gr equals e.AgcfGatewayRecord join sp in db.SubParties on e equals sp.AgcfEndpoint join p in partyIdList on sp.PartyId equals p select gr).ToList();
                    foreach (var v in agcfGatewayRecordList) gatewayRecordIdList.Add(v.Id);
                    gatewayRecordIdList = gatewayRecordIdList.Distinct().ToList();

                    agcfEndpointList = (from e in db.AgcfEndpoints join grid in gatewayRecordIdList on e.AgcfGatewayRecord.Id equals grid select e).ToList();

                    subscriberList = (from s in db.Subscribers join p in partyIdList on s.PartyId equals p select s).ToList();
                    subPartyList = (from sp in db.SubParties join p in partyIdList on sp.PartyId equals p select sp).ToList();

                    huSbrList = (from h in db.HuSbrs join i in impuList on h.IMPU equals i select h).ToList();

                    serviceRequestList = (from sr in db.ServiceRequests join n in numberList on sr.Number equals n select sr).ToList();
                    serviceRequestTypeList = (from srt in db.ServiceRequestTypes join sr in db.ServiceRequests on srt.ServiceRequest equals sr join n in numberList on sr.Number equals n select srt).ToList();

                    reportList = (from r in db.Reports join sl in serviceIdList on r.Service equals sl select r).Include(x => x.ReportHistories).ToList();
                }
            }
            catch (Exception ex)
            {
                result.AddError("Exception: Retrieval error of data for \"" + input + "\": " + ex.ToString());
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void Ip(string input,
            out List<Ia.Ngn.Cl.Model.Service2> serviceList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList,
            out List<Ia.Ngn.Cl.Model.Nokia.SubParty> subPartyList,
            out List<Ia.Ngn.Cl.Model.Nokia.Subscriber> subscriberList,
            out List<Ia.Ngn.Cl.Model.Huawei.HuSbr> huSbrList,
            out List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> nddOntList,
            out List<Ia.Ngn.Cl.Model.Ont> ontList,
            out List<Ia.Ngn.Cl.Model.OntServiceVoip> ontServiceVoipList,
            out List<Ia.Ngn.Cl.Model.OntOntPots> ontOntPotsList,
            out List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList,
            out List<Ia.Ngn.Cl.Model.Event> eventList,
            out List<Ia.Ngn.Cl.Model.Access> accessList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList,
            out List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestOnt> serviceRequestOntList,
            out List<Ia.Ngn.Cl.Model.Report> reportList,
            out List<Ia.Ngn.Cl.Model.ReportHistory> reportHistoryList,
            out Ia.Cl.Model.Result result)
        {
            Initialize();

            result = new Ia.Cl.Model.Result();

            serviceList = null;

            agcfGatewayRecordList = null;
            agcfEndpointList = null;
            subPartyList = null;
            subscriberList = null;

            huSbrList = null;
            nddOntList = null;
            ontList = null;
            ontServiceVoipList = null;
            ontOntPotsList = null;
            ontServiceHsiList = null;
            eventList = null;
            accessList = null;
            serviceRequestServiceList = null;
            serviceRequestList = null;
            serviceRequestTypeList = null;
            serviceRequestOntList = null;
            reportList = null;
            reportHistoryList = null;

            try
            {
                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    ontServiceVoipList = (from ov in db.OntServiceVoips where ov.Ip == input select ov).ToList();
                    foreach (var v in ontServiceVoipList) ontIdList.Add(v.Ont.Id);
                    ontIdList = ontIdList.Distinct().ToList();

                    agcfGatewayRecordList = (from gr in db.AgcfGatewayRecords where gr.IP1 == input || gr.IP2 == input select gr).ToList();
                    foreach (var v in agcfGatewayRecordList) gatewayRecordIdList.Add(v.Id);
                    gatewayRecordIdList = gatewayRecordIdList.Distinct().ToList();

                    agcfEndpointList = (from e in db.AgcfEndpoints join grid in gatewayRecordIdList on e.AgcfGatewayRecord.Id equals grid select e).ToList();
                    foreach (var v in agcfEndpointList)
                    {
                        number = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Number(v.PrividUser);
                        numberWithCountryCode = long.Parse(Ia.Ngn.Cl.Model.Data.Service.CountryCode.ToString() + number);
                        numberString = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Service(v.PrividUser);
                        partyId = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(v.PrividUser);

                        numberList.Add(number);
                        serviceIdList.Add(numberString);
                        impuList.Add(Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Impu(number));
                        numberStringList.Add(numberString);
                        serviceLongList.Add(numberWithCountryCode);
                        partyIdList.Add(partyId);
                    }
                    numberList = numberList.Distinct().ToList();
                    serviceIdList = serviceIdList.Distinct().ToList();
                    impuList = impuList.Distinct().ToList();
                    numberStringList = numberStringList.Distinct().ToList();
                    serviceLongList = serviceLongList.Distinct().ToList();
                    partyIdList = partyIdList.Distinct().ToList();

                    serviceList = (from se in db.Service2s join ns in numberStringList on se.Service equals ns select se).ToList();

                    serviceRequestServiceList = (from srs in db.ServiceRequestServices join aid in accessIdList on srs.Access.Id equals aid select srs).ToList();

                    subscriberList = (from s in db.Subscribers join p in partyIdList on s.PartyId equals p select s).ToList();
                    subPartyList = (from sp in db.SubParties join p in partyIdList on sp.PartyId equals p select sp).ToList();

                    huSbrList = (from h in db.HuSbrs join i in impuList on h.IMPU equals i select h).ToList();

                    serviceRequestList = (from sr in db.ServiceRequests join n in numberList on sr.Number equals n select sr).ToList();
                    serviceRequestTypeList = (from srt in db.ServiceRequestTypes join sr in db.ServiceRequests on srt.ServiceRequest equals sr join n in numberList on sr.Number equals n select srt).ToList();

                    reportList = (from r in db.Reports join sl in serviceIdList on r.Service equals sl select r).Include(x => x.ReportHistories).ToList();


                    nddOntList = (from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where nddo.Ip == input select nddo).ToList();
                    foreach (var v in nddOntList) ontIdList.Add(v.Id);
                    ontIdList = ontIdList.Distinct().ToList();

                    ontList = (from o in db.Onts join oid in ontIdList on o.Id equals oid select o).ToList();
                    foreach (var v in ontList) ontIdList.Add(v.Id);
                    ontIdList = ontIdList.Distinct().ToList();

                    foreach (var v in ontList) if (v.Access != null) accessIdList.Add(v.Access.Id);
                    accessIdList = accessIdList.Distinct().ToList();

                    ontOntPotsList = (from op in db.OntOntPotses join oid in ontIdList on op.Ont.Id equals oid select op).ToList();
                    ontServiceVoipList = (from ov in db.OntServiceVoips join oid in ontIdList on ov.Ont.Id equals oid select ov).ToList();
                    ontServiceHsiList = (from oh in db.OntServiceHsis join oid in ontIdList on oh.Ont.Id equals oid select oh).ToList();

                    foreach (var v in nddOntList)
                    {
                        eventSystemList.Add(v.Pon.Olt.AmsName);
                        eventAidOntList.Add("ONT-1-1-" + v.Card + "-" + v.Port + "-" + v.Number);
                        eventAidOntVoipList.Add("ONTVOIP-1-1-" + v.Card + "-" + v.Port + "-1");

                        foreach (var w in ontOntPotsList) eventAidOntPotsList.Add("ONTPOTS-1-1-" + v.Card + "-" + v.Port + "-" + w.Card + "-" + w.Port);
                    }

                    eventSystemList = eventSystemList.Distinct().ToList();
                    eventAidOntList = eventAidOntList.Distinct().ToList();
                    eventAidOntVoipList = eventAidOntVoipList.Distinct().ToList();

                    /*
                    eventList = (from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao select e)
                                .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaov in eventAidOntVoipList on e.Aid equals eaov select e)
                                .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaop in eventAidOntPotsList on e.Aid equals eaop select e)
                                .ToList();
                                */

                    accessList = (from a in db.Accesses join aid in accessIdList on a.Id equals aid select a).ToList();
                }
            }
            catch (Exception ex)
            {
                result.AddError("Exception: Retrieval error of data for \"" + input + "\": " + ex.ToString());
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void PonName(string input,
            out List<Ia.Ngn.Cl.Model.Service2> serviceList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList,
            out List<Ia.Ngn.Cl.Model.Nokia.SubParty> subPartyList,
            out List<Ia.Ngn.Cl.Model.Nokia.Subscriber> subscriberList,
            out List<Ia.Ngn.Cl.Model.Huawei.HuSbr> huSbrList,
            out List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> nddOntList,
            out List<Ia.Ngn.Cl.Model.Ont> ontList,
            out List<Ia.Ngn.Cl.Model.OntServiceVoip> ontServiceVoipList,
            out List<Ia.Ngn.Cl.Model.OntOntPots> ontOntPotsList,
            out List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList,
            out List<Ia.Ngn.Cl.Model.Event> eventList,
            out List<Ia.Ngn.Cl.Model.Access> accessList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList,
            out List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestOnt> serviceRequestOntList,
            out List<Ia.Ngn.Cl.Model.Report> reportList,
            out List<Ia.Ngn.Cl.Model.ReportHistory> reportHistoryList,
            out Ia.Cl.Model.Result result)
        {
            Initialize();

            result = new Ia.Cl.Model.Result();

            serviceList = null;

            agcfGatewayRecordList = null;
            agcfEndpointList = null;
            subPartyList = null;
            subscriberList = null;

            huSbrList = null;
            nddOntList = null;
            ontList = null;
            ontServiceVoipList = null;
            ontOntPotsList = null;
            ontServiceHsiList = null;
            eventList = null;
            accessList = null;
            serviceRequestServiceList = null;
            serviceRequestList = null;
            serviceRequestTypeList = null;
            serviceRequestOntList = null;
            reportList = null;
            reportHistoryList = null;

            // below: get olt and pon
            ponName = Ia.Ngn.Cl.Model.Business.Ont.ExtractPonNameWithValidSymbolAndLegalFormatForPonAndOntFromValue(input);

            if (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.PonNameIsWithinAllowedOntList(ponName))
            {
                Ia.Ngn.Cl.Model.Data.Access.ExtractOltIdAndPonNumberAndOntNumberFromOntName(ponName + ".1", out oltId, out ponNumber, out ontNumber);

                try
                {
                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        nddOntList = (from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList
                                      where nddo.Pon.Olt.Id == oltId && nddo.Pon.Number == ponNumber
                                      select nddo).ToList();
                        foreach (var v in nddOntList) nddOntIdList.Add(v.Id);
                        nddOntIdList = nddOntIdList.Distinct().ToList();

                        accessList = (from a in db.Accesses where a.Olt == oltId && a.Pon == ponNumber select a).ToList();
                        foreach (var v in accessList) accessIdList.Add(v.Id);
                        accessIdList = accessIdList.Distinct().ToList();

                        ontList = (from o in db.Onts join aid in accessIdList on o.Access.Id equals aid select o)
                                   .Union(from o in db.Onts join nid in nddOntIdList on o.Id equals nid select o)
                                   .ToList();
                        foreach (var v in ontList) ontIdList.Add(v.Id);
                        ontIdList = ontIdList.Distinct().ToList();

                        ontOntPotsList = (from op in db.OntOntPotses join oid in ontIdList on op.Ont.Id equals oid select op).ToList();
                        ontServiceVoipList = (from ov in db.OntServiceVoips join oid in ontIdList on ov.Ont.Id equals oid select ov).ToList();
                        ontServiceHsiList = (from oh in db.OntServiceHsis join oid in ontIdList on oh.Ont.Id equals oid select oh).ToList();

                        nddOntList = (from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList join aid in accessIdList on nddo.Access.Id equals aid select nddo)
                                      .Union(from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList join oid in ontIdList on nddo.Id equals oid select nddo)
                                      .Union(from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList join nid in nddOntIdList on nddo.Id equals nid select nddo)
                                      .Union(from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where nddo.Pon.Olt.Id == oltId && nddo.Pon.Number == ponNumber && nddo.Number == ontNumber select nddo).ToList();
                        foreach (var v in nddOntList)
                        {
                            ontIdList.Add(v.Id);

                            //eventSystemList.Add(v.Pon.Olt.AmsName);
                            //eventAidOntList.Add("ONT-1-1-" + v.Card + "-" + v.Port + "-" + v.Number);
                            //eventAidOntVoipList.Add("ONTVOIP-1-1-" + v.Card + "-" + v.Port + "-1");
                            
                            foreach (var w in ontOntPotsList) eventAidOntPotsList.Add("ONTPOTS-1-1-" + v.Card + "-" + v.Port + "-" + w.Card + "-" + w.Port);
                        }
                        ontIdList = ontIdList.Distinct().ToList();
                        //eventSystemList = eventSystemList.Distinct().ToList();
                        //eventAidOntList = eventAidOntList.Distinct().ToList();
                        //eventAidOntVoipList = eventAidOntVoipList.Distinct().ToList();

                        //eventSystemList.Add(ont);

                        /*
                        eventList = (from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao select e)
                                    .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaov in eventAidOntVoipList on e.Aid equals eaov select e)
                                    .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaop in eventAidOntPotsList on e.Aid equals eaop select e)
                                    .ToList();
                                    */


                        serviceRequestServiceList = (from srs in db.ServiceRequestServices
                                                     join aid in accessIdList on srs.Access.Id equals aid
                                                     select srs).ToList();

                        foreach (var v in serviceRequestServiceList)
                        {
                            number = int.Parse(v.Service);
                            numberWithCountryCode = long.Parse(Ia.Ngn.Cl.Model.Data.Service.CountryCode.ToString() + number);
                            numberString = v.Service;
                            partyId = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(numberString);

                            numberList.Add(number);
                            serviceIdList.Add(v.Service);
                            impuList.Add(Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Impu(number));
                            numberStringList.Add(numberString);
                            serviceLongList.Add(numberWithCountryCode);
                            partyIdList.Add(partyId);
                            prividUserList.Add(Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PrividUser(numberString));
                        }
                        numberList = numberList.Distinct().ToList();
                        serviceIdList = serviceIdList.Distinct().ToList();
                        impuList = impuList.Distinct().ToList();
                        numberStringList = numberStringList.Distinct().ToList();
                        serviceLongList = serviceLongList.Distinct().ToList();
                        partyIdList = partyIdList.Distinct().ToList();

                        serviceList = (from se in db.Service2s join ns in numberStringList on se.Service equals ns select se).ToList();

                        agcfEndpointList = (from e in db.AgcfEndpoints join puid in prividUserList on e.PrividUser equals puid select e).ToList();
                        foreach (var v in agcfEndpointList) agcfEndpointIdList.Add(v.Id);
                        agcfEndpointIdList = agcfEndpointIdList.Distinct().ToList();

                        agcfGatewayRecordList = (from gr in db.AgcfGatewayRecords join e in db.AgcfEndpoints on gr equals e.AgcfGatewayRecord join eid in agcfEndpointIdList on e.Id equals eid select gr).ToList();
                        foreach (var v in agcfGatewayRecordList) gatewayRecordIdList.Add(v.Id);
                        gatewayRecordIdList = gatewayRecordIdList.Distinct().ToList();

                        subscriberList = (from s in db.Subscribers join p in partyIdList on s.PartyId equals p select s).ToList();
                        subPartyList = (from sp in db.SubParties join p in partyIdList on sp.PartyId equals p select sp).ToList();

                        huSbrList = (from h in db.HuSbrs join i in impuList on h.IMPU equals i select h).ToList();

                        serviceRequestList = (from sr in db.ServiceRequests join n in numberList on sr.Number equals n select sr).ToList();
                        serviceRequestTypeList = (from srt in db.ServiceRequestTypes join sr in db.ServiceRequests on srt.ServiceRequest equals sr join n in numberList on sr.Number equals n select srt).ToList();

                        reportList = (from r in db.Reports join sl in serviceIdList on r.Service equals sl select r).Include(x => x.ReportHistories).ToList();
                    }
                }
                catch (Exception ex)
                {
                    result.AddError("Exception: Retrieval error of data for \"" + ponName + "\": " + ex.ToString());
                }
            }
            else
            {
                result.AddError("The PON name \"" + input + "\" does not belong to the network (المجال لا ينتمي للشبكة). ");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void OntPosition(string input,
            out List<Ia.Ngn.Cl.Model.Service2> serviceList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList,
            out List<Ia.Ngn.Cl.Model.Nokia.SubParty> subPartyList,
            out List<Ia.Ngn.Cl.Model.Nokia.Subscriber> subscriberList,
            out List<Ia.Ngn.Cl.Model.Huawei.HuSbr> huSbrList,
            out List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> nddOntList,
            out List<Ia.Ngn.Cl.Model.Ont> ontList,
            out List<Ia.Ngn.Cl.Model.OntServiceVoip> ontServiceVoipList,
            out List<Ia.Ngn.Cl.Model.OntOntPots> ontOntPotsList,
            out List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList,
            out List<Ia.Ngn.Cl.Model.Event> eventList,
            out List<Ia.Ngn.Cl.Model.Access> accessList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList,
            out List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestOnt> serviceRequestOntList,
            out List<Ia.Ngn.Cl.Model.Report> reportList,
            out List<Ia.Ngn.Cl.Model.ReportHistory> reportHistoryList,
            out Ia.Cl.Model.Result result)
        {
            Initialize();

            result = new Ia.Cl.Model.Result();

            serviceList = null;

            agcfGatewayRecordList = null;
            agcfEndpointList = null;
            subPartyList = null;
            subscriberList = null;

            huSbrList = null;
            nddOntList = null;
            ontList = null;
            ontServiceVoipList = null;
            ontOntPotsList = null;
            ontServiceHsiList = null;
            eventList = null;
            accessList = null;
            serviceRequestServiceList = null;
            serviceRequestList = null;
            serviceRequestTypeList = null;
            serviceRequestOntList = null;
            reportList = null;
            reportHistoryList = null;

            // below:
            Ia.Ngn.Cl.Model.Business.Nokia.Ams.OltIdRackSubCardPortOntFromOntPosition(input, out oltId, out rack, out sub, out card, out port, out ontNumber);

            nddOnt = (from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where nddo.Pon.Olt.Id == oltId && nddo.Rack == rack && nddo.Sub == sub && nddo.Card == card && nddo.Port == port && nddo.Number == ontNumber select nddo).SingleOrDefault();

            if (nddOnt != null)
            {
                try
                {
                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        ontList = (from o in db.Onts where o.Id == nddOnt.Id select o).ToList();
                        foreach (var v in ontList) ontIdList.Add(v.Id);
                        ontIdList = ontIdList.Distinct().ToList();

                        ontOntPotsList = (from op in db.OntOntPotses join oid in ontIdList on op.Ont.Id equals oid select op).ToList();
                        ontServiceVoipList = (from ov in db.OntServiceVoips join oid in ontIdList on ov.Ont.Id equals oid select ov).ToList();
                        ontServiceHsiList = (from oh in db.OntServiceHsis join oid in ontIdList on oh.Ont.Id equals oid select oh).ToList();

                        eventSystemList.Add(nddOnt.Pon.Olt.AmsName);
                        eventAidOntList.Add("ONT-1-1-" + nddOnt.Card + "-" + nddOnt.Port + "-" + nddOnt.Number);
                        eventAidOntVoipList.Add("ONTVOIP-1-1-" + nddOnt.Card + "-" + nddOnt.Port + "-1");

                        //foreach (var w in ontOntPotsList) eventAidOntPotsList.Add("ONTPOTS-1-1-" + nddOnt.Card + "-" + nddOnt.Port + "-" + w.Card + "-" + w.Port);

                        /*
                        eventList = (from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao select e)
                                    .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaov in eventAidOntVoipList on e.Aid equals eaov select e)
                                    .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaop in eventAidOntPotsList on e.Aid equals eaop select e)
                                    .ToList();
                                    */

                        accessList = (from a in db.Accesses where a.Olt == nddOnt.Pon.Olt.Id && a.Pon == nddOnt.Pon.Number && a.Ont == nddOnt.Number select a).ToList();
                        foreach (var v in accessList) accessIdList.Add(v.Id);
                        accessIdList = accessIdList.Distinct().ToList();

                        serviceRequestServiceList = (from srs in db.ServiceRequestServices join a in db.Accesses on srs.Access equals a 
                                                     //join o in db.Onts on a equals o.Access where nddOnt.Pon.Olt.Odf.Router.Site.KuwaitNgnAreas.Any(x => x.Id == a.AreaId) && a.Pon == nddOnt.Pon.Number && a.Ont == nddOnt.Number 
                                                     select srs).ToList();

                        foreach (var v in serviceRequestServiceList)
                        {
                            number = int.Parse(v.Service);
                            numberWithCountryCode = long.Parse(Ia.Ngn.Cl.Model.Data.Service.CountryCode.ToString() + number);
                            numberString = v.Service;
                            partyId = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(numberString);

                            numberList.Add(number);
                            serviceIdList.Add(v.Service);
                            impuList.Add(Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Impu(number));
                            numberStringList.Add(numberString);
                            serviceLongList.Add(numberWithCountryCode);
                            partyIdList.Add(partyId);
                        }
                        numberList = numberList.Distinct().ToList();
                        serviceIdList = serviceIdList.Distinct().ToList();
                        impuList = impuList.Distinct().ToList();
                        numberStringList = numberStringList.Distinct().ToList();
                        serviceLongList = serviceLongList.Distinct().ToList();
                        partyIdList = partyIdList.Distinct().ToList();

                        serviceList = (from se in db.Service2s join ns in numberStringList on se.Service equals ns select se).ToList();

                        agcfGatewayRecordList = (from gr in db.AgcfGatewayRecords join e in db.AgcfEndpoints on gr equals e.AgcfGatewayRecord join sp in db.SubParties on e equals sp.AgcfEndpoint join p in partyIdList on sp.PartyId equals p select gr).ToList();
                        foreach (var v in agcfGatewayRecordList) gatewayRecordIdList.Add(v.Id);
                        gatewayRecordIdList = gatewayRecordIdList.Distinct().ToList();

                        agcfEndpointList = (from e in db.AgcfEndpoints join grid in gatewayRecordIdList on e.AgcfGatewayRecord.Id equals grid select e).ToList();

                        subscriberList = (from s in db.Subscribers join p in partyIdList on s.PartyId equals p select s).ToList();
                        subPartyList = (from sp in db.SubParties join p in partyIdList on sp.PartyId equals p select sp).ToList();

                        huSbrList = (from h in db.HuSbrs join i in impuList on h.IMPU equals i select h).ToList();

                        serviceRequestList = (from sr in db.ServiceRequests join n in numberList on sr.Number equals n select sr).ToList();
                        serviceRequestTypeList = (from srt in db.ServiceRequestTypes join sr in db.ServiceRequests on srt.ServiceRequest equals sr join n in numberList on sr.Number equals n select srt).ToList();

                        reportList = (from r in db.Reports join sl in serviceIdList on r.Service equals sl select r).Include(x => x.ReportHistories).ToList();
                    }
                }
                catch (Exception ex)
                {
                    result.AddError("Exception: Retrieval error of data for \"" + input + "\": " + ex.ToString());
                }
            }
            else
            {
                result.AddError("The ONT \"" + input + "\" does not belong to the Network Design Document (NDD) documment (الصندوق لا ينتمي للشبكة). ");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void GatewayId(string input,
            out List<Ia.Ngn.Cl.Model.Service2> serviceList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList,
            out List<Ia.Ngn.Cl.Model.Nokia.SubParty> subPartyList,
            out List<Ia.Ngn.Cl.Model.Nokia.Subscriber> subscriberList,
            out List<Ia.Ngn.Cl.Model.Huawei.HuSbr> huSbrList,
            out List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> nddOntList,
            out List<Ia.Ngn.Cl.Model.Ont> ontList,
            out List<Ia.Ngn.Cl.Model.OntServiceVoip> ontServiceVoipList,
            out List<Ia.Ngn.Cl.Model.OntOntPots> ontOntPotsList,
            out List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList,
            out List<Ia.Ngn.Cl.Model.Event> eventList,
            out List<Ia.Ngn.Cl.Model.Access> accessList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList,
            out List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestOnt> serviceRequestOntList,
            out List<Ia.Ngn.Cl.Model.Report> reportList,
            out List<Ia.Ngn.Cl.Model.ReportHistory> reportHistoryList,
            out Ia.Cl.Model.Result result)
        {
            Initialize();

            result = new Ia.Cl.Model.Result();

            serviceList = null;

            agcfGatewayRecordList = null;
            agcfEndpointList = null;
            subPartyList = null;
            subscriberList = null;

            huSbrList = null;
            nddOntList = null;
            ontList = null;
            ontServiceVoipList = null;
            ontOntPotsList = null;
            ontServiceHsiList = null;
            eventList = null;
            accessList = null;
            serviceRequestServiceList = null;
            serviceRequestList = null;
            serviceRequestTypeList = null;
            serviceRequestOntList = null;
            reportList = null;
            reportHistoryList = null;

            gatewayId = int.Parse(input);

            if (gatewayId >= Ia.Ngn.Cl.Model.Business.Nokia.Ims.FirstGatewayId && gatewayId <= Ia.Ngn.Cl.Model.Business.Nokia.Ims.LastGatewayId)
            {
                try
                {
                    using (var db = new Ia.Ngn.Cl.Model.Ngn())
                    {
                        // tier 1
                        agcfEndpointList = (from e in db.AgcfEndpoints where e.GwId == gatewayId select e).ToList();
                        foreach (var v in agcfEndpointList) agcfEndpointIdList.Add(v.Id);
                        agcfEndpointIdList = agcfEndpointIdList.Distinct().ToList();

                        agcfGatewayRecordList = (from gr in db.AgcfGatewayRecords where gr.GwId == gatewayId select gr).ToList();
                        foreach (var v in agcfGatewayRecordList)
                        {
                            gatewayRecordIdList.Add(v.Id);
                            ipList.Add(v.IP1);
                        }
                        gatewayRecordIdList = gatewayRecordIdList.Distinct().ToList();
                        ipList = ipList.Distinct().ToList();

                        subPartyList = (from sp in db.SubParties join eid in agcfEndpointIdList on sp.AgcfEndpoint.Id equals eid select sp).ToList();
                        foreach (var v in subPartyList) subPartyIdList.Add(v.Id);
                        subPartyIdList = subPartyIdList.Distinct().ToList();

                        subscriberList = (from s in db.Subscribers join sid in subPartyIdList on s.SubParty.Id equals sid select s).ToList();
                        foreach (var v in subscriberList)
                        {
                            number = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Number(v.PartyId);
                            numberList.Add(number);
                            numberStringList.Add(number.ToString());
                            partyIdList.Add(v.PartyId);
                        }
                        numberList = numberList.Distinct().ToList();
                        numberStringList = numberStringList.Distinct().ToList();
                        partyIdList = partyIdList.Distinct().ToList();

                        subPartyList = (from sp in db.SubParties join e in agcfEndpointIdList on sp.AgcfEndpoint.Id equals e where sp.AgcfEndpoint != null select sp)
                            .Union(from sp in db.SubParties join p in partyIdList on sp.PartyId equals p select sp)
                            .ToList();
                        foreach (var v in subPartyList) subPartyIdList.Add(v.Id);
                        subPartyIdList = subPartyIdList.Distinct().ToList();

                        // below: db.Subscribers contains XML fields which can not be evaluated in a Union() function.
                        subscriberList = (from s in db.Subscribers join pid in partyIdList on s.PartyId equals pid select s).ToList();
                        foreach (var v in subscriberList) subscriberIdList.Add(v.Id);
                        subscriberIdList = subscriberIdList.Distinct().ToList();
                        subscriberList = (from s in db.Subscribers join spid in subPartyIdList on s.SubParty.Id equals spid select s).ToList();
                        foreach (var v in subscriberList) subscriberIdList.Add(v.Id);
                        subscriberIdList = subscriberIdList.Distinct().ToList();
                        subscriberList = (from s in db.Subscribers join sid in subscriberIdList on s.Id equals sid select s).ToList();

                        foreach (var v in partyIdList)
                        {
                            numberStringList.Add(Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Service(v));
                            numberList.Add(Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Number(v));
                            impuList.Add(Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Impu(v));
                        }
                        numberStringList = numberStringList.Distinct().ToList();
                        numberList = numberList.Distinct().ToList();
                        impuList = impuList.Distinct().ToList();

                        serviceRequestServiceList = (from srs in db.ServiceRequestServices join ns in numberStringList on srs.Service equals ns select srs).ToList();
                        foreach (var v in serviceRequestServiceList) serviceRequestServiceIdList.Add(v.Id);
                        serviceRequestServiceIdList = serviceRequestServiceIdList.Distinct().ToList();

                        serviceList = (from s in db.Service2s join ns in numberStringList on s.Service equals ns select s).ToList();

                        huSbrList = (from h in db.HuSbrs join i in impuList on h.IMPU equals i select h).ToList();

                        serviceRequestList = (from sr in db.ServiceRequests join n in numberList on sr.Number equals n select sr).ToList();
                        serviceRequestList = serviceRequestList.Union(from sr in db.ServiceRequests join srsid in serviceRequestServiceIdList on sr.ServiceRequestService.Id equals srsid where sr.ServiceRequestService != null select sr).ToList();
                        foreach (var v in serviceRequestList) serviceRequestIdList.Add(v.Id);
                        serviceRequestIdList = serviceRequestIdList.Distinct().ToList();

                        serviceRequestTypeList = (from srt in db.ServiceRequestTypes join srid in serviceRequestIdList on srt.ServiceRequest.Id equals srid where srt.ServiceRequest != null select srt).ToList();

                        serviceRequestOntList = (from sro in db.ServiceRequestOnts join srid in serviceRequestIdList on sro.ServiceRequest.Id equals srid where sro.ServiceRequest != null select sro).ToList();
                        foreach (var v in serviceRequestOntList) serviceRequestOntIdList.Add(v.Id);
                        serviceRequestOntIdList = serviceRequestOntIdList.Distinct().ToList();

                        reportList = ((from r in db.Reports join n in numberStringList on r.Service equals n select r).Include(x => x.ReportHistories)).ToList();
                        foreach (var v in reportList) reportIdList.Add(v.Id);
                        reportIdList = reportIdList.Distinct().ToList();

                        nddOntList = (from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList join ip in ipList on nddo.Ip equals ip select nddo).ToList();
                        foreach (var v in nddOntList)
                        {
                            ontIdList.Add(v.Id);

                            eventSystemList.Add(v.Pon.Olt.AmsName);
                            eventAidOntList.Add("ONT-1-1-" + v.Card + "-" + v.Port + "-" + v.Number);
                            eventAidOntVoipList.Add("ONTVOIP-1-1-" + v.Card + "-" + v.Port + "-1");
                        }
                        ontIdList = ontIdList.Distinct().ToList();
                        eventSystemList = eventSystemList.Distinct().ToList();
                        eventAidOntList = eventAidOntList.Distinct().ToList();
                        eventAidOntVoipList = eventAidOntVoipList.Distinct().ToList();

                        ontList = (from o in db.Onts join oid in ontIdList on o.Id equals oid select o).ToList();
                        foreach (var v in ontList) ontIdList.Add(v.Id);
                        ontIdList = ontIdList.Distinct().ToList();

                        foreach (var v in ontList) if (v.Access != null) accessIdList.Add(v.Access.Id);
                        accessIdList = accessIdList.Distinct().ToList();

                        ontOntPotsList = (from op in db.OntOntPotses join oid in ontIdList on op.Ont.Id equals oid select op).ToList();
                        ontServiceVoipList = (from ov in db.OntServiceVoips join oid in ontIdList on ov.Ont.Id equals oid select ov).ToList();
                        ontServiceHsiList = (from oh in db.OntServiceHsis join oid in ontIdList on oh.Ont.Id equals oid select oh).ToList();

                        accessList = (from a in db.Accesses join aid in accessIdList on a.Id equals aid select a).ToList();
                        foreach (var v in nddOntList)
                        {
                            ontIdList.Add(v.Id);

                            eventSystemList.Add(v.Pon.Olt.AmsName);
                            eventAidOntList.Add("ONT-1-1-" + v.Card + "-" + v.Port + "-" + v.Number);
                            eventAidOntVoipList.Add("ONTVOIP-1-1-" + v.Card + "-" + v.Port + "-1");

                            foreach (var w in ontOntPotsList) eventAidOntPotsList.Add("ONTPOTS-1-1-" + v.Card + "-" + v.Port + "-" + w.Card + "-" + w.Port);
                        }
                        ontIdList = ontIdList.Distinct().ToList();
                        eventSystemList = eventSystemList.Distinct().ToList();
                        eventAidOntList = eventAidOntList.Distinct().ToList();
                        eventAidOntVoipList = eventAidOntVoipList.Distinct().ToList();

                        /*
                        eventList = (from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao select e)
                                    .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaov in eventAidOntVoipList on e.Aid equals eaov select e)
                                    .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaop in eventAidOntPotsList on e.Aid equals eaop select e)
                                    .ToList();
                                    */
                    }
                }
                catch (Exception ex)
                {
                    result.AddError("Exception: Retrieval error of data for \"" + number + "\": " + ex.ToString());
                }
            }
            else
            {
                result.AddError("The gatewayId \"" + gatewayId + "\" is not within the allowed gatewayId range (الرقم لا ينتمي للمجال). ");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void Address(string input,
            out List<Ia.Ngn.Cl.Model.Service2> serviceList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList,
            out List<Ia.Ngn.Cl.Model.Nokia.SubParty> subPartyList,
            out List<Ia.Ngn.Cl.Model.Nokia.Subscriber> subscriberList,
            out List<Ia.Ngn.Cl.Model.Huawei.HuSbr> huSbrList,
            out List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> nddOntList,
            out List<Ia.Ngn.Cl.Model.Ont> ontList,
            out List<Ia.Ngn.Cl.Model.OntServiceVoip> ontServiceVoipList,
            out List<Ia.Ngn.Cl.Model.OntOntPots> ontOntPotsList,
            out List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList,
            out List<Ia.Ngn.Cl.Model.Event> eventList,
            out List<Ia.Ngn.Cl.Model.Access> accessList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList,
            out List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestOnt> serviceRequestOntList,
            out List<Ia.Ngn.Cl.Model.Report> reportList,
            out List<Ia.Ngn.Cl.Model.ReportHistory> reportHistoryList,
            out Ia.Cl.Model.Result result)
        {
            Initialize();

            string streetString, premisesOldString, premisesNewString;

            result = new Ia.Cl.Model.Result();
            serviceList = null;
            agcfGatewayRecordList = null;
            agcfEndpointList = null;
            subPartyList = null;
            subscriberList = null;
            huSbrList = null;
            nddOntList = null;
            ontList = null;
            ontServiceVoipList = null;
            ontOntPotsList = null;
            ontServiceHsiList = null;
            eventList = null;
            accessList = null;
            serviceRequestServiceList = null;
            serviceRequestList = null;
            serviceRequestTypeList = null;
            serviceRequestOntList = null;
            reportList = null;
            reportHistoryList = null;

            try
            {
                ExtractAddressFromSearchAddressTerm(input, out kuwaitNgnArea, out block, out street, out premisesOld, out premisesNew);

                streetString = street.ToString();
                premisesOldString = premisesOld.ToString();
                premisesNewString = premisesNew.ToString();

                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    oltIdList = kuwaitNgnArea.Site.Routers.SelectMany(v => v.Odfs.SelectMany(u => u.Olts.Select(w => w.Id))).ToList();
               
                    accessList = (from a in db.Accesses join oid in oltIdList on a.Olt equals oid
                                  where
                                  a.Block == block && a.Street == streetString && a.PremisesOld == premisesOldString && a.PremisesNew == premisesNewString
                                  || a.Block == block && a.Street == streetString && a.PremisesOld == premisesOldString
                                  || a.Block == block && a.Street == streetString && a.PremisesNew == premisesNewString
                                  || a.Block == block && a.PremisesOld == premisesOldString && a.PremisesNew == premisesNewString
                                  select a).ToList();

                    if (accessList.Count == 0)
                    {
                        accessList = (from a in db.Accesses
                                      join oid in oltIdList on a.Olt equals oid
                                      where
                                      a.Block == block && a.PremisesOld == premisesOldString && a.PremisesNew == premisesNewString
                                      || a.Block == block && a.PremisesOld == premisesOldString
                                      || a.Block == block && a.PremisesNew == premisesNewString
                                      select a).ToList();

                        if (accessList.Count == 0)
                        {
                            accessList = (from a in db.Accesses join oid in oltIdList on a.Olt equals oid where a.Block == block && a.Street == streetString select a).ToList();

                            if(accessList.Count == 0) accessList = (from a in db.Accesses join oid in oltIdList on a.Olt equals oid where a.Block == block select a).ToList();
                        }
                    }

                    foreach (var v in accessList) accessIdList.Add(v.Id);
                    accessIdList = accessIdList.Distinct().ToList();

                    /*
                    ontList = (from o in db.Onts join aid in accessIdList on o.Access.Id equals aid select o)
                               .Union(from o in db.Onts join nid in nddOntIdList on o.Id equals nid select o)
                               .ToList();
                    foreach (var v in ontList) ontIdList.Add(v.Id);
                    ontIdList = ontIdList.Distinct().ToList();

                    ontOntPotsList = (from op in db.OntOntPotses join oid in ontIdList on op.Ont.Id equals oid select op).ToList();
                    ontServiceVoipList = (from ov in db.OntServiceVoips join oid in ontIdList on ov.Ont.Id equals oid select ov).ToList();
                    ontServiceHsiList = (from oh in db.OntServiceHsis join oid in ontIdList on oh.Ont.Id equals oid select oh).ToList();

                    nddOntList = (from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList join aid in accessIdList on nddo.Access.Id equals aid select nddo)
                                  .Union(from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList join oid in ontIdList on nddo.Id equals oid select nddo)
                                  .Union(from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList join nid in nddOntIdList on nddo.Id equals nid select nddo)
                                  .Union(from nddo in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where nddo.Pon.Olt.Id == oltId && nddo.Pon.Number == ponNumber && nddo.Number == ontNumber select nddo).ToList();
                    foreach (var v in nddOntList)
                    {
                        ontIdList.Add(v.Id);

                        //eventSystemList.Add(v.Pon.Olt.AmsName);
                        //eventAidOntList.Add("ONT-1-1-" + v.Card + "-" + v.Port + "-" + v.Number);
                        //eventAidOntVoipList.Add("ONTVOIP-1-1-" + v.Card + "-" + v.Port + "-1");

                        foreach (var w in ontOntPotsList) eventAidOntPotsList.Add("ONTPOTS-1-1-" + v.Card + "-" + v.Port + "-" + w.Card + "-" + w.Port);
                    }
                    ontIdList = ontIdList.Distinct().ToList();
                    //eventSystemList = eventSystemList.Distinct().ToList();
                    //eventAidOntList = eventAidOntList.Distinct().ToList();
                    //eventAidOntVoipList = eventAidOntVoipList.Distinct().ToList();

                    //eventSystemList.Add(ont);

                /*
                    eventList = (from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao select e)
                    .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaov in eventAidOntVoipList on e.Aid equals eaov select e)
                    .Union(from e in db.Events join es in eventSystemList on e.System equals es join eao in eventAidOntList on e.Aid equals eao join eaop in eventAidOntPotsList on e.Aid equals eaop select e)
                                .ToList();
                                * /


                    serviceRequestServiceList = (from srs in db.ServiceRequestServices
                                                 join aid in accessIdList on srs.Access.Id equals aid
                                                 select srs).ToList();

                    foreach (var v in serviceRequestServiceList)
                    {
                        number = int.Parse(v.Service);
                        numberWithCountryCode = long.Parse(Ia.Ngn.Cl.Model.Data.Service.CountryCode.ToString() + number);
                        numberString = v.Service;
                        partyId = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PartyId(numberString);

                        numberList.Add(number);
                        serviceIdList.Add(v.Service);
                        impuList.Add(Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Impu(number));
                        numberStringList.Add(numberString);
                        serviceLongList.Add(numberWithCountryCode);
                        partyIdList.Add(partyId);
                        prividUserList.Add(Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PrividUser(numberString));
                    }
                    numberList = numberList.Distinct().ToList();
                    serviceIdList = serviceIdList.Distinct().ToList();
                    impuList = impuList.Distinct().ToList();
                    numberStringList = numberStringList.Distinct().ToList();
                    serviceLongList = serviceLongList.Distinct().ToList();
                    partyIdList = partyIdList.Distinct().ToList();

                    serviceList = (from se in db.Service2s join ns in numberStringList on se.Service equals ns select se).ToList();

                    agcfEndpointList = (from e in db.AgcfEndpoints join puid in prividUserList on e.PrividUser equals puid select e).ToList();
                    foreach (var v in agcfEndpointList) agcfEndpointIdList.Add(v.Id);
                    agcfEndpointIdList = agcfEndpointIdList.Distinct().ToList();

                    agcfGatewayRecordList = (from gr in db.AgcfGatewayRecords join e in db.AgcfEndpoints on gr equals e.AgcfGatewayRecord join eid in agcfEndpointIdList on e.Id equals eid select gr).ToList();
                    foreach (var v in agcfGatewayRecordList) gatewayRecordIdList.Add(v.Id);
                    gatewayRecordIdList = gatewayRecordIdList.Distinct().ToList();

                    subscriberList = (from s in db.Subscribers join p in partyIdList on s.PartyId equals p select s).ToList();
                    subPartyList = (from sp in db.SubParties join p in partyIdList on sp.PartyId equals p select sp).ToList();

                    huSbrList = (from h in db.HuSbrs join i in impuList on h.IMPU equals i select h).ToList();

                    serviceRequestList = (from sr in db.ServiceRequests join n in numberList on sr.Number equals n select sr).ToList();
                    serviceRequestTypeList = (from srt in db.ServiceRequestTypes join sr in db.ServiceRequests on srt.ServiceRequest equals sr join n in numberList on sr.Number equals n select srt).ToList();

                    reportList = (from r in db.Reports join sl in serviceIdList on r.Service equals sl select r).Include(x => x.ReportHistories).ToList();
                    */
                }
            }
            catch (Exception ex)
            {
                result.AddError("Exception: Retrieval error of data for \"" + input + "\": " + ex.ToString());
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void CustomerName(string input,
            out List<Ia.Ngn.Cl.Model.Service2> serviceList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList,
            out List<Ia.Ngn.Cl.Model.Nokia.SubParty> subPartyList,
            out List<Ia.Ngn.Cl.Model.Nokia.Subscriber> subscriberList,
            out List<Ia.Ngn.Cl.Model.Huawei.HuSbr> huSbrList,
            out List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> nddOntList,
            out List<Ia.Ngn.Cl.Model.Ont> ontList,
            out List<Ia.Ngn.Cl.Model.OntServiceVoip> ontServiceVoipList,
            out List<Ia.Ngn.Cl.Model.OntOntPots> ontOntPotsList,
            out List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList,
            out List<Ia.Ngn.Cl.Model.Event> eventList,
            out List<Ia.Ngn.Cl.Model.Access> accessList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList,
            out List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestOnt> serviceRequestOntList,
            out List<Ia.Ngn.Cl.Model.Report> reportList,
            out List<Ia.Ngn.Cl.Model.ReportHistory> reportHistoryList,
            out Ia.Cl.Model.Result result)
        {
            Initialize();

            int maxNumberOfResults = 100;
            string customerName;

            result = new Ia.Cl.Model.Result();
            serviceList = null;
            agcfGatewayRecordList = null;
            agcfEndpointList = null;
            subPartyList = null;
            subscriberList = null;
            huSbrList = null;
            nddOntList = null;
            ontList = null;
            ontServiceVoipList = null;
            ontOntPotsList = null;
            ontServiceHsiList = null;
            eventList = null;
            accessList = null;
            serviceRequestServiceList = null;
            serviceRequestList = null;
            serviceRequestTypeList = null;
            serviceRequestOntList = null;
            reportList = null;
            reportHistoryList = null;

            try
            {
                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    customerName = Ia.Ngn.Cl.Model.Business.Default.CorrectCustomerName(input);

                    serviceRequestList = (from sr in db.ServiceRequests where sr.CustomerName == customerName || sr.CustomerName.Contains(customerName) select sr).Take(maxNumberOfResults).ToList();

                    if (serviceRequestList.Count >= maxNumberOfResults) result.Message = "The search returned " + maxNumberOfResults + " or more results. The output is limited to " + maxNumberOfResults + ".";
                }
            }
            catch (Exception ex)
            {
                result.AddError("Exception: Retrieval error of data for \"" + input + "\": " + ex.ToString());
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Extract address from search term like "السلام,2,202,339,36" {kuwaitArea,block,street,old premises,new premises}
        /// </summary>
        public static void ExtractAddressFromSearchAddressTerm(string addressTerm, out Ia.Ngn.Cl.Model.Business.Service.KuwaitNgnArea kuwaitNgnArea, out int block, out int street, out int premisesOld, out int premisesNew)
        {
            int i;
            string kuwaitNgnAreaString;
            Match match;

            // "السلام,2,202,339,36"
            match = Regex.Match(addressTerm, @"^(\w+),(\d{1,3}),(\d{0,3}),(\d{0,3}),(\d{0,3})$");

            kuwaitNgnAreaString = match.Groups[1].Value;
            block = int.TryParse(match.Groups[2].Value, out i)? i :0;
            street = int.TryParse(match.Groups[3].Value, out i) ? i : 0;
            premisesOld = int.TryParse(match.Groups[4].Value, out i) ? i : 0;
            premisesNew = int.TryParse(match.Groups[5].Value, out i) ? i : 0;

            kuwaitNgnArea = (from kna in Ia.Ngn.Cl.Model.Data.Service.KuwaitNgnAreaList
                             where
                             kna.ArabicName == kuwaitNgnAreaString || kna.ArabicName.Contains(kuwaitNgnAreaString)
                             || kna.Name == kuwaitNgnAreaString || kna.Name.Contains(kuwaitNgnAreaString)
                             select kna).FirstOrDefault();
        }

        //    ////////////////////////////////////////////////////////////////////////////

        //    /// <summary>
        //    ///
        //    /// </summary>
        //    private DataTable Service_Request_Type(DataTable dt)
        //    {
        //        // below: build a special format of the service_request_type_dt to produce nice, comprehensive display
        //        string key, value;
        //        string[] sa, sc;
        //        DataTable service_request_type_dt;

        //        service_request_type_dt = new DataTable();
        //        service_request_type_dt.Columns.Add("id", System.Type.GetType("System.Int32"));
        //        service_request_type_dt.Columns.Add("dn", System.Type.GetType("System.Int32"));
        //        service_request_type_dt.Columns.Add("serial");
        //        service_request_type_dt.Columns.Add("service_id");
        //        foreach (XElement xe in main.Data.ReturnAllIfFieldsForServiceRequestTypeTypeId())
        //        {
        //            service_request_type_dt.Columns.Add(xe.Attribute("id").Value);
        //        }

        //        // below: build a middle hash
        //        ht = new Hashtable(dt.Rows.Count + 1);
        //        ht.Clear();
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            key = r["id"].ToString() + "|" + r["dn"].ToString() + "|" + r["serial"].ToString() + "|" + r["service_id"].ToString();
        //            value = r["type_id"].ToString() + "≡" + r["type_value"].ToString() + "‼";

        //            if (ht.ContainsKey(key)) ht[key] += value;
        //            else ht[key] = value;
        //        }

        //        DataRow dr;

        //        // below: fill with values
        //        foreach (string s in ht.Keys)
        //        {
        //            dr = service_request_type_dt.NewRow();

        //            sa = s.Split('|');
        //            dr["id"] = sa[0].ToString();
        //            dr["dn"] = sa[1].ToString();
        //            dr["serial"] = sa[2].ToString();
        //            dr["service_id"] = sa[3].ToString();

        //            sa = ht[s].ToString().Split('‼');

        //            foreach (string t in sa)
        //            {
        //                if (t.Length > 0)
        //                {
        //                    sc = t.Split('≡');
        //                    if (service_request_type_dt.Columns.Contains(sc[0].ToString()))
        //                    {
        //                        dr[sc[0].ToString()] = sc[1].ToString();
        //                    }
        //                }
        //            }

        //            service_request_type_dt.Rows.Add(dr);
        //        }

        //        /*
        //        foreach (DataColumn c in service_request_type_dt.Columns)
        //        {
        //            foreach (DataControlField dcf in service_request_type_gv.Columns)
        //            {
        //                if (dcf.HeaderText == "Service") dcf.Visible = true;
        //                else if (dcf.HeaderText == "Severity" || dcf.HeaderText == "Priority") dcf.Visible = false;
        //                else if (dcf.HeaderText == "Category" || dcf.HeaderText == "Area") dcf.Visible = false;
        //                else if (dcf.HeaderText == "Contact") dcf.Visible = true;
        //            }
        //        }
        //        */

        //        return service_request_type_dt;
        //    }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////   
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
