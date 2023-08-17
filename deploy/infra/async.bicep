targetScope = 'subscription'

param projectName string
param serviceName string
param environmentName string
param sku string = 'Basic'
param location string

var pattern = '${projectName}-${serviceName}-{0}-${environmentName}'

resource rg 'Microsoft.Resources/resourceGroups@2021-01-01' = {
  name: format(pattern, 'rg')
  location: location
}

module site 'modules/service-bus.bicep' = {
  name: 'site'
  scope: rg
  params:{
    pattern: pattern
    sku: sku
    location: rg.location
  }
}
