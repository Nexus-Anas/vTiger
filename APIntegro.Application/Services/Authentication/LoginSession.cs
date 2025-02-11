namespace APIntegro.Application.Services.Authentication;

public class LoginSession
{
    public User User { get; set; }
}

public class User
{
    public string username { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string email { get; set; }
    public string time_zone { get; set; }
    public string hour_format { get; set; }
    public string date_format { get; set; }
    public string is_admin { get; set; }
    public string call_duration { get; set; }
    public string other_event_duration { get; set; }
    public string sessionName { get; set; }
    public string userId { get; set; }
    public string version { get; set; }
    public string vtigerVersion { get; set; }
}