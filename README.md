# resilience-server
Simple HTTP-server with endpoints for testing resilience libraries.

## Installation
Resilience Server is available as a Docker image on Docker Hub `eirikjd/resilience-server` .

Pull image:
```
docker pull eirikjd/resilience-server
```

## Starting
To start Resilience Server, just fire up the Docker image. The server is exposed on port 80.
```
docker run -p 5002:80 eirikjd/resilience-server
```

## Endpoints

### Swagger
Resilience Server is documented on Open API standard with swagger. A swagger UI is available at `/swagger`.

### `/resilience/stable`
This endpoint will always succeed and respond quickly.

### `/resilience/mightfail`
This endpoint will occationaly fail. The default failure rate is set to `0.3` (will fail on average 30% of all calls).

### `/resilience/waitforit`
This endpoint always returns an successful result, but it takes a while to respond. The respons time is random based on a normal distribution. Default mean is `5.0` seconds, and default standard deviation is `1.8`.

### `/resilience/fragile`
This endpoint will occationaly fail, and when it fails it will be unavailable for some time. When it is unavailable, it will also remain unavailable and respond slower if it is continued to be called. 