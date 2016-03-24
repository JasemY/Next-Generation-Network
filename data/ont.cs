using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Data.Entity;
using System.Collections;

namespace Ia.Ngn.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// ONT support class for Next Generation Network (NGN) data model.
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
    public partial class Ont
    {
        /// <summary/>
        public Ont() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(Ia.Ngn.Cl.Model.Ont ont, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ont.Created = ont.Updated = ont.Inspected = DateTime.UtcNow.AddHours(3);

                db.Onts.Add(ont);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Ont Read(string id)
        {
            Ia.Ngn.Cl.Model.Ont ont;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ont = (from o in db.Onts where o.Id == id select o).Include(o => o.OntServiceVoips).SingleOrDefault();
            }

            return ont;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ont> ReadList()
        {
            List<Ia.Ngn.Cl.Model.Ont> ontList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ontList = (from q in db.Onts select q).ToList();
            }

            return ontList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(Ia.Ngn.Cl.Model.Ont ont, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ont = (from q in db.Onts where q.Id == ont.Id select q).SingleOrDefault();

                ont.Updated = DateTime.UtcNow.AddHours(3);

                db.Onts.Attach(ont);

                db.Entry(ont).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Delete(string id, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                var v = (from q in db.Onts where q.Id == id select q).FirstOrDefault();

                db.Onts.Remove(v);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string FamilyTypeFromId(int familyTypeId)
        {
            string s;
            Ia.Ngn.Cl.Model.Business.Ont.FamilyType familyType;

            familyType = (Ia.Ngn.Cl.Model.Business.Ont.FamilyType)familyTypeId;

            s = familyType.ToString().ToUpper();

            return s;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ont> ReadList(string serial)
        {
            List<Ia.Ngn.Cl.Model.Ont> list;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                list = (from q in db.Onts where q.Serial == serial select q).ToList();
            }

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ont> ReadList(int oltId)
        {
            List<Ia.Ngn.Cl.Model.Ont> list;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                list = (from q in db.Onts where q.Access.Olt == oltId select q).ToList();
            }

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ont> ReadListIncludeOntServiceVoipsAndAccess(int oltId)
        {
            List<Ia.Ngn.Cl.Model.Ont> list;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                list = (from q in db.Onts where q.Access.Olt == oltId select q).Include(x => x.Access).Include(x => x.OntServiceVoips).ToList();
            }

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ont> ReadListIncludeAccess()
        {
            List<Ia.Ngn.Cl.Model.Ont> ontList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ontList = (from q in db.Onts select q).Include(q => q.Access).ToList();
            }

            return ontList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ont> ReadListIncludeAccessAndOntOntPots()
        {
            List<Ia.Ngn.Cl.Model.Ont> ontList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ontList = (from q in db.Onts select q).Include(q => q.Access).Include(q => q.OntOntPotses).ToList();

                /*
                ontList = (from o in db.Onts 
                           join a in db.Accesses on o.Access equals a 
                           join oop in db.OntOntPotses on o equals oop.Ont select o).ToList();
                 */ 
            }

            return ontList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ont> ReadListIncludeAccessAndOntOntPots(int oltId)
        {
            List<Ia.Ngn.Cl.Model.Ont> ontList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ontList = (from q in db.Onts where q.Access.Olt == oltId select q).Include(q => q.Access).Include(q => q.OntOntPotses).ToList();
            }

            return ontList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ont> ReadListIncludeAccess(long oltId)
        {
            List<Ia.Ngn.Cl.Model.Ont> ontList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                ontList = (from q in db.Onts where q.Access.Olt == oltId select q).Include(q => q.Access).ToList();
            }

            return ontList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<string> ReadNddAccessNameListWithEquipmentIdNotNullAndAccessIsNullIncludeOntServiceVoips
        {
            get
            {
                string ip;
                Hashtable ht;
                List<string> ontNameList;
                List<Ia.Ngn.Cl.Model.Ont> list;

                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    list = (from o in db.Onts where o.EquipmentId != null && o.Access == null select o).Include(x => x.OntServiceVoips).ToList();
                }

                ontNameList = new List<string>(list.Count);

                ht = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntListIpToAccessNameHashtable;

                foreach(var v in list)
                {
                    if (v.OntServiceVoips != null)
                    {
                        if (v.OntServiceVoips.Count > 0)
                        {
                            ip = v.OntServiceVoips.FirstOrDefault().Ip;

                            if (ht.ContainsKey(ip)) ontNameList.Add(ht[ip].ToString());
                        }
                    }
                }

                return ontNameList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}