# Setting up database access

## Projects using this

- Example.Api
- Example.TestDataLoader

You need to add a connection string into the relevant file, e.g. user secrets, appsettings.Dvelopment.json, in the following structure:

```
{
  "SqlServerSettings": {
    "ConnectionString": "<<your connection string>>"
  }
}
```

# Setting up service bus access

## Projects using this

- Example.Api
- Example.Receiver

You need to add an endpoint into the relevant file, e.g. user secrets, appsettings.Dvelopment.json, in the following structure:

```
{
  "ServiceBusSettings": {
    "Uri": "<<your endpoint>>"
  }
}
```