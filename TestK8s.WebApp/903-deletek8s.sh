#!/bin/sh
# delete k8s
# docker ps | grep testk
kubectl delete -f ./k8sfiles/testv1_host.yaml
kubectl delete configmap testv1-file-config -n testv1
kubectl delete -f ./k8sfiles/testv1_config.yaml
kubectl delete -f ./k8sfiles/namespace.yaml
