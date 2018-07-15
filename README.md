# Best Bank Portugal Connect [![NuGet](https://img.shields.io/nuget/v/BestBankPortugalConnect.svg)](https://www.nuget.org/packages/BestBankPortugalConnect/)
The ***Best Bank Portugal Connect*** is a C# library the helps to connect to the [Best Bank](https://www.bancobest.pt/)'s [Open Banking](https://openbankproject.com/) [PSD2](https://www.evry.com/en/news/articles/psd2-the-directive-that-will-change-banking-as-we-know-it/) API, bank based in Portugal. 



This library helps to:

* Authorization Flow (OAuth 2)
* Get Balances
* Get Moviments
* Make transferences
* Make payments via portuguese standarts.

## Usage

The main entry point for the API is to create a valid `BestBankAPI` object:

By default, without any options, Markdig is using the plain CommonMark parser:

```csharp

var app = new Application(appConsumer, appSecret, Environment.Sandbox); // Set your application details
var user = new User(accessToken); // Set the user Access Token (In this case this user already made the whole OAUth 2 previously)

var api = new BancoBestAPI(user, app); // Setup a BancoBestAPI object
var balance = api.Balance(); // Get for example the account balance
```

## Download

The Best Bank Portugal Connect is available as a NuGet package: [![NuGet](https://img.shields.io/nuget/v/BestBankPortugalConnect.svg)](https://www.nuget.org/packages/BestBankPortugalConnect/)
