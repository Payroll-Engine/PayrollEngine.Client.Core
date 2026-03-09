# Payroll Engine Client Core

> Part of the [Payroll Engine](https://github.com/Payroll-Engine/PayrollEngine) open-source payroll automation framework.
> Full documentation at [payrollengine.org](https://payrollengine.org).

Base library for all Payroll Engine clients. Defines the shared contract between the backend and client applications:

- **Object model** — C# representations of all API resources (Swagger schema)
- **API services** — typed service interfaces and HTTP implementations for every API endpoint
- **HTTP client** — `PayrollHttpClient` wrapper for the REST API with connection management and authentication
- **Exchange** — data import, export, and merge using the [Visitor Pattern](https://en.wikipedia.org/wiki/Visitor_pattern); JSON and YAML support
- **Query expressions** — [OData](https://www.odata.org/)-based fluent API for building REST API queries
- **Script tools** — script export to `.cs` development files and backend script rebuild
- **Console application framework** — base classes for commands and console programs

---

## NuGet Package

Available on [NuGet.org](https://www.nuget.org/profiles/PayrollEngine):

```sh
dotnet add package PayrollEngine.Client.Core
```

---

## HTTP Configuration

`PayrollHttpConfiguration` describes the backend connection. The `ConsoleProgram<TApp>` base class resolves configuration from the following sources in priority order:

| Priority | Source | Description |
|:--|:--|:--|
| 1 | `PayrollApiConnection` env var | Connection string with inline settings |
| 2 | `PayrollApiConfiguration` env var | Path to a JSON configuration file |
| 3 | `apisettings.json` | JSON file in the program folder |
| 4 | `appsettings.json` | Program configuration file |

Configuration settings:

| Setting | Type | Default | Description |
|:--|:--|:--|:--|
| `BaseUrl` | string | — | Backend base URL |
| `Port` | int | none | Backend port |
| `Timeout` | TimeSpan | 100 s | Request timeout |
| `ApiKey` | string | none | API key for authentication <sup>1)</sup> |

<sup>1)</sup> The `ApiKey` setting should only be used in development environments. In production, use the `PayrollApiKey` environment variable.

JSON configuration example (`apisettings.json`):
```json
{
  "baseUrl": "https://localhost",
  "port": 44354,
  "timeout": "00:30:00"
}
```

Connection string example:
```
BaseUrl=https://localhost; Port=44354; Timeout=00:30:00
```

---

## Architecture

### Object Model (`Model`)

C# representations of all API resources derived from the Swagger schema — tenants, regulations, payrolls, payruns, cases, wage types, collectors, lookups, reports, webhooks, and their result types. Each resource has a concrete class and a corresponding interface (e.g., `Employee` / `IEmployee`).

### API Services (`Service` / `Service.Api`)

Typed service layer covering every API endpoint. Services follow a context-based hierarchy that mirrors the REST URL structure:

```
RootServiceContext
  └─ TenantServiceContext
       ├─ RegulationServiceContext  → CaseService, WageTypeService, CollectorService, ...
       ├─ PayrollServiceContext     → PayrollService, PayrollLayerService, ...
       ├─ PayrunServiceContext      → PayrunService, PayrunJobService, ...
       └─ ...
```

`Service` contains the interfaces (`ICaseService`, `IPayrunService`, …) and `Service.Api` contains the `PayrollHttpClient`-backed implementations.

### `PayrollHttpClient`

HTTP wrapper for the Payroll Engine REST API.

| Aspect | Behaviour |
|:--|:--|
| Thread safety | Not thread-safe — do not share instances across threads |
| Ownership | Owned when created via URL/handler constructors; unowned when an external `HttpClient` is injected |
| Authentication | `SetApiKey()` / `SetTenantAuthorization()` set per-instance HTTP headers |
| Error handling | Non-success responses are thrown as `HttpRequestException` with the API error message |
| CRUD helpers | `UpsertObjectAsync<T>` inserts or updates based on presence of an existing object |

### Exchange Module

Handles import, export, and in-memory merge of payroll data. Supports JSON (default) and YAML file formats.

**Class hierarchy:**
```
VisitorBase                   Validation, exchange model access
  └─ Visitor                  Full exchange traversal (tenants, regulations, payrolls, …)
       └─ AttachmentsLoader   Loads embedded script and report files
            └─ ExchangeImportVisitor   Resolves names to IDs (user, employee, division)
                 └─ ExchangeImport     Upserts objects via API; submits case changes
       └─ ExchangeMerge        In-memory merge of two exchange models with duplicate detection
ExchangeExport                Reads data from the API into an exchange model
```

- `ExchangeImportVisitor` resolves identifiers when the tenant is already persisted (`tenant.Id > 0`).
- `ExchangeImport.SetupCaseChangeAsync` always resolves identifiers and then submits the case change via `AddCaseAsync`.
- `FileReader` / `FileWriter` auto-detect JSON or YAML based on file extension.

### Query Expressions (`QueryExpression`)

Fluent OData-based API for building REST query strings:

```csharp
using PayrollEngine.Client.QueryExpression;

// filter: active employees with a specific identifier
var filter = new Active().And(new EqualIdentifier("emp-001"));

// order by name ascending, then by created date descending
var order = new OrderByAscending("LastName").AndThenBy(new OrderByDescending("Created"));

// combine into query parameters
var query = new QueryParameters()
    .Filter(filter)
    .OrderBy(order)
    .Top(50);
```

Available expression types: `Active`, `Inactive`, `EqualId`, `EqualName`, `EqualIdentifier`, `EqualStatus`, `Contains`, `StartsWith`, `EndsWith`, `Equals`, `NotEquals`, `Greater`, `GreaterEquals`, `LessEquals`, `LessFilter`, `Date`, `Year`, `Month`, `Day`, `Hour`, `Minute`, `Time`, `OrderByAscending`, `OrderByDescending`, `Select`.

### Script Tools (`Script`)

**`PayrollScriptExport`** exports regulation scripts from the API into standalone `.cs` files for local C# development. It reads function expressions from cases, case relations, collectors, wage types, and reports, merges them into embedded C# function templates, and returns typed `DevelopmentScript` objects ready to be written to disk.

Export scope is controlled by `ScriptExportContext.ScriptObject` (`All`, `Case`, `CaseRelation`, `Collector`, `WageType`, `Report`, `GlobalScript`) and `ScriptExportMode` (`All` or `Existing` — skip objects without any expression).

**`ScriptRebuild`** triggers a backend Roslyn recompile for regulation objects or a payrun by name. Supports targeted rebuild for a single named object or a full regulation sweep across all object types.

**Embedded function templates** — the `.Template.cs` files in `Script\` are compiled as embedded resources and used by `PayrollScriptExport` to generate the `.cs` output. They are excluded from the compiled assembly.

### Console Application Framework (`Command`, `ConsoleProgram`)

**`ConsoleProgram<TApp>`** — base class for all console applications. Provides:
- Lifecycle hooks: `InitializeAsync`, `RunAsync`, `ShutdownAsync`
- Automatic HTTP client setup using `PayrollHttpConfiguration`
- Help mode detection (`/?`, `-?`, `/help`)
- Culture configuration
- Structured error handling with optional exit codes

**`CommandBase`** — base class for individual commands. Implements `ICommand` with parameter validation, error display, and console output helpers. Commands are registered with and dispatched by `CommandManager`.

---

## Build

```sh
dotnet build -c Release
dotnet pack -c Release
```

Environment variables used during build:

| Variable | Description |
|:--|:--|
| `PayrollEngineSchemaDir` | Builds and publishes `PayrollEngine.Exchange.schema.json` to this directory (optional) |
| `PayrollEnginePackageDir` | Output directory for the NuGet package (optional) |

---

## Third Party Components
- YAML serialization with [YamlDotNet](https://github.com/aaubry/YamlDotNet) — license `MIT`

---

## See Also
- [Payroll Engine Client Scripting](https://github.com/Payroll-Engine/PayrollEngine.Client.Scripting) — scripting API built on top of this library
- [Payroll Engine Client Services](https://github.com/Payroll-Engine/PayrollEngine.Client.Services) — local script execution services
- [Payroll Engine Console](https://github.com/Payroll-Engine/PayrollEngine.PayrollConsole) — console application using this library
- [Payroll Engine WebApp](https://github.com/Payroll-Engine/PayrollEngine.WebApp) — web application using this library
- [OData Query Options](https://docs.oasis-open.org/odata/odata/v4.01/odata-v4.01-part2-url-conventions.html) — OData URL conventions reference
