using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Ia.Ngn.Cl.Model.AlcatelLucent
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Alcatel-Lucent Initial Installation Entity Framework class for Next Generation Network (NGN) entity model.
    /// </summary>
    /// 
    /// <remarks> 
    /// Copyright © 2014-2015 Jasem Y. Al-Shamlan (info@ia.com.kw), Internet Applications - Kuwait. All Rights Reserved.
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
    public partial class AlInitialInstallation
    {
        /// <summary/>
        public AlInitialInstallation() { }

        /// <summary/>
        public long Id { get; set; }

        public string PremisesSlPon { get; set; }
        public string OwnerName { get; set; }
        public string CivilId { get; set; }
        public string Plot { get; set; }
        public string Building { get; set; }
        public string Contact { get; set; }
        public string Block { get; set; }
        public string Pon { get; set; }
        public string Street { get; set; }
        public string BuildingType { get; set; }
        public string OntType { get; set; }
        public string OntSerial { get; set; }
        public string OntId { get; set; }
        public string Remark { get; set; }

        /// <summary/>
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Viewed { get; set; }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}