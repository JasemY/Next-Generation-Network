using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Report Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class Report
    {
        /// <summary/>
        public Report()
        {
            // this.ReportHistories = new List<ReportHistory>();
        }

        /// <summary/>
        public int Id { get; set; }

        /// <summary/>
        public string Service { get; set; }

        /// <summary/>
        public int ServiceType { get; set; }

        /// <summary/>
        public int State { get; set; }

        /// <summary/>
        public int Status { get; set; }

        /// <summary/>
        public int Category { get; set; }

        /// <summary/>
        public int Area { get; set; }

        /// <summary/>
        public int Priority { get; set; }

        /// <summary/>
        public int Severity { get; set; }

        /// <summary/>
        public string Detail { get; set; }

        /// <summary/>
        public string Contact { get; set; }

        /// <summary/>
        public string Note { get; set; }

        /// <summary/>
        public DateTime Created { get; set; }

        /// <summary/>
        public DateTime Updated { get; set; }

        /// <summary/>
        public DateTime Inspected { get; set; }

        /// <summary/>
        public System.Guid UserId { get; set; }

        /// <summary/>
        public virtual ICollection<ReportHistory> ReportHistories { get; set; }

        /// <summary/>
        [NotMapped]
        public bool StatusIsOpen 
        {
            get
            {
                return this.Status == (int)Ia.Ngn.Cl.Model.Business.Report.Status.Open;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int Create(Report report, out string result)
        {
            int id;

            id = -1;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                report.Created = report.Updated = report.Inspected = DateTime.UtcNow.AddHours(3);

                db.Reports.Add(report);
                db.SaveChanges();

                id = report.Id;
            }

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Report Read(int reportId)
        {
            Report report;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                report = (from q in db.Reports where q.Id == reportId select q).Include(r => r.ReportHistories).SingleOrDefault();
            }

            return report;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Report> ReadList(string service)
        {
            List<Ia.Ngn.Cl.Model.Report> reportList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                try
                {
                    reportList = (from q in db.Reports where q.Service == service select q).Include(r => r.ReportHistories).ToList();
                }
                catch
                {
                    reportList = null;
                }
            }

            return reportList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Report> ReadListForServiceFromReportId(int reportId)
        {
            List<Ia.Ngn.Cl.Model.Report> reportList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                try
                {
                    reportList = (from q in db.Reports join r in db.Reports on q.Service equals r.Service where r.Id == reportId select r).Include(r => r.ReportHistories).ToList();
                }
                catch
                {
                    reportList = null;
                }
            }

            return reportList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        [NotMapped]
        public ReportHistory LastReportHistory
        {
            get
            {
                ReportHistory reportHistory;

                reportHistory = null;

                if (this.ReportHistories != null)
                {
                    if (this.ReportHistories.Count > 0)
                    {
                        reportHistory = this.ReportHistories.Skip(this.ReportHistories.Count - 1).FirstOrDefault();
                    }
                    else reportHistory = null;
                }
                else reportHistory = null;

                return reportHistory;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        [NotMapped]
        public ReportHistory SecondToLastReportHistory
        {
            get
            {
                ReportHistory reportHistory;

                reportHistory = null;

                if (this.ReportHistories != null)
                {
                    if (this.ReportHistories.Count > 1)
                    {
                        reportHistory = this.ReportHistories.Skip(this.ReportHistories.Count - 2).FirstOrDefault();
                    }
                    else reportHistory = null;
                }
                else reportHistory = null;

                return reportHistory;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool CloseStatus(Ia.Ngn.Cl.Model.Staff staff)
        {
            bool b;

            b = false;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                this.Status = 2; // <status id="2" name="Closed" ...>

                this.Updated = DateTime.UtcNow.AddHours(3);
                //this.UserId = staff.UserId;
                // above: can't do that because it will remove the name of the record inserter

                db.Reports.Attach(this);
                db.Entry(this).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool OpenStatus(Ia.Ngn.Cl.Model.Staff staff)
        {
            bool b;

            b = false;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                this.Status = 1; // <status id="1" name="Opened" ...>

                this.Updated = DateTime.UtcNow.AddHours(3);
                //this.UserId = staff.UserId;
                // above: can't do that because it will remove the name of the record inserter

                db.Reports.Attach(this);
                db.Entry(this).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateMigratedList(List<Report> reportList, out string result)
        {
            bool b;
            Report report;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                foreach (Report updatedReport in reportList)
                {
                    report = (from q in db.Reports where q.Id == updatedReport.Id select q).SingleOrDefault();

                    if (report == null)
                    {
                        //updatedReport.Created = updatedReport.Updated = updatedReport.Inspected = DateTime.UtcNow.AddHours(3);

                        db.Reports.Add(updatedReport);
                    }
                    else
                    {
                        // below: copy values from updatedReport to report

                        report.UpdateMigrated(updatedReport);

                        db.Reports.Attach(report);

                        db.Entry(report).State = System.Data.Entity.EntityState.Modified;
                    }

                    b = true;

                }

                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool UpdateMigrated(Report updatedReport)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.Area != updatedReport.Area) { this.Area = updatedReport.Area; updated = true; }
            if (this.Category != updatedReport.Category) { this.Category = updatedReport.Category; updated = true; }
            if (this.Contact != updatedReport.Contact) { this.Contact = updatedReport.Contact; updated = true; }
            if (this.Detail != updatedReport.Detail) { this.Detail = updatedReport.Detail; updated = true; }
            if (this.Note != updatedReport.Note) { this.Note = updatedReport.Note; updated = true; }
            if (this.Priority != updatedReport.Priority) { this.Priority = updatedReport.Priority; updated = true; }
            if (this.Service != updatedReport.Service) { this.Service = updatedReport.Service; updated = true; }

            if (this.ServiceType != updatedReport.ServiceType) { this.ServiceType = updatedReport.ServiceType; updated = true; }
            if (this.ReportHistories != updatedReport.ReportHistories) { this.ReportHistories = updatedReport.ReportHistories; updated = true; }
            if (this.Severity != updatedReport.Severity) { this.Severity = updatedReport.Severity; updated = true; }
            if (this.State != updatedReport.State) { this.State = updatedReport.State; updated = true; }
            if (this.Status != updatedReport.Status) { this.Status = updatedReport.Status; updated = true; }

            if (this.UserId != updatedReport.UserId) { this.UserId = updatedReport.UserId; updated = true; }

            // below: this is an update of migrated data
            if (this.Created != updatedReport.Created) { this.Created = updatedReport.Created; updated = true; }
            if (this.Updated != updatedReport.Updated) { this.Updated = updatedReport.Updated; updated = true; }

            this.Inspected = DateTime.UtcNow.AddHours(3);

            //if (updated) this.Updated = this.Inspected = DateTime.UtcNow.AddHours(3);
            //else this.Inspected = DateTime.UtcNow.AddHours(3);

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool NullifyUserId(Guid userId, out int numberOfRecordsUpdated)
        {
            return MoveUserId(userId, Guid.Empty, out numberOfRecordsUpdated);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool MoveUserId(Guid fromUserId, Guid toUserId, out int numberOfRecordsUpdated)
        {
            bool b;

            b = false;
            numberOfRecordsUpdated = 0;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                var query = (from q in db.Reports where q.UserId == fromUserId select q).ToList();

                foreach (var v in query)
                {
                    v.UserId = toUserId;
                    numberOfRecordsUpdated++;
                }

                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Delete(int id/*, out string result*/)
        {
            bool b;

            b = false;
            //result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                var v = (from q in db.Reports where q.Id == id select q).FirstOrDefault();

                db.Reports.Remove(v);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }
}