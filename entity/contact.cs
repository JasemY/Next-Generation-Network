using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ia.Ngn.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Contact Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class Contact
    {
        /// <summary/>
        public Contact() {}

        /// <summary/>
        public int Id { get; set; }

        /// <summary/>
        public string FirstName { get; set; }

        /// <summary/>
        public string MiddleName { get; set; }

        /// <summary/>
        public string LastName { get; set; }

        /// <summary/>
        public string Company { get; set; }

        /// <summary/>
        public string Email { get; set; }

        /// <summary/>
        public string Phone { get; set; }

        /// <summary/>
        public string Address { get; set; }

        /// <summary/>
        public int CountryId { get; set; }

        /// <summary/>
        public string Url { get; set; }

        /// <summary/>
        public bool IsApproved { get; set; }

        /// <summary/>
        public string Note { get; set; }

        /// <summary/>
        public DateTime Created { get; set; }

        /// <summary/>
        public DateTime Updated { get; set; }

        /// <summary/>
        public System.Guid UserId { get; set; }

        /// <summary/>
        [NotMapped]
        public string FullName
        {
            get
            {
                string fullName;

                fullName = FirstName + " " + MiddleName + " " + LastName;

                fullName = fullName.Replace("  ", " ");

                return fullName;
            }
        }

        /// <summary/>
        [NotMapped]
        public string FirstAndMiddleName
        {
            get
            {
                string firstAndMiddleName;

                firstAndMiddleName = FirstName + " " + MiddleName;

                firstAndMiddleName = firstAndMiddleName.Replace("  ", " ");

                return firstAndMiddleName;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(Contact newContact, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                newContact.Created = newContact.Updated = DateTime.UtcNow.AddHours(3);

                db.Contacts.Add(newContact);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Contact Read(int id)
        {
            Contact contact;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                contact = (from q in db.Contacts where q.Id == id select q).SingleOrDefault();
            }

            return contact;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Contact> List()
        {
            List<Contact> contactList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                contactList = (from q in db.Contacts select q).ToList();
            }

            return contactList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(Contact updatedContact, out string result)
        {
            bool b;
            Contact contact;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                contact = (from q in db.Contacts where q.Id == updatedContact.Id select q).SingleOrDefault();

                if (contact.Update(updatedContact))
                {
                    db.Contacts.Attach(contact);
                    db.Entry(contact).State = System.Data.Entity.EntityState.Modified;
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
        public bool Update(Contact updatedContact)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.Address != updatedContact.Address) { this.Address = updatedContact.Address; updated = true; }
            if (this.Company != updatedContact.Company) { this.Company = updatedContact.Company; updated = true; }
            if (this.CountryId != updatedContact.CountryId) { this.CountryId = updatedContact.CountryId; updated = true; }
            if (this.Email != updatedContact.Email) { this.Email = updatedContact.Email; updated = true; }
            if (this.FirstName != updatedContact.FirstName) { this.FirstName = updatedContact.FirstName; updated = true; }
            if (this.IsApproved != updatedContact.IsApproved) { this.IsApproved = updatedContact.IsApproved; updated = true; }
            if (this.LastName != updatedContact.LastName) { this.LastName = updatedContact.LastName; updated = true; }
            if (this.MiddleName != updatedContact.MiddleName) { this.MiddleName = updatedContact.MiddleName; updated = true; }
            if (this.Note != updatedContact.Note) { this.Note = updatedContact.Note; updated = true; }
            if (this.Phone != updatedContact.Phone) { this.Phone = updatedContact.Phone; updated = true; }
            if (this.Url != updatedContact.Url) { this.Url = updatedContact.Url; updated = true; }
            if (this.UserId != updatedContact.UserId) { this.UserId = updatedContact.UserId; updated = true; }

            if (updated) this.Updated = /*this.Inspected =*/ DateTime.UtcNow.AddHours(3);
            //else this.Inspected = DateTime.UtcNow.AddHours(3);

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Delete(int id, out string result)
        {
            bool b;

            b = false;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                var v = (from q in db.Contacts where q.Id == id select q).FirstOrDefault();

                db.Contacts.Remove(v);
                db.SaveChanges();

                b = true;
                result = "Contact record deleted. ";
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}
