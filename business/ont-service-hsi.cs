using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model.Business
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// ONT-SERVICEHSI support class of Next Generation Network'a (NGN's) business model.
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
    public partial class OntServiceHsi
    {
        /// <summary/>
        public OntServiceHsi() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.OntServiceHsi> ReturnOntServiceHsiAndOntAndAccessListForIspAndBandwidth(Ia.Ngn.Cl.Model.Data.Hsi.IspName ispName, Ia.Ngn.Cl.Model.Data.Hsi.ProfileName profileName)
        {
            Ia.Ngn.Cl.Model.Data.Hsi.Isp isp;
            Ia.Ngn.Cl.Model.Data.Hsi.Profile profile;

            isp = Ia.Ngn.Cl.Model.Data.Hsi.IspList.Find(s => s.Id == (int)ispName);
            profile = Ia.Ngn.Cl.Model.Data.Hsi.ProfileList.Find(s => s.Id == (int)profileName);

            return ReturnOntServiceHsiAndOntAndAccessListForIspAndBandwidthAndTakeN(isp.Id, profile.Id, 10000000);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.OntServiceHsi> ReturnOntServiceHsiAndOntAndAccessListForIspAndBandwidthAndTakeN(int ispNameId, int profileNameId, int take)
        {
            Ia.Ngn.Cl.Model.Data.Hsi.Isp isp;
            Ia.Ngn.Cl.Model.Data.Hsi.Profile profile;
            List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList;

            isp = Ia.Ngn.Cl.Model.Data.Hsi.IspFromIspId(ispNameId);
            profile = Ia.Ngn.Cl.Model.Data.Hsi.ProfileFromProfileId(profileNameId);

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                if (ispNameId != 0 && profileNameId != 0)
                {
                    ontServiceHsiList = (from q in db.OntServiceHsis where q.Ont.Access != null && q.Svlan == isp.Lan && (q.DownstreamBandwidthProfileId == profile.Id || q.UpstreamBandwidthProfileId == profile.Id) select q).Include(x => x.Ont.Access).Take(take).ToList();
                }
                else if (ispNameId != 0)
                {
                    ontServiceHsiList = (from q in db.OntServiceHsis where q.Ont.Access != null && q.Svlan == isp.Lan select q).Include(x => x.Ont.Access).Take(take).ToList();
                }
                else if (profileNameId != 0)
                {
                    ontServiceHsiList = (from q in db.OntServiceHsis where q.Ont.Access != null && (q.DownstreamBandwidthProfileId == profile.Id || q.UpstreamBandwidthProfileId == profile.Id) select q).Include(x => x.Ont.Access).Take(take).ToList();
                }
                else
                {
                    ontServiceHsiList = (from q in db.OntServiceHsis where q.Ont.Access != null select q).Include(x => x.Ont.Access).Take(take).ToList();
                }
            }

            return ontServiceHsiList;
        }
        
        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}