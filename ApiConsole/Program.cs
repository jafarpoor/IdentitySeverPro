// See https://aka.ms/new-console-template for more information

using IdentityModel.Client;

HttpClient InfoIDP = new HttpClient();
var DisCoveryInfo = InfoIDP.GetDiscoveryDocumentAsync("https://localhost:7151").Result;
if (DisCoveryInfo.IsError)
{
    Console.WriteLine(DisCoveryInfo.Error);
    return;
}

var accessToken = InfoIDP.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = DisCoveryInfo.TokenEndpoint,
    ClientId = "ApiWeatherId",
    ClientSecret = "123456",
    Scope = "ApiWeather"
}).Result;

if (accessToken.IsError)
{
    Console.WriteLine(DisCoveryInfo.Error);
    return;
}

Console.WriteLine(accessToken.Json);


HttpClient apiClient = new HttpClient();
apiClient.SetBearerToken(accessToken.AccessToken);
var result = apiClient.GetAsync("https://localhost:7188/WeatherForecast").Result;
var content = result.Content.ReadAsStringAsync().Result;
Console.WriteLine(content);


Console.WriteLine("Hello World!");
Console.ReadKey();
