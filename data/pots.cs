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

namespace Ia.Ngn.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// POTS legacy support class for Next Generation Network (NGN) data model.
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
    public partial class Pots
    {
        private static XDocument xDocument;
        private static List<Category> categoryList;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Pots() { }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public class Category
        {
            public Category() { }

            public int Id { get; set; }
            public string Name { get; set; }
            public string ArabicName { get; set; }
            /*
            public virtual ICollection<Area> Areas
            {
                get
                {
                    return (from q in AreaList where q.Category.Id == this.Id select q).ToList();
                }
            }
             */ 
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.Data.Pots.Category> CategoryList
        {
            get
            {
                if (categoryList == null || categoryList.Count == 0)
                {
                    int id;
                    Category category;

                    categoryList = new List<Category>();

                    foreach (XElement x in XDocument.Element("report").Elements("category"))
                    {
                        category = new Category();

                        id = int.Parse(x.Attribute("id").Value);

                        category.Id = id;
                        category.Name = x.Attribute("name").Value;
                        category.ArabicName = (x.Attribute("arabicName") != null) ? x.Attribute("arabicName").Value : string.Empty;

                        categoryList.Add(category);
                    }
                }

                return categoryList;
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////
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

        public static XDocument XDocument
        {
            get
            {
                Assembly _assembly;
                StreamReader streamReader;

                if (xDocument == null)
                {
                    _assembly = Assembly.GetExecutingAssembly();
                    streamReader = new StreamReader(_assembly.GetManifestResourceStream("Ia.Ngn.Cl.model.data.pots.xml"));

                    try
                    {
                        if (streamReader.Peek() != -1) xDocument = System.Xml.Linq.XDocument.Load(streamReader);
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
