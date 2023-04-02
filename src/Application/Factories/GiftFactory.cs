using Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Factories
{
    public class GiftFactory : IGiftFactory
    {
        private readonly Func<IEnumerable<IGiftService>> _factory;

        public GiftFactory(Func<IEnumerable<IGiftService>> factory)
        {
            _factory = factory;
        }

        public IGiftService Create(string userType)
        {
            var set = _factory();

            return set.Where(x => x.Type == userType).First();
        }
    }
}