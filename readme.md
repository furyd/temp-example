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

# Setting up Application Insights

## Projects using this

- Example.Api

You need to add a connectionstring into the relevant file, e.g. user secrets, appsettings.Dvelopment.json, in the following structure:

```
{
  "ApplicationInsights": {
    "ConnectionString": "<<your connectionstring>>"
  }
}
```

And update your logging section:

```
{
  "Logging": {
    "ApplicationInsights": {
      "Default": "Information"
    }
  }
}

```