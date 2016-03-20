using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model.Business.Huawei
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Optical Fiber Network Management Intranet Portal (OFN) support class for Huawei's Next Generation Network (NGN) business model
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
    public class Ims
    {
        private static int allPossibleServiceNumbersWithinHuaweiSwitchArrayListIndex;
        private static List<int> allPossibleServiceNumbersWithinHuaweiSwitchArrayList;

        private static int serviceRequestServiceServiceOfUnmatchedServicesListIndex;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Ims()
        {
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static int ServiceNumberToReadServiceData
        {
            get
            {
                int number;

                if (allPossibleServiceNumbersWithinHuaweiSwitchArrayList == null || allPossibleServiceNumbersWithinHuaweiSwitchArrayListIndex == 0)
                {
                    allPossibleServiceNumbersWithinHuaweiSwitchArrayList = Ia.Ngn.Cl.Model.Data.Service.AllPossibleServiceNumberListWithinHuaweiSwitch;

                    // below: start at a random index in ArrayList
                    allPossibleServiceNumbersWithinHuaweiSwitchArrayListIndex = Ia.Cl.Model.Default.Random(allPossibleServiceNumbersWithinHuaweiSwitchArrayList.Count - 1);
                }

                number = (int)allPossibleServiceNumbersWithinHuaweiSwitchArrayList[allPossibleServiceNumbersWithinHuaweiSwitchArrayListIndex];

                allPossibleServiceNumbersWithinHuaweiSwitchArrayListIndex = Ia.Cl.Model.Default.IncrementListIndexOrRestart(allPossibleServiceNumbersWithinHuaweiSwitchArrayList, allPossibleServiceNumbersWithinHuaweiSwitchArrayListIndex);

                return number;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool SynchronizeDatabaseTablesWithSbrForService(ims.api.huawei.ATSV100R003C01SPC100Client client, ims.api.huawei.Authentication authentication, string service, out string result)
        {
            bool b;
            string impu, messageId, meName;
            Hashtable ht;
            ims.api.huawei.LST_SBRType lstSbrType;

            messageId = "1";
            meName = "tecats1";
            result = "";

            lstSbrType = null;

            ht = new Hashtable(100);

            impu = Ia.Ngn.Cl.Model.Business.NumberFormatConverter.Impu(service);

            lstSbrType = client.LST_SBR(authentication, meName, ref messageId, impu, null, null);

            b = UpdateSbrAndServiceFromLstSbrType(client, authentication, impu, lstSbrType);

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private static bool UpdateSbrAndServiceFromLstSbrType(ims.api.huawei.ATSV100R003C01SPC100Client client, ims.api.huawei.Authentication authentication, string impu, ims.api.huawei.LST_SBRType lstSbrType)
        {
            bool b;
            string result;
            Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode resultCode;
            Ia.Ngn.Cl.Model.Huawei.HuSbr huSbr;

            resultCode = (Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode)lstSbrType.ResultCode;

            if (resultCode == Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode.OperationSucceeded)
            {
                huSbr = CreateSbrFromLstSbrType(ref lstSbrType.ResultData.Table1[0]);
            }
            else if (resultCode == Ia.Ngn.Cl.Model.Data.Huawei.Ims.ResultCode.TheSubscriberIsNotDefinedInTheHssOrAtsOrServiceDataIsNotConfiguredForTheSubscriber)
            {
                huSbr = null;
            }
            else huSbr = null;

            b = Ia.Ngn.Cl.Model.Data.Huawei.Ims.UpdateSbrWithResultData(impu, resultCode, ref huSbr);

            b = Ia.Ngn.Cl.Model.Data.Huawei.Ims.UpdateServiceForSbr(impu, huSbr);

            result = resultCode.ToString();

            return true;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Ia.Ngn.Cl.Model.Huawei.HuSbr CreateSbrFromLstSbrType(ref ims.api.huawei.LST_SBRStruct1 lstSbrStruct1)
        {
            int i;
            Ia.Ngn.Cl.Model.Huawei.HuSbr huSbr;

            huSbr = new Ia.Ngn.Cl.Model.Huawei.HuSbr();

            huSbr.IMPU = lstSbrStruct1.IMPU;

            huSbr.TEMPLATEIDX = (int)lstSbrStruct1.TEMPLATEIDX;
            huSbr.DSPIDX = (int)lstSbrStruct1.DSPIDX;
            huSbr.LP = (int)lstSbrStruct1.LP;
            huSbr.CSC = (int)lstSbrStruct1.CSC;
            huSbr.UNAME = lstSbrStruct1.UNAME;
            huSbr.VCCFLAG = lstSbrStruct1.VCCFLAG.ToBool();
            huSbr.VTFLAG = lstSbrStruct1.VTFLAG.ToBool();
            huSbr.NSCFU = Convert.ToBoolean(lstSbrStruct1.NSCFU);
            huSbr.NSCFUVM = Convert.ToBoolean(lstSbrStruct1.NSCFUVM);
            huSbr.NSCFB = Convert.ToBoolean(lstSbrStruct1.NSCFB);
            huSbr.NSCFBVM = Convert.ToBoolean(lstSbrStruct1.NSCFBVM);
            huSbr.NSCFNR = Convert.ToBoolean(lstSbrStruct1.NSCFNR);
            huSbr.NSCFNRVM = Convert.ToBoolean(lstSbrStruct1.NSCFNRVM);
            huSbr.NSCFNL = Convert.ToBoolean(lstSbrStruct1.NSCFNL);
            huSbr.NSCFNLVM = Convert.ToBoolean(lstSbrStruct1.NSCFNLVM);
            huSbr.NSCD = Convert.ToBoolean(lstSbrStruct1.NSCD);
            huSbr.NSCDVM = Convert.ToBoolean(lstSbrStruct1.NSCDVM);
            huSbr.NSCFNRC = Convert.ToBoolean(lstSbrStruct1.NSCFNRC);
            huSbr.NSCFNRCVM = Convert.ToBoolean(lstSbrStruct1.NSCFNRCVM);
            huSbr.NSCLIP = Convert.ToBoolean(lstSbrStruct1.NSCLIP);
            huSbr.NSCIDCW = Convert.ToBoolean(lstSbrStruct1.NSCIDCW);
            huSbr.NSRIO = Convert.ToBoolean(lstSbrStruct1.NSRIO);
            huSbr.NSCNIP = Convert.ToBoolean(lstSbrStruct1.NSCNIP);
            huSbr.NSCLIR = Convert.ToBoolean(lstSbrStruct1.NSCLIR);
            huSbr.NSRIP = Convert.ToBoolean(lstSbrStruct1.NSRIP);
            huSbr.NSCNIR = Convert.ToBoolean(lstSbrStruct1.NSCNIR);
            huSbr.NSRID = Convert.ToBoolean(lstSbrStruct1.NSRID);
            huSbr.NSNRID = Convert.ToBoolean(lstSbrStruct1.NSNRID);
            huSbr.NSRND = Convert.ToBoolean(lstSbrStruct1.NSRND);
            huSbr.NSNRND = Convert.ToBoolean(lstSbrStruct1.NSNRND);
            huSbr.NSCW = Convert.ToBoolean(lstSbrStruct1.NSCW);
            huSbr.NSCCW = Convert.ToBoolean(lstSbrStruct1.NSCCW);
            huSbr.NSOIP = Convert.ToBoolean(lstSbrStruct1.NSOIP);
            huSbr.NSACRM = Convert.ToBoolean(lstSbrStruct1.NSACRM);
            huSbr.NSGOIR = Convert.ToBoolean(lstSbrStruct1.NSGOIR);
            huSbr.NSMOIR = Convert.ToBoolean(lstSbrStruct1.NSMOIR);
            huSbr.NSTIP = Convert.ToBoolean(lstSbrStruct1.NSTIP);
            huSbr.NSTIR = Convert.ToBoolean(lstSbrStruct1.NSTIR);
            huSbr.NSOTIR = Convert.ToBoolean(lstSbrStruct1.NSOTIR);
            huSbr.NSCLIPNOSCREENING = Convert.ToBoolean(lstSbrStruct1.NSCLIPNOSCREENING);
            huSbr.NSCR = Convert.ToBoolean(lstSbrStruct1.NSCR);
            huSbr.NSWAKE_UP = Convert.ToBoolean(lstSbrStruct1.NSWAKE_UP);
            huSbr.NSAOC_D = Convert.ToBoolean(lstSbrStruct1.NSAOC_D);
            huSbr.NSAOC_E = Convert.ToBoolean(lstSbrStruct1.NSAOC_E);
            huSbr.NSXEXH = Convert.ToBoolean(lstSbrStruct1.NSXEXH);
            huSbr.NSXEGJ = Convert.ToBoolean(lstSbrStruct1.NSXEGJ);
            huSbr.NSCWCFNR = Convert.ToBoolean(lstSbrStruct1.NSCWCFNR);
            huSbr.NSIIFC = Convert.ToBoolean(lstSbrStruct1.NSIIFC);
            huSbr.NSDN_CALL_OUT_BAR = Convert.ToBoolean(lstSbrStruct1.NSDN_CALL_OUT_BAR);
            huSbr.NSCCBS = Convert.ToBoolean(lstSbrStruct1.NSCCBS);
            huSbr.NSCCNR = Convert.ToBoolean(lstSbrStruct1.NSCCNR);
            huSbr.NSCCBSR = Convert.ToBoolean(lstSbrStruct1.NSCCBSR);
            huSbr.NSCCNRR = Convert.ToBoolean(lstSbrStruct1.NSCCNRR);
            huSbr.NS3PTY = Convert.ToBoolean(lstSbrStruct1.NS3PTY);
            huSbr.NSNPTY = Convert.ToBoolean(lstSbrStruct1.NSNPTY);
            huSbr.NSDND = Convert.ToBoolean(lstSbrStruct1.NSDND);
            huSbr.NSMCR = Convert.ToBoolean(lstSbrStruct1.NSMCR);
            huSbr.NSCBA = Convert.ToBoolean(lstSbrStruct1.NSCBA);
            huSbr.NSTMP_LIN = Convert.ToBoolean(lstSbrStruct1.NSTMP_LIN);
            huSbr.NSCODEC_CNTRL = Convert.ToBoolean(lstSbrStruct1.NSCODEC_CNTRL);
            huSbr.NSMWI = Convert.ToBoolean(lstSbrStruct1.NSMWI);
            huSbr.NSDC = Convert.ToBoolean(lstSbrStruct1.NSDC);
            huSbr.NSHOLD = Convert.ToBoolean(lstSbrStruct1.NSHOLD);
            huSbr.NSECT = Convert.ToBoolean(lstSbrStruct1.NSECT);
            huSbr.NSCFTB = Convert.ToBoolean(lstSbrStruct1.NSCFTB);
            huSbr.NSDAN = Convert.ToBoolean(lstSbrStruct1.NSDAN);
            huSbr.NSSTOP_SECRET = Convert.ToBoolean(lstSbrStruct1.NSSTOP_SECRET);
            huSbr.NSMCID = Convert.ToBoolean(lstSbrStruct1.NSMCID);
            huSbr.NSEBO = Convert.ToBoolean(lstSbrStruct1.NSEBO);
            huSbr.NSICO = Convert.ToBoolean(lstSbrStruct1.NSICO);
            huSbr.NSOUTG = Convert.ToBoolean(lstSbrStruct1.NSOUTG);
            huSbr.NSINQYH = Convert.ToBoolean(lstSbrStruct1.NSINQYH);
            huSbr.NSUINFO = Convert.ToBoolean(lstSbrStruct1.NSUINFO);
            huSbr.NSDN_CALL_OUT_ALLOW = Convert.ToBoolean(lstSbrStruct1.NSDN_CALL_OUT_ALLOW);
            huSbr.NSSIC = Convert.ToBoolean(lstSbrStruct1.NSSIC);
            huSbr.NSSOC = Convert.ToBoolean(lstSbrStruct1.NSSOC);
            huSbr.NSSETCFNRTIME = Convert.ToBoolean(lstSbrStruct1.NSSETCFNRTIME);
            huSbr.NSCFS = Convert.ToBoolean(lstSbrStruct1.NSCFS);
            huSbr.NSCFSB = Convert.ToBoolean(lstSbrStruct1.NSCFSB);
            huSbr.NSFAX = Convert.ToBoolean(lstSbrStruct1.NSFAX);
            huSbr.NSABRC = Convert.ToBoolean(lstSbrStruct1.NSABRC);
            huSbr.NSACRTOVM = Convert.ToBoolean(lstSbrStruct1.NSACRTOVM);
            huSbr.NSPREPAID = Convert.ToBoolean(lstSbrStruct1.NSPREPAID);
            huSbr.NSCRBT = Convert.ToBoolean(lstSbrStruct1.NSCRBT);
            huSbr.NSICB = Convert.ToBoolean(lstSbrStruct1.NSICB);
            huSbr.NSMRINGING = Convert.ToBoolean(lstSbrStruct1.NSMRINGING);
            huSbr.NSCIS = Convert.ToBoolean(lstSbrStruct1.NSCIS);
            huSbr.NSCBEG = Convert.ToBoolean(lstSbrStruct1.NSCBEG);
            huSbr.NSCOLP = Convert.ToBoolean(lstSbrStruct1.NSCOLP);
            huSbr.NSCOLR = Convert.ToBoolean(lstSbrStruct1.NSCOLR);
            huSbr.NSCOLPOVR = Convert.ToBoolean(lstSbrStruct1.NSCOLPOVR);
            huSbr.NSBAOC = Convert.ToBoolean(lstSbrStruct1.NSBAOC);
            huSbr.NSBOIC = Convert.ToBoolean(lstSbrStruct1.NSBOIC);
            huSbr.NSBOICEXHC = Convert.ToBoolean(lstSbrStruct1.NSBOICEXHC);
            huSbr.NSBAIC = Convert.ToBoolean(lstSbrStruct1.NSBAIC);
            huSbr.NSBICROM = Convert.ToBoolean(lstSbrStruct1.NSBICROM);
            huSbr.NSSPEED_DIAL = Convert.ToBoolean(lstSbrStruct1.NSSPEED_DIAL);
            huSbr.NSSD1D = Convert.ToBoolean(lstSbrStruct1.NSSD1D);
            huSbr.NSSD2D = Convert.ToBoolean(lstSbrStruct1.NSSD2D);
            huSbr.NSGRNCALL = Convert.ToBoolean(lstSbrStruct1.NSGRNCALL);
            huSbr.NSCPARK = Convert.ToBoolean(lstSbrStruct1.NSCPARK);
            huSbr.NSGAA = Convert.ToBoolean(lstSbrStruct1.NSGAA);
            huSbr.NSQSNS = Convert.ToBoolean(lstSbrStruct1.NSQSNS);
            huSbr.NSMSN = Convert.ToBoolean(lstSbrStruct1.NSMSN);
            huSbr.NSHOTLINE = Convert.ToBoolean(lstSbrStruct1.NSHOTLINE);
            huSbr.NSAOC_S = Convert.ToBoolean(lstSbrStruct1.NSAOC_S);
            huSbr.NSNIGHTSRV = Convert.ToBoolean(lstSbrStruct1.NSNIGHTSRV);
            huSbr.NSBACKNUM = Convert.ToBoolean(lstSbrStruct1.NSBACKNUM);
            huSbr.NSAUTOCON = Convert.ToBoolean(lstSbrStruct1.NSAUTOCON);
            huSbr.NSCAMPON = Convert.ToBoolean(lstSbrStruct1.NSCAMPON);
            huSbr.NSCTD = Convert.ToBoolean(lstSbrStruct1.NSCTD);
            huSbr.NSCLICKHOLD = Convert.ToBoolean(lstSbrStruct1.NSCLICKHOLD);
            huSbr.NSQUEUE = Convert.ToBoolean(lstSbrStruct1.NSQUEUE);
            huSbr.NSSANSWER = Convert.ToBoolean(lstSbrStruct1.NSSANSWER);
            huSbr.NSICENCF = Convert.ToBoolean(lstSbrStruct1.NSICENCF);
            huSbr.NSCFGO = Convert.ToBoolean(lstSbrStruct1.NSCFGO);
            huSbr.NSCECT = Convert.ToBoolean(lstSbrStruct1.NSCECT);
            huSbr.NSCTGO = Convert.ToBoolean(lstSbrStruct1.NSCTGO);
            huSbr.NSCTIO = Convert.ToBoolean(lstSbrStruct1.NSCTIO);
            huSbr.NSSETBUSY = Convert.ToBoolean(lstSbrStruct1.NSSETBUSY);
            huSbr.NSOVERSTEP = Convert.ToBoolean(lstSbrStruct1.NSOVERSTEP);
            huSbr.NSABSENT = Convert.ToBoolean(lstSbrStruct1.NSABSENT);
            huSbr.NSMONITOR = Convert.ToBoolean(lstSbrStruct1.NSMONITOR);
            huSbr.NSFMONITOR = Convert.ToBoolean(lstSbrStruct1.NSFMONITOR);
            huSbr.NSDISCNT = Convert.ToBoolean(lstSbrStruct1.NSDISCNT);
            huSbr.NSFDISCNT = Convert.ToBoolean(lstSbrStruct1.NSFDISCNT);
            huSbr.NSINSERT = Convert.ToBoolean(lstSbrStruct1.NSINSERT);
            huSbr.NSFINSERT = Convert.ToBoolean(lstSbrStruct1.NSFINSERT);
            huSbr.NSASI = Convert.ToBoolean(lstSbrStruct1.NSASI);
            huSbr.NSPWCB = Convert.ToBoolean(lstSbrStruct1.NSPWCB);
            huSbr.NSRD = Convert.ToBoolean(lstSbrStruct1.NSRD);
            huSbr.NSLCPS = Convert.ToBoolean(lstSbrStruct1.NSLCPS);
            huSbr.NSNCPS = Convert.ToBoolean(lstSbrStruct1.NSNCPS);
            huSbr.NSICPS = Convert.ToBoolean(lstSbrStruct1.NSICPS);
            huSbr.NSCBCLOCK = Convert.ToBoolean(lstSbrStruct1.NSCBCLOCK);
            huSbr.NSMINIBAR = Convert.ToBoolean(lstSbrStruct1.NSMINIBAR);
            huSbr.NSMCN = Convert.ToBoolean(lstSbrStruct1.NSMCN);
            huSbr.NSDSTR = Convert.ToBoolean(lstSbrStruct1.NSDSTR);
            huSbr.NSOPRREG = Convert.ToBoolean(lstSbrStruct1.NSOPRREG);
            huSbr.NSONEKEY = Convert.ToBoolean(lstSbrStruct1.NSONEKEY);
            huSbr.NSINBOUND = Convert.ToBoolean(lstSbrStruct1.NSINBOUND);
            huSbr.NSOUTBOUND = Convert.ToBoolean(lstSbrStruct1.NSOUTBOUND);
            huSbr.NSCALLERID = Convert.ToBoolean(lstSbrStruct1.NSCALLERID);
            huSbr.NSCUN = Convert.ToBoolean(lstSbrStruct1.NSCUN);
            huSbr.NSIPTVVC = Convert.ToBoolean(lstSbrStruct1.NSIPTVVC);
            huSbr.NSNP = Convert.ToBoolean(lstSbrStruct1.NSNP);
            huSbr.NSSEC = Convert.ToBoolean(lstSbrStruct1.NSSEC);
            huSbr.NSSECSTA = Convert.ToBoolean(lstSbrStruct1.NSSECSTA);
            huSbr.NSHRCN = Convert.ToBoolean(lstSbrStruct1.NSHRCN);
            huSbr.NSSB = Convert.ToBoolean(lstSbrStruct1.NSSB);
            huSbr.NSOCCR = Convert.ToBoolean(lstSbrStruct1.NSOCCR);
            huSbr.LCO = Convert.ToBoolean(lstSbrStruct1.LCO);
            huSbr.LC = Convert.ToBoolean(lstSbrStruct1.LC);
            huSbr.LCT = Convert.ToBoolean(lstSbrStruct1.LCT);
            huSbr.NTT = Convert.ToBoolean(lstSbrStruct1.NTT);
            huSbr.ITT = Convert.ToBoolean(lstSbrStruct1.ITT);
            huSbr.ICTX = Convert.ToBoolean(lstSbrStruct1.ICTX);
            huSbr.OCTX = Convert.ToBoolean(lstSbrStruct1.OCTX);
            huSbr.INTT = Convert.ToBoolean(lstSbrStruct1.INTT);
            huSbr.IITT = Convert.ToBoolean(lstSbrStruct1.IITT);
            huSbr.ICLT = Convert.ToBoolean(lstSbrStruct1.ICLT);
            huSbr.ICDDD = Convert.ToBoolean(lstSbrStruct1.ICDDD);
            huSbr.ICIDD = Convert.ToBoolean(lstSbrStruct1.ICIDD);
            huSbr.IOLT = Convert.ToBoolean(lstSbrStruct1.IOLT);
            huSbr.CTLCO = Convert.ToBoolean(lstSbrStruct1.CTLCO);
            huSbr.CTLCT = Convert.ToBoolean(lstSbrStruct1.CTLCT);
            huSbr.CTLD = Convert.ToBoolean(lstSbrStruct1.CTLD);
            huSbr.CTINTNANP = Convert.ToBoolean(lstSbrStruct1.CTINTNANP);
            huSbr.CTINTWORLD = Convert.ToBoolean(lstSbrStruct1.CTINTWORLD);
            huSbr.CTDA = Convert.ToBoolean(lstSbrStruct1.CTDA);
            huSbr.CTOSM = Convert.ToBoolean(lstSbrStruct1.CTOSM);
            huSbr.CTOSP = Convert.ToBoolean(lstSbrStruct1.CTOSP);
            huSbr.CTOSP1 = Convert.ToBoolean(lstSbrStruct1.CTOSP1);
            huSbr.CCO1 = Convert.ToBoolean(lstSbrStruct1.CCO1);
            huSbr.CCO2 = Convert.ToBoolean(lstSbrStruct1.CCO2);
            huSbr.CCO3 = Convert.ToBoolean(lstSbrStruct1.CCO3);
            huSbr.CCO4 = Convert.ToBoolean(lstSbrStruct1.CCO4);
            huSbr.CCO5 = Convert.ToBoolean(lstSbrStruct1.CCO5);
            huSbr.CCO6 = Convert.ToBoolean(lstSbrStruct1.CCO6);
            huSbr.CCO7 = Convert.ToBoolean(lstSbrStruct1.CCO7);
            huSbr.CCO8 = Convert.ToBoolean(lstSbrStruct1.CCO8);
            huSbr.CCO9 = Convert.ToBoolean(lstSbrStruct1.CCO9);
            huSbr.CCO10 = Convert.ToBoolean(lstSbrStruct1.CCO10);
            huSbr.CCO11 = Convert.ToBoolean(lstSbrStruct1.CCO11);
            huSbr.CCO12 = Convert.ToBoolean(lstSbrStruct1.CCO12);
            huSbr.CCO13 = Convert.ToBoolean(lstSbrStruct1.CCO13);
            huSbr.CCO14 = Convert.ToBoolean(lstSbrStruct1.CCO14);
            huSbr.CCO15 = Convert.ToBoolean(lstSbrStruct1.CCO15);
            huSbr.CCO16 = Convert.ToBoolean(lstSbrStruct1.CCO16);
            huSbr.HIGHENTCO = Convert.ToBoolean(lstSbrStruct1.HIGHENTCO);
            huSbr.OPERATOR = Convert.ToBoolean(lstSbrStruct1.OPERATOR);
            huSbr.SUPYSRV = Convert.ToBoolean(lstSbrStruct1.SUPYSRV);
            huSbr.IDDCI = Convert.ToBoolean(lstSbrStruct1.IDDCI);
            huSbr.NTCI = Convert.ToBoolean(lstSbrStruct1.NTCI);
            huSbr.LTCI = Convert.ToBoolean(lstSbrStruct1.LTCI);
            huSbr.RSC = (int)lstSbrStruct1.RSC;
            huSbr.CIG = (int)lstSbrStruct1.CIG;
            huSbr.OUTRST = Convert.ToBoolean(lstSbrStruct1.OUTRST);
            huSbr.INRST = Convert.ToBoolean(lstSbrStruct1.INRST);
            huSbr.NOAT = (int)lstSbrStruct1.NOAT;
            huSbr.RINGCOUNT = (int)lstSbrStruct1.RINGCOUNT;
            huSbr.VMAIND = (int)lstSbrStruct1.VMAIND;
            huSbr.VDMAIND = (int)lstSbrStruct1.VDMAIND;
            huSbr.TGRP = (int)lstSbrStruct1.TGRP;
            huSbr.TIDHLD = (lstSbrStruct1.TIDHLD != null) ? int.TryParse(lstSbrStruct1.TIDHLD, out i) ? i : 0 : 0;
            huSbr.TIDCW = (lstSbrStruct1.TIDCW != null) ? int.TryParse(lstSbrStruct1.TIDCW, out i) ? i : 0 : 0;
            huSbr.SCF = lstSbrStruct1.SCF.ToBool();
            huSbr.LMTGRP = (int)lstSbrStruct1.LMTGRP;
            huSbr.FLBGRP = (int)lstSbrStruct1.FLBGRP;
            huSbr.SLBGRP = (int)lstSbrStruct1.SLBGRP;
            huSbr.COP = lstSbrStruct1.COP;
            huSbr.G711_64K_A_LAW = Convert.ToBoolean(lstSbrStruct1.G711_64K_A_LAW);
            huSbr.G711_64K_U_LAW = Convert.ToBoolean(lstSbrStruct1.G711_64K_U_LAW);
            huSbr.G722 = Convert.ToBoolean(lstSbrStruct1.G722);
            huSbr.G723 = Convert.ToBoolean(lstSbrStruct1.G723);
            huSbr.G726 = Convert.ToBoolean(lstSbrStruct1.G726);
            huSbr.G728 = Convert.ToBoolean(lstSbrStruct1.G728);
            huSbr.G729 = Convert.ToBoolean(lstSbrStruct1.G729);
            huSbr.CODEC_MP4A = Convert.ToBoolean(lstSbrStruct1.CODEC_MP4A);
            huSbr.CODEC2833 = Convert.ToBoolean(lstSbrStruct1.CODEC2833);
            huSbr.CODEC2198 = Convert.ToBoolean(lstSbrStruct1.CODEC2198);
            huSbr.G726_40 = Convert.ToBoolean(lstSbrStruct1.G726_40);
            huSbr.G726_32 = Convert.ToBoolean(lstSbrStruct1.G726_32);
            huSbr.G726_24 = Convert.ToBoolean(lstSbrStruct1.G726_24);
            huSbr.G726_16 = Convert.ToBoolean(lstSbrStruct1.G726_16);
            huSbr.AMR = Convert.ToBoolean(lstSbrStruct1.AMR);
            huSbr.CLEARMODE = Convert.ToBoolean(lstSbrStruct1.CLEARMODE);
            huSbr.ILBC = Convert.ToBoolean(lstSbrStruct1.ILBC);
            huSbr.SPEEX = Convert.ToBoolean(lstSbrStruct1.SPEEX);
            huSbr.G729EV = Convert.ToBoolean(lstSbrStruct1.G729EV);
            huSbr.EVRC = Convert.ToBoolean(lstSbrStruct1.EVRC);
            huSbr.EVRCB = Convert.ToBoolean(lstSbrStruct1.EVRCB);
            huSbr.H261 = Convert.ToBoolean(lstSbrStruct1.H261);
            huSbr.H263 = Convert.ToBoolean(lstSbrStruct1.H263);
            huSbr.CODEC_MP4V = Convert.ToBoolean(lstSbrStruct1.CODEC_MP4V);
            huSbr.H264 = Convert.ToBoolean(lstSbrStruct1.H264);
            huSbr.T38 = Convert.ToBoolean(lstSbrStruct1.T38);
            huSbr.T120 = Convert.ToBoolean(lstSbrStruct1.T120);
            huSbr.G711A_VBD = Convert.ToBoolean(lstSbrStruct1.G711A_VBD);
            huSbr.G711U_VBD = Convert.ToBoolean(lstSbrStruct1.G711U_VBD);
            huSbr.G726_VBD = Convert.ToBoolean(lstSbrStruct1.G726_VBD);
            huSbr.G726_40_VBD = Convert.ToBoolean(lstSbrStruct1.G726_40_VBD);
            huSbr.G726_32_VBD = Convert.ToBoolean(lstSbrStruct1.G726_32_VBD);
            huSbr.G726_24_VBD = Convert.ToBoolean(lstSbrStruct1.G726_24_VBD);
            huSbr.G726_16_VBD = Convert.ToBoolean(lstSbrStruct1.G726_16_VBD);
            huSbr.WIND_BAND_AMR = Convert.ToBoolean(lstSbrStruct1.WIND_BAND_AMR);
            huSbr.GSM610 = Convert.ToBoolean(lstSbrStruct1.GSM610);
            huSbr.H263_2000 = Convert.ToBoolean(lstSbrStruct1.H263_2000);
            huSbr.BROADVOICE_32 = Convert.ToBoolean(lstSbrStruct1.BROADVOICE_32);
            huSbr.UNKNOWN_CODEC = Convert.ToBoolean(lstSbrStruct1.UNKNOWN_CODEC);
            huSbr.ACODEC = (lstSbrStruct1.ACODEC != null) ? int.TryParse(lstSbrStruct1.ACODEC, out i) ? i : 0 : 0;
            huSbr.VCODEC = (lstSbrStruct1.VCODEC != null) ? int.TryParse(lstSbrStruct1.VCODEC, out i) ? i : 0 : 0;
            huSbr.POLIDX = (int)lstSbrStruct1.POLIDX;
            huSbr.NCPI = (int)lstSbrStruct1.NCPI;
            huSbr.ICPI = (int)lstSbrStruct1.ICPI;
            huSbr.EBOCL = (lstSbrStruct1.EBOCL != null) ? int.TryParse(lstSbrStruct1.EBOCL, out i) ? i : 0 : 0;
            huSbr.EBOPL = (lstSbrStruct1.EBOPL != null) ? int.TryParse(lstSbrStruct1.EBOPL, out i) ? i : 0 : 0;
            huSbr.EBOIT = (lstSbrStruct1.EBOIT != null) ? int.TryParse(lstSbrStruct1.EBOIT, out i) ? i : 0 : 0;
            huSbr.RM = lstSbrStruct1.RM.ToBool();
            huSbr.CPC = (lstSbrStruct1.CPC != null) ? int.TryParse(lstSbrStruct1.CPC, out i) ? i : 0 : 0;
            huSbr.PCHG = (int)lstSbrStruct1.PCHG;
            huSbr.TFPT = (lstSbrStruct1.TFPT != null) ? int.TryParse(lstSbrStruct1.TFPT, out i) ? i : 0 : 0;
            huSbr.CHT = lstSbrStruct1.CHT.ToBool();
            huSbr.MCIDMODE = lstSbrStruct1.MCIDMODE.ToBool();
            huSbr.MCIDCMODE = (lstSbrStruct1.MCIDCMODE != null) ? int.TryParse(lstSbrStruct1.MCIDCMODE, out i) ? i : 0 : 0;
            huSbr.MCIDAMODE = lstSbrStruct1.MCIDAMODE.ToBool();
            huSbr.PREPAIDIDX = (int)lstSbrStruct1.PREPAIDIDX;
            huSbr.CRBTID = (int)lstSbrStruct1.CRBTID;
            huSbr.ODBBICTYPE = (lstSbrStruct1.ODBBICTYPE != null) ? int.TryParse(lstSbrStruct1.ODBBICTYPE, out i) ? i : 0 : 0;
            huSbr.ODBBOCTYPE = (lstSbrStruct1.ODBBOCTYPE != null) ? int.TryParse(lstSbrStruct1.ODBBOCTYPE, out i) ? i : 0 : 0;
            huSbr.ODBBARTYPE = (lstSbrStruct1.ODBBARTYPE != null) ? int.TryParse(lstSbrStruct1.ODBBARTYPE, out i) ? i : 0 : 0;
            huSbr.ODBSS = lstSbrStruct1.ODBSS.ToBool();
            huSbr.ODBBRCFTYPE = (lstSbrStruct1.ODBBRCFTYPE != null) ? int.TryParse(lstSbrStruct1.ODBBRCFTYPE, out i) ? i : 0 : 0;
            huSbr.PNOTI = lstSbrStruct1.PNOTI.ToBool();
            huSbr.MAXPARACALL = (int)lstSbrStruct1.MAXPARACALL;
            huSbr.ATSDTMBUSY = lstSbrStruct1.ATSDTMBUSY.ToBool();
            huSbr.CALLCOUNT = (lstSbrStruct1.CALLCOUNT != null) ? int.TryParse(lstSbrStruct1.CALLCOUNT, out i) ? i : 0 : 0;
            huSbr.CDNOTICALLER = lstSbrStruct1.CDNOTICALLER.ToBool();
            huSbr.ISCHGFLAG = lstSbrStruct1.ISCHGFLAG.ToBool();
            huSbr.CHC = (lstSbrStruct1.CHC != null) ? int.TryParse(lstSbrStruct1.CHC, out i) ? i : 0 : 0;
            huSbr.CUSER = Convert.ToBoolean(lstSbrStruct1.CUSER);
            huSbr.CGRP = lstSbrStruct1.CGRP;
            huSbr.CUSERGRP = lstSbrStruct1.CUSERGRP;
            huSbr.STCF = Convert.ToBoolean(lstSbrStruct1.STCF);
            huSbr.CHARSC = (int)lstSbrStruct1.CHARSC;
            huSbr.REGUIDX = (int)lstSbrStruct1.REGUIDX;
            huSbr.SOCBFUNC = (lstSbrStruct1.SOCBFUNC != null) ? int.TryParse(lstSbrStruct1.SOCBFUNC, out i) ? i : 0 : 0;
            huSbr.SOCBPTONEIDX = (int)lstSbrStruct1.SOCBPTONEIDX;
            huSbr.ADMINCBA = lstSbrStruct1.ADMINCBA.ToBool();
            huSbr.ADCONTROL_DIVERSION = lstSbrStruct1.ADCONTROL_DIVERSION.ToBool();
            huSbr.DPR = lstSbrStruct1.DPR;
            huSbr.PRON = lstSbrStruct1.PRON;
            huSbr.CPCRUS = (int)lstSbrStruct1.CPCRUS;
            huSbr.CUSCAT = Convert.ToBoolean(lstSbrStruct1.CUSCAT);
            huSbr.SPT100REL = Convert.ToBoolean(lstSbrStruct1.SPT100REL);

            return huSbr;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}