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
using System.Reflection;

namespace Ia.Ngn.Cl.Model.Business
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Default general support class of Next Generation Network'a (NGN's) business model.
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
    public partial class Default
    {
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// ASP.NET State Management
        /// below: remove later to default.cs or state.cs
        /// <remarks>https://msdn.microsoft.com/en-us/library/z1hkazw7(v=vs.100).aspx</remarks>
        /// </summary>
        public static bool Application(string name, int lifeInMinutes, object o)
        {
            bool valueStored;
            DateTime expiration;

            if (name != string.Empty)
            {
                expiration = DateTime.UtcNow.AddMinutes(3 * 60 + lifeInMinutes);

                HttpContext.Current.Application[name + "|" + lifeInMinutes.ToString()] = o;

                valueStored = true;
            }
            else valueStored = false;

            return valueStored;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static object Application(string name)
        {
            string expirationString;
            DateTime expiration;
            object o;

            o = null;

            // below: loop through keys to find the one that starts with name
            foreach(string s in HttpContext.Current.Application.AllKeys)
            {
                if(s.Contains(name + "|"))
                {
                    expirationString = Ia.Cl.Model.Default.Match(s, @"\|(.+)");

                    expiration = DateTime.Parse(expirationString);

                    if (expiration < DateTime.UtcNow.AddHours(3))
                    {
                        // below: did not expire

                        o = HttpContext.Current.Application[s];
                    }
                    else o = null;
                }
            }

            return o;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// RecordState holds the current state of the record according to user and system interactions with it. It could be used as an
        /// indicator to define the current state of the record and how it should be handle in state monitoring execution cycles.
        /// </summary>
        public enum RecordState
        {
            Undefined = 0, Synchronized = 10, Synchronize = 20, Modified = 30, Updated = 40, Etc = 50
        };

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Default() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string VendorNameFromId(int id)
        {
            string name;

            switch (id)
            {
                case 1: name = "Nokia"; break;
                case 2: name = "Huawei"; break;
                case 3: name = "Nokia-Siemens"; break;
                default: name = ""; break;
            }

            return name;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string ShortVendorNameFromId(int id)
        {
            string name;

            switch (id)
            {
                case 1: name = "No"; break;
                case 2: name = "Hu"; break;
                case 3: name = "NS"; break;
                default: name = ""; break;
            }

            return name;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int VendorIdFromName(string name)
        {
            int id;

            switch (name)
            {
                case "Nokia": id = 1; break;
                case "ALCL": id = 1; break;
                case "Huawei": id = 2; break;
                case "Nokia-Siemens": id = 3; break;
                default: id = 0; break;
            }

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string CorrectCustomerAddress(string addressString)
        {
            addressString = addressString.Trim();

            //line = Ia.Cl.Model.Language.ConvertSingleLatinDigitsToArabicWordEquivalents(line);
            //line = Ia.Cl.Model.Language.RemoveNonArabicAndNonArabicExtendedLettersAndDigits(line);
            //line = Ia.Cl.Model.Language.CorrectArabicNameNounStringFormat(line);
            // to do line = Ia.Cl.Model.Language.RemoveTitlesFromNames(line);
            // to do line = Ia.Cl.Model.Language.CorrectArabicNameNounFormat(line);
            // to do line = Ia.Cl.Model.Language.CorrectArabicNonNameNounStringFormat(line);
            //line = Ia.Cl.Model.Language.RemoveWrongSpaceBetweenArabicDefinitArticleAndItsWord(line);
            //line = Regex.Replace(line, @"\s+", @" ");

            addressString = Regex.Replace(addressString, @"[0]{2,}", "0"); // "000" to "0"

            addressString = addressString.Replace("جادة جاده", "جادة ");
            addressString = addressString.Replace("جادة جادة", "جادة ");
            addressString = addressString.Replace("شارع الاول", "شارع 1");
            addressString = addressString.Replace("شارع الثانى", "شارع 2");
            addressString = addressString.Replace("شارع الثالث", "شارع 3");
            addressString = addressString.Replace("شارع الرابع", "شارع 4");
            addressString = addressString.Replace("شارع الخامس", "شارع 5");
            addressString = addressString.Replace("شارع السادس", "شارع 6");
            addressString = addressString.Replace("شارع السابع", "شارع 7");
            addressString = addressString.Replace("شارع الثامن", "شارع 8");
            addressString = addressString.Replace("شارع التاسع", "شارع 9");

            addressString = addressString.Trim();

            return addressString;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string CorrectCustomerAddressMissingProvinceArea(string service, string addressString)
        {
            // correct some missing information in address lines based on service number
            string domain;

            domain = service.Substring(0, 4);

            if (domain == "2453" || domain == "2454") addressString = "الجهراء سعد العبدالله" + addressString;
            else if (domain == "2466") addressString = "الفروانية القيروان" + addressString;
            else if (domain == "2435" || domain == "2436") addressString = "الفروانية عبدالله المبارك" + addressString;
            else if (domain == "2363") addressString = "الأحمدي فهد الأحمد" + addressString;
         
            return addressString;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string CorrectCustomerName(string line)
        {
            line = line.Trim();
            //line = Ia.Cl.Model.Language.ConvertSingleLatinDigitsToArabicWordEquivalents(line);
            line = Ia.Cl.Model.Language.RemoveNonArabicAndNonArabicExtendedLettersAndDigits(line);
            line = Ia.Cl.Model.Language.CorrectArabicNameNounStringFormat(line);
            // to do line = Ia.Cl.Model.Language.RemoveTitlesFromNames(line);
            // to do line = Ia.Cl.Model.Language.CorrectArabicNameNounFormat(line);
            // to do line = Ia.Cl.Model.Language.CorrectArabicNonNameNounStringFormat(line);
            line = Ia.Cl.Model.Language.RemoveWrongSpaceBetweenArabicDefinitArticleAndItsWord(line);
            line = Regex.Replace(line, @"\s+", @" ");
            line = line.Trim();

            return line;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static DataTable ReturnDataTableOfServiceAdministrativeStateOfANumberInOldNgnDatabase(long dn)
        {
            string sql;
            DataTable dt;
            Ia.Cl.Model.Db.SqlServer ngn;

            sql = @"SELECT sa.state, srs.active FROM ia_service_administrative AS sa LEFT OUTER JOIN ia_service_request_service AS srs ON srs.dn = sa.dn WHERE (sa.dn = " + dn + ")";
            dt = null;

            try
            {
                ngn = new Ia.Cl.Model.Db.SqlServer(ConfigurationManager.ConnectionStrings["DefaultConnectionToNgn"].ConnectionString);

                dt = ngn.Select(sql);
            }
            catch (Exception)
            {
            }

            return dt;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void UpdateServiceAdministrativeStateOfANumberInOldNgnDatabase(long dn, string state)
        {
            string sql;
            Ia.Cl.Model.Db.SqlServer ngn;

            sql = @"UPDATE ia_service_administrative SET state = " + state + " WHERE dn = " + dn;

            try
            {
                ngn = new Ia.Cl.Model.Db.SqlServer(ConfigurationManager.ConnectionStrings["DefaultConnectionToNgn"].ConnectionString);

                ngn.Sql(sql);
            }
            catch (Exception)
            {
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int ChangeOldSevenDigitNumbersToEightDigitFormat(int o)
        {
            // below: take an old 7 digit number and covert it to the new 8 digit number according to plan

            int n;

            // below: check if it is already an 8 digit number
            if (o >= 10000000) n = o;

            // 2 (MOC):
            else if (
            (o >= 2000000 && o <= 2999999) ||
            (o >= 3000000 && o <= 3999999) ||
            (o >= 4100000 && o <= 4399999) ||
            (o >= 4500000 && o <= 4999999) ||
            (o >= 5000000 && o <= 5009999) ||
            (o >= 5030000 && o <= 5049999) ||
            (o >= 5200000 && o <= 5499999) ||
            (o >= 5510000 && o <= 5539999) ||
            (o >= 5600000 && o <= 5699999) ||
            (o >= 5710000 && o <= 5779999)) { n = 20000000 + o; }

            //6 (Wataniya):
            else if (
            (o >= 5010000 && o <= 5029999) ||
            (o >= 5050000 && o <= 5099999) ||
            (o >= 5100000 && o <= 5199999) ||
            (o >= 5500000 && o <= 5509999) ||
            (o >= 5540000 && o <= 5599999) ||
            (o >= 5700000 && o <= 5709999) ||
            (o >= 5780000 && o <= 5799999) ||
            (o >= 5800000 && o <= 5999999) ||
            (o >= 6000000 && o <= 6999999) ||
            (o >= 7000000 && o <= 7019999) ||
            (o >= 7030000 && o <= 7099999) ||
            (o >= 7700000 && o <= 7769999) ||
            (o >= 7780000 && o <= 7799999)) { n = 60000000 + o; }

            //1 (MOC):
            else if (o >= 800000 && o <= 899999) { n = 1000000 + o; }

            //9 (Zain):
            else if (
            (o >= 7020000 && o <= 7029999) ||
            (o >= 7100000 && o <= 7699999) ||
            (o >= 7800000 && o <= 7999999) ||
            (o >= 9000000 && o <= 9999999) ||
            (o >= 4400000 && o <= 4499999) ||
            (o >= 4000000 && o <= 4099999)) { n = 90000000 + o; }

            else n = o;

            return n;

            /*
New numbering list

Add digit,Old Numbers Ranges,Operator
,From,To,
2,2-000000,2-999999,MOC
2,3-000000,3-999999,MOC
9,40-00000,40-99999,Zain
2,41-00000,43-99999,MOC
9,44-00000,44-99999,Zain
2,45-00000,49-99999,MOC
2,500-0000,500-9999,MOC
6,501-0000,502-9999,Wataniya
2,503-0000,504-9999,MOC
6,505-0000,509-9999,Wataniya
6,51-00000,51-99999,Wataniya
2,52-00000,54-99999,MOC
6,550-0000,550-9999,Wataniya
2,551-0000,553-9999,MOC
6,554-0000,559-9999,Wataniya
2,56-00000,56-99999,MOC
6,570-0000,570-9999,Wataniya
2,571-0000,577-9999,MOC
6,578-0000,579-9999,Wataniya
6,58-00000,59-99999,Wataniya
6,6-000000,6-999999,Wataniya
6,700-0000,701-9999,Wataniya
9,702-0000,702-9999,Zain
6,703-0000,709-9999,Wataniya
9,71-00000,76-99999,Zain
6,770-0000,776-9999,Wataniya
6,778-0000,779-9999,Wataniya
9,78-00000,79-99999,Zain
1,800-000,899-999,MOC
9,9-000000,9-999999,Zain


Example: the number 2123456 will become 2 2123456.

Notice: Unchanged numbers:
•,The international numbers outside Kuwait do not change and need the prefix 00 followed by the country code. For example, for the United Kingdom, dial 00 44 1234567890.
•,The country code for Kuwait 965 stays the same (for international incoming calls).
•,The 3 digits numbers do not change (numbers from 100 to 179). For example, for the Inquiry Directory, dial 101.
•,The emergency number in Kuwait 777 does not change.
             */
        }

        ////////////////////////////////////////////////////////////////////////////    
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Number Format Covnerter for Nokia and Huaweri Number Formats for the Optical Fiber Network - Kuwait
    /// </summary>
    /// 
    /// <remarks> 
    /// Copyright © 2001-2015 Jasem Y. Al-Shamlan (info@ia.com.kw), Internet Applications - Kuwait. All Rights Reserved.
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
    public class NumberFormatConverter
    {
        /// <summary/>
        public NumberFormatConverter()
        {
            /*
        <Dn>+96522239100</Dn>
        <PrividUser>priv_96522239100</PrividUser>
        <PartyId>+96522239501</PartyId>
        <PrimaryPUID>+96522239501</PrimaryPUID>
        <aid>+96522239501</aid>
        <PublicUID>+96522239100@ims.moc1.kw</PublicUID>
        <PrivateId>priv_96522239100</PrivateId>
        <Puid>sip:+96522239100</Puid>
        <PridUser>priv_96522239100</PridUser>
             * 
             *             impu = "tel:+" + Ia.Ngn.Cl.Model.Data.Service.CountryCode + number;

             */
        }

        /// <summary/>
        public static string Dn(string service)
        {
            return "+965" + service;
        }

        /// <summary/>
        public static string PrividUser(string service)
        {
            return "priv_965" + service;
        }

        /// <summary/>
        public static string PartyId(string service)
        {
            return "+965" + Service(service);
        }

        /// <summary/>
        public static string PrimaryPuid(string service)
        {
            return "+965" + service;
        }

        /// <summary/>
        public static string Aid(string service)
        {
            return "+965" + service;
        }

        /// <summary/>
        public static string PublicUid(string service)
        {
            return "+965" + service + "@ims.moc1.kw";
        }

        /// <summary/>
        public static string PrivateId(string service)
        {
            return "priv_965" + service;
        }

        /// <summary/>
        public static string Puid(string service)
        {
            return "sip:+965" + service;
        }

        /// <summary/>
        public static string PridUser(string service)
        {
            return "priv_965" + service;
        }

        /// <summary/>
        public static string Impu(int number)
        {
            return "tel:+" + Ia.Ngn.Cl.Model.Data.Service.CountryCode + number;
        }

        /// <summary/>
        public static string Impu(string service)
        {
            return "tel:+" + Ia.Ngn.Cl.Model.Data.Service.CountryCode + Service(service);
        }

        /// <summary/>
        public static string ImpuSipDomain(string number)
        {
            return "sip:+" + Ia.Ngn.Cl.Model.Data.Service.CountryCode + number + "@ims.moc.kw";
        }

        /// <summary/>
        public static int Number(string service)
        {
            int i, number;

            service = Service(service);

            number = int.TryParse(service, out i) ? i : 0;

            return number;
        }

        /// <summary/>
        public static string Service(string someNumberFormat)
        {
            string s;

            if (Regex.IsMatch(someNumberFormat, "tel:")) s = someNumberFormat.Replace("tel:+" + Ia.Ngn.Cl.Model.Data.Service.CountryCode, "");
            else if (someNumberFormat.Contains("priv_965")) s = someNumberFormat.Replace("priv_965", "");
            else if (someNumberFormat.Contains("+965")) s = someNumberFormat.Replace("+965", "");
            else if (Regex.IsMatch(someNumberFormat, @"\d{8}")) s = someNumberFormat; // order important
            else s = Ia.Cl.Model.Default.Match(someNumberFormat, @".+(\d{8})");

            return s;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
