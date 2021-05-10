# Appsettings Sample

Small sample to use the configuration from `appsettings.development.json` file in a .NET 5 console app.

In a typical scenario, we want to have a `appsettings.json` file with all the placeholders for the secrets we need to run our application. At the same time, we want to have another file, saved only locally and called `appsettings.development.json` with the real secrets, which will not be committed in the repo.

The goal of this sample is to show how to configure the application to read the real secret values from `appsettings.development.json`.

*Please notice that in this sample the `appsettings.development.json` file is committed in the repo just to show how to make it work, in a real scenario this file should be saved locally and added to the `.gitignore`.*

## Run the application

You just need to clone the repo and press F5 in Visual Studio 2019 to run the console app. This is the output you should get:

```bash
URL is: DEV URL
Key is: DEV Key
With no class: DEV property
```

## How to add the same configuration in your own project

1. Add the following Nuget Packages to your project: `Microsoft.Extensions.Configuration.Json` and `Microsoft.Extensions.Configuration.Binder`
2. Create the following environment variable: `DOTNET_ENVIRONMENT` as `development`. If you are using Visual Studio 2019, then you can do that by right clicking on the project and adding it in the `Debug` section in the `Environment variables` list. However, as you can see in the repo, you can also just create/modify the `Properties/launchSettings.json` file.
3. Create the `appsettings.json` file with the structure you need and the placeholders to explain what values are necessary to run your application. This is the example we used here:

    ```json
    {
      "SectionConfig": {
        "URL": "placeholder",
        "SecretKey": "placeholder"
      },
      "SectionWithNoClass": {
        "Property": "placeholder"
      }
    }
    ```

4. Copy the same structure in a new file called `appsettings.development.json` and replace the placeholders with the real secrets you need.
5. Add a new line in the `.gitignore` file, so that the development configuration will not be committed:

    ```bash
    appsettings.development.json
    ```

6. Copy the `appsettings.development.json` in the output folder. To do so, you need to add these few lines in the project file:

    ```xml
    <ItemGroup>
      <None Update="appsettings.development.json">
        <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
      </None>
    </ItemGroup>
    ```

7. You can now access the values stored in `appsettings.development.json` with the following notation (the code is in `Program.cs`):

    ```c#
    // access the properties with no associated class
    var prop = config["SectionWithNoClass:Property"];
    Console.WriteLine($"With no class: {prop}");
    ```

8. *(optional step)* You can also create a class with the same structure and names you have in the `appsettings.development.json`. In this case we created the `SectionConfig.cs` with the same 2 properties `URL` and `SecretKey`.

    ```c#
    namespace AppsettingsSample
    {
        public class SectionConfig
        {
            public string URL { get; set; }
            public string SecretKey { get; set; }
        }
    }
    ```

    Having this class changes the way you can access the values stored in your `appsettings.development.json` from the code (in `Program.cs`):

    ```c#
    // access the properties of the SectionConfig class
    var section = config.GetSection(nameof(SectionConfig));
    var settings = section.Get<SectionConfig>();

    Console.WriteLine($"URL is: {settings.URL}");
    Console.WriteLine($"Key is: {settings.SecretKey}");
    ```
