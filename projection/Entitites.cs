using System.Collections.Generic;

namespace projection
{
    public class Claim
    {
        public int Id { get; set; }
        public int? PolicyId { get; set; }
        public Policy Policy { get; set; }
        public string ClaimNumber { get; set; }
    }
    
    public class Policy
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public List<Claim> ClaimCases { get; set; }
        public List<TermsAndConditions> TermsAndConditions { get; set; }
    }
    
    public class TermsAndConditions
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Label { get; set; }
        public int PolicyId { get; set; }
        public Policy Policy { get; set; }
    }
}