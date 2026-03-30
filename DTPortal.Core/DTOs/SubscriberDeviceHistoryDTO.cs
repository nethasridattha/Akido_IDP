using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class SubscriberDeviceHistoryDTO
    {
        public Subscriber Subscriber { get; set; }
        public SubscriberDevice SubscriberDevice { get; set; }
        public List<SubscriberDeviceHistory> SubscriberDeviceHistory { get; set; }
    }
    public class Subscriber
    {
        public string SubscriberId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DateOfBirth { get; set; }
        public string EmailId { get; set; }
        public string FullName { get; set; }
        public string IdDocNumber { get; set; }
        public string IdDocType { get; set; }
        public string MobileNumber { get; set; }
        public string SubscriberUid { get; set; }
        public string UpdatedDate { get; set; }
        public string OsName { get; set; }
        public string OsVersion { get; set; }
        public string AppVersion { get; set; }
        public string DeviceInfo { get; set; }
        public object NationalId { get; set; }
    }

    public class SubscriberDevice
    {
        public string CreatedDate { get; set; }
        public string DeviceStatus { get; set; }
        public string DeviceUid { get; set; }
        public string SubscriberDeviceId { get; set; }
        public string SubscriberUid { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class SubscriberDeviceHistory
    {
        public DateTime Created_Date { get; set; }
        public string DeviceUid { get; set; }
    }

}
