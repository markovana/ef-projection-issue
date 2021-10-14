using System.Collections.Generic;

namespace projection
{
    public class ClaimDto
    {
        public string ClaimNumber { get; set; }
        public PolicyDto Policy { get; set; }
    }

    public class PolicyDto
    {
        public string PolicyNumber { get; set; }
        public List<TermsAndConditionsDto> TermsAndConditions = new List<TermsAndConditionsDto>();
    }

    public class TermsAndConditionsDto
    {
        public string Label { get; set; }
        public string Code { get; set; }
    }
}