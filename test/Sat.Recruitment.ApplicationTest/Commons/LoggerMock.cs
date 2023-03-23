using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Application.Commands.CreateUser;
using Sat.Recruitment.Application.Queries.GetUser;

namespace Sat.Recruitment.ApplicationTest.Commons
{
    public static class LoggerMock
    {
        public static ILogger<CreateUserCommandHandler> Log()
        {
            var mock = new Mock<ILogger<CreateUserCommandHandler>>();

            mock.Setup(x => x.Log(
                                It.IsAny<LogLevel>(),
                                It.IsAny<EventId>(),
                                It.IsAny<It.IsAnyType>(),
                                It.IsAny<Exception>(),
                                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()))
                                .Callback(new InvocationAction(invocation =>
                                {
                                    var logLevel = (LogLevel)invocation.Arguments[0]; // The first two will always be whatever is specified in the setup above
                                    var eventId = (EventId)invocation.Arguments[1];  // so I'm not sure you would ever want to actually use them
                                    var state = invocation.Arguments[2];
                                    var exception = (Exception)invocation.Arguments[3];
                                    var formatter = invocation.Arguments[4];

                                    var invokeMethod = formatter.GetType().GetMethod("Invoke");
                                    var logMessage = (string)invokeMethod?.Invoke(formatter, new[] { state, exception });
                                }));

            return mock.Object;
        }

        public static ILogger<GetUserQueryHandler> LogGetUserQueryHandler()
        {
            var mock = new Mock<ILogger<GetUserQueryHandler>>();

            mock.Setup(x => x.Log(
                                It.IsAny<LogLevel>(),
                                It.IsAny<EventId>(),
                                It.IsAny<It.IsAnyType>(),
                                It.IsAny<Exception>(),
                                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()))
                                .Callback(new InvocationAction(invocation =>
                                {
                                    var logLevel = (LogLevel)invocation.Arguments[0]; // The first two will always be whatever is specified in the setup above
                                    var eventId = (EventId)invocation.Arguments[1];  // so I'm not sure you would ever want to actually use them
                                    var state = invocation.Arguments[2];
                                    var exception = (Exception)invocation.Arguments[3];
                                    var formatter = invocation.Arguments[4];

                                    var invokeMethod = formatter.GetType().GetMethod("Invoke");
                                    var logMessage = (string)invokeMethod?.Invoke(formatter, new[] { state, exception });
                                }));

            return mock.Object;
        }
    }
}
