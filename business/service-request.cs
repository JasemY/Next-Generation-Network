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
    /// Service Request support class of Next Generation Network'a (NGN's) business model.
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
    public partial class ServiceRequest
    {
        /// <summary/>
        public ServiceRequest() { }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public struct NumberSerial
        {
            public long Id
            {
                get { return (long)Number * 100 + Serial; }
            }
            public int Number { get; set; }
            public int Serial { get; set; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Business.ServiceAddress StatisticalServiceAddress(string service, string customerAddress, out string level)
        {
            int i, block;
            string provinceArea;
            Match match;
            Ia.Ngn.Cl.Model.Business.ServiceAddress serviceAddress;

            serviceAddress = new ServiceAddress();

            serviceAddress.Service = service;

            if (customerAddress != null)
            {
                customerAddress = Ia.Ngn.Cl.Model.Business.Default.CorrectCustomerAddress(customerAddress);

                // below: special handeling needed here
                customerAddress = Ia.Ngn.Cl.Model.Business.Default.CorrectCustomerAddressMissingProvinceArea(service, customerAddress);


                // ',قطعة 4 قسيمة 538,'
                // below:
                // (.+),قطعة
                // ? entries
                match = Regex.Match(customerAddress, @"^(.+),قطعة", RegexOptions.Singleline);

                if (match.Success)
                {
                    level = "1";

                    provinceArea = match.Groups[1].Value;
                    serviceAddress.AreaId = (from a in Ia.Ngn.Cl.Model.Data.Service.KuwaitNgnAreaList where provinceArea == a.ServiceRequestAddressProvinceAreaName select a.Id).SingleOrDefault();
                }
                else
                {
                    serviceAddress.AreaId = 0;

                    /*
                    domain = int.Parse(service.Substring(0, 4));

                    serviceAddress.AreaId = (from a in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.RouterList where a.DomainList.Contains(domain) select a.Id).SingleOrDefault();

                    provinceArea = (from a in Ia.Ngn.Cl.Model.Data.Service.KuwaitNgnAreaList where a.Site.Routers.Any(c => c.DomainListString.Contains(service.Substring(0, 4))) select a.ArabicName).SingleOrDefault();

                    customerAddress = provinceArea + customerAddress;
                     */ 
                }

                // below:
                // الجهراء الجهراء المدينة,قطعة 79 شارع 5 جادة 5 قسيمة 17, منزل 17
                match = Regex.Match(customerAddress, @"^(.+),قطعة (.+) شارع (.+) جادة (.+) قسيمة (.+), منزل (.+)$", RegexOptions.Singleline);

                if (match.Success)
                {
                    level = "1-";

                    serviceAddress.Block = int.TryParse(Ia.Cl.Model.Default.RemoveNonNumericCharacters(match.Groups[2].Value), out block) ? block : 0;
                    serviceAddress.Street = match.Groups[3].Value;
                    serviceAddress.Boulevard = match.Groups[4].Value;
                    serviceAddress.PremisesOld = match.Groups[5].Value;
                    serviceAddress.PremisesNew = match.Groups[6].Value;
                }
                else
                {
                    level = "2";

                    // below:
                    // الجهراء الجهراء المدينة,قطعة 79 شارع 5 قسيمة 17, منزل 17
                    match = Regex.Match(customerAddress, @"^(.+),قطعة (.+) شارع (.+) قسيمة (.+), منزل (.+)$", RegexOptions.Singleline);

                    if (match.Success)
                    {
                        level = "2-";

                        serviceAddress.Block = int.TryParse(Ia.Cl.Model.Default.RemoveNonNumericCharacters(match.Groups[2].Value), out block) ? block : 0;
                        serviceAddress.Street = match.Groups[3].Value;
                        serviceAddress.Boulevard = null;
                        serviceAddress.PremisesOld = match.Groups[4].Value;
                        serviceAddress.PremisesNew = match.Groups[5].Value;
                    }
                    else
                    {
                        level = "3";

                        // below:
                        // الجهراء الجهراء المدينة,قطعة 79 شارع 5, منزل 17
                        match = Regex.Match(customerAddress, @"^(.+),قطعة (.+) شارع (.+), منزل (.+)$", RegexOptions.Singleline);

                        if (match.Success)
                        {
                            level = "3-";

                            serviceAddress.Block = int.TryParse(Ia.Cl.Model.Default.RemoveNonNumericCharacters(match.Groups[2].Value), out block) ? block : 0;
                            serviceAddress.Street = match.Groups[3].Value;
                            serviceAddress.Boulevard = null;
                            serviceAddress.PremisesOld = null;
                            serviceAddress.PremisesNew = match.Groups[4].Value;
                        }
                        else
                        {
                            level = "4";

                            // below:
                            // الجهراء الجهراء المدينة,قطعة 79 شارع 5 قسيمة 17
                            match = Regex.Match(customerAddress, @"^(.+),قطعة (.+) شارع (.+) قسيمة (.+),$", RegexOptions.Singleline);

                            if (match.Success)
                            {
                                level = "4-";

                                serviceAddress.Block = int.TryParse(Ia.Cl.Model.Default.RemoveNonNumericCharacters(match.Groups[2].Value), out block) ? block : 0;
                                serviceAddress.Street = match.Groups[3].Value;
                                serviceAddress.Boulevard = null;
                                serviceAddress.PremisesOld = match.Groups[4].Value;
                                serviceAddress.PremisesNew = null;
                            }
                            else
                            {
                                level = "5";

                                // below:
                                // الجهراء الجهراء المدينة,قطعة 79 شارع 5 جادة 5
                                match = Regex.Match(customerAddress, @"^(.+),قطعة (.+) شارع (.+) جادة (.+),$", RegexOptions.Singleline);

                                if (match.Success)
                                {
                                    level = "5-";

                                    serviceAddress.Block = int.TryParse(Ia.Cl.Model.Default.RemoveNonNumericCharacters(match.Groups[2].Value), out block) ? block : 0;
                                    serviceAddress.Street = match.Groups[3].Value;
                                    serviceAddress.Boulevard = match.Groups[4].Value;
                                    serviceAddress.PremisesOld = null;
                                    serviceAddress.PremisesNew = null;
                                }
                                else
                                {
                                    level = "6";

                                    // below:
                                    // الجهراء الجهراء المدينة,قطعة 79 قسيمة 17, منزل 17
                                    match = Regex.Match(customerAddress, @"^(.+),قطعة (.+) قسيمة (.+), منزل (.+)$", RegexOptions.Singleline);

                                    if (match.Success)
                                    {
                                        level = "6-";

                                        serviceAddress.Block = int.TryParse(Ia.Cl.Model.Default.RemoveNonNumericCharacters(match.Groups[2].Value), out block) ? block : 0;
                                        serviceAddress.Street = null;
                                        serviceAddress.Boulevard = null;
                                        serviceAddress.PremisesOld = match.Groups[3].Value;
                                        serviceAddress.PremisesNew = match.Groups[4].Value;
                                    }
                                    else
                                    {
                                        level = "7";

                                        // below:
                                        // الجهراء الجهراء المدينة,قطعة 79 قسيمة 17
                                        match = Regex.Match(customerAddress, @"^(.+),قطعة (.+) قسيمة (.+),$", RegexOptions.Singleline);

                                        if (match.Success)
                                        {
                                            level = "7-";

                                            serviceAddress.Block = int.TryParse(Ia.Cl.Model.Default.RemoveNonNumericCharacters(match.Groups[2].Value), out block) ? block : 0;
                                            serviceAddress.Street = null;
                                            serviceAddress.Boulevard = null;
                                            serviceAddress.PremisesOld = match.Groups[3].Value;
                                            serviceAddress.PremisesNew = null;
                                        }
                                        else
                                        {
                                            level = "8";

                                            // below:
                                            // الجهراء الجهراء المدينة,قطعة 79, منزل 17
                                            match = Regex.Match(customerAddress, @"^(.+),قطعة (.+), منزل (.+)$", RegexOptions.Singleline);

                                            if (match.Success)
                                            {
                                                level = "8-";

                                                serviceAddress.Block = int.TryParse(Ia.Cl.Model.Default.RemoveNonNumericCharacters(match.Groups[2].Value), out block) ? block : 0;
                                                serviceAddress.Street = null;
                                                serviceAddress.Boulevard = null;
                                                serviceAddress.PremisesOld = null;
                                                serviceAddress.PremisesNew = match.Groups[3].Value;
                                            }
                                            else
                                            {
                                                level = "9";

                                                // below:
                                                // الجهراء الجهراء المدينة,قطعة 79 شارع 5
                                                match = Regex.Match(customerAddress, @"^(.+),قطعة (.+) شارع (.+)$", RegexOptions.Singleline);

                                                if (match.Success)
                                                {
                                                    level = "9-";

                                                    serviceAddress.Block = int.TryParse(Ia.Cl.Model.Default.RemoveNonNumericCharacters(match.Groups[2].Value), out block) ? block : 0;
                                                    serviceAddress.Street = match.Groups[3].Value;
                                                    serviceAddress.Boulevard = null;
                                                    serviceAddress.PremisesOld = null;
                                                    serviceAddress.PremisesNew = null;
                                                }
                                                else
                                                {
                                                    level = "10";

                                                    // below:
                                                    // الجهراء الجهراء المدينة,قطعة 79
                                                    match = Regex.Match(customerAddress, @"^(.+),قطعة (.+),$", RegexOptions.Singleline);

                                                    if (match.Success)
                                                    {
                                                        level = "10-";

                                                        serviceAddress.Block = int.TryParse(Ia.Cl.Model.Default.RemoveNonNumericCharacters(match.Groups[2].Value), out block) ? block : 0;
                                                        serviceAddress.Street = null;
                                                        serviceAddress.Boulevard = null;
                                                        serviceAddress.PremisesOld = null;
                                                        serviceAddress.PremisesNew = null;
                                                    }
                                                    else
                                                    {
                                                        level = "11";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // below: we will try to store integers in integer format

                //serviceAddress.Block this is already in int format
                serviceAddress.Street = int.TryParse(serviceAddress.Street, out i) ? i.ToString() : serviceAddress.Street;
                serviceAddress.Boulevard = int.TryParse(serviceAddress.Boulevard, out i) ? i.ToString() : serviceAddress.Boulevard;
                serviceAddress.PremisesOld = int.TryParse(serviceAddress.PremisesOld, out i) ? i.ToString() : serviceAddress.PremisesOld;
                serviceAddress.PremisesNew = int.TryParse(serviceAddress.PremisesNew, out i) ? i.ToString() : serviceAddress.PremisesNew;
            }
            else
            {
                level = "0";

                serviceAddress.AreaId = 0;// (from a in Ia.Ngn.Cl.Model.Data.Service.KuwaitNgnAreaList where a.DomainListString.Contains(service.Substring(0, 4)) select a.Id).SingleOrDefault();
            }

            /*
            sql = @"
SELECT        CONVERT(varchar, f.area) + '_' + CONVERT(varchar, f.pon) AS area_pon, f.area, f.pon, f.ont, srs.dn, srs.customer_address, srs.statistic_area, 
                         srs.statistic_pon, srs.statistic_ont
FROM            ia_service_request_service AS srs LEFT OUTER JOIN
                         ia_field AS f ON f.area = srs.area AND f.pon = srs.pon AND f.ont = srs.ont
WHERE        (srs.status = 3 OR
                         srs.status = 5) AND (srs.active = 'true')
ORDER BY srs.dn
";

            sql = @"
SELECT        CONVERT(varchar, f.area) + '_' + CONVERT(varchar, f.pon) AS area_pon, f.area, f.pon, f.ont, srs.dn, srs.customer_address, srs.statistic_area, 
                         srs.statistic_pon, srs.statistic_ont, st.area AS st_area, st.pon AS st_pon, st.ont AS st_ont
FROM            ia_service_request_service AS srs LEFT OUTER JOIN
                         ia_field AS f ON f.area = srs.area AND f.pon = srs.pon AND f.ont = srs.ont LEFT OUTER JOIN
                         ia_system AS s ON s.dn = srs.dn LEFT OUTER JOIN
                         ia_protocol AS p ON p.lceid = s.lceid AND p.lan = s.lan LEFT OUTER JOIN
                         ia_ont_voip AS ov ON ov.ip = p.ip LEFT OUTER JOIN
                         ia_ont AS o ON o.id = ov.ia_ont_id LEFT OUTER JOIN
                         ia_standard AS st ON st.ia_ont_id = o.id
WHERE        (srs.status = 3 OR
                         srs.status = 5) AND (srs.active = 'true')
ORDER BY srs.dn
";
             */

            /*
            // below: debuging checking uniqueness of dn the problem was with same ip connected to multiple ONTs
            ht = new Hashtable(srs_dt.Rows.Count);
            foreach (DataRow r in srs_dt.Rows)
            {
                dn = r["dn"].ToString();

                if (!ht.ContainsKey(dn)) ht[dn] = 1;
                else dn = "";
            }

            DataColumn[] keys = new DataColumn[1];
            keys[0] = srs_dt.Columns["dn"];
            srs_dt.PrimaryKey = keys;

            srs_ht = new Hashtable(srs_dt.Rows.Count + 1);
            address_ont_id_ht = new Hashtable(srs_dt.Rows.Count + 1);
             */


            /*
            // below: collect ont_id information from srs

            foreach (DataRow r in srs_dt.Rows)
            {
                dn = r["dn"].ToString();

                try
                {
                    if (r["customer_address"] != null) srs_ht[dn] = r["customer_address"].ToString();
                    else srs_ht[dn] = null;
                }
                catch (Exception) { srs_ht[dn] = null; }
            }
             */


            /*
            // below: collect address information from ia_field:

            sql = @"
SELECT        CONVERT(varchar, f.area) + '_' + CONVERT(varchar, f.pon) AS area_pon, f.area, f.pon, f.ont, f.block, f.street, f.premises_old, f.premises_new
FROM            ia_field AS f INNER JOIN
                         ia_standard AS st ON st.id = f.id INNER JOIN
                         ia_ont AS o ON o.id = st.ia_ont_id
WHERE        (o.family_type IS NOT NULL)
ORDER BY f.id
";
             */

            //dt = null; // Ia.Cs.Db.SqlServer.StaticSelect(sql);










            /*
            a_selected_ht = new Hashtable(dt.Rows.Count + 1);
            area_pon_ht = new Hashtable(dt.Rows.Count);

                    area = r["area"].ToString();
                    s = ont_id = "";

                        ont_id = area + "." + r["pon"].ToString() + "." + r["ont"].ToString();

                        if (r["area"] == null) area = null;
                        else area = r["area"].ToString();

                        if (r["block"] == null) block = null;
                        else block = r["block"].ToString();

                        if (r["street"] == null) street = null;
                        else street = r["street"].ToString();

                        if (r["premises_old"] == null) premises_old = null;
                        else
                        {
                            premises_old = r["premises_old"].ToString();

                            //if (premises_old.Contains(")")) { }

                            // below: remove -1,-2 from 37-2 16-1 40(3-2)
                            premises_old = premises_old.Replace("-1", "");
                            premises_old = premises_old.Replace("-2", "");

                            // below: change from "332(333)" to "332-333"
                            // premises_old = Regex.Replace(premises_old, @"\(.*?\)", "");
                            premises_old = premises_old.Replace("(", "-");
                            premises_old = premises_old.Replace(")", "");
                        }

                        if (r["premises_new"] == null) premises_new = null;
                        else premises_new = r["premises_new"].ToString();

                        ////////////////////////

                        // below: this will take care of cases where street/premises is like "210-232-11". If this is the case we will enter all street/premises formation into seperate listings
                        // I will assigned null to any field that does not containt digits only

                        if (street != null && premises_old != null)
                        {
                            street_sp = street.ToString().Split('-');
                            premises_old_sp = premises_old.ToString().Split('-');

                            for (int i = 0; i < street_sp.Length; i++)
                            {
                                for (int j = 0; j < premises_old_sp.Length; j++)
                                {
                                    s = "";

                                    if (area != null && Ia.Cl.Model.Default.IsInt(area)) s += area + "|";
                                    else s += "|";

                                    if (block != null && Ia.Cl.Model.Default.IsInt(block)) s += block + "|";
                                    else s += "|";

                                    if (Ia.Cl.Model.Default.IsInt(street_sp[i].ToString())) s += street_sp[i].ToString() + "|";
                                    else s += "|";

                                    if (Ia.Cl.Model.Default.IsInt(premises_old_sp[j].ToString())) s += premises_old_sp[j].ToString() + "|";
                                    else s += "|";

                                    if (premises_new != null && Ia.Cl.Model.Default.IsInt(premises_new)) s += premises_new + "|";
                                    else s += "|";

                                    // below: build area_pon according to pon addresses

                                    if (s.Length > 0 && ont_id.Length > 0)
                                    {
                                        area_pon = r["area_pon"].ToString();

                                        if (area_pon_ht.ContainsKey(area_pon)) area_pon_ht[area_pon] += "[" + s + ":" + ont_id + "]";
                                        else area_pon_ht[area_pon] = "[" + s + ":" + ont_id + "]";
                                    }
                                }
                            }
                        }
                        else if (street != null && premises_old == null)
                        {
                            street_sp = street.ToString().Split('-');

                            for (int i = 0; i < street_sp.Length; i++)
                            {
                                s = "";

                                if (area != null && Ia.Cl.Model.Default.IsInt(area)) s += area + "|";
                                else s += "|";

                                if (block != null && Ia.Cl.Model.Default.IsInt(block)) s += block + "|";
                                else s += "|";

                                if (Ia.Cl.Model.Default.IsInt(street_sp[i].ToString())) s += street_sp[i].ToString() + "|";
                                else s += "|";

                                s += "|"; // premises_old is null

                                if (premises_new != null && Ia.Cl.Model.Default.IsInt(premises_new)) s += premises_new + "|";
                                else s += "|";

                                // below: build area_pon according to pon addresses

                                if (s.Length > 0 && ont_id.Length > 0)
                                {
                                    area_pon = r["area_pon"].ToString();

                                    if (area_pon_ht.ContainsKey(area_pon)) area_pon_ht[area_pon] += "[" + s + ":" + ont_id + "]";
                                    else area_pon_ht[area_pon] = "[" + s + ":" + ont_id + "]";
                                }
                            }
                        }
                        else if (street == null && premises_old != null)
                        {
                            premises_old_sp = premises_old.ToString().Split('-');

                            for (int i = 0; i < premises_old_sp.Length; i++)
                            {
                                s = "";

                                if (area != null && Ia.Cl.Model.Default.IsInt(area)) s += area + "|";
                                else s += "|";

                                if (block != null && Ia.Cl.Model.Default.IsInt(block)) s += block + "|";
                                else s += "|";

                                s += "|"; // street is null

                                if (Ia.Cl.Model.Default.IsInt(premises_old_sp[i].ToString())) s += premises_old_sp[i].ToString() + "|";
                                else s += "|";

                                if (premises_new != null && Ia.Cl.Model.Default.IsInt(premises_new)) s += premises_new + "|";
                                else s += "|";

                                // below: build area_pon according to pon addresses

                                if (s.Length > 0 && ont_id.Length > 0)
                                {
                                    area_pon = r["area_pon"].ToString();

                                    if (area_pon_ht.ContainsKey(area_pon)) area_pon_ht[area_pon] += "[" + s + ":" + ont_id + "]";
                                    else area_pon_ht[area_pon] = "[" + s + ":" + ont_id + "]";
                                }
                            }
                        }
                        else //if (street == null && premises_old == null)
                        {
                            s = "";

                            if (area != null && Ia.Cl.Model.Default.IsInt(area)) s += area + "|";
                            else s += "|";

                            if (block != null && Ia.Cl.Model.Default.IsInt(block)) s += block + "|";
                            else s += "|";

                            s += "|"; // street is null

                            s += "|"; // premises_old is null

                            if (premises_new != null && Ia.Cl.Model.Default.IsInt(premises_new)) s += premises_new + "|";
                            else s += "|";

                            // below: build area_pon according to pon addresses

                            if (s.Length > 0 && ont_id.Length > 0)
                            {
                                area_pon = r["area_pon"].ToString();

                                if (area_pon_ht.ContainsKey(area_pon)) area_pon_ht[area_pon] += "[" + s + ":" + ont_id + "]";
                                else area_pon_ht[area_pon] = "[" + s + ":" + ont_id + "]";
                            }
                        }
                    
    

            // below: first loop for current address matching then for all addresses in pon

            for (int i = 0; i <= 1; i++)
            {
                // below: look through levels
                for (level = 0; level < 5; level++)
                {
                    // below: for all srs_ht items convert them into an ont_id as close as possible to address
                    foreach (DataRow r in srs_dt.Rows)
                    {
                        // when i = 0 we will match current ont_id
                        // where i = 1 we will match all ont_id in pon

                        if (i == 0)
                        {
                            if (r["st_area"].ToString().Length == 0 || r["st_pon"].ToString().Length == 0 || r["st_ont"].ToString().Length == 0) continue;
                            else u = @"(" + r["st_area"].ToString() + "." + r["st_pon"].ToString() + "." + r["st_ont"].ToString() + ")";
                        }
                        else u = @"(\d{1,3}\.\d{1,3}\.\d{1,3})";

                        dn = r["dn"].ToString();

                        //if (dn == "25212978") { }

                        area_pon = r["area_pon"].ToString();

                        if (area_pon.Length > 0)
                        {
                            sbs = area_pon_ht[area_pon].ToString();

                            if (address_ont_id_ht[dn] == null)
                            {
                                // below: will only check numbers that are not already assigned ont_ids

                                b = false;
                                ont_id = null;

                                address = r["customer_address"].ToString();
                                domain = dn.Substring(0, 4);

                                if (domain == "2363")
                                {
                                    address = address.Replace(",", "");
                                    area = "94"; // FHD
                                }
                                else if (domain == "2453")
                                {
                                    address = address.Replace(",", "");
                                    area = "95"; // SJA
                                }
                                else if (domain == "2466")
                                {
                                    address = address.Replace(",", "");
                                    area = "96"; // QRW
                                }
                                else if (domain == "2374")
                                {
                                    address = address.Replace(",", "");
                                    area = "98"; // MGF
                                }
                                else if (domain == "2435" || domain == "2436")
                                {
                                    address = address.Replace(",", "");
                                    area = "97"; // ABM
                                }
                                else if (domain == "2490")
                                {
                                    address = address.Replace(",", "");
                                    area = "55"; // SBN
                                }
                                else if (domain == "2438")
                                {
                                    address = address.Replace(",", "");
                                    area = "34"; // ESH
                                }
                                else
                                {
                                    area = Ia.Cl.Model.Default.Match(address, "حولى (.*?),");

                                    if (area == "السلام") area = "42";
                                    else if (area == "حطين") area = "43";
                                    else if (area == "الشهداء") area = "44";
                                    else if (area == "الزهراء") area = "46";
                                    else area = null;

                                    address = address.Replace(",", "");
                                }

                                //if (dn == "25231472") {};

                                par = 0;

                                if (area != null)
                                {
                                    // below: I will use par to indicate the number of parameters given for this ont_id. If I have, say, 4 givin parameters I will check down to 3 only and nothing less

                                    par++; // area is known

                                    block = Ia.Cl.Model.Default.Match(address, @"قطعة (.+?) [شارع|جادة|قسيمة|منزل ]");

                                    street = Ia.Cl.Model.Default.Match(address, @"شارع (\d+?) [جادة|قسيمة|منزل ]");
                                    if (street == null) street = Ia.Cl.Model.Default.Match(address, @"جادة (\d+?) [قسيمة|منزل ]");

                                    premises_old = Ia.Cl.Model.Default.Match(address, @"قسيمة (.+?) [منزل ]");
                                    if (premises_old == null) premises_old = Ia.Cl.Model.Default.Match(address, @"قسيمة (.+?)$");

                                    premises_new = Ia.Cl.Model.Default.Match(address, @"منزل (.+?)$");

                                    if (block != null && Ia.Cl.Model.Default.IsInt(block)) { par++; }
                                    else block = null;

                                    if (street != null && Ia.Cl.Model.Default.IsInt(street)) { par++; }
                                    else street = null;

                                    if (premises_old != null && Ia.Cl.Model.Default.IsInt(premises_old)) { par++; }
                                    else premises_old = null;

                                    if (premises_new != null && Ia.Cl.Model.Default.IsInt(premises_new)) { par++; }
                                    else premises_new = null;

                                    // below: testing according to relevance

                                    add = ont_id = "";

                                    if (level == 0)
                                    {
                                        if (area != null && block != null && street != null && premises_old != null && premises_new != null && par >= 4)
                                        {
                                            regex = @"\[(" + area + @"\|" + block + @"\|" + street + @"\|" + premises_old + @"\|" + premises_new + @"\|" + ")[:]" + u + @"\]";
                                            m = Regex.Match(sbs, regex);
                                            if (m.Success)
                                            {
                                                add = m.Groups[1].Captures[0].Value;
                                                ont_id = m.Groups[2].Captures[0].Value;
                                                b = true;
                                            }
                                        }
                                    }
                                    else if (level == 1)
                                    {
                                        if (area == "95" || area == "98")
                                        {
                                            if (area != null && block != null && premises_old != null && par >= 2)
                                            {
                                                regex = @"\[(" + area + @"\|" + block + @"\|\d*\|" + premises_old + @"\|\d*\|" + ")[:]" + u + @"\]";
                                                m = Regex.Match(sbs, regex);
                                                if (m.Success)
                                                {
                                                    add = m.Groups[1].Captures[0].Value;
                                                    ont_id = m.Groups[2].Captures[0].Value;
                                                    b = true;
                                                }
                                            }
                                        }

                                        if (!b)
                                        {
                                            if (area != null && block != null && street != null && premises_new != null && par >= 3)
                                            {
                                                regex = @"\[(" + area + @"\|" + block + @"\|" + street + @"\|\d*\|" + premises_new + @"\|" + ")[:]" + u + @"\]";
                                                m = Regex.Match(sbs, regex);
                                                if (m.Success)
                                                {
                                                    add = m.Groups[1].Captures[0].Value;
                                                    ont_id = m.Groups[2].Captures[0].Value;
                                                    b = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (level == 2)
                                    {
                                        if (area == "95" || area == "98")
                                        {
                                            if (area != null && block != null && premises_new != null && par >= 2)
                                            {
                                                regex = @"\[(" + area + @"\|" + block + @"\|\d*\|\d*\|" + premises_new + @"\|" + ")[:]" + u + @"\]";
                                                m = Regex.Match(sbs, regex);
                                                if (m.Success)
                                                {
                                                    add = m.Groups[1].Captures[0].Value;
                                                    ont_id = m.Groups[2].Captures[0].Value;
                                                    b = true;
                                                }
                                            }
                                        }

                                        if (!b)
                                        {
                                            if (area != null && block != null && street != null && premises_old != null && par >= 3)
                                            {
                                                regex = @"\[(" + area + @"\|" + block + @"\|" + street + @"\|" + premises_old + @"\|\d*\|" + ")[:]" + u + @"\]";
                                                m = Regex.Match(sbs, regex);
                                                if (m.Success)
                                                {
                                                    add = m.Groups[1].Captures[0].Value;
                                                    ont_id = m.Groups[2].Captures[0].Value;
                                                    b = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (level == 3)
                                    {
                                        if (area == "95" || area == "98")
                                        {
                                            if (area != null && block != null && premises_new != null && par >= 2)
                                            {
                                                regex = @"\[(" + area + @"\|" + block + @"\|\d*\|" + premises_new + @"\|\d*\|" + ")[:]" + u + @"\]";
                                                m = Regex.Match(sbs, regex);
                                                if (m.Success)
                                                {
                                                    add = m.Groups[1].Captures[0].Value;
                                                    ont_id = m.Groups[2].Captures[0].Value;
                                                    b = true;
                                                }
                                            }
                                        }

                                        if (!b)
                                        {
                                            if (area != null && block != null && street != null && premises_old != null && par >= 3)
                                            {
                                                regex = @"\[(" + area + @"\|" + block + @"\|" + street + @"\|\d*\|" + premises_old + @"\|" + ")[:]" + u + @"\]";
                                                m = Regex.Match(sbs, regex);
                                                if (m.Success)
                                                {
                                                    add = m.Groups[1].Captures[0].Value;
                                                    ont_id = m.Groups[2].Captures[0].Value;
                                                    b = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (level == 4)
                                    {
                                        if (area == "95" || area == "98")
                                        {
                                            if (area != null && block != null && premises_old != null && par >= 2)
                                            {
                                                regex = @"\[(" + area + @"\|" + block + @"\|\d*\|\d*\|" + premises_old + @"\|" + ")[:]" + u + @"\]";
                                                m = Regex.Match(sbs, regex);
                                                if (m.Success)
                                                {
                                                    add = m.Groups[1].Captures[0].Value;
                                                    ont_id = m.Groups[2].Captures[0].Value;
                                                    b = true;
                                                }
                                            }
                                        }

                                        if (!b)
                                        {
                                            if (area != null && block != null && street != null && premises_new != null && par >= 3)
                                            {
                                                regex = @"\[(" + area + @"\|" + block + @"\|" + street + @"\|" + premises_new + @"\|\d*\|" + ")[:]" + u + @"\]";
                                                m = Regex.Match(sbs, regex);
                                                if (m.Success)
                                                {
                                                    add = m.Groups[1].Captures[0].Value;
                                                    ont_id = m.Groups[2].Captures[0].Value;
                                                    b = true;
                                                }
                                            }
                                        }
                                    }

                                    if (b) address_ont_id_ht[dn] = ont_id;
                                    else address_ont_id_ht[dn] = null;
                                }
                            }
                        }
                    }
                }
            }

            // fill current state table with values from service requests
            foreach (string si in address_ont_id_ht.Keys)
            {
                dr = srs_dt.Rows.Find(si);

                if (dr != null)
                {
                    if (address_ont_id_ht.ContainsKey(si))
                    {
                        if (address_ont_id_ht[si] != null)
                        {
                            s = address_ont_id_ht[si].ToString();
                            sp = s.Split('.');

                            if (sp.Length == 3)
                            {
                                dr["statistic_area"] = int.Parse(sp[0].ToString());
                                dr["statistic_pon"] = int.Parse(sp[1].ToString());
                                dr["statistic_ont"] = int.Parse(sp[2].ToString());
                            }
                            else { }
                        }
                        else
                        {
                            dr["statistic_area"] = DBNull.Value;
                            dr["statistic_pon"] = DBNull.Value;
                            dr["statistic_ont"] = DBNull.Value;
                        }
                    }
                    else
                    {
                    }
                }
                else { }
            }
             */

            return serviceAddress;
        }

        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////    
    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public static class ServiceRequestExtension
    {
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Extension method: Collect distinct numbers Number list
        /// </summary>
        public static List<int> DistinctNumberList(this List<Ia.Ngn.Cl.Model.ServiceRequest> sourceList)
        {
            List<int> numberList;

            numberList = (from q in sourceList select q.Number).Distinct().ToList<int>();

            return numberList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Extension method: Collect distinct number-serials into NumberSerial list
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Business.ServiceRequest.NumberSerial> DistinctNumberSerialList(this List<Ia.Ngn.Cl.Model.ServiceRequest> sourceList)
        {
            List<Ia.Ngn.Cl.Model.Business.ServiceRequest.NumberSerial> numberSerialList;

            numberSerialList = (from q in sourceList select new Ia.Ngn.Cl.Model.Business.ServiceRequest.NumberSerial { Number = q.Number, Serial = q.Serial }).Distinct().ToList();

            return numberSerialList;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}