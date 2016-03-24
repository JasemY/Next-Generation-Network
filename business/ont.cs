using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text.RegularExpressions;

namespace Ia.Ngn.Cl.Model.Business
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// ONT support class of Next Generation Network'a (NGN's) business model.
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
    public partial class Ont
    {
        /// <summary/>
        public enum FamilyType { Undefined = 0, Sfu = 1, Soho = 2, Mdu = 3 };

        /// <summary/>
        public Ont() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string OntId(long oltId, int rack, int sub, int card, int port, int ontNumber)
        {
            string id;

            id = oltId.ToString() + rack.ToString().PadLeft(2, '0') + sub.ToString().PadLeft(2, '0') + card.ToString().PadLeft(2, '0') + port.ToString().PadLeft(2, '0') + ontNumber.ToString().PadLeft(2, '0');

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string OntId(string amsName, string cardPortOntSequence)
        {
            int oltId, rack, sub, card, port, ont;
            string id;
            string ontPosition;

            ontPosition = Ia.Ngn.Cl.Model.Business.Nokia.Ams.OntPositionFromAmsNameAndCardPortOntSquence(amsName, cardPortOntSequence);

            Ia.Ngn.Cl.Model.Business.Nokia.Ams.OltIdRackSubCardPortOntFromOntPosition(ontPosition, out oltId, out rack, out sub, out card, out port, out ont);

            id = Ia.Ngn.Cl.Model.Business.Ont.OntId(oltId, rack, sub, card, port, ont);

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void OltIdRackSubCardPortOntFromOntId(string ontId, out int oltId, out int rack, out int sub, out int card, out int port, out int ont)
        {
            string rest;

            oltId = int.Parse(ontId.Substring(0, 1 + 2 + 2 + 2 + 2)); // network id, site id, router id, odf id, olt id

            rest = Regex.Replace(ontId, "^" + oltId.ToString(), "");

            rack = int.Parse(rest.Substring(0, 2));
            sub = int.Parse(rest.Substring(2, 2));
            card = int.Parse(rest.Substring(4, 2));
            port = int.Parse(rest.Substring(6, 2));
            ont = int.Parse(rest.Substring(8, 2));
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string OntPositionFromOntId(string ontId)
        {
            string rest, ontPosition, amsName;
            int oltId, card, port, ont;

            oltId = int.Parse(ontId.Substring(0, 1 + 2 + 2 + 2 + 2)); // network id, site id, router id, odf id, olt id

            rest = Regex.Replace(ontId, "^" + oltId.ToString(), "");

            card = int.Parse(rest.Substring(4, 2));
            port = int.Parse(rest.Substring(6, 2));
            ont = int.Parse(rest.Substring(8, 2));

            amsName = Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.AmsNameFromOltId(oltId);

            ontPosition = amsName + "-" + card + "-" + port + "-" + ont;

            return ontPosition;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string ExtractOntNameWithValidSymbolAndLegalFormatForPonAndOntFromValue(string value)
        {
            bool isValid;
            int oltId, ponNumber, ontNumber;
            string ontName;
            Dictionary<int, string> typeDictionary;

            typeDictionary = new Dictionary<int, string>(1);
            typeDictionary.Add(1, value);

            isValid = ExtractOntNameWithValidSymbolAndLegalFormatForPonAndOntFromDictionaryValueList(typeDictionary, out oltId, out ponNumber, out ontNumber);

            if (isValid) ontName = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList where q.Id == oltId select q.Symbol).SingleOrDefault() + "." + ponNumber + "." + ontNumber;
            else ontName = string.Empty;

            return ontName;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string ExtractPonNameWithValidSymbolAndLegalFormatForPonAndOntFromValue(string value)
        {
            bool isValid;
            int oltId, ponNumber, ontNumber;
            string ontName;
            Dictionary<int, string> typeDictionary;

            // below: we will replace ' ' and '/' with '.' to avoid errors
            value = value.Replace(" ", ".");
            value = value.Replace("/", ".");

            typeDictionary = new Dictionary<int, string>(1);
            typeDictionary.Add(1, value + ".1"); // to mimic a WWW.D.1 format

            isValid = ExtractOntNameWithValidSymbolAndLegalFormatForPonAndOntFromDictionaryValueList(typeDictionary, out oltId, out ponNumber, out ontNumber);

            if (isValid) ontName = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OltList where q.Id == oltId select q.Symbol).SingleOrDefault() + "." + ponNumber; // + "." + ontNumber;
            else ontName = string.Empty;

            return ontName;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string StandardizeOntNameInLegalFormatFromValue(string value)
        {
            string ontName;
            Dictionary<int, string> typeDictionary;

            typeDictionary = new Dictionary<int, string>(1);
            typeDictionary.Add(1, value);

            ontName = ExtractOntNameInLegalFormatFromDictionaryValueList(typeDictionary);

            return ontName;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool ExtractOntNameWithValidSymbolAndLegalFormatForPonAndOntFromValue(string value, out int oltId, out int ponNumber, out int ontNumber)
        {
            Dictionary<int, string> typeDictionary;

            typeDictionary = new Dictionary<int, string>(1);
            typeDictionary.Add(1, value);

            return ExtractOntNameWithValidSymbolAndLegalFormatForPonAndOntFromDictionaryValueList(typeDictionary, out oltId, out ponNumber, out ontNumber);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool ExtractOntNameWithValidSymbolAndLegalFormatForPonAndOntFromDictionaryValueList(Dictionary<int, string> typeDictionary, out int oltId, out int ponNumber, out int ontNumber)
        {
            bool b, isValidOntNameWithValidSymbolAndLegalFormatForPonAndOnt;
            int p, o;
            string p1, p2, p3, p4, p5, p45, symbol;
            Match match;

            b = isValidOntNameWithValidSymbolAndLegalFormatForPonAndOnt = false;
            oltId = ponNumber = ontNumber = 0;

            p1 = (typeDictionary.ContainsKey(1)) ? typeDictionary[1] : "";
            p2 = (typeDictionary.ContainsKey(2)) ? typeDictionary[2] : "";
            p3 = (typeDictionary.ContainsKey(3)) ? typeDictionary[3] : "";
            p4 = (typeDictionary.ContainsKey(4)) ? typeDictionary[4] : "";
            p5 = (typeDictionary.ContainsKey(5)) ? typeDictionary[5] : "";
            p45 = (typeDictionary.ContainsKey(45)) ? typeDictionary[45] : "";

            b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromString(p2, out match);

            if (!b)
            {
                b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromString(p1, out match);

                if (!b)
                {
                    b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromTwoStrings(p1, p2, out match);

                    if (!b)
                    {
                        b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromTwoStrings(p1, p45, out match);

                        if (!b)
                        {
                            b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromTwoStrings(p2, p45, out match);

                            if (!b)
                            {
                                b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromTwoStrings(p1, p4, out match);

                                if (!b)
                                {
                                    b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromString(p4, out match);

                                    if (!b)
                                    {
                                        b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromString(p5, out match);

                                        if (!b)
                                        {
                                            b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromString(p3, out match);

                                            if (!b)
                                            {
                                                b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromTwoStrings(p1, p3, out match);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (b)
            {
                if (match.Success)
                {
                    symbol = match.Groups[1].Captures[0].Value;
                    ponNumber = p = int.Parse(match.Groups[2].Captures[0].Value);
                    ontNumber = o = int.Parse(match.Groups[3].Captures[0].Value);

                    oltId = (from q in Ia.Ngn.Cl.Model.Data.NetworkDesignDocument.OntList where q.Pon.Olt.Symbol == symbol && q.Pon.Number == p && q.Number == o select q.Pon.Olt.Id).SingleOrDefault();

                    if (oltId != 0)
                    {
                        isValidOntNameWithValidSymbolAndLegalFormatForPonAndOnt = true;
                    }
                    else
                    {
                        oltId = ponNumber = ontNumber = 0;
                        isValidOntNameWithValidSymbolAndLegalFormatForPonAndOnt = false;
                    }
                }
                else
                {
                    oltId = ponNumber = ontNumber = 0;
                    isValidOntNameWithValidSymbolAndLegalFormatForPonAndOnt = false;
                }
            }
            else isValidOntNameWithValidSymbolAndLegalFormatForPonAndOnt = false;

            return isValidOntNameWithValidSymbolAndLegalFormatForPonAndOnt;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string ExtractOntNameInLegalFormatFromDictionaryValueList(Dictionary<int, string> typeDictionary)
        {
            bool b;
            int ponNumber, ontNumber;
            string p1, p2, p3, p4, p5, p45, symbol, ontName;
            Match match;

            p1 = (typeDictionary.ContainsKey(1)) ? typeDictionary[1] : "";
            p2 = (typeDictionary.ContainsKey(2)) ? typeDictionary[2] : "";
            p3 = (typeDictionary.ContainsKey(3)) ? typeDictionary[3] : "";
            p4 = (typeDictionary.ContainsKey(4)) ? typeDictionary[4] : "";
            p5 = (typeDictionary.ContainsKey(5)) ? typeDictionary[5] : "";
            p45 = (typeDictionary.ContainsKey(45)) ? typeDictionary[45] : "";

            b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromString(p2, out match);

            if (!b)
            {
                b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromString(p1, out match);

                if (!b)
                {
                    b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromTwoStrings(p1, p2, out match);

                    if (!b)
                    {
                        b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromTwoStrings(p1, p45, out match);

                        if (!b)
                        {
                            b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromTwoStrings(p2, p45, out match);

                            if (!b)
                            {
                                b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromTwoStrings(p1, p4, out match);

                                if (!b)
                                {
                                    b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromString(p4, out match);

                                    if (!b)
                                    {
                                        b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromString(p5, out match);

                                        if (!b)
                                        {
                                            b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromString(p3, out match);

                                            if (!b)
                                            {
                                                b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromTwoStrings(p1, p3, out match);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (b)
            {
                if (match.Success)
                {
                    symbol = match.Groups[1].Captures[0].Value;

                    ponNumber = int.Parse(match.Groups[2].Captures[0].Value);
                    ontNumber = int.Parse(match.Groups[3].Captures[0].Value);

                    ontName = symbol.ToUpper() + "." + ponNumber + "." + ontNumber;
                }
                else
                {
                    ontName = string.Empty;
                }
            }
            else
            {
                ontName = string.Empty;
            }

            return ontName;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static bool ExtractOntNameWithLegalFormatForPonAndOntMatchFromString(string s, out Match match)
        {
            // below: this checks if string conferms
            bool b;
            match = null;

            s = s.Replace("-", ".");
            s = s.Replace("/", ".");
            s = s.ToUpper();
            //s = Regex.Replace(s, @"SL[^AW]", @"SLA");
            //s = s.Replace("SAL", "SLA");
            //s = s.Replace("JSA", "SJA");
            //s = s.Replace("SHA", "SJA");
            //s = s.Replace("O", "0");

            s = Regex.Replace(s, @"\.$", @"");

            s = Regex.Replace(s, @"(\w{3})(\d{1,3})\.(\d{1,3})", @"$1.$2.$3");
            s = Regex.Replace(s, @"(\w{3})\.(\d{3})(\d{3})", @"$1.$2.$3");
            s = Regex.Replace(s, @"(\w{3})(\d{3})(\d{3})", @"$1.$2.$3");
            s = Regex.Replace(s, @"(\w{3})\.0(\d{1,3})\.(\d{1,3})", @"$1.$2.$3");
            s = Regex.Replace(s, @"(\w{3}) (\d{1,3}) (\d{1,3})", @"$1.$2.$3");

            match = Regex.Match(s, @"(\w{3})\.(\d{1,3})\.(\d{1,3})");

            if (match.Success) b = true;
            else b = false;

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static bool ExtractOntNameWithLegalFormatForPonAndOntMatchFromTwoStrings(string s1, string s2, out Match match)
        {
            // below: this checks if string conferms
            bool b;
            match = null;

            s1 = s1.Replace("-", ".");
            s1 = s1.Replace("/", ".");
            s1 = s1.ToUpper();
            //s1 = Regex.Replace(s1, @"SL[^AW]", @"SLA");
            //s1 = s1.Replace("O", "0");
            //s1 = Regex.Replace(s1, @"\.$", @"");
            s1 = Regex.Replace(s1, @"(\w{3})(\d{1,3})", @"$1.$2");

            if (Regex.IsMatch(s1, @"\w{3}\.\d{1,3}") && Regex.IsMatch(s2, @"\d{1,3}"))
            {
                b = ExtractOntNameWithLegalFormatForPonAndOntMatchFromString(s1 + @"." + s2, out match);
            }
            else b = false;

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}