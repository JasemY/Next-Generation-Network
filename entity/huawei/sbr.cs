using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Ia.Ngn.Cl.Model.Huawei
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Sbr Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class Sbr
    {
        /// <summary/>
        public Sbr() { }

        //ADD SBR: IMPU="tel:+96523900039", UTYPE=POTS, CPC=ORDINARY, NSCFU=1, NSCFB=1, NSCFNR=1, NSCW=1, NS3PTY=1, NSCLIP=1, NSCBA=1, NSWAKE_UP=1, ITT=1, IITT=1, ICIDD=1, COP="4498";

        /// <summary/>
        public long Id { get; set; }

        /// <summary/>
        [Key]
        public string IMPU { get; set; }
        public string UTYPE { get; set; }
        public string CPC { get; set; }
        public bool NSCFU { get; set; }
        public bool NSCFB { get; set; }
        public bool NSCFNR { get; set; }
        public bool NSCW { get; set; }
        public bool NS3PTY { get; set; }
        public bool NSCLIP { get; set; }
        public bool NSCBA { get; set; }
        public bool NSWAKE_UP { get; set; }
        public bool ITT { get; set; }
        public bool IITT { get; set; }
        public bool ICIDD { get; set; }
        public string COP { get; set; }

        public int LMTGRP { get; set; }
        public string STYPE { get; set; }
        public string TREAT { get; set; }
        
        public int NSICO { get; set; }
        public int NSOUTG { get; set; }

        /// <summary/>
        public DateTime Created { get; set; }

        /// <summary/>
        public DateTime Updated { get; set; }

        /// <summary/>
        public DateTime Viewed { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Equal(Sbr b)
        {
            // below: this will not check the Id, Created, Updated, or Viewed fields
            bool areEqual;

            if (this.IMPU != b.IMPU) areEqual = false;
            else if (this.UTYPE != b.UTYPE) areEqual = false;
            else if (this.CPC != b.CPC) areEqual = false;
            else if (this.NSCFU != b.NSCFU) areEqual = false;
            else if (this.NSCFB != b.NSCFB) areEqual = false;
            else if (this.NSCFNR != b.NSCFNR) areEqual = false;
            else if (this.NSCW != b.NSCW) areEqual = false;
            else if (this.NS3PTY != b.NS3PTY) areEqual = false;
            else if (this.NSCLIP != b.NSCLIP) areEqual = false;
            else if (this.NSCBA != b.NSCBA) areEqual = false;
            else if (this.NSWAKE_UP != b.NSWAKE_UP) areEqual = false;
            else if (this.ITT != b.ITT) areEqual = false;
            else if (this.IITT != b.IITT) areEqual = false;
            else if (this.ICIDD != b.ICIDD) areEqual = false;
            else if (this.COP != b.COP) areEqual = false;
            else if (this.LMTGRP != b.LMTGRP) areEqual = false;
            else if (this.STYPE != b.STYPE) areEqual = false;
            else if (this.TREAT != b.TREAT) areEqual = false;
            else if (this.NSICO != b.NSICO) areEqual = false;
            else if (this.NSOUTG != b.NSOUTG) areEqual = false;
            else areEqual = true;

            return areEqual;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void Update(Sbr b)
        {
            // below: this will not update Id or Created

            if (this.IMPU != b.IMPU) this.IMPU = b.IMPU;
            if (this.UTYPE != b.UTYPE) this.UTYPE = b.UTYPE;
            if (this.CPC != b.CPC) this.CPC = b.CPC;
            if (this.NSCFU != b.NSCFU) this.NSCFU = b.NSCFU;
            if (this.NSCFB != b.NSCFB) this.NSCFB = b.NSCFB;
            if (this.NSCFNR != b.NSCFNR) this.NSCFNR = b.NSCFNR;
            if (this.NSCW != b.NSCW) this.NSCW = b.NSCW;
            if (this.NS3PTY != b.NS3PTY) this.NS3PTY = b.NS3PTY;
            if (this.NSCLIP != b.NSCLIP) this.NSCLIP = b.NSCLIP;
            if (this.NSCBA != b.NSCBA) this.NSCBA = b.NSCBA;
            if (this.NSWAKE_UP != b.NSWAKE_UP) this.NSWAKE_UP = b.NSWAKE_UP;
            if (this.ITT != b.ITT) this.ITT = b.ITT;
            if (this.IITT != b.IITT) this.IITT = b.IITT;
            if (this.ICIDD != b.ICIDD) this.ICIDD = b.ICIDD;
            if (this.COP != b.COP) this.COP = b.COP;
            if (this.LMTGRP != b.LMTGRP) this.LMTGRP = b.LMTGRP;
            if (this.STYPE != b.STYPE) this.STYPE = b.STYPE;
            if (this.TREAT != b.TREAT) this.TREAT = b.TREAT;
            if (this.NSICO != b.NSICO) this.NSICO = b.NSICO;
            if (this.NSOUTG != b.NSOUTG) this.NSOUTG = b.NSOUTG;

            if (this.Updated != b.Updated) this.Updated = b.Updated;
            if (this.Viewed != b.Viewed) this.Viewed = b.Viewed;
        }

        /*
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static long ServiceId(string lceid, int lan)
        {
            // below: logic below is based on LCEID data in service.xml
            long id;

            id = Ia.Cl.Model.Default.HexToDec(lceid);
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
         */ 

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}