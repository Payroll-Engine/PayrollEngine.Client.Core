# Payroll Engine Client Core
👉 This library is part of the [Payroll Engine](https://github.com/Payroll-Engine/PayrollEngine/wiki).

The base library for each Payroll Engine client, defining the commonality between the backend and the clients:
- Object model: Swagger schema as C# objects
- API services: Swagger endpoints as C# services
- Exchange tools: data import/export and [data visitors](https://en.wikipedia.org/wiki/Visitor_pattern)
- Query expressions:  [OData](https://www.odata.org/) based fluent API for building REST API queries
- Script parser and tools
- Console application tools

## Build
Supported runtime environment variables:
- *PayrollEngineSchemaDir* - the Json schema destination directory (optional)
- *PayrollEnginePackageDir* - the NuGet package destination directory (optional)