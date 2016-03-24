using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Ia.Ngn.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Logical-Circuit Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class LogicalCircuit
    {
        /// <summary/>
        public LogicalCircuit() { }

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary/>
        public int GatewayIndex { get; set; } // GWY_INDEX

        /// <summary/>
        public int TerminationIdIndex { get; set; } // TERMID_INDEX

        /// <summary/>
        public string GatewayName { get; set; } // GWY_NAME

        /// <summary/>
        public string TerminationString { get; set; } // TERM_STRING

        /// <summary/>
        public int Lceid { get; set; } // LCEID

        /// <summary/>
        public string LceidName { get; set; } // LCEID Name

        /// <summary/>
        public int Tn { get; set; } // TN

        /// <summary/>
        public DateTime Created { get; set; }

        /// <summary/>
        public DateTime Updated { get; set; }

        /*
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int LogicalCircuitId(string lceid, int lan)
        {
            // below: logic below is based on LCEID data in service.xml
            int id;

            id = global::Ia.Cl.Model.Default.HexToDec(lceid);
            id = (id - 48000) / 16 * 100000;
            id += lan;

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int LogicalCircuitId(int lceid, int lan)
        {
            // below: logic below is based on LCEID data in service.xml
            int id;

            id = (lceid - 48000) / 16 * 100000;
            id += lan;

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(LogicalCircuit newLogicalCircuit, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                newLogicalCircuit.Created = newLogicalCircuit.Updated = DateTime.UtcNow.AddHours(3);

                db.LogicalCircuits.Add(newLogicalCircuit);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static LogicalCircuit Read(int id)
        {
            LogicalCircuit logicalCircuit;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                logicalCircuit = (from q in db.LogicalCircuits where q.Id == id select q).SingleOrDefault();
            }

            return logicalCircuit;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read all logical circuits
        /// </summary>
        public static List<LogicalCircuit> ReadList()
        {
            List<LogicalCircuit> logicalCircuitList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                logicalCircuitList = (from q in db.LogicalCircuits orderby q.Id select q).ToList();
            }

            return logicalCircuitList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read all logical circuits for a certain LCEID
        /// </summary>
        public static List<LogicalCircuit> ReadList(string lceidName)
        {
            int logicalCircuitNumber;
            List<LogicalCircuit> logicalCircuitList;

            // lceid = Ia.Ngn.Cl.Model.Data.Service.LceidInDecFromLceidName(lceidName);
            logicalCircuitNumber = (from q in Ia.Ngn.Cl.Model.Data.Service.LceidList where q.Name == lceidName select q.LogicalCircuitNumber).SingleOrDefault();

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                logicalCircuitList = (from q in db.LogicalCircuits where q.Lceid == logicalCircuitNumber select q).ToList();
            }

            return logicalCircuitList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read all logical circuits within lan range given the lceid number
        /// </summary>
        public static List<LogicalCircuit> ReadList(int lceid, int startTn, int endTn)
        {
            List<LogicalCircuit> logicalCircuitList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                logicalCircuitList = (from q in db.LogicalCircuits where q.Lceid == lceid && q.Tn >= startTn && q.Tn <= endTn select q).ToList();
            }

            return logicalCircuitList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Update(LogicalCircuit updatedLogicalCircuit)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.GatewayIndex != updatedLogicalCircuit.GatewayIndex) { this.GatewayIndex = updatedLogicalCircuit.GatewayIndex; updated = true; }
            if (this.GatewayName != updatedLogicalCircuit.GatewayName) { this.GatewayName = updatedLogicalCircuit.GatewayName; updated = true; }
            if (this.Lceid != updatedLogicalCircuit.Lceid) { this.Lceid = updatedLogicalCircuit.Lceid; updated = true; }
            if (this.LceidName != updatedLogicalCircuit.LceidName) { this.LceidName = updatedLogicalCircuit.LceidName; updated = true; }
            if (this.TerminationIdIndex != updatedLogicalCircuit.TerminationIdIndex) { this.TerminationIdIndex = updatedLogicalCircuit.TerminationIdIndex; updated = true; }
            if (this.TerminationString != updatedLogicalCircuit.TerminationString) { this.TerminationString = updatedLogicalCircuit.TerminationString; updated = true; }
            if (this.Tn != updatedLogicalCircuit.Tn) { this.Tn = updatedLogicalCircuit.Tn; updated = true; }

            if (updated) this.Updated = /*this.Inspected =* / DateTime.UtcNow.AddHours(3);
            //else this.Inspected = DateTime.UtcNow.AddHours(3);

            return updated;
        }

        //////////////////////////////////////////////////////////////////////////////

        ///// <summary>
        /////
        ///// </summary>
        //public static bool CreateOrUpdate(LogicalCircuit updatedLogicalCircuit, out string result)
        //{
        //    bool b;

        //    b = false;
        //    result = "";

        //    using (var db = new Ia.Ngn.Cl.Model.Ngn())
        //    {
        //        if (Read(updatedLogicalCircuit.Id) != null) b = Update(updatedLogicalCircuit, out result);
        //        else b = Create(updatedLogicalCircuit, out result);
        //    }

        //    return b;
        //}

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Delete(int id, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                var x = (from q in db.LogicalCircuits where q.Id == id select q).FirstOrDefault();

                db.LogicalCircuits.Remove(x);
                db.SaveChanges();

                b = true;
            }

            return b;
        }
         */ 

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}