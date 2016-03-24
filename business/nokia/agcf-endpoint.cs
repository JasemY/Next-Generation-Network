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
    /// AGCF Endpoint support class for Nokia's Next Generation Network (NGN) business model.
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
    public partial class AgcfEndpoint
    {
        public AgcfEndpoint() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> ParseFromDictionary(List<Dictionary<string, string>> parameterDictionaryList)
        {
            int i;
            string prividUser;
            Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint agcfEndpoint;
            List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList;

            agcfEndpointList = new List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint>();

            foreach (Dictionary<string, string> parameterDictionary in parameterDictionaryList)
            {
                agcfEndpoint = new Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint();

                prividUser = parameterDictionary["PrividUser"].ToString();

                agcfEndpoint.Id = Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint.AgcfEndpointId(prividUser);

                agcfEndpoint.PrividUser = prividUser;
                agcfEndpoint.Slot = int.TryParse(parameterDictionary["Slot"].ToString(), out i) ? i : 0;
                agcfEndpoint.Port = int.TryParse(parameterDictionary["Port"].ToString(), out i) ? i : 0;
                agcfEndpoint.GwId = int.TryParse(parameterDictionary["GwId"].ToString(), out i) ? i : 0;
                agcfEndpoint.Dn = parameterDictionary["Dn"].ToString();
                agcfEndpoint.AdditionalDNs = parameterDictionary["AdditionalDNs"].ToString();
                agcfEndpoint.DomainIndex = int.TryParse(parameterDictionary["DomainIndex"].ToString(), out i) ? i : 0;
                agcfEndpoint.FeatureFlag = int.TryParse(parameterDictionary["FeatureFlag"].ToString(), out i) ? i : 0;
                agcfEndpoint.DslamId = parameterDictionary["DslamId"].ToString();
                agcfEndpoint.Rack = int.TryParse(parameterDictionary["Rack"].ToString(), out i) ? i : 0;
                agcfEndpoint.Shelf = int.TryParse(parameterDictionary["Shelf"].ToString(), out i) ? i : 0;
                agcfEndpoint.FlatTermID = int.TryParse(parameterDictionary["FlatTermID"].ToString(), out i) ? i : 0;
                agcfEndpoint.SubscriberType = (parameterDictionary["SubscriberType"].ToString() == "Analog") ? true : false;
                agcfEndpoint.ReversePolarity = int.TryParse(parameterDictionary["ReversePolarity"].ToString(), out i) ? i : 0;
                agcfEndpoint.PayphoneMetering = int.TryParse(parameterDictionary["PayphoneMetering"].ToString(), out i) ? i : 0;
                agcfEndpoint.DigitMap1st = int.TryParse(parameterDictionary["DigitMap1st"].ToString(), out i) ? i : 0;
                agcfEndpoint.DigitMap2nd = int.TryParse(parameterDictionary["DigitMap2nd"].ToString(), out i) ? i : 0;
                agcfEndpoint.DialTone2nd = int.TryParse(parameterDictionary["DialTone2nd"].ToString(), out i) ? i : 0;
                agcfEndpoint.CallHoldLc = Convert.ToBoolean(parameterDictionary["CallHoldLc"].ToString());
                agcfEndpoint.CallWaitingLc = Convert.ToBoolean(parameterDictionary["CallWaitingLc"].ToString());
                agcfEndpoint.CallToggleLc = Convert.ToBoolean(parameterDictionary["CallToggleLc"].ToString());
                agcfEndpoint.ThreeWayCallLc = Convert.ToBoolean(parameterDictionary["ThreeWayCallLc"].ToString());
                agcfEndpoint.McidLc = Convert.ToBoolean(parameterDictionary["McidLc"].ToString());
                agcfEndpoint.Password = (parameterDictionary.ContainsKey("Password")) ? parameterDictionary["Password"].ToString() : null;
                agcfEndpoint.CallTransferLc = Convert.ToBoolean(parameterDictionary["CallTransferLc"].ToString());

                agcfEndpoint.Created = DateTime.UtcNow.AddHours(3);
                agcfEndpoint.Updated = DateTime.UtcNow.AddHours(3);
                agcfEndpoint.Inspected = DateTime.UtcNow.AddHours(3);
                agcfEndpoint.UserId = Guid.Empty;

                agcfEndpointList.Add(agcfEndpoint);
            }

            return agcfEndpointList;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}