using Sat.Recruitment.Domain.BonusCalculation;
using Sat.Recruitment.Domain.Model;
using System;
using Xunit;

namespace Sat.Recruitment.Test
{

    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class BonusCalculationTest
    {

        public BonusCalculationTest() { 
        }


        [Fact]
        public void BonusCalculationUserNormalGreatherThan100()
        {
            var user = new User() { UserType = "Normal", Money = 105 };
            var bonusCalculate = new BonusCalculationUserNormal();
            Assert.Equal(Convert.ToDecimal( 117.6), bonusCalculate.CalculateBonus(user));
        }

        [Fact]
        public void BonusCalculationUserNormalLessThan100()
        {
            var user = new User() { UserType = "Normal", Money = 80 };
            var bonusCalculate = new BonusCalculationUserNormal();
            Assert.Equal(Convert.ToDecimal(144), bonusCalculate.CalculateBonus(user));
        }

        [Fact]
        public void BonusCalculationUserNormalLessThan10()
        {
            var user = new User() { UserType = "Normal", Money = 8 };
            var bonusCalculate = new BonusCalculationUserNormal();
            Assert.Equal(Convert.ToDecimal(8), bonusCalculate.CalculateBonus(user));
        }

        [Fact]
        public void BonusCalculationUserSuperUserMoreThan100()
        {
            var user = new User() { UserType = "SuperUser", Money = 150 };
            var bonusCalculate = new BonusCalculationSuperUser();
            Assert.Equal(Convert.ToDecimal(180), bonusCalculate.CalculateBonus(user));
        }

        [Fact]
        public void BonusCalculationUserSuperUserLesThan100()
        {
            var user = new User() { UserType = "SuperUser", Money = 80 };
            var bonusCalculate = new BonusCalculationSuperUser();
            Assert.Equal(Convert.ToDecimal(80), bonusCalculate.CalculateBonus(user));
        }

        [Fact]
        public void BonusCalculationUserPremiumMoreThan100()
        {
            var user = new User() { UserType = "SuperUser", Money = 102 };
            var bonusCalculate = new BonusCalculationPremiun();
            Assert.Equal(Convert.ToDecimal(306), bonusCalculate.CalculateBonus(user));
        }

        [Fact]
        public void BonusCalculationUserPremiumLessThan100()
        {
            var user = new User() { UserType = "SuperUser", Money = 50 };
            var bonusCalculate = new BonusCalculationPremiun();
            Assert.Equal(Convert.ToDecimal(50), bonusCalculate.CalculateBonus(user));
        }
    }

}
