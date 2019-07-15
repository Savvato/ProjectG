# ProjectG

## What is it

This project represents different system design approaches such as CQRS, Domain Driven Design, Event Driven Design.

## Infrastructure

It consists of the following services:

* Client Service
* Customer Service
  * Read API (based on GraphQL)
  * Write API
  * Database (PostgreSQL)
* Product Service
  * Read API (based on GraphQL)
  * Write API
  * Database (PostgreSQL)
  * Cache Server (Redis)
* Basket Service
  * Read API (based on GraphQL)
  * Write API
  * Database (PostgreSQL)
  * Cache Server (Redis)
* Order Service
  * Read API (based on GraphQL)
  * Write API
  * Database (PostgreSQL, master)
  * Read-Only Database (PostgreSQL, slave)
  * Order Builder Service

![Infrastructure](./images/Infrastructure_Diagram.png)


## Architecture (samples of CQRS, EDD, DDD)

`[CQRS, EDD, DDD]` 

Some databases store data from other services, to keep these data up-to-date some services listen to updates by subscribing on appropriate topics in the Kafka instance. 

For example, `Basket Service` stores product names and descriptions got from `Product Service`. When `Product Service` updates one of own products, it publishes `ProductUpdatedEvent`. 

So, when `Client Service` requests data about customer's basket, it doesn't make an additional request to `Product Service` to get product names and so on.

--------------------

`[CQRS]`

Each service is split on Read API and Write API. 

Read APIs are implemented using GraphQL concept. They handle only `GET` requests.

Write API are REST APIs, they handle only `POST`/`PUT`/`DELETE` requests.

`Order Service` also has two databases with `master-slave` replication. `Order Read API` works with only the read-only database (`slave`), `Order Write API` works with the `master` database.

--------------------

`[DDD]`

In terms of this application, there are the following business contexts:

* Order
* Basket
* Customer
* Product

Each of them is represented by own bounded group of `C#` projects. 



