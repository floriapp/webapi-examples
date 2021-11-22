using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FloriApp.WebApi;
using FloriApp.WebApi.Models;

namespace FloriApp.ExampleApp
{
  class Program
  {
    static async Task Main(string[] args)
    {
      Console.WriteLine("Hello FloriApp!");
      Console.WriteLine();
      var client = new FloriAppApiClient(new FloriAppApiOptions()
      {
        ApiBaseUrl = "https://api.testflori.app/",
        ApiKey = "xxx",
      });

      var allowedCompanies = await client.CompaniesGet();
      Console.WriteLine("The ApiKey has access to the following companies:");
      foreach (var allowedCompany in allowedCompanies)
        Console.WriteLine($"- {allowedCompany.Name} ({allowedCompany.Id})");
      Console.WriteLine();

      var company = allowedCompanies.First();
      Console.WriteLine($"Using {company.Name} to create example stock");
      var companyClient = client.AsCompany(company);

      var stock = new Stock()
      {
        Vbn = "124039", // Alstroemeria Sansa
        Date = DateTime.Today,
        //Description = // override article name
        //AddText = "super!", // additional text
        Manufacturer = "8713783295847", // kweker
        //Supplier = "",
        Amount = 2,
        ApE = 50,
        Packing = "998", // emballage
        Price = 0.29m, // piece price
        Foto = "https://raw.githubusercontent.com/floriapp/webapi-examples/master/assets/alstroemeria-sansa.jpg",
      };
      stock.Features.Add("S20", "075"); // steellengte
      stock.Features.Add("S21", "090"); // gewicht
      stock.Features.Add("S05", "023"); // rijpheid
      stock.Features.Add("L11", "010"); // stelen per bos
      stock.Features.Add("S22", "005"); // aantal bloemknoppen
      stock.Features.Add("S65", "008"); // voorbehandeling
      stock.Features.Add("S97", "004"); // mps-a
      stock.Features.Add("S62", "NL");  // country of origin
      stock.Features.Add("S98", "A1");  // quality
      stock.Features.Add("P02", "001"); // belicht
      stock.Features.Add("V22", "005"); // exclusief


      // send stock to server, and update local stock object
      stock = await companyClient.StockPost(stock);

      Console.WriteLine($"Created new stock with the following information:");
      Console.WriteLine($"- Id: {stock.Id}");
      Console.WriteLine($"- Date: {stock.Date}");
      Console.WriteLine($"- Article: {stock.Description} / {stock.Article.Name}");
      Console.WriteLine($"- Article trade name: {stock.Article.TradeName}");
      Console.WriteLine($"- Vbn number: {stock.Vbn}");
      Console.WriteLine($"- Vbn group: {stock.VbnGroup}");
      Console.WriteLine($"- Additional text: {stock.AddText}");
      Console.WriteLine($"- Manufacturer GLN: {stock.Manufacturer}");
      Console.WriteLine($"- Supplier: {stock.Supplier}");
      Console.WriteLine($"- Available pieces: {stock.PiecesAvailable}");
      Console.WriteLine($"- Available: {stock.PiecesAvailable/stock.ApE} x {stock.ApE}");
      Console.WriteLine($"- Price: {stock.Price}");
      Console.WriteLine($"- Packing: {stock.Packing}");
      foreach (var feature in stock.Features)
        Console.WriteLine($"- {feature.Key}: {feature.Value}");
      Console.WriteLine();


    }
  }
}
