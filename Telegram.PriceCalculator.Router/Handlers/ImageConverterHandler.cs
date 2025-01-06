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
            Directory.CreateDirectory(_tempFolder);//todo egor installeerr
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
        var photo = update.Message!.Photo.Last();
        var fileId = photo.FileId;
        var file = await botClient.GetFileAsync(fileId, cancellationToken: token);
        var fileName = Path.GetFileName(file.FilePath);
        var buffer = new byte[file.FileSize.Value];
        await using var ms = new MemoryStream(buffer);
        await botClient.DownloadFileAsync(file.FilePath, ms, token);

        await Test(botClient, update);
    }

    private async Task Test(ITelegramBotClient botClient, Update e)
    {
        var fileId = e.Message.Photo[^1].FileId; // Get the highest resolution photo
        var file = await botClient.GetFileAsync(fileId);

        var fileName = Path.GetFileName(file.FilePath);
        var filePath = Path.Combine(_tempFolder, fileName);//todo egor mb it could be in default tg folder
        await using var fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
        await botClient.DownloadFileAsync(file.FilePath, fs);
        ;
    }
}
