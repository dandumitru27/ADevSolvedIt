# MongoDB - CSFLE - Unrecognized pipeline stage name: '$search'

Author: Dan Dumitru; Created: September 28, 2022; Last Edit: September 28, 2022  
Tags: MongoDB; Views: 66

## Problem

I'm having a MongoDB setup with CSFLE (Client-Side Field Level Encryption) enabled and I want to use the more advanced and recommended Atlas Search for full-text search.

I'm using the `$search` aggregation stage, but keep getting the following error message:

> *MongoDB.Driver.Encryption.MongoEncryptionException: Encryption related exception: Command aggregate failed: Unrecognized pipeline stage name: '$search'.*



## Solution

It turns out the `$search` stage is not supported with CSFLE, it's not in the list of supported aggregation stages - [https://www.mongodb.com/docs/manual/core/csfle/reference/supported-operations/#supported-aggregation-stages](https://www.mongodb.com/docs/manual/core/csfle/reference/supported-operations/#supported-aggregation-stages)

There are two workarounds I could think of:

1. Don't use the Atlas Search and use instead the basic Find with filters in MongoDB, employing regular expressions such as `.*{searchValue}.*`
2. If possible, use Atlas Search without CSFLE. For some use cases, when you don't have to search by fields that are encrypted, you can use an unencrypted client that ignores the encrypted fields.
