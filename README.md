# TDD Detroit style in-memory double for ORMs
This sample application has the goal to show how to perform unit testing Detroit style for an application where ORMs ([Entity Framework](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ef/overview), [NHibernate](http://nhibernate.info/)) are used.

## Implementation
* In the [Dal](https://github.com/RiccardoFragale/tdd-detroit-inmemory/tree/master/src/XDetroit.WebFrontend/Dal) folder in the XDetroit.WebFrontend project, you can find the class [SqlDataProvider.cs](https://github.com/RiccardoFragale/tdd-detroit-inmemory/blob/master/src/XDetroit.WebFrontend/Dal/SqlDataProvider.cs) which is a wrapper class to decouple the client code from the ORM specific code. The code to interact with entity framework and therefore the DbContext object will be available only inside this class

* In the [Doubles](https://github.com/RiccardoFragale/tdd-detroit-inmemory/tree/master/src/XDetroit.Testing.Behavioural/Doubles) folder in the XDetroit.Testing.Behavioural project, you can find the class [InMemoryDataProvider.cs](https://github.com/RiccardoFragale/tdd-detroit-inmemory/blob/master/src/XDetroit.Testing.Behavioural/Doubles/InMemoryDataProvider.cs) which holds the related implementation for using the code without the need for a database (typically this will be needed in tests, but it can be used during development as well)
