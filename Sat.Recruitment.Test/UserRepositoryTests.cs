using Moq;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Sat.Recruitment.Api.Repositories;
using Sat.Recruitment.Api.Models;
using System.IO;
using System.Data;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("UserRepositoryTests", DisableParallelization = true)]
    public class UserRepositoryTests
    {
        [Fact]
        public async Task ShouldReturnCreatedUserWhenDataIsCorrect()
        {
            var fileContent = "Juan,Juan@marmol.com,+5491154762312,Peru 2464,Normal,1234\r\n" +
                "Franco,Franco.Perez@gmail.com,+534645213542,Alvear y Colombres,Premium,112234\r\n" +
                "Agustina,Agustina@gmail.com,+534645213542,Garay y Otra Calle,SuperUser,112234\r\n";
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
            var reader = new Mock<StreamReader>(memoryStream);
            var writer = new Mock<StreamWriter>(memoryStream);
            User user = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mikeamigorena@gmail.com",
                Money = 124,
                Name = "Mike",
                UserType = "Normal"
            };
            reader.Setup(s => s.ReadToEndAsync()).ReturnsAsync(fileContent);
            writer.Setup(s => s.WriteLineAsync($"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}")).Returns(Task.CompletedTask);
            UserRepository userRepository = new UserRepository(reader.Object, writer.Object);
            User userCreated = await userRepository.Insert(user);
            Assert.NotNull(userCreated);
            Assert.Equal(user, userCreated);
        }
        [Fact]
        public async Task IsDuplicatedShouldReturnTrueWhenDuplicatedPresent()
        {
            var fileContent = "Juan,Juan@marmol.com,+5491154762312,Peru 2464,Normal,1234\r\n" +
                "Franco,Franco.Perez@gmail.com,+534645213542,Alvear y Colombres,Premium,112234\r\n" +
                "Agustina,Agustina@gmail.com,+534645213542,Garay y Otra Calle,SuperUser,112234\r\n";
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
            var reader = new Mock<StreamReader>(memoryStream);
            var writer = new Mock<StreamWriter>(memoryStream);
            reader.Setup(s => s.ReadToEndAsync()).ReturnsAsync(fileContent);

            User user = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "Juan@marmol.com",
                Money = 124,
                Name = "Mike",
                UserType = "Normal"
            };
            UserRepository userRepository = new UserRepository(reader.Object, writer.Object);
            bool isDuplicated = await userRepository.IsDuplicated(user);
            Assert.True(isDuplicated);
        }
        [Fact]
        public async Task ShouldThrowExceptionWhenConectionFails()
        {
            var fileContent = "Juan,Juan@marmol.com,+5491154762312,Peru 2464,Normal,1234\r\n" +
                "Franco,Franco.Perez@gmail.com,+534645213542,Alvear y Colombres,Premium,112234\r\n" +
                "Agustina,Agustina@gmail.com,+534645213542,Garay y Otra Calle,SuperUser,112234\r\n";
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
            var reader = new Mock<StreamReader>(memoryStream);
            var writer = new Mock<StreamWriter>(memoryStream);
            reader.Setup(s => s.ReadToEndAsync()).Throws(new Exception("Could not open file because it's in use by another process"));

            User user = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "Juan@marmol.com",
                Money = 124,
                Name = "Mike",
                UserType = "Normal"
            };
            UserRepository userRepository = new UserRepository(reader.Object, writer.Object);
            await Assert.ThrowsAsync<Exception>(async () => await userRepository.Insert(user));
        }
        [Fact]
        public async Task ShouldThrowDuplicatedExceptionWhenTheresADuplictedUser()
        {
            var fileContent = "Juan,Juan@marmol.com,+5491154762312,Peru 2464,Normal,1234\r\n" +
                "Franco,Franco.Perez@gmail.com,+534645213542,Alvear y Colombres,Premium,112234\r\n" +
                "Agustina,Agustina@gmail.com,+534645213542,Garay y Otra Calle,SuperUser,112234\r\n";
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
            var reader = new Mock<StreamReader>(memoryStream);
            var writer = new Mock<StreamWriter>(memoryStream);
            reader.Setup(s => s.ReadToEndAsync()).ReturnsAsync(fileContent);

            User user = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "Juan@marmol.com",
                Money = 124,
                Name = "Mike",
                UserType = "Normal"
            };
            UserRepository userRepository = new UserRepository(reader.Object, writer.Object);
            await Assert.ThrowsAsync<DuplicateNameException>(async () => await userRepository.Insert(user));
        }
    }
}
