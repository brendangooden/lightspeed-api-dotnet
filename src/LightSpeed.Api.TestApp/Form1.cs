using LightSpeed.Api.Client.V2;
using LightSpeed.Api.Client.V2Beta;
using LightSpeed.Api.Configuration;
using LightSpeed.Api.TestApp.Models;
using Newtonsoft.Json;

namespace LightSpeed.Api.TestApp;

public partial class Form1 : Form
{
    private LightSpeedStore _storeConfig;
    private LightSpeedApiClientV2Beta _apiClientBeta;
    private LightSpeedApiClientV2 _apiClient;
    private HttpClient _httpClient;

    public Form1()
    {
        InitializeComponent();

        _storeConfig = JsonConvert.DeserializeObject<LightSpeedStore>(File.ReadAllText("api_config.json"));
        _httpClient = new HttpClient();
        _apiClientBeta = new LightSpeedApiClientV2Beta(new TokenApiConfiguration(_storeConfig.StoreDomain, _storeConfig.ApiToken), _httpClient);
        _apiClient = new LightSpeedApiClientV2(new TokenApiConfiguration(_storeConfig.StoreDomain, _storeConfig.ApiToken), _httpClient);
    }
    private async void btnTest_Click(object sender, EventArgs e)
    {
    }
}