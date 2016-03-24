using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Reflection;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Entity.SqlServer;

namespace Ia.Ngn.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Service Request Service support class for Next Generation Network (NGN) data model.
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
        public ServiceRequestService() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Update the service request service table with a list using a service list as referece
        /// </summary>
        public static void UpdateWithServiceList(List<string> serviceList, List<Ia.Ngn.Cl.Model.ServiceRequestService> newServiceRequestServiceList, out string result)
        {
            int readItemCount, existingItemCount, insertedItemCount, updatedItemCount, deletedItemCount;
            Ia.Ngn.Cl.Model.ServiceRequestService serviceRequestService, newServiceRequestService;
            Ia.Ngn.Cl.Model.Access access;
            List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList;

            readItemCount = existingItemCount = insertedItemCount = updatedItemCount = deletedItemCount = 0;
            result = "";

            readItemCount = newServiceRequestServiceList.Count;

            //if (newServiceRequestServiceList.Count > 0)
            //{
            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below:
                foreach (Ia.Ngn.Cl.Model.ServiceRequestService srs in newServiceRequestServiceList)
                {
                    newServiceRequestService = new Ia.Ngn.Cl.Model.ServiceRequestService();

                    newServiceRequestService.Id = srs.Id;
                    newServiceRequestService.Service = srs.Service;
                    newServiceRequestService.ServiceType = srs.ServiceType;
                    newServiceRequestService.AreaCode = srs.AreaCode;
                    newServiceRequestService.CallWaiting = srs.CallWaiting;
                    newServiceRequestService.CallForwarding = srs.CallForwarding;
                    newServiceRequestService.AlarmCall = srs.AlarmCall;
                    newServiceRequestService.CallBarring = srs.CallBarring;
                    newServiceRequestService.InternationalCallingUserControlled = srs.InternationalCallingUserControlled;
                    newServiceRequestService.InternationalCalling = srs.InternationalCalling;
                    newServiceRequestService.CallerId = srs.CallerId;
                    newServiceRequestService.WakeupCall = srs.WakeupCall;
                    newServiceRequestService.ConferenceCall = srs.ConferenceCall;
                    newServiceRequestService.ServiceSuspension = srs.ServiceSuspension;
                    newServiceRequestService.ServiceSuspensionTypeId = srs.ServiceSuspensionTypeId;
                    newServiceRequestService.AbbriviatedCalling = srs.AbbriviatedCalling;
                    newServiceRequestService.CallForwardingUnconditional = srs.CallForwardingUnconditional;
                    newServiceRequestService.CallingLineIdentificationRestriction = srs.CallingLineIdentificationRestriction;
                    newServiceRequestService.ConnectedLineIdentificationRestriction = srs.ConnectedLineIdentificationRestriction;
                    newServiceRequestService.WakeUp = srs.WakeUp;
                    newServiceRequestService.CallForwardingByTime = srs.CallForwardingByTime;
                    newServiceRequestService.MutlimediaInformationPresentation = srs.MutlimediaInformationPresentation;
                    newServiceRequestService.SelectiveOutgoingCallBarring = srs.SelectiveOutgoingCallBarring;
                    newServiceRequestService.DialNumberCallOutAllow = srs.DialNumberCallOutAllow;
                    newServiceRequestService.DoNotDisturb = srs.DoNotDisturb;
                    newServiceRequestService.OutgoingCallBarring = srs.OutgoingCallBarring;
                    newServiceRequestService.TemporaryLine = srs.TemporaryLine;
                    newServiceRequestService.CodecControl = srs.CodecControl;
                    newServiceRequestService.SelectiveIncomingCallBarring = srs.SelectiveIncomingCallBarring;
                    newServiceRequestService.SelectiveCallForwarding = srs.SelectiveCallForwarding;
                    newServiceRequestService.DialNumberCallOutBarring = srs.DialNumberCallOutBarring;
                    newServiceRequestService.CallForwardingBasedonBlackList = srs.CallForwardingBasedonBlackList;
                    newServiceRequestService.CallForwardingBusy = srs.CallForwardingBusy;
                    newServiceRequestService.CallForwardingNoReply = srs.CallForwardingNoReply;
                    newServiceRequestService.CallForwardingOffline = srs.CallForwardingOffline;
                    newServiceRequestService.CallForwardingOnUserNotReachable = srs.CallForwardingOnUserNotReachable;
                    newServiceRequestService.CallForwardingNoReplyinCallWaiting = srs.CallForwardingNoReplyinCallWaiting;
                    newServiceRequestService.MultiRinging = srs.MultiRinging;
                    newServiceRequestService.ConvergentInterPersonalService = srs.ConvergentInterPersonalService;
                    newServiceRequestService.CallForwardingBySequence = srs.CallForwardingBySequence;
                    newServiceRequestService.UserNumberChange = srs.UserNumberChange;
                    newServiceRequestService.BlackNumberData = srs.BlackNumberData;
                    newServiceRequestService.WhiteNumberData = srs.WhiteNumberData;
                    newServiceRequestService.OwedRestriction = srs.OwedRestriction;
                    newServiceRequestService.Strategy = srs.Strategy;
                    newServiceRequestService.GreenNumberData = srs.GreenNumberData;
                    newServiceRequestService.RedNumberData = srs.RedNumberData;
                    newServiceRequestService.BarringOfAllOutgoingCalls = srs.BarringOfAllOutgoingCalls;
                    newServiceRequestService.BarringOfAllOutgoingInternationalCalls = srs.BarringOfAllOutgoingInternationalCalls;
                    newServiceRequestService.BarringOfOutgoingInternationalCallsExceptThoseDirectedToTheHomePlmnCountry = srs.BarringOfOutgoingInternationalCallsExceptThoseDirectedToTheHomePlmnCountry;
                    newServiceRequestService.BarringOfAllIncomingCalls = srs.BarringOfAllIncomingCalls;
                    newServiceRequestService.BarringOfIncomingCallsWhenRoamingOutsideTheHomePlmnCountry = srs.BarringOfIncomingCallsWhenRoamingOutsideTheHomePlmnCountry;
                    newServiceRequestService.SpeedDial = srs.SpeedDial;
                    newServiceRequestService.GreenCall = srs.GreenCall;
                    newServiceRequestService.SetDataUpgrade = srs.SetDataUpgrade;
                    newServiceRequestService.AutoConsole = srs.AutoConsole;
                    newServiceRequestService.NightService = srs.NightService;
                    newServiceRequestService.BackupNumber = srs.BackupNumber;
                    newServiceRequestService.Absence = srs.Absence;
                    newServiceRequestService.AuthorizedCodeForStdIdd = srs.AuthorizedCodeForStdIdd;
                    newServiceRequestService.Hotline = srs.Hotline;
                    newServiceRequestService.CetMaliciousCommunicationIdentification = srs.CetMaliciousCommunicationIdentification;
                    newServiceRequestService.MissCallNotify = srs.MissCallNotify;
                    newServiceRequestService.SubscriptionStatus = srs.SubscriptionStatus;
                    newServiceRequestService.UsbDongleOneKeyService = srs.UsbDongleOneKeyService;
                    newServiceRequestService.IRoamingInboundSingleImsiMultiMsisdn = srs.IRoamingInboundSingleImsiMultiMsisdn;
                    newServiceRequestService.IRoamingOutboundSingleImsiMultiMsisdn = srs.IRoamingOutboundSingleImsiMultiMsisdn;
                    newServiceRequestService.NpaSplit = srs.NpaSplit;
                    newServiceRequestService.AllCallForwardingGroup = srs.AllCallForwardingGroup;
                    newServiceRequestService.ConditionalCallForwardingGroup = srs.ConditionalCallForwardingGroup;
                    newServiceRequestService.GeneralOrigIdentificationRestriction = srs.GeneralOrigIdentificationRestriction;
                    newServiceRequestService.MultimediaOfOrigIdRestriction = srs.MultimediaOfOrigIdRestriction;
                    newServiceRequestService.TerminatingIdentityRestriction = srs.TerminatingIdentityRestriction;
                    newServiceRequestService.DistinctiveRing = srs.DistinctiveRing;
                    newServiceRequestService.VisitedNetworkImpu = srs.VisitedNetworkImpu;
                    newServiceRequestService.SeasonalSuspend = srs.SeasonalSuspend;
                    newServiceRequestService.NumberInvalidation = srs.NumberInvalidation;
                    newServiceRequestService.IroamingServiceProvision = srs.IroamingServiceProvision;
                    newServiceRequestService.IptvCallerId = srs.IptvCallerId;
                    newServiceRequestService.IptvVideoCall = srs.IptvVideoCall;
                    newServiceRequestService.NumberPortability = srs.NumberPortability;
                    newServiceRequestService.SecretaryService = srs.SecretaryService;
                    newServiceRequestService.SalesBlock = srs.SalesBlock;
                    newServiceRequestService.FilterCriteria = srs.FilterCriteria;
                    newServiceRequestService.CallerInformation = srs.CallerInformation;
                    newServiceRequestService.PresenceStatus = srs.PresenceStatus;
                    newServiceRequestService.OneNumberService = srs.OneNumberService;
                    newServiceRequestService.AssociationList = srs.AssociationList;
                    newServiceRequestService.Pin = srs.Pin;

                    newServiceRequestService.UserId = srs.UserId;

                    // important: ServiceRequestService.Update() will only update stored.Access if it is null, or (stored.userId == Guid.Empty && update.Id > stored.Id)
                    if (srs.Access != null) newServiceRequestService.Access = (from a in db.Accesses where a.Id == srs.Access.Id select a).SingleOrDefault();
                    else newServiceRequestService.Access = null;

                    serviceRequestService = (from q in db.ServiceRequestServices where q.Id == srs.Id select q).SingleOrDefault();

                    if (serviceRequestService == null)
                    {
                        newServiceRequestService.Created = newServiceRequestService.Updated = newServiceRequestService.Inspected = DateTime.UtcNow.AddHours(3);

                        db.ServiceRequestServices.Add(newServiceRequestService);

                        insertedItemCount++;
                    }
                    else
                    {
                        // below: copy values from newServiceRequestService to serviceRequestService

                        if (serviceRequestService.Update(newServiceRequestService))
                        {
                            db.ServiceRequestServices.Attach(serviceRequestService);
                            db.Entry(serviceRequestService).State = System.Data.Entity.EntityState.Modified;

                            updatedItemCount++;
                        }
                    }
                }

                // below: this function will remove SRS that were not present in the reading

                foreach (string service in serviceList)
                {
                    newServiceRequestService = (from q in newServiceRequestServiceList where q.Service == service select q).FirstOrDefault(); //.SingleOrDefault();

                    if (newServiceRequestService == null)
                    {
                        serviceRequestService = (from q in db.ServiceRequestServices where q.Service == service select q).SingleOrDefault();

                        if (serviceRequestService != null)
                        {
                            // below: will will set all references to this SRS from all SR to null

                            serviceRequestList = (from sr in db.ServiceRequests where sr.ServiceRequestService != null && sr.ServiceRequestService.Id == serviceRequestService.Id select sr).ToList();

                            foreach (Ia.Ngn.Cl.Model.ServiceRequest sr in serviceRequestList) sr.ServiceRequestService = null;

                            db.ServiceRequestServices.Remove(serviceRequestService);

                            deletedItemCount++;
                        }
                    }
                }

                db.SaveChanges();

                result = "(" + readItemCount + "/?/" + insertedItemCount + "," + updatedItemCount + "," + deletedItemCount + ") ";
            }
            //}
            //else
            //{
            //    result = "(" + readItemCount + "/?/?) ";
            //}
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Update the ServiceRequestService table's ServiceSuspension and ServiceSuspensionTypeId to the specified state for a number list
        /// </summary>
        public static bool UpdateServiceSuspensionAndServiceSuspensionTypeIdToSpecifiedSuspensionStateForAServiceStringList(List<string> serviceList, bool state, Guid userId)
        {
            bool b;
            Ia.Ngn.Cl.Model.ServiceRequestService serviceRequestService;

            b = false;

            if (serviceList.Count > 0)
            {
                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    // below:
                    foreach (string service in serviceList)
                    {
                        serviceRequestService = (from q in db.ServiceRequestServices where q.Service == service select q).SingleOrDefault();

                        if (serviceRequestService != null)
                        {
                            if (serviceRequestService.ServiceSuspension != state)
                            {
                                serviceRequestService.ServiceSuspension = state;
                                serviceRequestService.Updated = serviceRequestService.Inspected = DateTime.UtcNow.AddHours(3);
                                serviceRequestService.UserId = userId;

                                db.ServiceRequestServices.Attach(serviceRequestService);
                                db.Entry(serviceRequestService).State = System.Data.Entity.EntityState.Modified;

                                b = true;
                            }
                        }
                    }

                    db.SaveChanges();
                }
            }
            else
            {
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.ServiceRequestService> ReadSingleAsList(string id)
        {
            List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestServiceList = (from q in db.ServiceRequestServices where q.Id == id select q).ToList();
            }

            return serviceRequestServiceList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.ServiceRequestService> ReadList(ArrayList numberList)
        {
            long i;
            long[] sp;
            List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList;

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
        public static List<Ia.Ngn.Cl.Model.ServiceRequestService> ReadList()
        {
            List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below:
                serviceRequestServiceList = (from s in db.ServiceRequestServices select s).ToList();
            }

            return serviceRequestServiceList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void UpdateServiceSuspension(string service, bool serviceSuspensionState, Guid userId, out Ia.Cl.Model.Result result)
        {
            string serviceRequestServiceId;
            Ia.Ngn.Cl.Model.ServiceRequestService serviceRequestService;

            result = new Ia.Cl.Model.Result();
            serviceRequestServiceId = Ia.Ngn.Cl.Model.ServiceRequestService.ServiceRequestServiceId(service, 1); // <type id="1" name="Dn"

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestService = (from q in db.ServiceRequestServices where q.Id == serviceRequestServiceId select q).SingleOrDefault();

                if (serviceRequestService.ServiceSuspension != serviceSuspensionState)
                {
                    serviceRequestService.ServiceSuspension = serviceSuspensionState;
                    serviceRequestService.UserId = userId;

                    db.ServiceRequestServices.Attach(serviceRequestService);
                    db.Entry(serviceRequestService).Property(x => x.ServiceSuspension).IsModified = true;

                    db.SaveChanges();

                    result.Message = "ServiceSuspension updated. ";
                }
                else
                {
                    result.AddWarning("Warning: ServiceRequestService ServiceSuspension value was not updated because its the same. ");
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Dictionary<string, string> ReadServiceAndOntNameDictionaryWithFourDigitNumberDomain(int fourDigitNumberDomain)
        {
            string s;
            Dictionary<string, string> dictionary;

            dictionary = new Dictionary<string, string>(10000);

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                var list = (from q in db.ServiceRequestServices
                            where SqlFunctions.PatIndex(fourDigitNumberDomain.ToString() + "%", q.Service) > 0
                            orderby q.Service ascending
                            select new
                            {
                                Service = q.Service,
                                Access = q.Access
                            }).ToList();

                foreach (var v in list)
                {
                    if (v.Access != null) s = v.Service + " (" + v.Access.Name + ")";
                    else s = v.Service;

                    dictionary[v.Service] = s;
                }
            }

            return dictionary;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.ServiceRequestService> ReadListOfServiceRequestServicesWithSimilarServiceNumbers(List<Ia.Ngn.Cl.Model.ServiceRequest> serviceRequestList)
        {
            int i;
            string[] sp;
            List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList;

            sp = new string[serviceRequestList.Count];

            i = 0;

            foreach (Ia.Ngn.Cl.Model.ServiceRequest serviceRequest in serviceRequestList) sp[i++] = serviceRequest.Number.ToString();

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestServiceList = (from q in db.ServiceRequestServices where sp.Contains(q.Service) select q).ToList();
            }

            return serviceRequestServiceList;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static List<string> ServiceStringList()
        {
            List<string> serviceStringList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceStringList = (from q in db.ServiceRequestServices where q.ServiceType == 1 orderby q.Service ascending select q.Service).ToList();
            }

            return serviceStringList;
        }

        ////////////////////////////////////////////////////////////////////////////    

        /// <summary>
        ///
        /// </summary>
        public static List<string> ServiceStringWithNonNullAccessList()
        {
            List<string> serviceStringList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceStringList = (from q in db.ServiceRequestServices where q.ServiceType == 1 && q.Access != null orderby q.Service ascending select q.Service).ToList();
            }

            return serviceStringList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.ServiceRequestService> WithNullAccessList()
        {
            List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below: Take(100) temp
                serviceRequestServiceList = (from q in db.ServiceRequestServices where q.Access == null orderby q.Service ascending select q).Take(100).ToList();
            }

            return serviceRequestServiceList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.ServiceRequestService> ServiceSuspensionIsTrueList()
        {
            List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestServiceList = (from s in db.ServiceRequestServices where s.ServiceSuspension == true select s).ToList();
            }

            return serviceRequestServiceList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<string> ServiceSuspensionIsTrueStringNumberList()
        {
            List<string> serviceRequestServiceNumberStringList;
            List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                // below:                
                serviceRequestServiceList = ServiceSuspensionIsTrueList();

                if (serviceRequestServiceList.Count > 0)
                {
                    serviceRequestServiceNumberStringList = new List<string>(serviceRequestServiceList.Count);

                    foreach (Ia.Ngn.Cl.Model.ServiceRequestService srs in serviceRequestServiceList)
                    {
                        serviceRequestServiceNumberStringList.Add(srs.Service);
                    }
                }
                else
                {
                    // below: not null
                    serviceRequestServiceNumberStringList = new List<string>(1);
                }
            }

            return serviceRequestServiceNumberStringList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Ia.Ngn.Cl.Model.ServiceRequestService> ServiceRequestServiceWithNullAccessList
        {
            get
            {
                List<Ia.Ngn.Cl.Model.ServiceRequestService> serviceRequestServiceList;

                using (var db = new Ia.Ngn.Cl.Model.Ngn())
                {
                    serviceRequestServiceList = (from srs in db.ServiceRequestServices where srs.Access == null select srs).ToList();
                }

                return serviceRequestServiceList;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<string> ServiceRequestServiceServiceIdWithNullAccessTestList(string testServicePrefix)
        {
            List<string> list;

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                list = (from srs in db.ServiceRequestServices where srs.Service.StartsWith(testServicePrefix) && srs.Access == null select srs.Id).ToList();
            }

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void UpdateServiceRequestServiceAccess(string service, string updatedAccessId, Guid userId, out Ia.Cl.Model.Result result)
        {
            bool saveUpdate;
            string serviceRequestServiceId;
            Ia.Ngn.Cl.Model.ServiceRequestService serviceRequestService;

            saveUpdate = false;
            result = new Ia.Cl.Model.Result();
            serviceRequestServiceId = Ia.Ngn.Cl.Model.ServiceRequestService.ServiceRequestServiceId(service, 1); // <type id="1" name="Dn"

            using (var db = new Ia.Ngn.Cl.Model.Ngn())
            {
                serviceRequestService = (from srs in db.ServiceRequestServices where srs.Id == serviceRequestServiceId select srs).SingleOrDefault();

                if (serviceRequestService != null)
                {
                    if (serviceRequestService.Access != null && serviceRequestService.Access.Id != updatedAccessId
                        || serviceRequestService.Access == null && !string.IsNullOrEmpty(updatedAccessId))
                    {
                        serviceRequestService.Access = (from a in db.Accesses where a.Id == updatedAccessId select a).SingleOrDefault();
                        serviceRequestService.UserId = userId;
                        saveUpdate = true;
                    }
                    else if (string.IsNullOrEmpty(updatedAccessId))
                    {
                        // nulling
                        serviceRequestService.Access = null;
                        serviceRequestService.UserId = userId;
                        saveUpdate = true;
                    }

                    if (saveUpdate)
                    {
                        db.ServiceRequestServices.Attach(serviceRequestService);
                        db.Entry(serviceRequestService).State = System.Data.Entity.EntityState.Modified;

                        db.SaveChanges();

                        result.Message = "Access updated. ";
                    }
                    else
                    {
                        result.AddWarning("Warning: ServiceRequestService Access value was not updated. ");
                    }
                }
                else
                {
                    result.AddWarning("Warning: ServiceRequestService is null. ");
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////    
        ////////////////////////////////////////////////////////////////////////////    
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}