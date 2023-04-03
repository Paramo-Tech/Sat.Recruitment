using Sat.Recruitment.Api.Handlers;
using Sat.Recruitment.Api.Strategy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Models
{
    public class User
    {
        private IRewardStrategy _rewardStrategy;
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "The email address is invalid")]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }

        private void SetStrategy(IRewardStrategy strategy)
        {
            _rewardStrategy = strategy;
        }

        private void AssignReward()
        {
            Money = _rewardStrategy.AssignReward(Money);
        }

        public void ProcessNewUser()
        {
            switch (UserType)
            {
                case UserType.Normal:
                    SetStrategy(new NormalRewardStrategy());
                    break;
                case UserType.SuperUser:
                    SetStrategy(new SuperUserRewardStrategy());
                    break;
                case UserType.Premium:
                    SetStrategy(new PremiumRewardStrategy());
                    break;
            }
            if (UserType != UserType.NotAssigned)
                AssignReward();
            Email = EmailHandler.NormalizeEmail(Email);
        }
    }

    public enum UserType
    {
        NotAssigned = 0,
        Normal = 1,
        SuperUser = 2, 
        Premium = 3
    }
}
