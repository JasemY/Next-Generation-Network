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
    /// Service Entity Framework class for Next Generation Network (NGN) entity model.
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
    public partial class Service2
    {
        /// <summary/>
        public Service2() { }

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
        public bool CallBarring { get; set; }
        /// <summary/>
        public bool ServiceSuspension { get; set; }
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
        public string LineCard { get; set; }

        /// <summary/>
        public virtual Access Access { get; set; }

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
        public static string ServiceId(string service, int serviceType)
        {
            // below:
            string id;

            id = service + ":" + serviceType + ":" + Ia.Ngn.Cl.Model.Data.Service.CountryCode;

            return id;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read service from Id
        /// </summary>
        public static Service2 Read(string id)
        {
            Service2 service;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                service = (from q in db.Service2s where q.Id == id select q).SingleOrDefault();
            }

            return service;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read service from Id
        /// </summary>
        public static Service2 ReadWithAccess(string id)
        {
            Service2 service;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                service = (from s in db.Service2s.Include(a => a.Access) where s.Id == id select s).SingleOrDefault();
            }

            return service;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read service of a number
        /// </summary>
        public static Service2 Read(long number)
        {
            Service2 service;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                service = (from q in db.Service2s where q.Service == number.ToString() select q).SingleOrDefault();
            }

            return service;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Read all services for a number list
        /// </summary>
        public static List<Service2> ReadList(ArrayList numberList)
        {
            long i;
            long[] sp;
            List<Service2> serviceList;

            i = 0;
            sp = new long[numberList.Count];

            foreach (long l in numberList) sp[i++] = l;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                //serviceList = (from q in db.Services where dnList.Contains(q.DN) select q).ToList();

                // var pages = context.Pages.Where(x => keys.Any(key => x.Title.Contains(key)));
                serviceList = db.Service2s.Where(q => sp.Any(v => q.Service == v.ToString())).ToList();
            }

            return serviceList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(Service2 updatedService, out string result)
        {
            bool b;
            Service2 service;

            b = false;
            result = "";

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                service = (from q in db.Service2s where q.Id == updatedService.Id select q).SingleOrDefault();

                if (service.Update(updatedService))
                {
                    db.Service2s.Attach(service);
                    db.Entry(service).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public bool Update(Service2 updatedService)
        {
            // below: this will not update Id, Created
            bool updated;

            updated = false;

            if (this.Service != updatedService.Service) { this.Service = updatedService.Service; updated = true; }
            if (this.ServiceType != updatedService.ServiceType) { this.ServiceType = updatedService.ServiceType; updated = true; }
            if (this.AreaCode != updatedService.AreaCode) { this.AreaCode = updatedService.AreaCode; updated = true; }
            if (this.CallWaiting != updatedService.CallWaiting) { this.CallWaiting = updatedService.CallWaiting; updated = true; }
            if (this.CallForwarding != updatedService.CallForwarding) { this.CallForwarding = updatedService.CallForwarding; updated = true; }
            if (this.AlarmCall != updatedService.AlarmCall) { this.AlarmCall = updatedService.AlarmCall; updated = true; }
            if (this.CallBarring != updatedService.CallBarring) { this.CallBarring = updatedService.CallBarring; updated = true; }
            if (this.InternationalCallingUserControlled != updatedService.InternationalCallingUserControlled) { this.InternationalCallingUserControlled = updatedService.InternationalCallingUserControlled; updated = true; }
            if (this.InternationalCalling != updatedService.InternationalCalling) { this.InternationalCalling = updatedService.InternationalCalling; updated = true; }
            if (this.CallerId != updatedService.CallerId) { this.CallerId = updatedService.CallerId; updated = true; }
            if (this.WakeupCall != updatedService.WakeupCall) { this.WakeupCall = updatedService.WakeupCall; updated = true; }
            if (this.ConferenceCall != updatedService.ConferenceCall) { this.ConferenceCall = updatedService.ConferenceCall; updated = true; }
            if (this.ServiceSuspension != updatedService.ServiceSuspension) { this.ServiceSuspension = updatedService.ServiceSuspension; updated = true; }
            //if (this.ServiceSuspensionTypeId != updatedService.ServiceSuspensionTypeId) { this.ServiceSuspensionTypeId = updatedService.ServiceSuspensionTypeId; updated = true; }
            if (this.AbbriviatedCalling != updatedService.AbbriviatedCalling) { this.AbbriviatedCalling = updatedService.AbbriviatedCalling; updated = true; }
            if (this.CallForwardingUnconditional != updatedService.CallForwardingUnconditional) { this.CallForwardingUnconditional = updatedService.CallForwardingUnconditional; updated = true; }
            if (this.CallingLineIdentificationRestriction != updatedService.CallingLineIdentificationRestriction) { this.CallingLineIdentificationRestriction = updatedService.CallingLineIdentificationRestriction; updated = true; }
            if (this.ConnectedLineIdentificationRestriction != updatedService.ConnectedLineIdentificationRestriction) { this.ConnectedLineIdentificationRestriction = updatedService.ConnectedLineIdentificationRestriction; updated = true; }
            if (this.WakeUp != updatedService.WakeUp) { this.WakeUp = updatedService.WakeUp; updated = true; }
            if (this.CallForwardingByTime != updatedService.CallForwardingByTime) { this.CallForwardingByTime = updatedService.CallForwardingByTime; updated = true; }
            if (this.MutlimediaInformationPresentation != updatedService.MutlimediaInformationPresentation) { this.MutlimediaInformationPresentation = updatedService.MutlimediaInformationPresentation; updated = true; }
            if (this.SelectiveOutgoingCallBarring != updatedService.SelectiveOutgoingCallBarring) { this.SelectiveOutgoingCallBarring = updatedService.SelectiveOutgoingCallBarring; updated = true; }
            if (this.DialNumberCallOutAllow != updatedService.DialNumberCallOutAllow) { this.DialNumberCallOutAllow = updatedService.DialNumberCallOutAllow; updated = true; }
            if (this.DoNotDisturb != updatedService.DoNotDisturb) { this.DoNotDisturb = updatedService.DoNotDisturb; updated = true; }
            if (this.OutgoingCallBarring != updatedService.OutgoingCallBarring) { this.OutgoingCallBarring = updatedService.OutgoingCallBarring; updated = true; }
            if (this.TemporaryLine != updatedService.TemporaryLine) { this.TemporaryLine = updatedService.TemporaryLine; updated = true; }
            if (this.CodecControl != updatedService.CodecControl) { this.CodecControl = updatedService.CodecControl; updated = true; }
            if (this.SelectiveIncomingCallBarring != updatedService.SelectiveIncomingCallBarring) { this.SelectiveIncomingCallBarring = updatedService.SelectiveIncomingCallBarring; updated = true; }
            if (this.SelectiveCallForwarding != updatedService.SelectiveCallForwarding) { this.SelectiveCallForwarding = updatedService.SelectiveCallForwarding; updated = true; }
            if (this.DialNumberCallOutBarring != updatedService.DialNumberCallOutBarring) { this.DialNumberCallOutBarring = updatedService.DialNumberCallOutBarring; updated = true; }
            if (this.CallForwardingBasedonBlackList != updatedService.CallForwardingBasedonBlackList) { this.CallForwardingBasedonBlackList = updatedService.CallForwardingBasedonBlackList; updated = true; }
            if (this.CallForwardingBusy != updatedService.CallForwardingBusy) { this.CallForwardingBusy = updatedService.CallForwardingBusy; updated = true; }
            if (this.CallForwardingNoReply != updatedService.CallForwardingNoReply) { this.CallForwardingNoReply = updatedService.CallForwardingNoReply; updated = true; }
            if (this.CallForwardingOffline != updatedService.CallForwardingOffline) { this.CallForwardingOffline = updatedService.CallForwardingOffline; updated = true; }
            if (this.CallForwardingOnUserNotReachable != updatedService.CallForwardingOnUserNotReachable) { this.CallForwardingOnUserNotReachable = updatedService.CallForwardingOnUserNotReachable; updated = true; }
            if (this.CallForwardingNoReplyinCallWaiting != updatedService.CallForwardingNoReplyinCallWaiting) { this.CallForwardingNoReplyinCallWaiting = updatedService.CallForwardingNoReplyinCallWaiting; updated = true; }
            if (this.MultiRinging != updatedService.MultiRinging) { this.MultiRinging = updatedService.MultiRinging; updated = true; }
            if (this.ConvergentInterPersonalService != updatedService.ConvergentInterPersonalService) { this.ConvergentInterPersonalService = updatedService.ConvergentInterPersonalService; updated = true; }
            if (this.CallForwardingBySequence != updatedService.CallForwardingBySequence) { this.CallForwardingBySequence = updatedService.CallForwardingBySequence; updated = true; }
            if (this.UserNumberChange != updatedService.UserNumberChange) { this.UserNumberChange = updatedService.UserNumberChange; updated = true; }
            if (this.BlackNumberData != updatedService.BlackNumberData) { this.BlackNumberData = updatedService.BlackNumberData; updated = true; }
            if (this.WhiteNumberData != updatedService.WhiteNumberData) { this.WhiteNumberData = updatedService.WhiteNumberData; updated = true; }
            if (this.OwedRestriction != updatedService.OwedRestriction) { this.OwedRestriction = updatedService.OwedRestriction; updated = true; }
            if (this.Strategy != updatedService.Strategy) { this.Strategy = updatedService.Strategy; updated = true; }
            if (this.GreenNumberData != updatedService.GreenNumberData) { this.GreenNumberData = updatedService.GreenNumberData; updated = true; }
            if (this.RedNumberData != updatedService.RedNumberData) { this.RedNumberData = updatedService.RedNumberData; updated = true; }
            if (this.BarringOfAllOutgoingCalls != updatedService.BarringOfAllOutgoingCalls) { this.BarringOfAllOutgoingCalls = updatedService.BarringOfAllOutgoingCalls; updated = true; }
            if (this.BarringOfAllOutgoingInternationalCalls != updatedService.BarringOfAllOutgoingInternationalCalls) { this.BarringOfAllOutgoingInternationalCalls = updatedService.BarringOfAllOutgoingInternationalCalls; updated = true; }
            if (this.BarringOfOutgoingInternationalCallsExceptThoseDirectedToTheHomePlmnCountry != updatedService.BarringOfOutgoingInternationalCallsExceptThoseDirectedToTheHomePlmnCountry) { this.BarringOfOutgoingInternationalCallsExceptThoseDirectedToTheHomePlmnCountry = updatedService.BarringOfOutgoingInternationalCallsExceptThoseDirectedToTheHomePlmnCountry; updated = true; }
            if (this.BarringOfAllIncomingCalls != updatedService.BarringOfAllIncomingCalls) { this.BarringOfAllIncomingCalls = updatedService.BarringOfAllIncomingCalls; updated = true; }
            if (this.BarringOfIncomingCallsWhenRoamingOutsideTheHomePlmnCountry != updatedService.BarringOfIncomingCallsWhenRoamingOutsideTheHomePlmnCountry) { this.BarringOfIncomingCallsWhenRoamingOutsideTheHomePlmnCountry = updatedService.BarringOfIncomingCallsWhenRoamingOutsideTheHomePlmnCountry; updated = true; }
            if (this.SpeedDial != updatedService.SpeedDial) { this.SpeedDial = updatedService.SpeedDial; updated = true; }
            if (this.GreenCall != updatedService.GreenCall) { this.GreenCall = updatedService.GreenCall; updated = true; }
            if (this.SetDataUpgrade != updatedService.SetDataUpgrade) { this.SetDataUpgrade = updatedService.SetDataUpgrade; updated = true; }
            if (this.AutoConsole != updatedService.AutoConsole) { this.AutoConsole = updatedService.AutoConsole; updated = true; }
            if (this.NightService != updatedService.NightService) { this.NightService = updatedService.NightService; updated = true; }
            if (this.BackupNumber != updatedService.BackupNumber) { this.BackupNumber = updatedService.BackupNumber; updated = true; }
            if (this.Absence != updatedService.Absence) { this.Absence = updatedService.Absence; updated = true; }
            if (this.AuthorizedCodeForStdIdd != updatedService.AuthorizedCodeForStdIdd) { this.AuthorizedCodeForStdIdd = updatedService.AuthorizedCodeForStdIdd; updated = true; }
            if (this.Hotline != updatedService.Hotline) { this.Hotline = updatedService.Hotline; updated = true; }
            if (this.CetMaliciousCommunicationIdentification != updatedService.CetMaliciousCommunicationIdentification) { this.CetMaliciousCommunicationIdentification = updatedService.CetMaliciousCommunicationIdentification; updated = true; }
            if (this.MissCallNotify != updatedService.MissCallNotify) { this.MissCallNotify = updatedService.MissCallNotify; updated = true; }
            if (this.SubscriptionStatus != updatedService.SubscriptionStatus) { this.SubscriptionStatus = updatedService.SubscriptionStatus; updated = true; }
            if (this.UsbDongleOneKeyService != updatedService.UsbDongleOneKeyService) { this.UsbDongleOneKeyService = updatedService.UsbDongleOneKeyService; updated = true; }
            if (this.IRoamingInboundSingleImsiMultiMsisdn != updatedService.IRoamingInboundSingleImsiMultiMsisdn) { this.IRoamingInboundSingleImsiMultiMsisdn = updatedService.IRoamingInboundSingleImsiMultiMsisdn; updated = true; }
            if (this.IRoamingOutboundSingleImsiMultiMsisdn != updatedService.IRoamingOutboundSingleImsiMultiMsisdn) { this.IRoamingOutboundSingleImsiMultiMsisdn = updatedService.IRoamingOutboundSingleImsiMultiMsisdn; updated = true; }
            if (this.NpaSplit != updatedService.NpaSplit) { this.NpaSplit = updatedService.NpaSplit; updated = true; }
            if (this.AllCallForwardingGroup != updatedService.AllCallForwardingGroup) { this.AllCallForwardingGroup = updatedService.AllCallForwardingGroup; updated = true; }
            if (this.ConditionalCallForwardingGroup != updatedService.ConditionalCallForwardingGroup) { this.ConditionalCallForwardingGroup = updatedService.ConditionalCallForwardingGroup; updated = true; }
            if (this.GeneralOrigIdentificationRestriction != updatedService.GeneralOrigIdentificationRestriction) { this.GeneralOrigIdentificationRestriction = updatedService.GeneralOrigIdentificationRestriction; updated = true; }
            if (this.MultimediaOfOrigIdRestriction != updatedService.MultimediaOfOrigIdRestriction) { this.MultimediaOfOrigIdRestriction = updatedService.MultimediaOfOrigIdRestriction; updated = true; }
            if (this.TerminatingIdentityRestriction != updatedService.TerminatingIdentityRestriction) { this.TerminatingIdentityRestriction = updatedService.TerminatingIdentityRestriction; updated = true; }
            if (this.DistinctiveRing != updatedService.DistinctiveRing) { this.DistinctiveRing = updatedService.DistinctiveRing; updated = true; }
            if (this.VisitedNetworkImpu != updatedService.VisitedNetworkImpu) { this.VisitedNetworkImpu = updatedService.VisitedNetworkImpu; updated = true; }
            if (this.SeasonalSuspend != updatedService.SeasonalSuspend) { this.SeasonalSuspend = updatedService.SeasonalSuspend; updated = true; }
            if (this.NumberInvalidation != updatedService.NumberInvalidation) { this.NumberInvalidation = updatedService.NumberInvalidation; updated = true; }
            if (this.IroamingServiceProvision != updatedService.IroamingServiceProvision) { this.IroamingServiceProvision = updatedService.IroamingServiceProvision; updated = true; }
            if (this.IptvCallerId != updatedService.IptvCallerId) { this.IptvCallerId = updatedService.IptvCallerId; updated = true; }
            if (this.IptvVideoCall != updatedService.IptvVideoCall) { this.IptvVideoCall = updatedService.IptvVideoCall; updated = true; }
            if (this.NumberPortability != updatedService.NumberPortability) { this.NumberPortability = updatedService.NumberPortability; updated = true; }
            if (this.SecretaryService != updatedService.SecretaryService) { this.SecretaryService = updatedService.SecretaryService; updated = true; }
            if (this.SalesBlock != updatedService.SalesBlock) { this.SalesBlock = updatedService.SalesBlock; updated = true; }
            if (this.FilterCriteria != updatedService.FilterCriteria) { this.FilterCriteria = updatedService.FilterCriteria; updated = true; }
            if (this.CallerInformation != updatedService.CallerInformation) { this.CallerInformation = updatedService.CallerInformation; updated = true; }
            if (this.PresenceStatus != updatedService.PresenceStatus) { this.PresenceStatus = updatedService.PresenceStatus; updated = true; }
            if (this.OneNumberService != updatedService.OneNumberService) { this.OneNumberService = updatedService.OneNumberService; updated = true; }
            if (this.AssociationList != updatedService.AssociationList) { this.AssociationList = updatedService.AssociationList; updated = true; }
            if (this.Pin != updatedService.Pin) { this.Pin = updatedService.Pin; updated = true; }
            if (this.LineCard != updatedService.LineCard) { this.LineCard = updatedService.LineCard; updated = true; }

            if (this.Access != null && updatedService.Access != null && this.Access.Id != updatedService.Access.Id) { this.Access = updatedService.Access; updated = true; }
            else if (this.Access == null && updatedService.Access != null) { this.Access = updatedService.Access; updated = true; }

            if (this.UserId != updatedService.UserId) { this.UserId = updatedService.UserId; updated = true; }

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