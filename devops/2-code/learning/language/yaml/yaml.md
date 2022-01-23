# yaml tutorial

YAML is a **Data Serialization Language**

configuration files all written in YAML 
for different DevOps tools and applications

- docker
- kubernetes
- ansible
- prometheus

## 特點

- human readable and intuitive
- line separation
- indentation

## syntax

- key-value pair

    ````yaml
        app: user-authentication
        port: 9000
        version: 1.7
    ````
- comments
    ````yaml
    # some comments
    app: user-authentication
    port: 9000
    # some comments
    version: 1.7
    ````
- object

    ````yaml
    microservice:
        app: user-authentication
        port: 9000
        version: 1.7
    ````
- lists
    > 使用 `-`
    ````yaml
    microservices:
        - app: user-authentication
          port: 9000
          version: 1.7
        - app: shopping-cart
          port: 9002
          version: 1.9
    ````
- boolean
    ````yaml
    microservices:
        - app: user-authentication
          port: 9000
          version: 1.7
          deployed: true
        - app: shopping-cart
          port: 9002
          version: 1.9
    ````
- multi line strings
    - 分行呈現 `|`
    ````yaml
    multilinestring: |
        this is a multiline string
        and this is the next time
        next time
    ````
    - 合併為同一行 `>`
    ````yaml
    multilinestring: >
        this is a single line string,
        and this is the next time.
        next time
    ````
    - command attribute
    ````yaml
    command:
        - sh
        - -c
        - |
            #!/usr/bin/env bash -e
            http(){
                local path="${1}"
                set -- -XGET -s --fail
                # some more stuff here
                curl -k "$@" "http://localhost:5601${path}
            }
            http "/app/kibana"
    ````
- env variables

````yaml
command:
    - /bin/sh
    - -ec
    - >
        mysql -h 127.0.0.1 -u root -p$MYSQL_ROOT_PASSWORD -e "SELECT 1"
````

- placeholder
    > the {{}} can be use replace template generator
````yaml
api version: v1
kind: service
meatadata:
    name: {{.Values.service.name}}
spec:
    selector:
        app: {{.Values.service.app}}
    ports:
        - protocol: TCP
        - port: {{.Values.service.port}}
        - targetport: {{.Values.service.targetport}}
````

- multiple yaml documents
    > use `---`
````yaml
apiversion: v1
kind: ConfigMap
meatadata:
    name: mosquitto-config-file
# some stuff
---
apiversion: v1
kind: secret
meatadata:
    name: mosquitto-secret-file
type: oqaque
# some stuff
````