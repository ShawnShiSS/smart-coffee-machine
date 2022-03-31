# Smart Coffee Machine System
Showcase how we can decouple long-running tasks from HTTP request processing in REST API by introducing a distributed system using Azure Service Bus as a message broker to asynchronously handle messages, and Worker Services as message consumers, and Redis to track long-running tasks' status.

## System Design Diagram
<img src="https://github.com/ShawnShiSS/smart-coffee-machine/blob/main/Solution%20Items/System%20Diagram.png" width="100%">

## Network Topology using Azure Service Bus
<img src="https://github.com/ShawnShiSS/smart-coffee-machine/blob/main/Solution%20Items/NetworkTopology.png" width="100%">

## Getting Started
Prerequisites:
1. Azure Service Bus namespace

Development
1. Run "dotnet run" in Web API application to run the REST API.
2. Run "dotnet run --no-build" in Services application to run the consumer instances.

For detailed description on the development journey and how things work, please see resource articles:
* [REST API Best Practices — Decouple Long-running Tasks from HTTP Request Processing](https://medium.com/geekculture/rest-api-best-practices-decouple-long-running-tasks-from-http-request-processing-9fab2921ace8)
* [Decouple Long-running Tasks from HTTP Request Processing — Using In-Memory Message Broker](https://shawn-shi.medium.com/rest-api-best-practices-decouple-long-running-tasks-from-http-request-processing-d6efde658cc7)
* [Decouple Long-running Tasks from HTTP Request Processing — Using Azure Service Bus](https://shawn-shi.medium.com/decouple-long-running-tasks-from-http-request-processing-using-azure-service-bus-3a11605714ee)
* [Decouple Long-running Tasks from HTTP Request Processing — Scalable Consumers](https://shawn-shi.medium.com/decouple-long-running-tasks-from-http-request-processing-scalable-consumers-380e24d662de)
