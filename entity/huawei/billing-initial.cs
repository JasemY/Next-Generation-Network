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
    /// Initial Billing Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class InitialBilling
    {
        /// <summary/>
        public InitialBilling() { }

        // CENTRAL_ID,CENTRAL,PHONE_NO,MDF,VERTICAL,MDF_PAIR,FEEDER_BOX,FEEDER_PAIR,CABINET_NAME,DIST_BOX,DIST_PAIR,DP,TAC,DP_LINE,ACCOUNT_NO,CUST_NAME,CUST_CAT_NAME,STATUS,مفتاح بدالة,فرع بدالة,النداء الآلي,مجموعة خدمات,خدمة DSL,كاشف رقم,التحكم بالصفر الدولي,تحويل مكالمات,خدمة الإنتظار,خدمة المحاسبة,استشارة,ايقاف استقبال,منع الإتصال,خدمة الإيقاظ,نداء عاجل,إختصار الرقم,GOV,DISTRICT,BLOCK,SUB_BLOCK,STREET,SUB_STREET,BULDING_NO,BULDING_NAME,FLOOR,APARTMENT
        // 156,الصليبية,24670004,1,22,457,6,57,SL 04,I,143,SL 04 165,1,1,905144,فليح على أدريس الشمرى,أفراد,ON,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,الجهراء,الصليبيه,2,,13,,397,,,

        /// <summary/>
        public long Id { get; set; }

        /// <summary/>
        [Key]
        public long PHONE_NO { get; set; }
        public string CENTRAL_ID { get; set; }
        public string CENTRAL { get; set; }
        public string MDF { get; set; }
        public string VERTICAL { get; set; }
        public string MDF_PAIR { get; set; }
        public string FEEDER_BOX { get; set; }
        public string FEEDER_PAIR { get; set; }
        public string CABINET_NAME { get; set; }
        public string DIST_BOX { get; set; }
        public string DIST_PAIR { get; set; }
        public string DP { get; set; }
        public string TAC { get; set; }
        public string DP_LINE { get; set; }
        public string ACCOUNT_NO { get; set; }
        public string CUST_NAME { get; set; }
        public string CUST_CAT_NAME { get; set; }
        public string STATUS { get; set; }
        public string PbxMain { get; set; }
        public string PbxBranch { get; set; }
        public string InternationalCalling { get; set; }
        public string ServiceGroup { get; set; }
        public string Dsl { get; set; }
        public string CallerId { get; set; }
        public string InternationalCallControl { get; set; }
        public string CallForwarding { get; set; }
        public string CallWaiting { get; set; }
        public string Accounting { get; set; }
        public string ThreeWayCalling { get; set; }
        public string ReceptionHalt { get; set; }
        public string CallBarring { get; set; }
        public string WakeupCall { get; set; }
        public string UrgentCall { get; set; }
        public string CallAbbriviation { get; set; }
        public string GOV { get; set; }
        public string DISTRICT { get; set; }
        public string BLOCK { get; set; }
        public string SUB_BLOCK { get; set; }
        public string STREET { get; set; }
        public string SUB_STREET { get; set; }
        public string BULDING_NO { get; set; }
        public string BULDING_NAME { get; set; }
        public string FLOOR { get; set; }
        public string APARTMENT { get; set; }

        /// <summary/>
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Viewed { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Equal(InitialBilling b)
        {
            // below: this will not check the Id, Created, Updated, or Viewed fields
            bool areEqual;

            areEqual = false;

            /*
            if (this.IMPU != b.IMPU) areEqual = false;
            else if (this.UTYPE != b.UTYPE) areEqual = false;
            else areEqual = true;
             */ 

            return areEqual;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public void Update(InitialBilling b)
        {
            // below: this will not update Id or Created

            /*
            if (this.IMPU != b.IMPU) this.IMPU = b.IMPU;
            if (this.NSOUTG != b.NSOUTG) this.NSOUTG = b.NSOUTG;

            if (this.Updated != b.Updated) this.Updated = b.Updated;
            if (this.Viewed != b.Viewed) this.Viewed = b.Viewed;
             */ 
        }

        /*
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static long ServiceId(string lceid, int lan)
        {
            // below: logic below is based on LCEID data in service.xml
            long id;

            id = Ia.Cl.Model.Default.HexToDec(lceid);
            id = (id - 48000) / 16 * 100000;
            id += lan;

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read all services for a certain LCEID
        /// </summary>
        public static List<Service> ReadList(string lceidName)
        {
            List<Service> serviceList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceList = (from q in db.Services where q.LCEIDName == lceidName select q).ToList();
            }

            return serviceList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read service of a DN
        /// </summary>
        public static Service Read(long dn)
        {
            Service service;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                service = (from q in db.Services where q.DN == dn select q).SingleOrDefault();
            }

            return service;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read all services for a DN list
        /// </summary>
        public static List<Service> ReadList(ArrayList dnList)
        {
            long i;
            long[] sp;
            List<Service> serviceList;

            i = 0;
            sp = new long[dnList.Count];

            foreach (long l in dnList) sp[i++] = l;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                //serviceList = (from q in db.Services where dnList.Contains(q.DN) select q).ToList();

                // var pages = context.Pages.Where(x => keys.Any(key => x.Title.Contains(key)));
                serviceList = db.Services.Where(q => sp.Any(v => q.DN == v)).ToList();
            }

            return serviceList;
        }
         */ 

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}