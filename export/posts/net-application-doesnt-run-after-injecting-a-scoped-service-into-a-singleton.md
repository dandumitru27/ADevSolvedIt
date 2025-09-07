# .NET application doesn't run after injecting a Scoped service into a Singleton

Author: andreiflorin-c; Created: August 2, 2024; Last Edit: August 2, 2024  
Tags: .NET,C#; Views: 105

## Problem

I tried to inject a repository (service that uses DbContext to communicate with the database) into an Arduino communication service. The former has a Scoped lifetime because it is short-lived and instantiated on a per-request basis, every time it is needed, while the latter is Singleton because it's required to keep a persistent connection with the Arduino board during program execution.

Injecting it like this causes an unhandled exception, because it can lead to unexpected behaviours such as memory leaks, stale data etc. Changing the lifetime of either is not possible as it leads to other problems.

```
public ArduinoCommunicationService(IRoomsRepository roomsRepository)
{
    _roomsRepository = roomsRepository;
}

## Solution

Create a factory, inject it into the singleton service and request the scoped service from the factory.

```
namespace TestRooms.Repositories.Interface
{
    public interface IRoomsRepositoryFactory
    {
        IRoomsRepository CreateRepository();
    }
}
```
```
using TestRooms.Repositories.Interface;

namespace TestRooms.Repositories.Implementation
{
    public class RoomsRepositoryFactory : IRoomsRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RoomsRepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IRoomsRepository CreateRepository()
        {
            var scope = _serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IRoomsRepository>();
        }
    }
}
```
```
public ArduinoCommunicationService(IRoomsRepositoryFactory roomsRepositoryFactory)
{
    _roomsRepositoryFactory = roomsRepositoryFactory;
}
...
var roomsRepository = _roomsRepositoryFactory.CreateRepository();
```
