namespace Sat.Recruitment.Domain.Models.Enum
{
    public enum EUserType
    {
        Normal,
        SuperUser,
        Premium,
        NotSpecified //The original code is not validating if UserType is null or empty
    }
}
