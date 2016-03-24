using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Report History Entity Framework class for Next Generation Network (NGN) entity model.
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
    public class ReportHistory
    {
        /// <summary/>
        public ReportHistory() { }

        /// <summary/>
        public int Id { get; set; }

        /// <summary/>
        public int Indication { get; set; }

        /// <summary/>
        public int Action { get; set; }

        /// <summary/>
        public int Resolution { get; set; }

        /// <summary/>
        public int Estimate { get; set; }

        /// <summary/>
        public int Area { get; set; }

        /// <summary/>
        public string Detail { get; set; }

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
        public virtual Report Report { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(ReportHistory reportHistory, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                reportHistory.Report = (from q in db.Reports where q.Id == reportHistory.Report.Id select q).SingleOrDefault();

                reportHistory.Created = reportHistory.Updated = reportHistory.Inspected = DateTime.UtcNow.AddHours(3);

                db.ReportHistories.Add(reportHistory);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static ReportHistory Read(int reportHistoryId)
        {
            ReportHistory reportHistory;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                reportHistory = (from q in db.ReportHistories where q.Id == reportHistoryId select q).SingleOrDefault();
            }

            return reportHistory;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<ReportHistory> ReadListForReportId(int reportId)
        {
            List<ReportHistory> reportHistoryList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                reportHistoryList = (from q in db.ReportHistories where q.Report.Id == reportId select q).Include(x => x.Report).Include(x => x.Report.ReportHistories).ToList();
            }

            return reportHistoryList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateMigratedList(List<ReportHistory> reportHistoryList, out string result)
        {
            bool b;
            ReportHistory reportHistory;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                foreach (ReportHistory updatedReportHistory in reportHistoryList)
                {
                    reportHistory = (from q in db.ReportHistories where q.Id == updatedReportHistory.Id select q).SingleOrDefault();

                    if (reportHistory == null)
                    {
                        //updatedReport.Created = updatedReport.Updated = updatedReport.Inspected = DateTime.UtcNow.AddHours(3);

                        db.ReportHistories.Add(updatedReportHistory);
                    }
                    else
                    {
                        // below: copy values from updatedReport to report

                        reportHistory.UpdateMigrated(updatedReportHistory);

                        db.ReportHistories.Attach(reportHistory);

                        db.Entry(reportHistory).State = System.Data.Entity.EntityState.Modified;
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
        public bool UpdateMigrated(ReportHistory updatedReportHistory)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.Action != updatedReportHistory.Action) { this.Action = updatedReportHistory.Action; updated = true; }
            if (this.Area != updatedReportHistory.Area) { this.Area = updatedReportHistory.Area; updated = true; }
            if (this.Detail != updatedReportHistory.Detail) { this.Detail = updatedReportHistory.Detail; updated = true; }
            if (this.Note != updatedReportHistory.Note) { this.Note = updatedReportHistory.Note; updated = true; }
            if (this.Estimate != updatedReportHistory.Estimate) { this.Estimate = updatedReportHistory.Estimate; updated = true; }
            if (this.Indication != updatedReportHistory.Indication) { this.Indication = updatedReportHistory.Indication; updated = true; }

            if (this.Resolution != updatedReportHistory.Resolution) { this.Resolution = updatedReportHistory.Resolution; updated = true; }
            if (this.Report != updatedReportHistory.Report) { this.Report = updatedReportHistory.Report; updated = true; }

            if (this.UserId != updatedReportHistory.UserId) { this.UserId = updatedReportHistory.UserId; updated = true; }

            // below: this is an update of migrated data
            if (this.Created != updatedReportHistory.Created) { this.Created = updatedReportHistory.Created; updated = true; }
            if (this.Updated != updatedReportHistory.Updated) { this.Updated = updatedReportHistory.Updated; updated = true; }

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
                var query = (from q in db.ReportHistories where q.UserId == fromUserId select q).ToList();

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
                var v = (from q in db.ReportHistories where q.Id == id select q).FirstOrDefault();

                db.ReportHistories.Remove(v);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }
}