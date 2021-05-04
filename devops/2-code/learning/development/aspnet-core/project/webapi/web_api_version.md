# web_api_version

1. Uri-based (URL path segment) versioning
- GET /api/Items HTTP1.1
- Get /api/v2/Items HTTP1.1


2. Header-based (HTTP Header/Custom Header) versioning
- GET /api/Items HTTP1.1
- GET /api/Items HTTP1.1
ApiVersion:2

3. Query String Parameter versioning
- .../api/hi?api-version=2.0


4. Media type-based versioning
- Get /api/Items HTTP1.1
- Accept:application/vnd.smartit.esale.webapi+json
- Get /api/Items HTTP1.1
- Accept:application/vnd.smartit.esale.webapi-v2+json

在`ASP.NET Core Web API`採用`URI-based`與不同namespace的controllers做為版本控制。
以下為優點：
GET/api/v1/Hi
GET/api/v2/Hi

1. most common method that currently is in use by APIs today
2. easy to implement with the code
3. easy to consume
4. allows exploring different version with just a browser
5. fast performance, no overhead of search or switch statement needed
6. can be add to any old, not versioned or versioned REST API project
7. no extra configuration needed
8. extremely easy to test

缺點
1. it disrupts the RESTful Compliance:URIs should represent resources and not versions(one URL: one resource/resource version)


## complete REST API versioning for this example

1. create the sample project from
2. add API version 1 controller
3. add API version 2 controller
4. test and verify the v1 and v2 of APIs


