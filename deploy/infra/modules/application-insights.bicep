targetScope = 'resourceGroup'

param pattern string
param sku string = 'PerGB2018'
param location string = resourceGroup().location

resource workspace 'Microsoft.OperationalInsights/workspaces@2022-10-01' = {
  name: format(pattern, 'workspace')
  location: location
  identity:{
    type: 'SystemAssigned'
  }
  properties:{
    sku: {
      name: sku
    }
  }
}

resource applicationInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: format(pattern, 'ai')
  location: location
  kind: 'web'
  properties:{
    Application_Type: 'web'
    WorkspaceResourceId: workspace.id
  }
}

var connectionString = applicationInsights.listkeys().connectionString

output connectionString string = connectionString
