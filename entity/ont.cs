using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// ONT Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class Ont
    {
        /// <summary/>
        public Ont() { }

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        /// <summary/>
        public int StateId { get; set; }
        /// <summary/>
        public int FamilyTypeId { get; set; }
        /// <summary/>
        public string Serial { get; set; }
        /// <summary/>
        public string EquipmentId { get; set; }
        /// <summary/>
        public int VendorId { get; set; }
        /// <summary/>
        public string ActiveSoftware { get; set; }
        /// <summary/>
        public string PassiveSoftware { get; set; }
        /// <summary/>
        public string PlannedSoftware { get; set; }
        /// <summary/>
        public bool BatteryBackupAvailable { get; set; }
        /// <summary/>
        public string Description1 { get; set; }
        /// <summary/>
        public string Description2 { get; set; }
        /// <summary/>
        public DateTime Created { get; set; }
        /// <summary/>
        public DateTime Updated { get; set; }
        /// <summary/>
        public DateTime Inspected { get; set; }
        /// <summary/>
        public System.Guid UserId { get; set; }

        /// <summary/>
        public virtual Access Access { get; set; }
        /// <summary/>
        public virtual ICollection<OntServiceHsi> OntServiceHsis { get; set; }
        /// <summary/>
        public virtual ICollection<OntServiceVoip> OntServiceVoips { get; set; }
        /// <summary/>
        public virtual ICollection<OntOntPots> OntOntPotses { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Equal(Ia.Ngn.Cl.Model.Ont b)
        {
            // below: this will not check the Id, Created, Updated, or Inspected fields
            bool areEqual;

            if (this.BatteryBackupAvailable != b.BatteryBackupAvailable) areEqual = false;
            else areEqual = true;

            return areEqual;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Update(Ia.Ngn.Cl.Model.Ont updatedOnt)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.StateId != updatedOnt.StateId) { this.StateId = updatedOnt.StateId; updated = true; }
            if (this.FamilyTypeId != updatedOnt.FamilyTypeId) { this.FamilyTypeId = updatedOnt.FamilyTypeId; updated = true; }
            if (this.Serial != updatedOnt.Serial) { this.Serial = updatedOnt.Serial; updated = true; }
            if (this.EquipmentId != updatedOnt.EquipmentId) { this.EquipmentId = updatedOnt.EquipmentId; updated = true; }
            if (this.VendorId != updatedOnt.VendorId) { this.VendorId = updatedOnt.VendorId; updated = true; }
            if (this.ActiveSoftware != updatedOnt.ActiveSoftware) { this.ActiveSoftware = updatedOnt.ActiveSoftware; updated = true; }
            if (this.PassiveSoftware != updatedOnt.PassiveSoftware) { this.PassiveSoftware = updatedOnt.PassiveSoftware; updated = true; }

            if (this.PlannedSoftware != updatedOnt.PlannedSoftware) { this.PlannedSoftware = updatedOnt.PlannedSoftware; updated = true; }
            if (this.BatteryBackupAvailable != updatedOnt.BatteryBackupAvailable) { this.BatteryBackupAvailable = updatedOnt.BatteryBackupAvailable; updated = true; }
            if (this.Description1 != updatedOnt.Description1) { this.Description1 = updatedOnt.Description1; updated = true; }
            if (this.Description2 != updatedOnt.Description2) { this.Description2 = updatedOnt.Description2; updated = true; }
            if (this.UserId != updatedOnt.UserId) { this.UserId = updatedOnt.UserId; updated = true; }
            if (this.Access != updatedOnt.Access) { this.Access = updatedOnt.Access; updated = true; }

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