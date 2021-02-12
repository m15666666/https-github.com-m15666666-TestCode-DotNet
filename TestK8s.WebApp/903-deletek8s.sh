#!/bin/sh
# delete k8s
# docker ps | grep testk
kubectl delete -f ./k8sfiles/testv1_host.yaml
kubectl delete -f ./k8sfiles/namespace.yaml
