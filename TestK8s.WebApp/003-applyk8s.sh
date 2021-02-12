#!/bin/sh
# apply k8s
# kubectl get pod -n testv1
# kubectl describe pod -n testv1
# kubectl describe pod -n testv1 -l app=host
# kubectl logs -f -n testv1 -l app=host
kubectl apply -f ./k8sfiles/namespace.yaml
kubectl apply -f ./k8sfiles/testv1_host.yaml
