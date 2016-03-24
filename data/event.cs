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

namespace Ia.Ngn.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// AMS Event support class for Next Generation Network (NGN) data model.
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
        private static long startEventId;
        private static XDocument xDocument;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public enum Priority
        {
            Urgent = 1, Important, Regular, Unspecified
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public struct Severity
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public struct ServiceEffect
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public struct Alarm
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public Severity Severity { get; set; }
            public ServiceEffect ServiceEffect { get; set; }
            public bool SaveMessage { get; set; }
            public string Condition { get; set; }
            public string Resolution { get; set; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Event() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// This defines a starting point of event id for searching
        /// </summary>
        public static long StartEventId
        {
            get
            {
                if (startEventId == 0)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application["eventStartEventId"] != null)
                    {
                        startEventId = long.Parse(HttpContext.Current.Application["eventStartEventId"].ToString());
                    }
                    else
                    {
                        startEventId = StartIdFromEventTime(DateTime.UtcNow.AddHours(3).AddDays(-30));

                        if (HttpContext.Current != null) HttpContext.Current.Application["eventStartEventId"] = startEventId.ToString();
                    }
                }

                return startEventId;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static long StartIdFromEventTime(DateTime startEventTime)
        {
            long id;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                id = (from e in db.Events where e.EventTime >= startEventTime select e.Id).Min();
            }

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Event> ReadList()
        {
            List<Ia.Ngn.Cl.Model.Event> list;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                list = (from q in db.Events select q).ToList();
            }

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Event> ReadNewOntList()
        {
            List<Ia.Ngn.Cl.Model.Event> list;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                list = (from q in db.Events where q.Cause == "NEWONT" select q).ToList();
            }

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Event> ReadListTakeNOrderByEventTimeDescending(int elementsToTake)
        {
            List<Ia.Ngn.Cl.Model.Event> list;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                list = (from e in db.Events orderby e.EventTime descending select e).Take(elementsToTake).ToList();
            }

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Event> ReadCauseNewOntListTakeNOrderByIdDescending(int elementsToTake)
        {
            List<Ia.Ngn.Cl.Model.Event> list;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                list = (from e in db.Events where e.Id > Ia.Ngn.Cl.Model.Data.Event.StartEventId && e.Cause == "NEWONT" orderby e.Id descending select e).Take(elementsToTake).ToList();
            }

            /*
                string s, sql;
                StringBuilder sb;
                Hashtable ht, a_ht;
                DataTable dt, a_dt;

                sb = new StringBuilder(1000);
                ht = new Hashtable(1000);
                a_ht = new Hashtable(1000);

                sql = @"SELECT e.id, '' AS last_event, f.area, f.pon, f.ont, f.olt, f.olt_card, f.olt_port, e.system_id, e.aid, f.ont_serial, e.detail, f.area AS f_area, f.block, f.street, 
                                 f.premises_old, f.premises_new, e.node_time, e.event_time, e.created
        FROM            ia_event AS e LEFT OUTER JOIN
                                 ia_field AS f ON e.detail LIKE '%' + f.ont_serial + '%'
        WHERE        (e.cause = 'NEWONT') AND (e.id =
                                     (SELECT        MAX(id) AS max_id
                                       FROM            ia_event AS e2
                                       WHERE        (cause = 'NEWONT') AND (system_id = e.system_id) AND (aid = e.aid) AND (detail = e.detail))) AND (e.severity <> 'CL')
        ORDER BY e.created DESC";

                dt = null; // main.Data.Select(sql);

                if (dt != null && dt.Rows.Count > 1)
                {
                    // below: now collect all valid ont_position entries and get the last event for them
                    ///gh3
                    sql = "";

                    foreach (DataRow r in dt.Rows)
                    {
                        if (r["ont"] != null && r["ont"].ToString().Length > 0)
                        {
                            s = r["system_id"].ToString() + ":" + r["aid"].ToString().Replace("PON", "ONT") + "-" + r["ont"].ToString();
                            ht[s] = r["id"].ToString();
                            sb.Append("e.system_id='" + r["system_id"].ToString() + "' AND " + "e.aid='" + r["aid"].ToString().Replace("PON", "ONT") + "-" + r["ont"].ToString() + "' OR ");
                        }
                    }

                    sql = sb.ToString();

                    if (sql.Length > 0)
                    {
                        sql = sql.Remove(sql.Length - 4, 4);

                        a_dt = null; // main.Data.Select(@"SELECT system_id, aid, cause, severity FROM ia_event AS e WHERE (" + sql + @") AND (id = (SELECT MAX(id) AS max_id FROM ia_event AS e2 WHERE (system_id = e.system_id) AND (aid = e.aid)))");

                        // below: now loop through table and collect cause + severity
                        foreach (DataRow r in a_dt.Rows)
                        {
                            s = r["system_id"].ToString() + ":" + r["aid"].ToString();
                            a_ht[ht[s].ToString()] = r["cause"].ToString() + ":" + r["severity"].ToString();
                        }

                        // below: now loop through original table and insert last_event

                        foreach (DataRow r in dt.Rows)
                        {
                            s = r["id"].ToString();
                            if (a_ht.ContainsKey(s)) r["last_event"] = a_ht[s].ToString();
                        }

                        gv.DataSource = dt.DefaultView;
                        gv.DataBind();
                    }
                }
            }
                 */

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Alarm> AlarmList
        {
            get
            {
                Alarm alarm;
                List<Alarm> alarmList;

                alarmList = new List<Alarm>();

                foreach (XElement xe in XDocument.Element("event").Element("alarm").Element("aid").Elements("alarmList").Elements("alarm"))
                {
                    alarm = new Alarm();

                    alarm.Id = xe.Attribute("id").Value;
                    alarm.Name = xe.Attribute("name").Value;
                    //alarm.Severity = xe.Attribute("severity").Value;
                    //alarm.ServiceEffect = xe.Attribute("serviceEffect").Value;
                    alarm.SaveMessage = bool.Parse(xe.Attribute("saveMessage").Value);
                    alarm.Condition = xe.Element("condition").Value;
                    alarm.Resolution = xe.Element("resolution").Value;

                    alarmList.Add(alarm);
                }

                return alarmList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// How to embed and access resources by using Visual C# http://support.microsoft.com/kb/319292/en-us
        /// 
        /// 1. Change the "Build Action" property of your XML file from "Content" to "Embedded Resource".
        /// 2. Add "using System.Reflection".
        /// 3. See sample below.
        /// 
        /// </summary>

        private static XDocument XDocument
        {
            get
            {
                if (xDocument == null)
                {
                    Assembly _assembly;
                    StreamReader streamReader;

                    _assembly = Assembly.GetExecutingAssembly();
                    streamReader = new StreamReader(_assembly.GetManifestResourceStream("Ia.Ngn.Cl.model.data.event.xml"));

                    try
                    {
                        if (streamReader.Peek() != -1)
                        {
                            xDocument = System.Xml.Linq.XDocument.Load(streamReader);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                    }
                }

                return xDocument;
            }
        }

        ////////////////////////////////////////////////////////////////////////////    
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
