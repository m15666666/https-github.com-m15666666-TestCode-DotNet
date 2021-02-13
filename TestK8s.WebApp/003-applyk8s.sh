#!/bin/sh
# apply k8s
# kubectl get pod -n testv1
# kubectl get configmap -n testv1
# kubectl describe pod -n testv1
# kubectl describe StatefulSet -n testv1
# kubectl describe service -n testv1
# kubectl describe pod -n testv1 -l app=host
# kubectl delete pod -n testv1 -l app=host
# kubectl logs -f -n testv1 -l app=host
kubectl apply -f ./k8sfiles/namespace.yaml
kubectl apply -f ./k8sfiles/testv1_config.yaml
kubectl create configmap testv1-file-config -n testv1 --from-file ./appsettings.json
kubectl apply -f ./k8sfiles/testv1_host.yaml
