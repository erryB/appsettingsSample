using Microsoft.Extensions.Configuration;
using System;

namespace AppsettingsSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // get the environment variable
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile($"appsettings.{environment}.json", true, true)
                .Build();

            // access the properties of the SectionConfig class
            var section = config.GetSection(nameof(SectionConfig));
            var settings = section.Get<SectionConfig>();

            Console.WriteLine($"URL is: {settings.URL}");
            Console.WriteLine($"Key is: {settings.SecretKey}");

            // access the properties with no associated class
            var prop = config["SectionWithNoClass:Property"];

            Console.WriteLine($"With no class: {prop}");

            // Application code should start here.
        }
    }
}
