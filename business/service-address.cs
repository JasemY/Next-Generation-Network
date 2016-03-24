using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Ia.Ngn.Cl.Model.Business
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// ServiceAddress Framework class for Next Generation Network (NGN) business model.
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
    public partial class ServiceAddress
    {
        /// <summary/>
        public ServiceAddress() { }

        /// <summary/>
        public string Service { get; set; }
        /// <summary/>
        public int AreaId { get; set; }
        /// <summary/>
        public int Block { get; set; }
        /// <summary/>
        public string Street { get; set; }
        /// <summary/>
        public string Boulevard { get; set; }
        /// <summary/>
        public string PremisesOld { get; set; }
        /// <summary/>
        public string PremisesNew { get; set; }

        /// <summary/>
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
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}