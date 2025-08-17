# BindOpen.Commands

![BindOpen logo](https://storage.bindopen.org/img/logos/logo_bindopen.png)

![Github release version](https://img.shields.io/nuget/v/BindOpen.Kernel.Abstractions.svg?style=plastic)


BindOpen is a framework that allows to build widely-extended applications. It enables you to enhance your .NET projects with custom script functions, connectors, entities, and tasks.

## About

BindOpen.Commands is a straightforward and comprehensive API for manipulating command-line arguments and handling related tasks.


## Install

To get started, install the BindOpen.Commands module you want to use.

Note: We recommend that later on, you install only the package you need.

### From Visual Studio

| Module | Instruction |
|--------|-----|
| [BindOpen.Commands](https://www.nuget.org/packages/BindOpen.Commands) | ```PM> Install-Package BindOpen.Commands``` |
| [BindOpen.Commands.Abstrations](https://www.nuget.org/packages/BindOpen.Commands.Abstrations) | ```PM> Install-Package BindOpen.Commands.Abstrations``` |

### From .NET CLI

| Module | Instruction |
|--------|-----|
| [BindOpen.Commands](https://www.nuget.org/packages/BindOpen.Commands) | ```> dotnet add package BindOpen.Commands``` |
| [BindOpen.Commands.Abstrations](https://www.nuget.org/packages/BindOpen.Commands.Abstrations) | ```> dotnet add package BindOpen.Commands.Abstrations``` |

## Get started

### Define command options

Options are defined using BindOpen metadata, allowing for rich descriptions, default values, types, and validation rules.

```csharp
 var options = BdoCommands.NewOption(
        "sample",
        BdoCommands.NewOption("version")
            .WithAliases("version", "--version", "-v")
            .WithLabel(LabelFormats.NameSpaceValue)
            .WithDataType(DataValueTypes.Text)
            .AsRequired()
            .WithDescription("Display the version of the application.")
            .Execute(() => ShowVersion())
        ,
        BdoCommands.NewOption("path")
            .WithLabel(LabelFormats.OnlyValue)
            .WithDataType(DataValueTypes.Text)
            .AsRequired()
            .WithTitle("_path")
            .WithDescription("The path.")
    )
    .WithDescription("Sample shows you the way to simply specify the options of your application.");
```

### Parse arguments

```csharp
var scope = BdoScoping.NewScope();
var parameters = scope.ParseArguments(args, options);
```

### Check parameters against option definitions

```csharp
var log = BdoLogging.NewLog();

var scope = BdoScoping.NewScope();
var parameters = scope.ParseArguments(args, options, log: log);

// Parsing outputs are stored in the log object
```

### Invoke tasks dynamically

```csharp
var parameters = SystemData.Scope.ParseArguments(args, options);
await parameters.InvokeAsync(q => q.GetData<string>("run") == "A", async () => { await TaskA(); });
```

### Automatically generate help

```csharp
var scope = BdoScoping.NewScope();
var help = scope.GetHelpText(options);

Trace.WriteLine(help);
```

## License

This project is licensed under the terms of the MIT license. [See LICENSE](https://github.com/bindopen/BindOpen/blob/master/LICENSE).

## Packages

This repository contains the code of the following Nuget packages:

| Package | Provision |
|----------|-----|
| [BindOpen.Commands](https://www.nuget.org/packages/BindOpen.Commands) | Core package |
| [BindOpen.Commands.Abstrations](https://www.nuget.org/packages/BindOpen.Commands.Abstrations) | Interfaces and enumerations |

The atomicity of these packages allows you install only what you need respecting your solution's architecture.

All of our NuGet packages are available from [our NuGet.org profile page](https://www.nuget.org/profiles/bindopen).


## Other repos and Projects

[BindOpen.Hosting](https://github.com/bindopen/BindOpen.Hosting) enables you to integrate a BindOpen agent within the .NET service builder.

[BindOpen.Logging](https://github.com/bindopen/BindOpen.Logging) provides a simple and multidimensional logging system, perfect to monitor nested task executions.

A [full list of all the repos](https://github.com/bindopen?tab=repositories) is available as well.


## Documentation and Further Learning

### [BindOpen Docs](https://docs.bindopen.org/)

The BindOpen Docs are the ideal place to start if you are new to BindOpen. They are categorized in 3 broader topics:

* [Articles](https://docs.bindopen.org/articles) to learn how to use BindOpen;
* [Notes](https://docs.bindopen.org/notes) to follow our releases;
* [Api](https://docs.bindopen.org/api) to have an overview of BindOpen APIs.

### [BindOpen Blog](https://www.bindopen.org/blog)

The BindOpen Blog is where we announce new features, write engineering blog posts, demonstrate proof-of-concepts and features under development.


## Feedback

If you're having trouble with BindOpen, file a bug on the [BindOpen Issue Tracker](https://github.com/bindopen/BindOpen/issues). 

## Donation

You are welcome to support this project. All donations are optional but are greatly appreciated.

[![Please donate](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate/?hosted_button_id=PHG3WSUFYSMH4)


