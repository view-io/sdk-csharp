<img src="https://github.com/view-io/sdk-csharp/blob/main/assets/view_logo.png?raw=true" height="48">

# C# SDK for View AI

View AI enables organizations to rapidly deploy conversational AI experiences and yield instant insights.  This SDK enables simplified consumption of the on-premises View AI services.

## License

View software is licensed under the [Fair Core License (FCL)](https://fcl.dev/) with graduation after two years to an Apache 2.0 license.  Use of the software requires acceptance of the license terms found in the file `LICENSE.md`.

## Feedback and Issues

Have feedback or issues?  Please file an issue here.

## Getting Started

Refer to the [View Postman](https://github.com/view-io/postman) collection for REST API examples as well as the [View Documentation](https://docs.view.io) and `Test.*` projects within the solution.

Add `using` statements to include the SDKs you wish to use.  Most SDKs will be reliant on `View.Sdk` being included in a `using` statement.

- `using View.Sdk;` - the base SDK including classes that are used by function-specific SDK
- `using View.Sdk.Assistant;` - the SDK for View Assistant, which exposes chat and retrieval-augmented generation APIs with the View Assistant microservice
- `using View.Sdk.Configuration` - the SDK for View Configuration, provides APIs for configuring objects using the View Configuration microservice
- `using View.Sdk.Graph` - the SDK for [View Graph](https://github.com/jchristn/litegraph) for interacting with the graph database using the LiteGraph microservice
- `using View.Sdk.Lexi` - the SDK for View Lexi, the data catalog and metadata search platform, using the View Lexi microservice
- `using View.Sdk.Orchestrator` - the SDK for View Orchestrator, which handles invocation and execution of data flows, using the View Orchestrator microservice
- `using View.Sdk.Processor` - the SDK for generating Universal Data Representation (UDR) documents, using the View document processor microservice
- `using View.Sdk.Semantic` - the SDK for extracting semantic cells, using the View semantic cell extractor microservice
- `using View.Sdk.Serialization` - provides support for JSON and XML serialization, used commonly across View SDKs
- `using View.Sdk.Storage` - the SDK for configuring and managing storage resources and objects, using the View storage microservice
- `using View.Sdk.Vector` - the SDK for interacting with View vector storage, using the View Vector microservice

## Version History

Please refer to CHANGELOG.md
