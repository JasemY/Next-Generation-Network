using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Reflection;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Service Request Ont support class for Next Generation Network (NGN) data model.
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
    public partial class ServiceRequestOnt
    {
        /// <summary/>
        public ServiceRequestOnt() { }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static string OracleSqlCommandSelectLatestAddedOntRecordList()
        {
            int lastId;
            string sql;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                lastId = (from q in db.ServiceRequestOnts orderby q.Id descending select q.Id).FirstOrDefault();
            }

            sql = "select distinct DP_ID as Id, DP as Value from (select DP, DP_ID from FM_NET where (DP_ID > " + lastId + ") order by DP_ID asc) where (DP like '___/___/___' or DP like '___ ___ ___') and rownum <= 100";

            return sql;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void InsertList(DataTable dataTable, out string result)
        {
            int id, readItemCount, existingItemCount, insertedItemCount, updatedItemCount;
            Ia.Ngn.Cl.Model.ServiceRequestOnt serviceRequestOnt, newServiceRequestOnt;
            List<int> idList;

            readItemCount = existingItemCount = insertedItemCount = updatedItemCount = 0;
            result = "";
            idList = new List<int>();

            if (dataTable != null)
            {
                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    readItemCount = dataTable.Rows.Count;

                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            newServiceRequestOnt = new Ia.Ngn.Cl.Model.ServiceRequestOnt();

                            id = int.Parse(dataRow["Id"].ToString());

                            serviceRequestOnt = (from q in db.ServiceRequestOnts where q.Id == id select q).SingleOrDefault();

                            if (serviceRequestOnt == null)
                            {
                                newServiceRequestOnt.Id = id;
                                newServiceRequestOnt.Value = dataRow["Value"].ToString();

                                newServiceRequestOnt.UserId = Guid.Empty;
                                newServiceRequestOnt.Created = newServiceRequestOnt.Updated = newServiceRequestOnt.Inspected = DateTime.UtcNow.AddHours(3);

                                idList.Add(id);

                                db.ServiceRequestOnts.Add(newServiceRequestOnt);

                                insertedItemCount++;
                            }
                            //else throw new Exception(@"Non-null serviceRequestOnt found at Ia.Ngn.Cl.Model.Data.ServiceRequestOnt.Update()");
                        }

                        db.SaveChanges();

                        // below: I have to create the object first then assign a foreign key reference to it
                        foreach (int i in idList)
                        {
                            newServiceRequestOnt = new Ia.Ngn.Cl.Model.ServiceRequestOnt();

                            serviceRequestOnt = (from q in db.ServiceRequestOnts where q.Id == i select q).SingleOrDefault();

                            newServiceRequestOnt.Copy(serviceRequestOnt);

                            //newServiceRequestOnt.ServiceRequest = (from q in db.ServiceRequests where q.Id == serviceRequestId select q).SingleOrDefault();
                            newServiceRequestOnt.Access = Ia.Ngn.Cl.Model.Business.ServiceRequestType.ExtractAccess(db, serviceRequestOnt.Value);

                            if (serviceRequestOnt.Update(newServiceRequestOnt))
                            {
                                db.ServiceRequestOnts.Attach(serviceRequestOnt);
                                db.Entry(serviceRequestOnt).State = System.Data.Entity.EntityState.Modified;

                                updatedItemCount++;
                            }
                        }

                        db.SaveChanges();

                        result = "(" + readItemCount + "/" + existingItemCount + "/" + insertedItemCount + "," + updatedItemCount + ",na) "; //+r ;
                    }
                    else
                    {
                        result = "(" + readItemCount + "/?/?) ";
                    }
                }
            }
            else
            {
                result = "(dataTable == null/?/?) ";
            }
        }

        /*
        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public int Service_Request_Ont(long l, object o, out long next, out string result)
        {
            int op, top;
            int area, pon, ont;
            long id;
            string sql;
            string[] in_field, field;
            StringBuilder sb;
            Ia.Cs.Db.SqlServer.F[] field_rule;
            DataTable from_dt;
            DataRow dr;

            result = "";

            from_dt = null;
            op = 0;
            top = 100;
            next = 0;

            try
            {
                //oracle.Sql("ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY HH24:MI:SS'");
                from_dt = oracle.Select("SELECT DP, DP_ID AS id FROM (SELECT DP, DP_ID FROM FM_NET WHERE (DP_ID >= " + l + ") ORDER BY DP_ID ASC) WHERE (DP LIKE '___/___/___' OR DP LIKE '___ ___ ___') AND ROWNUM <= " + top);

                string[] in_field1 = { "id", "area", "pon", "ont", "", "" };
                string[] field1 = { "id", "area", "pon", "ont", "created", "updated" };
                sqlserver.F[] field_rule1 = { Ia.Cs.Db.SqlServer.F.In, Ia.Cs.Db.SqlServer.F.In, Ia.Cs.Db.SqlServer.F.In, Ia.Cs.Db.SqlServer.F.In, Ia.Cs.Db.SqlServer.F.Cr, Ia.Cs.Db.SqlServer.F.Up };

                in_field = in_field1;
                field = field1;
                field_rule = field_rule1;

                if (from_dt != null)
                {
                    // below: add a few more columns to from_dt
                    from_dt.Columns.Add(new DataColumn("area", System.Type.GetType("System.Int32")));
                    from_dt.Columns.Add(new DataColumn("pon", System.Type.GetType("System.Int32")));
                    from_dt.Columns.Add(new DataColumn("ont", System.Type.GetType("System.Int32")));

                    if (from_dt.Rows.Count > 0 /*|| dt.Rows.Count > 0* /)
                    {
                        sb = new StringBuilder(from_dt.Rows.Count * 10);

                        foreach (DataRow r in from_dt.Rows)
                        {
                            op = (((Ia.Ngn.Cs.Idea)o).Service_Request_Service_Dn_Hsi_Ont_Id(r["dp"].ToString(), out area, out pon, out ont));

                            if (op > 0)
                            {
                                id = Ia.Ngn.Cs.Application.Ont_Id_To_Id(r["dp"].ToString());

                                if (id > 0)
                                {
                                    r["area"] = area;
                                    r["pon"] = pon;
                                    r["ont"] = ont;
                                }
                            }
                        }

                        // below: remove records that have some ont_id value = 0 or null

                        for (int i = from_dt.Rows.Count - 1; i >= 0; i--)
                        {
                            dr = from_dt.Rows[i];

                            if (dr["area"] == null || dr["pon"].ToString() == null || dr["ont"].ToString() == null) dr.Delete();
                        }

                        from_dt.AcceptChanges();

                        // below: build SQL statement

                        foreach (DataRow r in from_dt.Rows)
                        {
                            sb.Append("sro.id=" + r["id"].ToString() + " OR ");

                            next = int.Parse(r["id"].ToString()) + 1;
                        }

                        sql = sb.ToString();

                        if (sql.Length > 0)
                        {
                            sql = sql.Remove(sql.Length - 4, 4);

                            op = sqlserver.Update(from_dt, "ia_service_request_ont", @"SELECT * FROM ia_service_request_ont AS sro WHERE (" + sql + ")", "id", in_field, field, field_rule, true, "all", out result);
                        }
                    }
                    else
                    {
                        result = "(0-0/0/0)";
                        op = 0;

                        next = 0;
                    }
                }
                else if (from_dt == null)
                {
                    result = "(0-0/null/?)";
                    op = 0;
                }
                else if (from_dt != null)
                {
                    result = "(0-0/?/" + from_dt.Rows.Count + ")";
                    op = 0;
                }
            }
            catch (Exception ex)
            {
                result += "Error: occured during process: " + ex.ToString();
                op = -1;
            }

            return op;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public int Service_Request_Ont_Lateset_Addition(object o, out string result)
        {
            // below: 
            int op, top;
            int area, pon, ont;
            long max_id, id;
            string sql;
            string[] in_field, field;
            StringBuilder sb;
            Ia.Cs.Db.SqlServer.F[] field_rule;
            DataTable from_dt;
            DataRow dr;

            result = "";

            from_dt = null;
            op = 0;
            top = 100;

            try
            {
                max_id = long.Parse(sqlserver.Scalar("SELECT MAX(sro.id) AS max_id FROM ia_service_request_ont AS sro"));

                if (max_id > 0)
                {
                    from_dt = oracle.Select("SELECT DP, DP_ID AS id FROM (SELECT DP, DP_ID FROM FM_NET WHERE (DP_ID > " + max_id + ") ORDER BY DP_ID ASC) WHERE (DP LIKE '___/___/___' OR DP LIKE '___ ___ ___') AND ROWNUM <= " + top);

                    string[] in_field1 = { "id", "area", "pon", "ont", "", "" };
                    string[] field1 = { "id", "area", "pon", "ont", "created", "updated" };
                    sqlserver.F[] field_rule1 = { sqlserver.F.In, sqlserver.F.In, sqlserver.F.In, sqlserver.F.In, sqlserver.F.Cr, sqlserver.F.Up };

                    in_field = in_field1;
                    field = field1;
                    field_rule = field_rule1;

                    if (from_dt != null)
                    {
                        // below: add a few more columns to from_dt
                        from_dt.Columns.Add(new DataColumn("area", System.Type.GetType("System.Int32")));
                        from_dt.Columns.Add(new DataColumn("pon", System.Type.GetType("System.Int32")));
                        from_dt.Columns.Add(new DataColumn("ont", System.Type.GetType("System.Int32")));

                        if (from_dt.Rows.Count > 0 /*|| dt.Rows.Count > 0* /)
                        {
                            sb = new StringBuilder(from_dt.Rows.Count * 10);

                            foreach (DataRow r in from_dt.Rows)
                            {
                                op = (((Ia.Ngn.Cs.Idea)o).Service_Request_Service_Dn_Hsi_Ont_Id(r["dp"].ToString(), out area, out pon, out ont));

                                if (op > 0)
                                {
                                    id = Ia.Ngn.Cs.Application.Ont_Id_To_Id(r["dp"].ToString());

                                    if (id > 0)
                                    {
                                        r["area"] = area;
                                        r["pon"] = pon;
                                        r["ont"] = ont;
                                    }
                                }
                            }

                            // below: remove records that have some ont_id value = 0 or null

                            for (int i = from_dt.Rows.Count - 1; i >= 0; i--)
                            {
                                dr = from_dt.Rows[i];

                                if (dr["area"] == null || dr["pon"].ToString() == null || dr["ont"].ToString() == null) dr.Delete();
                            }

                            from_dt.AcceptChanges();

                            // below: build SQL statement

                            foreach (DataRow r in from_dt.Rows) sb.Append("sro.id=" + r["id"].ToString() + " OR ");

                            sql = sb.ToString();

                            if (sql.Length > 0)
                            {
                                sql = sql.Remove(sql.Length - 4, 4);

                                op = sqlserver.Update(from_dt, "ia_service_request_ont", @"SELECT * FROM ia_service_request_ont AS sro WHERE (" + sql + ")", "id", in_field, field, field_rule, true, "all", out result);
                            }
                        }
                        else
                        {
                            result = "(0-0/0/0)";
                            op = 0;
                        }
                    }
                    else if (from_dt == null)
                    {
                        result = "(0-0/null/?)";
                        op = 0;
                    }
                    else if (from_dt != null)
                    {
                        result = "(0-0/?/" + from_dt.Rows.Count + ")";
                        op = 0;
                    }
                }
                else
                {
                    result = "(max id is zero)";
                    op = -1;
                }
            }
            catch (Exception ex)
            {
                result += "Error: occured during process: " + ex.ToString();
                op = -1;
            }
         * 
            return op;
        }

         */ 

        ////////////////////////////////////////////////////////////////////////////    
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}