# TicTacToeServer

### Build container image with docker
```
docker login
docker build -t isaoshinohara/tictactoeserver:1 .
docker push isaoshinohara/tictactoeserver:1
```

### Deploy application to Kubernetes cluster
```
kubectl apply -f kubernetes/development/
```
