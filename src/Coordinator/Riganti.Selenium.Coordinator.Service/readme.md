## Deployment Instructions



The easiest way is to run the service as a container directly on the machine which will run the browser instances.

Currently, everything runs on a virtual machine called `kube-master` with IP address `192.168.88.216`.

### Creating and publishing the container

1. You need to have Docker Tools for Visual Studio and Docker installed.

2. In Docker configuration, you need to allow access to the system volume and the volume with the cloned repo (if they differ).

3. In Docker configuration, add 192.168.88.216:5000 to a list of unsecured registries.

4. Delete the `bin` and `obj` directories from this folder, and from the parent folder.

5. Set the mode to `Release`.

6. Set `docker-compose` as startup project.

7. Rebuild it.

8. Run command line and execute the following command. It will push the image to the local Docker registry on the VM.

```
docker tag seleniumcoordinator.service 192.168.88.216:5000/seleniumcoordinator.service
docker push 192.168.88.216:5000/seleniumcoordinator.service
```

### Deploying the container on the VM 


1. Stop and uninstall previous versions (skip this step for a clean install).


```
docker container stop seleniumcoordinator
docker container rm seleniumcoordinator
```

2. Download the new image from the local Docker repository.

```
docker pull 192.168.88.216:5000/seleniumcoordinator.service
```

3. For a clean install, create the `/var/seleniumcoordinator/appsettings.json` file. You can reuse the configuration file from this repo.

```
mkdir /var/seleniumcoordinator
nano /var/seleniumcoordinator/appsettings.json
```

4. Run the service:

```
docker run -d -p 5001:80 -e "ASPNETCORE_ENVIRONMENT=Development" -v "/var/seleniumcoordinator:/mnt/config" --restart=always --name seleniumcoordinator 192.168.88.216:5000/seleniumcoordinator.service:latest
```


### Notes

I have tried to put nginx as a reverse proxy, but it messes up the SignalR communication.