using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model.Business.Nokia
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// AGCF Gateway Table support class for Nokia's Next Generation Network (NGN) business model.
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
    public partial class AgcfGatewayTable
    {
        /// <summary>
        /// 1360 COM WebAPI User Guide 255-400-419R3.X
        /// ngfs-agcfgatewaytable-v2(rtrv,ent,ed,dlt)
        /// 
        /// </summary>
        public AgcfGatewayTable() { }

        /*
         * OMC-P Web API Tester:
         * 
         * rtrv-ngfs-agcfgatewaytable-v2:
         * 
         * Always 6

<?xml version="1.0" encoding="UTF-8"?>
<soap-env:Envelope xmlns:soap-env="http://schemas.xmlsoap.org/soap/envelope/">
 <soap-env:Header />
 <soap-env:Body>
  <PlexViewRequest Command="rtrv-ngfs-agcfgatewaytable-v2" SwitchName="TECICS01" RequestId="" SessionId="ngnAPI:1946199540" MaxRows="-1">
   <NgfsAgcfGatewayTableAid></NgfsAgcfGatewayTableAid>
  </PlexViewRequest>
 </soap-env:Body>
</soap-env:Envelope>


<?xml version="1.0" encoding="UTF-8"?>
<soap-env:Envelope xmlns:soap-env="http://schemas.xmlsoap.org/soap/envelope/">
 <soap-env:Header />
 <soap-env:Body>
  <PlexViewResponse Command="rtrv-ngfs-agcfgatewaytable-v2" SwitchName="TECICS01" RequestId="" SessionId="ngnAPI:1946199540" CongestionIndicator="false" Status="SUCCESS">
   <ngfs-agcfgatewaytable-v2>
    <NgfsAgcfGatewayTableAid>6</NgfsAgcfGatewayTableAid>
    <TableName>AGCF Gateway 6</TableName>
   </ngfs-agcfgatewaytable-v2>
  </PlexViewResponse>
 </soap-env:Body>
</soap-env:Envelope>
         * 
         */

        /// <summary>
        /// NgfsAgcfGatewayTableAid (req for ent,req for ed). LCP-CTS R6.0 The AID of Agcf Gateway Table. Integer (0-99), where 0 will auto select the table number D. LCP Command. 229 Only 1 table can be supported LCP-ISC R22.1 Support 0-1000, 0 means auto select
        /// Fixed in MOC IMS at 6
        /// </summary>
        public static int NgfsAgcfGatewayTableAid { get { return 6; } }

        /// <summary>
        /// TableName (req for ent,req for ed). LCP-CTS R6.0 The name of Agcf Gateway Variant Table. If NgfsAgcfGWVariantTableAid set to 0 for auto selected, the TableName must be empty. String (0-20) [\S]|[\S][a-zA-Z0-9\*#\.,\-+'~`!@$%^:;=(){}\[\]?&quot;&apos;&amp;&gt;&lt;/_|\\ ]*[\S]
        /// Fixed "AGCF Gateway 6" in MOC IMS
        /// </summary>
        public static string TableName { get { return "AGCF Gateway 6"; } }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}