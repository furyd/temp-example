targetScope = 'subscription'

param projectName string
param serviceName string
param environmentName string
param location string

var pattern = '${projectName}-${serviceName}-{0}-${environmentName}'

resource rg 'Microsoft.Resources/resourceGroups@2021-01-01' = {
  name: format(pattern, 'rg')
  location: location
}

module applicationInsights 'modules/application-insights.bicep' = {
  name: 'app-insights'
  scope: rg
  params:{
    pattern: pattern
    location: rg.location
  }
}

module site 'modules/service-bus.bicep' = {
  name: 'site'
  scope: rg
  params:{
    pattern: pattern
    location: rg.location
  }
}
