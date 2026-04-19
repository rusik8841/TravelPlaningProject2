using Microsoft.Maui.Storage;

namespace TravelPlaningProject2.Helpers;

public static class AuthHelper
{
    private const string TokenKey = "auth_token";
    private const string UserIdKey = "user_id";

    public static void SaveToken(string token) => Preferences.Set(TokenKey, token);
    public static string GetToken() => Preferences.Get(TokenKey, string.Empty);
    public static void RemoveToken() => Preferences.Remove(TokenKey);
    public static bool IsAuthenticated() => !string.IsNullOrEmpty(GetToken());

    public static void SaveUserId(int id) => Preferences.Set(UserIdKey, id.ToString());
    public static int GetUserId() => int.TryParse(Preferences.Get(UserIdKey, string.Empty), out var id) ? id : -1;
    public static void RemoveUserId() => Preferences.Remove(UserIdKey);

    public static void Logout()
    {
        RemoveToken();
        RemoveUserId();
    }
}