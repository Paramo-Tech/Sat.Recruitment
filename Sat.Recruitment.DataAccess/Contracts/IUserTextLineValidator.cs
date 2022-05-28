namespace Sat.Recruitment.DataAccess.Contracts
{
    public interface IUserTextLineValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns>(array of fields values, has a correct format?)</returns>
        (string[], bool) IsValid(string line);
    }
}