# moto s3 using AWS_ENDPOINT_URL from .env

Author: Dan Dumitru; Created: October 24, 2023; Last Edit: October 24, 2023  
Tags: Python,AWS; Views: 684

## Problem

In my Python project, I'm uploading files to AWS S3 using their boto3 library, with code that starts like:

    client = boto3.client("s3", region_name=region, endpoint_url=endpoint_url)

I'm providing here the `endpoint_url`, as locally this points to LocalStack configured through an environment variable AWS_ENDPOINT_URL in my **.env** file.

To unit test this I'm using the **moto** library, which helps with mocking AWS services. I have a unit test method decorated with the `@mock_s3` attribute and inside it I'm first creating a bucket, as per their documentation:

```py
s3 = boto3.resource("s3")
s3.create_bucket(Bucket="test-bucket")
```

Because of the mocking attribute, this code is actually intercepted by moto.

I noticed that this unit test sometimes fails. Investigating this, I realized that it fails when my LocalStack Docker container is stopped, which lead me to the fact that the code is actually trying to create the bucket using the environment variable AWS_ENDPOINT_URL that I have configured in my **.env** file.

Now that's a serious problem, as I want the unit test to be independent and NOT use that AWS_ENDPOINT_URL.

## Solution

After some debugging and things that I tried, I managed to fix this by setting that environment variable to empty string from the unit test code, so before the code above I added:

```py
os.environ["AWS_ENDPOINT_URL"] = ""
```

**moto** do have advice along these lines in their documentation: [https://docs.getmoto.org/en/latest/docs/getting_started.html#how-do-i-avoid-tests-from-mutating-my-real-infrastructure](https://docs.getmoto.org/en/latest/docs/getting_started.html#how-do-i-avoid-tests-from-mutating-my-real-infrastructure)

, but this variable, AWS_ENDPOINT_URL, is not covered there.
