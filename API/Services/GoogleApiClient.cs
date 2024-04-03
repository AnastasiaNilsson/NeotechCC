using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace API;

public class GoogleApiClient
{
    public SheetsService SheetsService { get; set; }

    public GoogleApiClient()
    {
        SheetsService = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = ReadCredentials(),
            ApplicationName = "NeotechAPI"
        });
    }

    private GoogleCredential ReadCredentials()
    {
        using (var stream = new FileStream("../google-credentials.json", FileMode.Open, FileAccess.Read))
        {
            return GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.Spreadsheets);
        }
    }
}
