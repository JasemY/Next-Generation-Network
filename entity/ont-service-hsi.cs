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
    /// ONT-SERVICEHSI Entity Framework class for Next Generation Network (NGN) entity model.
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

        /// <summary/>
        public enum PriorityQueueProfile { A = 1 };

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        /// <summary/>
        public int StateId { get; set; }
        /// <summary/>
        public int Card { get; set; }
        /// <summary/>
        public int Port { get; set; }
        /// <summary/>
        public int Service { get; set; }
        /// <summary/>
        public int UpstreamBandwidthProfileId { get; set; }
        /// <summary/>
        public int DownstreamBandwidthProfileId { get; set; }
        /// <summary/>
        public int PriorityQueueProfileId { get; set; }
        /// <summary/>
        public bool Aes { get; set; }
        /// <summary/>
        public int Svlan { get; set; }
        /// <summary/>
        public string Label { get; set; }
        /// <summary/>
        public string Customer { get; set; }
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
        public static string OntServiceHsiId(string ontId, int card, int port, int service)
        {
            string id;

            id = ontId.ToString() + card.ToString().PadLeft(2, '0') + port.ToString().PadLeft(2, '0') + service.ToString().PadLeft(2, '0');

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(OntServiceHsi ontServiceHsi, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ontServiceHsi.Created = ontServiceHsi.Updated = ontServiceHsi.Inspected = DateTime.UtcNow.AddHours(3);

                db.OntServiceHsis.Add(ontServiceHsi);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static OntServiceHsi Read(string id)
        {
            OntServiceHsi ontServiceHsi;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ontServiceHsi = (from q in db.OntServiceHsis where q.Id == id select q).SingleOrDefault();
            }

            return ontServiceHsi;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<OntServiceHsi> ReadList()
        {
            List<OntServiceHsi> ontServiceHsiList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ontServiceHsiList = (from q in db.OntServiceHsis select q).ToList();
            }

            return ontServiceHsiList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(OntServiceHsi ontServiceHsi, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ontServiceHsi = (from q in db.OntServiceHsis where q.Id == ontServiceHsi.Id select q).SingleOrDefault();

                ontServiceHsi.Updated = DateTime.UtcNow.AddHours(3);

                db.OntServiceHsis.Attach(ontServiceHsi);

                var v = db.Entry(ontServiceHsi);
                v.State = System.Data.Entity.EntityState.Modified;
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
                var v = (from q in db.OntServiceHsis where q.Id == id select q).FirstOrDefault();

                db.OntServiceHsis.Remove(v);
                db.SaveChanges();

                b = true;
            }

            return b;
        }


        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Equal(OntServiceHsi b)
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
        public bool Update(OntServiceHsi b)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.Aes != b.Aes) { this.Aes = b.Aes; updated = true; }
            if (this.Card != b.Card) { this.Card = b.Card; updated = true; }
            if (this.Customer != b.Customer) { this.Customer = b.Customer; updated = true; }
            if (this.DownstreamBandwidthProfileId != b.DownstreamBandwidthProfileId) { this.DownstreamBandwidthProfileId = b.DownstreamBandwidthProfileId; updated = true; }
            if (this.Label != b.Label) { this.Label = b.Label; updated = true; }
            if (this.Ont != b.Ont) { this.Ont = b.Ont; updated = true; }
            if (this.Port != b.Port) { this.Port = b.Port; updated = true; }

            if (this.PriorityQueueProfileId != b.PriorityQueueProfileId) { this.PriorityQueueProfileId = b.PriorityQueueProfileId; updated = true; }
            if (this.Service != b.Service) { this.Service = b.Service; updated = true; }
            if (this.StateId != b.StateId) { this.StateId = b.StateId; updated = true; }
            if (this.Svlan != b.Svlan) { this.Svlan = b.Svlan; updated = true; }
            if (this.UpstreamBandwidthProfileId != b.UpstreamBandwidthProfileId) { this.UpstreamBandwidthProfileId = b.UpstreamBandwidthProfileId; updated = true; }
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