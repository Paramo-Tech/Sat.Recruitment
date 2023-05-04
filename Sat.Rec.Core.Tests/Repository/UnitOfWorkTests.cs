using Microsoft.EntityFrameworkCore;
using Sat.Rec.Core.Infrastructure;
using Sat.Rec.Core.Repository;
using Sat.Rec.Models;

namespace Sat.Rec.Core.Tests.Repository
{
    [TestClass]
    public class UnitOfWorkTests
    {
        private static DbUsersContext dbUsersContext { get; set; }
        private static int? userConsecutive { get; set; }

        [TestInitialize]
        public void Setup()
        {
            if (dbUsersContext == null)
            {
                dbUsersContext = GetDbUsersContext();
            }
            if (userConsecutive == null)
            {
                userConsecutive = 1000;
            }
            else
            {
                userConsecutive++;
            }
        }

        #region Users.Get*
        [TestMethod]
        public void When_Users_GetAll_HasSameRecordCount_And_FirstRecord_IsIdentical()
        {
            //Arrange

            var userRepository = new UserRepository(dbUsersContext);
            var userTypeRepository = new UserTypeRepository(dbUsersContext);
            var gifUserTypeRepository = new GIFUserTypeRepository(dbUsersContext);

            var unitOfWork = new UnitOfWork(dbUsersContext, userRepository, userTypeRepository, gifUserTypeRepository);

            //ACT
            var result = unitOfWork.Users.GetAll().Result;

            Assert.AreEqual(dbUsersContext.Users.Count(), result.Count());
            Assert.AreEqual(dbUsersContext.Users.First().UserId, result.First().UserId);
            Assert.AreEqual(dbUsersContext.Users.First().UserTypeId, result.First().UserTypeId);
            Assert.AreEqual(dbUsersContext.Users.First().Address, result.First().Address);
            Assert.AreEqual(dbUsersContext.Users.First().Email, result.First().Email);
            Assert.AreEqual(dbUsersContext.Users.First().Money, result.First().Money);
            Assert.AreEqual(dbUsersContext.Users.First().Name, result.First().Name);
            Assert.AreEqual(dbUsersContext.Users.First().Phone, result.First().Phone);
            Assert.AreEqual(dbUsersContext.Users.First().UserId, result.First().UserId);
        }

        [TestMethod]
        public void When_Users_GetById_IsOK_returnedValue_IsIdentical()
        {
            //Arrange

            var userRepository = new UserRepository(dbUsersContext);
            var userTypeRepository = new UserTypeRepository(dbUsersContext);
            var gifUserTypeRepository = new GIFUserTypeRepository(dbUsersContext);

            var unitOfWork = new UnitOfWork(dbUsersContext, userRepository, userTypeRepository, gifUserTypeRepository);

            //ACT
            var result = unitOfWork.Users.GetById(GetUsersList().First().UserId).Result;

            Assert.AreEqual(dbUsersContext.Users.First().UserId, result.UserId);
            Assert.AreEqual(dbUsersContext.Users.First().UserTypeId, result.UserTypeId);
            Assert.AreEqual(dbUsersContext.Users.First().Address, result.Address);
            Assert.AreEqual(dbUsersContext.Users.First().Email, result.Email);
            Assert.AreEqual(dbUsersContext.Users.First().Money, result.Money);
            Assert.AreEqual(dbUsersContext.Users.First().Name, result.Name);
            Assert.AreEqual(dbUsersContext.Users.First().Phone, result.Phone);
            Assert.AreEqual(dbUsersContext.Users.First().UserId, result.UserId);
        }

        [TestMethod]
        public void When_Users_GetByName_IsOK_returnedValue_IsIdentical()
        {
            //Arrange

            var userRepository = new UserRepository(dbUsersContext);
            var userTypeRepository = new UserTypeRepository(dbUsersContext);
            var gifUserTypeRepository = new GIFUserTypeRepository(dbUsersContext);

            var unitOfWork = new UnitOfWork(dbUsersContext, userRepository, userTypeRepository, gifUserTypeRepository);

            //ACT
            var result = unitOfWork.Users.GetByName(GetUsersList().First().Name).Result;

            Assert.AreEqual(dbUsersContext.Users.First().UserId, result.UserId);
            Assert.AreEqual(dbUsersContext.Users.First().UserTypeId, result.UserTypeId);
            Assert.AreEqual(dbUsersContext.Users.First().Address, result.Address);
            Assert.AreEqual(dbUsersContext.Users.First().Email, result.Email);
            Assert.AreEqual(dbUsersContext.Users.First().Money, result.Money);
            Assert.AreEqual(dbUsersContext.Users.First().Name, result.Name);
            Assert.AreEqual(dbUsersContext.Users.First().Phone, result.Phone);
            Assert.AreEqual(dbUsersContext.Users.First().UserId, result.UserId);
        }

        [TestMethod]
        public void When_Users_GetByAddress_IsOK_returnedValue_IsIdentical()
        {
            //Arrange

            var userRepository = new UserRepository(dbUsersContext);
            var userTypeRepository = new UserTypeRepository(dbUsersContext);
            var gifUserTypeRepository = new GIFUserTypeRepository(dbUsersContext);

            var unitOfWork = new UnitOfWork(dbUsersContext, userRepository, userTypeRepository, gifUserTypeRepository);

            //ACT
            var result = unitOfWork.Users.GetByAddress(GetUsersList().First().Address).Result;

            Assert.AreEqual(dbUsersContext.Users.First().UserId, result.UserId);
            Assert.AreEqual(dbUsersContext.Users.First().UserTypeId, result.UserTypeId);
            Assert.AreEqual(dbUsersContext.Users.First().Address, result.Address);
            Assert.AreEqual(dbUsersContext.Users.First().Email, result.Email);
            Assert.AreEqual(dbUsersContext.Users.First().Money, result.Money);
            Assert.AreEqual(dbUsersContext.Users.First().Name, result.Name);
            Assert.AreEqual(dbUsersContext.Users.First().Phone, result.Phone);
            Assert.AreEqual(dbUsersContext.Users.First().UserId, result.UserId);
        }

        [TestMethod]
        public void When_Users_GetByPhone_IsOK_returnedValue_IsIdentical()
        {
            //Arrange

            var userRepository = new UserRepository(dbUsersContext);
            var userTypeRepository = new UserTypeRepository(dbUsersContext);
            var gifUserTypeRepository = new GIFUserTypeRepository(dbUsersContext);

            var unitOfWork = new UnitOfWork(dbUsersContext, userRepository, userTypeRepository, gifUserTypeRepository);

            //ACT
            var result = unitOfWork.Users.GetByPhone(GetUsersList().First().Phone).Result;

            Assert.AreEqual(dbUsersContext.Users.First().UserId, result.UserId);
            Assert.AreEqual(dbUsersContext.Users.First().UserTypeId, result.UserTypeId);
            Assert.AreEqual(dbUsersContext.Users.First().Address, result.Address);
            Assert.AreEqual(dbUsersContext.Users.First().Email, result.Email);
            Assert.AreEqual(dbUsersContext.Users.First().Money, result.Money);
            Assert.AreEqual(dbUsersContext.Users.First().Name, result.Name);
            Assert.AreEqual(dbUsersContext.Users.First().Phone, result.Phone);
            Assert.AreEqual(dbUsersContext.Users.First().UserId, result.UserId);
        }

        [TestMethod]
        public void When_Users_GetByEmail_IsOK_returnedValue_IsIdentical()
        {
            //Arrange

            var userRepository = new UserRepository(dbUsersContext);
            var userTypeRepository = new UserTypeRepository(dbUsersContext);
            var gifUserTypeRepository = new GIFUserTypeRepository(dbUsersContext);

            var unitOfWork = new UnitOfWork(dbUsersContext, userRepository, userTypeRepository, gifUserTypeRepository);

            //ACT
            var result = unitOfWork.Users.GetByEmail(GetUsersList().First().Email).Result;

            Assert.AreEqual(dbUsersContext.Users.First().UserId, result.UserId);
            Assert.AreEqual(dbUsersContext.Users.First().UserTypeId, result.UserTypeId);
            Assert.AreEqual(dbUsersContext.Users.First().Address, result.Address);
            Assert.AreEqual(dbUsersContext.Users.First().Email, result.Email);
            Assert.AreEqual(dbUsersContext.Users.First().Money, result.Money);
            Assert.AreEqual(dbUsersContext.Users.First().Name, result.Name);
            Assert.AreEqual(dbUsersContext.Users.First().Phone, result.Phone);
            Assert.AreEqual(dbUsersContext.Users.First().UserId, result.UserId);
        }

        #endregion

        #region Users.Insert
        [TestMethod]
        public void When_Users_Insert_IsOK_Then_ZeroErrors()
        {
            //Arrange

            var newUser = GetNewRandomValidUser();

            var userRepository = new UserRepository(dbUsersContext);
            var userTypeRepository = new UserTypeRepository(dbUsersContext);
            var gifUserTypeRepository = new GIFUserTypeRepository(dbUsersContext);

            var unitOfWork = new UnitOfWork(dbUsersContext, userRepository, userTypeRepository, gifUserTypeRepository);

            //ACT
            unitOfWork.Users.Add(newUser);
            unitOfWork.Save();

            Assert.IsTrue(true);
        }
        #endregion

        #region Users.Update
        [TestMethod]
        public void When_Users_Update_IsOK_Then_ZeroErrors()
        {
            //Arrange

            var userRepository = new UserRepository(dbUsersContext);
            var userTypeRepository = new UserTypeRepository(dbUsersContext);
            var gifUserTypeRepository = new GIFUserTypeRepository(dbUsersContext);

            var unitOfWork = new UnitOfWork(dbUsersContext, userRepository, userTypeRepository, gifUserTypeRepository);
            var currentRecord = unitOfWork.Users.GetAll().Result.First();
            currentRecord.Name = "Some bla bla bla text";

            //ACT

            unitOfWork.Users.Update(currentRecord);
            unitOfWork.Save();

            Assert.IsTrue(true);
        }
        #endregion

        #region users.Delete
        [TestMethod]
        public void When_Users_Delete_IsOK_Then_ZeroErrors()
        {
            //Arrange

            var userIdToDelete = GetUsersList().First().UserId;

            var userRepository = new UserRepository(dbUsersContext);
            var userTypeRepository = new UserTypeRepository(dbUsersContext);
            var gifUserTypeRepository = new GIFUserTypeRepository(dbUsersContext);

            var unitOfWork = new UnitOfWork(dbUsersContext, userRepository, userTypeRepository, gifUserTypeRepository);

            var userToDelete = unitOfWork.Users.GetById(userIdToDelete).Result;

            //ACT
            unitOfWork.Users.Delete(userToDelete);

            Assert.IsTrue(true);
        }
        #endregion

        #region UserType.Get*
        [TestMethod]
        public void When_UserType_GetAll_HasSameRecordCount_And_FirstRecord_IsIdentical()
        {
            //Arrange

            var userRepository = new UserRepository(dbUsersContext);
            var userTypeRepository = new UserTypeRepository(dbUsersContext);
            var gifUserTypeRepository = new GIFUserTypeRepository(dbUsersContext);

            var unitOfWork = new UnitOfWork(dbUsersContext, userRepository, userTypeRepository, gifUserTypeRepository);

            //ACT
            var result = unitOfWork.UserTypes.GetAll().Result;

            Assert.AreEqual(dbUsersContext.UserTypes.Count(), result.Count());
            Assert.AreEqual(dbUsersContext.UserTypes.First().UserTypeId, result.First().UserTypeId);
            Assert.AreEqual(dbUsersContext.UserTypes.First().Name, result.First().Name);
        }
        #endregion

        #region GIFUserTypes.Get*
        [TestMethod]
        public void When_GIFUserTypes_GetAll_HasSameRecordCount_And_FirstRecord_IsIdentical()
        {
            //Arrange

            var userRepository = new UserRepository(dbUsersContext);
            var userTypeRepository = new UserTypeRepository(dbUsersContext);
            var gifUserTypeRepository = new GIFUserTypeRepository(dbUsersContext);

            var unitOfWork = new UnitOfWork(dbUsersContext, userRepository, userTypeRepository, gifUserTypeRepository);

            //ACT
            var result = unitOfWork.GIFUserTypes.GetAll().Result;

            Assert.AreEqual(dbUsersContext.GIFUserTypes.Count(), result.Count());
            Assert.AreEqual(dbUsersContext.GIFUserTypes.First().UserTypeId, result.First().UserTypeId);
            Assert.AreEqual(dbUsersContext.GIFUserTypes.First().LowerLimit, result.First().LowerLimit);
            Assert.AreEqual(dbUsersContext.GIFUserTypes.First().UpperLimit, result.First().UpperLimit);
            Assert.AreEqual(dbUsersContext.GIFUserTypes.First().GIF, result.First().GIF);
        }

        [TestMethod]
        public void When_GIFUserTypes_GetAllByUserTypeId_Then_Record_IsIdentical()
        {
            //Arrange

            var userRepository = new UserRepository(dbUsersContext);
            var userTypeRepository = new UserTypeRepository(dbUsersContext);
            var gifUserTypeRepository = new GIFUserTypeRepository(dbUsersContext);

            var unitOfWork = new UnitOfWork(dbUsersContext, userRepository, userTypeRepository, gifUserTypeRepository);

            //ACT
            var result = unitOfWork.GIFUserTypes.GetAllByUserTypeId(1).Result;

            Assert.AreEqual(2, result.Count());
        }
        #endregion

        #region Dispose implementation

        [TestMethod]
        public void Verify_DisposableImplementation()
        {
            //Arrange

            var options2 = new DbContextOptionsBuilder<DbUsersContext>()
            .UseInMemoryDatabase(databaseName: "RandomNameDatabase")
            .Options;

            var tempDbUsersContext = new DbUsersContext(options2);

            var userRepository = new UserRepository(tempDbUsersContext);
            var userTypeRepository = new UserTypeRepository(tempDbUsersContext);
            var gifUserTypeRepository = new GIFUserTypeRepository(tempDbUsersContext);

            //ACT
            //just to automatically call Dispose()
            using (var unitOfWork = new UnitOfWork(tempDbUsersContext, userRepository, userTypeRepository, gifUserTypeRepository))
            {
            }

            Assert.IsTrue(true);
        }
        #endregion

        private static DbUsersContext GetDbUsersContext()
        {
            var options = new DbContextOptionsBuilder<DbUsersContext>()
            .UseInMemoryDatabase(databaseName: "TestInMemoryDatabase")
            .Options;

            var context = new DbUsersContext(options);
            context.UserTypes.Add(new UserType { UserTypeId = 1, Name = "Normal" });
            context.UserTypes.Add(new UserType { UserTypeId = 2, Name = "SuperUser" });
            context.UserTypes.Add(new UserType { UserTypeId = 3, Name = "Premium" });

            context.Users.AddRange(GetUsersList());

            context.GIFUserTypes.AddRange(
                new GIFUserType { UserTypeId = 1, LowerLimit = 100M, UpperLimit = 2147483647M, GIF = 0.12M },
                new GIFUserType { UserTypeId = 1, LowerLimit = 10M, UpperLimit = 100M, GIF = 0.8M },
                new GIFUserType { UserTypeId = 2, LowerLimit = 100M, UpperLimit = 2147483647M, GIF = 0.2M },
                new GIFUserType { UserTypeId = 3, LowerLimit = 100M, UpperLimit = 2147483647M, GIF = 2M }
            );

            context.SaveChanges();

            return context;
        }
        private static List<User> GetUsersList()
        {
            var users = new List<User>()
            {
                new User { UserId = 1, Address = "Address 1", Email = "luismontanob@email.com", Name = "Luis 1", Money = 1100, Phone = "1234567890", UserTypeId = 1 },
                new User { UserId = 2, Address = "Address 2", Email = "luismontanob222@email.com", Name = "Luis 2", Money = 2200, Phone = "123456456", UserTypeId = 2 }
            };
            return users;
        }
        private User GetNewRandomValidUser()
        {
            return new User
            {
                Name = $"Test {userConsecutive}",
                Address = $"Address {userConsecutive}",
                Email = $"luis.{userConsecutive}@domain.com",
                Money = (int)userConsecutive + 0.33M,
                UserTypeId = new Random().Next(1, 3),
                Phone = $"Phone {userConsecutive}"
            };
        }
    }
}
