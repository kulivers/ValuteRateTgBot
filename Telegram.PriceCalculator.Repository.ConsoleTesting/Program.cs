// See https://aka.ms/new-console-template for more information

using Telegram.PriceCalculator.Repository;
using Telegram.PriceCalculator.Services;
using Telegram.PriceCalculator.Shared;

internal class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var cs1 = "Data Source=@localhost; User id=ekul; Password=ekul; Database = sys";
            cs1 = "Server=127.0.0.1;Database=sys;Uid=ekul;Pwd=ekul;";
            cs1 = "Server=127.0.0.1;Uid=ekul;Pwd=ekul;Database=valutebot";

            var factory = new RepositoryContextFactory();
            RepositoryContext ctx = factory.CreateDbContext(args);
            var repositoryManager = new RepositoryManager(ctx);
            var testRepo = new UserFormulaRepository(ctx);


            Console.WriteLine("YES!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        catch (Exception e)
        {
            Console.WriteLine("No(");
            Console.WriteLine(e.Message);
            if (e.InnerException != null)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}
