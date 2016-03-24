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

namespace Ia.Ngn.Cl.Model.Data.Nokia
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Access Management System (AMS) support class for Nokia data model.
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
    public partial class Ams
    {
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Ams() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string BellcoreStateFromId(int stateId)
        {
            string s;
            Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState bellcoreState;

            bellcoreState = (Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState)stateId;

            switch (bellcoreState)
            {
                case Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.IsNr: s = "IS-NR"; break;
                case Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosAu: s = "OOS-AU"; break;
                case Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosAuma: s = "OOS-AUMA"; break;
                case Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.OosMa: s = "OOS-MA"; break;
                case Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.Undefined: s = "Undefined"; break;
                default: s = "Undefined"; break;
            }

            return s;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string PlannedSoftware
        {
            get
            {
                return "3FE50853AFXA35";
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void ReadAccessOntHsiVoipPotsDetailsFromSerialTake10Onts(string serial, out List<Ia.Ngn.Cl.Model.Access> accessList, out List<Ia.Ngn.Cl.Model.Ont> ontList, out List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList, out List<Ia.Ngn.Cl.Model.OntServiceVoip> ontServiceVoipList, out List<Ia.Ngn.Cl.Model.OntOntPots> ontOntPotsList)
        {
            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                accessList = (from q in db.Onts where q.Serial == serial && q.Access != null select q.Access).Take(10).ToList();

                ontList = (from q in db.Onts where q.Serial == serial select q).Take(10).ToList();

                ontServiceHsiList = ontList.SelectMany(t => t.OntServiceHsis).ToList();
                ontServiceVoipList = ontList.SelectMany(t => t.OntServiceVoips).ToList();
                ontOntPotsList = ontList.SelectMany(t => t.OntOntPotses).ToList();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int NextVacantFlatTermIdForOnt(Ia.Ngn.Cl.Model.Ont ont)
        {
            int ft, gwId;
            List<int> list;

            gwId = Ia.Ngn.Cl.Model.Data.Nokia.AgcfGatewayRecord.GwIdFromIp(ont.OntServiceVoips.First().Ip);

            list = Ia.Ngn.Cl.Model.Data.Nokia.AgcfEndpoint.UsedFlatTermIdListForGatewayId(gwId);

            if(list.Count > 0) list = Ia.Cl.Model.Default.ExcludedNumberListFromNumberListWithinRange(list, Ia.Ngn.Cl.Model.Business.Nokia.Ams.PossibleNumberOfTdForOntFamilyType((Ia.Ngn.Cl.Model.Business.Ont.FamilyType)ont.FamilyTypeId));

            if (list.Count > 0) ft = list[0];
            else ft = 0;

            return ft;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Ont OntFromGwId(int gwId)
        {
            string ip;
            Ia.Ngn.Cl.Model.Ont ont;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ip = Ia.Ngn.Cl.Model.Data.Nokia.AgcfGatewayRecord.IpFromGwId(gwId);

                ont = (from o in db.Onts where o.OntServiceVoips.First().Ip == ip select o).SingleOrDefault();
            }

            return ont;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
