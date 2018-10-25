using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Carvana.Mediator.Tests
{
    [TestClass]
    public class InMemoryEventsTests
    {
        private readonly SampleEvent _sampleEvent = new SampleEvent { Content = "Don't be a Richard" };
        private readonly InMemoryEvents _events = new InMemoryEvents();
        
        [TestMethod]
        public async Task InMemoryEvents_NoHandlersRequiredPublishEvent_NoExceptions()
        {
            await _events.Publish(_sampleEvent);
        }
        
        [ExpectedException(typeof(HandlerNotRegisteredException))]
        [TestMethod]
        public async Task InMemoryEvents_PublishEventWithoutRequiredHandler_ThrowsHandlerNotRegisteredException()
        {
            var events = new InMemoryEvents(InMemoryEvents.RegistrationRequirements.ExactlyOnePerType);
            
            await events.Publish(_sampleEvent);
        }

        [TestMethod]
        public async Task InMemoryEvents_PublishEventWithOneHandler_HandlerInvokedOnce()
        {
            var output = new List<string>();
            _events.HandleEvent<SampleEvent>(x => output.Add(x.Content));

            await _events.Publish(_sampleEvent);
            
            Assert.AreEqual(1, output.Count);
            Assert.AreEqual(_sampleEvent.Content, output.First());
        }
        
        [TestMethod]
        public async Task InMemoryEvents_PublishEventWithMultipleHandlers_AllHandlersInvoked()
        {
            var output = new List<string>();
            _events.HandleEvent<SampleEvent>(x => output.Add("1"));
            _events.HandleEvent<SampleEvent>(x => output.Add("2"));
            _events.HandleEvent<SampleEvent>(x => output.Add("3"));

            await _events.Publish(_sampleEvent);
            
            CollectionAssert.AreEquivalent(new []{"1", "2", "3"}, output);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void InMemoryEvents_RegisterTwoHandlersForSameEventWithExactlyOneHandlerRestrictions_ThrowsInvalidOperationException()
        {
            var events = new InMemoryEvents(InMemoryEvents.RegistrationRequirements.ExactlyOnePerType);
            
            events.HandleEvent<SampleEvent>(x => { });
            events.HandleEvent<SampleEvent>(x => { });
        }
        
        private class SampleEvent
        {
            public string Content { get; set; }
        }
    }
}
