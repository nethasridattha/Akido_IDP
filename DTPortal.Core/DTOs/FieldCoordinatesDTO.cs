using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    internal class FieldCoordinatesDTO
    {
        public class Location
        {
            public string FieldTitle { get; set; }
            public float CoordinateX { get; set; }
            public float CoordinateY { get; set; }
            public float Height { get; set; }
            public float Width { get; set; }
        }

        public class MdlFields
        {
            public string LicenseNumber { get; set; }
            public string IssueDate { get; set; }
            public string Name { get; set; }
            public DateTime? DOB { get; set; }
            public string ExpiryDate { get; set; }
            public string Image { get; set; }
            public string PdfString { get; set; }
            public List<Location> Locations { get; set; }
        }

        public class HealthCardFields
        {
            public string InsuredMember { get; set; }
            public DateTime DOB { get; set; }
            public string PolicyNumber { get; set; }
            public string PolicyName { get; set; }
            public DateTime PolicyStartDate { get; set; }
            public DateTime PolicyEndDate { get; set; }
            public string PolicyStatus { get; set; }
            public string Photo { get; set; }
            public string Gender { get; set; }
            public string PhoneNumber { get; set; }
            public string PdfString { get; set; }
            public List<Location> Locations { get; set; }
        }
    }
}
