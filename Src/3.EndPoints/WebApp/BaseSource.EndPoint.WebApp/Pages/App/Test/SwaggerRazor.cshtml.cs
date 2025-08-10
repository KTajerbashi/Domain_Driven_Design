using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaseSource.EndPoint.WebApp.Pages.App.Test;

public class SwaggerRazorModel : PageModel
{
    [BindProperty]
    public string Method { get; set; } = "GET";

    [BindProperty]
    public string Url { get; set; } = "/api/Test"; // relative path like ControllerName/ActionName

    [BindProperty]
    public string Body { get; set; }

    public int StatusCode { get; set; }
    public string StatusText { get; set; }
    public Dictionary<string, string> Headers { get; set; } = new();
    public string ResponseBody { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        using var client = new HttpClient
        {
            BaseAddress = new Uri($"{Request.Scheme}://{Request.Host}")
        };

        HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(Method), Url);

        if (!string.IsNullOrWhiteSpace(Body) && (Method == "POST" || Method == "PUT"))
        {
            request.Content = new StringContent(Body, System.Text.Encoding.UTF8, "application/json");
        }

        var response = await client.SendAsync(request);

        StatusCode = (int)response.StatusCode;
        StatusText = response.ReasonPhrase ?? "";

        Headers = response.Headers
            .Concat(response.Content.Headers)
            .ToDictionary(h => h.Key, h => string.Join(", ", h.Value));

        ResponseBody = await response.Content.ReadAsStringAsync();

        return Page();
    }
}
