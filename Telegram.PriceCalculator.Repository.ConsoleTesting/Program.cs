// See https://aka.ms/new-console-template for more information

using Telegram.PriceCalculator.Repository;

internal class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var cs1 = "Data Source=@localhost; User id=ekul; Password=ekul; Database = sys";
            cs1 = "Server=127.0.0.1;Database=sys;Uid=ekul;Pwd=ekul;";
            cs1 = "Server=127.0.0.1;Uid=ekul;Pwd=ekul;";
            var testRepo = new TestRepo(cs1);
            testRepo.TestConnection().Wait();
            var result = testRepo.ExecuteStringAsync("SHOW DATABASES");
            var enumerator = result.GetAsyncEnumerator();
            while (await enumerator.MoveNextAsync())
            {
                var current = enumerator.Current;
            }
            Console.WriteLine(result);
            Console.WriteLine("YES!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        catch (Exception e)
        {
            Console.WriteLine("No(");
            Console.WriteLine(e.Message);
            if (e.InnerException!=null)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}
