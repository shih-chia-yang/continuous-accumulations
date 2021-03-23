# docker-exec

- run a command in a running container

```bash
docker exec [options] container command [arg]
```
- redis practice，使用docker ps 查詢container id後，以bash session方式指定container進入互動模式
```bash
docker exec -it 0564 bash
```

- options
|name,shorthand|default|description|
|--interactive,-i||keep STDIN open even if not attached|
|--tty ,-t|allocate a pseudo-tty|