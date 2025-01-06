using Telegram.Bot;
using Telegram.Bot.Types;
using File = Telegram.Bot.Types.File;

namespace Telegram.PriceCalculator.Router.Menu;

public class ImageConverterHandler : ActionHandler
{
    private readonly string _tempFolder = Path.Combine(Path.GetTempPath(), "tgBot");

    public ImageConverterHandler()
    {
        if (!Directory.Exists(_tempFolder))
        {
            Directory.CreateDirectory(_tempFolder); //todo egor installeerr
        }
    }

    public override string ActionName { get; } = Guid.NewGuid().ToString(); //todo egor remade all to shouldHandle

    public override Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public override bool ShouldHandle(Update update)
    {
        var photo = update?.Message?.Photo;
        return photo != null && photo.Length != 0;
    }

    public override async Task Handle(ITelegramBotClient botClient, UserContext userContext, Update update, CancellationToken token)
    {
        var filePath = await SaveFile(botClient, update, token);
        //todo send to gpt
        var gptResult = "C:\\Users\\kulivers\\Downloads\\Telegram Desktop\\qeqeqeq.xlsx";
        await using (var stream = new FileStream(gptResult, FileMode.Open))
        {
            var fileStream = InputFile.FromStream(stream, "qeqeqeq.xlsx");
            var inputFile = await botClient.SendDocumentAsync(update.Message.Chat.Id, fileStream, cancellationToken: token);
        }
    }

    private async Task<string> SaveFile(ITelegramBotClient botClient, Update update, CancellationToken token)//todo egor mb it could be in default tg folder
    {
        var fileId = update.Message.Photo[^1].FileId;
        var file = await botClient.GetFileAsync(fileId, cancellationToken: token);
        var filePath = await PreparePath(file);
        await using var fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
        await botClient.DownloadFileAsync(file.FilePath, fs, token);
        return filePath;
    }

    private async Task<string> PreparePath(File file)
    {
        var fileName = Path.GetFileName(file.FilePath);
        var filePath = Path.Combine(_tempFolder, fileName);
        return PreparePath(filePath);
    }

    private string PreparePath(string filePath)
    {
        while (true)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return filePath;
            }

            var extension = Path.GetExtension(filePath);
            var randomFileName = Path.GetRandomFileName();
            filePath = Path.Combine(_tempFolder, string.Join(".", randomFileName, extension));
        }
    }
}
