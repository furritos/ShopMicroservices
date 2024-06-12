# eShop Microservices

## Developer's Note

1.) Rename (or create) a src folder where all Microservices will live

2.) When building Microservices, this is the template we will use when assigning ports.  There are two ports being set, HTTP and HTTPS.  This is all mandatory, suggested port numbers are just that, suggested:

| Microservices | Local Environment | Docker Environment | Docker Inside |
|---------------|-------------------|--------------------|---------------|
| Catalog       | 5000, 5050        | 6000, 6060         | 8080, 8081    |
| Basket        | 5001, 5051        | 6000, 6061         | 8080, 8081    |
| Service 3     | 5002, 5052        | 6000, 6062         | 8080, 8081    |
| Service 4     | 5003, 5053        | 6000, 6063         | 8080, 8081    |

