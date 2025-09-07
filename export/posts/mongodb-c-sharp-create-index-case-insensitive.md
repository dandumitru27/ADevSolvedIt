# MongoDB C# Create Index case insensitive

Author: Dan Dumitru; Created: August 31, 2022; Last Edit: August 31, 2022  
Tags: MongoDB,C#; Views: 602

## Problem

By default indexes you create in MongoDB are case sensitive.

How to create a case insensitive index using the C# client?

## Solution

You have to specify the collation when creating the index, and set a collation strength for it.

Here is some sample code:

```
var mongoConnectionString = "your-mongo-connection-string";

var mongoClient = new MongoClient(mongoConnectionString);
var database = mongoClient.GetDatabase("Inventory");
var productCollection = database.GetCollection<Product>("Products");

var createIndexModel =
    new CreateIndexModel<Product>(Builders<Product>.IndexKeys.Ascending(i => i.ProductReference),
        new CreateIndexOptions
        {
            Collation = new Collation("en", false, CollationCaseFirst.Off, CollationStrength.Secondary)
        });
productCollection.Indexes.CreateOne(createIndexModel);
```
