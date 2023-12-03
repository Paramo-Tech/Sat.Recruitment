namespace Sat.Recruitment.Api.Domain
{
    public class GiftRule
    {
        public string userType { get; }
        public decimal from { get; }
        public decimal to { get; }
        public decimal coefficient { get; }  

        public GiftRule(string userType, decimal from, decimal to, decimal coefficient)
        {
            this.userType = userType;
            this.from = from;
            this.to = to;
            this.coefficient = coefficient;
        }
    }
}
