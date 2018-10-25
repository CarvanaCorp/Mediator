using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Carvana.Mediator.Tests
{
    [TestClass]
    public class InMemoryCommandsTests
    {
        private readonly SampleCommand _sampleCommand = new SampleCommand { Content = "Party like a rockstar!" };
        private readonly InMemoryCommands _commands = new InMemoryCommands();
        
        [ExpectedException(typeof(HandlerNotRegisteredException))]
        [TestMethod]
        public async Task InMemoryCommands_NoHandler_ThrowsHandlerNotRegisteredException()
        {
            await _commands.Execute(_sampleCommand);
        }
        
        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void InMemoryCommands_RegisterTwoHandlers_ThrowsInvalidOperationException()
        {
            _commands.HandleCommand<SampleCommand>(x => { });
            _commands.HandleCommand<SampleCommand>(x => { });
        }

        [TestMethod]
        public async Task InMemoryCommands_ExecuteCommand_CommandExecuted()
        {
            var output = new List<string>();
            _commands.HandleCommand<SampleCommand>(x => output.Add(x.Content));

            await _commands.Execute(_sampleCommand);
            
            Assert.AreEqual(1, output.Count);
            Assert.AreEqual(_sampleCommand.Content, output.First());
        }
        
        private class SampleCommand
        {
            public string Content { get; set; }
        }
    }
}
