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

namespace Ia.Ngn.Cl.Model.Business.Nokia
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Optical Fiber Network Management Intranet Portal (OFN) support class for Nokia's Next Generation Network (NGN) business model.
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
        // below: this is the almost official GwId range used
        private const int gwIdFirst = 2048;
        private const int gwIdLast = 65535;

        private static int allPossibleGwIdListIndex, agcfGatewayRecordGwIdListIndex, allPossibleServiceNumbersWithinNetworkListIndex, agcfEndpointPrividUserListIndex;
        private static int sequentialServiceRequestServiceServiceSuspensionWithNonNullAccessIndex;
        private static List<int> allPossibleGwIdList, agcfGatewayRecordGwIdList;
        private static List<int> allPossibleServiceNumbersWithinNetworkList;
        private static List<string> agcfEndpointPrividUserList;

        /// <summary/>
        public static int FirstGatewayId { get { return gwIdFirst; } }
        /// <summary/>
        public static int LastGatewayId { get { return gwIdLast; } }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Ims()
        {
        }

        public static string AnyEdgeRouter { get { return TECICS01; } }

        public static string TECICS01 { get { return "TECICS01"; } }

        public static string SKBICS02 { get { return "SKBICS02"; } }
        
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string AgcfSipIaPort(int imsService)
        {
            // see ImsServiceFromAgcfSipIaPort()
            string agcfSipIaPort;

            switch (imsService)
            {
                case 0: agcfSipIaPort = "agcf-stdn.imsgroup0-00" + imsService; break;
                case 1:
                case 2:
                case 3: agcfSipIaPort = "agcf-stdo.imsgroup0-00" + imsService; break;
                case 4:
                case 5:
                case 6: agcfSipIaPort = "agcf-stdp.imsgroup0-00" + imsService; break;
                case 7:
                case 8:
                case 9: agcfSipIaPort = "agcf-stdq.imsgroup0-00" + imsService; break;
                default: agcfSipIaPort = null; break;
            }

            return agcfSipIaPort;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int ImsServiceFromAgcfSipIaPort(string agcfSipIaPort)
        {
            // see AgcfSipIaPort()
            int imsService;

            switch (agcfSipIaPort)
            {
                case "agcf-stdn.imsgroup0-000": imsService = 0; break;
                case "agcf-stdo.imsgroup0-001": imsService = 1; break;
                case "agcf-stdo.imsgroup0-002": imsService = 2; break;
                case "agcf-stdo.imsgroup0-003": imsService = 3; break;
                case "agcf-stdp.imsgroup0-004": imsService = 4; break;
                case "agcf-stdp.imsgroup0-005": imsService = 5; break;
                case "agcf-stdp.imsgroup0-006": imsService = 6; break;
                case "agcf-stdq.imsgroup0-007": imsService = 7; break;
                case "agcf-stdq.imsgroup0-008": imsService = 8; break;
                case "agcf-stdq.imsgroup0-009": imsService = 9; break;
                default: imsService = -1; break;
            }

            return imsService;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string ImsFsdb(int imsService)
        {
            int fsdb;

            switch (imsService)
            {
                case 0: fsdb = 0; break;
                case 1:
                case 2:
                case 3: fsdb = 1; break;
                case 4:
                case 5:
                case 6: fsdb = 2; break;
                case 7:
                case 8:
                case 9: fsdb = 3; break;
                default: fsdb = 0; break;
            }

            return "fsdb" + fsdb;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string AlternateFsdbFqdn(string edgeRouter, string fsdb)
        {
            string alternateFsdbFqdn;

            if (edgeRouter == TECICS01)
            {
                switch (fsdb)
                {
                    case "fsdb0": alternateFsdbFqdn = "10.16.0.88"; break;
                    case "fsdb1": alternateFsdbFqdn = "10.16.0.89"; break;
                    case "fsdb2": alternateFsdbFqdn = "10.16.0.90"; break;
                    case "fsdb3": alternateFsdbFqdn = "10.16.0.91"; break;
                    default: alternateFsdbFqdn = string.Empty; break;
                }
            }
            else if(edgeRouter == SKBICS02)
            {
                switch (fsdb)
                {
                    case "fsdb0": alternateFsdbFqdn = "10.16.0.9"; break;
                    case "fsdb1": alternateFsdbFqdn = "10.16.0.10"; break;
                    case "fsdb2": alternateFsdbFqdn = "10.16.0.11"; break;
                    case "fsdb3": alternateFsdbFqdn = "10.16.0.12"; break;
                    default: alternateFsdbFqdn = string.Empty; break;
                }
            }
            else throw new Exception("Unknown edgeRouter") { };

            return alternateFsdbFqdn;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string AssocOtasRealm(string edgeRouter, int imsService)
        {
            string assocOtasRealm;

            if (edgeRouter == TECICS01)
            {
                assocOtasRealm = "stas-stdn.fsimsgroup0-00" + imsService + @".tecics01.ims.moc1.kw";
            }
            else if (edgeRouter == SKBICS02)
            {
                assocOtasRealm = "stas-stdn.fsimsgroup0-00" + imsService + @".skbics01.ims.moc1.kw";
            }
            else throw new Exception("Unknown edgeRouter") { };

            return assocOtasRealm;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string AlternateOtasRealm(string edgeRouter, int imsService)
        {
            string alternateOtasRealm;

            if (edgeRouter == TECICS01)
            {
                alternateOtasRealm = "stas-stdn.fsimsgroup0-00" + imsService + @".skbics01.ims.moc1.kw";
            }
            else if (edgeRouter == SKBICS02)
            {
                alternateOtasRealm = "stas-stdn.fsimsgroup0-00" + imsService + @".tecics01.ims.moc1.kw";
            }
            else throw new Exception("Unknown edgeRouter") { };

            return alternateOtasRealm;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string EdgeRouterFromAgcfGatewayRecord(bool isPrimary)
        {
            string edgeRouter;

            if (isPrimary) edgeRouter = Ia.Ngn.Cl.Model.Business.Nokia.Ims.AnyEdgeRouter;
            else edgeRouter = Ia.Ngn.Cl.Model.Business.Nokia.Ims.SKBICS02;

            return edgeRouter;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int ServiceProfileNumber(int imsService)
        {
            int serviceProfileNumber;

            switch (imsService)
            {
                case 0: serviceProfileNumber = 2; break;
                case 1:
                case 2:
                case 3: serviceProfileNumber = 10; break;
                case 4:
                case 5:
                case 6: serviceProfileNumber = 26; break;
                case 7:
                case 8:
                case 9: serviceProfileNumber = 42; break;
                default: serviceProfileNumber = 0; break;
            }

            return serviceProfileNumber;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int SequentialGwIdToReadAgcfGatewayRecordData
        {
            get
            {
                int number;

                if (allPossibleGwIdList == null || allPossibleGwIdListIndex == 0)
                {
                    allPossibleGwIdList = Ia.Ngn.Cl.Model.Data.Nokia.Ims.AllPossibleGatewayIdList;

                    // below:
                    allPossibleGwIdListIndex = 0;// Ia.Cl.Model.Default.Random(allPossibleGwIdList.Count - 1);
                }

                number = (int)allPossibleGwIdList[allPossibleGwIdListIndex];

                allPossibleGwIdListIndex = Ia.Cl.Model.Default.IncrementListIndexOrRestart(allPossibleGwIdList, allPossibleGwIdListIndex);

                return number;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int SequentialServiceNumberToReadServiceData
        {
            get
            {
                int number;

                if (allPossibleServiceNumbersWithinNetworkList == null || allPossibleServiceNumbersWithinNetworkListIndex == 0)
                {
                    allPossibleServiceNumbersWithinNetworkList = Ia.Ngn.Cl.Model.Data.Service.AllPossibleServiceNumberListWithinNokiaSwitch;

                    // below: start at a random index in ArrayList
                    allPossibleServiceNumbersWithinNetworkListIndex = 0;// Ia.Cl.Model.Default.Random(allPossibleServiceNumbersWithinNetworkList.Count - 1);
                }

                number = (int)allPossibleServiceNumbersWithinNetworkList[allPossibleServiceNumbersWithinNetworkListIndex];

                allPossibleServiceNumbersWithinNetworkListIndex = Ia.Cl.Model.Default.IncrementListIndexOrRestart(allPossibleServiceNumbersWithinNetworkList, allPossibleServiceNumbersWithinNetworkListIndex);

                return number;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string SequentialPrividUserFromAgcfEndpointToReadSubPartyAndSubscriberData
        {
            get
            {
                string prividUser;

                if (agcfEndpointPrividUserList == null || agcfEndpointPrividUserListIndex == 0)
                {
                    agcfEndpointPrividUserList = Ia.Ngn.Cl.Model.Data.Nokia.AgcfEndpoint.ReadPrividUserList();

                    // below: start at a random index in ArrayList
                    agcfEndpointPrividUserListIndex = 0;// Ia.Cl.Model.Default.Random(agcfEndpointPrividUserList.Count - 1);
                }

                prividUser = agcfEndpointPrividUserList[agcfEndpointPrividUserListIndex];

                agcfEndpointPrividUserListIndex = Ia.Cl.Model.Default.IncrementListIndexOrRestart(agcfEndpointPrividUserList, agcfEndpointPrividUserListIndex);

                return prividUser;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}