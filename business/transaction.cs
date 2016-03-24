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
using System.Threading;
using System.Linq;

namespace Ia.Ngn.Cl.Model.Business
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Transction support class of Next Generation Network'a (NGN's) business model.
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
    public partial class Transction
    {
        /*
        private static int currentItemId;
        private static StringBuilder log = new StringBuilder(100000);

        /// <summary/>
        public enum ProcessState
        {
            Initiated = 1, NoPendingTransaction = 2
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int Inbox(Transaction.Recipient recipientId, Transaction.Priority priorityId, string message)
        {
            bool b;
            int itemId;
            Transaction newItem;

            newItem = new Transaction();

            newItem.Message = message;
            newItem.RecipientId = (int)recipientId;
            newItem.PriorityId = (int)priorityId;
            newItem.RecipientId = (int)recipientId;
            newItem.RecipientId = (int)recipientId;

            newItem.Created = newItem.Updated = newItem.Inspected = DateTime.UtcNow.AddHours(3);

            b = Transaction.Create(newItem, out itemId);

            currentItemId = itemId;

            return itemId;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string Outbox(int itemId) 
        {
            string response;
            Transaction transaction;

            response = null;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below:
                transaction = (from q in db.Transactions where q.Id == itemId select q).FirstOrDefault();

                if (transaction != null)
                {
                    // below:
                    response = transaction.Response;
                    transaction.Inspected = DateTime.UtcNow.AddHours(3);
                }
                else
                {
                }

                db.SaveChanges();
            }

            return response;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ProcessState Process(ref Dart.PowerTCP.Telnet.Telnet telnet, Ia.Ngn.Cl.Model.Transaction.Recipient recepientId)
        {
            bool messageSent;
            ProcessState processState;
            string s;

            // below:

            processState = ProcessState.Initiated;

            messageSent = false;
            List<Transaction> itemList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below:
                itemList = (from q in db.Transactions where q.RecipientId == (int)recepientId && q.Closed == false && q.NumberOfTimesMessageSent == 0 select q).ToList();

                if (itemList.Count > 0)
                {
                    foreach (Transaction transaction in itemList)
                    {
                        // below: DEBUG
                        if (/*true ||* / telnet.Connected)
                        {
                            try
                            {
                                foreach (char c in transaction.Message.ToCharArray())
                                {
                                    s = c.ToString();

                                    // below: DEBUG
                                    telnet.Send(s);

                                    // below: log
                                    log.Append(s);

                                    Thread.Sleep(20); // 20 milliseconds for bytes
                                }

                                messageSent = true;
                            }
                            catch (Exception)
                            {
                                messageSent = false;
                            }
                            
                            if (messageSent)
                            {
                                // below:
                                transaction.NumberOfTimesMessageSent++;
                                transaction.MessageSent = transaction.Updated = transaction.Inspected = DateTime.UtcNow.AddHours(3);
                            }
                            else
                            {
                                transaction.MessageSent = null;
                            }
                        }
                        else
                        {
                            // below: no connection
                            transaction.StateId = (int)Ia.Ngn.Cl.Model.Transaction.State.NoConnection;
                            transaction.Updated = transaction.Inspected = DateTime.UtcNow.AddHours(3);
                        }
                    }
                }
                else
                {
                    processState = ProcessState.NoPendingTransaction;
                }

                db.SaveChanges();
            }

            return processState;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void ProcessReceive(string receivedData)
        {
            Transaction transaction;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below:
                transaction = (from q in db.Transactions where q.Id == currentItemId && q.Closed == false select q).FirstOrDefault();

                if (transaction != null)
                {
                    // below:
                    transaction.Response = Ia.Cl.Model.Telnet.FormatAndCleanData(receivedData);
                    transaction.ResponseReceived = transaction.Updated = transaction.Inspected = DateTime.UtcNow.AddHours(3);
                    transaction.StateId = (int)Transaction.State.Closed;
                    transaction.Closed = true;

                    db.SaveChanges();
                }
                else
                {
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static StringBuilder Log
        {
            get
            {
                return log;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void Second() 
        { 
        }
         */ 

        ////////////////////////////////////////////////////////////////////////////    
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
