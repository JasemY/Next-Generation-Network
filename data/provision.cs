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
using System.Linq;
using System.Reflection;

namespace Ia.Ngn.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Provision support class for Next Generation Network (NGN) data model.
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
    public class Provision
    {
        private static int allPossibleOntNotInAgcfGatewayRecordListIndex, allPossibleNddOntNotInAgcfGatewayRecordListIndex, allPossibleAgcfGatewayRecordFromWithinOltListIndex, allPossibleAgcfGatewayRecordNoInOntListIndex;
        private static List<Ia.Ngn.Cl.Model.Ont> allPossibleOntNotInAgcfGatewayRecordList;
        private static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> allPossibleNddOntNotInAgcfGatewayRecordList;
        private static List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> allPossibleAgcfGatewayRecordFromWithinOltList, allPossibleAgcfGatewayRecordNoInOntList;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Provision() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont NddOntNotInAgcfGatewayRecordList(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt, out string result)
        {
            Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont ont;

            if (allPossibleNddOntNotInAgcfGatewayRecordList == null || allPossibleNddOntNotInAgcfGatewayRecordListIndex == 0)
            {
                allPossibleNddOntNotInAgcfGatewayRecordList = Ia.Ngn.Cl.Model.Data.Nokia.Ims.AllPossibleNddOntNotInAgcfGatewayRecordList(olt);

                allPossibleNddOntNotInAgcfGatewayRecordListIndex = 0;
            }

            if (allPossibleNddOntNotInAgcfGatewayRecordList.Count > 0)
            {
                ont = (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont)allPossibleNddOntNotInAgcfGatewayRecordList[allPossibleNddOntNotInAgcfGatewayRecordListIndex];

                allPossibleNddOntNotInAgcfGatewayRecordListIndex = Ia.Cl.Model.Default.IncrementListIndexOrRestart(allPossibleNddOntNotInAgcfGatewayRecordList, allPossibleNddOntNotInAgcfGatewayRecordListIndex);
            }
            else ont = null;

            result = "(" + allPossibleNddOntNotInAgcfGatewayRecordListIndex + "/" + allPossibleNddOntNotInAgcfGatewayRecordList.Count + ") ";

            return ont;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord AgcfGatewayRecordFromWithinOltList(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt, out string result)
        {
            Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord agcfGatewayRecord;

            if (allPossibleAgcfGatewayRecordFromWithinOltList == null || allPossibleAgcfGatewayRecordFromWithinOltListIndex == 0)
            {
                allPossibleAgcfGatewayRecordFromWithinOltList = Ia.Ngn.Cl.Model.Data.Nokia.AgcfGatewayRecord.List(olt);

                allPossibleAgcfGatewayRecordFromWithinOltListIndex = 0;
            }

            if (allPossibleAgcfGatewayRecordFromWithinOltList.Count > 0)
            {
                agcfGatewayRecord = (Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord)allPossibleAgcfGatewayRecordFromWithinOltList[allPossibleAgcfGatewayRecordFromWithinOltListIndex];

                allPossibleAgcfGatewayRecordFromWithinOltListIndex = Ia.Cl.Model.Default.IncrementListIndexOrRestart(allPossibleAgcfGatewayRecordFromWithinOltList, allPossibleAgcfGatewayRecordFromWithinOltListIndex);
            }
            else agcfGatewayRecord = null;

            result = "(" + allPossibleAgcfGatewayRecordFromWithinOltListIndex + "/" + allPossibleAgcfGatewayRecordFromWithinOltList.Count + ") ";

            return agcfGatewayRecord;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord AgcfGatewayRecordNoInOntList(Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Olt olt)
        {
            Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord agcfGatewayRecord;

            if (allPossibleAgcfGatewayRecordNoInOntList == null || allPossibleAgcfGatewayRecordNoInOntListIndex == 0)
            {
                allPossibleAgcfGatewayRecordNoInOntList = Ia.Ngn.Cl.Model.Data.Nokia.Ims.AllPossibleAgcfGatewayRecordsNoInOntsList(olt);

                allPossibleAgcfGatewayRecordNoInOntListIndex = 0;
            }

            agcfGatewayRecord = (Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord)allPossibleAgcfGatewayRecordNoInOntList[allPossibleAgcfGatewayRecordNoInOntListIndex];

            allPossibleAgcfGatewayRecordNoInOntListIndex = Ia.Cl.Model.Default.IncrementListIndexOrRestart(allPossibleAgcfGatewayRecordNoInOntList, allPossibleAgcfGatewayRecordNoInOntListIndex);

            return agcfGatewayRecord;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateServiceRequestServiceServiceSuspensionWithNonNullAccess(out string result)
        {
            bool toTrue, toFalse;
            StringBuilder sb;
            List<string> srsCurrentList, toTrueList, toFalseList;

            toTrue = toFalse = false;

            srsCurrentList = Ia.Ngn.Cl.Model.Data.ServiceRequestService.ServiceSuspensionIsTrueStringNumberList();

            toTrueList = new List<string>(srsCurrentList.Count);
            toFalseList = new List<string>(srsCurrentList.Count);

            sb = new StringBuilder(12 * (srsCurrentList.Count));

            // below: numbers that should be added to SRS barring
            sb.Append("\r\nNumber(s) to be set in SRS as barred: ");

            toTrue = Ia.Ngn.Cl.Model.Data.ServiceRequestService.UpdateServiceSuspensionAndServiceSuspensionTypeIdToSpecifiedSuspensionStateForAServiceStringList(toTrueList, true, Guid.Empty);
            sb.Append("\r\nNumber(s) set?: " + toTrue.ToString());

            // below: numbres that should be removed from SRS barring
            sb.Append("\r\nNumber(s) to be reset in SRS as not barred: ");

            toFalse = Ia.Ngn.Cl.Model.Data.ServiceRequestService.UpdateServiceSuspensionAndServiceSuspensionTypeIdToSpecifiedSuspensionStateForAServiceStringList(toFalseList, false, Guid.Empty);
            sb.Append("\r\nNumber(s) reset?: " + toTrue.ToString());

            result = sb.ToString();

            return toTrue || toFalse;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static List<Ia.Ngn.Cl.Model.ServiceRequestService> ServiceRequestServiceWithAccessesWithNullAgcfEndpointList
        {
            get
            {
                List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList;

                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    serviceRequestServiceList = (from srs in db.ServiceRequestServices join e in db.AgcfEndpoints on Ia.Ngn.Cl.Model.Business.NumberFormatConverter.PrividUser(srs.Service) equals e.PrividUser 
                                                 into gj from u in gj.DefaultIfEmpty()
                                                 where u == null && srs.Access != null
                                                 select srs).ToList();
                }

                return serviceRequestServiceList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static SortedList ComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList()
        {
            SortedList serviceIdList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below: Service different from ServiceRequestService
                serviceIdList = new SortedList((from s in db.Service2s
                                                join srs in db.ServiceRequestServices on s.Id equals srs.Id
                                                where s.AbbriviatedCalling != srs.AbbriviatedCalling
                                                || s.AlarmCall != srs.AlarmCall
                                                || s.CallBarring != srs.CallBarring
                                                || s.CallerId != srs.CallerId
                                                || s.CallForwarding != srs.CallForwarding
                                                || s.CallWaiting != srs.CallWaiting
                                                || s.ConferenceCall != srs.ConferenceCall
                                                || s.InternationalCallingUserControlled != srs.InternationalCallingUserControlled
                                                || s.InternationalCalling != srs.InternationalCalling
                                                || s.SpeedDial != srs.SpeedDial
                                                || s.WakeupCall != srs.WakeupCall
                                                select s.Id).ToDictionary(n => n, n => 1));
            }

            return serviceIdList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static SortedList ComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdTestList(string testServicePrefix)
        {
            SortedList serviceIdList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below: Service different from ServiceRequestService
                serviceIdList = new SortedList((from s in db.Service2s
                                                join srs in db.ServiceRequestServices on s.Id equals srs.Id
                                                where srs.Service.StartsWith(testServicePrefix)
                                                &&
                                                (s.AbbriviatedCalling != srs.AbbriviatedCalling
                                                || s.AlarmCall != srs.AlarmCall
                                                || s.CallBarring != srs.CallBarring
                                                || s.CallerId != srs.CallerId
                                                || s.CallForwarding != srs.CallForwarding
                                                || s.CallWaiting != srs.CallWaiting
                                                || s.ConferenceCall != srs.ConferenceCall
                                                || s.InternationalCallingUserControlled != srs.InternationalCallingUserControlled
                                                || s.InternationalCalling != srs.InternationalCalling
                                                || s.SpeedDial != srs.SpeedDial
                                                || s.WakeupCall != srs.WakeupCall
                                                )
                                                select s.Id).ToDictionary(n => n, n => 1));
            }

            return serviceIdList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void ComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList_ServiceRequestServiceNotInServiceServiceIdList_ServiceNotInServiceRequestServiceServiceIdList(out SortedList complementaryIdSortedList, out SortedList serviceRequestServiceNotInServiceIdList, out SortedList serviceNotInServiceRequestServiceIdList)
        {
            Dictionary<string, int> srsIdDictionary, sIdDictionary;

            complementaryIdSortedList = Ia.Ngn.Cl.Model.Data.Provision.ComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList();

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below: ServiceRequestService dictionary
                srsIdDictionary = (from srs in db.ServiceRequestServices select srs.Id).ToDictionary(n => n, n => 1);

                // below: Service dictionary
                sIdDictionary = (from s in db.Service2s select s.Id).ToDictionary(n => n, n => 1);
            }

            // below: ServiceRequestService not in Service list
            serviceRequestServiceNotInServiceIdList = new SortedList(srsIdDictionary.Count);
            foreach (KeyValuePair<string, int> kvp in srsIdDictionary) if (!sIdDictionary.Contains(kvp)) serviceRequestServiceNotInServiceIdList.Add(kvp.Key, 1);

            // below: Service not in ServiceRequestService list
            serviceNotInServiceRequestServiceIdList = new SortedList(sIdDictionary.Count);
            foreach (KeyValuePair<string, int> kvp in sIdDictionary) if (!srsIdDictionary.Contains(kvp)) serviceNotInServiceRequestServiceIdList.Add(kvp.Key, 1);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static SortedList ServiceRequestServiceAndServiceMismatchAccessServiceIdList()
        {
            SortedList serviceRequestServiceAndServiceMismatchAccessServiceIdList;
            Dictionary<string, int> srsIdDictionary, sIdDictionary;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below: ServiceRequestService dictionary
                srsIdDictionary = (from srs in db.ServiceRequestServices join s in db.Service2s on srs.Id equals s.Id where srs.Access != null && s.Access != null && srs.Access.Id != s.Access.Id select srs.Id).ToDictionary(n => n, n => 1);

                // below: Service dictionary
                sIdDictionary = (from s in db.Service2s join srs in db.ServiceRequestServices on s.Id equals srs.Id where s.Access != null && srs.Access != null && s.Access.Id != srs.Access.Id select s.Id).ToDictionary(n => n, n => 1);
            }

            serviceRequestServiceAndServiceMismatchAccessServiceIdList = new SortedList(srsIdDictionary.Count + sIdDictionary.Count);

            foreach (KeyValuePair<string, int> kvp in srsIdDictionary) serviceRequestServiceAndServiceMismatchAccessServiceIdList.Add(kvp.Key, 1);
            foreach (KeyValuePair<string, int> kvp in sIdDictionary) serviceRequestServiceAndServiceMismatchAccessServiceIdList.Add(kvp.Key, 1);

            return serviceRequestServiceAndServiceMismatchAccessServiceIdList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static SortedList ServiceRequestServiceAndServiceMismatchAccessServiceIdList(string testServicePrefix)
        {
            SortedList serviceRequestServiceAndServiceMismatchAccessServiceIdList;
            Dictionary<string, int> srsIdDictionary, sIdDictionary;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below: ServiceRequestService dictionary
                srsIdDictionary = (from srs in db.ServiceRequestServices join s in db.Service2s on srs.Id equals s.Id where srs.Service.StartsWith(testServicePrefix) && srs.Access != null && s.Access != null && srs.Access.Id != s.Access.Id select srs.Id).ToDictionary(n => n, n => 1);

                // below: Service dictionary
                sIdDictionary = (from s in db.Service2s join srs in db.ServiceRequestServices on s.Id equals srs.Id where s.Service.StartsWith(testServicePrefix) && s.Access != null && srs.Access != null && s.Access.Id != srs.Access.Id select s.Id).ToDictionary(n => n, n => 1);
            }

            serviceRequestServiceAndServiceMismatchAccessServiceIdList = new SortedList(srsIdDictionary.Count + sIdDictionary.Count);

            foreach (KeyValuePair<string, int> kvp in srsIdDictionary)
            {
                if(!serviceRequestServiceAndServiceMismatchAccessServiceIdList.ContainsKey(kvp.Key)) serviceRequestServiceAndServiceMismatchAccessServiceIdList.Add(kvp.Key, 1);
            }

            foreach (KeyValuePair<string, int> kvp in sIdDictionary)
            {
                if (!serviceRequestServiceAndServiceMismatchAccessServiceIdList.ContainsKey(kvp.Key)) serviceRequestServiceAndServiceMismatchAccessServiceIdList.Add(kvp.Key, 1);
            }

            return serviceRequestServiceAndServiceMismatchAccessServiceIdList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void ComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList_ServiceRequestServiceNotInServiceServiceIdList_ServiceNotInServiceRequestServiceServiceIdTestList(string testServicePrefix, out SortedList complementaryIdSortedList, out SortedList serviceRequestServiceNotInServiceIdList, out SortedList serviceNotInServiceRequestServiceIdList)
        {
            Dictionary<string, int> srsIdDictionary, sIdDictionary;

            complementaryIdSortedList = Ia.Ngn.Cl.Model.Data.Provision.ComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdTestList(testServicePrefix);

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below: ServiceRequestService dictionary
                srsIdDictionary = (from srs in db.ServiceRequestServices where srs.Service.StartsWith(testServicePrefix) select srs.Id).ToDictionary(n => n, n => 1);

                // below: Service dictionary
                sIdDictionary = (from s in db.Service2s where s.Service.StartsWith(testServicePrefix) select s.Id).ToDictionary(n => n, n => 1);
            }

            // below: ServiceRequestService not in Service list
            serviceRequestServiceNotInServiceIdList = new SortedList(srsIdDictionary.Count);
            foreach (KeyValuePair<string, int> kvp in srsIdDictionary) if (!sIdDictionary.Contains(kvp)) serviceRequestServiceNotInServiceIdList.Add(kvp.Key, 1);

            // below: Service not in ServiceRequestService list
            serviceNotInServiceRequestServiceIdList = new SortedList(sIdDictionary.Count);
            foreach (KeyValuePair<string, int> kvp in sIdDictionary) if (!srsIdDictionary.Contains(kvp)) serviceNotInServiceRequestServiceIdList.Add(kvp.Key, 1);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
