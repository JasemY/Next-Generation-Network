using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Configuration;

namespace Ia.Ngn.Cl.Model.Business
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Mail process support class of Next Generation Network'a (NGN's) business model.
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
    public partial class Mail
    {
        private bool serverEnableSsl;
        private string serverHost, serverUser, serverPassword, defaultMailbox;
        private Ia.Cl.Model.Imap imap;

        private List<string> mailboxList;
        private List<string> mailRegexToMailboxList;

        /// <summary/>
        public bool EnabledSsl { get; set; }
        /// <summary/>
        public bool IsActive { get; set; }
        /// <summary/>
        public bool IsActiveDuringWorkingHoursOnly { get; set; }
        /// <summary/>
        public int MailboxCheckFrequencyInMinutes { get; set; }
        /// <summary/>
        public string Host { get; set; }
        /// <summary/>
        public string UserName { get; set; }
        /// <summary/>
        public string Password { get; set; }
        /// <summary/>
        public List<string> MailboxList { get { return mailboxList; } }
        /// <summary/>
        public List<string> MailRegexToMailboxList { get { return mailRegexToMailboxList; } }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Mail()
        {
            bool b;

            mailRegexToMailboxList = new List<string>(100);
            mailboxList = new List<string>(100);

            serverHost = ConfigurationManager.AppSettings["imapServerHost"].ToString();
            serverUser = ConfigurationManager.AppSettings["imapServerUser"].ToString();
            serverPassword = ConfigurationManager.AppSettings["imapServerPassword"].ToString();
            serverEnableSsl = bool.TryParse(ConfigurationManager.AppSettings["imapServerEnableSsl"].ToString(), out b) ? b : false;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void Connect()
        {
            imap = new Ia.Cl.Model.Imap(serverHost, serverUser, serverPassword, serverEnableSsl);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return imap.IsConnected;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void Disconnect()
        {
            imap.Disconnect();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void InitializeMailboxes()
        {
            // Create Processed and Unprocessed folders if they don't already exist

            bool b;
            List<string> mailboxList;

            b = false;

            try
            {
                this.Connect();

                if (this.IsConnected)
                {
                    mailboxList = imap.MailboxList();

                    // below: determain change if any
                    // below: mailboxes to add:
                    if (!mailboxList.Contains("Processed")) imap.CreateMailbox("Processed");
                    if (!mailboxList.Contains("Unprocessed")) imap.CreateMailbox("Unprocessed");

                    // below: read all mailboxes again
                    mailboxList = imap.MailboxList();

                    b = true;
                }

                this.Disconnect();
            }
            catch (Exception ex)
            {
                b = false;

                //result = ex.ToString();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool AddMailbox(string name, out string result)
        {
            bool b;
            string s;

            if (Ia.Cl.Model.File.IsValidFilename(name))
            {
                // below: check if the mailbox name exists in upper or lower forms
                s = (from q in mailboxList where q.ToLower() == name.ToLower() select q).SingleOrDefault();

                if (s == null) // no duplicates
                {
                    mailboxList.Add(name);

                    result = "Success: Mailbox name '" + name + "' assigned to be added. ";
                    b = true;
                }
                else
                {
                    result = "Error: Mailbox name '" + name + "' already assigned or exists. ";
                    b = false;
                }
            }
            else
            {
                result = "Error: Mailbox '" + name + "' is not valid. ";
                b = false;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool DeleteMailbox(string name, out string result)
        {
            bool b;

            if (mailboxList.Contains(name) || mailboxList.Contains(name.ToLower()))
            {
                mailboxList.Remove(name);

                result = "Success: Mailbox name '" + name + "' assigned to be deleted. ";
                b = true;
            }
            else
            {
                result = "Error: Mailbox name '" + name + "' does not exist. ";
                b = false;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void CopyMailboxList(List<string> newMailboxList)
        {
            // below: this will copy mailbox list into object

            mailboxList.Clear();

            foreach (string s in newMailboxList) mailboxList.Add(s);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool AddRegex(string mailboxName, string regex, out string result)
        {
            bool b;

            if (Ia.Cl.Model.Default.IsRegexPatternValid(regex))
            {
                if (mailboxList.Contains(mailboxName) || mailboxList.Contains(mailboxName.ToLower()))
                {
                    if (!mailRegexToMailboxList.Contains(regex + " (" + mailboxName + ")"))
                    {
                        mailRegexToMailboxList.Add(regex + " (" + mailboxName + ")");

                        result = "Success: Regex '" + regex + "' for mailbox '" + mailboxName + "' is added. ";
                        b = true;
                    }
                    else
                    {
                        result = "Error: Regex '" + regex + "' for mailbox '" + mailboxName + "' already exists. ";
                        b = false;
                    }
                }
                else
                {
                    result = "Error: Mailbox '" + mailboxName + "' is not assigned or does not exist. ";
                    b = false;
                }
            }
            else
            {
                result = "Error: Regex pattern '" + regex + "' is invalid. ";
                b = false;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool DeleteRegex(string regex, out string result)
        {
            bool b;

            if (mailRegexToMailboxList.Contains(regex))
            {
                mailRegexToMailboxList.Remove(regex);

                result = "Success: regex '" + regex + "' was removed. ";
                b = true;
            }
            else
            {
                result = "Error: regex does not exist. ";
                b = false;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Mail DeserializeFromString(string serializedObject)
        {
            Mail m;

            m = serializedObject.XmlDeserializeFromString<Mail>();

            return m;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public string SerializeToString()
        {
            StringBuilder sb;

            sb = new StringBuilder(10000);

            var serializer = new XmlSerializer(typeof(Mail));

            using (var writer = XmlWriter.Create(sb))
            {
                serializer.Serialize(writer, this);
            }

            return sb.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Sorter(Mail m, out string result)
        {
            bool b;
            int numberOfMessagesMoved;
            string regex, destinationMailbox; //, messageId, email;
            List<string> mailboxList, mailboxesToDeleteList, mailboxesToAddList;
            Hashtable mailRegexToMailboxHashtable, /*messageIdToFromEmailHashtable,*/ messageIdToDestinationMailboxHashtable;
            Match match;
            Ia.Cl.Model.Imap imap2;

            b = false;
            numberOfMessagesMoved = 0;
            result = "";

            mailboxesToDeleteList = new List<string>(100);
            mailboxesToAddList = new List<string>(100);
            mailRegexToMailboxHashtable = new Hashtable(100);
            messageIdToDestinationMailboxHashtable = new Hashtable(1000);

            try
            {
                imap2 = new Ia.Cl.Model.Imap(m.Host, m.UserName, m.Password, m.EnabledSsl);
                //imap2 = new Ia.Cl.Model.Imap("imap.*.com", "*@*.com", "*", false);

                imap2.Connect();

                if (imap2.IsConnected)
                {
                    // below: read all mailboxes
                    mailboxList = imap2.MailboxList();

                    // below: determain change if any
                    // below: mailboxes to add:
                    foreach (string s in m.MailboxList) if (!mailboxList.Contains(s)) mailboxesToAddList.Add(s);

                    // below: mailboxes to delete:
                    // below: I will disable deletion of mailboxes
                    // foreach (string s in mailboxArrayList) if (!m.MailboxList.Contains(s)) mailboxesToAddArrayList.Add(s);

                    // below: create mailboxes
                    foreach (string s in mailboxesToAddList) imap2.CreateMailbox(s);

                    // below: delete mailboxes
                    // foreach (string s in mailboxesToDeleteArrayList) imap.DeleteMailbox(s);

                    // below: read all mailboxes again
                    mailboxList = imap2.MailboxList();

                    // below: assign change to object
                    m.CopyMailboxList(mailboxList);

                    // below: start moving
                    foreach (string s in m.MailRegexToMailboxList)
                    {
                        match = Regex.Match(s, @"(.+?) \((.+?)\)");

                        if (match.Success)
                        {
                            regex = match.Groups[1].Value;
                            destinationMailbox = match.Groups[2].Value;

                            numberOfMessagesMoved += imap2.MoveMessagesFromEmailToMailbox(regex, destinationMailbox);
                        }
                    }

                    imap2.Disconnect();

                    result = "Number of messages moved: " + numberOfMessagesMoved;
                    b = true;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                b = false;

                result = ex.ToString();
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Scheduler(DateTime now, out string result)
        {
            bool b;
            List<Tuple<string, string>> recepientTupleList;

            b = true;
            result = "";

            if (Ia.Ngn.Cl.Model.Business.Mail.Schedule(now, "send-list-with-ont-list-provisioned-and-ready-but-do-not-exist-in-customer-department-database", out recepientTupleList))
            {
                foreach (Tuple<string, string> tuple in recepientTupleList)
                {
                    b = Ia.Ngn.Cl.Model.Ui.Administration.EmailListWithOntListProvisionedAndReadyButDoNotExistInCustomerDepartmentDatabase(tuple.Item1, tuple.Item2, out result);
                }
            }

            if (Ia.Ngn.Cl.Model.Business.Mail.Schedule(now, "email-daily-ofn-status-report", out recepientTupleList))
            {
                foreach (Tuple<string, string> tuple in recepientTupleList)
                {
                    b = Ia.Ngn.Cl.Model.Ui.Administration.EmailDailyOfnStatusReport(tuple.Item1, tuple.Item2, out result);
                }
            }

            if (Ia.Ngn.Cl.Model.Business.Mail.Schedule(now, "email-monthly-ofn-statistics-report", out recepientTupleList))
            {
                foreach (Tuple<string, string> tuple in recepientTupleList)
                {
                    b = Ia.Ngn.Cl.Model.Ui.Administration.EmailStatistics(tuple.Item1, tuple.Item2, out result);
                }
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static bool Schedule(DateTime dateTime, string function, out List<Tuple<string, string>> recepientList)
        {
            bool b;

            recepientList = new List<Tuple<string, string>>();

            if (dateTime.Hour == 8 && dateTime.Minute == 0 && function == "send-list-with-ont-list-provisioned-and-ready-but-do-not-exist-in-customer-department-database")
            {
                recepientList.Add(new Tuple<string, string>("NGN Access Section", "ngn.accessnet@gmail.com"));
                recepientList.Add(new Tuple<string, string>("Jasem Yacoub", "j.alshamlan@gmail.com"));

                b = true;
            }
            else if (dateTime.Hour == 8 && dateTime.Minute == 0 && function == "email-daily-ofn-status-report")
            {
                recepientList.Add(new Tuple<string, string>("Jasem Yacoub", "j.alshamlan@gmail.com"));

                b = true;
            }
            else if (dateTime.Hour == 8 && dateTime.Minute == 0 && dateTime.Day == 1 && function == "email-monthly-ofn-statistics-report")
            {
                recepientList.Add(new Tuple<string, string>("Jasem Yacoub", "j.alshamlan@gmail.com"));
                recepientList.Add(new Tuple<string, string>("MOC Traffic Section", "traffic_section@yahoo.com"));

                b = true;
            }
            else if (dateTime.Hour == 8 && dateTime.Minute == 0 && function == "email-monthly-ofn-statistics-report")
            {
                recepientList.Add(new Tuple<string, string>("Jasem Yacoub", "j.alshamlan@gmail.com"));

                b = true;
            }
            else b = false;

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool ProcessMessage(Ia.Cl.Model.Imap.Message message, out string result)
        {
            bool messageProcessed;
            string s, responseSubject, responseBodyText;
            Ia.Ngn.Cl.Model.Staff staff;
            List<List<Ia.Ngn.Cl.Model.Data.Administration.Authority>> authorityListList;

            messageProcessed = false;
            result = "";
            responseSubject = responseBodyText = "";

            staff = (from q in Ia.Ngn.Cl.Model.Data.Staff.List where q.User != null && q.User.Email != null && q.User.Email == message.From select q).SingleOrDefault();

            if (staff != null)
            {
                /*
                if (message.Subject.ToLower().Contains("help") || message.BodyText.ToLower().Contains("help"))
                {
                    authorityListList = (from q in staff.Framework.Ancestors select q.Authorities).ToList();

                    if (authorityListList != null)
                    {
                        s = "";

                        foreach (List<Ia.Ngn.Cl.Model.Data.Administration.Authority> authorityList in authorityListList)
                        {
                            foreach (Ia.Ngn.Cl.Model.Data.Administration.Authority authority in authorityList)
                            {
                                s += authority.Help + "\r\n";
                            }
                        }

                        responseBodyText = s;
                    }

                    messageProcessed = true;
                }
                else*/ if (message.Subject.ToLower().Contains("test") || message.BodyText.ToLower().Contains("test"))
                {
                    responseSubject = "Re: " + message.Subject;
                    responseBodyText = message.BodyText;

                    messageProcessed = Ia.Ngn.Cl.Model.Ui.Mail.Test(staff.FullName, staff.User.Email, out result);
                }
                else if (message.Subject.ToLower().Contains("find") || message.BodyText.ToLower().Contains("find"))
                {
                    // use URIs
                    messageProcessed = Ia.Ngn.Cl.Model.Ui.Maintenance.Find.SearchForMail(staff.FullName, staff.User.Email, message.Subject, out result);
                }
                else
                {
                    messageProcessed = false;
                }
            }
            else messageProcessed = false;

            result = message.From + "|" + "Processed?: " + messageProcessed.ToString().ToLower();

            return messageProcessed;
        }

        /*
/////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////

/// <summary>
///
/// </summary>
public int Isp_Mail(int isp_id, out string result)
{
    int op, count;
    string message, sql, from, to, sa; //, ni;
    string name, name_ar, description, service_request_customer_id, email, subject;
    StringBuilder sb;
    DateTime now;
    DataTable dt;

    op = 0;
    count = 0;
    result = "";
    sb = new StringBuilder(10000);

    / *
    <isp id="1" name="Quality Net"
    <isp id="2" name="FastTelco"
    <isp id="3" name="UCC-United Networks"
    <isp id="4" name="KEMS - Zajel"
    * /

    now = DateTime.UtcNow.AddHours(3);

    name = hsi_xd.SelectSingleNode("hsi/isp/isp[@id='" + isp_id + "']").Attributes["name"].Value;
    name_ar = hsi_xd.SelectSingleNode("hsi/isp/isp[@id='" + isp_id + "']").Attributes["name_ar"].Value;
    description = hsi_xd.SelectSingleNode("hsi/isp/isp[@id='" + isp_id + "']").Attributes["description"].Value;
    service_request_customer_id = hsi_xd.SelectSingleNode("hsi/isp/isp[@id='" + isp_id + "']").Attributes["service_request_customer_id"].Value;
    email = hsi_xd.SelectSingleNode("hsi/isp/isp[@id='" + isp_id + "']").Attributes["email"].Value;

    // below: collecting information

    // Note:  if today is a Sunday, we will read information from the last week hours to cover HSI provisioning from last Thursday
    if (now.DayOfWeek == DayOfWeek.Sunday)
    {
        subject = "List of Provisioned HSI During Last Working Week Hours Until <b>" + now.ToString("yyyy-MM-dd HH:mm") + "</b>";
        from = "'" + now.AddHours(-24 * 7).ToString("yyyy-MM-ddTHH:mm:ss") + "'";
    }
    else
    {
        subject = "List of Provisioned HSI During Last Working 24 Hours Until <b>" + now.ToString("yyyy-MM-dd HH:mm") + "</b>";
        from = "'" + now.AddHours(-24).ToString("yyyy-MM-ddTHH:mm:ss") + "'";
    }

    to = "'" + now.ToString("yyyy-MM-ddTHH:mm:ss") + "'";

    sql = "sr.id, sr.service_id, sr.service_category_id, sr.status, sr.request_time, sr.customer_id, sr.customer_name, sr.customer_category_id, sr.customer_address, srt1.type_value AS sr_ont_id, srt2.type_value AS sr_ip, srt3.type_value AS sr_dn, srt4.type_value AS sr_customer_name, c.olt, c.olt_rack, c.olt_sub, c.olt_card, c.olt_port, h.port, h.updated FROM ia_service_request AS sr LEFT OUTER JOIN ia_service_request_type AS srt1 ON sr.id = srt1.ia_service_request_id AND srt1.type_id = 17 LEFT OUTER JOIN ia_service_request_type AS srt2 ON sr.id = srt2.ia_service_request_id AND srt2.type_id = 46 LEFT OUTER JOIN ia_service_request_type AS srt3 ON sr.id = srt3.ia_service_request_id AND srt3.type_id = 48 LEFT OUTER JOIN ia_service_request_type AS srt4 ON sr.id = srt4.ia_service_request_id AND srt4.type_id = 49 INNER JOIN ia_connection AS c ON c.ip = srt2.type_value INNER JOIN ia_hsi AS h ON h.ia_connection_id = c.id WHERE (sr.id IN (SELECT ia_service_request_id FROM ia_service_request_type AS srt WHERE (type_id = 48)    )) AND (sr.service_category_id = 49) AND (sr.request_time > " + from + " AND sr.request_time <= " + to + ")";

    dt = Ia.Cs.Db.SqlServer.StaticSelect(@"SELECT " + sql + " AND (customer_id = " + service_request_customer_id + ") ORDER BY sr.request_time, sr.id");

    foreach (DataRow dr in dt.Rows)
    {
        sa = dr["sr_ont_id"].ToString().PadRight(12, ' ') + "  " + dr["sr_dn"].ToString() + "  " + dr["port"].ToString().PadRight(5, ' ') + "  " + dr["updated"].ToString() + "\n";
        sb.Append(sa);
        count++;
    }

    message = Ia.Ngn.Cs.This.Mail_Top();
    message += @"
<p>" + subject + @".</p>

<p><span style=""color:DarkRed"">Note</span>: This email is sent automatically. If you have requests or suggestions please email HSI support (<a href=mailto:hsi@moc.kw>hsi@moc.kw</a>)</p>

<table cellspacing=""0"">
<tr><td><hr/></td></tr>
<tr>
<td valign=""top"">
<table class=""form"">
<tr><td style=""vertical-align:top;color:MediumSlateBlue"">ISP:</td><td style=""vertical-align:top;""><span style=""color:Olive"">" + name + @"</span> (" + name_ar + @")</td></tr>
<tr><td style=""vertical-align:top;color:MediumSlateBlue"">Description:</td><td style=""vertical-align:top;"">" + description + @"</td></tr>
<tr><td style=""vertical-align:top;color:MediumSlateBlue"">Email:</td><td style=""vertical-align:top;"">" + email + @"</td></tr>
</table>
</td>
</tr>
<tr><td><hr/></td></tr>
</table>

<br/>";

    // below: remove last ' ' char
    if (sb.Length > 0)
    {
        sb.Remove(sb.Length - 1, 1);

        message += "Total of <u>" + count + "</u> service requests provisioned";
    }
    else
    {
        message += "Total of 0 (zero) service requests provisioned";
    }

    message += @"<pre style=""font-family:Courier New"">Location      Number   Port   Update" + "\n" + sb.ToString() + @"</pre>";
    message += "<br/><br/><br/><br/>";

    message += Ia.Ngn.Cs.This.Mail_Bottom();

    result = "";
    Ia.Cs.Mail.Send_Html(name, email, subject, "k.alazmi@qualitynet.net,Khaled Al-Azmi;hsi@moc.kw,Ali Faia", message, out result);

    if (result == "") { result = "ISP Mail sent"; op = 1; }
    else op = -1;

    return op;
}
*/

        /*
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public int Report_Mail(string email, string report_name, out string result)
        {
            int op;
            string message, description;
            string name, subject;
            StringBuilder sb;
            ArrayList al;
            DateTime now;

            op = 0;
            result = "";
            sb = new StringBuilder(10000);

            now = DateTime.UtcNow.AddHours(3);

            name = "NGN Staff";
            description = "Attn: Customer Care Department";
            subject = "List of Complete Address and Connection Information, of Ready ONTs as of " + now.ToString("yyyy-MM-dd HH:mm");

            // below: collecting information

            message = Ia.Ngn.Cs.This.Mail_Top();
            message += @"

<p>List of Complete Address and Connection Information, of Ready ONTs as of <b>" + now.ToString("yyyy-MM-dd HH:mm") + @"</b> This report does not contain any parameters sent in previous reports.</p>

<p><span style=""color:DarkRed"">Note</span>: This email is sent automatically. If you have requests or suggestions please email support (<a href=mailto:hsi@moc.kw>hsi@moc.kw</a>)</p>

<table cellspacing=""0"">
<tr><td><hr/></td></tr>
<tr>
<td valign=""top"">
<table class=""form"">
<tr><td style=""vertical-align:top;color:MediumSlateBlue"">Name:</td><td style=""vertical-align:top;""><span style=""color:Olive"">" + name + @"</span></td></tr>
<tr><td style=""vertical-align:top;color:MediumSlateBlue"">Description:</td><td style=""vertical-align:top;"">" + description + @"</td></tr>
<tr><td style=""vertical-align:top;color:MediumSlateBlue"">Email:</td><td style=""vertical-align:top;"">" + email + @"</td></tr>
<tr><td style=""vertical-align:top;color:MediumSlateBlue"">Report:</td><td style=""vertical-align:top;"">" + report_name.Replace("report_file ", "") + @"</td></tr>
</table>
</td>
</tr>
<tr><td><hr/></td></tr>
</table>

<br/>";

            message += @"<pre style=""font-family:Courier New"">Location" + "\n";

            Ia.Cs.Db.SqlServer.Misc_Select(report_name, out al);

            foreach (string s in al)
            {
                sb.Append(s + "\n");
            }

            message += sb.ToString();

            message += @"</pre>";
            message += "<br/><br/><br/><br/>";

            message += Ia.Ngn.Cs.This.Mail_Bottom();

            result = "";
            Ia.Cs.Smtp.Send_Html(name, email, subject/*, "k.alazmi@qualitynet.net,Khaled Al-Azmi;hsi@moc.kw,Ali Faia"* /, message, out result);

            if (result == "") { result = "Mail sent"; op = 1; }
            else op = -1;

            return op;
        }
         */

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}