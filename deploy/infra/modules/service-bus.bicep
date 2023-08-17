targetScope = 'resourceGroup'

param pattern string
param sku string = 'Basic'
param location string = resourceGroup().location

resource serviceBus 'Microsoft.ServiceBus/namespaces@2021-11-01' = {
  name: format(pattern, 'sb')
  location: location
  sku:{
    name: sku
  }
  identity:{
    type: 'SystemAssigned'
  }
}

resource deadLetterQueue 'Microsoft.ServiceBus/namespaces/queues@2021-11-01' = {
  name: 'dead-letters'
  parent: serviceBus
  properties: {
    requiresDuplicateDetection: false
    requiresSession: false
    enablePartitioning: false
  }
}

resource queue 'Microsoft.ServiceBus/namespaces/queues@2021-11-01' = {
  name: 'fieldmodel'
  parent: serviceBus
  properties: {
    requiresDuplicateDetection: false
    requiresSession: false
    enablePartitioning: false
  }
}

var serviceBusEndpoint = '${serviceBus.id}/AuthorizationRules/RootManageSharedAccessKey'
var connectionString = listKeys(serviceBusEndpoint, serviceBus.apiVersion).primaryConnectionString

output serviceBusConnectionString string = connectionString
