// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Telegram.PriceCalculator.Repository;
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
            var ctx = factory.CreateDbContext(args);
            var testRepo = new UserFormulaRepository(ctx);
            var r = new Random(int.MaxValue);
            r.Next();
            await testRepo.CreateAsync(new UserFormula(1321321, r.Next(), "1+1"));
            await testRepo.CreateAsync(new UserFormula(1832, r.Next(), "1+1", new List<Variable>()
            {
                new Variable() { Id = 2882313, Name = "nam", Value = 188231231 },
            }));
            await testRepo.CreateAsync(new UserFormula(132, r.Next(), "1+1", new List<Variable>()
            {
                new ValuteCalculatedVariable() { Id = 23238231, Name = "nam", Value = 123188231, VchCode = "3231" }
            }));

            await testRepo.CreateAsync(new UserFormula(1382, r.Next(), "1+1", new List<Variable>()
            {
                new Variable() { Id = 822313, Name = "nam", Value = 1231281 },
                new ValuteCalculatedVariable() { Id = 83231, Name = "nam", Value = 12331231, VchCode = "3231" }
            }));
            await ctx.SaveChangesAsync();
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
