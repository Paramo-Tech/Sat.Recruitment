namespace Sat.Recruitment.Api.Data
{
    public class UserData{

            public StreamReader ReadUsersFromFile()
            {
                var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

                FileStream fileStream = new FileStream(path, FileMode.Open);

                StreamReader reader = new StreamReader(fileStream);
                return reader;
            }
    }
}



