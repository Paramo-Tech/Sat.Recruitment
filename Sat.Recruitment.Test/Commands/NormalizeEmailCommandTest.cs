using Sat.Recruitment.Services.Commands.Imp;
using Xunit;

namespace Sat.Recruitment.Test.Commands
{
    public class NormalizeEmailCommandTest
    {
        [Fact]
        public void Execute_RemovesDotsBeforeAtSymbol()
        {
            var command = new NormalizeEmailCommand();
            var email = "Pedro.Gomez@gmail.com";

            var result = command.Execute(email);

            Assert.Equal("pedrogomez@gmail.com", result);
        }

        [Fact]
        public void Execute_RemovesPlusSymbolAndTextAfter()
        {
            var command = new NormalizeEmailCommand();
            var email = "pedro+gomez@gmail.com";

            var result = command.Execute(email);

            Assert.Equal("pedro@gmail.com", result);
        }

        [Fact]
        public void Execute_LowercasesAndTrimsEmail()
        {
            var command = new NormalizeEmailCommand();
            var email = "   PeDrO@gmail.com   ";

            var result = command.Execute(email);

            Assert.Equal("pedro@gmail.com", result);
        }
    }
}