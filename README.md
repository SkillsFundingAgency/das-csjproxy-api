## ⛔Never push sensitive information such as client id's, secrets or keys into repositories including in the README file⛔

# das-csjproxy-api

<img src="https://avatars.githubusercontent.com/u/9841374?s=200&v=4" align="right" alt="UK Government logo">

_Update these badges with the correct information for this project. These give the status of the project at a glance and also sign-post developers to the appropriate resources they will need to get up and running_

[![Build Status](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_apis/build/status/_projectname_?branchName=master)](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_build/latest?definitionId=_projectid_&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=_projectId_&metric=alert_status)](https://sonarcloud.io/dashboard?id=_projectId_)
[![Jira Project](https://img.shields.io/badge/Jira-Project-blue)](https://skillsfundingagency.atlassian.net/secure/RapidBoard.jspa?rapidView=564&projectKey=_projectKey_)
[![Confluence Project](https://img.shields.io/badge/Confluence-Project-blue)](https://skillsfundingagency.atlassian.net/wiki/spaces/_pageurl_)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg?longCache=true&style=flat-square)](https://en.wikipedia.org/wiki/MIT_License)

_Add a description of the project and the high-level features that it provides. This should give new developers an understanding of the background of the project and the reason for its existence._

## About

The das-csjproxy-api (https://github.com.mcas.ms/SkillsFundingAgency/das-csjproxy-api) is the inner api for retrieving civil service jobs from external sources.

## 🚀 Installation

### Pre-Requisites
* A clone of this repository(https://github.com.mcas.ms/SkillsFundingAgency/das-csjproxy-api)
* A code editor that supports .NetCore 8 and above
* A storage emulator like Azurite (https://learn.microsoft.com/en-us/azure/storage/common/storage-use-emulator)
* An Azure Active Directory account with the appropriate roles as per the [das-employer-config repository](https://github.com/SkillsFundingAgency/das-employer-config/blob/master/das-csjproxy-api/SFA.DAS.CSJProxy.Api.json)

### Config
You can find the latest config file in [das-employer-config repository](https://github.com/SkillsFundingAgency/das-employer-config/blob/master/das-csjproxy-api/SFA.DAS.CSJProxy.Api.json)

* If you are using Azure Storage Emulator for local development purpose, then In your Azure Storage Account, create a table called Configuration and Add the following

ParitionKey: LOCAL
RowKey: SFA.DAS.CSJProxy.Api.json
Data:
```json
{
  "AzureAd": {
    "identifier": "https://{TENANT-NAME}/{IDENTIFIER}",
    "tenant": "{TENANT-NAME}"
  }  
}
```

In the web project, if it does not exist already, add `AppSettings.Development.json` file with the following content:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConfigurationStorageConnectionString": "UseDevelopmentStorage=true",
  "ConfigNames": "SFA.DAS.CSJProxy.Api",
  "EnvironmentName": "LOCAL",
  "Version": "1.0"
}
```

## Technologies
* .NetCore 8.0
* Azure Storage Account
* MongoDb
* NUnit
* Moq
* FluentAssertions
* Azure App Insights
* MediatR

## How It Works

### Running

* Open command prompt and change directory to _**/src/SFA.DAS.CSJProxy.Api/**_
* Run the web project _**/src/SFA.DAS.FAA.CSJProxy.Api.csproj**_

MacOS
```
ASPNETCORE_ENVIRONMENT=Development dotnet run
```
Windows cmd
```
set ASPNETCORE_ENVIRONMENT=Development
dotnet run
```

### Application logs
Application logs are logged to [Application Insights](https://learn.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview) and can be viewed using [Azure Monitor](https://learn.microsoft.com/en-us/azure/azure-monitor/overview) at https://portal.azure.com

## Useful URLs

### Health check
https://localhost:7273/health - Endpoint to check the Mongo DB's connectivity health status

### Apprenticeship

https://localhost:7273/api/apprenticeship/{email} - Endpoint to get all the apprenticeships of a user by given email address

### User

https://localhost:7273/api/user/{email} - Endpoint to get user's information by given email address

https://localhost:7273/api/user/validate-credentials - Endpoint to validate user credentials

## 🐛 Known Issues

Nuget Package - MongoDB.Driver 2.13.0 contains vulnerabilities.


## License

Licensed under the [MIT license](LICENSE)
