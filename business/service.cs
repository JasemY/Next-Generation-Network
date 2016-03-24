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

namespace Ia.Ngn.Cl.Model.Business
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Service support class of Next Generation Network'a (NGN's) business model.
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
    public partial class Service
    {
        /// <summary/>
        /// BELOW: IS THIS NAME CORRECT? SupplementaryService?
        public enum SupplementaryService
        {
            CallerId = 1, InternationalCalling, InternationalCallingUserControlled, CallForwarding, CallWaiting, ConferenceCall, WakeupCall, AbbriviatedCalling, ServiceSuspension
        };

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Service() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string ServiceName(string service, int serviceType)
        {
            // below:
            string name;

            name = service;

            if (name == "0") name = "";

            return name;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class KuwaitNgnArea
        {
            public KuwaitNgnArea() { }

            public int Id { get; set; }
            public string Name { get; set; }
            public string ArabicName { get; set; }
            public string NameArabicName 
            { 
                get
                {
                    return Name + " (" + ArabicName +")";
                }
            }
            public string Symbol { get; set; }
            public string ServiceRequestAddressProvinceAreaName { get; set; }
            
            public virtual Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Site Site { get; set; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool NumberIsWithinAllowedDomainList(long number)
        {
            string numberString;

            numberString = number.ToString();

            return NumberIsWithinAllowedDomainList(numberString);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> SelectListOfNumbersWithinAllowedDomainList(List<int> inNumberList)
        {
            string numberString;
            List<int> numberList;

            numberList = new List<int>();

            if (inNumberList.Count > 0)
            {
                numberList = new List<int>(inNumberList.Count);

                foreach (int i in inNumberList)
                {
                    numberString = i.ToString();

                    if (NumberIsWithinAllowedDomainList(numberString)) numberList.Add(i);
                }
            }

            return numberList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool NumberIsWithinAllowedDomainList(string number)
        {
            bool isWithinAllowedDomains;
            int i;

            if (number.Length >= 4)
            {
                i = int.Parse(number.Substring(0, 4));

                if (Ia.Ngn.Cl.Model.Data.Service.FourDigitNumberDomainList.Contains(i)) isWithinAllowedDomains = true;
                else isWithinAllowedDomains = false;
            }
            else isWithinAllowedDomains = false;

            return isWithinAllowedDomains;
        }

        ////////////////////////////////////////////////////////////////////////////    
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public static class ServiceExtension
    {
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// <see cref="http://stackoverflow.com/questions/8910588/how-to-override-contains"/>
        /// </summary>
        public static bool Contains(this List<Ia.Ngn.Cl.Model.Business.ServiceRequest.NumberSerial> sourceList, int number, int serial)
        {
            return ((from q in sourceList where q.Number == number && q.Serial == serial select q) != null);
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static List<long> IdList(this List<Ia.Ngn.Cl.Model.Business.ServiceRequest.NumberSerial> sourceList)
        {
            return ((from q in sourceList select (long)((long)q.Number * 100 + q.Serial)).ToList());
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
