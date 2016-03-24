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
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Ia.Ngn.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Transaction Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class Transaction
    {
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Transaction() { }

        /// <summary/>
        public int Id { get; set; }

        /// <summary/>
        public int TypeId { get; set; }

        /// <summary/>
        public int StateId { get; set; }

        /// <summary/>
        public int PriorityId { get; set; }

        /// <summary/>
        public int RecipientId { get; set; }

        /// <summary/>
        public int NumberOfTimesMessageSent { get; set; }

        /// <summary/>
        public bool Closed { get; set; }

        /// <summary/>
        public string Message { get; set; }

        /// <summary/>
        public DateTime? MessageSent { get; set; }

        /// <summary/>
        public string Response { get; set; }

        /// <summary/>
        public DateTime? ResponseReceived { get; set; }

        /// <summary/>
        public DateTime Created { get; set; }

        /// <summary/>
        public DateTime Updated { get; set; }

        /// <summary/>
        public DateTime Inspected { get; set; }

        /// <summary/>
        public System.Guid UserId { get; set; }

        ////////////////////////////////////////////////////////////////////////////    
        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static bool Create(Transaction newItem, out int itemId)
        {
            bool b;

            b = false;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                newItem.Created = newItem.Updated = DateTime.UtcNow.AddHours(3);

                db.Transactions.Add(newItem);
                db.SaveChanges();

                itemId = newItem.Id;

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Transaction Read(int id)
        {
            Transaction item;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                item = (from q in db.Transactions where q.Id == id select q).SingleOrDefault();
            }

            return item;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Transaction> ReadList()
        {
            List<Transaction> itemList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                itemList = (from q in db.Transactions select q).ToList();
            }

            return itemList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(Transaction updatedItem, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                updatedItem = (from q in db.Transactions where q.Id == updatedItem.Id select q).SingleOrDefault();

                updatedItem.Updated = DateTime.UtcNow.AddHours(3);

                db.Transactions.Attach(updatedItem);

                var v = db.Entry(updatedItem);
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
        public bool Update(Transaction transactionOnt)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.Closed != transactionOnt.Closed) { this.Closed = transactionOnt.Closed; updated = true; }
            else if (this.Message != transactionOnt.Message) { this.Message = transactionOnt.Message; updated = true; }
            else if (this.MessageSent != transactionOnt.MessageSent) { this.MessageSent = transactionOnt.MessageSent; updated = true; }
            else if (this.NumberOfTimesMessageSent != transactionOnt.NumberOfTimesMessageSent) { this.NumberOfTimesMessageSent = transactionOnt.NumberOfTimesMessageSent; updated = true; }
            else if (this.PriorityId != transactionOnt.PriorityId) { this.PriorityId = transactionOnt.PriorityId; updated = true; }
            else if (this.RecipientId != transactionOnt.RecipientId) { this.RecipientId = transactionOnt.RecipientId; updated = true; }
            else if (this.Response != transactionOnt.Response) { this.Response = transactionOnt.Response; updated = true; }
            else if (this.ResponseReceived != transactionOnt.ResponseReceived) { this.ResponseReceived = transactionOnt.ResponseReceived; updated = true; }
            else if (this.StateId != transactionOnt.StateId) { this.StateId = transactionOnt.StateId; updated = true; }
            else if (this.TypeId != transactionOnt.TypeId) { this.TypeId = transactionOnt.TypeId; updated = true; }
            else if (this.UserId != transactionOnt.UserId) { this.UserId = transactionOnt.UserId; updated = true; }

            if (updated) this.Updated /*= this.Inspected*/ = DateTime.UtcNow.AddHours(3);

            return updated;
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
                var v = (from q in db.Transactions where q.Id == id select q).FirstOrDefault();

                db.Transactions.Remove(v);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
