using System;
using System.Threading;
using NUnit.Framework;
using TaskScheduler.EventBus;
using TaskScheduler.Events;
using TaskScheduler.UnitTests.EventBus.HelperTestHandlers;

namespace TaskScheduler.UnitTests.EventBus
{
    [TestFixture]
    public class When_the_handler_in_the_bus_throws_an_exception
    {
        private readonly ErrorThrownTestHandler _handler = new ErrorThrownTestHandler();

        [TestFixtureSetUp]
        public void Setup()
        {
            var eventHandler = new EventHandlerFactory();
            _handler.ResetTest();
            eventHandler.RegisterInstance(() => new FaultyElapsedTimeHandler());
            eventHandler.RegisterInstance(() => _handler);
            Bus.InitializeBus(eventHandler, null);
            Bus.Instance.Publish(new ElapsedTimeEvent());
            Thread.Sleep(20);
        }

        [Test]
        public void it_should_publish_an_error_thrown_event()
        {
             Assert.That(_handler.WasCalled, Is.True);
        }
    }


    namespace HelperTestHandlers
    {
        public class FaultyElapsedTimeHandler : IEventHandler<ElapsedTimeEvent>
        {
            public void Handle(ElapsedTimeEvent @event)
            {
                throw new Exception();
            }
        }

        public class ErrorThrownTestHandler : IEventHandler<ErrorThrownEvent>
        {
            public void ResetTest()
            {
                WasCalled = false;
            }
            public bool WasCalled { get; private set; }
            public void Handle(ErrorThrownEvent @event)
            {
                WasCalled = true;
            }
        }
    }
}
