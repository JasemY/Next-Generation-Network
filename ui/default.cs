using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Configuration;

namespace Ia.Ngn.Cl.Model.Ui
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Default support class for Next Generation Network (NGN) ui model.
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
    public partial class Default
    {
        private static List<string> lightBackgroundColorList, darkBackgroundColorList;
        private static List<ColorAndSuitableBackground> colorAndSuitableBackgroundList;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string MailTop()
        {
            // <title>" + ConfigurationManager.AppSettings["siteName"].ToString() + ": " + ConfigurationManager.AppSettings["companyName"].ToString() + @"</title>
            // <p><b>" + ConfigurationManager.AppSettings["siteName"].ToString() + "</b>: <b>" + ConfigurationManager.AppSettings["companyName"].ToString() + @"</b></p>

            return @"
<!DOCTYPE html>
<html xmlns=""http://www.w3.org/1999/xhtml"" >
<head>
<title>" + ConfigurationManager.AppSettings["siteName"].ToString() + @": " + ConfigurationManager.AppSettings["companyName"].ToString() + @"</title>
<style>
html { direction:ltr; }
body { background:#fff;margin:5px;padding:0px;color:DarkBlue; }
body,td,th,a,input.button_ia,input,textarea,option,select,pre { font:9pt normal #000066 ""Tahoma"";font-family:Tahoma;text-decoration:none; }
a:link { color:#0000ff;text-decoration:none; }
a:visited { color:#0000ff;text-decoration:none; }
a:hover { color:#ff8888;text-decoration:none; }
hr { color:rgb(204,204,204); }
p { line-height:18px;margin-top:9px;margin-bottom:9px; }
table.form { }
table.form td { }
</style>
</head>
<body>
<p>
<b>" + ConfigurationManager.AppSettings["siteName"].ToString() + @"</b>: 
<b>" + ConfigurationManager.AppSettings["companyName"].ToString() + @"</b>
</p>
";
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string PlainMailTop()
        {
            return ConfigurationManager.AppSettings["siteName"].ToString() + "\r\n"
                + ConfigurationManager.AppSettings["companyName"].ToString() + "\r\n" + "\r\n";
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string MailBottom()
        {
            return @"
</body>
</html>
";
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string PlainMailBottom()
        {
            return @"";
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public struct ColorAndSuitableBackground
        {
            public string Name, Background;

            public ColorAndSuitableBackground(string name, string background)
            {
                Name = name;
                Background = background;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<string> LightBackgroundColorList
        {
            get
            {
                if (lightBackgroundColorList == null)
                {
                    lightBackgroundColorList = new List<string>(200);

                    foreach (ColorAndSuitableBackground c in ColorAndSuitableBackgroundList)
                    {
                        if (c.Background == "light") lightBackgroundColorList.Add(c.Name);
                    }
                }

                return lightBackgroundColorList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<string> DarkBackgroundColorList
        {
            get
            {
                if (darkBackgroundColorList == null)
                {
                    darkBackgroundColorList = new List<string>(200);

                    foreach (ColorAndSuitableBackground c in ColorAndSuitableBackgroundList)
                    {
                        if (c.Background == "dark") darkBackgroundColorList.Add(c.Name);
                    }
                }

                return darkBackgroundColorList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<ColorAndSuitableBackground> ColorAndSuitableBackgroundList
        {
            get
            {
                if (colorAndSuitableBackgroundList == null)
                {
                    colorAndSuitableBackgroundList = new List<ColorAndSuitableBackground>(200);

                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("AliceBlue", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("AliceBlue", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("AntiqueWhite", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Aqua", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Aquamarine", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Azure", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Beige", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Bisque", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Black", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("BlanchedAlmond", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Blue", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("BlueViolet", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Brown", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("BurlyWood", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("CadetBlue", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Chartreuse", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Chocolate", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Coral", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("CornFlowerBlue", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Cornsilk", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Crimson", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Cyan", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkBlue", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkCyan", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkGoldenrod", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkGray", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkGreen", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkKhaki", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkMagenta", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkOliveGreen", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkOrange", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkOrchid", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkRed", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkSalmon", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkSeaGreen", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkSlateBlue", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkSlateGray", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkTurquoise", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DarkViolet", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DeepPink", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DeepSkyBlue", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DimGray", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("DodgerBlue", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Firebrick", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("FloralWhite", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("ForestGreen", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Fuchsia", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Gainsboro", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("GhostWhite", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Gold", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Goldenrod", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Gray", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Green", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("GreenYellow", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Honeydew", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("HotPink", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("IndianRed", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Indigo", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Ivory", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Khaki", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Lavender", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LavenderBlush", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LawnGreen", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LemonChiffon", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LightBlue", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LightCoral", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LightCyan", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LightGoldenRodYellow", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LightGray", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LightGreen", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LightPink", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LightSalmon", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LightSeaGreen", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LightSkyBlue", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LightSlateGray", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LightSteelBlue", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LightYellow", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Lime", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("LimeGreen", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Linen", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Magenta", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Maroon", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("MediumAquamarine", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("MediumBlue", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("MediumOrchid", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("MediumPurple", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("MediumSeaGreen", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("MediumSlateBlue", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("MediumSpringGreen", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("MediumTurquoise", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("MediumVioletRed", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("MidnightBlue", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("MintCream", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("MistyRose", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Moccasin", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("NavajoWhite", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Navy", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("OldLace", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Olive", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("OliveDrab", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Orange", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("OrangeRed", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Orchid", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("PaleGoldenrod", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("PaleGreen", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("PaleTurquoise", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("PaleVioletRed", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("PapayaWhip", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("PeachPuff", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Peru", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Pink", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Plum", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("PowderBlue", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Purple", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Red", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("RosyBrown", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("RoyalBlue", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("SaddleBrown", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Salmon", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("SandyBrown", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("SeaGreen", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("SeaShell", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Sienna", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Silver", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("SkyBlue", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("SlateBlue", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("SlateGray", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Snow", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("SpringGreen", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("SteelBlue", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Tan", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Teal", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Thistle", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Tomato", "dark"));
                    //colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Transparent", "light"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Turquoise", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Violet", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Wheat", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("White", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("WhiteSmoke", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("Yellow", "dark"));
                    colorAndSuitableBackgroundList.Add(new ColorAndSuitableBackground("YellowGreen", "light"));
                }

                return colorAndSuitableBackgroundList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}