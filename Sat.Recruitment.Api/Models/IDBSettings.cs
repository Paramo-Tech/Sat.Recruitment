namespace Sat.Recruitment.Api.Models
{
    public interface IDBSettings
    {
       string Server { get; set; }
       string Database { get; set; } 
       string Collection { get; set; }
    }
}
