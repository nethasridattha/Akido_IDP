using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DTPortal.Core.DTOs
{
    public class DataPivotByCatIdDTO
    {
        public string CategorId { get; set; }

        public string CategoryName { get; set; }
        public int Id { get; set; }
        public string DataPivotUID { get; set; }

        public string DataPivotName { get; set; }
        public string AuthSchema { get; set; }
        public string DataPivotLogo { get; set; }
        public string ProfileType { get; set; }
    }
}
