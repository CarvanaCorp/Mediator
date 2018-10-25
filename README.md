# Mediator &middot; [![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://www.google.com) [![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](https://www.google.com)

Carvana Mediator is a minimalist .NET Standard implementation of type-based in-memory messaging dispatch. 

## Features

1. Explicit Registration
2. Function-based instead of interface-based registration
3. Full support for Async/Await
4. Plugs into your application, no changes to existing code
5. Uses strongly-typed request/response data structures

## Installation

Carvana Mediator is available on NuGet.org. 

```Install-Package Carvana.Mediator```

```dotnet add package Carvana.Mediator```

## Examples

**Register Handler**
```
var mediator = new InMemoryMediator();
mediator.Register<SampleRequest, SampleResponse>(x => _sampleResponse);
```

**Get Response**

```
var response = mediator.Handle(new SampleRequest { Content = "Hello World" });
```

## Contributing

The main purpose of this repository is to share Carvana Mediator with the .NET ecosystem and to continue to evolve Carvana Mediator, making it easier to use and more feature-rich. 

### License

Mediator is [MIT licensed](./LICENSE).
