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

namespace Ia.Ngn.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// High Speed Internet (HSI) support class for Next Generation Network (NGN) data model.
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
    public partial class Hsi
    {
        private static XDocument xDocument;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public enum IspName
        {
            // below: synch ids with XML
            QualityNet = 1, FastTelco, UnitedNetworks, KemsZajel, FastTelcoData, Sip, Moc1, Moc2, Moc3, UnitedNetworksData, QualityNetData
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public enum ProfileName
        {
            // below: synch ids with XML
            bw64Kbs = 2, bw128Kbs = 3, bw256Kbs = 4, bw512Kbs = 5, bw1024Kbs = 6, bw8192Kbs = 7, bw20Mbs = 20, bw50Mbs = 50 
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Hsi() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public struct Isp
        {
            public int Id { get; set; }
            public bool Active { get; set; }
            public int Lan { get; set; }
            public string Name { get; set; }
            public string ArabicName { get; set; }
            public string Description { get; set; }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public struct Profile
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        //////////////////////////////////////////////////////////////////////////////

        ///// <summary>
        /////
        ///// </summary>
        public static Isp IspFromIspId(int ispId)
        {
            Isp isp;

            isp = new Isp();

            foreach (Isp i in IspList)
            {
                if (i.Id == ispId)
                {
                    isp = i;
                    break;
                }
            }

            return isp;
        }

        //////////////////////////////////////////////////////////////////////////////

        ///// <summary>
        /////
        ///// </summary>
        public static Profile ProfileFromProfileId(int profileId)
        {
            Profile profile;

            profile = new Profile();

            foreach (Profile i in ProfileList)
            {
                if (i.Id == profileId)
                {
                    profile = i;
                    break;
                }
            }

            return profile;
        }

        //////////////////////////////////////////////////////////////////////////////

        ///// <summary>
        /////
        ///// </summary>
        public static List<Isp> IspList
        {
            get
            {
                Isp isp;
                List<Isp> ispList;

                ispList = new List<Isp>();

                foreach (XElement xe in XDocument.Element("hsi").Element("ispList").Elements("isp"))
                {
                    isp = new Isp();

                    isp.Id = int.Parse(xe.Attribute("id").Value);
                    isp.Active = bool.Parse(xe.Attribute("active").Value);
                    isp.Lan = int.Parse(xe.Attribute("lan").Value);
                    isp.Name = xe.Attribute("name").Value;
                    isp.ArabicName = xe.Attribute("arabicName").Value;
                    isp.Description = xe.Attribute("description").Value;

                    ispList.Add(isp);
                }

                return ispList;
            }
        }

        //////////////////////////////////////////////////////////////////////////////

        ///// <summary>
        /////
        ///// </summary>
        public static List<Profile> ProfileList
        {
            get
            {
                Profile profile;
                List<Profile> profileList;

                profileList = new List<Profile>();

                foreach (XElement xe in XDocument.Element("hsi").Element("profileList").Elements("profile"))
                {
                    profile = new Profile();

                    profile.Id = int.Parse(xe.Attribute("id").Value);
                    profile.Name = xe.Attribute("name").Value;
                    profile.Description = xe.Attribute("description").Value;

                    profileList.Add(profile);
                }

                return profileList;
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
                    streamReader = new StreamReader(_assembly.GetManifestResourceStream("Ia.Ngn.Cl.model.data.hsi.xml"));

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
