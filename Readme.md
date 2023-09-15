# BindOpen.Pulp.Commands

![BindOpen logo](https://storage.bindopen.org/img/logos/logo_bindopen.png)

![Github release version](https://img.shields.io/nuget/v/BindOpen.System.Abstractions.svg?style=plastic)


BindOpen is a framework that allows to build widely-extended applications. It enables you to enhance your .NET projects with custom script functions, connectors, entities, and tasks.

## About

BindOpen.System is the kernel of the BindOpen framework. It is composed of the following modules:

* __System.Data__ offers a comprehensive data model based on metadata.
* __System.Scoping__ offers an effective mechanism for defining and managing your extensions.
* [System.Hosting](https://github.com/bindopen/BindOpen.Hosting) allows you to integrate a BindOpen agent within the .NET service builder.
* [System.Logging](https://github.com/bindopen/BindOpen.Logging) provides a straightforward and multi-dimensional logging system.
* __System.IO__ provides packages to serialize and deserialize BindOpen.System objects.

This repository contains the System.Data, System.Scoping and System.IO modules. The two other ones are separate repositories.

A [full list of all the repos](https://github.com/bindopen?tab=repositories) is available as well.


## Install

To get started, install the BindOpen.System module you want to use.

Note: We recommend that later on, you install only the package you need.

### From Visual Studio

| Module | Instruction |
|--------|-----|
| [BindOpen.System.Data](https://www.nuget.org/packages/BindOpen.System.Data) | ```PM> Install-Package BindOpen.System.Data``` |
| [BindOpen.System.Scoping](https://www.nuget.org/packages/BindOpen.System.Scoping) | ```PM> Install-Package BindOpen.System.Scoping``` |
| [BindOpen.System.IO](https://www.nuget.org/packages/BindOpen.System.IO) | ```PM> Install-Package BindOpen.System.IO``` |

### From .NET CLI

| Module | Instruction |
|--------|-----|
| [BindOpen.System.Data](https://www.nuget.org/packages/BindOpen.System.Data) | ```> dotnet add package BindOpen.System.Data``` |
| [BindOpen.System.Scoping](https://www.nuget.org/packages/BindOpen.System.Scoping) | ```> dotnet add package BindOpen.System.Scoping``` |
| [BindOpen.System.IO](https://www.nuget.org/packages/BindOpen.System.IO) | ```> dotnet add package BindOpen.System.IO``` |

## Get started

### System.Data

#### Metadata

```csharp
var meta = BdoData.NewMeta("host", DataValueTypes.Text, "my-test-host");
```

#### Configuration

```csharp
var config = BdoData.NewConfig(
        "test-config",
        BdoData.NewScalar("comment", DataValueTypes.Text, "Sunny day"),
        BdoData.NewScalar("temperature", DataValueTypes.Integer, 25, 26, 26),
        BdoData.NewScalar("date", DataValueTypes.Date, DateTime.Now),
        BdoData.NewNode(
            "subscriber"
            BdoData.NewScalar("name", DataValueTypes.Text, "Ernest E."),
            BdoData.NewScalar("code", DataValueTypes.Integer, 1560))
    )
    .WithTitle("Example of configuration")
    .WithDescription(("en", "This is an example of description"))
```

### System.Scoping

```csharp
var scope = BdoScoping.NewScope()
    .LoadExtensions(q => q.AddAllAssemblies());
```

#### Script

```csharp

[BdoFunction(
    Name = "testFunction",
    Description = "Returns true if second string parameter is the first one ending with underscore")]
public static object Fun_Func2a(
    this string st1,
    string st2)
{
    return st1 == st2 + "_";
}

...

var exp = "$testFunction('MYTABLE', $text('MYTABLE_'))";
var result = scope.Interpreter.Evaluate<bool?>(exp);
// result is True
```

#### Tasks

```csharp

[BdoTask("taskFake")]
public class TaskFake : BdoTask
{
    [BdoProperty(Name = "boolValue")]
    public bool BoolValue { get; set; }

    [BdoOutput(Name = "stringValue")]
    public string StringValue { get; set; }

    [BdoInput(Name = "enumValue")]
    public ActionPriorities EnumValue { get; set; }

    ...

    public override Task<bool> ExecuteAsync(
        CancellationToken token,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        RuntimeModes runtimeMode = RuntimeModes.Normal,
        IBdoLog log = null)
    {
        ...

        Debug.WriteLine("Task completed");

        return Task.FromResult(true);
    }
}

...

var meta = BdoData.NewObject()
    .WithDataType(BdoExtensionKinds.Task, "bindopen.system.tests$taskFake")
    .WithProperties(("boolValue", false))
    .WithInputs(BdoData.NewScalar("enumValue", ActionPriorities.Low))
    .WithOutputs(("stringValue", "test-out"));
                    
var task = scope.CreateTask<TaskFake>(meta);
var cancelToken = new CancellationTokenSource();
task.Execute(cancelToken.Token, scope);
```

### System.IO

#### Serialization

```csharp
var metaSet = BdoData.NewSet("test-io").With(("host", "host-test"), ("address", "0.0.0.0"));
metaSet.ToDto().SaveXml("output.xml");
```

#### Deserialization

```csharp
var metaSet = JsonHelper.LoadJson<MetaSetDto>("output.xml").ToPoco();
```


## License

This project is licensed under the terms of the MIT license. [See LICENSE](https://github.com/bindopen/BindOpen/blob/master/LICENSE).

## Packages

This repository contains the code of the following Nuget packages:

| Package | Provision |
|----------|-----|
| [BindOpen.System.Abstractions](https://www.nuget.org/packages/BindOpen.System.Abstractions) | Interfaces and enumerations |
| [BindOpen.System.Data](https://www.nuget.org/packages/BindOpen.System.Data) | Core data model |
| [BindOpen.System.Scoping](https://www.nuget.org/packages/BindOpen.System.Scoping) | Extension manager |
| [BindOpen.System.Scoping.Extensions](https://www.nuget.org/packages/BindOpen.System.Scoping.Extensions) | Classes of extensions |
| [BindOpen.System.Scoping.Script](https://www.nuget.org/packages/BindOpen.System.Scoping.Script) | Script interpreter |
| [BindOpen.System.IO](https://www.nuget.org/packages/BindOpen.System.IO) | Serialization / Deserialization |
| [BindOpen.System.IO.Dtos](https://www.nuget.org/packages/BindOpen.System.IO.Dtos) | Data transfer classes |

The atomicity of these packages allows you install only what you need respecting your solution's architecture.

All of our NuGet packages are available from [our NuGet.org profile page](https://www.nuget.org/profiles/bindopen).


## Other repos and Projects

[BindOpen.System.Hosting](https://github.com/bindopen/BindOpen.System.Hosting) enables you to integrate a BindOpen agent within the .NET service builder.

[BindOpen.System.Logging](https://github.com/bindopen/BindOpen.System.Logging) provides a simple and multidimensional logging system, perfect to monitor nested task executions.

[BindOpen.Pulp](https://github.com/bindopen/BindOpen.Pulp) is a collection of projects based on BindOpen.System.


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


