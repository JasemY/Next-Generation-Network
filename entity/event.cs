using System;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Reflection;

namespace Ia.Ngn.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Event Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class Event
    {
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Event() { }

        /// <summary/>
        public long Id { get; set; }
        /// <summary/>
        public int TypeId { get; set; }
        /// <summary/>
        public string System { get; set; }
        /// <summary/>
        public string Class { get; set; }
        /// <summary/>
        public string Aid { get; set; }
        /// <summary/>
        public string Cause { get; set; }
        /// <summary/>
        public int Number { get; set; }
        /// <summary/>
        public string Detail { get; set; }
        /// <summary/>
        public string Severity { get; set; }
        /// <summary/>
        public string SeverityEffect { get; set; }
        /// <summary/>
        public DateTime EventTime { get; set; }
        /// <summary/>
        public DateTime? NodeTime { get; set; }
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
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
