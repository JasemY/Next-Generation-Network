using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Access support class for Next Generation Network (NGN) data model.
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
        public static List<int> OntsNotInAccessList(List<Ia.Ngn.Cl.Model.Access> accessList)
        {
            // below: produce a list of ONTs between 1 and 32 that are not in ontList
            List<int> ontNotInListArrayList = new List<int>(32);

            for (int i = 1; i <= 32; i++)
            {
                if ((from q in accessList where q.Ont == i select q).SingleOrDefault() == null)
                {
                    ontNotInListArrayList.Add(i);
                }
                else
                {
                }
            }

            return ontNotInListArrayList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Access ReadByAccessName(string accessName)
        {
            int oltId, pon, ont;
            Ia.Ngn.Cl.Model.Access access;

            // below: this expects ontName in exact format like SUR.12.3

            ExtractOltIdAndPonNumberAndOntNumberFromOntName(accessName, out oltId, out pon, out ont);

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                access = (from q in db.Accesses where q.Olt == oltId && q.Pon == pon && q.Ont == ont select q).SingleOrDefault();
            }

            return access;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Access> ReadListUsingOntFamilyType(Ia.Ngn.Cl.Model.Business.Ont.FamilyType ontFamilyType)
        {
            // below: return access list for family type
            List<Ia.Ngn.Cl.Model.Access> list;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                list = (from a in db.Accesses join o in db.Onts on a equals o.Access where o.FamilyTypeId == (int)ontFamilyType select a).ToList();
            }

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<string> ReadAccessNameListUsingKuwaitNgnAreaIdAndBlock(int kuwaitAreaId, int block)
        {
            Hashtable ht;
            List<string> accessIdList, accessNameList;

            if(kuwaitAreaId > 0 && block > 0)
            {
                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    accessIdList = (from a in db.Accesses where a.AreaId == kuwaitAreaId && a.Block == block select a.Id).ToList();
                    ht = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntListAccessIdToAccessNameHashtable;

                    if (accessIdList != null && accessIdList.Count > 0)
                    {
                        accessNameList = new List<string>(accessIdList.Count);

                        foreach (string accessId in accessIdList)
                        {
                            if (ht.ContainsKey(accessId)) accessNameList.Add(ht[accessId].ToString());
                        }
                    }
                    else
                    {
                        accessNameList = null;
                    }
                }                
            }
            else
            {
                accessNameList = null;
            }

            return accessNameList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<string, string> DistinctAccessKuwaitNgnAreaIdAndBlock
        {
            get
            {
                int kuwaitNgnAreaId, block;
                string kuwaitNgnAreaIdString, blockString, kuwaitNgnAreaBlockValue, kuwaitNgnAreaNameArabicName;
                Dictionary<string, string> tempDictionary;
                Dictionary<string, string> dictionary;

                dictionary = new Dictionary<string, string>();
                tempDictionary = new Dictionary<string, string>();

                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    tempDictionary = (from a in db.Accesses select new { a.AreaId, a.Block }).Distinct().OrderBy(u => u.AreaId).ThenBy(u => u.Block).ToDictionary(m => m.AreaId + "," + m.Block, m => m.AreaId + "," + m.Block);

                    if (tempDictionary != null && tempDictionary.Count > 0)
                    {
                        foreach(KeyValuePair<string,string> kvp in tempDictionary)
                        {
                            kuwaitNgnAreaIdString = kvp.Key.Split(',')[0].ToString();
                            blockString = kvp.Key.Split(',')[1].ToString();

                            if (int.TryParse(kuwaitNgnAreaIdString , out kuwaitNgnAreaId) && int.TryParse(blockString, out block))
                            {
                                kuwaitNgnAreaNameArabicName = (from kna in Ia.Ngn.Cl.Model.Data.Service.KuwaitNgnAreaList where kna.Id == kuwaitNgnAreaId select kna.NameArabicName).SingleOrDefault();

                                kuwaitNgnAreaBlockValue = kuwaitNgnAreaNameArabicName + ", block " + block;

                                dictionary.Add(kvp.Key, kuwaitNgnAreaBlockValue);
                            }
                            else
                            {
                            }
                        }
                    }
                    else
                    {
                    }
                }

                return dictionary;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ui.Maintenance.AccessFamilyTypeAreaBlock> FamilyStatisticInKuwaitNgnAreaAndBlockTable
        {
            // FamilyType { Undefined = 0, Sfu = 1, Soho = 2, Mdu = 3 };

            get
            {
                Ia.Ngn.Cl.Model.Ui.Maintenance.AccessFamilyTypeAreaBlock item;
                List<Ia.Ngn.Cl.Model.Ui.Maintenance.AccessFamilyTypeAreaBlock> list, sfu, soho, mdu;

                list = new List<Ia.Ngn.Cl.Model.Ui.Maintenance.AccessFamilyTypeAreaBlock>();

                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    sfu = (from a in db.Accesses join o in db.Onts on a.Id equals o.Access.Id where o.FamilyTypeId == 1 group a by new { a.AreaId, a.Block } into g select new Ia.Ngn.Cl.Model.Ui.Maintenance.AccessFamilyTypeAreaBlock() { AreaId = g.Key.AreaId, Block = g.Key.Block, Sfu = g.Count() }).ToList();
                    soho = (from a in db.Accesses join o in db.Onts on a.Id equals o.Access.Id where o.FamilyTypeId == 2 group a by new { a.AreaId, a.Block } into g select new Ia.Ngn.Cl.Model.Ui.Maintenance.AccessFamilyTypeAreaBlock() { AreaId = g.Key.AreaId, Block = g.Key.Block, Soho = g.Count() }).ToList();
                    mdu = (from a in db.Accesses join o in db.Onts on a.Id equals o.Access.Id where o.FamilyTypeId == 3 group a by new { a.AreaId, a.Block } into g select new Ia.Ngn.Cl.Model.Ui.Maintenance.AccessFamilyTypeAreaBlock() { AreaId = g.Key.AreaId, Block = g.Key.Block, Mdu = g.Count() }).ToList();

                    foreach (Ia.Ngn.Cl.Model.Ui.Maintenance.AccessFamilyTypeAreaBlock a in sfu) list.Add(new Ia.Ngn.Cl.Model.Ui.Maintenance.AccessFamilyTypeAreaBlock { AreaId = a.AreaId,Block = a.Block, Sfu = a.Sfu });

                    foreach (Ia.Ngn.Cl.Model.Ui.Maintenance.AccessFamilyTypeAreaBlock a in soho)
                    {
                        item = (from b in list where b.AreaId == a.AreaId && b.Block == a.Block select b).SingleOrDefault();

                        if(item != null) item.Soho = a.Soho;
                        else list.Add(new Ia.Ngn.Cl.Model.Ui.Maintenance.AccessFamilyTypeAreaBlock { AreaId = a.AreaId, Block = a.Block, Soho = a.Soho });
                    }

                    foreach (Ia.Ngn.Cl.Model.Ui.Maintenance.AccessFamilyTypeAreaBlock a in mdu)
                    {
                        item = (from b in list where b.AreaId == a.AreaId && b.Block == a.Block select b).SingleOrDefault();

                        if (item != null) item.Mdu = a.Mdu;
                        else list.Add(new Ia.Ngn.Cl.Model.Ui.Maintenance.AccessFamilyTypeAreaBlock { AreaId = a.AreaId, Block = a.Block, Mdu = a.Mdu });
                    }

                    list = list.OrderBy(u => u.AreaId).ThenBy(u => u.Block).ToList();

                    return list;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Access> ReadListWithOntListProvisionedAndReadyButDoNotExistInCustomerDepartmentDatabase()
        {
            // below:
            List<Ia.Ngn.Cl.Model.Access> list;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                list = (from a in db.Accesses
                        join o in db.Onts on a equals o.Access
                        join osv in db.OntServiceVoips on o equals osv.Ont
                        join sro in db.ServiceRequestOnts on a equals sro.Access into gj
                        from subsro in gj.DefaultIfEmpty()
                        where o.StateId == (int)Ia.Ngn.Cl.Model.Business.Nokia.Ams.BellcoreState.IsNr && o.Serial != null && o.Serial != "ALCL00000000" && o.ActiveSoftware != null && o.ActiveSoftware == o.PlannedSoftware && osv.Ip != null && subsro.Access.Id == null
                        select a).Include(c => c.Onts).ToList();
            }

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void ExtractOltIdAndPonNumberAndOntNumberFromOntName(string ontName, out int oltId, out int pon, out int ont)
        {
            // below: this expects ontName in exact format like SUR.12.3
            int ponNumber;
            string oltSymbol;
            string[] sp;

            sp = ontName.Split('.');

            oltSymbol = sp[0];
            pon = ponNumber = int.Parse(sp[1]);
            ont = int.Parse(sp[2]);

            oltId = (from Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Pon p in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.PonList where p.Olt.Symbol == oltSymbol && p.Number == ponNumber select p.Olt.Id).SingleOrDefault();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Access ReadByIp(string ip)
        {
            // below: return the Access object of this ip

            Ia.Ngn.Cl.Model.Access item;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                item = (from a in db.Accesses join o in db.Onts on a equals o.Access join ov in db.OntServiceVoips on o equals ov.Ont where ov.Ip == ip select a).SingleOrDefault();
            }

            return item;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Access ReadByIp(Ia.Ngn.Cl.Model.Ngn db, string ip)
        {
            // below: return the Access object of this ip

            return (from a in db.Accesses join o in db.Onts on a equals o.Access join ov in db.OntServiceVoips on o equals ov.Ont where ov.Ip == ip select a).SingleOrDefault();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Access StatisticalAccess(Ia.Ngn.Cl.Model.Business.ServiceAddress serviceAddress, ref List<Ia.Ngn.Cl.Model.Access> accessList)
        {
            // below:

            Ia.Ngn.Cl.Model.Access statisticalAccess;

            statisticalAccess = (from a in accessList
                                 where a.AreaId == serviceAddress.AreaId
                                     && a.Block == serviceAddress.Block
                                     && a.Street == serviceAddress.Street
                                     && a.PremisesOld == serviceAddress.PremisesOld
                                     && a.PremisesNew == serviceAddress.PremisesNew
                                 select a).FirstOrDefault();

            if (statisticalAccess == null)
            {
                statisticalAccess = (from a in accessList
                                     where a.AreaId == serviceAddress.AreaId
                                         && a.Block == serviceAddress.Block
                                         && a.Street == serviceAddress.Street
                                         && a.PremisesOld == serviceAddress.PremisesOld
                                     select a).FirstOrDefault();

                if (statisticalAccess == null)
                {
                    statisticalAccess = (from a in accessList
                                         where a.AreaId == serviceAddress.AreaId
                                             && a.Block == serviceAddress.Block
                                             && a.Street == serviceAddress.Street
                                             && a.PremisesNew == serviceAddress.PremisesNew
                                         select a).FirstOrDefault();

                    if (statisticalAccess == null)
                    {
                        statisticalAccess = (from a in accessList
                                             where a.AreaId == serviceAddress.AreaId
                                                 && a.Block == serviceAddress.Block
                                                 && a.Street == serviceAddress.Street
                                                 && a.PremisesOld == serviceAddress.PremisesNew
                                             select a).FirstOrDefault();

                        if (statisticalAccess == null)
                        {
                            statisticalAccess = (from a in accessList
                                                 where a.AreaId == serviceAddress.AreaId
                                                     && a.Block == serviceAddress.Block
                                                     && a.Street == serviceAddress.Street
                                                     && a.PremisesNew == serviceAddress.PremisesOld
                                                 select a).FirstOrDefault();

                            if (statisticalAccess == null)
                            {
                                statisticalAccess = (from a in accessList
                                                     where a.AreaId == serviceAddress.AreaId
                                                         && a.Block == serviceAddress.Block
                                                         && a.Street == serviceAddress.Street
                                                         && a.PremisesNew == serviceAddress.Boulevard
                                                     select a).FirstOrDefault();

                                if (statisticalAccess == null)
                                {
                                    statisticalAccess = (from a in accessList
                                                         where a.AreaId == serviceAddress.AreaId
                                                             && a.Block == serviceAddress.Block
                                                             && a.Street == serviceAddress.Street
                                                             && a.PremisesOld == serviceAddress.Boulevard
                                                         select a).FirstOrDefault();

                                    if (statisticalAccess == null)
                                    {
                                        statisticalAccess = (from a in accessList
                                                             where a.AreaId == serviceAddress.AreaId
                                                                 && a.Block == serviceAddress.Block
                                                                 && a.PremisesOld == serviceAddress.PremisesOld
                                                             select a).FirstOrDefault();

                                        if (statisticalAccess == null)
                                        {
                                            statisticalAccess = (from a in accessList
                                                                 where a.AreaId == serviceAddress.AreaId
                                                                     && a.Block == serviceAddress.Block
                                                                     && a.PremisesNew == serviceAddress.PremisesNew
                                                                 select a).FirstOrDefault();

                                            if (statisticalAccess == null)
                                            {
                                                statisticalAccess = (from a in accessList
                                                                     where a.AreaId == serviceAddress.AreaId
                                                                         && a.Block == serviceAddress.Block
                                                                         && a.PremisesOld == serviceAddress.PremisesNew
                                                                     select a).FirstOrDefault();

                                                if (statisticalAccess == null)
                                                {
                                                    statisticalAccess = (from a in accessList
                                                                         where a.AreaId == serviceAddress.AreaId
                                                                             && a.Block == serviceAddress.Block
                                                                             && a.PremisesNew == serviceAddress.PremisesOld
                                                                         select a).FirstOrDefault();

                                                    if (statisticalAccess == null)
                                                    {
                                                        statisticalAccess = (from a in accessList
                                                                             where a.AreaId == serviceAddress.AreaId
                                                                                 && a.Block == serviceAddress.Block
                                                                                 && a.Street == serviceAddress.Street
                                                                                 && serviceAddress.PremisesOld != null && a.PremisesOld.Contains(serviceAddress.PremisesOld)
                                                                                 && a.PremisesNew == serviceAddress.PremisesNew
                                                                             select a).FirstOrDefault();

                                                        if (statisticalAccess == null)
                                                        {
                                                            statisticalAccess = (from a in accessList
                                                                                 where a.AreaId == serviceAddress.AreaId
                                                                                     && a.Block == serviceAddress.Block
                                                                                     && a.Street == serviceAddress.Street
                                                                                     && a.PremisesOld == serviceAddress.PremisesOld
                                                                                     && serviceAddress.PremisesNew != null && a.PremisesNew.Contains(serviceAddress.PremisesNew)
                                                                                 select a).FirstOrDefault();

                                                            if (statisticalAccess == null)
                                                            {
                                                                statisticalAccess = (from a in accessList
                                                                                     where a.AreaId == serviceAddress.AreaId
                                                                                         && a.Block == serviceAddress.Block
                                                                                         && a.Street == serviceAddress.Street
                                                                                         && serviceAddress.PremisesOld != null && a.PremisesOld.Contains(serviceAddress.PremisesOld)
                                                                                     select a).FirstOrDefault();

                                                                if (statisticalAccess == null)
                                                                {
                                                                    statisticalAccess = (from a in accessList
                                                                                         where a.AreaId == serviceAddress.AreaId
                                                                                             && a.Block == serviceAddress.Block
                                                                                             && a.Street == serviceAddress.Street
                                                                                             && serviceAddress.PremisesNew != null && a.PremisesNew.Contains(serviceAddress.PremisesNew)
                                                                                         select a).FirstOrDefault();

                                                                    if (statisticalAccess == null)
                                                                    {
                                                                        statisticalAccess = (from a in accessList
                                                                                             where a.AreaId == serviceAddress.AreaId
                                                                                                 && a.Block == serviceAddress.Block
                                                                                                 && serviceAddress.PremisesOld != null && a.PremisesOld.Contains(serviceAddress.PremisesOld)
                                                                                                 && a.PremisesNew == serviceAddress.PremisesNew
                                                                                             select a).FirstOrDefault();

                                                                        if (statisticalAccess == null)
                                                                        {
                                                                            statisticalAccess = (from a in accessList
                                                                                                 where a.AreaId == serviceAddress.AreaId
                                                                                                     && a.Block == serviceAddress.Block
                                                                                                     && a.PremisesOld == serviceAddress.PremisesOld
                                                                                                     && serviceAddress.PremisesNew != null && a.PremisesNew.Contains(serviceAddress.PremisesNew)
                                                                                                 select a).FirstOrDefault();

                                                                            if (statisticalAccess == null)
                                                                            {
                                                                                statisticalAccess = (from a in accessList
                                                                                                     where a.AreaId == serviceAddress.AreaId
                                                                                                         && a.Block == serviceAddress.Block
                                                                                                         && serviceAddress.PremisesOld != null && a.PremisesOld.Contains(serviceAddress.PremisesOld)
                                                                                                     select a).FirstOrDefault();

                                                                                if (statisticalAccess == null)
                                                                                {
                                                                                    statisticalAccess = (from a in accessList
                                                                                                         where a.AreaId == serviceAddress.AreaId
                                                                                                             && a.Block == serviceAddress.Block
                                                                                                             && serviceAddress.PremisesNew != null && a.PremisesNew.Contains(serviceAddress.PremisesNew)
                                                                                                         select a).FirstOrDefault();
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return statisticalAccess;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}