using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ia.Ngn.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Service Request Ont Entity Framework class for Next Generation Network (NGN) entity model.
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
    public class ServiceRequestOnt
    {
        /// <summary/>
        public ServiceRequestOnt() { }

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
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

        /// <summary/>
        public virtual ServiceRequest ServiceRequest { get; set; }

        /// <summary/>
        public virtual Access Access { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void Copy(ServiceRequestOnt serviceRequestOnt)
        {
            this.Access = serviceRequestOnt.Access;
            this.Created = serviceRequestOnt.Created;
            this.Id = serviceRequestOnt.Id;
            this.Inspected = serviceRequestOnt.Inspected;
            this.ServiceRequest = serviceRequestOnt.ServiceRequest;
            this.Updated = serviceRequestOnt.Updated;
            this.UserId = serviceRequestOnt.UserId;
            this.Value = serviceRequestOnt.Value;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Update(ServiceRequestOnt updatedServiceRequestOnt)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.Access != updatedServiceRequestOnt.Access) { this.Access = updatedServiceRequestOnt.Access; updated = true; }
            if (this.ServiceRequest != updatedServiceRequestOnt.ServiceRequest) { this.ServiceRequest = updatedServiceRequestOnt.ServiceRequest; updated = true; }
            if (this.UserId != updatedServiceRequestOnt.UserId) { this.UserId = updatedServiceRequestOnt.UserId; updated = true; }
            if (this.Value != updatedServiceRequestOnt.Value) { this.Value = updatedServiceRequestOnt.Value; updated = true; }

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