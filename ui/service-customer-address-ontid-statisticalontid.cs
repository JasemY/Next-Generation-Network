using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Ia.Ngn.Cl.Model.Ui
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// ServiceRequest ServiceRequestService Access Statistic support class for Next Generation Network (NGN) ui model.
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
    public partial class ServiceCustomerAddressAccessOntNameStatisticalOntName
    {
        /// <summary/>
        public ServiceCustomerAddressAccessOntNameStatisticalOntName() { }
        /// <summary/>
        public string Service { get; set; }
        /// <summary/>
        public string CustomerAddress { get; set; }
        /// <summary/>
        public string KuwaitNgnAreaName { get; set; }
        /// <summary/>
        public int Block { get; set; }
        /// <summary/>
        public string Street { get; set; }
        /// <summary/>
        public string PremisesOld { get; set; }
        /// <summary/>
        public string PremisesNew { get; set; }
        /// <summary/>
        public Ia.Ngn.Cl.Model.Access Access { get; set; }
        /// <summary/>
        public string OntName { get; set; }
        /// <summary/>
        public string OntAddress { get; set; }
        /// <summary/>
        public string StatisticalOntName { get; set; }
        /// <summary/>
        public string StatisticalAddress { get; set; }
        /// <summary/>
        public string Note { get; set; }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}