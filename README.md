# AI Console App in .NET (using Microsoft.Extensions.AI)

A minimal console chat application built with .NET that calls an AI chat model. The sample uses `OpenAI.Chat` with a configurable endpoint (default points to GitHub Models) and maintains a running chat history in the console.

## Overview
- Target framework: `net10.0` (project `ConsoleAppUingAI`)
- Key packages:
  - `Microsoft.Extensions.AI` (10.0.0)
  - `OpenAI.Chat` (transitively via `Azure`/`System.ClientModel`)
- Default model: `gpt-4.1-mini`
- Default endpoint: `https://models.github.ai/inference`

## Prerequisites
- .NET SDK 10 (Preview) or newer that supports `net10.0`.
- An access token/API key for the chosen model host:
  - GitHub Models: a GitHub token with access (PAT or issued token).
  - Or another compatible endpoint and key (e.g., Azure OpenAI/OpenAI) if you change the client configuration.
- Windows PowerShell, macOS, or Linux shell.

## Project Structure
```
AI-App-in-.NET-using-microsoft.Extensions.AI/
├─ ConsoleAppUingAI.sln
├─ ConsoleAppUingAI/
│  ├─ ConsoleAppUingAI.csproj
│  └─ Program.cs
├─ .gitattributes
├─ .gitignore
└─ README.md
```

## Quick Start
1) Clone the repo
```powershell
git clone "https://github.com/<your-account>/AI-App-in-.NET-using-microsoft.Extensions.AI.git"
cd "AI-App-in-.NET-using-microsoft.Extensions.AI"
```

2) Provide your API key/token
- Fastest (edit the sample): open `ConsoleAppUingAI/Program.cs` and replace `"your api key here"` with your token.
- Recommended (avoid hardcoding): store your token in an environment variable and adjust the code to read it.

PowerShell example for a session-scoped env var:
```powershell
# Choose a name; match it in your code if you switch to env-based loading
$env:GITHUB_MODELS_TOKEN = "<your-token>"
```

3) Restore, build, and run
```powershell
# From the repo root
dotnet restore
_dot = (Get-Location).Path
dotnet run --project "$_.\ConsoleAppUingAI"
```
Alternatively, open the folder in VS Code and use Run/Debug on the project.

## Usage
- The app starts a simple REPL.
- Type your message and press Enter.
- Type `exit` to quit.

## Configuration
You can customize these in `Program.cs`:
- Model ID: `"gpt-4.1-mini"` (change to another supported model if desired).
- Endpoint: `https://models.github.ai/inference` (change for Azure OpenAI/OpenAI endpoints).
- Credentials: swap `new ApiKeyCredential("...")` to use an environment variable, e.g.:

```csharp
var apiKey = Environment.GetEnvironmentVariable("GITHUB_MODELS_TOKEN");
if (string.IsNullOrWhiteSpace(apiKey))
{
    Console.Error.WriteLine("Missing API key: set GITHUB_MODELS_TOKEN.");
    return;
}
var chatClient = new ChatClient(
    "gpt-4.1-mini",
    new ApiKeyCredential(apiKey),
    new OpenAI.OpenAIClientOptions { Endpoint = new Uri("https://models.github.ai/inference") }
);
```

## Security & Git Hygiene
- Do not commit secrets (API keys, tokens, `.env` files). The `.gitignore` in this repo excludes common user/build artifacts like `bin/`, `obj/`, `.vs/`, and `.env`.
- Keep personal IDE settings private unless intentionally shared.

## Troubleshooting
- SDK/TFM errors: ensure you have a .NET SDK that supports `net10.0` (preview at the time of writing). Use `dotnet --info` to verify.
- Auth errors: confirm the token is valid and permitted to call the chosen endpoint. For GitHub Models, ensure the token has access and the endpoint is `https://models.github.ai/inference`.
- Network issues: check proxy/firewall; try again later.

## Next Steps
- Swap to environment-variable based credentials to avoid hardcoding.
- Add streaming responses and cancellation.
- Parameterize model/endpoint with `appsettings.json` or CLI args.
- Add minimal tests for message flow.

---
Maintained by: Your Name