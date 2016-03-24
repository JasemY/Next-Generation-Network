using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

namespace Ia.Ngn.Cl.Model.Business
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Service Request Type support class of Next Generation Network'a (NGN's) business model.
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
    public partial class ServiceRequestType
    {
        /// <summary/>
        public ServiceRequestType() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> PossibleChangedNumberList(List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList)
        {
            int i;
            List<int> numberList;
            List<string> stringNumberList;

            numberList = new List<int>();
            stringNumberList = (from srt in serviceRequestTypeList where srt.TypeId == 11 select srt.Value).ToList();

            if (stringNumberList.Count > 0)
            {
                foreach(string u in stringNumberList)
                {
                    if(int.TryParse(u, out i)) numberList.Add(i);
                }
            }
            else
            {
            }

            return numberList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Access ExtractAccess(int serviceRequestId, List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList)
        {
            Ia.Ngn.Cl.Model.Access access;
            List<Ia.Ngn.Cl.Model.ServiceRequestType> subtypeSrtList;

            subtypeSrtList = (from srt in serviceRequestTypeList where srt.ServiceRequest.Id == serviceRequestId select srt).ToList();

            access = ExtractAccess(subtypeSrtList);

            return access;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static Ia.Ngn.Cl.Model.Access ExtractAccess(List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList)
        {
            int areaId, ponNumber, ontNumber;
            Dictionary<int, string> typeDictionary;
            Ia.Ngn.Cl.Model.Access access;

            typeDictionary = TypeDictionary(serviceRequestTypeList);

            Ia.Ngn.Cl.Model.Business.Ont.ExtractOntNameWithValidSymbolAndLegalFormatForPonAndOntFromDictionaryValueList(typeDictionary, out areaId, out ponNumber, out ontNumber);

            access = Ia.Ngn.Cl.Model.Access.Read(areaId, ponNumber, ontNumber);

            return access;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Access ExtractAccess(Ia.Ngn.Cl.Model.Ngn db, string value)
        {
            int areaId, ponNumber, ontNumber;
            Dictionary<int, string> typeDictionary;
            Ia.Ngn.Cl.Model.Access access;

            typeDictionary = new Dictionary<int, string>(1);
            typeDictionary.Add(1, value);

            Ia.Ngn.Cl.Model.Business.Ont.ExtractOntNameWithValidSymbolAndLegalFormatForPonAndOntFromDictionaryValueList(typeDictionary, out areaId, out ponNumber, out ontNumber);

            access = Ia.Ngn.Cl.Model.Access.Read(db, areaId, ponNumber, ontNumber);

            return access;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<int, string> TypeDictionary(List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList)
        {
            Dictionary<int, string> typeDictionary;

            typeDictionary = new Dictionary<int, string>(63); // <serviceRequestType> <typeList> <type> has about 63 types max

            foreach (Ia.Ngn.Cl.Model.ServiceRequestType serviceRequestType in serviceRequestTypeList)
            {
                typeDictionary.Add(serviceRequestType.TypeId, serviceRequestType.Value);
            }

            return typeDictionary;
        }

        ////////////////////////////////////////////////////////////////////////////    
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}