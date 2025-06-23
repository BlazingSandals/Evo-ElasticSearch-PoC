Running ES locally for dev purposes: https://www.elastic.co/docs/deploy-manage/deploy/self-managed/local-development-installation-quickstart

* Installed Docker Desktop https://docs.docker.com/desktop/setup/install/windows-install/
* Install Linux subsystem https://learn.microsoft.com/en-us/windows/wsl/install
* Install default distro `wsl --install`
* wsl should have the default `distro  wsl --list --all`

** docker desktop experimental env complete **

 * Reference: 
 https://www.elastic.co/docs/deploy-manage/deploy/self-managed/local-development-installation-quickstart
 
* Install Git Bash https://git-scm.com/downloads or a bash terminal for running ES locally

* Open Git Bash and run `curl -fsSL https://elastic.co/start-local | sh`

Add random test data using
https://github.com/oliver006/elasticsearch-test-data

docker run --rm -it --network host oliver006/es-test-data  \
    --es_url=http://localhost:9200  \
    --batch_size=10000  \
    --username="elastic" \
    --password="EYbDKCoC" 


*** Run App ***
bash StartElasticSearchLocal.sh