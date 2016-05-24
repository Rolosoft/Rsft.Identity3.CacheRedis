# Redis Cache For Identity Server 3

[![rolosoft_public_packages MyGet Build Status](https://www.myget.org/BuildSource/Badge/rolosoft_public_packages?identifier=a53e1b7a-1d56-43f7-8e0e-a518e82c8cb5)](https://www.myget.org/)

## About
A library that delivers a Redis based implementation of Identity Server 3 extension points for:

* ```ICache<Client>```
* ```ICache<IEnumerable<Scope>>```
* ```ICache<IEnumerable<Claim>>```

## Who and what is this for?
Identity Server 3 default implementations for Client, Scope and Claim persistence are volatile in-memory stores. This is no problem for single server solutions. However, in load balanced environments, the default in-memory stores fail.

In such shared server scenarios, a central persistence store is required. Redis offers a perfect solution.

## Features and Benefits
* Thread safe, high performance implementation
* Extensive async logging built in using [SLAB](https://msdn.microsoft.com/en-us/library/dn440729(v=pandp.60).aspx)
* Built in compression (can be disabled if required)
* Optional configuration over-rides for cache duration, cache prefix and compression options

## Installation
From Nuget
~~~
install-package Rsft.Identity3.CacheRedis
~~~

## Quick Start
See unit / integration tests for full examples. A default setup might look like this:

```csharp
/*Create a singleton instance of our Redis integration extension */
var caches = RedisCacheFactory.Create(RedisConnectionString);

/*Assign caches to vars for ease of single-step debugging / inspection etc.*/
var clientCache = caches.ClientCache;
var scopesCache = caches.ScopesCache;
var userServiceCache = caches.UserServiceCache;

/*Create Identity Server 3 with default, in memory persistence stores. NOTE: persistence stores will be your SQL, EF or storage production stores or whatever*/
var identityServerServiceFactory = new IdentityServerServiceFactory().UseInMemoryClients(new List<Client>()).UseInMemoryScopes(new List<Scope>()).UseInMemoryUsers(new List<InMemoryUser>());

/*Add the Redis cache stores as extension objects into Identity Server 3*/
identityServerServiceFactory.ConfigureClientStoreCache(new Registration<ICache<Client>>(clientCache));
identityServerServiceFactory.ConfigureScopeStoreCache(new Registration<ICache<IEnumerable<Scope>>>(scopesCache));
identityServerServiceFactory.ConfigureUserServiceCache(new Registration<ICache<IEnumerable<Claim>>>(userServiceCache));
```

## Advanced Topics

### Connection Multiplexer
A Redis ConnectionMultiplexer or connection string is required to create a connection to Redis.

There are 3 ways of doing this:

1) Pass in a connection string
e.g.
```csharp
var caches RedisCacheFactory.Create("<your connection string>");
```

2) Pass in an existing Connection Multiplexer
e.g.
```csharp
var caches RedisCacheFactory.Create(MyConnectionMultiplexer);
```
__Note:__ If you are creating a Connection Multiplexer yourself, be sure to ensure that it is a singleton instance as per recommendations described [here](https://github.com/StackExchange/StackExchange.Redis/blob/master/Docs/Basics.md)

3) Connection string
In App.config or Web.config, ConnectionString or value named ```'Rsft:Identity3:CacheConnectionString'```

We use Azure CloudConfigurationManager wo ensure that configuration options are as diverse as possible for Azure hosting options (e.g Cloud service, Web etc)

e.g.
__Web.config__ or __App.config__
```xml
<appSettings>
    <add key="Rsft:Identity3:CacheConnectionString" value="MyConnectionString"/>
</appSettings>
```

e.g.
__*.cscfg__
```xml
<ConfigurationSettings>
  <Setting name="Rsft:Identity3:CacheConnectionString" value="MyConnectionString" />
</ConfigurationSettings>
```

### Configuration
Customize configuration by passing in an optional ```IConfiguration<RedisCacheConfigurationEntity>``` to the ```RedisCacheFactory.Create(..)``` method.

Configuration options are:

|Option Name               | Description                                 | Default      |
|--------------------------|---------------------------------------------|--------------|
| CacheDuration            | Duration of cache (in seconds)              | 3600         |
| RedisCacheDefaultPrefix  | Prefix of keys to store in cache            | rsftid3cache |
| UseObjectCompression     | Use compression to store objects in Redis ? | true         |

### Logging
Extensive async logging is built in using [SLAB](https://msdn.microsoft.com/en-us/library/dn440729(v=pandp.60).aspx).

There are two logs:
* __Exception__ - Add listener(s) for ```ExceptionLoggingEventSource.Log```
* __Activity__ - Add listener(s) for ```ActivityLoggingEventSource.Log```
 
Example of Creating Console Listener for Exception Logging.
```csharp
var observableEventListener = new ObservableEventListener();
observableEventListener.EnableEvents(ExceptionLoggingEventSource.Log, EventLevel.LogAlways);
observableEventListener.LogToConsole();
```

See unit tests in source code for more examples of logging verbosity options and filters.

