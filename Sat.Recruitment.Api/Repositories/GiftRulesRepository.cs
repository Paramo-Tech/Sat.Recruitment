namespace Sat.Recruitment.Api.Repositories
{
    using System.Collections.Generic;
    using Sat.Recruitment.Api.Domain;

    public class GiftRulesRepository: IGiftRulesRepository
    {
        public List<GiftRule> get()
        {
            var giftRules = new List<GiftRule> {
                new GiftRule("Normal", 10, 100, (decimal)0.8),
                new GiftRule("Normal", 100, 0, (decimal)0.12),
                new GiftRule("SuperUser", 100, 0, (decimal)0.2),
                new GiftRule("Premium", 100, 0, (decimal)2)
            };
            return giftRules;
        }
    }
}
