using LightSpeed.Api.Client;
using LightSpeed.Api.Client.V2;
using LightSpeed.Api.Client.V2Beta;
using LightSpeed.Api.Configuration;
using LightSpeed.Api.TestApp.Models;
using Newtonsoft.Json;

namespace LightSpeed.Api.TestApp;

public partial class Form1 : Form
{
    private LightSpeedStore _storeConfig;
    private LightSpeedApiClientV2Beta _apiClient;
    private HttpClient _httpClient;
    public Form1()
    {
        InitializeComponent();

        _storeConfig = JsonConvert.DeserializeObject<LightSpeedStore>(File.ReadAllText("api_config.json"));
        _httpClient = new HttpClient();
        _apiClient = new LightSpeedApiClientV2Beta(new TokenApiBase(_storeConfig.StoreDomain, _storeConfig.ApiToken), _httpClient);
    }
}