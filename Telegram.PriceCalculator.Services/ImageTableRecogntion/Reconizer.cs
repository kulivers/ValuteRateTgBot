using DocumentsManagement;
using YandexCloudOCR;
using YandexCloudOCR.Services;

namespace Telegram.PriceCalculator.Services.ImageTableRecogntion;

public static class Reconizer
{
    public static string Convert(string inputFile)
    {
        var imageToTableConverter = new ImageToTableConverter();
        var base64 = imageToTableConverter.ConvertImageToBase64(inputFile);
        var response = OcrClient.SendRequest(base64);
        var dictTable = imageToTableConverter.ConvertToDict(response);
        var excelManager = new ExcelManager();
        var excelFile = excelManager.CreateStreamFromTable(dictTable);
        return excelFile;
    }
}
