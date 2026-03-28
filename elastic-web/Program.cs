using Elastic.Clients.Elasticsearch;
using elastic_web.Components;
using Nest;


var builder = WebApplication.CreateBuilder(args);

const string ProviderIndex = "provider-index";

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Initialize the Elasticsearch client.
builder.Services.AddScoped(sp =>
{
    // Getting access to the configuration service to read the Elasticsearch credentials.
    var configuration = sp.GetRequiredService<IConfiguration>();
    var apiKey = configuration["ElasticsearchApiKey"];

    var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex(ProviderIndex) // Specify the default index
            .BasicAuthentication("elastic", "EYbDKCoC") // Use your Elasticsearch credentials
            .EnableDebugMode(); // Optional: Enable debug mode for detailed logs
    var client = new ElasticClient(settings);

    return client;
});


builder.Services.AddScoped<ElasticsearchClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
