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

namespace Ia.Ngn.Cl.Model.Ui
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Mail process support class of Next Generation Network'a (NGN's) UI model.
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
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Mail()
        {
        }

        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Test(string name, string email, out string result)
        {
            bool b;
            string content, subject;
            DateTime now;

            now = DateTime.UtcNow.AddHours(3);

            subject = "Test (" + now.ToString("yyyy-MM-dd HH:mm") + ")";

            content = "Test: Daily Next Generation Network OFN Status Report: " + now.ToString("yyyy-MM-dd HH:mm") + "\r\n"
                + "Status: undefined.\r\n"
                + @"For help send ""help"" in a direct message." + "\r\n";

            b = Ia.Ngn.Cl.Model.Ui.Mail.SendPlainMail(name, email, subject, content, out result);

            return b;
        }

        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool SendPlainMail(string name, string email, string subject, string content, out string result)
        {
            bool b;
            string message;

            message = Ia.Ngn.Cl.Model.Ui.Default.PlainMailTop();

            message += content;

            message += Ia.Ngn.Cl.Model.Ui.Default.PlainMailBottom();

            result = "";

            b = global::Ia.Cl.Model.Smtp.SendPlain(name, email, subject, message, out result);

            if (b) result = "Email sent. ";
            else result = "Email was not send: " + result;

            return b;
        }

        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool SendMail(string name, string email, string subject, string content, out string result)
        {
            bool b;
            string message;

            message = Ia.Ngn.Cl.Model.Ui.Default.MailTop();

            message += content;

            message += Ia.Ngn.Cl.Model.Ui.Default.MailBottom();

            result = "";

            b = global::Ia.Cl.Model.Smtp.SendHtml(name, email, subject, message, out result);

            if (b) result = "Email sent. ";
            else result = "Email was not send: " + result;

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}