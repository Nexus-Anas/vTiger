using APIntegro.Application.Common.Errors;
using APIntegro.Application.IHandlers;
using APIntegro.Application.Services.Authentication;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace APIntegro.Infrastructure.Handlers;

public class AuthHandler : IAuthHandler
{
    private readonly IApiHandler _apiHandler;

    public AuthHandler(IApiHandler apiHandler)
    {
        _apiHandler = apiHandler;
    }

    public async Task<AuthenticationResponse> Login(LoginRequest loginRequest)
    {
        var challResult = await GetChallengeToken(loginRequest.UserName);

        if (challResult["success"].Value<bool>())
            {
                var challengeResponse = challResult.ToObject<ChallengeResponse>();

                var token = challengeResponse.Result.Token;
                var combinedString = $"{token}{loginRequest.AccessKey}";
                var hashedKey = GetMd5Hash(combinedString);

                var logparamsDictionary = new Dictionary<string, string>
                {
                    { "operation" , "login" },
                    { "username" , loginRequest.UserName },
                    { "accessKey" , hashedKey }
                };

                var loginResponse = await _apiHandler.PostApiResponse(logparamsDictionary);

                if (loginResponse["success"].Value<bool>())
                {
                    return loginResponse.ToObject<AuthenticationResponse>();
                }
                else
                {
                    var errorObject = loginResponse["error"];
                    var errorMessage = errorObject["message"].Value<string>();
                    var errorCode = errorObject["code"].Value<string>();

                    throw new VtigerException(errorCode, errorMessage);
                }

            }
        else
        {
            var errorObject = challResult["error"];
            var errorMessage = errorObject["message"].Value<string>();
            var errorCode = errorObject["code"].Value<string>();

            throw new VtigerException(errorCode,errorMessage);
        }

    }

    public async Task<JObject?> GetChallengeToken(string userName)
    {
        var challparamsDictionary = new Dictionary<string, string>
            { 
                { "operation" , "getchallenge" },
                { "username" , userName }
            };

        return await _apiHandler.GetApiResponse(challparamsDictionary);
    }

    public async Task<AuthenticationResponse> Logout(string SessionName)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "logout" },
            { "sessionName", SessionName }
        };

        var response = await _apiHandler.PostApiResponse(urlParams);

        if (response["success"].Value<bool>())
        {
            return response.ToObject<AuthenticationResponse>();
        }
        else
        {
            var errorObject = response["error"];
            var errorMessage = errorObject["message"].Value<string>();
            var errorCode = errorObject["code"].Value<string>();

            throw new VtigerException(errorCode, errorMessage);
        }

    }

    public string GetMd5Hash(string combinedString)
    {
        if (combinedString == null)
            return string.Empty;
        byte[] data;
        using (MD5 md5Hasher = MD5.Create())
            data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(combinedString));
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        return sBuilder.ToString();
    }

}
