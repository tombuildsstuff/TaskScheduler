using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TaskScheduler.EventBus;
using TaskScheduler.Events;
using TaskScheduler.Logging;
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
            var faultyElapsedTimeHandler = new FaultyElapsedTimeHandler();
            var eventHandler = new EventHandlerFactory();
            var redisLogger = Substitute.For<IRedisLogger>();
            _handler.ResetTest();
            eventHandler.RegisterInstance(() => faultyElapsedTimeHandler);
            eventHandler.RegisterInstance(() => _handler);
            Bus.InitializeBus(eventHandler, redisLogger);
            Bus.Instance.Publish(new ElapsedTimeEvent());
            Task.WaitAll(new [] { faultyElapsedTimeHandler.Task, _handler.Task}, 1000);
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
            private TaskCompletionSource<bool> _tcSource = new TaskCompletionSource<bool>();
            public Task Task 
            {
                get { return _tcSource.Task; }
            }

            public void Handle(ElapsedTimeEvent @event)
            {
                _tcSource.SetResult(true);
                throw new Exception();
            }
        }

        public class ErrorThrownTestHandler : IEventHandler<ErrorThrownEvent>
        {
            private TaskCompletionSource<bool> _tcSource = new TaskCompletionSource<bool>();
            public Task Task
            {
                get { return _tcSource.Task; }
            }
            public void ResetTest()
            {
                WasCalled = false;
            }
            public bool WasCalled { get; private set; }
            public void Handle(ErrorThrownEvent @event)
            {
                WasCalled = true;
                _tcSource.SetResult(true);
            }
        }
    }
}
