namespace Sat.Recruitment.Api.Repositories
{
    using Sat.Recruitment.Api.Domain;
    using System.Collections.Generic;

    public interface IGiftRulesRepository
    {
        public List<GiftRule> get();
    }
}
