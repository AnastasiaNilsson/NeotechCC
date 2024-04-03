using Google.Apis.Sheets.v4;

namespace API;

public class GoogleSheets
{
    private string _spreadsheetId = "1oDK0x-KTo38YGfIPyOIWN8xk5aR3kaT8Ntao7X-Kvt0";

    private SpreadsheetsResource.ValuesResource _googleSheetData;
    public GoogleSheets(GoogleApiClient googleApiClient)
    {
        _googleSheetData = googleApiClient.SheetsService.Spreadsheets.Values;
    }

    private async Task<IList<IList<object>>> GetSheetAsync(string sheetName)
    {
        var request = _googleSheetData.Get(_spreadsheetId, sheetName);
        var response = await request.ExecuteAsync();
        return response.Values;
    }

    public async Task<List<T>> RefreshDataOfType<T>() where T : NeotechEdgeType
    {
        var sheet = await GetSheetAsync(typeof(T).Name + "s");
        var headers = sheet.Take(1).ElementAt(0);
        sheet.RemoveAt(0);

        return sheet.Select(row => {
            var dictionary = new Dictionary<string, string>();
            foreach (var cell in row.Select((value, index) => new {Value = value, Index = index}))
            {
                dictionary.Add((string)headers.ElementAt(cell.Index), (string)cell.Value);
            }
            return (T)T.CreateFromGoogleData(dictionary);
        }).ToList();
    }
}
