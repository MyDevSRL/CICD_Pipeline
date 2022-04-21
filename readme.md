[![Continuous Integration And Deployement](https://github.com/MyDevSRL/CICD_Pipeline/actions/workflows/ci-cd.yaml/badge.svg)](https://github.com/MyDevSRL/CICD_Pipeline/actions/workflows/ci-cd.yaml)

# Test project (https://community.veeam.com/vug-africa-91/veeam-kasten-with-rancher-k3s-wsl-2-first-app-guide-2292)

# Installazione K3S
Aprire una power shell come amministratore e lanciare I seguenti comandi per poter ospitare una vm linux:
- Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux
- wsl --list --online
- wsl --install -d ubuntu-20.04

Creare username e password per l'account di ubuntu. 

# Abilitare systemd come sistema di boot in WSL: 
- sudo -b unshare --pid --fork --mount-proc /lib/systemd/systemd --system-unit=basic.target
- sudo -E nsenter --all -t $(pgrep -xo systemd) runuser -P -l $USER -c "exec $SHELL"
- sudo update-alternatives --set iptables /usr/sbin/iptables-legacy

# Installazione K3S:
	- sudo curl -sfL https://get.k3s.io | sh -

# Check version:
- k3s --version

# Verify binaries:
- k3s check-config

# Launch K3S in background:
- screen -d -m -L -Logfile /var/log/k3s.log k3s server

# Installazione HELM:
- sudo curl https://raw.githubusercontent.com/helm/helm/master/scripts/get-helm-3 | bash


# Creazione kube config file 
- cd /home/giulio
- sudo  mkdir .kube
- sudo chown -R "giulio" /home/giulio/.kube/
- sudo kubectl config view --raw >~/.kube/config

# Installazione Kasten
- sudo helm repo add kasten https://charts.kasten.io/
- sudo kubectl create ns kasten-io
- sudo helm install k10 kasten/k10 --namespace=kasten-io --kubeconfig /etc/rancher/k3s/k3s.yaml
- sudo kubectl get pods --namespace kasten-io --watch (LONG OPERATION)

# APRIRE UN NUOVO TERMINALE UBUNTU

# Installazione applicazione 
- git clone https://github.com/MyDevSRL/CICD_Pipeline

# Creazione namespace e applicazione file yaml
- sudo kubectl create namespace test-pipeline
- sudo kubectl apply -f CICD_Pipeline/deploy/kubernetes/deploy.yaml
- sudo kubectl get pods --namespace test-pipeline --watch

# Port forwarding
- sudo kubectl --namespace kasten-io port-forward service/gateway 8080:8000 &

# App details
- sudo kubectl describe svc <<SERVICE_NAME>> -n test-pipeline

# Management console at:
- http://127.0.0.1:8080/k10/#/dashboard