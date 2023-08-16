# Setting up database access

You need to add a connection string into the relevant file, e.g. user secrets, appsettings.Dvelopment.json, in the following structure:

```
{
  "SqlServerSettings": {
    "ConnectionString": "<<your connection string>>"
  }
}
```