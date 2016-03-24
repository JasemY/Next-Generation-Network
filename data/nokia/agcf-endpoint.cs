using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model.Data.Nokia
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// AGCF Endpoint support class for Nokia data model.
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
        public static Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord AgcfGatewayRecordFromId(string agcfEndpointId)
        {
            Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord agcfGatewayRecord;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfGatewayRecord = (from e in db.AgcfEndpoints join g in db.AgcfGatewayRecords on e.GwId equals g.GwId where e.Id == agcfEndpointId select g).SingleOrDefault();
            }

            return agcfGatewayRecord;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<string> ReadPrividUserList()
        {
            List<string> prividUserlist;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                prividUserlist = (from q in db.AgcfEndpoints orderby q.GwId ascending select q.PrividUser).ToList();
            }

            return prividUserlist;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<int> UsedFlatTermIdListForGatewayId(int gwId)
        {
            List<int> list;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                list = (from e in db.AgcfEndpoints where e.GwId == gwId orderby e.FlatTermID ascending select e.FlatTermID).ToList<int>();
            }

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> List(int gwId)
        {
            List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfEndpointList = (from a in db.AgcfEndpoints where a.GwId == gwId select a).Include(a => a.AgcfGatewayRecord).ToList();
            }

            return agcfEndpointList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint Read(string prividUser)
        {
            Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint agcfEndpoint;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                agcfEndpoint = (from a in db.AgcfEndpoints where a.PrividUser == prividUser select a).Include(a => a.AgcfGatewayRecord).SingleOrDefault();
            }

            return agcfEndpoint;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}