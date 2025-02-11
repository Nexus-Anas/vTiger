using APIntegro.WEB.Data;
using MudBlazor;

namespace APIntegro.WEB.Pages.Authentication;

public partial class Login
{
    private async Task Submit()
    {
        if (_isDisabled) return;
        _isDisabled = true;

        var user = await AttemptLogin();

        if (user is not null)
            await SignIn(user);
        else ShowLoginError();
    }


    private async Task<User> AttemptLogin()
    {
        _btnSubmitIcon = string.Empty;
        _isLoading = true;
        return await _service.Login(LoginVM);
    }


    private async Task SignIn(User user)
    {
        Session.User = user;
        Session.IsAdminLogin = true;
        Session.DisplayNotif = true;

        await InvokeAsync(StateHasChanged);
        await Task.Delay(2000).ConfigureAwait(false);

        Nav.NavigateTo("/");
    }


    private void ShowLoginError()
    {
        _isLoading = false;
        _isDisabled = false;
        _btnSubmitIcon = Icons.Material.Filled.Login;
        Snackbar.Add("User does not exist", Severity.Error);
    }
}