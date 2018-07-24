# Best Bank Portugal Connect [![NuGet](https://img.shields.io/nuget/v/BestBankPortugalConnect.svg)](https://www.nuget.org/packages/BestBankPortugalConnect/)
The ***Best Bank Portugal Connect*** is a C# SDK (.NET/.NET Core) that helps (*a lot!*) to connect to the portuguese bank [Banco Best](https://www.bancobest.pt/)'s [Open Banking API](https://openbankproject.com/). The Banco Best is the first bank to implement the [PSD2](https://www.evry.com/en/news/articles/psd2-the-directive-that-will-change-banking-as-we-know-it/) standard in Portugal.

With this SDK is possible to make **Android** and **iOS** applications using [Xamarin](https://visualstudio.microsoft.com/xamarin/). Not just mobile applications, since that .NET Framework and .NET Core works on a wide places, it's possible to use this SDKs in a lot of places.

This library helps to:

* Authorization Flow (OAuth 2)
* Get Balances
* Get Moviments
* Make transferences
* Make payments via portuguese standarts.

## Usage

The main entry point for the API is to create a valid [BestBankConnector](https://github.com/Liques/BestBankPortugalConnect/wiki/BestBankConnector) object:

```csharp

// Set your application details
var app = new Application(appConsumer, appSecret, Environment.Sandbox); 
// Set the user Access Token (In this case this user already made the whole OAUth 2 previously)
var user = new User(accessToken); 

// Setup a BestBankConnector object
var api = new BestBankConnector(user, app); 
// Get for example the account balance
var balance = api.Balance(); 
```

This SDK also simplify the painful OAuth 2.0 authorization with [AuthorizationFlow](https://github.com/Liques/BestBankPortugalConnect/wiki/AuthorizationFlow) object:

```csharp

// Step 1: Gets the URL Login where the Banco Best costumer will authorize the app to access his accounts
var url = AuthorizationFlow.GetBankLoginUrl(app,@"http:\\www.httpbin.org\get");

// .... Wait the costumer back to the application ... //

// Step 2: Gets the user Access Token based on a temporary code returned after the costumer authorization
var accessToken = AuthorizationFlow.GetUserAccessToken(app, "5f7c768d060a43568779df1b86ffb407");

```

## Documentation

Yes! There is documentation. It's hosted [here on the repository wiki](https://github.com/Liques/BestBankPortugalConnect/wiki). Also, if you take a look at the repository, there is a [.NET Core console app example](https://github.com/Liques/BestBankPortugalConnect/tree/master/ExampleConsoleApp) where you could learn how this SDK works debugging step by step.

## Download

The Best Bank Portugal Connect is available as a NuGet package: [![NuGet](https://img.shields.io/nuget/v/BestBankPortugalConnect.svg)](https://www.nuget.org/packages/BestBankPortugalConnect/)

> ``` PM> Install-Package BestBankPortugalConnect ```
