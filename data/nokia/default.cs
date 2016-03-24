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

namespace Ia.Ngn.Cl.Model.Data.Nokia
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Defaul general support class for Nokia's Next Generation Network (NGN) data model.
    /// </summary>
    /// 
    /// <remarks> 
    /// Copyright © 2014-2015 Jasem Y. Al-Shamlan (info@ia.com.kw), Internet Applications - Kuwait. All Rights Reserved.
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
    public partial class Default
    {
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Default() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Ui.Nokia.AlInitialInstallation> NokiaCustomerInitialInstallationData
        {
            get
            {
                bool b;
                int areaId, i, ontNumber, ponNumber;
                long oltId;
                string desktopDirectory, outLine, /*filePath,*/ symbol, accessId;
                string[] fileLines;
                StringBuilder sb;
                ArrayList fileList, keyList;
                Match match;
                Ia.Ngn.Cl.Model.Ui.Nokia.AlInitialInstallation alInitialInstallation;
                List<Ia.Ngn.Cl.Model.Ui.Nokia.AlInitialInstallation> alInitialInstallationList;

                outLine = "";
                desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                fileList = new ArrayList(1000);
                keyList = new ArrayList(100000);

                alInitialInstallationList = new List<Ia.Ngn.Cl.Model.Ui.Nokia.AlInitialInstallation>();

                int[] ip;
                int[] Halifa = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                int[] Fahaheel = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                int[] Mubarak = { -1, 2, 3, 4, 5, 6, 7, 8, 9, -1, 10, 11, 12, 13 };
                int[] Naseem = { -1, 1, 2, 3, 4, 5, 6, 7, 8, -1, 9, 10, 11, 12 };
                int[] Mangaf = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
                int[] Qurain = { -1, 2, 3, 4, 5, 6, 7, 8, 9, -1, 10, 11, 12, 13 };
                int[] Sabahiya = { -1, 2, 3, 4, 5, 6, 7, 8, 9, -1, 10, 11, 12, 13 };
                int[] Waha = { -1, 1, 2, 3, 4, 5, 6, 7, 8, 9, -1, -1, 10, 11 };

                // below: read all files withing the directory add txt files that have the "ADD COMMAND" in the path
                foreach (string fileName in Directory.GetFiles(@"C:\Users\Jasem\Documents\Visual Studio 2015\Projects\Next Generation Network\Documents\Nokia\Customer Data", "*.*", SearchOption.AllDirectories))
                {
                    if (fileName.Contains("Customer Data") && fileName.Contains(".csv")) fileList.Add(fileName);
                }

                fileList.Sort();

                foreach (string fileName in fileList)
                {
                    //if (fileName.Contains("Halifa")) { ip = Halifa; areaId = 20206; symbol = "ABH"; }
                    //else if (fileName.Contains("Fahaheel")) { ip = Fahaheel; areaId = 20222; symbol = "FHH"; }
                    /*else*/
                    if (fileName.Contains("Mubarak")) { ip = Mubarak; areaId = 50502; symbol = "MUB"; }
                    //else if (fileName.Contains("Naseem")) { ip = Naseem; areaId = 40406; symbol = "NAS"; }
                    //else if (fileName.Contains("Mangaf")) { ip = Mangaf; areaId = 20215; symbol = "MGF"; }
                    else if (fileName.Contains("Qurain")) { ip = Qurain; areaId = 50507; symbol = "QRN"; }
                    //else if (fileName.Contains("Sabahiya")) { ip = Sabahiya; areaId = 50501; symbol = "SSB"; }
                    //else if (fileName.Contains("Waha")) { ip = Waha; areaId = 40408; symbol = "WAH"; }
                    else { ip = null; areaId = 0; symbol = ""; }

                    if (ip != null)
                    {
                        sb = new StringBuilder(1000000); // 1M

                        sb.AppendLine(@"/* File: */");

                        fileLines = File.ReadAllLines(fileName);

                        foreach (string line in fileLines)
                        {
                            {
                                // below:
                                match = Regex.Match(line, @".*?,(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),");
                                // S.No.,Premises Sl.No/PON,OWNER NAME,Civil ID No.,Plot No.,Building No,Contact No.,Block No,PON No,Street No,Type of Building,Type of ONT,ONT Serial No,ONT ID No.,Remarks
                                // 6,1,Abdul Ali Nasser,,37,77,99825049 23717567,1,2,9,APT,MDU,ALCLA0A6DFAA,1,

                                if (match.Success)
                                {
                                    if (line.Length > 20)
                                    {
                                        alInitialInstallation = new Ia.Ngn.Cl.Model.Ui.Nokia.AlInitialInstallation();
                                        // PremisesSlPon,OwnerName, CivilId, Plot, Building, Contact, Block, Pon, Street, BuildingType, OntType, OntSerial, OntId, Remark, 

                                        try
                                        {
                                            b = int.TryParse(match.Groups[ip[7]].Value, out ponNumber);

                                            if (b)
                                            {
                                                oltId = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.PonList where q.Number == ponNumber && q.Olt.Symbol == symbol select q.Olt.Id).SingleOrDefault();
                                            }
                                            else oltId = 0;

                                            i = 0;
                                            int.TryParse(match.Groups[ip[0]].Value, out i);

                                            alInitialInstallation.PremisesSlPon = i.ToString();
                                            alInitialInstallation.OwnerName = match.Groups[ip[1]].Value;
                                            alInitialInstallation.CivilId = match.Groups[ip[2]].Value;

                                            alInitialInstallation.Plot = int.TryParse(match.Groups[ip[3]].Value, out i) ? i.ToString() : match.Groups[ip[3]].Value;
                                            alInitialInstallation.Building = int.TryParse(match.Groups[ip[4]].Value, out i) ? i.ToString() : match.Groups[ip[4]].Value;
                                            alInitialInstallation.Contact = match.Groups[ip[5]].Value;
                                            alInitialInstallation.Block = int.TryParse(match.Groups[ip[6]].Value, out i) ? i.ToString() : match.Groups[ip[6]].Value;
                                            alInitialInstallation.Pon = int.TryParse(match.Groups[ip[7]].Value, out i) ? i.ToString() : match.Groups[ip[7]].Value;
                                            alInitialInstallation.Street = int.TryParse(match.Groups[ip[8]].Value, out i) ? i.ToString() : match.Groups[ip[8]].Value;
                                            alInitialInstallation.OntType = match.Groups[ip[10]].Value;
                                            alInitialInstallation.OntSerial = match.Groups[ip[11]].Value;
                                            alInitialInstallation.OntNumber = int.TryParse(match.Groups[ip[12]].Value, out i) ? i.ToString() : match.Groups[ip[12]].Value;

                                            b = int.TryParse(match.Groups[ip[7]].Value, out ponNumber);

                                            if (b)
                                            {
                                                b = int.TryParse(match.Groups[ip[12]].Value, out ontNumber);

                                                if (b)
                                                {
                                                    accessId = Ia.Ngn.Cl.Model.Access.AccessId((int)oltId, ponNumber, ontNumber);

                                                    alInitialInstallation.Remark = accessId.ToString();

                                                    alInitialInstallation.AccessId = accessId.ToString();
                                                }
                                                else alInitialInstallation.Remark = "0";
                                            }
                                            else alInitialInstallation.Remark = "0";

                                            alInitialInstallation.BuildingType = oltId.ToString();
                                            alInitialInstallation.PremisesSlPon = areaId.ToString(); /* match.Groups[ip[0]].Value;*/

                                            alInitialInstallationList.Add(alInitialInstallation);

                                            sb.AppendLine(outLine);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("Exception: " + ex);
                                        }
                                    }
                                }
                            }
                        }

                        //filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\nokia-initial-installation-" + fileCounter + ".sql";
                        //File.WriteAllText(filePath, sb.ToString());
                    }
                }

                // <site id="2" name="QRN" arabicName="قرين" areaSymbolList="Mubarak Al Kabeer,Qurain,Sabah Al Salem,Adan,Qusoor">
                //List<Ia.Ngn.Cl.Model.Ui.ServiceCustomerAddressAccessOntNameStatisticalOntName> rrr;
                //var v = Ia.Ngn.Cl.Model.Data.Default.DifferentOntNameAndStatisticalOntNameList(102).OrderBy(o => o.OntName);

                return alInitialInstallationList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////   
}
