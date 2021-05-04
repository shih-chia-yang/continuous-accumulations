# architecture

- monolithic applications usually have a presentation layer(ui-user interface),a business layer(bl-business layer,application logic),and a data access layer

- monolithic systems can be scale with more cpu,more memory , or replacing current server with more power full new server which is called scale up

- monolithic arch pros

1. it is simple to develop
2. it is simple to deploy
3. it is simple to test
4. it is simple to scale horizontally by running multiple copies behind a load balancer.
5. it may cost less to develop and maintain

- monolithic arch cons
1. it has a limitation in size and complexity
2. difficult to understand and change when the application becomes too large and too complex
3. impact of a change is usually not very well understood which leads to do extensive manual testing
4. it is tightly coupled; entire application must have redeployed on each update
5. the continuous deployment became difficult
6. it can be difficult to scale with different modules have conflicting resource requirements
7. it can be less reliable. bug in any module can potentially bring down the entire application
8. it may have a barrier to adopting new technologies. since changes in frameworks or languages will affect an entire application it is extremely expensive in both time and cost
9. unit testing for a large monolithic code-based application can take time, and regression testing be QA also a very time-consuming

- microservice is an approach of building large enterprise system/platform with multiple small unit called service, each service develops, deploy and test individually

- microservice arch pros
1. scalability can be easy to scale based on demand
2. scale independently. each service can be deployed independently of other services.
3. works best for handling increased in workload and demand for growth
4. loosely coupled, can be deployed independently
5. simplifying adding new features
6. can be used different technology stack
7. modeled around business domain

- microservice arch cons
1. complexity: development and operational complexity
2. loss of performance: (network latency(網路延遲), fault tolerance(容錯), unreadable network congestion(網路塞車),serialization/deserialization etc)
3. can be unreliable, requires retry loops with exponential backup(備份)/circuit breakers
4. data consistency is more difficult
5. cost more to develop and maintain
