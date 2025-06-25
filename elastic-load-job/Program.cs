using Bogus;
using Nest;

class Program
{
    public static string ProviderIndex = "provider-index";

    static void Main(string[] args)
    {
        // Create a connection to Elasticsearch
        var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex(ProviderIndex) // Specify the default index
            .BasicAuthentication("elastic", "EYbDKCoC") // Use your Elasticsearch credentials
            .EnableDebugMode(); // Optional: Enable debug mode for detailed logs
        var client = new ElasticClient(settings);

        var documents = GetPersons<Patient>(1000);

        var bulkResponse = client.Bulk(d => d
            .IndexMany(documents)
        );

        if (!bulkResponse.IsValid)
        {
            Console.WriteLine("Bulk insert failed:");
            Console.WriteLine(bulkResponse.ServerError?.Error?.Reason);
            Console.WriteLine(bulkResponse.ServerError?.Error?.StackTrace);
            return;
        }
        
        Console.WriteLine($"Inserted {bulkResponse.Items?.Count??0} items");
        Console.WriteLine($"Total number of items: {bulkResponse.Items?.Count??0}");
   }
 
    public static List<T> GetPersons<T>(int count) where T : Person, new()
    {

        var personFaker = new Faker<T>()
            .RuleFor(u => u.Id, f => f.IndexFaker + + DateTime.Now.Second)
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Country, f => f.Address.Country())
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumberFormat())
            .RuleFor(u => u.PersonType, f => typeof(T).Name)
            .RuleFor(u => u.State, f => StateArray.Abbreviations()[new Random().Next(0, StateArray.Abbreviations().Length)]);
    
        if (typeof(T) == typeof(Provider))
        {   
            personFaker.RuleFor(u => (u as Provider).Patients, f => GetPatients(250));
        }

        var persons = personFaker.Generate(count);

        return persons;

    }

    public static List<Patient> GetPatients(int count)
    {
        var patientFaker = new Faker<Patient>()
            .RuleFor(p => p.Id, f => f.IndexFaker + DateTime.Now.Second)
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Country, f => f.Address.Country())
            .RuleFor(p => p.Phone, f => f.Phone.PhoneNumberFormat())
            .RuleFor(p => p.State, f => StateArray.Abbreviations()[new Random().Next(0, StateArray.Abbreviations().Length)]);

        return patientFaker.Generate(count);
    }
} 