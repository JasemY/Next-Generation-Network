using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Ia.Ngn.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Service support class for Next Generation Network (NGN) data model.
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
    public partial class Service2
    {
        public Service2() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Access ReadAccess(string id)
        {
            Ia.Ngn.Cl.Model.Access access;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                access = (from q in db.Service2s where q.Id == id select q.Access).SingleOrDefault();
            }

            return access;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<string, string> ServiceLineCardDictionary
        {
            get
            {
                Dictionary<string, string> dictionary;

                dictionary = new Dictionary<string, string>();

                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    dictionary = (from q in db.Service2s where q.LineCard != null select new { q.Service, q.LineCard }).ToDictionary(m => m.Service, m => m.LineCard);
                }

                return dictionary;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Access ReadAccess(Ia.Ngn.Cl.Model.Ngn db, string id)
        {
            Ia.Ngn.Cl.Model.Access access;

            access = (from q in db.Service2s where q.Id == id select q.Access).SingleOrDefault();

            return access;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateLineCard(Ia.Ngn.Cl.Model.Service2 service, string lineCard, out string result)
        {
            bool b;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                service = (from q in db.Service2s where q.Id == service.Id select q).SingleOrDefault();

                if (service.LineCard != lineCard)
                {
                    service.LineCard = lineCard;

                    db.Service2s.Attach(service);
                    db.Entry(service).Property(x => x.LineCard).IsModified = true;

                    db.SaveChanges();

                    result = "Success: Service LineCard updated. ";
                    b = true;
                }
                else
                {
                    result = "Warning: Service LineCard value was not updated because its the same. ";

                    b = false;
                }
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Service2> ServiceWithNullAccessList
        {
            get
            {
                List<Ia.Ngn.Cl.Model.Service2> serviceList;

                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    serviceList = (from s in db.Service2s where s.Access == null select s).ToList();
                }

                return serviceList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<string> ServiceServiceIdWithNullAccessTestList(string testServicePrefix)
        {
            List<string> list;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                list = (from s in db.Service2s where s.Service.StartsWith(testServicePrefix) && s.Access == null select s.Id).ToList();
            }

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}