using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Ia.Ngn.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Service Entity Framework class for Next Generation Network (NGN) entity model.
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
        public Service() { }

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        /// <summary/>
        public long DN { get; set; }

        /// <summary/>
        public int AREACODE { get; set; }

        /// <summary/>
        public long IDFDN { get; set; }

        /// <summary/>
        public long REFDN { get; set; }

        /// <summary/>
        public long TAXDN { get; set; }

        /// <summary/>
        public string SUBTYP { get; set; }

        /// <summary/>
        public int ACCNBR { get; set; }

        /// <summary/>
        public string ACCTYPE { get; set; }

        /// <summary/>
        public string LINEHNT { get; set; }

        /// <summary/>
        public string TCEHNT { get; set; }

        /// <summary/>
        public string SCOPE { get; set; }

        /// <summary/>
        public bool RECALL { get; set; }

        /// <summary/>
        public string LCEID { get; set; }

        /// <summary/>
        public string LCEIDName { get; set; } // LCEID Name

        /// <summary/>
        public string NA { get; set; }

        /// <summary/>
        public int LAN { get; set; }

        /// <summary/>
        public string SUBGNBR { get; set; }

        /// <summary/>
        public int ORGCH { get; set; }

        /// <summary/>
        public int ORGRTG { get; set; }

        /// <summary/>
        public string ACB { get; set; }

        /// <summary/>
        public string OCBP { get; set; }

        /// <summary/>
        public bool OCBUC_ass { get; set; }

        /// <summary/>
        public bool OCBUC_act { get; set; }

        /// <summary/>
        public string OCBUC_lev { get; set; }

        /// <summary/>
        public bool CLIP { get; set; }

        /// <summary/>
        public bool CLIR_ass { get; set; }

        /// <summary/>
        public bool CLIR_perm { get; set; }

        /// <summary/>
        public bool CLIR_pres { get; set; }

        /// <summary/>
        public bool TPS_ass { get; set; }

        /// <summary/>
        public string TPS_type { get; set; }

        /// <summary/>
        public bool CONF { get; set; }

        /// <summary/>
        public bool CW_ass { get; set; }

        /// <summary/>
        public bool CW_act { get; set; }

        /// <summary/>
        public bool CW_notcg { get; set; }

        /// <summary/>
        public bool CFU_ass { get; set; }

        /// <summary/>
        public bool CFU_act { get; set; }

        /// <summary/>
        public long CFU_F_DN { get; set; }

        /// <summary/>
        public bool CFU_WFTN { get; set; }

        /// <summary/>
        public bool PIN_ass { get; set; }

        /// <summary/>
        public int PIN_code { get; set; }

        /// <summary/>
        public bool ALM_ass { get; set; }

        /// <summary/>
        public bool ALM_once { get; set; }

        /// <summary/>
        public bool ALM_daily { get; set; }

        /// <summary/>
        public bool ALM_days { get; set; }

        /// <summary/>
        public bool ABD_ass { get; set; }

        /// <summary/>
        public int ABD_Cod1 { get; set; }

        /// <summary/>
        public long ABD_DN1 { get; set; }

        /// <summary/>
        public int ABD_Cod2 { get; set; }

        /// <summary/>
        public long ABD_DN2 { get; set; }

        /// <summary/>
        public int ABD_Cod3 { get; set; }

        /// <summary/>
        public long ABD_DN3 { get; set; }

        /// <summary/>
        public int ABD_Cod4 { get; set; }

        /// <summary/>
        public long ABD_DN4 { get; set; }



        /// <summary/>
        public int ABD_Cod5 { get; set; }

        /// <summary/>
        public long ABD_DN5 { get; set; }

        /// <summary/>
        public int ABD_Cod6 { get; set; }

        /// <summary/>
        public long ABD_DN6 { get; set; }

        /// <summary/>
        public int ABD_Cod7 { get; set; }

        /// <summary/>
        public long ABD_DN7 { get; set; }

        /// <summary/>
        public int ABD_Cod8 { get; set; }

        /// <summary/>
        public long ABD_DN8 { get; set; }

        /// <summary/>
        public int ABD_Cod9 { get; set; }

        /// <summary/>
        public long ABD_DN9 { get; set; }

        /// <summary/>
        public int ABD_Cod10 { get; set; }

        /// <summary/>
        public long ABD_DN10 { get; set; }

        
        /// <summary/>
        public DateTime Created { get; set; }

        /// <summary/>
        public DateTime Updated { get; set; }

        /// <summary/>
        public DateTime Viewed { get; set; }

        /*
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static long ServiceId(string lceid, int lan)
        {
            // below: logic below is based on LCEID data in service.xml
            long id;

            id = global::Ia.Cl.Model.Default.HexToDec(lceid);
            id = (id - 48000) / 16 * 100000;
            id += lan;

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read all services for a certain LCEID
        /// </summary>
        public static List<Service> ReadList(string lceidName)
        {
            List<Service> serviceList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceList = (from q in db.Services where q.LCEIDName == lceidName select q).ToList();
            }

            return serviceList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read service of a DN
        /// </summary>
        public static Service Read(long dn)
        {
            Service service;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                service = (from q in db.Services where q.DN == dn select q).SingleOrDefault();
            }

            return service;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read all services for a DN list
        /// </summary>
        public static List<Service> ReadList(ArrayList dnList)
        {
            long i;
            long[] sp;
            List<Service> serviceList;

            i = 0;
            sp = new long[dnList.Count];

            foreach (long l in dnList) sp[i++] = l;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                //serviceList = (from q in db.Services where dnList.Contains(q.DN) select q).ToList();

                // var pages = context.Pages.Where(x => keys.Any(key => x.Title.Contains(key)));
                serviceList = db.Services.Where(q => sp.Any(v => q.DN == v)).ToList();
            }

            return serviceList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Update(Service updatedService)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.DN != updatedService.DN) { this.DN = updatedService.DN; updated = true; }
            if (this.AREACODE != updatedService.AREACODE) { this.AREACODE = updatedService.AREACODE; updated = true; }
            if (this.IDFDN != updatedService.IDFDN) { this.IDFDN = updatedService.IDFDN; updated = true; }
            if (this.REFDN != updatedService.REFDN) { this.REFDN = updatedService.REFDN; updated = true; }
            if (this.TAXDN != updatedService.TAXDN) { this.TAXDN = updatedService.TAXDN; updated = true; }
            if (this.SUBTYP != updatedService.SUBTYP) { this.SUBTYP = updatedService.SUBTYP; updated = true; }
            if (this.ACCNBR != updatedService.ACCNBR) { this.ACCNBR = updatedService.ACCNBR; updated = true; }
            if (this.ACCTYPE != updatedService.ACCTYPE) { this.ACCTYPE = updatedService.ACCTYPE; updated = true; }
            if (this.LINEHNT != updatedService.LINEHNT) { this.LINEHNT = updatedService.LINEHNT; updated = true; }
            if (this.TCEHNT != updatedService.TCEHNT) { this.TCEHNT = updatedService.TCEHNT; updated = true; }
            if (this.SCOPE != updatedService.SCOPE) { this.SCOPE = updatedService.SCOPE; updated = true; }
            if (this.RECALL != updatedService.RECALL) { this.RECALL = updatedService.RECALL; updated = true; }
            if (this.LCEID != updatedService.LCEID) { this.LCEID = updatedService.LCEID; updated = true; }
            if (this.LCEIDName != updatedService.LCEIDName) { this.LCEIDName = updatedService.LCEIDName; updated = true; }
            if (this.NA != updatedService.NA) { this.NA = updatedService.NA; updated = true; }
            if (this.LAN != updatedService.LAN) { this.LAN = updatedService.LAN; updated = true; }
            if (this.SUBGNBR != updatedService.SUBGNBR) { this.SUBGNBR = updatedService.SUBGNBR; updated = true; }
            if (this.ORGCH != updatedService.ORGCH) { this.ORGCH = updatedService.ORGCH; updated = true; }
            if (this.ORGRTG != updatedService.ORGRTG) { this.ORGRTG = updatedService.ORGRTG; updated = true; }
            if (this.ACB != updatedService.ACB) { this.ACB = updatedService.ACB; updated = true; }
            if (this.OCBP != updatedService.OCBP) { this.OCBP = updatedService.OCBP; updated = true; }
            if (this.OCBUC_ass != updatedService.OCBUC_ass) { this.OCBUC_ass = updatedService.OCBUC_ass; updated = true; }
            if (this.OCBUC_act != updatedService.OCBUC_act) { this.OCBUC_act = updatedService.OCBUC_act; updated = true; }
            if (this.OCBUC_lev != updatedService.OCBUC_lev) { this.OCBUC_lev = updatedService.OCBUC_lev; updated = true; }
            if (this.CLIP != updatedService.CLIP) { this.CLIP = updatedService.CLIP; updated = true; }
            if (this.CLIR_ass != updatedService.CLIR_ass) { this.CLIR_ass = updatedService.CLIR_ass; updated = true; }
            if (this.CLIR_perm != updatedService.CLIR_perm) { this.CLIR_perm = updatedService.CLIR_perm; updated = true; }
            if (this.CLIR_pres != updatedService.CLIR_pres) { this.CLIR_pres = updatedService.CLIR_pres; updated = true; }
            if (this.TPS_ass != updatedService.TPS_ass) { this.TPS_ass = updatedService.TPS_ass; updated = true; }
            if (this.TPS_type != updatedService.TPS_type) { this.TPS_type = updatedService.TPS_type; updated = true; }
            if (this.CONF != updatedService.CONF) { this.CONF = updatedService.CONF; updated = true; }
            if (this.CW_ass != updatedService.CW_ass) { this.CW_ass = updatedService.CW_ass; updated = true; }
            if (this.CW_act != updatedService.CW_act) { this.CW_act = updatedService.CW_act; updated = true; }
            if (this.CW_notcg != updatedService.CW_notcg) { this.CW_notcg = updatedService.CW_notcg; updated = true; }
            if (this.CFU_ass != updatedService.CFU_ass) { this.CFU_ass = updatedService.CFU_ass; updated = true; }
            if (this.CFU_act != updatedService.CFU_act) { this.CFU_act = updatedService.CFU_act; updated = true; }
            if (this.CFU_F_DN != updatedService.CFU_F_DN) { this.CFU_F_DN = updatedService.CFU_F_DN; updated = true; }
            if (this.CFU_WFTN != updatedService.CFU_WFTN) { this.CFU_WFTN = updatedService.CFU_WFTN; updated = true; }
            if (this.PIN_ass != updatedService.PIN_ass) { this.PIN_ass = updatedService.PIN_ass; updated = true; }
            if (this.PIN_code != updatedService.PIN_code) { this.PIN_code = updatedService.PIN_code; updated = true; }
            if (this.ALM_ass != updatedService.ALM_ass) { this.ALM_ass = updatedService.ALM_ass; updated = true; }
            if (this.ALM_once != updatedService.ALM_once) { this.ALM_once = updatedService.ALM_once; updated = true; }
            if (this.ALM_daily != updatedService.ALM_daily) { this.ALM_daily = updatedService.ALM_daily; updated = true; }
            if (this.ALM_days != updatedService.ALM_days) { this.ALM_days = updatedService.ALM_days; updated = true; }
            if (this.ABD_ass != updatedService.ABD_ass) { this.ABD_ass = updatedService.ABD_ass; updated = true; }
            if (this.ABD_Cod1 != updatedService.ABD_Cod1) { this.ABD_Cod1 = updatedService.ABD_Cod1; updated = true; }
            if (this.ABD_DN1 != updatedService.ABD_DN1) { this.ABD_DN1 = updatedService.ABD_DN1; updated = true; }
            if (this.ABD_Cod2 != updatedService.ABD_Cod2) { this.ABD_Cod2 = updatedService.ABD_Cod2; updated = true; }
            if (this.ABD_DN2 != updatedService.ABD_DN2) { this.ABD_DN2 = updatedService.ABD_DN2; updated = true; }
            if (this.ABD_Cod3 != updatedService.ABD_Cod3) { this.ABD_Cod3 = updatedService.ABD_Cod3; updated = true; }
            if (this.ABD_DN3 != updatedService.ABD_DN3) { this.ABD_DN3 = updatedService.ABD_DN3; updated = true; }
            if (this.ABD_Cod4 != updatedService.ABD_Cod4) { this.ABD_Cod4 = updatedService.ABD_Cod4; updated = true; }
            if (this.ABD_DN4 != updatedService.ABD_DN4) { this.ABD_DN4 = updatedService.ABD_DN4; updated = true; }
            if (this.ABD_Cod5 != updatedService.ABD_Cod5) { this.ABD_Cod5 = updatedService.ABD_Cod5; updated = true; }
            if (this.ABD_DN5 != updatedService.ABD_DN5) { this.ABD_DN5 = updatedService.ABD_DN5; updated = true; }
            if (this.ABD_Cod6 != updatedService.ABD_Cod6) { this.ABD_Cod6 = updatedService.ABD_Cod6; updated = true; }
            if (this.ABD_DN6 != updatedService.ABD_DN6) { this.ABD_DN6 = updatedService.ABD_DN6; updated = true; }
            if (this.ABD_Cod7 != updatedService.ABD_Cod7) { this.ABD_Cod7 = updatedService.ABD_Cod7; updated = true; }
            if (this.ABD_DN7 != updatedService.ABD_DN7) { this.ABD_DN7 = updatedService.ABD_DN7; updated = true; }
            if (this.ABD_Cod8 != updatedService.ABD_Cod8) { this.ABD_Cod8 = updatedService.ABD_Cod8; updated = true; }
            if (this.ABD_DN8 != updatedService.ABD_DN8) { this.ABD_DN8 = updatedService.ABD_DN8; updated = true; }
            if (this.ABD_Cod9 != updatedService.ABD_Cod9) { this.ABD_Cod9 = updatedService.ABD_Cod9; updated = true; }
            if (this.ABD_DN9 != updatedService.ABD_DN9) { this.ABD_DN9 = updatedService.ABD_DN9; updated = true; }
            if (this.ABD_Cod10 != updatedService.ABD_Cod10) { this.ABD_Cod10 = updatedService.ABD_Cod10; updated = true; }
            if (this.ABD_DN10 != updatedService.ABD_DN10) { this.ABD_DN10 = updatedService.ABD_DN10; updated = true; }

            if (updated) this.Updated = DateTime.UtcNow.AddHours(3);

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
         */ 
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}