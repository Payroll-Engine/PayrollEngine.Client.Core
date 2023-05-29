<h1>Payroll Engine Client Core</h1>

The base library for any Payroll Engine client, which determines the commonality between the backend and the clients:
- Object model: Swagger schema as C# objects
- Api services: Swagger Endpoints as C# services
- Exchange tools: data import/export and [data visitors](https://en.wikipedia.org/wiki/Visitor_pattern)
- Query expressions:  [OData](https://www.odata.org/) based fluent api to build REST API queris
- Script parser and tools
- Console application tools

## Build
Supported runtime environment variables:
- *PayrollEngineSchemaDir* - the Json schema target direcotry (optional)
- *PayrollEnginePackageDir* - the NuGet package target direcotry (optional)