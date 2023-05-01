# SpaceDailies

## Technologies

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [.NET MAUI](https://docs.microsoft.com/en-us/dotnet/maui/what-is-maui)
- [NASA APOD API](https://api.nasa.gov/#apod)

## Installation and Setup

To get started with the SpaceDailies application, follow these steps:

1. Ensure that you have the latest version of [Visual Studio](https://visualstudio.microsoft.com/) and the [.NET MAUI workload](https://docs.microsoft.com/en-us/dotnet/maui/get-started/installation) installed.

2. Clone this repository:

```bash
git clone https://github.com/Stempnio/space-dailies.git
```

3. Before running the application, you need to provide your own NASA API key. Create a Secrets.cs file in the main project directory (next to the .csproj file) with the following content:

```cs
using System;
namespace SpaceDailies;

public class Secrets
{
	public const string apiKey = "your_api_key";
}
```

Replace your_api_key with your actual NASA API key. You can obtain a free API key by signing up at NASA API Portal.




