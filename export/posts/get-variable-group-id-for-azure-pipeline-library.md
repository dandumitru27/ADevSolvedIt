# Get variable group id for Azure Pipeline Library

Author: Vlad Fibrich; Created: March 10, 2023; Last Edit: March 12, 2023  
Tags: Azure; Views: 185

## Problem

I want to update some values (and secrets) automatically in an Azure Library variable group from an Azure Pipeline. For this I need  to pass in the variable group id, 123 in the example below:

```
# yml pipeline file
echo $AZURE_DEVOPS_CLI_PAT | az devops login
az pipelines variable-group variable update --group-id 123 --name librarySecret --value $(somePipelineVar) --output table
```
Command updates 'librarySecret' with a the value of 'somePipelineVar'.\
Where can I find the variable group id?

## Solution

## Azure CLI
Run CLI command (can be run locally) and get ID value
```
# Azure CLI Command
az pipelines variable-group list --group-name my-variable-group

# Output
[
  {
    "createdBy": {
       ...
    },
    "createdOn": "2020-09-04T10:47:54.763333+00:00",
    "description": "",
    "id": 123,
    "isShared": false,
    "modifiedBy": {
       ...
    },
    "modifiedOn": "2021-05-12T12:52:24.343333+00:00",
    "name": "my-variable-group",
    "providerData": null,
    "type": "Vsts",
    "variableGroupProjectReferences": null,
    "variables": {
       ...
    }
  }
]
```

## Azure Devops

Azure DevOps -> Pipelines -> Library\
Check Azure Devops URL in your browser for variableGroupId
>/_library?itemType=VariableGroups&view=VariableGroupView&variableGroupId=123&path=
