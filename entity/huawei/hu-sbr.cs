using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Ia.Ngn.Cl.Model.Huawei
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Huawei's Sbr Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class HuSbr
    {
        /// <summary/>
        public enum ServiceTypeList { IMPU = 1, TEMPLATEIDX, DSPIDX, LP, CSC, UNAME, VCCFLAG, VTFLAG, NSCFU, NSCFUVM, NSCFB, NSCFBVM, NSCFNR, NSCFNRVM, NSCFNL, NSCFNLVM, NSCD, NSCDVM, NSCFNRC, NSCFNRCVM, NSCLIP, NSCIDCW, NSRIO, NSCNIP, NSCLIR, NSRIP, NSCNIR, NSRID, NSNRID, NSRND, NSNRND, NSCW, NSCCW, NSOIP, NSACRM, NSGOIR, NSMOIR, NSTIP, NSTIR, NSOTIR, NSCLIPNOSCREENING, NSCR, NSWAKE_UP, NSAOC_D, NSAOC_E, NSXEXH, NSXEGJ, NSCWCFNR, NSIIFC, NSDN_CALL_OUT_BAR, NSCCBS, NSCCNR, NSCCBSR, NSCCNRR, NS3PTY, NSNPTY, NSDND, NSMCR, NSCBA, NSTMP_LIN, NSCODEC_CNTRL, NSMWI, NSDC, NSHOLD, NSECT, NSCFTB, NSDAN, NSSTOP_SECRET, NSMCID, NSEBO, NSICO, NSOUTG, NSINQYH, NSUINFO, NSDN_CALL_OUT_ALLOW, NSSIC, NSSOC, NSSETCFNRTIME, NSCFS, NSCFSB, NSFAX, NSABRC, NSACRTOVM, NSPREPAID, NSCRBT, NSICB, NSMRINGING, NSCIS, NSCBEG, NSCOLP, NSCOLR, NSCOLPOVR, NSBAOC, NSBOIC, NSBOICEXHC, NSBAIC, NSBICROM, NSSPEED_DIAL, NSSD1D, NSSD2D, NSGRNCALL, NSCPARK, NSGAA, NSQSNS, NSMSN, NSHOTLINE, NSAOC_S, NSNIGHTSRV, NSBACKNUM, NSAUTOCON, NSCAMPON, NSCTD, NSCLICKHOLD, NSQUEUE, NSSANSWER, NSICENCF, NSCFGO, NSCECT, NSCTGO, NSCTIO, NSSETBUSY, NSOVERSTEP, NSABSENT, NSMONITOR, NSFMONITOR, NSDISCNT, NSFDISCNT, NSINSERT, NSFINSERT, NSASI, NSPWCB, NSRD, NSLCPS, NSNCPS, NSICPS, NSCBCLOCK, NSMINIBAR, NSMCN, NSDSTR, NSOPRREG, NSONEKEY, NSINBOUND, NSOUTBOUND, NSCALLERID, NSCUN, NSIPTVVC, NSNP, NSSEC, NSSECSTA, NSHRCN, NSSB, NSOCCR, LCO, LC, LCT, NTT, ITT, ICTX, OCTX, INTT, IITT, ICLT, ICDDD, ICIDD, IOLT, CTLCO, CTLCT, CTLD, CTINTNANP, CTINTWORLD, CTDA, CTOSM, CTOSP, CTOSP1, CCO1, CCO2, CCO3, CCO4, CCO5, CCO6, CCO7, CCO8, CCO9, CCO10, CCO11, CCO12, CCO13, CCO14, CCO15, CCO16, HIGHENTCO, OPERATOR, SUPYSRV, IDDCI, NTCI, LTCI, RSC, CIG, OUTRST, INRST, NOAT, RINGCOUNT, VMAIND, VDMAIND, TGRP, TIDHLD, TIDCW, SCF, LMTGRP, FLBGRP, SLBGRP, COP, G711_64K_A_LAW, G711_64K_U_LAW, G722, G723, G726, G728, G729, CODEC_MP4A, CODEC2833, CODEC2198, G726_40, G726_32, G726_24, G726_16, AMR, CLEARMODE, ILBC, SPEEX, G729EV, EVRC, EVRCB, H261, H263, CODEC_MP4V, H264, T38, T120, G711A_VBD, G711U_VBD, G726_VBD, G726_40_VBD, G726_32_VBD, G726_24_VBD, G726_16_VBD, WIND_BAND_AMR, GSM610, H263_2000, BROADVOICE_32, UNKNOWN_CODEC, ACODEC, VCODEC, POLIDX, NCPI, ICPI, EBOCL, EBOPL, EBOIT, RM, CPC, PCHG, TFPT, CHT, MCIDMODE, MCIDCMODE, MCIDAMODE, PREPAIDIDX, CRBTID, ODBBICTYPE, ODBBOCTYPE, ODBBARTYPE, ODBSS, ODBBRCFTYPE, PNOTI, MAXPARACALL, ATSDTMBUSY, CALLCOUNT, CDNOTICALLER, ISCHGFLAG, CHC, CUSER, CGRP, CUSERGRP, STCF, CHARSC, REGUIDX, SOCBFUNC, SOCBPTONEIDX, ADMINCBA, ADCONTROL_DIVERSION, DPR, PRON, CPCRUS, CUSCAT, SPT100REL }

        /// <summary/>
        public HuSbr() { }

        /*
         * List Subscriber(LST SBR)
         * 
         * Function This command is used to list basic subscriber data. Unless otherwise specified, parameters
         * related with service authority in this command use 0 to indicate that the subscriber does not have
         * the service authority, and 1 to indicate that the subscriber has the service authority.
         *
         * --------------------------------------------------------------------------------
         * Huawei Proprietary and Confidential
         * Copyright © Huawei Technologies Co., Ltd.
         * 
         */

        //ADD SBR: IMPU="tel:+96523900039", UTYPE=POTS, CPC=ORDINARY, NSCFU=1, NSCFB=1, NSCFNR=1, NSCW=1, NS3PTY=1, NSCLIP=1, NSCBA=1, NSWAKE_UP=1, ITT=1, IITT=1, ICIDD=1, COP="4498";

        /// <summary/>
        public long Id { get; set; }

        /// <summary>
        /// It specifies the IMPU of a specific subscriber. It can be a TEL URI or a SIP URI. The rules for setting the parameter are as follows:
        /// To configure an IMPU in SIP URI format, type a string such as sip:userinfo@huawei.com or userinfo@huawei.com.
        /// To configure an IMPU in TEL URI format, type a string such as tel:+867557780000 or +867557780000.
        ///. Value: a string of a maximum of 128 characters. Default value: none. This parameter is mandatory.
        /// </summary>
        [Key]
        public string IMPU { get; set; }

        /// <summary>
        /// Subscriber Data Template Index. Value: an integer ranging from 0 to 65535. Default value: none. This parameter is optional.
        /// </summary>
        public int TEMPLATEIDX { get; set; }

        /// <summary>
        /// Display index. Value: an integer ranging from 0 to 65534. Default value: none. This parameter is optional.
        /// </summary>
        public int DSPIDX { get; set; }

        /// <summary>
        /// Local DN set. Value: an integer ranging from 0 to 65534. Default value: none. This parameter is optional.
        /// </summary>
        public int LP { get; set; }

        /// <summary>
        /// Call source code. Value: an integer ranging from 0 to 65533. Default value: none. This parameter is optional.
        /// </summary>
        public int CSC { get; set; }

        /// <summary>
        /// Name. Value: a string of a maximum of 32 characters. Default value: none. This parameter is optional.
        /// </summary>
        public string UNAME { get; set; }

        /// <summary>
        /// UTYPE Type. Options: 
        /// 0:IMSSIPUE(IMSSIPUSER): The subscriber is an IMS SIP-UE subscriber.
        /// 1:POTS(POTSUSER): The subscriber is a SIP subscriber connected to the SIPIAD of the AGCF.
        /// 4:G/U(GUUSER): The subscriber is a G/U subscriber.
        /// 5:CDMA(CDMAUSER): The subscriber is a CDMA subscriber.
        /// 6:PSTN(PSTNUSER): The subscriber is a PSTN subscriber.
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int UTYPE { get; set; }

        /// <summary>
        /// VCC Flag. Options: 0:NO(NO), 1:YES(YES). Default value: none. This parameter is optional.
        /// </summary>
        public bool VCCFLAG { get; set; }

        /// <summary>
        /// VT Flag. Options: 0:NO(NO), 1:YES(YES), Default value: none. This parameter is optional.
        /// </summary>
        public bool VTFLAG { get; set; }

        /// <summary>
        /// Call Forwarding Unconditional Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCFU { get; set; }

        /// <summary>
        /// NSCFUVM Call Forwarding Unconditional to Voice Mailbox Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCFUVM { get; set; }

        /// <summary>
        /// Call Forwarding Busy Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCFB { get; set; }

        /// <summary>
        /// Call Forwarding Busy to Voice Mailbox Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCFBVM { get; set; }

        /// <summary>
        /// Call Forwarding No Reply Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCFNR { get; set; }

        /// <summary>
        /// Call Forwarding No Reply to Voice Mailbox Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCFNRVM { get; set; }

        /// <summary>
        /// Call Forwarding Offline Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCFNL { get; set; }

        /// <summary>
        /// Call Forwarding Offline to Voice Mailbox Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCFNLVM { get; set; }

        /// <summary>
        /// Communication Deflection Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCD { get; set; }

        /// <summary>
        /// Communication Deflection to Voice Mailbox Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCDVM { get; set; }

        /// <summary>
        /// Call Forwarding on User Not Reachable Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCFNRC { get; set; }

        /// <summary>
        /// Call Forwarding on User Not Reachable to Voice Mailbox Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCFNRCVM { get; set; }

        /// <summary>
        /// Calling Line Identification Presentation Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCLIP { get; set; }

        /// <summary>
        /// CIDCW Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCIDCW { get; set; }

        /// <summary>
        /// Calling Line Identification Restriction Override Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSRIO { get; set; }

        /// <summary>
        /// Calling Name Identification Presentation Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCNIP { get; set; }

        /// <summary>
        /// Calling Line Identification Restriction Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCLIR { get; set; }

        /// <summary>
        /// RIP Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional. 
        /// </summary>
        public bool NSRIP { get; set; }

        /// <summary>
        /// Calling Name Identification Restriction Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCNIR { get; set; }

        /// <summary>
        /// RID Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSRID { get; set; }

        /// <summary>
        /// NRID Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSNRID { get; set; }

        /// <summary>
        /// RND Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSRND { get; set; }

        /// <summary>
        /// NRND Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSNRND { get; set; }

        /// <summary>
        /// Call Waiting Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCW { get; set; }

        /// <summary>
        /// Cancel Call Waiting Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCCW { get; set; }

        /// <summary>
        /// OIP Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSOIP { get; set; }

        /// <summary>
        /// Anonymous Call Rejection Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSACRM { get; set; }

        /// <summary>
        /// GOIR Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSGOIR { get; set; }

        /// <summary>
        /// MOIR Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSMOIR { get; set; }

        /// <summary>
        /// TIP Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSTIP { get; set; }

        /// <summary>
        /// TIR Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSTIR { get; set; }

        /// <summary>
        /// OTIR Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSOTIR { get; set; }

        /// <summary>
        /// Calling Line Identification Presentation No Screening Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCLIPNOSCREENING { get; set; }

        /// <summary>
        /// Call Return Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCR { get; set; }

        /// <summary>
        /// Wake Up Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSWAKE_UP { get; set; }

        /// <summary>
        /// Advice of Charge During the Communication Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSAOC_D { get; set; }

        /// <summary>
        /// Advice of Charge at the End of the Communication Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSAOC_E { get; set; }

        /// <summary>
        /// XEXH Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSXEXH { get; set; }

        /// <summary>
        /// XEGJ Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSXEGJ { get; set; }

        /// <summary>
        /// Call Forwarding No Reply in Call Waiting Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCWCFNR { get; set; }

        /// <summary>
        /// Forwarded Incoming Call Rejection Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSIIFC { get; set; }

        /// <summary>
        /// DN_CALL_OUT_BAR Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSDN_CALL_OUT_BAR { get; set; }

        /// <summary>
        /// Completion of Communications to Busy Subscriber Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCCBS { get; set; }

        /// <summary>
        /// Completion of Communications by No Reply Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCCNR { get; set; }

        /// <summary>
        /// Completion of Communications to Busy Subscriber Restriction Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCCBSR { get; set; }

        /// <summary>
        /// Completion of Communications by No Reply Restriction Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCCNRR { get; set; }

        /// <summary>
        /// Three Party Conference Call Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NS3PTY { get; set; }

        /// <summary>
        /// Conference Call Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSNPTY { get; set; }

        /// <summary>
        /// Do Not Disturb Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSDND { get; set; }

        /// <summary>
        /// Malicious Call Rejection Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSMCR { get; set; }

        /// <summary>
        /// Outgoing Call Barring Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCBA { get; set; }

        /// <summary>
        /// Temporary Line Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSTMP_LIN { get; set; }

        /// <summary>
        /// CODEC_CNTRL Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCODEC_CNTRL { get; set; }

        /// <summary>
        /// Message Waiting Indication Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSMWI { get; set; }

        /// <summary>
        /// Double Communication Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSDC { get; set; }

        /// <summary>
        /// Call Hold Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSHOLD { get; set; }

        /// <summary>
        /// Explicit Communication Transfer Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSECT { get; set; }

        /// <summary>
        /// Call Forwarding by Time Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCFTB { get; set; }

        /// <summary>
        /// Designated Pickup Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSDAN { get; set; }

        /// <summary>
        /// STOP_SECRET Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSSTOP_SECRET { get; set; }

        /// <summary>
        /// Malicious Communication Identification Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSMCID { get; set; }

        /// <summary>
        /// Executive Busy Override Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSEBO { get; set; }

        /// <summary>
        /// Incoming Only Line Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSICO { get; set; }

        /// <summary>
        /// Outgoing Only Line Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSOUTG { get; set; }

        /// <summary>
        /// INQYH Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSINQYH { get; set; }

        /// <summary>
        /// Mutlimedia Information Presentation Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSUINFO { get; set; }

        /// <summary>
        /// Dial Number Call Out Allow Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSDN_CALL_OUT_ALLOW { get; set; }

        /// <summary>
        /// Selective Incoming Call Barring Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSSIC { get; set; }

        /// <summary>
        /// Selective Outgoing Call Barring Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSSOC { get; set; }

        /// <summary>
        /// SETCFNRTIME Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSSETCFNRTIME { get; set; }

        /// <summary>
        /// Selective Call Forwarding Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCFS { get; set; }

        /// <summary>
        /// Call Forwarding Based on Black List Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCFSB { get; set; }

        /// <summary>
        /// Fax Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSFAX { get; set; }

        /// <summary>
        /// Abbreviated Recall Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSABRC { get; set; }

        /// <summary>
        /// Anonymous Call Rejection Forwarding To Voice Mailbox Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSACRTOVM { get; set; }

        /// <summary>
        /// Prepaid Prefix Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSPREPAID { get; set; }

        /// <summary>
        /// Customized Ring Back Tone Control and Trigger Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCRBT { get; set; }

        /// <summary>
        /// Incoming Call Barring Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSICB { get; set; }

        /// <summary>
        /// Multiple Ringing Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSMRINGING { get; set; }

        /// <summary>
        /// Convergent Inter-personal Service Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCIS { get; set; }

        /// <summary>
        /// Outgoing Call Barring Except Green Number List Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCBEG { get; set; }

        /// <summary>
        /// Connected Line Identification Presentation Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCOLP { get; set; }

        /// <summary>
        /// Connected Line Identification Restriction Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCOLR { get; set; }

        /// <summary>
        /// Connected Line Identification Restriction Override Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCOLPOVR { get; set; }

        /// <summary>
        /// Barring of All Outgoing Calls Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSBAOC { get; set; }

        /// <summary>
        /// Barring of All Outgoing International Calls Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSBOIC { get; set; }

        /// <summary>
        /// Barring of Outgoing International Calls Except Those Directed to the Home PLMN Country Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSBOICEXHC { get; set; }

        /// <summary>
        /// Barring of All Incoming Calls Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSBAIC { get; set; }

        /// <summary>
        /// Barring of Incoming Calls When Roaming Outside the Home PLMN Country Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSBICROM { get; set; }

        /// <summary>
        /// Speed Dial Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSSPEED_DIAL { get; set; }

        /// <summary>
        /// Speed Dial One Digit Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSSD1D { get; set; }

        /// <summary>
        /// Speed Dial Two Digit Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSSD2D { get; set; }

        /// <summary>
        /// Green Call Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSGRNCALL { get; set; }

        /// <summary>
        /// Call Park Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCPARK { get; set; }

        /// <summary>
        /// Group Pickup Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSGAA { get; set; }

        /// <summary>
        /// Automatic Report User Number Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSQSNS { get; set; }

        /// <summary>
        /// Multiple Subscriber Number Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSMSN { get; set; }

        /// <summary>
        /// Hotline Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSHOTLINE { get; set; }

        /// <summary>
        /// Advice of Charge at Communication Set-up Time Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSAOC_S { get; set; }

        /// <summary>
        /// NIGHTSRV Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSNIGHTSRV { get; set; }

        /// <summary>
        /// BACKNUM Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSBACKNUM { get; set; }

        /// <summary>
        /// AUTOCON Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSAUTOCON { get; set; }

        /// <summary>
        /// CAMPON Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCAMPON { get; set; }

        /// <summary>
        /// Click to Dial Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCTD { get; set; }

        /// <summary>
        /// Click to Hold Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCLICKHOLD { get; set; }

        /// <summary>
        /// Call Queue Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSQUEUE { get; set; }

        /// <summary>
        /// SANSWER Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSSANSWER { get; set; }

        /// <summary>
        /// Call Forwarding of Incoming Centrex Call Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSICENCF { get; set; }

        /// <summary>
        /// Call Forwarding within Group Only Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCFGO { get; set; }

        /// <summary>
        /// Click to Transfer Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCECT { get; set; }

        /// <summary>
        /// Communication Transfer within Group Only Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCTGO { get; set; }

        /// <summary>
        /// Communication Transfer Incoming Centrex Only Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCTIO { get; set; }

        /// <summary>
        /// Console Set Busy Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSSETBUSY { get; set; }

        /// <summary>
        /// Overstep Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSOVERSTEP { get; set; }

        /// <summary>
        /// Absence Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSABSENT { get; set; }

        /// <summary>
        /// Operator Monitor Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSMONITOR { get; set; }

        /// <summary>
        /// Forbid Operator Monitor Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSFMONITOR { get; set; }

        /// <summary>
        /// Operator Disconnect Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSDISCNT { get; set; }

        /// <summary>
        /// Forbid Operator Disconnect Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSFDISCNT { get; set; }

        /// <summary>
        /// Operator Insert Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSINSERT { get; set; }

        /// <summary>
        /// Forbid Operator Insert Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSFINSERT { get; set; }

        /// <summary>
        /// Authorized Code for STD/IDD Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSASI { get; set; }

        /// <summary>
        /// Password Call Barring Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSPWCB { get; set; }

        /// <summary>
        /// Repeat Dial Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSRD { get; set; }

        /// <summary>
        /// Local Carrier Pre-Selection Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSLCPS { get; set; }

        /// <summary>
        /// National Carrier Pre-Selection Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSNCPS { get; set; }

        /// <summary>
        /// International Carrier Pre-Selection Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSICPS { get; set; }

        /// <summary>
        /// Carrier Selection on Call by Call Restriction Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCBCLOCK { get; set; }

        /// <summary>
        /// MINIBAR Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSMINIBAR { get; set; }

        /// <summary>
        /// Miss Call Notify Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSMCN { get; set; }

        /// <summary>
        /// Distinctive Ringing Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSDSTR { get; set; }

        /// <summary>
        /// Operator Registration Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSOPRREG { get; set; }

        /// <summary>
        /// USBDongle OneKey Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSONEKEY { get; set; }

        /// <summary>
        /// iRoaming Inbound Single IMSI Multi MSISDN Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSINBOUND { get; set; }

        /// <summary>
        /// iRoaming Outbound Single IMSI Multi MSISDN Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSOUTBOUND { get; set; }

        /// <summary>
        /// IPTV Caller ID Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCALLERID { get; set; }

        /// <summary>
        /// One Number Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSCUN { get; set; }

        /// <summary>
        /// IPTV Video Call Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSIPTVVC { get; set; }

        /// <summary>
        /// Number Portability Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSNP { get; set; }

        /// <summary>
        /// Secretary Service Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSSEC { get; set; }

        /// <summary>
        /// Secretary Station Service Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSSECSTA { get; set; }

        /// <summary>
        /// High Rate Call Notification Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSHRCN { get; set; }

        /// <summary>
        /// Sales Block Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSSB { get; set; }

        /// <summary>
        /// Operator Collect Call Rejection Right. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NSOCCR { get; set; }

        /// <summary>
        /// intra office. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool LCO { get; set; }

        /// <summary>
        /// Local. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool LC { get; set; }

        /// <summary>
        /// local toll. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool LCT { get; set; }

        /// <summary>
        /// national toll. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NTT { get; set; }

        /// <summary>
        /// international toll. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool ITT { get; set; }

        /// <summary>
        /// intra-Centrex. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool ICTX { get; set; }

        /// <summary>
        /// outgoing Centrex. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool OCTX { get; set; }

        /// <summary>
        /// intra-office national toll. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool INTT { get; set; }

        /// <summary>
        /// intra-office international toll. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool IITT { get; set; }

        /// <summary>
        /// Centrex local toll. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool ICLT { get; set; }

        /// <summary>
        /// Centrex national toll. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool ICDDD { get; set; }

        /// <summary>
        /// Centrex international toll. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool ICIDD { get; set; }

        /// <summary>
        /// intra-office local toll. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool IOLT { get; set; }

        /// <summary>
        /// CallTyping Local. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CTLCO { get; set; }

        /// <summary>
        /// CallTyping Local toll. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CTLCT { get; set; }

        /// <summary>
        /// CallTyping LD. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CTLD { get; set; }

        /// <summary>
        /// CallTyping International NANP. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CTINTNANP { get; set; }

        /// <summary>
        /// CallTyping International World. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CTINTWORLD { get; set; }

        /// <summary>
        /// CallTyping DA. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CTDA { get; set; }

        /// <summary>
        /// CallTyping OSM. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CTOSM { get; set; }

        /// <summary>
        /// CallTyping OSP. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CTOSP { get; set; }

        /// <summary>
        /// CallTyping OSP1. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CTOSP1 { get; set; }

        /// <summary>
        /// customized call-out authority 1. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO1 { get; set; }

        /// <summary>
        /// customized call-out authority 2. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO2 { get; set; }

        /// <summary>
        /// customized call-out authority 3. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO3 { get; set; }

        /// <summary>
        /// customized call-out authority 4. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO4 { get; set; }

        /// <summary>
        /// customized call-out authority 5. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO5 { get; set; }

        /// <summary>
        /// customized call-out authority 6. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO6 { get; set; }

        /// <summary>
        /// customized call-out authority 7. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO7 { get; set; }

        /// <summary>
        /// customized call-out authority 8. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO8 { get; set; }

        /// <summary>
        /// customized call-out authority 9. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO9 { get; set; }

        /// <summary>
        /// customized call-out authority 10. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO10 { get; set; }

        /// <summary>
        /// customized call-out authority 11. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO11 { get; set; }

        /// <summary>
        /// customized call-out authority 12. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO12 { get; set; }

        /// <summary>
        /// customized call-out authority 13. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO13 { get; set; }

        /// <summary>
        /// customized call-out authority 14. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO14 { get; set; }

        /// <summary>
        /// customized call-out authority 15. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO15 { get; set; }

        /// <summary>
        /// customized call-out authority 16. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CCO16 { get; set; }

        /// <summary>
        /// customized High entertainment call-out. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool HIGHENTCO { get; set; }

        /// <summary>
        /// customized Opertator. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool OPERATOR { get; set; }

        /// <summary>
        /// customized supply service. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool SUPYSRV { get; set; }

        /// <summary>
        /// International call-in authority. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool IDDCI { get; set; }

        /// <summary>
        /// National Toll call-in authority. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool NTCI { get; set; }

        /// <summary>
        /// Local Toll call-in authority. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool LTCI { get; set; }

        /// <summary>
        /// Callee route source code. Value: an integer ranging from 0 to 65535. Default value: none. This parameter is optional.
        /// </summary>
        public int RSC { get; set; }

        /// <summary>
        /// Pickup group number. Value: an integer ranging from 0 to 4294967295. Default value: none. This parameter is optional.
        /// </summary>
        public long CIG { get; set; }

        /// <summary>
        /// Outgoing call restriction status Options:
        /// 0:NO(NO): When this option is selected, all outgoing calls of a subscriber are permited.
        /// 1:YES(YES): When this option is selected, all outgoing calls of a subscriber are restricted.
        /// Default value: none. This parameter is optional.
        /// </summary>
        public bool OUTRST { get; set; }

        /// <summary>
        /// Incoming call restriction status Options:
        /// 0:NO(NO): When this option is selected, all incoming calls of a subscriber are permited.
        /// 1:YES(YES): When this option is selected, all incoming calls of a subscriber are rejected.
        /// Default value: none. This parameter is optional.
        /// </summary>
        public bool INRST { get; set; }

        /// <summary>
        /// Duration of no response. Value: an integer ranging from 1 to 65534. Default value: none. This parameter is optional.
        /// </summary>
        public int NOAT { get; set; }

        /// <summary>
        /// Count of ring. Value: an integer ranging from 0 to 65534. Default value: none. This parameter is optional.
        /// </summary>
        public int RINGCOUNT { get; set; }

        /// <summary>
        /// Voice mailbox address index. Value: an integer ranging from 0 to 65535. Default value: none. This parameter is optional.
        /// </summary>
        public int VMAIND { get; set; }

        /// <summary>
        /// Video mailbox address index. Value: an integer ranging from 0 to 65535. Default value: none. This parameter is optional.
        /// </summary>
        public int VDMAIND { get; set; }

        /// <summary>
        /// Voice group number. Value: an integer ranging from 0 to 65535. Default value: none. This parameter is optional.
        /// </summary>
        public int TGRP { get; set; }

        /// <summary>
        /// Personalized HOLD tone Options:
        /// 0:TID_117(TID_117)
        /// 1:BUSY(BUSY)
        /// 2:RING_BACK(RING_BACK)
        /// 3:CALLED_BILLING(CALLED_BILLING)
        /// 4:CALLIN_RESTRICTION(CALLIN_RESTRICTION)
        /// 5:WAKEUP(WAKEUP)
        /// 6:NETWORK_CONGESTION(NETWORK_CONGESTION)
        /// 7:NEW_SERVICE_CANCEL(NEW_SERVICE_CANCEL)
        /// 8:CALLED_BUSY(CALLED_BUSY)
        /// 9:ERROR_NUMBER(ERROR_NUMBER)
        /// 10:NO_SUCH_NUMBER(NO_SUCH_NUMBER)
        /// 11:NOT_DISTURB(NOT_DISTURB)
        /// 12:NEW_SERVICE_REGISTER(NEW_SERVICE_REGISTER)
        /// 13:NEW_SERVICE_FAIL(NEW_SERVICE_FAIL)
        /// 14:BILLING(BILLING)
        /// 15:CALL_RESTRICTION(CALL_RESTRICTION)
        /// 16:MALICIOUS_CALL_SUCCESS(MALICIOUS_CALL_SUCCESS)
        /// 17:MUSIC(MUSIC)
        /// 18:NUMBER_CHANGED(NUMBER_CHANGED)
        /// 19:WRONG_PASSWORD(WRONG_PASSWORD)
        /// 20:CALLED_UNREACHABLE(CALLED_UNREACHABLE)
        /// 21:CALLED_OUT_OF_SERVICE(CALLED_OUT_OF_SERVICE)
        /// 22:OVERLOAD_RESTRICTION(OVERLOAD_RESTRICTION)
        /// 23:PLEASE_WAITING(PLEASE_WAITING)
        /// 24:SERVICE_NOT_PROVIDED(SERVICE_NOT_PROVIDED)
        /// 25:SERVICE_NOT_APPLIED(SERVICE_NOT_APPLIED)
        /// 26:FORWARD_TO_NUMBER(FORWARD_TO_NUMBER)
        /// 27:VERIFY_CF_FAIL(VERIFY_CF_FAIL)
        /// 28:HOLD(Call Hold)
        /// 29:CALL_WAITING_A(CALL_WAITING_A)
        /// 30:TMPLINE_CALLER(TMPLINE_CALLER)
        /// 31:XEXH(XEXH)
        /// 32:QUERY_SELECTIVE_CALL_CLI(QUERY_SELECTIVE_CALL_CLI)
        /// 33:CR_HAVE_MISSED_CALL(CR_HAVE_MISSED_CALL)
        /// 34:NUMBER_IS(NUMBER_IS)
        /// 35:TIME_IS(TIME_IS)
        /// 36:CR_PLEASE_DIAL_FIVE(CR_PLEASE_DIAL_FIVE)
        /// 37:CR_DIAL_ERR_PLEASE_REDIAL(CR_DIAL_ERR_PLEASE_REDIAL)
        /// 38:CR_CALLINFO_ALREADY_DELETE(CR_CALLINFO_ALREADY_DELETE)
        /// 39:CALL_PROCESSING_PLEASE_WAIT(CALL_PROCESSING_PLEASE_WAIT)
        /// 40:DIAL_ERROR_PLEASE_HOOK(DIAL_ERROR_PLEASE_HOOK)
        /// 41:CR_NO_MISSED_CALL_PRECENTLY(CR_NO_MISSED_CALL_PRECENTLY)
        /// 42:NUMBER_CHANGED_VOICE(NUMBER_CHANGED_VOICE)
        /// 43:TMPLINE_CALLEE(TMPLINE_CALLEE)
        /// 44:NEW_SERVICE_USE_FAIL(NEW_SERVICE_USE_FAIL)
        /// 45:NEW_SERVICE_CANCEL_FAIL(NEW_SERVICE_CANCEL_FAIL)
        /// 46:NEW_SERVICE_VERIFY_FAIL(NEW_SERVICE_VERIFY_FAIL)
        /// 47:INPUT_CCBS_ACTIVE_CODE(INPUT_CCBS_ACTIVE_CODE)
        /// 48:INPUT_CCNR_ACTIVE_CODE(INPUT_CCNR_ACTIVE_CODE)
        /// 49:WRONG_CCBS_CCNR_ACTIVE_CODE(WRONG_CCBS_CCNR_ACTIVE_CODE)
        /// 50:CALL_BACK_CCBS_CCNR(CALL_BACK_CCBS_CCNR)
        /// 51:VERIFY_CFNR_OK(VERIFY_CFNR_OK)
        /// 52:VERIFY_CFU_OK(VERIFY_CFU_OK)
        /// 53:VERIFY_CFB_OK(VERIFY_CFB_OK)
        /// 54:VERIFY_CFNL_OK(VERIFY_CFNL_OK)
        /// 55:VERIFY_CFNRc_OK(VERIFY_CFNRc_OK)
        /// 56:VERIFY_IIFC_OK(VERIFY_IIFC_OK)
        /// 57:CFNRTIME(CFNRTIME)
        /// 58:VERIFY_CFNRVM_OK(VERIFY_CFNRVM_OK)
        /// 59:VERIFY_CFUVM_OK(VERIFY_CFUVM_OK)
        /// 60:VERIFY_CFBVM_OK(VERIFY_CFBVM_OK)
        /// 61:VERIFY_CFNLVM_OK(VERIFY_CFNLVM_OK)
        /// 62:VERIFY_CFNRcVM_OK(VERIFY_CFNRcVM_OK)
        /// 63:PLAY_RECORD_PRE(PLAY_RECORD_PRE)
        /// 64:PLAY_PRE(PLAY_PRE)
        /// 65:PLAY_RECORD(PLAY_RECORD)
        /// 66:PLAY_NOTIFT_CALLEE(PLAY_NOTIFT_CALLEE)
        /// 67:PLAY_REJECT_CALLER(PLAY_REJECT_CALLER)
        /// 68:ANONYMOUS_CALL_REJECT(ANONYMOUS_CALL_REJECT)
        /// 69:MALICIOUS_CALL_REJECT(MALICIOUS_CALL_REJECT)
        /// 65535:TID_BUTT(TID_BUTT)
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int TIDHLD { get; set; }

        /// <summary>
        /// Personalized CW tone Options:
        /// 0:TID_117(TID_117)
        /// 1:BUSY(BUSY)
        /// 2:RING_BACK(RING_BACK)
        /// 3:CALLED_BILLING(CALLED_BILLING)
        /// 4:CALLIN_RESTRICTION(CALLIN_RESTRICTION)
        /// 5:WAKEUP(WAKEUP)
        /// 6:NETWORK_CONGESTION(NETWORK_CONGESTION)
        /// 7:NEW_SERVICE_CANCEL(NEW_SERVICE_CANCEL)
        /// 8:CALLED_BUSY(CALLED_BUSY)
        /// 9:ERROR_NUMBER(ERROR_NUMBER)
        /// 10:NO_SUCH_NUMBER(NO_SUCH_NUMBER)
        /// 11:NOT_DISTURB(NOT_DISTURB)
        /// 12:NEW_SERVICE_REGISTER(NEW_SERVICE_REGISTER)
        /// 13:NEW_SERVICE_FAIL(NEW_SERVICE_FAIL)
        /// 14:BILLING(BILLING)
        /// 15:CALL_RESTRICTION(CALL_RESTRICTION)
        /// 16:MALICIOUS_CALL_SUCCESS(MALICIOUS_CALL_SUCCESS)
        /// 17:MUSIC(MUSIC)
        /// 18:NUMBER_CHANGED(NUMBER_CHANGED)
        /// 19:WRONG_PASSWORD(WRONG_PASSWORD)
        /// 20:CALLED_UNREACHABLE(CALLED_UNREACHABLE)
        /// 21:CALLED_OUT_OF_SERVICE(CALLED_OUT_OF_SERVICE)
        /// 22:OVERLOAD_RESTRICTION(OVERLOAD_RESTRICTION)
        /// 23:PLEASE_WAITING(PLEASE_WAITING)
        /// 24:SERVICE_NOT_PROVIDED(SERVICE_NOT_PROVIDED)
        /// 25:SERVICE_NOT_APPLIED(SERVICE_NOT_APPLIED)
        /// 26:FORWARD_TO_NUMBER(FORWARD_TO_NUMBER)
        /// 27:VERIFY_CF_FAIL(VERIFY_CF_FAIL)
        /// 28:HOLD(Call Hold)
        /// 29:CALL_WAITING_A(CALL_WAITING_A)
        /// 30:TMPLINE_CALLER(TMPLINE_CALLER)
        /// 31:XEXH(XEXH)
        /// 32:QUERY_SELECTIVE_CALL_CLI(QUERY_SELECTIVE_CALL_CLI)
        /// 33:CR_HAVE_MISSED_CALL(CR_HAVE_MISSED_CALL)
        /// 34:NUMBER_IS(NUMBER_IS)
        /// 35:TIME_IS(TIME_IS)
        /// 36:CR_PLEASE_DIAL_FIVE(CR_PLEASE_DIAL_FIVE)
        /// 37:CR_DIAL_ERR_PLEASE_REDIAL(CR_DIAL_ERR_PLEASE_REDIAL)
        /// 38:CR_CALLINFO_ALREADY_DELETE(CR_CALLINFO_ALREADY_DELETE)
        /// 39:CALL_PROCESSING_PLEASE_WAIT(CALL_PROCESSING_PLEASE_WAIT)
        /// 40:DIAL_ERROR_PLEASE_HOOK(DIAL_ERROR_PLEASE_HOOK)
        /// 41:CR_NO_MISSED_CALL_PRECENTLY(CR_NO_MISSED_CALL_PRECENTLY)
        /// 42:NUMBER_CHANGED_VOICE(NUMBER_CHANGED_VOICE)
        /// 43:TMPLINE_CALLEE(TMPLINE_CALLEE)
        /// 44:NEW_SERVICE_USE_FAIL(NEW_SERVICE_USE_FAIL)
        /// 45:NEW_SERVICE_CANCEL_FAIL(NEW_SERVICE_CANCEL_FAIL)
        /// 46:NEW_SERVICE_VERIFY_FAIL(NEW_SERVICE_VERIFY_FAIL)
        /// 47:INPUT_CCBS_ACTIVE_CODE(INPUT_CCBS_ACTIVE_CODE)
        /// 48:INPUT_CCNR_ACTIVE_CODE(INPUT_CCNR_ACTIVE_CODE)
        /// 49:WRONG_CCBS_CCNR_ACTIVE_CODE(WRONG_CCBS_CCNR_ACTIVE_CODE)
        /// 50:CALL_BACK_CCBS_CCNR(CALL_BACK_CCBS_CCNR)
        /// 51:VERIFY_CFNR_OK(VERIFY_CFNR_OK)
        /// 52:VERIFY_CFU_OK(VERIFY_CFU_OK)
        /// 53:VERIFY_CFB_OK(VERIFY_CFB_OK)
        /// 54:VERIFY_CFNL_OK(VERIFY_CFNL_OK)
        /// 55:VERIFY_CFNRc_OK(VERIFY_CFNRc_OK)
        /// 56:VERIFY_IIFC_OK(VERIFY_IIFC_OK)
        /// 57:CFNRTIME(CFNRTIME)
        /// 58:VERIFY_CFNRVM_OK(VERIFY_CFNRVM_OK)
        /// 59:VERIFY_CFUVM_OK(VERIFY_CFUVM_OK)
        /// 60:VERIFY_CFBVM_OK(VERIFY_CFBVM_OK)
        /// 61:VERIFY_CFNLVM_OK(VERIFY_CFNLVM_OK)
        /// 62:VERIFY_CFNRcVM_OK(VERIFY_CFNRcVM_OK)
        /// 63:PLAY_RECORD_PRE(PLAY_RECORD_PRE)
        /// 64:PLAY_PRE(PLAY_PRE)
        /// 65:PLAY_RECORD(PLAY_RECORD)
        /// 66:PLAY_NOTIFT_CALLEE(PLAY_NOTIFT_CALLEE)
        /// 67:PLAY_REJECT_CALLER(PLAY_REJECT_CALLER)
        /// 68:ANONYMOUS_CALL_REJECT(ANONYMOUS_CALL_REJECT)
        /// 69:MALICIOUS_CALL_REJECT(MALICIOUS_CALL_REJECT)
        /// 65535:TID_BUTT(TID_BUTT)
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int TIDCW { get; set; }

        /// <summary>
        /// CCBS holding flag Options:
        /// 0:NO(NO), 1:YES(YES), Default value: none. This parameter is optional.
        /// </summary>
        public bool SCF { get; set; }

        /// <summary>
        /// Restriction group. Value: an integer ranging from 0 to 65535. Default value: none. This parameter is optional.
        /// </summary>
        public int LMTGRP { get; set; }

        /// <summary>
        /// First level bill group. Value: an integer ranging from 0 to 65535. Default value: none. This parameter is optional.
        /// </summary>
        public int FLBGRP { get; set; }

        /// <summary>
        /// Second level bill group. Value: an integer ranging from 0 to 65535. Default value: none. This parameter is optional.
        /// </summary>
        public int SLBGRP { get; set; }

        /// <summary>
        /// Call-out password. Value: a string of a maximum of 8 characters. Default value: none. This parameter is optional.
        /// </summary>
        public string COP { get; set; }

        /// <summary>
        /// G711_64K_A_LAW. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G711_64K_A_LAW { get; set; }

        /// <summary>
        /// G711_64K_U_LAW. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G711_64K_U_LAW { get; set; }

        /// <summary>
        /// G722. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G722 { get; set; }

        /// <summary>
        /// G723. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G723 { get; set; }

        /// <summary>
        /// G726. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G726 { get; set; }

        /// <summary>
        /// G728. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G728 { get; set; }

        /// <summary>
        /// G729. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G729 { get; set; }

        /// <summary>
        /// CODEC_MP4A. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CODEC_MP4A { get; set; }

        /// <summary>
        /// CODEC2833. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CODEC2833 { get; set; }

        /// <summary>
        /// CODEC2198. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CODEC2198 { get; set; }

        /// <summary>
        /// G726_40. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G726_40 { get; set; }

        /// <summary>
        /// G726_32. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G726_32 { get; set; }

        /// <summary>
        /// G726_24. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G726_24 { get; set; }

        /// <summary>
        /// G726_16. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G726_16 { get; set; }

        /// <summary>
        /// AMR. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool AMR { get; set; }

        /// <summary>
        /// CLEARMODE. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CLEARMODE { get; set; }

        /// <summary>
        /// ILBC. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool ILBC { get; set; }

        /// <summary>
        /// SPEEX. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool SPEEX { get; set; }

        /// <summary>
        /// G729EV. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G729EV { get; set; }

        /// <summary>
        /// EVRC. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool EVRC { get; set; }

        /// <summary>
        /// EVRCB. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool EVRCB { get; set; }

        /// <summary>
        /// H261. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool H261 { get; set; }

        /// <summary>
        /// H263. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool H263 { get; set; }

        /// <summary>
        /// CODEC_MP4V. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CODEC_MP4V { get; set; }

        /// <summary>
        /// H264. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool H264 { get; set; }

        /// <summary>
        /// T38. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool T38 { get; set; }

        /// <summary>
        /// T120. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool T120 { get; set; }

        /// <summary>
        /// G711A_VBD. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G711A_VBD { get; set; }

        /// <summary>
        /// G711U_VBD. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G711U_VBD { get; set; }

        /// <summary>
        /// G726_VBD. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G726_VBD { get; set; }

        /// <summary>
        /// G726_40_VBD. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G726_40_VBD { get; set; }

        /// <summary>
        /// G726_32_VBD. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G726_32_VBD { get; set; }

        /// <summary>
        /// G726_24_VBD. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G726_24_VBD { get; set; }

        /// <summary>
        /// G726_16_VBD. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool G726_16_VBD { get; set; }

        /// <summary>
        /// WIND_BAND_AMR. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool WIND_BAND_AMR { get; set; }

        /// <summary>
        /// GSM610. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool GSM610 { get; set; }

        /// <summary>
        /// H263_2000. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool H263_2000 { get; set; }

        /// <summary>
        /// BROADVOICE_32. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool BROADVOICE_32 { get; set; }

        /// <summary>
        /// UNKNOWN_CODEC. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool UNKNOWN_CODEC { get; set; }

        /// <summary>
        /// Audio codec prefer Options:
        /// 0:AUDIO_NULL_PREFER(AUDIO_NULL_PREFER)
        /// 1:G711_64K_A_LAW(G711_64K_A_LAW)
        /// 2:G711_64K_U_LAW(G711_64K_U_LAW)
        /// 3:G722(G722)
        /// 4:G723(G723)
        /// 5:G726(G726)
        /// 6:G728(G728)
        /// 7:G729(G729)
        /// 8:CODEC_MP4A(CODEC_MP4A)
        /// 9:G726_40(G726_40)
        /// 10:G726_32(G726_32)
        /// 11:G726_24(G726_24)
        /// 12:G726_16(G726_16)
        /// 13:AMR(AMR)
        /// 14:ILBC(ILBC)
        /// 15:SPEEX(SPEEX)
        /// 16:G729EV(G729EV)
        /// 17:WIND_BAND_AMR(WIND_BAND_AMR)
        /// 18:GSM610(GSM610)
        /// 19:BROADVOICE_32(BROADVOICE_32)
        /// 21:EVRC(EVRC)
        /// 22:EVRCB(EVRCB)
        /// 255:NONE(NONE)
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int ACODEC { get; set; }

        /// <summary>
        /// Video codec prefer Options:
        /// 0:VIDEO_NULL_PREFER(VIDEO_NULL_PREFER)
        /// 1:H261(H261)
        /// 3:H263(H263)
        /// 4:CODEC_MP4V(CODEC_MP4V)
        /// 5:H264(H264)
        /// 7:H263_2000(H263_2000)
        /// 255:NONE(NONE)
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int VCODEC { get; set; }

        /// <summary>
        /// Carrier preselection index. Value: an integer ranging from 0 to 255. Default value: none. This parameter is optional.
        /// </summary>
        public int POLIDX { get; set; }

        /// <summary>
        /// National carrier preselection code ID. Value: an integer ranging from 0 to 255. Default value: none. This parameter is optional.
        /// </summary>
        public int NCPI { get; set; }

        /// <summary>
        /// International carrier preselection code ID. Value: an integer ranging from 0 to 255. Default value: none. This parameter is optional.
        /// </summary>
        public int ICPI { get; set; }

        /// <summary>
        /// EBO level Options:
        /// 0:NOEBOL(NOEBOL)
        /// 1:LOW(LOW)
        /// 2:MEDI(MEDI)
        /// 3:HIGH(HIGH)
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int EBOCL { get; set; }

        /// <summary>
        /// EBO protection level Options:
        /// 0:NOEBOL(NOEBOL)
        /// 1:LOW(LOW)
        /// 2:MEDI(MEDI)
        /// 3:HIGH(HIGH)
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int EBOPL { get; set; }

        /// <summary>
        /// EBO trigger mode Options:
        /// 0:IMD(IMD): The subscriber barges into an session without dialing the service code.
        /// 1:CON(CON): The subscriber barges into an session by dialing the service code.
        /// 2:INVALID(INVALID): Other subscribers are not allowed to barge into an session.
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int EBOIT { get; set; }

        /// <summary>
        /// Restriction mode Options:
        /// 0:NOTPLAYTONE(NOTPLAYTONE): The call is released without playing the outgoing call barring announcement.
        /// 1:PLAYRESTRICTTONE(PLAYRESTRICTTONE): The call is released after the outgoing call barring announcement is played.
        /// Default value: none. This parameter is optional.
        /// </summary>
        public bool RM { get; set; }

        /// <summary>
        /// User category Options:
        /// 0:ORDINARY(NRM): The subscriber is an ordinary subscriber. An ordinary subscriber does not have the preferred line hunting right during system congestion. In addition, calls of an ordinary subscriber are the first to be restricted during system overload and the last to be connected during system recovery.
        /// 1:TEST(TEST): The subscriber is a test subscriber. Only a tester or an operator can use the service of designated trunk dial test (dedicated line calling).
        /// 2:OPERATOR(OPR): The subscriber is an operator. An operator has the right to break in, forcibly release, intercept, and record a call.
        /// 3:PAYPHONE(PAYPHONE): The subscriber is a prepaid subscriber, such as an APS subscriber.
        /// 4:PRIORITY(PRI): The subscriber is a priority subscriber. A priority subscriber has the preferred line hunting right during system congestion. In addition, calls of a priority subscriber are the last to be restricted during system overload and the first to be connected during system recovery.
        /// 5:DATA(DATA): The subscriber is a data subscriber. The call of a data subscriber or a protected subscriber cannot be broken in or forcibly released by other subscribers, including operators.
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int CPC { get; set; }

        /// <summary>
        /// Pulse charge case. Value: an integer ranging from 0 to 65535. Default value: none. This parameter is optional.
        /// </summary>
        public int PCHG { get; set; }

        /// <summary>
        /// Pulse charge type Options:
        /// 0:KC16(KC16)
        /// 1:KC12(KC12)
        /// 2:POLAR(POLAR)
        /// 3:POLARPUL(POLARPUL)
        /// 4:NULL(NULL)
        /// 5:LOCIMC(Local charge)
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int TFPT { get; set; }

        /// <summary>
        /// Charge mode Options:
        /// 0:CCF(Offline charging)
        /// 1:OCS(Online charging and offline charging)
        /// Default value: none. This parameter is optional.
        /// </summary>
        public bool CHT { get; set; }

        /// <summary>
        /// MCID mode when no reply Options:
        /// 0:NSWNA(NSWNA): The system notifys the subscriber of the MCID information when the subscriber does not answer a call (The system does not send a message to the subscriber.)
        /// 1:SWNA(SWNA): The system notifys the subscriber of the MCID information when the subscriber does not answer the call (The system sends a message to the subscriber).
        /// Default value: none. This parameter is optional.
        /// </summary>
        public bool MCIDMODE { get; set; }

        /// <summary>
        /// MCID Control Mode Options:
        /// 0:CMUC(User Control Mode): The MCID alarm is generated after the subscriber presses the hookflash and dials the MCID access code.
        /// 1:CMSC(System Control Mode): The MCID alarm is generated after the subscriber presses the hookflash.
        /// 255:NOCM(No Control Mode): The same as User Control Mode.
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int MCIDCMODE { get; set; }

        /// <summary>
        /// MCID Alarm Mode Options:
        /// 0:TEMPORARY(Temporary Mode): The system sends an alarm when the MCID service is triggered.
        /// 1:FOREVER(Forever Mode): The system sends an alarm upon receiving an INVITE message.
        /// Default value: none. This parameter is optional.
        /// </summary>
        public bool MCIDAMODE { get; set; }

        /// <summary>
        /// Prepaid Prefix Index. Value: an integer ranging from 0 to 65535. Default value: none. This parameter is optional.
        /// </summary>
        public int PREPAIDIDX { get; set; }

        /// <summary>
        /// CRBT Index. Value: an integer ranging from 0 to 65535. Default value: none. This parameter is optional.
        /// </summary>
        public int CRBTID { get; set; }

        /// <summary>
        /// ODB Barring Incoming Call Type Options:
        /// 0:ODBNOBIC(ODBNOBIC): The system allows incoming calls.
        /// 1:ODBBAIC(ODBBAIC): The system bars incoming calls.
        /// 2:ODBBICROAM(ODBBICROAM): The system bars incoming calls from subscribers roaming out of the country.
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int ODBBICTYPE { get; set; }

        /// <summary>
        /// ODB Barring Outgoing Call Type Options:
        /// 0:ODBNOBOC(ODBNOBOC): The system allows outgoing calls.
        /// 1:ODBBAOC(ODBBAOC): The system bars outgoing calls.
        /// 2:ODBBOIC(ODBBOIC): The system bars international outgoing calls.
        /// 3:ODBBOICEXHC(ODBBOICEXHC): The system bars international outgoing calls to non-home countries.
        /// 4:ODBBOCROAM(ODBBOCROAM): The system bars outgoing calls when subscribers are roaming.
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int ODBBOCTYPE { get; set; }

        /// <summary>
        /// ODB Barring Roaming Type Options:
        /// 0:ODBNOBAR(ODBNOBAR): The system allows roaming.
        /// 1:ODBBROHPLMN(ODBBROHPLMN): The system bars the roaming out of the home network.
        /// 2:ODBBROHPLMNC(ODBBROHPLMNC): The system bars the roaming out of the home country.
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int ODBBARTYPE { get; set; }

        /// <summary>
        /// ODB Barring Supplementary Services Options:
        /// 0:NO(NO), 1:YES(YES). Default value: none. This parameter is optional.
        /// </summary>
        public bool ODBSS { get; set; }

        /// <summary>
        /// ODB Barring Register Call Forwarded Options:
        /// 0:ODBNOBRCF(ODBNOBRCF): not to bar the registration of all forwarding services.
        /// 1:ODBBRACF(ODBBRACF): to bar the registration of all forwarding services.
        /// 2:ODBBRICF(ODBBRICF): to bar the registration of international forwarding services.
        /// 3:ODBBRICFEXHC(ODBBRICFEXHC): to bar international forwarding to non-home country.
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int ODBBRCFTYPE { get; set; }

        /// <summary>
        /// Send P-Notification or not Options:
        /// 0:NO(NO), 1:YES(YES). Default value: none. This parameter is optional.
        /// </summary>
        public bool PNOTI { get; set; }

        /// <summary>
        /// limitation of parallel calls. Value: an integer ranging from 1 to 254. Default value: none. This parameter is optional.
        /// </summary>
        public int MAXPARACALL { get; set; }

        /// <summary>
        /// ATS Determine User Busy Options:
        /// 0:NO(NO), 1:YES(YES). Default value: none. This parameter is optional.
        /// </summary>
        public bool ATSDTMBUSY { get; set; }

        /// <summary>
        /// Calls count mode Options:
        /// 0:COUNT_ALL(COUNT_ALL): to count all calls.
        /// 1:COUNT_CR(COUNT_CR): to count the calls using speech channels.
        /// 2:COUNT_ALL_IRS(COUNT_ALL_IRS): to count the calls in explicit registration sets.
        /// 3:COUNT_CR_IRS(COUNT_CR_IRS): to count the calls using implicit registration channels.
        /// 4:COUNT_CR_EXTERN_IRS(COUNT_CR_EXTERN_IRS): to count the external calls of implicit registration.
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int CALLCOUNT { get; set; }

        /// <summary>
        /// CD notify caller Options:
        /// 0:NO(NO), 1:YES(YES). Default value: none. This parameter is optional.
        /// </summary>
        public bool CDNOTICALLER { get; set; }

        /// <summary>
        /// Charge Flag Options:
        /// 0:CHARGE(Generate offline CDR)
        /// 1:NOT_CHARGE(Not generate offline CDR)
        /// Default value: none. This parameter is optional.
        /// </summary>
        public bool ISCHGFLAG { get; set; }

        /// <summary>
        /// Charging Category Options:
        /// 0:NORMAL_USER(NORMAL_USER)
        /// 1:FREE_USER(FREE_USER)
        /// 2:PREPAID_USER(PREPAID_USER)
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int CHC { get; set; }

        /// <summary>
        /// Multi-AS Service Sharing VPN User Flag. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool CUSER { get; set; }

        /// <summary>
        /// Multi-AS Service Sharing VPN Group NO. Value: a string of a maximum of 31 characters. Default value: none. This parameter is optional.
        /// </summary>
        public string CGRP { get; set; }

        /// <summary>
        /// Multi-AS Service Sharing VPN User Group NO. Value: a string of a maximum of 31 characters. Default value: none. This parameter is optional.
        /// </summary>
        public string CUSERGRP { get; set; }

        /// <summary>
        /// Support tight coupling flag. Value: an integer ranging from 0 to 1. Default value: none. This parameter is optional.
        /// </summary>
        public bool STCF { get; set; }

        /// <summary>
        /// Charge Source Code. Value: an integer ranging from 0 to 65535. Default value: none. This parameter is optional.
        /// </summary>
        public int CHARSC { get; set; }

        /// <summary>
        /// Regulation index. Value: an integer ranging from 0 to 254. Default value: none. This parameter is optional.
        /// </summary>
        public int REGUIDX { get; set; }

        /// <summary>
        /// SOCB Function Options:
        /// 0:SOCB-P(Special Type Outgoing Call Barring: Play Prompt Tone): The call is connected after a specified announcement is played.
        /// 1:SOCB-R(Special Type Outgoing Call Barring: Route to Service Center): The call is routed to the specified service platform.
        /// 255:NULL(NULL)
        /// Default value: none. This parameter is optional.
        /// </summary>
        public int SOCBFUNC { get; set; }

        /// <summary>
        /// SOCBP Tone Index. Value: an integer ranging from 0 to 65535. Default value: none. This parameter is optional.
        /// </summary>
        public int SOCBPTONEIDX { get; set; }

        /// <summary>
        // Admin control CBA Options:
        /// 0:NO(NO), 1:YES(YES). Default value: none. This parameter is optional.
        /// </summary>
        public bool ADMINCBA { get; set; }

        /// <summary>
        /// Admin controlled call diversion Options:
        /// 0:NO(NO), 1:YES(YES). Default value: none. This parameter is optional.
        /// </summary>
        public bool ADCONTROL_DIVERSION { get; set; }

        /// <summary>
        /// Dial Profile. Value: a string of a maximum of 20 characters. Default value: none. This parameter is optional.
        /// </summary>
        public string DPR { get; set; }

        /// <summary>
        /// Profile Name. Value: a string of a maximum of 20 characters. Default value: none. This parameter is optional.
        /// </summary>
        public string PRON { get; set; }

        /// <summary>
        /// Caller type. Value: an integer ranging from 0 to 255. Default value: none. This parameter is optional.
        /// </summary>
        public int CPCRUS { get; set; }

        /// <summary>
        /// Customized user category Options:
        /// 0:NORMAL(Normal), 1:LWM(Local with more). Default value: none. This parameter is optional.
        /// </summary>
        public bool CUSCAT { get; set; }

        /// <summary>
        /// Support 100rel flag Options:
        /// 0:NO(NO), 1:YES(YES). Default value: none. This parameter is optional.
        /// </summary>
        public bool SPT100REL { get; set; }

        /// <summary/>
        public DateTime Created { get; set; }

        /// <summary/>
        public DateTime Updated { get; set; }

        /// <summary/>
        public DateTime Inspected { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static HuSbr Read(string impu)
        {
            HuSbr huSbr;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                huSbr = (from q in db.HuSbrs where q.IMPU == impu select q).SingleOrDefault();
            }

            return huSbr;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<HuSbr> ReadList(ArrayList impuList)
        {
            long i;
            long[] sp;
            List<HuSbr> huSbrList;

            i = 0;
            sp = new long[impuList.Count];

            foreach (long l in impuList) sp[i++] = l;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                //serviceList = (from q in db.Services where dnList.Contains(q.DN) select q).ToList();

                // var pages = context.Pages.Where(x => keys.Any(key => x.Title.Contains(key)));
                huSbrList = db.HuSbrs.Where(q => sp.Any(v => q.IMPU == v.ToString())).ToList();
            }

            return huSbrList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Equal(HuSbr huSbr)
        {
            // below: this will not check the Id, Created, Updated, or Viewed fields
            bool areEqual;

            if (this.IMPU != huSbr.IMPU) areEqual = false;
            else if (this.TEMPLATEIDX != huSbr.TEMPLATEIDX) { areEqual = false; }
            else if (this.DSPIDX != huSbr.DSPIDX) { areEqual = false; }
            else if (this.LP != huSbr.LP) { areEqual = false; }
            else if (this.CSC != huSbr.CSC) { areEqual = false; }
            else if (this.UNAME != huSbr.UNAME) { areEqual = false; }
            else if (this.UTYPE != huSbr.UTYPE) { areEqual = false; }
            else if (this.VCCFLAG != huSbr.VCCFLAG) { areEqual = false; }
            else if (this.VTFLAG != huSbr.VTFLAG) { areEqual = false; }
            else if (this.NSCFU != huSbr.NSCFU) { areEqual = false; }
            else if (this.NSCFUVM != huSbr.NSCFUVM) { areEqual = false; }
            else if (this.NSCFB != huSbr.NSCFB) { areEqual = false; }
            else if (this.NSCFBVM != huSbr.NSCFBVM) { areEqual = false; }
            else if (this.NSCFNR != huSbr.NSCFNR) { areEqual = false; }
            else if (this.NSCFNRVM != huSbr.NSCFNRVM) { areEqual = false; }
            else if (this.NSCFNL != huSbr.NSCFNL) { areEqual = false; }
            else if (this.NSCFNLVM != huSbr.NSCFNLVM) { areEqual = false; }
            else if (this.NSCD != huSbr.NSCD) { areEqual = false; }
            else if (this.NSCDVM != huSbr.NSCDVM) { areEqual = false; }
            else if (this.NSCFNRC != huSbr.NSCFNRC) { areEqual = false; }
            else if (this.NSCFNRCVM != huSbr.NSCFNRCVM) { areEqual = false; }
            else if (this.NSCLIP != huSbr.NSCLIP) { areEqual = false; }
            else if (this.NSCIDCW != huSbr.NSCIDCW) { areEqual = false; }
            else if (this.NSRIO != huSbr.NSRIO) { areEqual = false; }
            else if (this.NSCNIP != huSbr.NSCNIP) { areEqual = false; }
            else if (this.NSCLIR != huSbr.NSCLIR) { areEqual = false; }
            else if (this.NSRIP != huSbr.NSRIP) { areEqual = false; }
            else if (this.NSCNIR != huSbr.NSCNIR) { areEqual = false; }
            else if (this.NSRID != huSbr.NSRID) { areEqual = false; }
            else if (this.NSNRID != huSbr.NSNRID) { areEqual = false; }
            else if (this.NSRND != huSbr.NSRND) { areEqual = false; }
            else if (this.NSNRND != huSbr.NSNRND) { areEqual = false; }
            else if (this.NSCW != huSbr.NSCW) { areEqual = false; }
            else if (this.NSCCW != huSbr.NSCCW) { areEqual = false; }
            else if (this.NSOIP != huSbr.NSOIP) { areEqual = false; }
            else if (this.NSACRM != huSbr.NSACRM) { areEqual = false; }
            else if (this.NSGOIR != huSbr.NSGOIR) { areEqual = false; }
            else if (this.NSMOIR != huSbr.NSMOIR) { areEqual = false; }
            else if (this.NSTIP != huSbr.NSTIP) { areEqual = false; }
            else if (this.NSTIR != huSbr.NSTIR) { areEqual = false; }
            else if (this.NSOTIR != huSbr.NSOTIR) { areEqual = false; }
            else if (this.NSCLIPNOSCREENING != huSbr.NSCLIPNOSCREENING) { areEqual = false; }
            else if (this.NSCR != huSbr.NSCR) { areEqual = false; }
            else if (this.NSWAKE_UP != huSbr.NSWAKE_UP) { areEqual = false; }
            else if (this.NSAOC_D != huSbr.NSAOC_D) { areEqual = false; }
            else if (this.NSAOC_E != huSbr.NSAOC_E) { areEqual = false; }
            else if (this.NSXEXH != huSbr.NSXEXH) { areEqual = false; }
            else if (this.NSXEGJ != huSbr.NSXEGJ) { areEqual = false; }
            else if (this.NSCWCFNR != huSbr.NSCWCFNR) { areEqual = false; }
            else if (this.NSIIFC != huSbr.NSIIFC) { areEqual = false; }
            else if (this.NSDN_CALL_OUT_BAR != huSbr.NSDN_CALL_OUT_BAR) { areEqual = false; }
            else if (this.NSCCBS != huSbr.NSCCBS) { areEqual = false; }
            else if (this.NSCCNR != huSbr.NSCCNR) { areEqual = false; }
            else if (this.NSCCBSR != huSbr.NSCCBSR) { areEqual = false; }
            else if (this.NSCCNRR != huSbr.NSCCNRR) { areEqual = false; }
            else if (this.NS3PTY != huSbr.NS3PTY) { areEqual = false; }
            else if (this.NSNPTY != huSbr.NSNPTY) { areEqual = false; }
            else if (this.NSDND != huSbr.NSDND) { areEqual = false; }
            else if (this.NSMCR != huSbr.NSMCR) { areEqual = false; }
            else if (this.NSCBA != huSbr.NSCBA) { areEqual = false; }
            else if (this.NSTMP_LIN != huSbr.NSTMP_LIN) { areEqual = false; }
            else if (this.NSCODEC_CNTRL != huSbr.NSCODEC_CNTRL) { areEqual = false; }
            else if (this.NSMWI != huSbr.NSMWI) { areEqual = false; }
            else if (this.NSDC != huSbr.NSDC) { areEqual = false; }
            else if (this.NSHOLD != huSbr.NSHOLD) { areEqual = false; }
            else if (this.NSECT != huSbr.NSECT) { areEqual = false; }
            else if (this.NSCFTB != huSbr.NSCFTB) { areEqual = false; }
            else if (this.NSDAN != huSbr.NSDAN) { areEqual = false; }
            else if (this.NSSTOP_SECRET != huSbr.NSSTOP_SECRET) { areEqual = false; }
            else if (this.NSMCID != huSbr.NSMCID) { areEqual = false; }
            else if (this.NSEBO != huSbr.NSEBO) { areEqual = false; }
            else if (this.NSICO != huSbr.NSICO) { areEqual = false; }
            else if (this.NSOUTG != huSbr.NSOUTG) { areEqual = false; }
            else if (this.NSINQYH != huSbr.NSINQYH) { areEqual = false; }
            else if (this.NSUINFO != huSbr.NSUINFO) { areEqual = false; }
            else if (this.NSDN_CALL_OUT_ALLOW != huSbr.NSDN_CALL_OUT_ALLOW) { areEqual = false; }
            else if (this.NSSIC != huSbr.NSSIC) { areEqual = false; }
            else if (this.NSSOC != huSbr.NSSOC) { areEqual = false; }
            else if (this.NSSETCFNRTIME != huSbr.NSSETCFNRTIME) { areEqual = false; }
            else if (this.NSCFS != huSbr.NSCFS) { areEqual = false; }
            else if (this.NSCFSB != huSbr.NSCFSB) { areEqual = false; }
            else if (this.NSFAX != huSbr.NSFAX) { areEqual = false; }
            else if (this.NSABRC != huSbr.NSABRC) { areEqual = false; }
            else if (this.NSACRTOVM != huSbr.NSACRTOVM) { areEqual = false; }
            else if (this.NSPREPAID != huSbr.NSPREPAID) { areEqual = false; }
            else if (this.NSCRBT != huSbr.NSCRBT) { areEqual = false; }
            else if (this.NSICB != huSbr.NSICB) { areEqual = false; }
            else if (this.NSMRINGING != huSbr.NSMRINGING) { areEqual = false; }
            else if (this.NSCIS != huSbr.NSCIS) { areEqual = false; }
            else if (this.NSCBEG != huSbr.NSCBEG) { areEqual = false; }
            else if (this.NSCOLP != huSbr.NSCOLP) { areEqual = false; }
            else if (this.NSCOLR != huSbr.NSCOLR) { areEqual = false; }
            else if (this.NSCOLPOVR != huSbr.NSCOLPOVR) { areEqual = false; }
            else if (this.NSBAOC != huSbr.NSBAOC) { areEqual = false; }
            else if (this.NSBOIC != huSbr.NSBOIC) { areEqual = false; }
            else if (this.NSBOICEXHC != huSbr.NSBOICEXHC) { areEqual = false; }
            else if (this.NSBAIC != huSbr.NSBAIC) { areEqual = false; }
            else if (this.NSBICROM != huSbr.NSBICROM) { areEqual = false; }
            else if (this.NSSPEED_DIAL != huSbr.NSSPEED_DIAL) { areEqual = false; }
            else if (this.NSSD1D != huSbr.NSSD1D) { areEqual = false; }
            else if (this.NSSD2D != huSbr.NSSD2D) { areEqual = false; }
            else if (this.NSGRNCALL != huSbr.NSGRNCALL) { areEqual = false; }
            else if (this.NSCPARK != huSbr.NSCPARK) { areEqual = false; }
            else if (this.NSGAA != huSbr.NSGAA) { areEqual = false; }
            else if (this.NSQSNS != huSbr.NSQSNS) { areEqual = false; }
            else if (this.NSMSN != huSbr.NSMSN) { areEqual = false; }
            else if (this.NSHOTLINE != huSbr.NSHOTLINE) { areEqual = false; }
            else if (this.NSAOC_S != huSbr.NSAOC_S) { areEqual = false; }
            else if (this.NSNIGHTSRV != huSbr.NSNIGHTSRV) { areEqual = false; }
            else if (this.NSBACKNUM != huSbr.NSBACKNUM) { areEqual = false; }
            else if (this.NSAUTOCON != huSbr.NSAUTOCON) { areEqual = false; }
            else if (this.NSCAMPON != huSbr.NSCAMPON) { areEqual = false; }
            else if (this.NSCTD != huSbr.NSCTD) { areEqual = false; }
            else if (this.NSCLICKHOLD != huSbr.NSCLICKHOLD) { areEqual = false; }
            else if (this.NSQUEUE != huSbr.NSQUEUE) { areEqual = false; }
            else if (this.NSSANSWER != huSbr.NSSANSWER) { areEqual = false; }
            else if (this.NSICENCF != huSbr.NSICENCF) { areEqual = false; }
            else if (this.NSCFGO != huSbr.NSCFGO) { areEqual = false; }
            else if (this.NSCECT != huSbr.NSCECT) { areEqual = false; }
            else if (this.NSCTGO != huSbr.NSCTGO) { areEqual = false; }
            else if (this.NSCTIO != huSbr.NSCTIO) { areEqual = false; }
            else if (this.NSSETBUSY != huSbr.NSSETBUSY) { areEqual = false; }
            else if (this.NSOVERSTEP != huSbr.NSOVERSTEP) { areEqual = false; }
            else if (this.NSABSENT != huSbr.NSABSENT) { areEqual = false; }
            else if (this.NSMONITOR != huSbr.NSMONITOR) { areEqual = false; }
            else if (this.NSFMONITOR != huSbr.NSFMONITOR) { areEqual = false; }
            else if (this.NSDISCNT != huSbr.NSDISCNT) { areEqual = false; }
            else if (this.NSFDISCNT != huSbr.NSFDISCNT) { areEqual = false; }
            else if (this.NSINSERT != huSbr.NSINSERT) { areEqual = false; }
            else if (this.NSFINSERT != huSbr.NSFINSERT) { areEqual = false; }
            else if (this.NSASI != huSbr.NSASI) { areEqual = false; }
            else if (this.NSPWCB != huSbr.NSPWCB) { areEqual = false; }
            else if (this.NSRD != huSbr.NSRD) { areEqual = false; }
            else if (this.NSLCPS != huSbr.NSLCPS) { areEqual = false; }
            else if (this.NSNCPS != huSbr.NSNCPS) { areEqual = false; }
            else if (this.NSICPS != huSbr.NSICPS) { areEqual = false; }
            else if (this.NSCBCLOCK != huSbr.NSCBCLOCK) { areEqual = false; }
            else if (this.NSMINIBAR != huSbr.NSMINIBAR) { areEqual = false; }
            else if (this.NSMCN != huSbr.NSMCN) { areEqual = false; }
            else if (this.NSDSTR != huSbr.NSDSTR) { areEqual = false; }
            else if (this.NSOPRREG != huSbr.NSOPRREG) { areEqual = false; }
            else if (this.NSONEKEY != huSbr.NSONEKEY) { areEqual = false; }
            else if (this.NSINBOUND != huSbr.NSINBOUND) { areEqual = false; }
            else if (this.NSOUTBOUND != huSbr.NSOUTBOUND) { areEqual = false; }
            else if (this.NSCALLERID != huSbr.NSCALLERID) { areEqual = false; }
            else if (this.NSCUN != huSbr.NSCUN) { areEqual = false; }
            else if (this.NSIPTVVC != huSbr.NSIPTVVC) { areEqual = false; }
            else if (this.NSNP != huSbr.NSNP) { areEqual = false; }
            else if (this.NSSEC != huSbr.NSSEC) { areEqual = false; }
            else if (this.NSSECSTA != huSbr.NSSECSTA) { areEqual = false; }
            else if (this.NSHRCN != huSbr.NSHRCN) { areEqual = false; }
            else if (this.NSSB != huSbr.NSSB) { areEqual = false; }
            else if (this.NSOCCR != huSbr.NSOCCR) { areEqual = false; }
            else if (this.LCO != huSbr.LCO) { areEqual = false; }
            else if (this.LC != huSbr.LC) { areEqual = false; }
            else if (this.LCT != huSbr.LCT) { areEqual = false; }
            else if (this.NTT != huSbr.NTT) { areEqual = false; }
            else if (this.ITT != huSbr.ITT) { areEqual = false; }
            else if (this.ICTX != huSbr.ICTX) { areEqual = false; }
            else if (this.OCTX != huSbr.OCTX) { areEqual = false; }
            else if (this.INTT != huSbr.INTT) { areEqual = false; }
            else if (this.IITT != huSbr.IITT) { areEqual = false; }
            else if (this.ICLT != huSbr.ICLT) { areEqual = false; }
            else if (this.ICDDD != huSbr.ICDDD) { areEqual = false; }
            else if (this.ICIDD != huSbr.ICIDD) { areEqual = false; }
            else if (this.IOLT != huSbr.IOLT) { areEqual = false; }
            else if (this.CTLCO != huSbr.CTLCO) { areEqual = false; }
            else if (this.CTLCT != huSbr.CTLCT) { areEqual = false; }
            else if (this.CTLD != huSbr.CTLD) { areEqual = false; }
            else if (this.CTINTNANP != huSbr.CTINTNANP) { areEqual = false; }
            else if (this.CTINTWORLD != huSbr.CTINTWORLD) { areEqual = false; }
            else if (this.CTDA != huSbr.CTDA) { areEqual = false; }
            else if (this.CTOSM != huSbr.CTOSM) { areEqual = false; }
            else if (this.CTOSP != huSbr.CTOSP) { areEqual = false; }
            else if (this.CTOSP1 != huSbr.CTOSP1) { areEqual = false; }
            else if (this.CCO1 != huSbr.CCO1) { areEqual = false; }
            else if (this.CCO2 != huSbr.CCO2) { areEqual = false; }
            else if (this.CCO3 != huSbr.CCO3) { areEqual = false; }
            else if (this.CCO4 != huSbr.CCO4) { areEqual = false; }
            else if (this.CCO5 != huSbr.CCO5) { areEqual = false; }
            else if (this.CCO6 != huSbr.CCO6) { areEqual = false; }
            else if (this.CCO7 != huSbr.CCO7) { areEqual = false; }
            else if (this.CCO8 != huSbr.CCO8) { areEqual = false; }
            else if (this.CCO9 != huSbr.CCO9) { areEqual = false; }
            else if (this.CCO10 != huSbr.CCO10) { areEqual = false; }
            else if (this.CCO11 != huSbr.CCO11) { areEqual = false; }
            else if (this.CCO12 != huSbr.CCO12) { areEqual = false; }
            else if (this.CCO13 != huSbr.CCO13) { areEqual = false; }
            else if (this.CCO14 != huSbr.CCO14) { areEqual = false; }
            else if (this.CCO15 != huSbr.CCO15) { areEqual = false; }
            else if (this.CCO16 != huSbr.CCO16) { areEqual = false; }
            else if (this.HIGHENTCO != huSbr.HIGHENTCO) { areEqual = false; }
            else if (this.OPERATOR != huSbr.OPERATOR) { areEqual = false; }
            else if (this.SUPYSRV != huSbr.SUPYSRV) { areEqual = false; }
            else if (this.IDDCI != huSbr.IDDCI) { areEqual = false; }
            else if (this.NTCI != huSbr.NTCI) { areEqual = false; }
            else if (this.LTCI != huSbr.LTCI) { areEqual = false; }
            else if (this.RSC != huSbr.RSC) { areEqual = false; }
            else if (this.CIG != huSbr.CIG) { areEqual = false; }
            else if (this.OUTRST != huSbr.OUTRST) { areEqual = false; }
            else if (this.INRST != huSbr.INRST) { areEqual = false; }
            else if (this.NOAT != huSbr.NOAT) { areEqual = false; }
            else if (this.RINGCOUNT != huSbr.RINGCOUNT) { areEqual = false; }
            else if (this.VMAIND != huSbr.VMAIND) { areEqual = false; }
            else if (this.VDMAIND != huSbr.VDMAIND) { areEqual = false; }
            else if (this.TGRP != huSbr.TGRP) { areEqual = false; }
            else if (this.TIDHLD != huSbr.TIDHLD) { areEqual = false; }
            else if (this.TIDCW != huSbr.TIDCW) { areEqual = false; }
            else if (this.SCF != huSbr.SCF) { areEqual = false; }
            else if (this.LMTGRP != huSbr.LMTGRP) { areEqual = false; }
            else if (this.FLBGRP != huSbr.FLBGRP) { areEqual = false; }
            else if (this.SLBGRP != huSbr.SLBGRP) { areEqual = false; }
            else if (this.COP != huSbr.COP) { areEqual = false; }
            else if (this.G711_64K_A_LAW != huSbr.G711_64K_A_LAW) { areEqual = false; }
            else if (this.G711_64K_U_LAW != huSbr.G711_64K_U_LAW) { areEqual = false; }
            else if (this.G722 != huSbr.G722) { areEqual = false; }
            else if (this.G723 != huSbr.G723) { areEqual = false; }
            else if (this.G726 != huSbr.G726) { areEqual = false; }
            else if (this.G728 != huSbr.G728) { areEqual = false; }
            else if (this.G729 != huSbr.G729) { areEqual = false; }
            else if (this.CODEC_MP4A != huSbr.CODEC_MP4A) { areEqual = false; }
            else if (this.CODEC2833 != huSbr.CODEC2833) { areEqual = false; }
            else if (this.CODEC2198 != huSbr.CODEC2198) { areEqual = false; }
            else if (this.G726_40 != huSbr.G726_40) { areEqual = false; }
            else if (this.G726_32 != huSbr.G726_32) { areEqual = false; }
            else if (this.G726_24 != huSbr.G726_24) { areEqual = false; }
            else if (this.G726_16 != huSbr.G726_16) { areEqual = false; }
            else if (this.AMR != huSbr.AMR) { areEqual = false; }
            else if (this.CLEARMODE != huSbr.CLEARMODE) { areEqual = false; }
            else if (this.ILBC != huSbr.ILBC) { areEqual = false; }
            else if (this.SPEEX != huSbr.SPEEX) { areEqual = false; }
            else if (this.G729EV != huSbr.G729EV) { areEqual = false; }
            else if (this.EVRC != huSbr.EVRC) { areEqual = false; }
            else if (this.EVRCB != huSbr.EVRCB) { areEqual = false; }
            else if (this.H261 != huSbr.H261) { areEqual = false; }
            else if (this.H263 != huSbr.H263) { areEqual = false; }
            else if (this.CODEC_MP4V != huSbr.CODEC_MP4V) { areEqual = false; }
            else if (this.H264 != huSbr.H264) { areEqual = false; }
            else if (this.T38 != huSbr.T38) { areEqual = false; }
            else if (this.T120 != huSbr.T120) { areEqual = false; }
            else if (this.G711A_VBD != huSbr.G711A_VBD) { areEqual = false; }
            else if (this.G711U_VBD != huSbr.G711U_VBD) { areEqual = false; }
            else if (this.G726_VBD != huSbr.G726_VBD) { areEqual = false; }
            else if (this.G726_40_VBD != huSbr.G726_40_VBD) { areEqual = false; }
            else if (this.G726_32_VBD != huSbr.G726_32_VBD) { areEqual = false; }
            else if (this.G726_24_VBD != huSbr.G726_24_VBD) { areEqual = false; }
            else if (this.G726_16_VBD != huSbr.G726_16_VBD) { areEqual = false; }
            else if (this.WIND_BAND_AMR != huSbr.WIND_BAND_AMR) { areEqual = false; }
            else if (this.GSM610 != huSbr.GSM610) { areEqual = false; }
            else if (this.H263_2000 != huSbr.H263_2000) { areEqual = false; }
            else if (this.BROADVOICE_32 != huSbr.BROADVOICE_32) { areEqual = false; }
            else if (this.UNKNOWN_CODEC != huSbr.UNKNOWN_CODEC) { areEqual = false; }
            else if (this.ACODEC != huSbr.ACODEC) { areEqual = false; }
            else if (this.VCODEC != huSbr.VCODEC) { areEqual = false; }
            else if (this.POLIDX != huSbr.POLIDX) { areEqual = false; }
            else if (this.NCPI != huSbr.NCPI) { areEqual = false; }
            else if (this.ICPI != huSbr.ICPI) { areEqual = false; }
            else if (this.EBOCL != huSbr.EBOCL) { areEqual = false; }
            else if (this.EBOPL != huSbr.EBOPL) { areEqual = false; }
            else if (this.EBOIT != huSbr.EBOIT) { areEqual = false; }
            else if (this.RM != huSbr.RM) { areEqual = false; }
            else if (this.CPC != huSbr.CPC) { areEqual = false; }
            else if (this.PCHG != huSbr.PCHG) { areEqual = false; }
            else if (this.TFPT != huSbr.TFPT) { areEqual = false; }
            else if (this.CHT != huSbr.CHT) { areEqual = false; }
            else if (this.MCIDMODE != huSbr.MCIDMODE) { areEqual = false; }
            else if (this.MCIDCMODE != huSbr.MCIDCMODE) { areEqual = false; }
            else if (this.MCIDAMODE != huSbr.MCIDAMODE) { areEqual = false; }
            else if (this.PREPAIDIDX != huSbr.PREPAIDIDX) { areEqual = false; }
            else if (this.CRBTID != huSbr.CRBTID) { areEqual = false; }
            else if (this.ODBBICTYPE != huSbr.ODBBICTYPE) { areEqual = false; }
            else if (this.ODBBOCTYPE != huSbr.ODBBOCTYPE) { areEqual = false; }
            else if (this.ODBBARTYPE != huSbr.ODBBARTYPE) { areEqual = false; }
            else if (this.ODBSS != huSbr.ODBSS) { areEqual = false; }
            else if (this.ODBBRCFTYPE != huSbr.ODBBRCFTYPE) { areEqual = false; }
            else if (this.PNOTI != huSbr.PNOTI) { areEqual = false; }
            else if (this.MAXPARACALL != huSbr.MAXPARACALL) { areEqual = false; }
            else if (this.ATSDTMBUSY != huSbr.ATSDTMBUSY) { areEqual = false; }
            else if (this.CALLCOUNT != huSbr.CALLCOUNT) { areEqual = false; }
            else if (this.CDNOTICALLER != huSbr.CDNOTICALLER) { areEqual = false; }
            else if (this.ISCHGFLAG != huSbr.ISCHGFLAG) { areEqual = false; }
            else if (this.CHC != huSbr.CHC) { areEqual = false; }
            else if (this.CUSER != huSbr.CUSER) { areEqual = false; }
            else if (this.CGRP != huSbr.CGRP) { areEqual = false; }
            else if (this.CUSERGRP != huSbr.CUSERGRP) { areEqual = false; }
            else if (this.STCF != huSbr.STCF) { areEqual = false; }
            else if (this.CHARSC != huSbr.CHARSC) { areEqual = false; }
            else if (this.REGUIDX != huSbr.REGUIDX) { areEqual = false; }
            else if (this.SOCBFUNC != huSbr.SOCBFUNC) { areEqual = false; }
            else if (this.SOCBPTONEIDX != huSbr.SOCBPTONEIDX) { areEqual = false; }
            else if (this.ADMINCBA != huSbr.ADMINCBA) { areEqual = false; }
            else if (this.ADCONTROL_DIVERSION != huSbr.ADCONTROL_DIVERSION) { areEqual = false; }
            else if (this.DPR != huSbr.DPR) { areEqual = false; }
            else if (this.PRON != huSbr.PRON) { areEqual = false; }
            else if (this.CPCRUS != huSbr.CPCRUS) { areEqual = false; }
            else if (this.CUSCAT != huSbr.CUSCAT) { areEqual = false; }
            else if (this.SPT100REL != huSbr.SPT100REL) { areEqual = false; }
            else areEqual = true;

            return areEqual;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Update(HuSbr huSbr)
        {
            // below: this will not update Id, IMPU, Created
            bool updated;

            updated = false;

            if (this.TEMPLATEIDX != huSbr.TEMPLATEIDX) { this.TEMPLATEIDX = huSbr.TEMPLATEIDX; updated = true; }
            if (this.DSPIDX != huSbr.DSPIDX) { this.DSPIDX = huSbr.DSPIDX; updated = true; }
            if (this.LP != huSbr.LP) { this.LP = huSbr.LP; updated = true; }
            if (this.CSC != huSbr.CSC) { this.CSC = huSbr.CSC; updated = true; }
            if (this.UNAME != huSbr.UNAME) { this.UNAME = huSbr.UNAME; updated = true; }
            if (this.UTYPE != huSbr.UTYPE) { this.UTYPE = huSbr.UTYPE; updated = true; }
            if (this.VCCFLAG != huSbr.VCCFLAG) { this.VCCFLAG = huSbr.VCCFLAG; updated = true; }
            if (this.VTFLAG != huSbr.VTFLAG) { this.VTFLAG = huSbr.VTFLAG; updated = true; }
            if (this.NSCFU != huSbr.NSCFU) { this.NSCFU = huSbr.NSCFU; updated = true; }
            if (this.NSCFUVM != huSbr.NSCFUVM) { this.NSCFUVM = huSbr.NSCFUVM; updated = true; }
            if (this.NSCFB != huSbr.NSCFB) { this.NSCFB = huSbr.NSCFB; updated = true; }
            if (this.NSCFBVM != huSbr.NSCFBVM) { this.NSCFBVM = huSbr.NSCFBVM; updated = true; }
            if (this.NSCFNR != huSbr.NSCFNR) { this.NSCFNR = huSbr.NSCFNR; updated = true; }
            if (this.NSCFNRVM != huSbr.NSCFNRVM) { this.NSCFNRVM = huSbr.NSCFNRVM; updated = true; }
            if (this.NSCFNL != huSbr.NSCFNL) { this.NSCFNL = huSbr.NSCFNL; updated = true; }
            if (this.NSCFNLVM != huSbr.NSCFNLVM) { this.NSCFNLVM = huSbr.NSCFNLVM; updated = true; }
            if (this.NSCD != huSbr.NSCD) { this.NSCD = huSbr.NSCD; updated = true; }
            if (this.NSCDVM != huSbr.NSCDVM) { this.NSCDVM = huSbr.NSCDVM; updated = true; }
            if (this.NSCFNRC != huSbr.NSCFNRC) { this.NSCFNRC = huSbr.NSCFNRC; updated = true; }
            if (this.NSCFNRCVM != huSbr.NSCFNRCVM) { this.NSCFNRCVM = huSbr.NSCFNRCVM; updated = true; }
            if (this.NSCLIP != huSbr.NSCLIP) { this.NSCLIP = huSbr.NSCLIP; updated = true; }
            if (this.NSCIDCW != huSbr.NSCIDCW) { this.NSCIDCW = huSbr.NSCIDCW; updated = true; }
            if (this.NSRIO != huSbr.NSRIO) { this.NSRIO = huSbr.NSRIO; updated = true; }
            if (this.NSCNIP != huSbr.NSCNIP) { this.NSCNIP = huSbr.NSCNIP; updated = true; }
            if (this.NSCLIR != huSbr.NSCLIR) { this.NSCLIR = huSbr.NSCLIR; updated = true; }
            if (this.NSRIP != huSbr.NSRIP) { this.NSRIP = huSbr.NSRIP; updated = true; }
            if (this.NSCNIR != huSbr.NSCNIR) { this.NSCNIR = huSbr.NSCNIR; updated = true; }
            if (this.NSRID != huSbr.NSRID) { this.NSRID = huSbr.NSRID; updated = true; }
            if (this.NSNRID != huSbr.NSNRID) { this.NSNRID = huSbr.NSNRID; updated = true; }
            if (this.NSRND != huSbr.NSRND) { this.NSRND = huSbr.NSRND; updated = true; }
            if (this.NSNRND != huSbr.NSNRND) { this.NSNRND = huSbr.NSNRND; updated = true; }
            if (this.NSCW != huSbr.NSCW) { this.NSCW = huSbr.NSCW; updated = true; }
            if (this.NSCCW != huSbr.NSCCW) { this.NSCCW = huSbr.NSCCW; updated = true; }
            if (this.NSOIP != huSbr.NSOIP) { this.NSOIP = huSbr.NSOIP; updated = true; }
            if (this.NSACRM != huSbr.NSACRM) { this.NSACRM = huSbr.NSACRM; updated = true; }
            if (this.NSGOIR != huSbr.NSGOIR) { this.NSGOIR = huSbr.NSGOIR; updated = true; }
            if (this.NSMOIR != huSbr.NSMOIR) { this.NSMOIR = huSbr.NSMOIR; updated = true; }
            if (this.NSTIP != huSbr.NSTIP) { this.NSTIP = huSbr.NSTIP; updated = true; }
            if (this.NSTIR != huSbr.NSTIR) { this.NSTIR = huSbr.NSTIR; updated = true; }
            if (this.NSOTIR != huSbr.NSOTIR) { this.NSOTIR = huSbr.NSOTIR; updated = true; }
            if (this.NSCLIPNOSCREENING != huSbr.NSCLIPNOSCREENING) { this.NSCLIPNOSCREENING = huSbr.NSCLIPNOSCREENING; updated = true; }
            if (this.NSCR != huSbr.NSCR) { this.NSCR = huSbr.NSCR; updated = true; }
            if (this.NSWAKE_UP != huSbr.NSWAKE_UP) { this.NSWAKE_UP = huSbr.NSWAKE_UP; updated = true; }
            if (this.NSAOC_D != huSbr.NSAOC_D) { this.NSAOC_D = huSbr.NSAOC_D; updated = true; }
            if (this.NSAOC_E != huSbr.NSAOC_E) { this.NSAOC_E = huSbr.NSAOC_E; updated = true; }
            if (this.NSXEXH != huSbr.NSXEXH) { this.NSXEXH = huSbr.NSXEXH; updated = true; }
            if (this.NSXEGJ != huSbr.NSXEGJ) { this.NSXEGJ = huSbr.NSXEGJ; updated = true; }
            if (this.NSCWCFNR != huSbr.NSCWCFNR) { this.NSCWCFNR = huSbr.NSCWCFNR; updated = true; }
            if (this.NSIIFC != huSbr.NSIIFC) { this.NSIIFC = huSbr.NSIIFC; updated = true; }
            if (this.NSDN_CALL_OUT_BAR != huSbr.NSDN_CALL_OUT_BAR) { this.NSDN_CALL_OUT_BAR = huSbr.NSDN_CALL_OUT_BAR; updated = true; }
            if (this.NSCCBS != huSbr.NSCCBS) { this.NSCCBS = huSbr.NSCCBS; updated = true; }
            if (this.NSCCNR != huSbr.NSCCNR) { this.NSCCNR = huSbr.NSCCNR; updated = true; }
            if (this.NSCCBSR != huSbr.NSCCBSR) { this.NSCCBSR = huSbr.NSCCBSR; updated = true; }
            if (this.NSCCNRR != huSbr.NSCCNRR) { this.NSCCNRR = huSbr.NSCCNRR; updated = true; }
            if (this.NS3PTY != huSbr.NS3PTY) { this.NS3PTY = huSbr.NS3PTY; updated = true; }
            if (this.NSNPTY != huSbr.NSNPTY) { this.NSNPTY = huSbr.NSNPTY; updated = true; }
            if (this.NSDND != huSbr.NSDND) { this.NSDND = huSbr.NSDND; updated = true; }
            if (this.NSMCR != huSbr.NSMCR) { this.NSMCR = huSbr.NSMCR; updated = true; }
            if (this.NSCBA != huSbr.NSCBA) { this.NSCBA = huSbr.NSCBA; updated = true; }
            if (this.NSTMP_LIN != huSbr.NSTMP_LIN) { this.NSTMP_LIN = huSbr.NSTMP_LIN; updated = true; }
            if (this.NSCODEC_CNTRL != huSbr.NSCODEC_CNTRL) { this.NSCODEC_CNTRL = huSbr.NSCODEC_CNTRL; updated = true; }
            if (this.NSMWI != huSbr.NSMWI) { this.NSMWI = huSbr.NSMWI; updated = true; }
            if (this.NSDC != huSbr.NSDC) { this.NSDC = huSbr.NSDC; updated = true; }
            if (this.NSHOLD != huSbr.NSHOLD) { this.NSHOLD = huSbr.NSHOLD; updated = true; }
            if (this.NSECT != huSbr.NSECT) { this.NSECT = huSbr.NSECT; updated = true; }
            if (this.NSCFTB != huSbr.NSCFTB) { this.NSCFTB = huSbr.NSCFTB; updated = true; }
            if (this.NSDAN != huSbr.NSDAN) { this.NSDAN = huSbr.NSDAN; updated = true; }
            if (this.NSSTOP_SECRET != huSbr.NSSTOP_SECRET) { this.NSSTOP_SECRET = huSbr.NSSTOP_SECRET; updated = true; }
            if (this.NSMCID != huSbr.NSMCID) { this.NSMCID = huSbr.NSMCID; updated = true; }
            if (this.NSEBO != huSbr.NSEBO) { this.NSEBO = huSbr.NSEBO; updated = true; }
            if (this.NSICO != huSbr.NSICO) { this.NSICO = huSbr.NSICO; updated = true; }
            if (this.NSOUTG != huSbr.NSOUTG) { this.NSOUTG = huSbr.NSOUTG; updated = true; }
            if (this.NSINQYH != huSbr.NSINQYH) { this.NSINQYH = huSbr.NSINQYH; updated = true; }
            if (this.NSUINFO != huSbr.NSUINFO) { this.NSUINFO = huSbr.NSUINFO; updated = true; }
            if (this.NSDN_CALL_OUT_ALLOW != huSbr.NSDN_CALL_OUT_ALLOW) { this.NSDN_CALL_OUT_ALLOW = huSbr.NSDN_CALL_OUT_ALLOW; updated = true; }
            if (this.NSSIC != huSbr.NSSIC) { this.NSSIC = huSbr.NSSIC; updated = true; }
            if (this.NSSOC != huSbr.NSSOC) { this.NSSOC = huSbr.NSSOC; updated = true; }
            if (this.NSSETCFNRTIME != huSbr.NSSETCFNRTIME) { this.NSSETCFNRTIME = huSbr.NSSETCFNRTIME; updated = true; }
            if (this.NSCFS != huSbr.NSCFS) { this.NSCFS = huSbr.NSCFS; updated = true; }
            if (this.NSCFSB != huSbr.NSCFSB) { this.NSCFSB = huSbr.NSCFSB; updated = true; }
            if (this.NSFAX != huSbr.NSFAX) { this.NSFAX = huSbr.NSFAX; updated = true; }
            if (this.NSABRC != huSbr.NSABRC) { this.NSABRC = huSbr.NSABRC; updated = true; }
            if (this.NSACRTOVM != huSbr.NSACRTOVM) { this.NSACRTOVM = huSbr.NSACRTOVM; updated = true; }
            if (this.NSPREPAID != huSbr.NSPREPAID) { this.NSPREPAID = huSbr.NSPREPAID; updated = true; }
            if (this.NSCRBT != huSbr.NSCRBT) { this.NSCRBT = huSbr.NSCRBT; updated = true; }
            if (this.NSICB != huSbr.NSICB) { this.NSICB = huSbr.NSICB; updated = true; }
            if (this.NSMRINGING != huSbr.NSMRINGING) { this.NSMRINGING = huSbr.NSMRINGING; updated = true; }
            if (this.NSCIS != huSbr.NSCIS) { this.NSCIS = huSbr.NSCIS; updated = true; }
            if (this.NSCBEG != huSbr.NSCBEG) { this.NSCBEG = huSbr.NSCBEG; updated = true; }
            if (this.NSCOLP != huSbr.NSCOLP) { this.NSCOLP = huSbr.NSCOLP; updated = true; }
            if (this.NSCOLR != huSbr.NSCOLR) { this.NSCOLR = huSbr.NSCOLR; updated = true; }
            if (this.NSCOLPOVR != huSbr.NSCOLPOVR) { this.NSCOLPOVR = huSbr.NSCOLPOVR; updated = true; }
            if (this.NSBAOC != huSbr.NSBAOC) { this.NSBAOC = huSbr.NSBAOC; updated = true; }
            if (this.NSBOIC != huSbr.NSBOIC) { this.NSBOIC = huSbr.NSBOIC; updated = true; }
            if (this.NSBOICEXHC != huSbr.NSBOICEXHC) { this.NSBOICEXHC = huSbr.NSBOICEXHC; updated = true; }
            if (this.NSBAIC != huSbr.NSBAIC) { this.NSBAIC = huSbr.NSBAIC; updated = true; }
            if (this.NSBICROM != huSbr.NSBICROM) { this.NSBICROM = huSbr.NSBICROM; updated = true; }
            if (this.NSSPEED_DIAL != huSbr.NSSPEED_DIAL) { this.NSSPEED_DIAL = huSbr.NSSPEED_DIAL; updated = true; }
            if (this.NSSD1D != huSbr.NSSD1D) { this.NSSD1D = huSbr.NSSD1D; updated = true; }
            if (this.NSSD2D != huSbr.NSSD2D) { this.NSSD2D = huSbr.NSSD2D; updated = true; }
            if (this.NSGRNCALL != huSbr.NSGRNCALL) { this.NSGRNCALL = huSbr.NSGRNCALL; updated = true; }
            if (this.NSCPARK != huSbr.NSCPARK) { this.NSCPARK = huSbr.NSCPARK; updated = true; }
            if (this.NSGAA != huSbr.NSGAA) { this.NSGAA = huSbr.NSGAA; updated = true; }
            if (this.NSQSNS != huSbr.NSQSNS) { this.NSQSNS = huSbr.NSQSNS; updated = true; }
            if (this.NSMSN != huSbr.NSMSN) { this.NSMSN = huSbr.NSMSN; updated = true; }
            if (this.NSHOTLINE != huSbr.NSHOTLINE) { this.NSHOTLINE = huSbr.NSHOTLINE; updated = true; }
            if (this.NSAOC_S != huSbr.NSAOC_S) { this.NSAOC_S = huSbr.NSAOC_S; updated = true; }
            if (this.NSNIGHTSRV != huSbr.NSNIGHTSRV) { this.NSNIGHTSRV = huSbr.NSNIGHTSRV; updated = true; }
            if (this.NSBACKNUM != huSbr.NSBACKNUM) { this.NSBACKNUM = huSbr.NSBACKNUM; updated = true; }
            if (this.NSAUTOCON != huSbr.NSAUTOCON) { this.NSAUTOCON = huSbr.NSAUTOCON; updated = true; }
            if (this.NSCAMPON != huSbr.NSCAMPON) { this.NSCAMPON = huSbr.NSCAMPON; updated = true; }
            if (this.NSCTD != huSbr.NSCTD) { this.NSCTD = huSbr.NSCTD; updated = true; }
            if (this.NSCLICKHOLD != huSbr.NSCLICKHOLD) { this.NSCLICKHOLD = huSbr.NSCLICKHOLD; updated = true; }
            if (this.NSQUEUE != huSbr.NSQUEUE) { this.NSQUEUE = huSbr.NSQUEUE; updated = true; }
            if (this.NSSANSWER != huSbr.NSSANSWER) { this.NSSANSWER = huSbr.NSSANSWER; updated = true; }
            if (this.NSICENCF != huSbr.NSICENCF) { this.NSICENCF = huSbr.NSICENCF; updated = true; }
            if (this.NSCFGO != huSbr.NSCFGO) { this.NSCFGO = huSbr.NSCFGO; updated = true; }
            if (this.NSCECT != huSbr.NSCECT) { this.NSCECT = huSbr.NSCECT; updated = true; }
            if (this.NSCTGO != huSbr.NSCTGO) { this.NSCTGO = huSbr.NSCTGO; updated = true; }
            if (this.NSCTIO != huSbr.NSCTIO) { this.NSCTIO = huSbr.NSCTIO; updated = true; }
            if (this.NSSETBUSY != huSbr.NSSETBUSY) { this.NSSETBUSY = huSbr.NSSETBUSY; updated = true; }
            if (this.NSOVERSTEP != huSbr.NSOVERSTEP) { this.NSOVERSTEP = huSbr.NSOVERSTEP; updated = true; }
            if (this.NSABSENT != huSbr.NSABSENT) { this.NSABSENT = huSbr.NSABSENT; updated = true; }
            if (this.NSMONITOR != huSbr.NSMONITOR) { this.NSMONITOR = huSbr.NSMONITOR; updated = true; }
            if (this.NSFMONITOR != huSbr.NSFMONITOR) { this.NSFMONITOR = huSbr.NSFMONITOR; updated = true; }
            if (this.NSDISCNT != huSbr.NSDISCNT) { this.NSDISCNT = huSbr.NSDISCNT; updated = true; }
            if (this.NSFDISCNT != huSbr.NSFDISCNT) { this.NSFDISCNT = huSbr.NSFDISCNT; updated = true; }
            if (this.NSINSERT != huSbr.NSINSERT) { this.NSINSERT = huSbr.NSINSERT; updated = true; }
            if (this.NSFINSERT != huSbr.NSFINSERT) { this.NSFINSERT = huSbr.NSFINSERT; updated = true; }
            if (this.NSASI != huSbr.NSASI) { this.NSASI = huSbr.NSASI; updated = true; }
            if (this.NSPWCB != huSbr.NSPWCB) { this.NSPWCB = huSbr.NSPWCB; updated = true; }
            if (this.NSRD != huSbr.NSRD) { this.NSRD = huSbr.NSRD; updated = true; }
            if (this.NSLCPS != huSbr.NSLCPS) { this.NSLCPS = huSbr.NSLCPS; updated = true; }
            if (this.NSNCPS != huSbr.NSNCPS) { this.NSNCPS = huSbr.NSNCPS; updated = true; }
            if (this.NSICPS != huSbr.NSICPS) { this.NSICPS = huSbr.NSICPS; updated = true; }
            if (this.NSCBCLOCK != huSbr.NSCBCLOCK) { this.NSCBCLOCK = huSbr.NSCBCLOCK; updated = true; }
            if (this.NSMINIBAR != huSbr.NSMINIBAR) { this.NSMINIBAR = huSbr.NSMINIBAR; updated = true; }
            if (this.NSMCN != huSbr.NSMCN) { this.NSMCN = huSbr.NSMCN; updated = true; }
            if (this.NSDSTR != huSbr.NSDSTR) { this.NSDSTR = huSbr.NSDSTR; updated = true; }
            if (this.NSOPRREG != huSbr.NSOPRREG) { this.NSOPRREG = huSbr.NSOPRREG; updated = true; }
            if (this.NSONEKEY != huSbr.NSONEKEY) { this.NSONEKEY = huSbr.NSONEKEY; updated = true; }
            if (this.NSINBOUND != huSbr.NSINBOUND) { this.NSINBOUND = huSbr.NSINBOUND; updated = true; }
            if (this.NSOUTBOUND != huSbr.NSOUTBOUND) { this.NSOUTBOUND = huSbr.NSOUTBOUND; updated = true; }
            if (this.NSCALLERID != huSbr.NSCALLERID) { this.NSCALLERID = huSbr.NSCALLERID; updated = true; }
            if (this.NSCUN != huSbr.NSCUN) { this.NSCUN = huSbr.NSCUN; updated = true; }
            if (this.NSIPTVVC != huSbr.NSIPTVVC) { this.NSIPTVVC = huSbr.NSIPTVVC; updated = true; }
            if (this.NSNP != huSbr.NSNP) { this.NSNP = huSbr.NSNP; updated = true; }
            if (this.NSSEC != huSbr.NSSEC) { this.NSSEC = huSbr.NSSEC; updated = true; }
            if (this.NSSECSTA != huSbr.NSSECSTA) { this.NSSECSTA = huSbr.NSSECSTA; updated = true; }
            if (this.NSHRCN != huSbr.NSHRCN) { this.NSHRCN = huSbr.NSHRCN; updated = true; }
            if (this.NSSB != huSbr.NSSB) { this.NSSB = huSbr.NSSB; updated = true; }
            if (this.NSOCCR != huSbr.NSOCCR) { this.NSOCCR = huSbr.NSOCCR; updated = true; }
            if (this.LCO != huSbr.LCO) { this.LCO = huSbr.LCO; updated = true; }
            if (this.LC != huSbr.LC) { this.LC = huSbr.LC; updated = true; }
            if (this.LCT != huSbr.LCT) { this.LCT = huSbr.LCT; updated = true; }
            if (this.NTT != huSbr.NTT) { this.NTT = huSbr.NTT; updated = true; }
            if (this.ITT != huSbr.ITT) { this.ITT = huSbr.ITT; updated = true; }
            if (this.ICTX != huSbr.ICTX) { this.ICTX = huSbr.ICTX; updated = true; }
            if (this.OCTX != huSbr.OCTX) { this.OCTX = huSbr.OCTX; updated = true; }
            if (this.INTT != huSbr.INTT) { this.INTT = huSbr.INTT; updated = true; }
            if (this.IITT != huSbr.IITT) { this.IITT = huSbr.IITT; updated = true; }
            if (this.ICLT != huSbr.ICLT) { this.ICLT = huSbr.ICLT; updated = true; }
            if (this.ICDDD != huSbr.ICDDD) { this.ICDDD = huSbr.ICDDD; updated = true; }
            if (this.ICIDD != huSbr.ICIDD) { this.ICIDD = huSbr.ICIDD; updated = true; }
            if (this.IOLT != huSbr.IOLT) { this.IOLT = huSbr.IOLT; updated = true; }
            if (this.CTLCO != huSbr.CTLCO) { this.CTLCO = huSbr.CTLCO; updated = true; }
            if (this.CTLCT != huSbr.CTLCT) { this.CTLCT = huSbr.CTLCT; updated = true; }
            if (this.CTLD != huSbr.CTLD) { this.CTLD = huSbr.CTLD; updated = true; }
            if (this.CTINTNANP != huSbr.CTINTNANP) { this.CTINTNANP = huSbr.CTINTNANP; updated = true; }
            if (this.CTINTWORLD != huSbr.CTINTWORLD) { this.CTINTWORLD = huSbr.CTINTWORLD; updated = true; }
            if (this.CTDA != huSbr.CTDA) { this.CTDA = huSbr.CTDA; updated = true; }
            if (this.CTOSM != huSbr.CTOSM) { this.CTOSM = huSbr.CTOSM; updated = true; }
            if (this.CTOSP != huSbr.CTOSP) { this.CTOSP = huSbr.CTOSP; updated = true; }
            if (this.CTOSP1 != huSbr.CTOSP1) { this.CTOSP1 = huSbr.CTOSP1; updated = true; }
            if (this.CCO1 != huSbr.CCO1) { this.CCO1 = huSbr.CCO1; updated = true; }
            if (this.CCO2 != huSbr.CCO2) { this.CCO2 = huSbr.CCO2; updated = true; }
            if (this.CCO3 != huSbr.CCO3) { this.CCO3 = huSbr.CCO3; updated = true; }
            if (this.CCO4 != huSbr.CCO4) { this.CCO4 = huSbr.CCO4; updated = true; }
            if (this.CCO5 != huSbr.CCO5) { this.CCO5 = huSbr.CCO5; updated = true; }
            if (this.CCO6 != huSbr.CCO6) { this.CCO6 = huSbr.CCO6; updated = true; }
            if (this.CCO7 != huSbr.CCO7) { this.CCO7 = huSbr.CCO7; updated = true; }
            if (this.CCO8 != huSbr.CCO8) { this.CCO8 = huSbr.CCO8; updated = true; }
            if (this.CCO9 != huSbr.CCO9) { this.CCO9 = huSbr.CCO9; updated = true; }
            if (this.CCO10 != huSbr.CCO10) { this.CCO10 = huSbr.CCO10; updated = true; }
            if (this.CCO11 != huSbr.CCO11) { this.CCO11 = huSbr.CCO11; updated = true; }
            if (this.CCO12 != huSbr.CCO12) { this.CCO12 = huSbr.CCO12; updated = true; }
            if (this.CCO13 != huSbr.CCO13) { this.CCO13 = huSbr.CCO13; updated = true; }
            if (this.CCO14 != huSbr.CCO14) { this.CCO14 = huSbr.CCO14; updated = true; }
            if (this.CCO15 != huSbr.CCO15) { this.CCO15 = huSbr.CCO15; updated = true; }
            if (this.CCO16 != huSbr.CCO16) { this.CCO16 = huSbr.CCO16; updated = true; }
            if (this.HIGHENTCO != huSbr.HIGHENTCO) { this.HIGHENTCO = huSbr.HIGHENTCO; updated = true; }
            if (this.OPERATOR != huSbr.OPERATOR) { this.OPERATOR = huSbr.OPERATOR; updated = true; }
            if (this.SUPYSRV != huSbr.SUPYSRV) { this.SUPYSRV = huSbr.SUPYSRV; updated = true; }
            if (this.IDDCI != huSbr.IDDCI) { this.IDDCI = huSbr.IDDCI; updated = true; }
            if (this.NTCI != huSbr.NTCI) { this.NTCI = huSbr.NTCI; updated = true; }
            if (this.LTCI != huSbr.LTCI) { this.LTCI = huSbr.LTCI; updated = true; }
            if (this.RSC != huSbr.RSC) { this.RSC = huSbr.RSC; updated = true; }
            if (this.CIG != huSbr.CIG) { this.CIG = huSbr.CIG; updated = true; }
            if (this.OUTRST != huSbr.OUTRST) { this.OUTRST = huSbr.OUTRST; updated = true; }
            if (this.INRST != huSbr.INRST) { this.INRST = huSbr.INRST; updated = true; }
            if (this.NOAT != huSbr.NOAT) { this.NOAT = huSbr.NOAT; updated = true; }
            if (this.RINGCOUNT != huSbr.RINGCOUNT) { this.RINGCOUNT = huSbr.RINGCOUNT; updated = true; }
            if (this.VMAIND != huSbr.VMAIND) { this.VMAIND = huSbr.VMAIND; updated = true; }
            if (this.VDMAIND != huSbr.VDMAIND) { this.VDMAIND = huSbr.VDMAIND; updated = true; }
            if (this.TGRP != huSbr.TGRP) { this.TGRP = huSbr.TGRP; updated = true; }
            if (this.TIDHLD != huSbr.TIDHLD) { this.TIDHLD = huSbr.TIDHLD; updated = true; }
            if (this.TIDCW != huSbr.TIDCW) { this.TIDCW = huSbr.TIDCW; updated = true; }
            if (this.SCF != huSbr.SCF) { this.SCF = huSbr.SCF; updated = true; }
            if (this.LMTGRP != huSbr.LMTGRP) { this.LMTGRP = huSbr.LMTGRP; updated = true; }
            if (this.FLBGRP != huSbr.FLBGRP) { this.FLBGRP = huSbr.FLBGRP; updated = true; }
            if (this.SLBGRP != huSbr.SLBGRP) { this.SLBGRP = huSbr.SLBGRP; updated = true; }
            if (this.COP != huSbr.COP) { this.COP = huSbr.COP; updated = true; }
            if (this.G711_64K_A_LAW != huSbr.G711_64K_A_LAW) { this.G711_64K_A_LAW = huSbr.G711_64K_A_LAW; updated = true; }
            if (this.G711_64K_U_LAW != huSbr.G711_64K_U_LAW) { this.G711_64K_U_LAW = huSbr.G711_64K_U_LAW; updated = true; }
            if (this.G722 != huSbr.G722) { this.G722 = huSbr.G722; updated = true; }
            if (this.G723 != huSbr.G723) { this.G723 = huSbr.G723; updated = true; }
            if (this.G726 != huSbr.G726) { this.G726 = huSbr.G726; updated = true; }
            if (this.G728 != huSbr.G728) { this.G728 = huSbr.G728; updated = true; }
            if (this.G729 != huSbr.G729) { this.G729 = huSbr.G729; updated = true; }
            if (this.CODEC_MP4A != huSbr.CODEC_MP4A) { this.CODEC_MP4A = huSbr.CODEC_MP4A; updated = true; }
            if (this.CODEC2833 != huSbr.CODEC2833) { this.CODEC2833 = huSbr.CODEC2833; updated = true; }
            if (this.CODEC2198 != huSbr.CODEC2198) { this.CODEC2198 = huSbr.CODEC2198; updated = true; }
            if (this.G726_40 != huSbr.G726_40) { this.G726_40 = huSbr.G726_40; updated = true; }
            if (this.G726_32 != huSbr.G726_32) { this.G726_32 = huSbr.G726_32; updated = true; }
            if (this.G726_24 != huSbr.G726_24) { this.G726_24 = huSbr.G726_24; updated = true; }
            if (this.G726_16 != huSbr.G726_16) { this.G726_16 = huSbr.G726_16; updated = true; }
            if (this.AMR != huSbr.AMR) { this.AMR = huSbr.AMR; updated = true; }
            if (this.CLEARMODE != huSbr.CLEARMODE) { this.CLEARMODE = huSbr.CLEARMODE; updated = true; }
            if (this.ILBC != huSbr.ILBC) { this.ILBC = huSbr.ILBC; updated = true; }
            if (this.SPEEX != huSbr.SPEEX) { this.SPEEX = huSbr.SPEEX; updated = true; }
            if (this.G729EV != huSbr.G729EV) { this.G729EV = huSbr.G729EV; updated = true; }
            if (this.EVRC != huSbr.EVRC) { this.EVRC = huSbr.EVRC; updated = true; }
            if (this.EVRCB != huSbr.EVRCB) { this.EVRCB = huSbr.EVRCB; updated = true; }
            if (this.H261 != huSbr.H261) { this.H261 = huSbr.H261; updated = true; }
            if (this.H263 != huSbr.H263) { this.H263 = huSbr.H263; updated = true; }
            if (this.CODEC_MP4V != huSbr.CODEC_MP4V) { this.CODEC_MP4V = huSbr.CODEC_MP4V; updated = true; }
            if (this.H264 != huSbr.H264) { this.H264 = huSbr.H264; updated = true; }
            if (this.T38 != huSbr.T38) { this.T38 = huSbr.T38; updated = true; }
            if (this.T120 != huSbr.T120) { this.T120 = huSbr.T120; updated = true; }
            if (this.G711A_VBD != huSbr.G711A_VBD) { this.G711A_VBD = huSbr.G711A_VBD; updated = true; }
            if (this.G711U_VBD != huSbr.G711U_VBD) { this.G711U_VBD = huSbr.G711U_VBD; updated = true; }
            if (this.G726_VBD != huSbr.G726_VBD) { this.G726_VBD = huSbr.G726_VBD; updated = true; }
            if (this.G726_40_VBD != huSbr.G726_40_VBD) { this.G726_40_VBD = huSbr.G726_40_VBD; updated = true; }
            if (this.G726_32_VBD != huSbr.G726_32_VBD) { this.G726_32_VBD = huSbr.G726_32_VBD; updated = true; }
            if (this.G726_24_VBD != huSbr.G726_24_VBD) { this.G726_24_VBD = huSbr.G726_24_VBD; updated = true; }
            if (this.G726_16_VBD != huSbr.G726_16_VBD) { this.G726_16_VBD = huSbr.G726_16_VBD; updated = true; }
            if (this.WIND_BAND_AMR != huSbr.WIND_BAND_AMR) { this.WIND_BAND_AMR = huSbr.WIND_BAND_AMR; updated = true; }
            if (this.GSM610 != huSbr.GSM610) { this.GSM610 = huSbr.GSM610; updated = true; }
            if (this.H263_2000 != huSbr.H263_2000) { this.H263_2000 = huSbr.H263_2000; updated = true; }
            if (this.BROADVOICE_32 != huSbr.BROADVOICE_32) { this.BROADVOICE_32 = huSbr.BROADVOICE_32; updated = true; }
            if (this.UNKNOWN_CODEC != huSbr.UNKNOWN_CODEC) { this.UNKNOWN_CODEC = huSbr.UNKNOWN_CODEC; updated = true; }
            if (this.ACODEC != huSbr.ACODEC) { this.ACODEC = huSbr.ACODEC; updated = true; }
            if (this.VCODEC != huSbr.VCODEC) { this.VCODEC = huSbr.VCODEC; updated = true; }
            if (this.POLIDX != huSbr.POLIDX) { this.POLIDX = huSbr.POLIDX; updated = true; }
            if (this.NCPI != huSbr.NCPI) { this.NCPI = huSbr.NCPI; updated = true; }
            if (this.ICPI != huSbr.ICPI) { this.ICPI = huSbr.ICPI; updated = true; }
            if (this.EBOCL != huSbr.EBOCL) { this.EBOCL = huSbr.EBOCL; updated = true; }
            if (this.EBOPL != huSbr.EBOPL) { this.EBOPL = huSbr.EBOPL; updated = true; }
            if (this.EBOIT != huSbr.EBOIT) { this.EBOIT = huSbr.EBOIT; updated = true; }
            if (this.RM != huSbr.RM) { this.RM = huSbr.RM; updated = true; }
            if (this.CPC != huSbr.CPC) { this.CPC = huSbr.CPC; updated = true; }
            if (this.PCHG != huSbr.PCHG) { this.PCHG = huSbr.PCHG; updated = true; }
            if (this.TFPT != huSbr.TFPT) { this.TFPT = huSbr.TFPT; updated = true; }
            if (this.CHT != huSbr.CHT) { this.CHT = huSbr.CHT; updated = true; }
            if (this.MCIDMODE != huSbr.MCIDMODE) { this.MCIDMODE = huSbr.MCIDMODE; updated = true; }
            if (this.MCIDCMODE != huSbr.MCIDCMODE) { this.MCIDCMODE = huSbr.MCIDCMODE; updated = true; }
            if (this.MCIDAMODE != huSbr.MCIDAMODE) { this.MCIDAMODE = huSbr.MCIDAMODE; updated = true; }
            if (this.PREPAIDIDX != huSbr.PREPAIDIDX) { this.PREPAIDIDX = huSbr.PREPAIDIDX; updated = true; }
            if (this.CRBTID != huSbr.CRBTID) { this.CRBTID = huSbr.CRBTID; updated = true; }
            if (this.ODBBICTYPE != huSbr.ODBBICTYPE) { this.ODBBICTYPE = huSbr.ODBBICTYPE; updated = true; }
            if (this.ODBBOCTYPE != huSbr.ODBBOCTYPE) { this.ODBBOCTYPE = huSbr.ODBBOCTYPE; updated = true; }
            if (this.ODBBARTYPE != huSbr.ODBBARTYPE) { this.ODBBARTYPE = huSbr.ODBBARTYPE; updated = true; }
            if (this.ODBSS != huSbr.ODBSS) { this.ODBSS = huSbr.ODBSS; updated = true; }
            if (this.ODBBRCFTYPE != huSbr.ODBBRCFTYPE) { this.ODBBRCFTYPE = huSbr.ODBBRCFTYPE; updated = true; }
            if (this.PNOTI != huSbr.PNOTI) { this.PNOTI = huSbr.PNOTI; updated = true; }
            if (this.MAXPARACALL != huSbr.MAXPARACALL) { this.MAXPARACALL = huSbr.MAXPARACALL; updated = true; }
            if (this.ATSDTMBUSY != huSbr.ATSDTMBUSY) { this.ATSDTMBUSY = huSbr.ATSDTMBUSY; updated = true; }
            if (this.CALLCOUNT != huSbr.CALLCOUNT) { this.CALLCOUNT = huSbr.CALLCOUNT; updated = true; }
            if (this.CDNOTICALLER != huSbr.CDNOTICALLER) { this.CDNOTICALLER = huSbr.CDNOTICALLER; updated = true; }
            if (this.ISCHGFLAG != huSbr.ISCHGFLAG) { this.ISCHGFLAG = huSbr.ISCHGFLAG; updated = true; }
            if (this.CHC != huSbr.CHC) { this.CHC = huSbr.CHC; updated = true; }
            if (this.CUSER != huSbr.CUSER) { this.CUSER = huSbr.CUSER; updated = true; }
            if (this.CGRP != huSbr.CGRP) { this.CGRP = huSbr.CGRP; updated = true; }
            if (this.CUSERGRP != huSbr.CUSERGRP) { this.CUSERGRP = huSbr.CUSERGRP; updated = true; }
            if (this.STCF != huSbr.STCF) { this.STCF = huSbr.STCF; updated = true; }
            if (this.CHARSC != huSbr.CHARSC) { this.CHARSC = huSbr.CHARSC; updated = true; }
            if (this.REGUIDX != huSbr.REGUIDX) { this.REGUIDX = huSbr.REGUIDX; updated = true; }
            if (this.SOCBFUNC != huSbr.SOCBFUNC) { this.SOCBFUNC = huSbr.SOCBFUNC; updated = true; }
            if (this.SOCBPTONEIDX != huSbr.SOCBPTONEIDX) { this.SOCBPTONEIDX = huSbr.SOCBPTONEIDX; updated = true; }
            if (this.ADMINCBA != huSbr.ADMINCBA) { this.ADMINCBA = huSbr.ADMINCBA; updated = true; }
            if (this.ADCONTROL_DIVERSION != huSbr.ADCONTROL_DIVERSION) { this.ADCONTROL_DIVERSION = huSbr.ADCONTROL_DIVERSION; updated = true; }
            if (this.DPR != huSbr.DPR) { this.DPR = huSbr.DPR; updated = true; }
            if (this.PRON != huSbr.PRON) { this.PRON = huSbr.PRON; updated = true; }
            if (this.CPCRUS != huSbr.CPCRUS) { this.CPCRUS = huSbr.CPCRUS; updated = true; }
            if (this.CUSCAT != huSbr.CUSCAT) { this.CUSCAT = huSbr.CUSCAT; updated = true; }
            if (this.SPT100REL != huSbr.SPT100REL) { this.SPT100REL = huSbr.SPT100REL; updated = true; }

            if (updated) this.Updated = this.Inspected = DateTime.UtcNow.AddHours(3);
            //else this.Inspected = DateTime.UtcNow.AddHours(3);

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}