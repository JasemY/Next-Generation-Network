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
    /// Transaction support class for Next Generation Network (NGN) data model.
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
    public partial class Transaction
    {
        private static XDocument xDocument;
        private static List<System> systemList;
        private static List<Process> processList;
        private static List<Function> functionList;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public struct Direction
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ArabicName { get; set; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class System
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public virtual ICollection<Process> Processes
            {
                get
                {
                    return (from q in ProcessList where q.System.Id == this.Id select q).ToList();
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Process
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public virtual System System { get; set; }
            public virtual ICollection<Function> Functions
            {
                get
                {
                    return (from q in FunctionList where q.Process.Id == this.Id select q).ToList();
                }
            }

            public static int ProcessId(int systemId, int id)
            {
                return systemId * 100 + id;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public class Function
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Parameter { get; set; }
            public virtual Process Process { get; set; }

            public static int FunctionId(int processId, int id)
            {
                return processId * 100 + id;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Transaction() { }

        //////////////////////////////////////////////////////////////////////////////

        ///// <summary>
        /////
        ///// </summary>
        public static List<System> SystemList
        {
            get
            {
                if (systemList == null || systemList.Count == 0)
                {
                    System system;

                    systemList = new List<System>();

                    foreach (XElement xe in XDocument.Element("transaction").Elements("systemList").Elements("system"))
                    {
                        system = new System();

                        system.Id = int.Parse(xe.Attribute("id").Value);
                        system.Name = xe.Attribute("name").Value;

                        systemList.Add(system);
                    }
                }

                return systemList;
            }
        }

        //////////////////////////////////////////////////////////////////////////////

        ///// <summary>
        /////
        ///// </summary>
        public static List<Process> ProcessList
        {
            get
            {
                if (processList == null || processList.Count == 0)
                {
                    int systemId, id;
                    Process process;

                    processList = new List<Process>();

                    foreach (XElement xe in XDocument.Element("transaction").Elements("systemList").Elements("system").Elements("processList").Elements("process"))
                    {
                        process = new Process();

                        systemId = int.Parse(xe.Parent.Parent.Attribute("id").Value);
                        id = int.Parse(xe.Attribute("id").Value);

                        process.System = (from q in SystemList where q.Id == systemId select q).Single();
                        process.Id = Process.ProcessId(systemId, id);
                        process.Name = xe.Attribute("name").Value;

                        processList.Add(process);
                    }
                }

                return processList;
            }
        }

        //////////////////////////////////////////////////////////////////////////////

        ///// <summary>
        /////
        ///// </summary>
        public static List<Function> FunctionList
        {
            get
            {
                if (functionList == null || functionList.Count == 0)
                {
                    int systemId, processId, id;
                    Function function;

                    functionList = new List<Function>();

                    foreach (XElement xe in XDocument.Element("transaction").Elements("systemList").Elements("system").Elements("processList").Elements("process").Elements("functionList").Elements("function"))
                    {
                        function = new Function();

                        systemId = int.Parse(xe.Parent.Parent.Parent.Parent.Attribute("id").Value);
                        id = int.Parse(xe.Parent.Parent.Attribute("id").Value);
                        processId = Process.ProcessId(systemId, id);
                        id = int.Parse(xe.Attribute("id").Value);

                        function.Process = (from q in ProcessList where q.Id == processId select q).Single();
                        function.Id = Function.FunctionId(processId, id);
                        function.Name = xe.Attribute("name").Value;

                        functionList.Add(function);
                    }
                }

                return functionList;
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
                    streamReader = new StreamReader(_assembly.GetManifestResourceStream("Ia.Ngn.Cl.model.data.transaction.xml"));

                    try
                    {
                        if (streamReader.Peek() != -1)
                        {
                            xDocument = global::System.Xml.Linq.XDocument.Load(streamReader);
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

    /*
    /// <summary/>
    public enum State
    {
        Initiated = 1, NoConnection = 2, NotSent = 3, Sent = 4, Closed = 5, Unspecified = 6
    }

    /// <summary/>
    public enum Priority
    {
        Urgent = 1, Important = 2, Regular = 3, Unspecified = 4
    }

    /// <summary/>
    public enum Recipient
    {
        AluMgc = 1, AluAms = 2, AluIms = 3 //Ftp, Hsi, HuIms, Si???, Tnd, etc
    }
     */

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
