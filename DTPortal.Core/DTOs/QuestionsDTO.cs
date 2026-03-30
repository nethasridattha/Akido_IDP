using System.Collections;
using System.Collections.Generic;

namespace DTPortal.Core.DTOs
{
    public class QuestionsDTO
    {
        public int orgOnboardingFormId { get; set; }
        public string question { get; set; }
        public int id { get; set; }
        public string answer { get; set; }
        public string createdOn { get; set; }
        public string updatedOn { get; set; }
    }

    public class BusinessRequirementsDTO
    {
        public IEnumerable<string> SoftwareRecommendations { get; set; }
        public IList<QuestionsDTO> AnswersList { get; set; }
    }
}
