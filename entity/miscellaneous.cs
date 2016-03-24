using System;
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
    /// Miscellaneous Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class Miscellaneous
    {
        /// <summary/>
        public Miscellaneous() { }

        /// <summary/>
        public long Id { get; set; }
        /// <summary/>
        public int TypeId { get; set; }
        /// <summary/>
        public string Name { get; set; }
        /// <summary/>
        public string Value { get; set; }
        /// <summary/>
        public DateTime Created { get; set; }
        /// <summary/>
        public DateTime Updated { get; set; }
        /// <summary/>
        public DateTime Inspected { get; set; }
        /// <summary/>
        public System.Guid UserId { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(Miscellaneous item, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                item.Created = item.Updated = item.Inspected = DateTime.UtcNow.AddHours(3);

                db.Miscellaneous.Add(item);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(string name, string value, out string result)
        {
            bool b;
            Miscellaneous misc;

            b = false;
            result = "";
            misc = new Ia.Ngn.Cl.Model.Miscellaneous();

            misc.Name = name;
            misc.Value = value;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                misc.Created = misc.Updated = misc.Inspected = DateTime.UtcNow.AddHours(3);

                db.Miscellaneous.Add(misc);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Miscellaneous Read(long id)
        {
            Miscellaneous item;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                item = (from q in db.Miscellaneous where q.Id == id select q).SingleOrDefault();
            }

            return item;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string Read(string name)
        {
            string value;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                value = (from q in db.Miscellaneous where q.Name == name select q.Value).SingleOrDefault();
            }

            return value;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Miscellaneous> ReadList()
        {
            List<Miscellaneous> itemList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                itemList = (from q in db.Miscellaneous select q).ToList();
            }

            return itemList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(Miscellaneous item, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                item = (from q in db.Miscellaneous where q.Id == item.Id select q).SingleOrDefault();

                item.Updated = DateTime.UtcNow.AddHours(3);

                db.Miscellaneous.Attach(item);

                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(string name, string value, out string result)
        {
            bool b;
            Miscellaneous misc;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                misc = (from q in db.Miscellaneous where q.Name == name select q).SingleOrDefault();

                if (misc == null)
                {
                    misc = new Ia.Ngn.Cl.Model.Miscellaneous();

                    misc.Name = name;
                    misc.Value = value;
                    misc.Created = misc.Updated = misc.Inspected = DateTime.UtcNow.AddHours(3);

                    db.Miscellaneous.Add(misc);
                    b = true;
                }
                else
                {
                    misc.Updated = DateTime.UtcNow.AddHours(3);

                    misc.Value = value;

                    db.Miscellaneous.Attach(misc);

                    db.Entry(misc).State = System.Data.Entity.EntityState.Modified;

                    b = true;
                }

                db.SaveChanges();
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Update(/*Ont updatedOnt*/ object updatedObject)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            //if (this.StateId != updatedOnt.StateId) { this.StateId = updatedOnt.StateId; updated = true; }

            if (updated) this.Updated = this.Inspected = DateTime.UtcNow.AddHours(3);
            //else this.Inspected = DateTime.UtcNow.AddHours(3);

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Delete(long id, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                var v = (from q in db.Miscellaneous where q.Id == id select q).FirstOrDefault();

                db.Miscellaneous.Remove(v);
                db.SaveChanges();

                b = true;
            }

            return b;
        }


        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Equal(Miscellaneous item)
        {
            // below: this will not check the Id, Created, Updated, or Inspected fields
            bool areEqual;

            if (this.TypeId != item.TypeId) areEqual = false;
            else if (this.Name != item.Name) areEqual = false;
            else if (this.Value != item.Value) areEqual = false;
            else areEqual = true;

            return areEqual;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Update(Miscellaneous item)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.TypeId != item.TypeId) { this.TypeId = item.TypeId; updated = true; }
            if (this.Name != item.Name) { this.Name = item.Name; updated = true; }
            if (this.Value != item.Value) { this.Value = item.Value; updated = true; }
            if (this.UserId != item.UserId) { this.UserId = item.UserId; updated = true; }

            if (updated) this.Updated = this.Inspected = DateTime.UtcNow.AddHours(3);
            //else this.Inspected = DateTime.UtcNow.AddHours(3);

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}