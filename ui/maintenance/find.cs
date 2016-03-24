using System;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Ia.Ngn.Cl.Model.Ui.Maintenance
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Find subscriber and network information support class for the Next Generation Network ui model
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
    public partial class Find
    {
        private static List<Ia.Ngn.Cl.Model.Service2> serviceList;
        private static List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList;
        private static List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList;
        private static List<Ia.Ngn.Cl.Model.Nokia.SubParty> subPartyList;
        private static List<Ia.Ngn.Cl.Model.Nokia.Subscriber> subscriberList;
        private static List<Ia.Ngn.Cl.Model.Huawei.HuSbr> huSbrList;
        private static List<Ia.Ngn.Cl.Model.Ont> ontList;
        private static List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> nddOntList;
        private static List<Ia.Ngn.Cl.Model.OntServiceVoip> ontServiceVoipList;
        private static List<Ia.Ngn.Cl.Model.OntOntPots> ontOntPotsList;
        private static List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList;
        private static List<Ia.Ngn.Cl.Model.Event> eventList;
        private static List<Ia.Ngn.Cl.Model.Access> accessList;
        private static List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList;
        private static List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList;
        private static List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList;
        private static List<Ia.Ngn.Cl.Model.ServiceRequestOnt> serviceRequestOntList;
        private static List<Ia.Ngn.Cl.Model.Report> reportList;
        private static List<Ia.Ngn.Cl.Model.ReportHistory> reportHistoryList;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Find() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void Search(string input,
            out List<Ia.Ngn.Cl.Model.Service2> serviceList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord> agcfGatewayRecordList,
            out List<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint> agcfEndpointList,
            out List<Ia.Ngn.Cl.Model.Nokia.SubParty> subPartyList,
            out List<Ia.Ngn.Cl.Model.Nokia.Subscriber> subscriberList,
            out List<Ia.Ngn.Cl.Model.Huawei.HuSbr> huSbrList,
            out List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> nddOntList,
            out List<Ia.Ngn.Cl.Model.Ont> ontList,
            out List<Ia.Ngn.Cl.Model.OntServiceVoip> ontServiceVoipList,
            out List<Ia.Ngn.Cl.Model.OntOntPots> ontOntPotsList,
            out List<Ia.Ngn.Cl.Model.OntServiceHsi> ontServiceHsiList,
            out List<Ia.Ngn.Cl.Model.Event> eventList,
            out List<Ia.Ngn.Cl.Model.Access> accessList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList,
            out List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestType> serviceRequestTypeList,
            out List<Ia.Ngn.Cl.Model.ServiceRequestOnt> serviceRequestOntList,
            out List<Ia.Ngn.Cl.Model.Report> reportList,
            out List<Ia.Ngn.Cl.Model.ReportHistory> reportHistoryList,
            out Ia.Cl.Model.Result result)
        {
            bool inputIsValid;

            inputIsValid = false;
            result = null;

            serviceList = null;
            agcfGatewayRecordList = null;
            agcfEndpointList = null;
            subPartyList = null;
            subscriberList = null;
            huSbrList = null;
            nddOntList = null;
            ontList = null;
            ontServiceVoipList = null;
            ontOntPotsList = null;
            ontServiceHsiList = null;
            eventList = null;
            accessList = null;
            serviceRequestServiceList = null;
            serviceRequestList = null;
            serviceRequestTypeList = null;
            serviceRequestOntList = null;
            reportList = null;
            reportHistoryList = null;

            result = new Ia.Cl.Model.Result();

            if (!string.IsNullOrEmpty(input))
            {
                input = input.Trim();

                // below: space characters from start and end
                input = Regex.Replace(input, @"^\s+", "");
                input = Regex.Replace(input, @"\s+$", "");
                input = input.ToUpper();

                if (input.Length > 0)
                {
                    inputIsValid = true;

                    if (Regex.IsMatch(input, @"^\d{8}$") || Regex.IsMatch(input, @"^" + Ia.Ngn.Cl.Model.Data.Service.CountryCode + @"\d{8}$"))
                    {
                        Ia.Ngn.Cl.Model.Business.Maintenance.Find.Number(input, out serviceList, out agcfGatewayRecordList, out agcfEndpointList, out subPartyList, out subscriberList, out huSbrList, out nddOntList, out ontList, out ontServiceVoipList, out ontOntPotsList, out ontServiceHsiList, out eventList, out accessList, out serviceRequestServiceList, out serviceRequestList, out serviceRequestTypeList, out serviceRequestOntList, out reportList, out reportHistoryList, out result);
                    }
                    else if (Regex.IsMatch(input, @"^\w{3}\s+\d{1,3}\s+\d{1,3}$") || Regex.IsMatch(input, @"^\w{3}\.\d{1,3}\.\d{1,3}$") || Regex.IsMatch(input, @"^\w{3}\/\d{1,3}\/\d{1,3}$"))
                    {
                        Ia.Ngn.Cl.Model.Business.Maintenance.Find.OntName(input, out serviceList, out agcfGatewayRecordList, out agcfEndpointList, out subPartyList, out subscriberList, out huSbrList, out nddOntList, out ontList, out ontServiceVoipList, out ontOntPotsList, out ontServiceHsiList, out eventList, out accessList, out serviceRequestServiceList, out serviceRequestList, out serviceRequestTypeList, out serviceRequestOntList, out reportList, out reportHistoryList, out result);
                    }
                    else if (Regex.IsMatch(input, @"^ALCL.{8}$")) // or the Huawei/Nokia-Siemens ONT serial
                    {
                        Ia.Ngn.Cl.Model.Business.Maintenance.Find.Serial(input, out serviceList, out agcfGatewayRecordList, out agcfEndpointList, out subPartyList, out subscriberList, out huSbrList, out nddOntList, out ontList, out ontServiceVoipList, out ontOntPotsList, out ontServiceHsiList, out eventList, out accessList, out serviceRequestServiceList, out serviceRequestList, out serviceRequestTypeList, out serviceRequestOntList, out reportList, out reportHistoryList, out result);
                    }
                    else if (Regex.IsMatch(input, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$"))
                    {
                        Ia.Ngn.Cl.Model.Business.Maintenance.Find.Ip(input, out serviceList, out agcfGatewayRecordList, out agcfEndpointList, out subPartyList, out subscriberList, out huSbrList, out nddOntList, out ontList, out ontServiceVoipList, out ontOntPotsList, out ontServiceHsiList, out eventList, out accessList, out serviceRequestServiceList, out serviceRequestList, out serviceRequestTypeList, out serviceRequestOntList, out reportList, out reportHistoryList, out result);
                    }
                    else if (Regex.IsMatch(input, @"^\w{3}\s+\d{1,3}$") || Regex.IsMatch(input, @"^\w{3}\.\d{1,3}$") || Regex.IsMatch(input, @"^\w{3}\/\d{1,3}$"))
                    {
                        Ia.Ngn.Cl.Model.Business.Maintenance.Find.PonName(input, out serviceList, out agcfGatewayRecordList, out agcfEndpointList, out subPartyList, out subscriberList, out huSbrList, out nddOntList, out ontList, out ontServiceVoipList, out ontOntPotsList, out ontServiceHsiList, out eventList, out accessList, out serviceRequestServiceList, out serviceRequestList, out serviceRequestTypeList, out serviceRequestOntList, out reportList, out reportHistoryList, out result);
                    }
                    else if (Regex.IsMatch(input, @"^(\w{3})-\d{1,2}-\d{1,2}-\d{1,2}-\d{1,2}-\d{1,2}$"))
                    {
                        Ia.Ngn.Cl.Model.Business.Maintenance.Find.OntPosition(input, out serviceList, out agcfGatewayRecordList, out agcfEndpointList, out subPartyList, out subscriberList, out huSbrList, out nddOntList, out ontList, out ontServiceVoipList, out ontOntPotsList, out ontServiceHsiList, out eventList, out accessList, out serviceRequestServiceList, out serviceRequestList, out serviceRequestTypeList, out serviceRequestOntList, out reportList, out reportHistoryList, out result);
                    }
                    //else if (Regex.IsMatch(input, @"^(\w{3})-\d{1,2}-\d{1,2}-\d{1,2}-\d{1,2}$")) Ont_Position_Pon(input);
                    else if (Regex.IsMatch(input, @"^\w+,\d{1,3},\d{0,3},\d{0,3},\d{0,3}$"))
                    {
                        Ia.Ngn.Cl.Model.Business.Maintenance.Find.Address(input, out serviceList, out agcfGatewayRecordList, out agcfEndpointList, out subPartyList, out subscriberList, out huSbrList, out nddOntList, out ontList, out ontServiceVoipList, out ontOntPotsList, out ontServiceHsiList, out eventList, out accessList, out serviceRequestServiceList, out serviceRequestList, out serviceRequestTypeList, out serviceRequestOntList, out reportList, out reportHistoryList, out result);
                    }
                    else if (Regex.IsMatch(input, @"^\d{1,7}$"))
                    {
                        Ia.Ngn.Cl.Model.Business.Maintenance.Find.GatewayId(input, out serviceList, out agcfGatewayRecordList, out agcfEndpointList, out subPartyList, out subscriberList, out huSbrList, out nddOntList, out ontList, out ontServiceVoipList, out ontOntPotsList, out ontServiceHsiList, out eventList, out accessList, out serviceRequestServiceList, out serviceRequestList, out serviceRequestTypeList, out serviceRequestOntList, out reportList, out reportHistoryList, out result);
                    }
                    else if (Regex.IsMatch(input, @"^[\w|\s]+$"))
                    {
                        Ia.Ngn.Cl.Model.Business.Maintenance.Find.CustomerName(input, out serviceList, out agcfGatewayRecordList, out agcfEndpointList, out subPartyList, out subscriberList, out huSbrList, out nddOntList, out ontList, out ontServiceVoipList, out ontOntPotsList, out ontServiceHsiList, out eventList, out accessList, out serviceRequestServiceList, out serviceRequestList, out serviceRequestTypeList, out serviceRequestOntList, out reportList, out reportHistoryList, out result);
                    }
                    else inputIsValid = false;

                    if (inputIsValid)
                    {
                        if (
                        (serviceList == null || serviceList != null && serviceList.Count == 0) &&
                        (agcfGatewayRecordList == null || agcfGatewayRecordList != null && agcfGatewayRecordList.Count == 0) &&
                        (agcfEndpointList == null || agcfEndpointList != null && agcfEndpointList.Count == 0) &&
                        (subPartyList == null || subPartyList != null && subPartyList.Count == 0) &&
                        (subscriberList == null || subscriberList != null && subscriberList.Count == 0) &&
                        (huSbrList == null || huSbrList != null && huSbrList.Count == 0) &&
                        (nddOntList == null || nddOntList != null && nddOntList.Count == 0) &&
                        (ontList == null || ontList != null && ontList.Count == 0) &&
                        (ontServiceVoipList == null || ontServiceVoipList != null && ontServiceVoipList.Count == 0) &&
                        (ontOntPotsList == null || ontOntPotsList != null && ontOntPotsList.Count == 0) &&
                        (ontServiceHsiList == null || ontServiceHsiList != null && ontServiceHsiList.Count == 0) &&
                        (eventList == null || eventList != null && eventList.Count == 0) &&
                        (accessList == null || accessList != null && accessList.Count == 0) &&
                        (serviceRequestServiceList == null || serviceRequestServiceList != null && serviceRequestServiceList.Count == 0) &&
                        (serviceRequestList == null || serviceRequestList != null && serviceRequestList.Count == 0) &&
                        (serviceRequestTypeList == null || serviceRequestTypeList != null && serviceRequestTypeList.Count == 0) &&
                        (serviceRequestOntList == null || serviceRequestOntList != null && serviceRequestOntList.Count == 0) &&
                        (reportList == null || reportList != null && reportList.Count == 0) &&
                        (reportHistoryList == null || reportHistoryList != null && reportHistoryList.Count == 0))
                        {
                            if (result.IsSuccessful)
                            {
                                result.AddWarning("No data records were found for \"" + input + "\" (لا توجد معلومات عن القيمة المعطاة). ");
                            }
                            else result.AddError(result.Error);
                        }
                        else
                        {
                            if (result.IsSuccessful)
                            {
                                if (!string.IsNullOrEmpty(result.Message)) result.Message = "Success: " + result.Message;
                            }
                            //else result.AddError(result.Error);
                        }
                    }
                    else result.AddError("Input format is unknown (قيمة غير مفهومة). ");
                }
                else result.AddError("No input was entered (لم يتم إدخال أي شيء). ");
            }
            else result.AddError("Input is null or empty (لم يتم إدخال أي شيء). ");
        }

        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool SearchForMail(string name, string email, string input, out string response)
        {
            bool b;
            string content, subject;
            DateTime now;
            DataTable serviceListDataTable, agcfGatewayRecordListDataTable, agcfEndpointListDataTable, subPartyListDataTable, subscriberListDataTable, huSbrListDataTable, nddOntListDataTable, ontListDataTable, ontServiceVoipListDataTable, ontOntPotsListDataTable, ontServiceHsiListDataTable, eventListDataTable, accessListDataTable, serviceRequestServiceListDataTable, serviceRequestListDataTable, serviceRequestTypeListDataTable, serviceRequestOntListDataTable;
            Ia.Cl.Model.Result result;

            result = null;

            now = DateTime.UtcNow.AddHours(3);

            // subject can't have \r\n
            subject = "Optical Fiber Network (OFN) Find Result (" + now.ToString("yyyy-MM-dd HH:mm") + ")";

            content = "Optical Fiber Network (OFN) Find Result: " + now.ToString("yyyy-MM-dd HH:mm") + "\r\n";

            content += @"Find input: """ + input + @"""." + "\r\n";
            content += "\r\n";
            content += "\r\n";

            input = input.ToLower();
            input = input.Replace("find", "");

            //input = Server.HtmlEncode(input);

            Ia.Ngn.Cl.Model.Ui.Maintenance.Find.Search(input, out serviceList, out agcfGatewayRecordList, out agcfEndpointList, out subPartyList, out subscriberList, out huSbrList, out nddOntList, out ontList, out ontServiceVoipList, out ontOntPotsList, out ontServiceHsiList, out eventList, out accessList, out serviceRequestServiceList, out serviceRequestList, out serviceRequestTypeList, out serviceRequestOntList, out reportList, out reportHistoryList, out result);

            if (result.IsSuccessful)
            {
                serviceListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.Service2>(serviceList);
                agcfGatewayRecordListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.Nokia.AgcfGatewayRecord>(agcfGatewayRecordList);
                agcfEndpointListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.Nokia.AgcfEndpoint>(agcfEndpointList);
                subPartyListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.Nokia.SubParty>(subPartyList);
                subscriberListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.Nokia.Subscriber>(subscriberList);
                huSbrListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.Huawei.HuSbr>(huSbrList);
                nddOntListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont>(nddOntList);
                ontListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.Ont>(ontList);
                ontServiceVoipListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.OntServiceVoip>(ontServiceVoipList);
                ontOntPotsListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.OntOntPots>(ontOntPotsList);
                ontServiceHsiListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.OntServiceHsi>(ontServiceHsiList);
                if (eventList != null) eventListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.Event>(eventList); // .OrderByDescending(ev => ev.EventTime)
                else eventListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.Event>(eventList);
                accessListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.Access>(accessList);
                serviceRequestServiceListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.ServiceRequestService>(serviceRequestServiceList);
                serviceRequestListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.ServiceRequest>(serviceRequestList);
                serviceRequestTypeListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.ServiceRequestType>(serviceRequestTypeList);
                serviceRequestOntListDataTable = Ia.Cl.Model.Default.GenerateDataTableFromListOfGenericClass<Ia.Ngn.Cl.Model.ServiceRequestOnt>(serviceRequestOntList);

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(serviceListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(agcfGatewayRecordListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(agcfEndpointListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(subPartyListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(subscriberListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(huSbrListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(nddOntListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(ontListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(ontServiceVoipListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(ontOntPotsListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(ontServiceHsiListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(eventListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(accessListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(serviceRequestServiceListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(serviceRequestListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(serviceRequestTypeListDataTable);
                content += "\r\n";
                content += "\r\n";

                content += Ia.Cl.Model.Default.GenerateTabSeparatedTextFromDataTable(serviceRequestOntListDataTable);
                content += "\r\n";
                content += "\r\n";

                if (result.HasWarning)
                {
                    content += result.Message + result.Warning;
                }
                else
                {
                    content += result.Message;
                }
            }
            else
            {
                content += result.Error;
            }

            content += "\r\n";
            content += "\r\n";
            content += @"Help? Send email with subject ""maintenance/find {23632222}"". ";
            content += "\r\n";
            content += "\r\n";

            b = Ia.Ngn.Cl.Model.Ui.Mail.SendPlainMail(name, email, subject, content, out response);

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string OntNameForTwitter(string input, out Ia.Cl.Model.Result result)
        {
            // below:
            string s, response;
            List<Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont> nddOntList;
            List<Ia.Ngn.Cl.Model.Ont> ontList;

            result = null;

            Ia.Ngn.Cl.Model.Business.Maintenance.Find.OntName(input, out nddOntList, out ontList, out result);

            if (
                (nddOntList == null || nddOntList != null && nddOntList.Count == 0) &&
                (ontList == null || ontList != null && ontList.Count == 0)
                )
            {
                response = "No data records were found for \"" + input + "\" (لا توجد معلومات عن القيمة المعطاة). ";
            }
            else
            {
                s = "";

                foreach (Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.Ont ont in nddOntList)
                {
                    s += ont.Access.Name + ", " + ont.Ip + ", " + ont.Position + ", " + ont.Position + "\r\n\r\n";
                }

                foreach (Ia.Ngn.Cl.Model.Ont ont in ontList)
                {
                    s += ont.StateId + ", " + ont.Serial + ", " /*+ ont..Position + ", " + ont.Position*/ + "\r\n\r\n";
                }

                response = s;
            }

            if (result.IsSuccessful)
            {
            }
            else
            {
                response = result.Error;
            }

            return response;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
