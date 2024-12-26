using Priskartan.Components;
using Priskartan.Services.Eon;
using Priskartan.Services.SvenskMaeklarstatistik;
using Priskartan.Services.DataCollector;
using Priskartan.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddSingleton<IEonService, EonService>()
    .AddSingleton<ISvenskMaeklarstatistikService, SvenskMaeklarstatistikService>()
    .AddSingleton<IDataCollector, DataCollector>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapGet("/admin/run-data-collection", async context => {
    var authMiddleware = new Auth(async innerContext => {
        var dataCollector = app.Services.GetService<IDataCollector>();

        if (dataCollector is null)
        {
            innerContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await innerContext.Response.WriteAsync("Something went wrong while collecting the required data.");
            return;
        }

        var res = await dataCollector.CollectPricingData();

        innerContext.Response.StatusCode = StatusCodes.Status200OK;
        await innerContext.Response.WriteAsync("OK");
        return;
    }, app.Services.GetRequiredService<IConfiguration>());

    await authMiddleware.InvokeAsync(context);
});

app.Run();
