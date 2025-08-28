using BaseSource.Core.Application.Providers;
using BaseSource.EndPoint.WebApp.Pages.App.BasePage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaseSource.EndPoint.WebApp.Pages.App.Auth
{
    public class LogoutModel : BasePageModel
    {
        private readonly HttpClient _httpClient;

        public LogoutModel(ProviderFactory factory, IHttpClientFactory httpClientFactory) : base(factory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

      
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Call your API logout endpoint
                await _httpClient.GetAsync("api/Authentication/Signout");

                // Clear authentication cookies or tokens
                await HttpContext.SignOutAsync();

                return RedirectToPage("/App/Index");
            }
            catch
            {
                // Even if API call fails, try to clear local authentication
                await HttpContext.SignOutAsync();
                return RedirectToPage("/App/Index");
            }
        }
    }
}
