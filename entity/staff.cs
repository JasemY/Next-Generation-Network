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
    /// Staff Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class Staff
    {
        /// <summary/>
        public Staff() { }

        /// <summary/>
        public int Id { get; set; }

        /// <summary/>
        public bool IsHead { get; set; }

        /// <summary/>
        public int AdministrativeFrameworkId { get; set; }

        /// <summary/>
        public string FirstName { get; set; }

        /// <summary/>
        public string MiddleName { get; set; }

        /// <summary/>
        public string LastName { get; set; }

        /// <summary/>
        public long CivilId { get; set; }

        /// <summary/>
        public int EmploymentId { get; set; }

        /// <summary/>
        public DateTime? EmploymentDate { get; set; }

        /// <summary/>
        public string IpPbxExtension { get; set; }

        /// <summary/>
        public string TwitterUserName { get; set; }

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

        /// <summary/>
        [NotMapped]
        public virtual Ia.Ngn.Cl.Model.Data.Administration.Framework Framework { get; set; }

        /// <summary/>
        [NotMapped]
        public Staff Head { get; set; }

        /// <summary/>
        [NotMapped]
        public List<Staff> Heads { get; set; }

        /// <summary/>
        [NotMapped]
        public List<Staff> Colleagues  { get; set; }

        /// <summary/>
        [NotMapped]
        public List<Staff> Subordinates  { get; set; }

        /// <summary/>
        [NotMapped]
        public Ia.Cl.Model.Identity.User User  { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(Staff newStaff, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                newStaff.Created = newStaff.Updated = DateTime.UtcNow.AddHours(3);

                db.Staff.Add(newStaff);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Staff Read(int id)
        {
            Staff staff;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                staff = (from q in db.Staff where q.Id == id select q).SingleOrDefault();
            }

            return staff;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Staff> ReadList()
        {
            List<Staff> staffList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                staffList = (from q in db.Staff select q).ToList();
            }

            return staffList.ToList();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(Staff updatedStaff, out string result)
        {
            bool b;
            Staff staff;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                staff = (from q in db.Staff where q.Id == updatedStaff.Id select q).SingleOrDefault();

                if (staff.Update(updatedStaff))
                {
                    db.Staff.Attach(staff);
                    db.Entry(staff).State = System.Data.Entity.EntityState.Modified;
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
        public bool Update(Staff updatedStaff)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.FirstName != updatedStaff.FirstName) { this.FirstName = updatedStaff.FirstName; updated = true; }
            if (this.MiddleName != updatedStaff.MiddleName) { this.MiddleName = updatedStaff.MiddleName; updated = true; }
            if (this.LastName != updatedStaff.LastName) { this.LastName = updatedStaff.LastName; updated = true; }
            if (this.CivilId != updatedStaff.CivilId) { this.CivilId = updatedStaff.CivilId; updated = true; }
            if (this.EmploymentId != updatedStaff.EmploymentId) { this.EmploymentId = updatedStaff.EmploymentId; updated = true; }
            if (this.EmploymentDate != updatedStaff.EmploymentDate) { this.EmploymentDate = updatedStaff.EmploymentDate; updated = true; }
            if (this.IpPbxExtension != updatedStaff.IpPbxExtension) { this.IpPbxExtension = updatedStaff.IpPbxExtension; updated = true; }
            if (this.TwitterUserName != updatedStaff.TwitterUserName) { this.TwitterUserName = updatedStaff.TwitterUserName; updated = true; }
            if (this.IsHead != updatedStaff.IsHead) { this.IsHead = updatedStaff.IsHead; updated = true; }
            if (this.AdministrativeFrameworkId != updatedStaff.AdministrativeFrameworkId) { this.AdministrativeFrameworkId = updatedStaff.AdministrativeFrameworkId; updated = true; }
            if (this.UserId != updatedStaff.UserId) { this.UserId = updatedStaff.UserId; updated = true; }

            if (updated) this.Updated = /*this.Inspected =*/ DateTime.UtcNow.AddHours(3);
            //else this.Inspected = DateTime.UtcNow.AddHours(3);

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool AssignUserId(int staffId, Guid userId)
        {
            bool b;
            Staff updatedStaff;

            b = false;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                updatedStaff = (from q in db.Staff where q.Id == staffId select q).SingleOrDefault();

                if (updatedStaff != null)
                {
                    updatedStaff.UserId = userId;
                    updatedStaff.Updated = DateTime.UtcNow.AddHours(3);

                    db.Staff.Attach(updatedStaff);

                    var v = db.Entry(updatedStaff);
                    v.State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    b = true;
                }
                else
                {
                    b = false;
                }
            }

            return b;
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
                var v = (from q in db.Staff where q.Id == id select q).FirstOrDefault();

                db.Staff.Remove(v);
                db.SaveChanges();

                b = true;
                result = "Staff record deleted. ";
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}