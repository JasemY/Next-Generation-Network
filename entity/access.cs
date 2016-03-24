using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Ia.Ngn.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Access Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class Access
    {
        /// <summary/>
        public Access() { }

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        /// <summary/>
        public int Olt { get; set; }
        /// <summary/>
        public int Pon { get; set; }
        /// <summary/>
        public int Ont { get; set; }
        /// <summary/>
        public string Odf { get; set; }
        /// <summary/>
        public int AreaId { get; set; }
        /// <summary/>
        public int Block { get; set; }
        /// <summary/>
        public string Street { get; set; }
        /// <summary/>
        public string PremisesOld { get; set; }
        /// <summary/>
        public string PremisesNew { get; set; }
        /// <summary/>
        public DateTime Created { get; set; }
        /// <summary/>
        public DateTime Updated { get; set; }
        /// <summary/>
        public DateTime Inspected { get; set; }
        /// <summary/>
        public System.Guid UserId { get; set; }

        /// <summary/>
        public virtual ICollection<Ont> Onts { get; set; }

        /// <summary/>
        [NotMapped]
        public string Name
        {
            get
            {
                return Ia.Ngn.Cl.Model.Business.Access.Name(this);
            }
        }

        /// <summary/>
        [NotMapped]
        public string Position
        {
            get
            {
                string position;

                position = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where q.Pon.Olt.Id == this.Olt && q.Pon.Number == this.Pon && q.Number == this.Ont select q.Position).SingleOrDefault();

                return position;
            }
        }

        /// <summary/>
        [NotMapped]
        public string Address
        {
            get
            {
                string address;

                address = (from q in Ia.Ngn.Cl.Model.Data.Service.KuwaitNgnAreaList where q.Id == this.AreaId select q.Name).SingleOrDefault();

                address += ", block " + this.Block + ", street " + this.Street + ", old p. " + this.PremisesOld + ", new p. " + this.PremisesNew;

                return address;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string AccessId(int oltId, int ponNumber, int ontNumber)
        {
            string id;

            id = oltId.ToString() + ponNumber.ToString().PadLeft(3, '0') + ontNumber.ToString().PadLeft(3, '0');

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string AccessId(string ontId)
        {
            int oltId, rack, sub, card, port, ontNumber;
            string id;
            Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont ont;

            Ia.Ngn.Cl.Model.Business.Ont.OltIdRackSubCardPortOntFromOntId(ontId, out oltId, out rack, out sub, out card, out port, out ontNumber);

            ont = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where q.Pon.Olt.Id == oltId && q.Rack == rack && q.Sub == sub && q.Card == card && q.Port == port && q.Number == ontNumber select q).SingleOrDefault();

            if (ont != null) id = AccessId(oltId, ont.Pon.Number, ont.Number);
            else id = "0";

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(Access newItem, out string result)
        {
            bool b;

            b = false;

            try
            {
                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    newItem.Created = newItem.Updated = DateTime.UtcNow.AddHours(3);

                    db.Accesses.Add(newItem);
                    db.SaveChanges();

                    b = true;
                    result = "Access record created. ";
                }
            }
            catch(Exception ex)
            {
                result = "Access record was not created. " + ex.Message;

                b = false;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Access Read(Ia.Ngn.Cl.Model.Ngn db, string id)
        {
            Access item;

            item = (from q in db.Accesses where q.Id == id select q).SingleOrDefault();

            return item;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Access Read(string id)
        {
            Access item;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                item = (from q in db.Accesses where q.Id == id select q).SingleOrDefault();
            }

            return item;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Access Read(int areaId, int ponNumber, int ontNumber)
        {
            Access item;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                item = (from q in db.Accesses where q.AreaId == areaId && q.Pon == ponNumber && q.Ont == ontNumber select q).SingleOrDefault();
            }

            return item;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Access Read(Ia.Ngn.Cl.Model.Ngn db, int areaId, int ponNumber, int ontNumber)
        {
            return (from q in db.Accesses where q.AreaId == areaId && q.Pon == ponNumber && q.Ont == ontNumber select q).SingleOrDefault();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Access> ReadList()
        {
            List<Access> itemList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                itemList = (from q in db.Accesses select q).ToList();
            }

            return itemList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Access> ReadListOfAreaId(int areaId)
        {
            List<Access> itemList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                itemList = (from q in db.Accesses where q.AreaId == areaId select q).ToList();
            }

            return itemList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Access> ReadList(int oltId, int ponNumber)
        {
            List<Access> itemList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                itemList = (from q in db.Accesses where q.Olt == oltId && q.Pon == ponNumber select q).ToList();
            }

            return itemList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(Access updatedAccess, out string result)
        {
            bool b;
            Access access;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                access = (from q in db.Accesses where q.Id == updatedAccess.Id select q).SingleOrDefault();

                if (access.Update(updatedAccess))
                {
                    db.Accesses.Attach(access);
                    db.Entry(access).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();

                b = true;
            }

            return b;
        }

     	////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Update(Access updatedAccess)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.AreaId != updatedAccess.AreaId) { this.AreaId = updatedAccess.AreaId; updated = true; }
            if (this.Block != updatedAccess.Block) { this.Block = updatedAccess.Block; updated = true; }
            //if (this.Name != updatedAccess.Name) { this.Name = updatedAccess.Name; updated = true; }
            if (this.Odf != updatedAccess.Odf) { this.Odf = updatedAccess.Odf; updated = true; }
            if (this.Olt != updatedAccess.Olt) { this.Olt = updatedAccess.Olt; updated = true; }
            if (this.Ont != updatedAccess.Ont) { this.Ont = updatedAccess.Ont; updated = true; }
            if (this.Onts != updatedAccess.Onts) { this.Onts = updatedAccess.Onts; updated = true; }
            if (this.Pon != updatedAccess.Pon) { this.Pon = updatedAccess.Pon; updated = true; }
            //if (this.Position != updatedAccess.Position) { this.Position = updatedAccess.Position; updated = true; }
            if (this.PremisesNew != updatedAccess.PremisesNew) { this.PremisesNew = updatedAccess.PremisesNew; updated = true; }
            if (this.PremisesOld != updatedAccess.PremisesOld) { this.PremisesOld = updatedAccess.PremisesOld; updated = true; }
            if (this.Street != updatedAccess.Street) { this.Street = updatedAccess.Street; updated = true; }
            if (this.UserId != updatedAccess.UserId) { this.UserId = updatedAccess.UserId; updated = true; }

            if (updated) this.Updated = this.Inspected = DateTime.UtcNow.AddHours(3);
            //else this.Inspected = DateTime.UtcNow.AddHours(3);

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Delete(string id, out string result)
        {
            bool b;

            b = false;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                var v = (from q in db.Accesses where q.Id == id select q).FirstOrDefault();

                db.Accesses.Remove(v);
                db.SaveChanges();

                b = true;
                result = "Access record deleted. ";
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}