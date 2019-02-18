using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carvana.Mediator
{
    public sealed class WithoutPublishingTypes : IEventHandler
    {
        private readonly HashSet<Type> _excludedTypes;
        private readonly IEventHandler _inner;

        public WithoutPublishingTypes(IEnumerable<Type> excludedTypes, IEventHandler inner)
        {
            _excludedTypes = new HashSet<Type>(excludedTypes);
            _inner = inner;
        }

        public async Task Publish<TEvent>(TEvent eventMessage)
        {
            if (!_excludedTypes.Contains(typeof(TEvent)))
                await _inner.Publish(eventMessage);
        }
    }
}
