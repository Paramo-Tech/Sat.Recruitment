namespace Sat.Rec.Api.Extensions.DTO
{
    public static class UserDTOExtensions
    {
        public static Models.User ToModel(this Api.DTO.CreateUserDTO dtoModel)
        {
            return new Models.User
            {
                Address = dtoModel.Address,
                Email = dtoModel.Email,
                Name = dtoModel.Name,
                Money = dtoModel.Money,
                Phone = dtoModel.Phone,
                UserTypeId = dtoModel.UserTypeId
            };
        }

        public static Models.User ToModel(this Api.DTO.UpdateUserDTO dtoModel, int userId)
        {
            return new Models.User
            {
                UserId = userId,
                Address = dtoModel.Address,
                Email = dtoModel.Email,
                Name = dtoModel.Name,
                Money = dtoModel.Money,
                Phone = dtoModel.Phone,
                UserTypeId = dtoModel.UserTypeId
            };
        }
    }
}
