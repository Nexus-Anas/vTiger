namespace APIntegro.WEB.Data;

public class LoginSession
{
    public User User { get; set; }
    public bool IsAdminLogin { get; set; }
    public bool DisplayNotif { get; set; }
}