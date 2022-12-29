# Repository Content
Library to abstract file persistence from File System or cloud provider

[![Build Status](https://garaproject.visualstudio.com/UmbrellaFramework/_apis/build/status/Umbrella.Infrastructure.FileStorage?branchName=main)](https://garaproject.visualstudio.com/UmbrellaFramework/_build/latest?definitionId=81&branchName=main)


[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.FileStorage&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.FileStorage)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.FileStorage&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.FileStorage)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.FileStorage&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.FileStorage)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.FileStorage&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.FileStorage)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.FileStorage&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.FileStorage)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.FileStorage&metric=bugs)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.FileStorage)


To install it, use proper command:

```bat
dotnet add package Infrastructure.FileStorage
dotnet add package Infrastructure.FileStorage.GoogleCloudStorage
```

Infrastructure.FileStorage:
[![Nuget](https://img.shields.io/nuget/v/Umbrella.Infrastructure.FileStorage.svg?style=plastic)](https://www.nuget.org/packages/Umbrella.Infrastructure.FileStorage/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Umbrella.Infrastructure.FileStorage.svg)](https://www.nuget.org/packages/Umbrella.Infrastructure.FileStorage/)

Infrastructure.FileStorage.GoogleCloudStorage:
[![Nuget](https://img.shields.io/nuget/v/Umbrella.Infrastructure.FileStorage.GoogleCloudStorage.svg?style=plastic)](https://www.nuget.org/packages/Umbrella.Infrastructure.FileStorage.GoogleCloudStorage/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Umbrella.Infrastructure.FileStorage.GoogleCloudStorage.svg)](https://www.nuget.org/packages/Umbrella.Infrastructure.FileStorage.GoogleCloudStorage/)


For more details about download, see [NuGet Web Site](https://www.nuget.org/packages/Umbrella.Infrastructure.FileStorage/)


## Usage

for the usage of the library, please refer to extension methods.

<b>Standard usage</b>

```c#
services.UseFileStorageFromFileSystem(@"C:\Temp\MyFolder");
```

or if you need to store data on Google Cloud Storage:

```c#
services.UseFileStorageFromGoogle("my-bucket-name");
```

than, once you inject this depedency to your component, use such snippets:

```c#
readonly IFileStorage _Storage;

public MyComponent(IFileStorage storage)
{
    this._Storage = storage ?? throw new ArgumentNullException(nameof(storage));
    if(!this._Storage.IsScanPerformed)
        this._Storage.ScanStorageTree();
}

```

it's important ot scan the tree to get the content information ready for accessing it.