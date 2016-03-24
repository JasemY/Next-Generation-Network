using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ia.Ngn.Cl.Model.BusinessProcessFramwork
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Business Process Framwork support class for Next Generation Network (NGN) data model.
    /// </summary>
    /// 
    /// <remarks> 
    /// Copyright © 2012-2015 Jasem Y. Al-Shamlan (info@ia.com.kw), Internet Applications - Kuwait. All Rights Reserved.
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
    public class CrmSupportAndReadiness : IOperationsSupportAndReadiness, ICustomerRelationshipManagement
    {
        /*
        public SupportCustomerInterfaceManagement supportCustomerInterfaceManagement { get; set; }
        public void SupportOrderHandling supportOrderHandling { get; set; }
        public void SupportProblemHandling supportProblemHandling { get; set; }
        public void SupportBillInvoiceManagement supportBillInvoiceManagement { get; set; }
        public void SupportBillPaymentsAndReceivablesManagement supportBillPaymentsAndReceivablesManagement { get; set; }
        public void SupportRetentionAndLoyalty supportRetentionAndLoyalty { get; set; }
        public void SupportMarketingFulfillment supportMarketingFulfillment { get; set; }
        public void SupportSelling supportSelling { get; set; }
        public void SupportBillInquiryHandling supportBillInquiryHandling { get; set; }
        public void ManageCampaign manageCampaign { get; set; }
        public void ManageCustomerInventory manageCustomerInventory { get; set; }
        public void ManageProductOfferingInventory manageProductOfferingInventory { get; set; }
        public void ManageSalesInventory manageSalesInventory { get; set; }
        public void SupportCustomerQoSPerSLA supportCustomerQoSPerSLA { get; set; }
         */
        //public OperationsSupportAndReadiness OperationsSupportAndReadiness { get; set; }

        //public CustomerRelationshipManagement CustomerRelationshipManagement { get; set; }

        public CrmSupportAndReadiness()
        {
            //OperationsSupportAndReadiness = new OperationsSupportAndReadiness();
            //CustomerRelationshipManagement = new CustomerRelationshipManagement();

        }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public class SupportCustomerInterfaceManagement
    {
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public class Main
    {
        /*
        CrmSupportAndReadiness ngn = new CrmSupportAndReadiness();

        public void main()
        {
            ngn.Name();
            ngn.Stop();

        }
         */ 
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public class OperationsSupportAndReadiness : Operations
    {
        string Name { get { return "Name"; } }
        string ArabicName { get { return "ArabicName"; } }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public interface IOperationsSupportAndReadiness
    {
        OperationsSupportAndReadiness OperationsSupportAndReadiness { get; set; }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public static class OperationsSupportAndReadinessExtensions
    {
        public static string Name(this IOperationsSupportAndReadiness i)
        {
            return "Operations";
        }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public class Fulfillment : Operations
    {
        string Name { get { return "Name"; } }
        string ArabicName { get { return "ArabicName"; } }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public class Assurance : Operations
    {
        string Name { get { return "Name"; } }
        string ArabicName { get { return "ArabicName"; } }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public class BillingAndRevenueManagement : Operations
    {
        string Name { get { return "Name"; } }
        string ArabicName { get { return "ArabicName"; } }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public class CustomerRelationshipManagement : Operations
    {
        string Name { get { return "Name"; } }
        string ArabicName { get { return "ArabicName"; } }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public interface ICustomerRelationshipManagement
    {
        CustomerRelationshipManagement CustomerRelationshipManagement { get; set; }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public static class CustomerRelationshipManagementExtensions
    {
        public static string Name(this IOperationsSupportAndReadiness i)
        {
            return "Customer Relationship";
        }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public class ServiceManagementAndOperations : Operations
    {
        string Name { get { return "Name"; } }
        string ArabicName { get { return "ArabicName"; } }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public class ResourceManagementAndOperations : Operations
    {
        string Name { get { return "Name"; } }
        string ArabicName { get { return "ArabicName"; } }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public class SupplierAndPartnerRelationshipManagement : Operations
    {
        string Name { get { return "Name"; } }
        string ArabicName { get { return "ArabicName"; } }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public class Operations
    {
        string Name { get { return "Name"; } }
        string ArabicName { get { return "ArabicName"; } }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public class StrategyInfrastructureAndProduct
    {
        public string Name { get; set; }
        public string ArabicName { get; set; }
    }

    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public class EnterpriseManagement
    {
        public string Name { get; set; }
        public string ArabicName { get; set; }
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}