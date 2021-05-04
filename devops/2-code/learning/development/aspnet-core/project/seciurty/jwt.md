# jwt

Json Web Token is an open standard that defines a compact and self-contained way for securely transmitting information between

parties as a JSON object. this information can be verified and trusted because it is digitally signed.

JWT is digitally signed using a secret key by a token provider or authentication server.
a JWT helps the resource server verify the token data using the same secret key,so that you can trust the data.

JWT consists of the following three parts:

Header: encoded data of token type end the algorithm used to sign the data
Payload: encoded data of claims intended to share
Signature: created by signing (encoded header + encoded payload) using a secret key

**final JWT token :Header.Payload.Signature**

## JWT token workflow in the following

1. Client requesting token
    - the client sends a request to the authentication server with the necessary information to prove its identity

2. Token creation
    - the authentication server receives the token request and verifies the identity if it is found valid, a token will be created
    with the necessary claims, and a JWT token will be sent back to the client

3. Client sends token to resource server
    - for each request to Resource of the API server ,the client needs to include a token in the header and request the resource using its URI

4. Resource server verifies the token
    - Read the token from authentication header.
    - split the header,payload,and signature from token
    - Create signature of received header and payload using the same secret key used when creating the token

> [!CAUTION]
> don't share confidential information using a JWT , since a JWT can be decoded and the claims or data it possesses can be viewed
## vs code extension REST-Client

[docs](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)
[VSCode 實用套件： REST Client](https://blog.rex-tsou.com/2017/10/vscode-%E5%AF%A6%E7%94%A8%E5%A5%97%E4%BB%B6-rest-client/)

## 網路資源

[ASP.NET 5: Authorization and Authentication with Bearer and JWT](https://levelup.gitconnected.com/asp-net-5-authorization-and-authentication-with-bearer-and-jwt-2d0cef85dc5d)