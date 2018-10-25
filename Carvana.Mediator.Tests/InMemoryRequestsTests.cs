using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Carvana.Mediator.Tests
{
    [TestClass]
    public class InMemoryRequestsTests
    {
        private readonly SampleRequest _sampleRequest = new SampleRequest { Content = "Where are the fruit snacks?" };
        private readonly SampleResponse _sampleResponse = new SampleResponse { Content = "In the breakroom" };
        private readonly InMemoryRequests _requests = new InMemoryRequests();
        
        [ExpectedException(typeof(HandlerNotRegisteredException))]
        [TestMethod]
        public async Task InMemoryRequests_NoHandler_ThrowsHandlerNotRegisteredException()
        {
            await _requests.Handle<SampleRequest, SampleResponse>(_sampleRequest);
        }
        
        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void InMemoryRequests_RegisterTwoHandlers_ThrowsInvalidOperationException()
        {
            _requests.Handle<SampleRequest, SampleResponse>(x => _sampleResponse);
            _requests.Handle<SampleRequest, SampleResponse>(x => _sampleResponse);
        }

        [TestMethod]
        public async Task InMemoryRequests_GetResponse_ResponseCorrect()
        {
            _requests.Handle<SampleRequest, SampleResponse>(x => _sampleResponse);

            var resp = await _requests.Handle<SampleRequest, SampleResponse>(_sampleRequest);
            
            Assert.AreEqual(_sampleResponse.Content, resp.Content);
        }
        
        private class SampleRequest
        {
            public string Content { get; set; }
        }
        
        private class SampleResponse
        {
            public string Content { get; set; }
        }
    }
}
