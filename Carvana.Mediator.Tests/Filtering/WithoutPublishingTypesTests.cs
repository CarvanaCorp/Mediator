using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Carvana.Mediator.Tests
{
    [TestClass]
    public sealed class WithoutPublishingTypesTests
    {
        private readonly InMemoryEvents _inner;
        private bool _eventWasPublished;

        public WithoutPublishingTypesTests()
        {
            _inner = new InMemoryEvents();
            _inner.RegisterEvent<SampleEvent>(x =>
            {
                _eventWasPublished = true;
                return Task.CompletedTask;
            });
            _inner.RegisterEvent<SampleBlacklistedEvent>(x =>
            {
                _eventWasPublished = true;
                return Task.CompletedTask;
            });
        }

        [TestMethod]
        public async Task WithoutPublishingTypes_NormalEvent_IsPublished()
        {
            var events = new WithoutPublishingTypes(new List<Type> { typeof(SampleBlacklistedEvent) }, _inner);

            await events.Publish(new SampleEvent());
            
            Assert.IsTrue(_eventWasPublished);
        }
        
        [TestMethod]
        public async Task WithoutPublishingTypes_BlacklistedEvent_NotPublished()
        {
            var events = new WithoutPublishingTypes(new List<Type> { typeof(SampleBlacklistedEvent) }, _inner);

            await events.Publish(new SampleBlacklistedEvent());
            
            Assert.IsFalse(_eventWasPublished);
        }

        private class SampleEvent { }
        
        private class SampleBlacklistedEvent { }
    }
}
