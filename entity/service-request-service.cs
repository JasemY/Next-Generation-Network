using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;

namespace Ia.Ngn.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Service Request Service Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class ServiceRequestService
    {
        /// <summary/>
        public ServiceRequestService()
        {
            //ServiceRequests = new List< Ia.Ngn.Cl.Model.ServiceRequest>();
        }

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        /// <summary/>
        public string Service { get; set; }

        /// <summary/>
        public int ServiceType { get; set; }

        /// <summary/>
        public int AreaCode { get; set; }

        /// <summary/>
        public bool CallWaiting { get; set; }

        /// <summary/>
        public bool CallForwarding { get; set; }

        /// <summary/>
        public bool AlarmCall { get; set; }

        /// <summary/>
        public bool CallBarring { get; set; }

        /// <summary/>
        public bool InternationalCallingUserControlled { get; set; }

        /// <summary/>
        public bool InternationalCalling { get; set; }

        /// <summary/>
        public bool CallerId { get; set; }

        /// <summary/>
        public bool WakeupCall { get; set; }

        /// <summary/>
        public bool ConferenceCall { get; set; }
        /// <summary/>
        public bool ServiceSuspension { get; set; }

        /// <summary/>
        public int ServiceSuspensionTypeId { get; set; }

        /// <summary/>
        public bool AbbriviatedCalling { get; set; }

        /// <summary/>
        public bool CallForwardingUnconditional { get; set; }

        /// <summary/>
        public bool CallingLineIdentificationRestriction { get; set; }

        /// <summary/>
        public bool ConnectedLineIdentificationRestriction { get; set; }

        /// <summary/>
        public bool WakeUp { get; set; }

        /// <summary/>
        public bool CallForwardingByTime { get; set; }

        /// <summary/>
        public bool MutlimediaInformationPresentation { get; set; }

        /// <summary/>
        public bool SelectiveOutgoingCallBarring { get; set; }

        /// <summary/>
        public bool DialNumberCallOutAllow { get; set; }

        /// <summary/>
        public bool DoNotDisturb { get; set; }

        /// <summary/>
        public bool OutgoingCallBarring { get; set; }

        /// <summary/>
        public bool TemporaryLine { get; set; }

        /// <summary/>
        public bool CodecControl { get; set; }

        /// <summary/>
        public bool SelectiveIncomingCallBarring { get; set; }

        /// <summary/>
        public bool SelectiveCallForwarding { get; set; }

        /// <summary/>
        public bool DialNumberCallOutBarring { get; set; }

        /// <summary/>
        public bool CallForwardingBasedonBlackList { get; set; }

        /// <summary/>
        public bool CallForwardingBusy { get; set; }

        /// <summary/>
        public bool CallForwardingNoReply { get; set; }

        /// <summary/>
        public bool CallForwardingOffline { get; set; }

        /// <summary/>
        public bool CallForwardingOnUserNotReachable { get; set; }

        /// <summary/>
        public bool CallForwardingNoReplyinCallWaiting { get; set; }

        /// <summary/>
        public bool MultiRinging { get; set; }

        /// <summary/>
        public bool ConvergentInterPersonalService { get; set; }

        /// <summary/>
        public bool CallForwardingBySequence { get; set; }

        /// <summary/>
        public bool UserNumberChange { get; set; }

        /// <summary/>
        public bool BlackNumberData { get; set; }

        /// <summary/>
        public bool WhiteNumberData { get; set; }

        /// <summary/>
        public bool OwedRestriction { get; set; }

        /// <summary/>
        public bool Strategy { get; set; }

        /// <summary/>
        public bool GreenNumberData { get; set; }

        /// <summary/>
        public bool RedNumberData { get; set; }

        /// <summary/>
        public bool BarringOfAllOutgoingCalls { get; set; }

        /// <summary/>
        public bool BarringOfAllOutgoingInternationalCalls { get; set; }

        /// <summary/>
        public bool BarringOfOutgoingInternationalCallsExceptThoseDirectedToTheHomePlmnCountry { get; set; }

        /// <summary/>
        public bool BarringOfAllIncomingCalls { get; set; }

        /// <summary/>
        public bool BarringOfIncomingCallsWhenRoamingOutsideTheHomePlmnCountry { get; set; }

        /// <summary/>
        public bool SpeedDial { get; set; }

        /// <summary/>
        public bool GreenCall { get; set; }

        /// <summary/>
        public bool SetDataUpgrade { get; set; }

        /// <summary/>
        public bool AutoConsole { get; set; }

        /// <summary/>
        public bool NightService { get; set; }

        /// <summary/>
        public bool BackupNumber { get; set; }

        /// <summary/>
        public bool Absence { get; set; }

        /// <summary/>
        public bool AuthorizedCodeForStdIdd { get; set; }

        /// <summary/>
        public bool Hotline { get; set; }

        /// <summary/>
        public bool CetMaliciousCommunicationIdentification { get; set; }

        /// <summary/>
        public bool MissCallNotify { get; set; }

        /// <summary/>
        public bool SubscriptionStatus { get; set; }

        /// <summary/>
        public bool UsbDongleOneKeyService { get; set; }

        /// <summary/>
        public bool IRoamingInboundSingleImsiMultiMsisdn { get; set; }

        /// <summary/>
        public bool IRoamingOutboundSingleImsiMultiMsisdn { get; set; }

        /// <summary/>
        public bool NpaSplit { get; set; }

        /// <summary/>
        public bool AllCallForwardingGroup { get; set; }

        /// <summary/>
        public bool ConditionalCallForwardingGroup { get; set; }

        /// <summary/>
        public bool GeneralOrigIdentificationRestriction { get; set; }

        /// <summary/>
        public bool MultimediaOfOrigIdRestriction { get; set; }

        /// <summary/>
        public bool TerminatingIdentityRestriction { get; set; }

        /// <summary/>
        public bool DistinctiveRing { get; set; }

        /// <summary/>
        public bool VisitedNetworkImpu { get; set; }

        /// <summary/>
        public bool SeasonalSuspend { get; set; }

        /// <summary/>
        public bool NumberInvalidation { get; set; }

        /// <summary/>
        public bool IroamingServiceProvision { get; set; }

        /// <summary/>
        public bool IptvCallerId { get; set; }

        /// <summary/>
        public bool IptvVideoCall { get; set; }

        /// <summary/>
        public bool NumberPortability { get; set; }

        /// <summary/>
        public bool SecretaryService { get; set; }

        /// <summary/>
        public bool SalesBlock { get; set; }

        /// <summary/>
        public bool FilterCriteria { get; set; }

        /// <summary/>
        public bool CallerInformation { get; set; }

        /// <summary/>
        public bool PresenceStatus { get; set; }

        /// <summary/>
        public bool OneNumberService { get; set; }

        /// <summary/>
        public bool AssociationList { get; set; }

        /// <summary/>
        public int Pin { get; set; }

        /// <summary/>
        public virtual Access Access { get; set; }

        /// <summary/>
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }

        /// <summary/>
        public DateTime Created { get; set; }

        /// <summary/>
        public DateTime Updated { get; set; }

        /// <summary/>
        public DateTime Inspected { get; set; }

        /// <summary/>
        public System.Guid UserId { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string ServiceRequestServiceId(string service, int serviceType)
        {
            // below:
            string id;

            id = Ia.Ngn.Cl.Model.Service2.ServiceId(service, serviceType);

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read service using id
        /// </summary>
        public static ServiceRequestService Read(string id)
        {
            ServiceRequestService serviceRequestService;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestService = (from q in db.ServiceRequestServices where q.Id == id select q).SingleOrDefault();
            }

            return serviceRequestService;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read service using id
        /// </summary>
        public static ServiceRequestService ReadIncludeAccess(string id)
        {
            ServiceRequestService serviceRequestService;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestService = (from q in db.ServiceRequestServices.Include(a => a.Access) where q.Id == id select q).SingleOrDefault();
            }

            return serviceRequestService;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read service of a number
        /// </summary>
        public static ServiceRequestService Read(long number)
        {
            ServiceRequestService serviceRequestService;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestService = (from q in db.ServiceRequestServices where q.Service == number.ToString() select q).SingleOrDefault();
            }

            return serviceRequestService;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read all services for a number list
        /// </summary>
        public static List<ServiceRequestService> ReadList(ArrayList numberList)
        {
            long i;
            long[] sp;
            List<ServiceRequestService> serviceRequestServiceList;

            i = 0;
            sp = new long[numberList.Count];

            foreach (long l in numberList) sp[i++] = l;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                //serviceList = (from q in db.Services where dnList.Contains(q.DN) select q).ToList();

                // var pages = context.Pages.Where(x => keys.Any(key => x.Title.Contains(key)));
                serviceRequestServiceList = db.ServiceRequestServices.Where(q => sp.Any(v => q.Service == v.ToString())).ToList();
            }

            return serviceRequestServiceList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Update(ServiceRequestService updatedServiceRequestService)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.Service != updatedServiceRequestService.Service) { this.Service = updatedServiceRequestService.Service; updated = true; }
            if (this.ServiceType != updatedServiceRequestService.ServiceType) { this.ServiceType = updatedServiceRequestService.ServiceType; updated = true; }
            if (this.AreaCode != updatedServiceRequestService.AreaCode) { this.AreaCode = updatedServiceRequestService.AreaCode; updated = true; }
            if (this.CallWaiting != updatedServiceRequestService.CallWaiting) { this.CallWaiting = updatedServiceRequestService.CallWaiting; updated = true; }
            if (this.CallForwarding != updatedServiceRequestService.CallForwarding) { this.CallForwarding = updatedServiceRequestService.CallForwarding; updated = true; }
            if (this.AlarmCall != updatedServiceRequestService.AlarmCall) { this.AlarmCall = updatedServiceRequestService.AlarmCall; updated = true; }
            if (this.CallBarring != updatedServiceRequestService.CallBarring) { this.CallBarring = updatedServiceRequestService.CallBarring; updated = true; }
            if (this.InternationalCallingUserControlled != updatedServiceRequestService.InternationalCallingUserControlled) { this.InternationalCallingUserControlled = updatedServiceRequestService.InternationalCallingUserControlled; updated = true; }
            if (this.InternationalCalling != updatedServiceRequestService.InternationalCalling) { this.InternationalCalling = updatedServiceRequestService.InternationalCalling; updated = true; }
            if (this.CallerId != updatedServiceRequestService.CallerId) { this.CallerId = updatedServiceRequestService.CallerId; updated = true; }
            if (this.WakeupCall != updatedServiceRequestService.WakeupCall) { this.WakeupCall = updatedServiceRequestService.WakeupCall; updated = true; }
            if (this.ConferenceCall != updatedServiceRequestService.ConferenceCall) { this.ConferenceCall = updatedServiceRequestService.ConferenceCall; updated = true; }
            if (this.ServiceSuspension != updatedServiceRequestService.ServiceSuspension) { this.ServiceSuspension = updatedServiceRequestService.ServiceSuspension; updated = true; }
            if (this.ServiceSuspensionTypeId != updatedServiceRequestService.ServiceSuspensionTypeId) { this.ServiceSuspensionTypeId = updatedServiceRequestService.ServiceSuspensionTypeId; updated = true; }
            if (this.AbbriviatedCalling != updatedServiceRequestService.AbbriviatedCalling) { this.AbbriviatedCalling = updatedServiceRequestService.AbbriviatedCalling; updated = true; }
            if (this.CallForwardingUnconditional != updatedServiceRequestService.CallForwardingUnconditional) { this.CallForwardingUnconditional = updatedServiceRequestService.CallForwardingUnconditional; updated = true; }
            if (this.CallingLineIdentificationRestriction != updatedServiceRequestService.CallingLineIdentificationRestriction) { this.CallingLineIdentificationRestriction = updatedServiceRequestService.CallingLineIdentificationRestriction; updated = true; }
            if (this.ConnectedLineIdentificationRestriction != updatedServiceRequestService.ConnectedLineIdentificationRestriction) { this.ConnectedLineIdentificationRestriction = updatedServiceRequestService.ConnectedLineIdentificationRestriction; updated = true; }
            if (this.WakeUp != updatedServiceRequestService.WakeUp) { this.WakeUp = updatedServiceRequestService.WakeUp; updated = true; }
            if (this.CallForwardingByTime != updatedServiceRequestService.CallForwardingByTime) { this.CallForwardingByTime = updatedServiceRequestService.CallForwardingByTime; updated = true; }
            if (this.MutlimediaInformationPresentation != updatedServiceRequestService.MutlimediaInformationPresentation) { this.MutlimediaInformationPresentation = updatedServiceRequestService.MutlimediaInformationPresentation; updated = true; }
            if (this.SelectiveOutgoingCallBarring != updatedServiceRequestService.SelectiveOutgoingCallBarring) { this.SelectiveOutgoingCallBarring = updatedServiceRequestService.SelectiveOutgoingCallBarring; updated = true; }
            if (this.DialNumberCallOutAllow != updatedServiceRequestService.DialNumberCallOutAllow) { this.DialNumberCallOutAllow = updatedServiceRequestService.DialNumberCallOutAllow; updated = true; }
            if (this.DoNotDisturb != updatedServiceRequestService.DoNotDisturb) { this.DoNotDisturb = updatedServiceRequestService.DoNotDisturb; updated = true; }
            if (this.OutgoingCallBarring != updatedServiceRequestService.OutgoingCallBarring) { this.OutgoingCallBarring = updatedServiceRequestService.OutgoingCallBarring; updated = true; }
            if (this.TemporaryLine != updatedServiceRequestService.TemporaryLine) { this.TemporaryLine = updatedServiceRequestService.TemporaryLine; updated = true; }
            if (this.CodecControl != updatedServiceRequestService.CodecControl) { this.CodecControl = updatedServiceRequestService.CodecControl; updated = true; }
            if (this.SelectiveIncomingCallBarring != updatedServiceRequestService.SelectiveIncomingCallBarring) { this.SelectiveIncomingCallBarring = updatedServiceRequestService.SelectiveIncomingCallBarring; updated = true; }
            if (this.SelectiveCallForwarding != updatedServiceRequestService.SelectiveCallForwarding) { this.SelectiveCallForwarding = updatedServiceRequestService.SelectiveCallForwarding; updated = true; }
            if (this.DialNumberCallOutBarring != updatedServiceRequestService.DialNumberCallOutBarring) { this.DialNumberCallOutBarring = updatedServiceRequestService.DialNumberCallOutBarring; updated = true; }
            if (this.CallForwardingBasedonBlackList != updatedServiceRequestService.CallForwardingBasedonBlackList) { this.CallForwardingBasedonBlackList = updatedServiceRequestService.CallForwardingBasedonBlackList; updated = true; }
            if (this.CallForwardingBusy != updatedServiceRequestService.CallForwardingBusy) { this.CallForwardingBusy = updatedServiceRequestService.CallForwardingBusy; updated = true; }
            if (this.CallForwardingNoReply != updatedServiceRequestService.CallForwardingNoReply) { this.CallForwardingNoReply = updatedServiceRequestService.CallForwardingNoReply; updated = true; }
            if (this.CallForwardingOffline != updatedServiceRequestService.CallForwardingOffline) { this.CallForwardingOffline = updatedServiceRequestService.CallForwardingOffline; updated = true; }
            if (this.CallForwardingOnUserNotReachable != updatedServiceRequestService.CallForwardingOnUserNotReachable) { this.CallForwardingOnUserNotReachable = updatedServiceRequestService.CallForwardingOnUserNotReachable; updated = true; }
            if (this.CallForwardingNoReplyinCallWaiting != updatedServiceRequestService.CallForwardingNoReplyinCallWaiting) { this.CallForwardingNoReplyinCallWaiting = updatedServiceRequestService.CallForwardingNoReplyinCallWaiting; updated = true; }
            if (this.MultiRinging != updatedServiceRequestService.MultiRinging) { this.MultiRinging = updatedServiceRequestService.MultiRinging; updated = true; }
            if (this.ConvergentInterPersonalService != updatedServiceRequestService.ConvergentInterPersonalService) { this.ConvergentInterPersonalService = updatedServiceRequestService.ConvergentInterPersonalService; updated = true; }
            if (this.CallForwardingBySequence != updatedServiceRequestService.CallForwardingBySequence) { this.CallForwardingBySequence = updatedServiceRequestService.CallForwardingBySequence; updated = true; }
            if (this.UserNumberChange != updatedServiceRequestService.UserNumberChange) { this.UserNumberChange = updatedServiceRequestService.UserNumberChange; updated = true; }
            if (this.BlackNumberData != updatedServiceRequestService.BlackNumberData) { this.BlackNumberData = updatedServiceRequestService.BlackNumberData; updated = true; }
            if (this.WhiteNumberData != updatedServiceRequestService.WhiteNumberData) { this.WhiteNumberData = updatedServiceRequestService.WhiteNumberData; updated = true; }
            if (this.OwedRestriction != updatedServiceRequestService.OwedRestriction) { this.OwedRestriction = updatedServiceRequestService.OwedRestriction; updated = true; }
            if (this.Strategy != updatedServiceRequestService.Strategy) { this.Strategy = updatedServiceRequestService.Strategy; updated = true; }
            if (this.GreenNumberData != updatedServiceRequestService.GreenNumberData) { this.GreenNumberData = updatedServiceRequestService.GreenNumberData; updated = true; }
            if (this.RedNumberData != updatedServiceRequestService.RedNumberData) { this.RedNumberData = updatedServiceRequestService.RedNumberData; updated = true; }
            if (this.BarringOfAllOutgoingCalls != updatedServiceRequestService.BarringOfAllOutgoingCalls) { this.BarringOfAllOutgoingCalls = updatedServiceRequestService.BarringOfAllOutgoingCalls; updated = true; }
            if (this.BarringOfAllOutgoingInternationalCalls != updatedServiceRequestService.BarringOfAllOutgoingInternationalCalls) { this.BarringOfAllOutgoingInternationalCalls = updatedServiceRequestService.BarringOfAllOutgoingInternationalCalls; updated = true; }
            if (this.BarringOfOutgoingInternationalCallsExceptThoseDirectedToTheHomePlmnCountry != updatedServiceRequestService.BarringOfOutgoingInternationalCallsExceptThoseDirectedToTheHomePlmnCountry) { this.BarringOfOutgoingInternationalCallsExceptThoseDirectedToTheHomePlmnCountry = updatedServiceRequestService.BarringOfOutgoingInternationalCallsExceptThoseDirectedToTheHomePlmnCountry; updated = true; }
            if (this.BarringOfAllIncomingCalls != updatedServiceRequestService.BarringOfAllIncomingCalls) { this.BarringOfAllIncomingCalls = updatedServiceRequestService.BarringOfAllIncomingCalls; updated = true; }
            if (this.BarringOfIncomingCallsWhenRoamingOutsideTheHomePlmnCountry != updatedServiceRequestService.BarringOfIncomingCallsWhenRoamingOutsideTheHomePlmnCountry) { this.BarringOfIncomingCallsWhenRoamingOutsideTheHomePlmnCountry = updatedServiceRequestService.BarringOfIncomingCallsWhenRoamingOutsideTheHomePlmnCountry; updated = true; }
            if (this.SpeedDial != updatedServiceRequestService.SpeedDial) { this.SpeedDial = updatedServiceRequestService.SpeedDial; updated = true; }
            if (this.GreenCall != updatedServiceRequestService.GreenCall) { this.GreenCall = updatedServiceRequestService.GreenCall; updated = true; }
            if (this.SetDataUpgrade != updatedServiceRequestService.SetDataUpgrade) { this.SetDataUpgrade = updatedServiceRequestService.SetDataUpgrade; updated = true; }
            if (this.AutoConsole != updatedServiceRequestService.AutoConsole) { this.AutoConsole = updatedServiceRequestService.AutoConsole; updated = true; }
            if (this.NightService != updatedServiceRequestService.NightService) { this.NightService = updatedServiceRequestService.NightService; updated = true; }
            if (this.BackupNumber != updatedServiceRequestService.BackupNumber) { this.BackupNumber = updatedServiceRequestService.BackupNumber; updated = true; }
            if (this.Absence != updatedServiceRequestService.Absence) { this.Absence = updatedServiceRequestService.Absence; updated = true; }
            if (this.AuthorizedCodeForStdIdd != updatedServiceRequestService.AuthorizedCodeForStdIdd) { this.AuthorizedCodeForStdIdd = updatedServiceRequestService.AuthorizedCodeForStdIdd; updated = true; }
            if (this.Hotline != updatedServiceRequestService.Hotline) { this.Hotline = updatedServiceRequestService.Hotline; updated = true; }
            if (this.CetMaliciousCommunicationIdentification != updatedServiceRequestService.CetMaliciousCommunicationIdentification) { this.CetMaliciousCommunicationIdentification = updatedServiceRequestService.CetMaliciousCommunicationIdentification; updated = true; }
            if (this.MissCallNotify != updatedServiceRequestService.MissCallNotify) { this.MissCallNotify = updatedServiceRequestService.MissCallNotify; updated = true; }
            if (this.SubscriptionStatus != updatedServiceRequestService.SubscriptionStatus) { this.SubscriptionStatus = updatedServiceRequestService.SubscriptionStatus; updated = true; }
            if (this.UsbDongleOneKeyService != updatedServiceRequestService.UsbDongleOneKeyService) { this.UsbDongleOneKeyService = updatedServiceRequestService.UsbDongleOneKeyService; updated = true; }
            if (this.IRoamingInboundSingleImsiMultiMsisdn != updatedServiceRequestService.IRoamingInboundSingleImsiMultiMsisdn) { this.IRoamingInboundSingleImsiMultiMsisdn = updatedServiceRequestService.IRoamingInboundSingleImsiMultiMsisdn; updated = true; }
            if (this.IRoamingOutboundSingleImsiMultiMsisdn != updatedServiceRequestService.IRoamingOutboundSingleImsiMultiMsisdn) { this.IRoamingOutboundSingleImsiMultiMsisdn = updatedServiceRequestService.IRoamingOutboundSingleImsiMultiMsisdn; updated = true; }
            if (this.NpaSplit != updatedServiceRequestService.NpaSplit) { this.NpaSplit = updatedServiceRequestService.NpaSplit; updated = true; }
            if (this.AllCallForwardingGroup != updatedServiceRequestService.AllCallForwardingGroup) { this.AllCallForwardingGroup = updatedServiceRequestService.AllCallForwardingGroup; updated = true; }
            if (this.ConditionalCallForwardingGroup != updatedServiceRequestService.ConditionalCallForwardingGroup) { this.ConditionalCallForwardingGroup = updatedServiceRequestService.ConditionalCallForwardingGroup; updated = true; }
            if (this.GeneralOrigIdentificationRestriction != updatedServiceRequestService.GeneralOrigIdentificationRestriction) { this.GeneralOrigIdentificationRestriction = updatedServiceRequestService.GeneralOrigIdentificationRestriction; updated = true; }
            if (this.MultimediaOfOrigIdRestriction != updatedServiceRequestService.MultimediaOfOrigIdRestriction) { this.MultimediaOfOrigIdRestriction = updatedServiceRequestService.MultimediaOfOrigIdRestriction; updated = true; }
            if (this.TerminatingIdentityRestriction != updatedServiceRequestService.TerminatingIdentityRestriction) { this.TerminatingIdentityRestriction = updatedServiceRequestService.TerminatingIdentityRestriction; updated = true; }
            if (this.DistinctiveRing != updatedServiceRequestService.DistinctiveRing) { this.DistinctiveRing = updatedServiceRequestService.DistinctiveRing; updated = true; }
            if (this.VisitedNetworkImpu != updatedServiceRequestService.VisitedNetworkImpu) { this.VisitedNetworkImpu = updatedServiceRequestService.VisitedNetworkImpu; updated = true; }
            if (this.SeasonalSuspend != updatedServiceRequestService.SeasonalSuspend) { this.SeasonalSuspend = updatedServiceRequestService.SeasonalSuspend; updated = true; }
            if (this.NumberInvalidation != updatedServiceRequestService.NumberInvalidation) { this.NumberInvalidation = updatedServiceRequestService.NumberInvalidation; updated = true; }
            if (this.IroamingServiceProvision != updatedServiceRequestService.IroamingServiceProvision) { this.IroamingServiceProvision = updatedServiceRequestService.IroamingServiceProvision; updated = true; }
            if (this.IptvCallerId != updatedServiceRequestService.IptvCallerId) { this.IptvCallerId = updatedServiceRequestService.IptvCallerId; updated = true; }
            if (this.IptvVideoCall != updatedServiceRequestService.IptvVideoCall) { this.IptvVideoCall = updatedServiceRequestService.IptvVideoCall; updated = true; }
            if (this.NumberPortability != updatedServiceRequestService.NumberPortability) { this.NumberPortability = updatedServiceRequestService.NumberPortability; updated = true; }
            if (this.SecretaryService != updatedServiceRequestService.SecretaryService) { this.SecretaryService = updatedServiceRequestService.SecretaryService; updated = true; }
            if (this.SalesBlock != updatedServiceRequestService.SalesBlock) { this.SalesBlock = updatedServiceRequestService.SalesBlock; updated = true; }
            if (this.FilterCriteria != updatedServiceRequestService.FilterCriteria) { this.FilterCriteria = updatedServiceRequestService.FilterCriteria; updated = true; }
            if (this.CallerInformation != updatedServiceRequestService.CallerInformation) { this.CallerInformation = updatedServiceRequestService.CallerInformation; updated = true; }
            if (this.PresenceStatus != updatedServiceRequestService.PresenceStatus) { this.PresenceStatus = updatedServiceRequestService.PresenceStatus; updated = true; }
            if (this.OneNumberService != updatedServiceRequestService.OneNumberService) { this.OneNumberService = updatedServiceRequestService.OneNumberService; updated = true; }
            if (this.AssociationList != updatedServiceRequestService.AssociationList) { this.AssociationList = updatedServiceRequestService.AssociationList; updated = true; }
            if (this.Pin != updatedServiceRequestService.Pin) { this.Pin = updatedServiceRequestService.Pin; updated = true; }

            // important: ServiceRequestService.Update() will only update stored.Access if it is null, or (stored.userId == Guid.Empty && update.Id > stored.Id)
            // this is important to enable manual staff update.
            if (this.Access == null && updatedServiceRequestService.Access != null) { this.Access = updatedServiceRequestService.Access; updated = true; }
            else if (this.Access != null && updatedServiceRequestService.Access != null && this.UserId == Guid.Empty && long.Parse(updatedServiceRequestService.Id) > long.Parse(this.Id)) { this.Access = updatedServiceRequestService.Access; updated = true; }

            // important: I will not update this.UserId if update.UserId is Guid.Empty so that I don't override staff this.UserId. I need this to be valid for this.Access test above
            if (this.UserId != updatedServiceRequestService.UserId && updatedServiceRequestService.UserId != Guid.Empty) { this.UserId = updatedServiceRequestService.UserId; updated = true; }

            //if (this.ServiceRequests != updatedServiceRequestService.ServiceRequests) { this.ServiceRequests = updatedServiceRequestService.ServiceRequests; updated = true; }

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