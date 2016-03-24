using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ia.Ngn.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// ONT-SERVICEVOIP Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class OntServiceVoip
    {
        /// <summary/>
        public OntServiceVoip() { }

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        /// <summary/>
        public int StateId { get; set; }
        /// <summary/>
        public int Svlan { get; set; }
        /// <summary/>
        public string Ip { get; set; }
        /// <summary/>
        public string FtpIp { get; set; }
        /// <summary/>
        public string MgcIp { get; set; }
        /// <summary/>
        public string MgcSecondaryIp { get; set; }
        /// <summary/>
        public string ConfiguratinFile { get; set; }
        /// <summary/>
        public string Customer { get; set; }
        /// <summary/>
        public string Label { get; set; }
        /// <summary/>
        public DateTime Created { get; set; }
        /// <summary/>
        public DateTime Updated { get; set; }
        /// <summary/>
        public DateTime Inspected { get; set; }
        /// <summary/>
        public System.Guid UserId { get; set; }
        /// <summary/>
        public virtual Ont Ont { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string OntServiceVoipId(string ontId, int card)
        {
            string id;

            id = ontId + card.ToString().PadLeft(2, '0');

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(OntServiceVoip ontServiceVoip, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ontServiceVoip.Created = ontServiceVoip.Updated = ontServiceVoip.Inspected = DateTime.UtcNow.AddHours(3);

                db.OntServiceVoips.Add(ontServiceVoip);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static OntServiceVoip Read(string id)
        {
            OntServiceVoip ontServiceVoip;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ontServiceVoip = (from q in db.OntServiceVoips where q.Id == id select q).SingleOrDefault();
            }

            return ontServiceVoip;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<OntServiceVoip> ReadList()
        {
            List<OntServiceVoip> ontServiceVoipList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ontServiceVoipList = (from q in db.OntServiceVoips select q).ToList();
            }

            return ontServiceVoipList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(OntServiceVoip ontServiceVoip, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ontServiceVoip = (from q in db.OntServiceVoips where q.Id == ontServiceVoip.Id select q).SingleOrDefault();

                ontServiceVoip.Updated = DateTime.UtcNow.AddHours(3);

                db.OntServiceVoips.Attach(ontServiceVoip);

                db.Entry(ontServiceVoip).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Delete(string id, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                var v = (from q in db.OntServiceVoips where q.Id == id select q).FirstOrDefault();

                db.OntServiceVoips.Remove(v);
                db.SaveChanges();

                b = true;
            }

            return b;
        }


        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Equal(OntServiceVoip b)
        {
            // below: this will not check the Id, Created, Updated, or Inspected fields
            bool areEqual;

            /*if (this.BatteryBackupAvailable != b.BatteryBackupAvailable) areEqual = false;
            else*/ areEqual = true;

            return areEqual;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Update(OntServiceVoip b)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.ConfiguratinFile != b.ConfiguratinFile) { this.ConfiguratinFile = b.ConfiguratinFile; updated = true; }
            if (this.Customer != b.Customer) { this.Customer = b.Customer; updated = true; }
            if (this.FtpIp != b.FtpIp) { this.FtpIp = b.FtpIp; updated = true; }
            if (this.Ip != b.Ip) { this.Ip = b.Ip; updated = true; }
            if (this.Label != b.Label) { this.Label = b.Label; updated = true; }
            if (this.MgcIp != b.MgcIp) { this.MgcIp = b.MgcIp; updated = true; }
            if (this.MgcSecondaryIp != b.MgcSecondaryIp) { this.MgcSecondaryIp = b.MgcSecondaryIp; updated = true; }

            if (this.Ont != b.Ont) { this.Ont = b.Ont; updated = true; }
            if (this.StateId != b.StateId) { this.StateId = b.StateId; updated = true; }
            if (this.Svlan != b.Svlan) { this.Svlan = b.Svlan; updated = true; }
            if (this.UserId != b.UserId) { this.UserId = b.UserId; updated = true; }

            if(updated) this.Updated = this.Inspected = DateTime.UtcNow.AddHours(3);
            //else this.Inspected = DateTime.UtcNow.AddHours(3);

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}