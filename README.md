# Payroll Engine Client Core
👉 This library is part of the [Payroll Engine](https://github.com/Payroll-Engine/PayrollEngine/wiki).

The base library for each Payroll Engine client, defining the commonality between the backend and the clients:
- Object model: Swagger schema as C# objects
- API services: Swagger endpoints as C# services
- Exchange tools: data import/export and [data visitors](https://en.wikipedia.org/wiki/Visitor_pattern)
- Query expressions:  [OData](https://www.odata.org/) based fluent API for building REST API queries
- Script parser and tools
- Console application tools

## Payroll HTTP Configuration
The HTTP Payroll configuration consists of the following values:

| Setting      | Description                             | Default        |
|:--|:--|:--|
| `BaseUrl`    | The backend base URL (string)           |                |
| `Port`       | The backend url port (string)           | None           |
| `Timeout`    | The backend request timeout (TimeSpan)  | 100 seconds    |
| `ApiKey`     | The backend API key (string)            | None           |

> The `ApiKey` setting should only be set in development environments.

Example of a JSON payroll http configuration `apisettings.json`:
```json
{
  "baseUrl": "https://localhost",
  "port": 44354,
  "timeout": "00:30:00"
}
```

Example of a connection string containing the payroll http configuration:
```txt
BaseUrl=https://localhost; Port=44354; Timeout=00:30:00
```

### HTTP Configuration Setup
The configuration of the Payroll HTTP client is determined by the following priorities:

1. Connection string from the `PayrollApiConnection` environment variable.
2. Configuration JSON file name from the `PayrollApiConfiguration` environment variable.
3. Configuration JSON file `apisettings.json` located in the program folder.
4. Program configuration `appsettings.json` located in the program folder.

> The base class for console applications, `ConsoleProgram<TApp>`, uses this prioritization.

## Build
Supported runtime environment variables:
- *PayrollEngineSchemaDir* - the Json schema destination directory (optional)
- *PayrollEnginePackageDir* - the NuGet package destination directory (optional)

## Third party components
- YAML serialization with [YamlDotNet](https://github.com/aaubry/YamlDotNet) - license `MIT`
