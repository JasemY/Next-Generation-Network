using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model.Ui
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Access support class for Next Generation Network (NGN) ui model.
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
    public partial class Access
    {
        /// <summary/>
        public Access() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static DataTable ReadListWithOntListProvisionedAndReadyButDoNotExistInCustomerDepartmentDatabaseDataTable()
        {
            // below:
            string areaName;
            DataRow dr;
            DataTable dt;
            List<Ia.Ngn.Cl.Model.Access> accessList;

            dt = new DataTable("");
            dt.Columns.Add("Name");
            dt.Columns.Add("Area");
            dt.Columns.Add("Block");
            dt.Columns.Add("Street");
            dt.Columns.Add("PremisesOld");
            dt.Columns.Add("PremisesNew");

            accessList = Ia.Ngn.Cl.Model.Data.Access.ReadListWithOntListProvisionedAndReadyButDoNotExistInCustomerDepartmentDatabase();

            accessList = accessList.OrderByDescending(a => a.Updated).Take(20).ToList();

            foreach (Ia.Ngn.Cl.Model.Access access in accessList)
            {
                dr = dt.NewRow();
                dr["Name"] = access.Name;

                areaName = (from q in Ia.Ngn.Cl.Model.Data.Service.KuwaitNgnAreaList where q.Id == access.AreaId select q.ArabicName).SingleOrDefault();
                dr["Area"] = areaName;

                dr["Block"] = access.Block;
                dr["Street"] = access.Street;
                dr["PremisesOld"] = access.PremisesOld;
                dr["PremisesNew"] = access.PremisesNew;

                dt.Rows.Add(dr);
            }

            return dt;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}