In-memory double for unit testing with ORMs:

How to use:

1 - Create your own implementation for the "~\Interfaces\IDataProvider.cs" interface using your favourite ORM to access the data.

2 - Use the class "~\Doubles\InMemoryDataProvider.cs", which holds the fake implementation, inside your unit tests or in your implementation for debugging purposes.
