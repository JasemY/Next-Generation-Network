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
    /// Service Request Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class ServiceRequest
    {
        /// <summary/>
        public ServiceRequest() { }

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        /// <summary/>
        public int Number { get; set; }
        /// <summary/>
        public int Serial { get; set; }
        /// <summary/>
        public int Status { get; set; }
        /// <summary/>
        public DateTime RequestDateTime { get; set; }
        /// <summary/>
        public int CustomerId { get; set; }
        /// <summary/>
        public string CustomerName { get; set; }
        /// <summary/>
        public int CustomerCategoryId { get; set; }
        /// <summary/>
        public string CustomerAddress { get; set; }
        /// <summary/>
        public int AreaId { get; set; }
        /// <summary/>
        public int ServiceId { get; set; }
        /// <summary/>
        public int ServiceCategoryId { get; set; }
        /// <summary/>
        public double Balance { get; set; }
        /// <summary/>
        public DateTime Created { get; set; }
        /// <summary/>
        public DateTime Updated { get; set; }
        /// <summary/>
        public DateTime Inspected { get; set; }
        /// <summary/>
        public System.Guid UserId { get; set; }

        /// <summary/>
        public virtual ServiceRequestService ServiceRequestService { get; set; }
        /// <summary/>
        public virtual ICollection<ServiceRequestType> ServiceRequestTypes { get; set; }
        /// <summary/>
        public virtual ICollection<ServiceRequestOnt> ServiceRequestOnts { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool UpdateSkipServiceRequestService(ServiceRequest updatedServiceRequest)
        {
            return Update(updatedServiceRequest, true);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Update(ServiceRequest updatedServiceRequest)
        {
            return Update(updatedServiceRequest, false);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private bool Update(ServiceRequest updatedServiceRequest, bool skipServiceRequestService)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.CustomerAddress != updatedServiceRequest.CustomerAddress) { this.CustomerAddress = updatedServiceRequest.CustomerAddress; updated = true; }
            if (this.AreaId != updatedServiceRequest.AreaId) { this.AreaId = updatedServiceRequest.AreaId; updated = true; }
            if (this.CustomerCategoryId != updatedServiceRequest.CustomerCategoryId) { this.CustomerCategoryId = updatedServiceRequest.CustomerCategoryId; updated = true; }
            if (this.CustomerId != updatedServiceRequest.CustomerId) { this.CustomerId = updatedServiceRequest.CustomerId; updated = true; }
            if (this.CustomerName != updatedServiceRequest.CustomerName) { this.CustomerName = updatedServiceRequest.CustomerName; updated = true; }
            if (this.Number != updatedServiceRequest.Number) { this.Number = updatedServiceRequest.Number; updated = true; }
            if (this.RequestDateTime != updatedServiceRequest.RequestDateTime) { this.RequestDateTime = updatedServiceRequest.RequestDateTime; updated = true; }
            if (this.Serial != updatedServiceRequest.Serial) { this.Serial = updatedServiceRequest.Serial; updated = true; }
            if (this.ServiceCategoryId != updatedServiceRequest.ServiceCategoryId) { this.ServiceCategoryId = updatedServiceRequest.ServiceCategoryId; updated = true; }
            if (this.ServiceId != updatedServiceRequest.ServiceId) { this.ServiceId = updatedServiceRequest.ServiceId; updated = true; }

            if (!skipServiceRequestService) if (this.ServiceRequestService != updatedServiceRequest.ServiceRequestService) { this.ServiceRequestService = updatedServiceRequest.ServiceRequestService; updated = true; }
            //if (this.ServiceRequestOnts != updatedServiceRequest.ServiceRequestOnts) { this.ServiceRequestOnts = updatedServiceRequest.ServiceRequestOnts; updated = true; }
            //if (this.ServiceRequestTypes != updatedServiceRequest.ServiceRequestTypes) { this.ServiceRequestTypes = updatedServiceRequest.ServiceRequestTypes; updated = true; }

            if (this.Status != updatedServiceRequest.Status) { this.Status = updatedServiceRequest.Status; updated = true; }
            if (this.Balance != updatedServiceRequest.Balance) { this.Balance = updatedServiceRequest.Balance; updated = true; }

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