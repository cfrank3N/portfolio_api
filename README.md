# Portfolio API

[![Build Status](https://img.shields.io/github/actions/workflow/status/cfrank3N/portfolio_api/.github/workflows/azure-webapps-dotnet-core.yml)](https://github.com/cfrank3N/portfolio_api/actions)  
![Last Commit](https://img.shields.io/github/last-commit/cfrank3N/portfolio_api)

Backend API in **ASP.NET Core** for my portfolio project.
Used this as an opportunity to learn how to write APIs in multiple languages.  
It handles storing messages from the contact form, emailing me the message contents,
and fetching pinned GitHub repositories for me to display on the frontend.

---

## Features

- RESTful API endpoints
- Save and manage contact form messages
- Fetch pinned / selected GitHub repos
- Email me each new message via BREVO
- Built with **ASP.NET Core Web API**
- CORS enabled for frontend integration
- CI/CD flow with Github actions

---

## Tech Stack

- **ASP.NET Core** (C#)
- **Entity Framework Core**
- **xUnit** testing
- **PostgreSQL**
- **GitHub API** integration
- **Brevo API** integration
- **Azure App Service**

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL DB hosted on [Render](https://render.com/) (for development preferably use a docker container running PostgreSQL)
- [GitHub presonal access token](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens)
- [Brevo API key](https://developers.brevo.com/docs/getting-started)

### Installation

```bash
git clone https://github.com/cfrank3N/portfolio_api.git
cd portfolio_api
```

### Configuration

Add a Properties directory from root and add launchSettings.json to it

```json
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "http://localhost:5182",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "https://localhost:7138;http://localhost:5182",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

Use [dotnet's secret manager](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0&tabs=windows)

```bash
dotnet user-secrets init
dotnet user-secrets set "ApiKeys:BrevoApiKey" "<your_brevo_key>"
dotnet user-secrets set "ApiKeys:GitHubToken" "<your_github_token>"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your_connection_string>"
dotnet user-secrets set "EmailList:SenderName" "<sender_name>"
dotnet user-secrets set "EmailList:SenderEmail" "<your_brevo_authenticated_email>"
dotnet user-secrets set "EmailList:RecipientName" "<recipient_name>"
dotnet user-secrets set "EmailList:RecipientEmail" "<recipient_email>"
```

Add your secrets to [Github secrets](https://docs.github.com/en/actions/how-tos/write-workflows/choose-what-workflows-do/use-secrets) (if you initialize a remote repo)
Github secret name: APIKEYS__BREVOAPIKEY
Dotnet translates __ into :

### Running the Server (Development)

```bash
dotnet restore
dotnet run
```

The API will run by default on `http://localhost:5182` or `https://localhost:7138` if you're running https.

---

## API Endpoints

| Method | Path                | Description                          | Request Body / Query                   |
| ------ | ------------------- | ------------------------------------ | -------------------------------------- |
| `GET`  | `/api/repos/pinned` | Fetch pinned / selected GitHub repos | —                                      |
| `POST` | `/api/savemessage`  | Save contact form message            | `{ senderName, senderEmail, content }` |

### Example: Save message

```http
POST /api/savemessage
Content-Type: application/json

{
  "senderName": "Adam",
  "senderEmail": "adam@example.com",
  "content": "Hello, please contact me!"
}
```

Response:
201 Created
```json
{
  "senderName": "Adam",
  "senderEmail": "adam@example.com",
  "content": "Hello, please contact me!"
}
```

### Example: Get pinned repositories

```http
GET /api/repos/pinned
```

Response:

```json
[
  {
  "name": "portfolio_api",
  "url": "https://github.com/cfrank3N/portfolio_api",
  "description": "API in ASP.NET for my portfolio project.
  Used this as an opportunity to learn how to write API's in multiple languages.
  Hosted on Azure. Implements an emailing service via Brevo and fetches my pinned
  repos on Github via their GraphQL API."
  }
]
```

---

## Error Handling & Validation

- Model validation with Data Annotations
- Returns appropriate HTTP codes (400 for validation errors, 500 for server issues)

---

## Deployment

This API can be deployed to **Azure App Service**, **Docker**, or any host supporting .NET.

Steps:

1. Build the project:
   ```bash
   dotnet publish -c Release
   ```
2. Deploy the published output from `bin/Release/netX/publish/`.
3. Configure environment variables on the host.

Or use [this guide](https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net80&pivots=development-environment-vs) to deploy to azure and use github actions as your CD flow.

## Contributing

Contributions, issues, and feature requests are welcome.

1. Fork the repo
2. Create a branch (`feature/your_feature` or `fix/your_fix`)
3. Commit your changes
4. Open a PR

---

## Acknowledgments

- [ASP.NET Core Documentation](https://learn.microsoft.com/aspnet/core)
- [Entity Framework Core](https://learn.microsoft.com/ef/core)
- [Azure App Service](https://azure.microsoft.com/services/app-service/)

