using System.Collections.Generic;

namespace DTPortal.Core.DTOs
{
    public class GraphDTO
    {
        public IEnumerable<WeekGraphDTO> WeekCount { get; set; }

        public IEnumerable<MonthGraphDTO> MonthCount { get; set; }

        public IEnumerable<YearGraphDTO> YearCount { get; set; }
    }
}
