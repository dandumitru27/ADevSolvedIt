# Kubernetes secrets in .NET configuration, from Azure Key Vault

Author: Dan Dumitru; Created: July 8, 2021; Last Edit: July 8, 2021  
Tags: Azure,Kubernetes,C#; Views: 68

## Problem

If you have a .NET app deployed in Azure Kubernetes, you will probably want to load your secrets from an Azure Key Vault, and you will probably arrive at this guide from MSDN - *"Tutorial: Configure and run the Azure Key Vault provider for the Secrets Store CSI driver on Kubernetes"*:

[https://docs.microsoft.com/en-us/azure/key-vault/general/key-vault-integrate-kubernetes](https://docs.microsoft.com/en-us/azure/key-vault/general/key-vault-integrate-kubernetes)

After you manage to get everything working, this guide leaves you with being able to read the secrets in your Kubernetes pod, with commands like:

```
kubectl exec nginx-secrets-store-inline -- ls /mnt/secrets-store/
```
and
```
kubectl exec nginx-secrets-store-inline -- cat /mnt/secrets-store/secret1
```

But how can you easily load these secrets in your .NET app's configuration, and make them apply over the other appsettings?

## Solution

It turns out that, after following the guide, your Kubernetes pod is able to connect to and get secrets from the key vault without supplying any other credentials, so you can use code like this:

```cs
// using Azure.Security.KeyVault.Secrets;
// using Azure.Identity;
// using Azure.Extensions.AspNetCore.Configuration.Secrets;

public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
            if (context.HostingEnvironment.IsProduction())
            {
                var builtConfig = config.Build();
                var secretClient = new SecretClient(
                    new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                    new DefaultAzureCredential());
                config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
            }
        })
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
```

This code was taken from this page, you can read the linked section for more information: [https://docs.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-5.0#use-managed-identities-for-azure-resources](https://docs.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-5.0#use-managed-identities-for-azure-resources)

One more thing I needed to do to make this work, I needed to give my pod's identity **list** rights to the key vault.

The guide is, at some point, giving **get** rights:
```
az keyvault set-policy -n contosoKeyVault5 --secret-permissions get --spn $clientId
```

So you need to also run:
```
az keyvault set-policy -n contosoKeyVault5 --secret-permissions list --spn $clientId
```

That's it.

I actually ended up not using entire parts of the guide, all those things with the Secrets Store CSI driver and the mounted volume; I just used the parts with creating the Azure identity, its associated Kubernetes resources, and giving it rights.
