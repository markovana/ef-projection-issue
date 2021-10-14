using System.Linq;

namespace projection
{
    public static class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new DatabaseContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                var queryable = dbContext.ClaimCases.Select(c => new ClaimDto
                {
                    ClaimNumber = c.ClaimNumber,
                    Policy = c.Policy == null
                        ? null
                        : new PolicyDto
                        {
                            PolicyNumber = c.Policy.PolicyNumber,
                            TermsAndConditions = c.Policy.TermsAndConditions.Select(tc =>
                                new TermsAndConditionsDto
                                {
                                    Label = tc.Label,
                                    Code = tc.Code
                                }).ToList()
                        }
                });

                var dto = queryable.Select(c => new ClaimDto
                    {
                        ClaimNumber = c.ClaimNumber,
                        Policy = c.Policy != null
                            ? new PolicyDto
                            {
                                PolicyNumber = c.Policy.PolicyNumber,
                                TermsAndConditions = c.Policy.TermsAndConditions.Select(t => // this will cause exception
                                    new TermsAndConditionsDto
                                    {
                                        Code = t.Code
                                    }).ToList()
                            }
                            : null
                    })
                    .ToList();
            }
        }
    }
}