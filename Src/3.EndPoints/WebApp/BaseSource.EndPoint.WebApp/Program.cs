var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

// Map Razor Pages (with /App prefix thanks to convention)
app.MapRazorPages()
   .WithStaticAssets();

app.MapGet("/", () => Results.Redirect("/App"));

app.Run();
