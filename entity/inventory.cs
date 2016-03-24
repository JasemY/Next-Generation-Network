using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Ia.Ngn.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Inventory Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class Inventory
    {
        /// <summary/>
        public Inventory() { }

        /// <summary/>
        public int Id { get; set; }

        /// <summary/>
        public string Name { get; set; }

        /// <summary/>
        public int Company { get; set; }

        /// <summary/>
        public string Description { get; set; }

        /// <summary/>
        public string BarCode { get; set; }

        /// <summary/>
        public int Quantity { get; set; }

        /// <summary/>
        public float Price { get; set; }

        /// <summary/>
        public DateTime Created { get; set; }

        /// <summary/>
        public DateTime Updated { get; set; }

        /// <summary/>
        public System.Guid UserId { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(Inventory newInventory, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                newInventory.Created = newInventory.Updated = DateTime.UtcNow.AddHours(3);

                db.Inventory.Add(newInventory);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Inventory Read(int id)
        {
            Inventory inventory;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                inventory = (from q in db.Inventory where q.Id == id select q).SingleOrDefault();
            }

            return inventory;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Inventory> ReadList()
        {
            List<Inventory> inventoryList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                inventoryList = (from q in db.Inventory select q).ToList();
            }

            return inventoryList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(Inventory updatedInventory, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                updatedInventory = (from q in db.Inventory where q.Id == updatedInventory.Id select q).SingleOrDefault();

                updatedInventory.Updated = DateTime.UtcNow.AddHours(3);

                db.Inventory.Attach(updatedInventory);

                var v = db.Entry(updatedInventory);
                v.State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                b = true;
            }

            return b;
        }

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

            //if (updated) this.Updated = this.Inspected = DateTime.UtcNow.AddHours(3);
            //else this.Inspected = DateTime.UtcNow.AddHours(3);

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool QuantityChange(int inventoryId, int increment, out string result)
        {
            bool b;
            Inventory updatedInventory;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                updatedInventory = (from q in db.Inventory where q.Id == inventoryId select q).SingleOrDefault();

                updatedInventory.Quantity += increment;

                if (updatedInventory.Quantity >= 0)
                {
                    // below: don't go below 0
                    updatedInventory.Updated = DateTime.UtcNow.AddHours(3);

                    db.Inventory.Attach(updatedInventory);

                    var v = db.Entry(updatedInventory);
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
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                var v = (from q in db.Inventory where q.Id == id select q).FirstOrDefault();

                if (v.Quantity == 0)
                {
                    db.Inventory.Remove(v);
                    db.SaveChanges();

                    b = true;
                }
                else
                {
                    result = "Item can't be deleted because quantity is not zero. ";

                    b = false;
                }
            }

            return b;
        }

        /*
<?xml version="1.0" encoding="utf-8"?>

<inventory>

  <!--
  Inventory System Requirements

  demand and
  replenishment lead time data

  forecasts

  reviewing the stock
  position;
  - preparing the purchase
  request;
  - selecting the supplier;
  receiving, inspecting, and
  placing the material in
  storage; and
  - paying the vendor.

  Does the system estimate and
  routinely update the per unit
  inventory holding cost, which
  is an estimate of the cost to
  hold each additional unit of
  inventory? Its primary
  elements are storage space,
  obsolescence, interest on
  inventory investment, and
  inventory shrinkage (due to
  deterioration, theft, damage,
  etc.).
  4.


  Does the system recompute
  the Economic Order Quantity
  (EOQ) on a regular, frequent
  schedule, using the demand
  forecast, ordering cost,
  inventory holding cost, and
  unit cost of the material? In
  lieu of the EOQ, any other
  optimum order quantity
  calculation may be used,
  provided (a) it is based on
  sound business principles and
  (b) it minimizes total cost,
  including the sum of ordering
  and inventory holding costs


  Does the system recompute
  the safety stock, if any, on a
  regular and frequent schedule


  Does the system recompute
  the reorder point level on a
  regular and frequent schedule


  Does the system determine if
  replenishment is needed on a
  regular and frequent schedule,
  basing the determination on
  net stock and reorder point? If
  needed, immediately initiate a
  replenishment action using the
  EOQ or other order quantity,

  Does the system provide
  information on current
  inventories and historical
  usage to be used in capacity
  planning?

  Does the system provide to
  agency inventory managers
  and designated internal review
  officials, on a periodic or
  requested basis, at least the
  following types of
  management information:
  - demand?
  - procurement lead time?
  - procurement cycle?
  - requirements?
  - assets?
  - available funds?
  - budget versus actual?
  - rates of fund utilization?

  Does the system record
  information on material
  returned by customers?

  Does the system provide
  support for physical
  verification of inventory
  balances by location and type?

  Does the system record
  changes in physical condition
  (e.g., excellent, good, fair, or
  poor), quantities, etc., based
  on the results of physical
  inventory verifications?

  =====================================

  Inventory System supports updating inventory information for all items, monitoring inventory depletion, and importing and exporting
  inventory information to and from external systems of record

  =====================================

  Flexible configuration that allows for backordering and display of out of stock stock-keeping units (SKUs).

  Order checking and update through pipeline components.

  Full text search and query integration with the Catalog System.

  Transactional updates.

  Import and export operations similar to the Catalog System.

  Runtime APIs with methods for searching, browsing, viewing details, inventory search options that include product filtering, assigning inventory conditions, and roll-up values.
  
  
  
  Inventory Disposition consists of the following processes: (a) loaning
process, (b) issuing process, and (c) disposal process.


inventory shrinkage (due to
deterioration, theft, damage,
etc.).
4.

Economic Order Quantity
(EOQ) on


  <inventory>
    <transaction>
      <type id="1" name="Purchased" name_ar=""/>
      <type id="2" name="Sold" name_ar=""/>
      <type id="3" name="On Hold" name_ar=""/>
      <type id="4" name="Waste" name_ar=""/>
      <type id="0" name="Unspecified" name_ar="غير معرف"/>
    </transaction>
  </inventory>

  -->

  <type id="1" name="ONT" description=""/>
  <type id="2" name="Computer" description=""/>
  <type id="3" name="Board" description=""/>
  <type id="4" name="Card" description=""/>
  <type id="5" name="Switch" description=""/>
  <type id="6" name="Cable" description=""/>
  <type id="7" name="RJ" description=""/>
  <type id="8" name="Screen" description=""/>
  <type id="9" name="Keyboard" description=""/>
  <type id="10" name="Mouse" description=""/>
  <type id="11" name="Phone" description=""/>

  <item id="1" type_id="1" name="SFU" code="" discontinued="" description=""/>

</inventory>
         */

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}