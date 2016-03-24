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
using System.Linq.Expressions;
using System.Reflection;

namespace Ia.Ngn.Cl.Model.Business
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Provision support class of Next Generation Network'a (NGN's) business model.
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
        private static int sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdListIndex;
        private static SortedList sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Provision() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string SequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceTestListItem(string testServicePrefix, out string result)
        {
            string service;

            if (sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList == null || sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdListIndex == 0)
            {
                sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList = DiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdTestList(testServicePrefix);

                // below:
                sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdListIndex = 0;
            }

            if (sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList.Count > 0)
                service = sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList.GetKey(sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdListIndex).ToString();
            else service = null;

            result = "(" + sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdListIndex + "/" + sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList.Count + ") ";

            if (sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList.Count > 0)
                Ia.Cl.Model.Default.IncrementIndexOrReset(sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList.Count, ref sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdListIndex);

            return service;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string SequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceListItem(out string result)
        {
            string service;

            if (sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList == null || sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdListIndex == 0)
            {
                sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList = DiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList;

                // below:
                sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdListIndex = 0;
            }

            if (sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList.Count > 0)
                service = sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList.GetKey(sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdListIndex).ToString();
            else service = null;

            result = "(" + sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdListIndex + "/" + sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList.Count + ") ";

            if (sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList.Count > 0)
                Ia.Cl.Model.Default.IncrementIndexOrReset(sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList.Count, ref sequentialDiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdListIndex);

            return service;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static SortedList DiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList
        {
            get
            {
                SortedList sortedList, complementaryIdSortedList, serviceRequestServiceNotInServiceServiceIdSortedList, serviceNotInServiceRequestServiceServiceIdSortedList;

                Ia.Ngn.Cl.Model.Data.Provision.ComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList_ServiceRequestServiceNotInServiceServiceIdList_ServiceNotInServiceRequestServiceServiceIdList(out complementaryIdSortedList, out serviceRequestServiceNotInServiceServiceIdSortedList, out serviceNotInServiceRequestServiceServiceIdSortedList);

                sortedList = new SortedList(complementaryIdSortedList.Count + serviceRequestServiceNotInServiceServiceIdSortedList.Count + serviceNotInServiceRequestServiceServiceIdSortedList.Count);

                foreach (string s in complementaryIdSortedList.Keys) sortedList[s] = 1;
                foreach (string s in serviceRequestServiceNotInServiceServiceIdSortedList.Keys) sortedList[s] = 1;
                foreach (string s in serviceNotInServiceRequestServiceServiceIdSortedList.Keys) sortedList[s] = 1;

                return sortedList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static SortedList DiscrepancyAndComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdTestList(string testServicePrefix)
        {
            SortedList sortedList, complementaryIdSortedList, serviceRequestServiceNotInServiceServiceIdSortedList, serviceNotInServiceRequestServiceServiceIdSortedList;

            Ia.Ngn.Cl.Model.Data.Provision.ComplementaryServiceMismatchBetweenServiceRequestServiceAndServiceServiceIdList_ServiceRequestServiceNotInServiceServiceIdList_ServiceNotInServiceRequestServiceServiceIdTestList(testServicePrefix, out complementaryIdSortedList, out serviceRequestServiceNotInServiceServiceIdSortedList, out serviceNotInServiceRequestServiceServiceIdSortedList);

            sortedList = new SortedList(complementaryIdSortedList.Count + serviceRequestServiceNotInServiceServiceIdSortedList.Count + serviceNotInServiceRequestServiceServiceIdSortedList.Count);

            foreach (string s in complementaryIdSortedList.Keys) sortedList[s] = 1;
            foreach (string s in serviceRequestServiceNotInServiceServiceIdSortedList.Keys) sortedList[s] = 1;
            foreach (string s in serviceNotInServiceRequestServiceServiceIdSortedList.Keys) sortedList[s] = 1;

            return sortedList;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
