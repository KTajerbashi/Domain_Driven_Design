using BaseSource.Core.Application.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaseSource.EndPoint.WebApp.Pages.App.BasePage;

public abstract class BasePageModel : PageModel
{
    protected readonly ProviderFactory Factory;
    protected BasePageModel(ProviderFactory factory)
    {
        Factory = factory;
    }
}

[Authorize]
public abstract class AuthPageModel : BasePageModel
{
    protected AuthPageModel(ProviderFactory factory) : base(factory)
    {
    }
}