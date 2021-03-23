# docker-run

- run a command in new container

## command example
Publish or expose port (-p, --expose)

```bash
docker run -p 127.0.0.1:80:8080/tcp ubuntu bash
```

This binds port 8080 of the container to TCP port 80 on 127.0.0.1 of the host machine

Note that ports which are not bound to the host (i.e., -p 80:80 instead of -p 127.0.0.1:80:80) will be accessible from the outside. This also applies if you configured UFW to block this specific port, as Docker manages his own iptables rules.

- -d
```bash
docker container run -p 6379:6379 -t --name=redis-python -d redis
```

- --name
```bash
docker container run -p 6379:6379 -t --name=redis-python  redis
```
- --rm
```bash
docker run -it --rm -p 8888:8888 jupyter/datascience-notebook
```

- Options

|name,shorthand|default|description|
|--|--|--|
|--detach,-d|| run container in background and print container id|
|--name| | assign a name to the container|
|--publish , -p| |Publish a container's port(s) to the host|
|--publish-all| | Publish all exposed ports to random ports|
|--rm||Automatically remove the container when it exits|


