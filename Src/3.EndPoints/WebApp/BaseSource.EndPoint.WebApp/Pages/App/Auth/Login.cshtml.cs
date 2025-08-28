using BaseSource.Core.Application.Providers;
using BaseSource.EndPoint.WebApp.Pages.App.BasePage;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BaseSource.EndPoint.WebApp.Pages.App.Auth;

public class LoginModel : BasePageModel
{
    private readonly HttpClient _httpClient;

    public LoginModel(ProviderFactory factory, IHttpClientFactory httpClientFactory) : base(factory)
    {
        _httpClient = httpClientFactory.CreateClient("API");
    }

    [BindProperty]
    public LoginRequest Parameter { get; set; } = new LoginRequest();

    public string ErrorMessage { get; set; } = string.Empty;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var json = Factory.Serializer.Serialize(Parameter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Authentication/Login", content);

            if (response.IsSuccessStatusCode)
            {
                // Read the response and handle the authentication token
                var responseContent = await response.Content.ReadAsStringAsync();
                // You'll need to store the authentication token (e.g., in cookies)

                return RedirectToPage("/App/Index");
            }
            else
            {
                ErrorMessage = "Invalid username or password";
                return Page();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "An error occurred during login";
            return Page();
        }
    }
}

public class LoginRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ReturnUrl { get; set; } = "/";
    public bool IsRemember { get; set; }
}
