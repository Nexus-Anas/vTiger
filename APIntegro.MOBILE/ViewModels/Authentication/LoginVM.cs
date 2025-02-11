using System.ComponentModel.DataAnnotations;

namespace APIntegro.MOBILE.ViewModels.Authentication;

public class LoginVM
{
    [Required(ErrorMessage = "Please enter a username")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Please enter an access key")]
    public string AccessKey { get; set; }

    public LoginVM() { }

    public LoginVM(string username, string accesskey)
    {
        UserName = username;
        AccessKey = accesskey;
    }
}