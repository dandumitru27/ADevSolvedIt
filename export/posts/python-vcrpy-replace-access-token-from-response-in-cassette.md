# Python VCR.py replace access token from response in cassette

Author: Dan Dumitru; Created: November 28, 2024; Last Edit: November 28, 2024  
Tags: Python; Views: 500

## Problem

In one of my unit tests I use VCR.py to save a cassette of the request and response to an API call that requests an access token.

The saved response contains something like:
```
  response:
    body:
      string: '{"access_token":"AQ1231KOsdasdOEWE..."}'
```

I'd like to replace the actual access token from the saved cassette, as it's sensitive info, and also automated checks raise errors for having tokens like that in the source code.

How can I do that with VCR.py?

## Solution

There is some documentation on how to achieve this, using a `before_record_response` configuration option:

[https://vcrpy.readthedocs.io/en/latest/advanced.html#custom-response-filtering](https://vcrpy.readthedocs.io/en/latest/advanced.html#custom-response-filtering)

The example there simply replaces a known string with a dummy value. As I don't know the actual access token value, I needed to use a regex to do the replacement:

```
def scrub_access_token(response):
    if "body" in response and "string" in response["body"]:
        response_body = response["body"]["string"].decode("utf-8")

        pattern = r'"access_token":".*"'
        replacement = '"access_token":"dummy_access_token"'

        response_body = re.sub(pattern, replacement, response_body)

        response["body"]["string"] = response_body.encode("utf-8")

    return response

my_vcr = vcr.VCR(
    before_record_response=scrub_access_token,
)
with my_vcr.use_cassette('test.yml'):
     # your http code here
```
