using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Ia.Ngn.Cl.Model.Ui
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Inventory For DataGridView support class for Next Generation Network (NGN) ui model.
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
    public partial class InventoryForDataGridView
    {
        /// <summary/>
        public int Id { get; set; }

        /// <summary/>
        public string Name { get; set; }

        /// <summary/>
        public string CompanyName { get; set; }

        /// <summary/>
        public string Description { get; set; }

        /// <summary/>
        public string BarCode { get; set; }

        /// <summary/>
        public int Quantity { get; set; }

        /// <summary/>
        public DateTime Created { get; set; }

        /// <summary/>
        public DateTime Updated { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ui.InventoryForDataGridView> ReadUiList()
        {
            List<Ia.Ngn.Cl.Model.Ui.InventoryForDataGridView> inventoryForDataGridView;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                inventoryForDataGridView = (from q in db.Inventory select q).AsEnumerable().Select(q => new InventoryForDataGridView
                                            {
                                                Id = q.Id,
                                                Name = q.Name,
                                                CompanyName = Ia.Ngn.Cl.Model.Business.Default.VendorNameFromId(q.Company),
                                                Description = q.Description,
                                                BarCode = q.BarCode,
                                                Quantity = q.Quantity,
                                                Created = q.Created,
                                                Updated = q.Updated
                                            }).ToList();
            }

            return inventoryForDataGridView;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}