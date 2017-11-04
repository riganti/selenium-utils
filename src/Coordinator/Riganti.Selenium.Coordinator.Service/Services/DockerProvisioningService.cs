using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Riganti.Selenium.Coordinator.Service.Data;

namespace Riganti.Selenium.Coordinator.Service.Services
{
    public class DockerProvisioningService
    {
        private readonly IOptions<AppConfiguration> options;
        private readonly ILogger<DockerProvisioningService> logger;
        private readonly DockerClient client;


        public DockerProvisioningService(IOptions<AppConfiguration> options, ILogger<DockerProvisioningService> logger)
        {
            this.options = options;
            this.logger = logger;

            var config = new DockerClientConfiguration(new Uri(options.Value.DockerApiUrl));
            client = config.CreateClient();
        }


        public async Task<ContainerInfo> StartContainer(string imageName, string browserType, int instanceId)
        {
            var container = await TryFindContainer(browserType, instanceId);

            // delete the container if the image has changed
            if (container != null && container.Image != imageName)
            {
                logger.LogInformation($"Container {browserType}({instanceId} has a different image and will be recreated.");

                await RemoveContainer(browserType, instanceId, container);
                container = null;
            }

            // the container does not exist, create it
            if (container == null)
            {
                await CreateContainer(imageName, browserType, instanceId);
                container = await TryFindContainer(browserType, instanceId);
            }

            // stop the container if it is running
            logger.LogDebug($"The state of the container {container.ID} is '{container.State}'.");
            if (container.State == "running")
            {
                await StopContainer(container.ID);
            }

            // start the container
            await StartContainer(browserType, instanceId, container);

            // retrieve container ports
            container = await TryFindContainer(browserType, instanceId);

            // return container info
            var port = container.Ports.First().PublicPort;
            logger.LogInformation($"The container {browserType}({instanceId}) ID={container.ID} is ready (public port {port}).");

            return new ContainerInfo()
            {
                ContainerId = container.ID,
                Url = string.Format(options.Value.ExternalUrlPattern, port)
            };
        }

        private async Task RemoveContainer(string browserType, int instanceId, ContainerListResponse container)
        {
            try
            {
                logger.LogInformation($"Removing container {browserType}({instanceId}) ID={container.ID}...");

                await client.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters()
                {
                    Force = true
                });

                logger.LogDebug($"Container {container.ID} removed.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error calling Docker API!");
                throw;
            }
        }

        private async Task StartContainer(string browserType, int instanceId, ContainerListResponse container)
        {
            try
            {
                logger.LogInformation($"Starting container {browserType}({instanceId}) ID={container.ID}...");

                await client.Containers.StartContainerAsync(container.ID, null);

                logger.LogDebug($"Container {container.ID} started.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error calling Docker API!");
                throw;
            }
        }

        private async Task CreateContainer(string imageName, string browserType, int instanceId)
        {
            try
            {
                logger.LogInformation($"Creating container {browserType}({instanceId})...");

                var createdContainer = await client.Containers.CreateContainerAsync(new CreateContainerParameters()
                {
                    Image = imageName,
                    Labels = new Dictionary<string, string>()
                    {
                        { "selenium-coordinator-instanceId", instanceId.ToString() },
                        { "selenium-coordinator-browserType", browserType }
                    },
                    ExposedPorts = new Dictionary<string, EmptyStruct>()
                    {
                        { "4444/tcp", new EmptyStruct() }
                    },
                    HostConfig = new HostConfig()
                    {
                        PublishAllPorts = true
                    }
                });

                logger.LogDebug($"Container {browserType}({instanceId} created (ID={createdContainer.ID}).");
                foreach (var warning in createdContainer.Warnings)
                {
                    logger.LogWarning($"Container {createdContainer.ID} warning: " + warning);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error calling Docker API!");
                throw;
            }
        }

        private async Task<ContainerListResponse> TryFindContainer(string browserType, int instanceId)
        {
            logger.LogDebug($"Looking for container {browserType}({instanceId}...");

            try
            {
                var containers = await client.Containers.ListContainersAsync(new ContainersListParameters()
                {
                    All = true,
                    Filters = new Dictionary<string, IDictionary<string, bool>>()
                    {
                        {
                            "label", new Dictionary<string, bool>()
                            {
                                { "selenium-coordinator-instanceId=" + instanceId, true },
                                { "selenium-coordinator-browserType=" + browserType, true }
                            }
                        }
                    }
                });
                var container = containers.FirstOrDefault();

                if (container == null)
                {
                    logger.LogDebug($"Container {browserType}({instanceId}) was not found.");
                }
                else
                {
                    logger.LogDebug($"Container {browserType}({instanceId}) found with ID={container.ID}, state={container.State}.");
                }

                return container;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error calling Docker API!");
                throw;
            }
        }

        public async Task StopContainer(string containerId)
        {
            try
            {
                logger.LogInformation($"Stopping container {containerId}...");

                await client.Containers.StopContainerAsync(containerId, new ContainerStopParameters()
                {
                    WaitBeforeKillSeconds = 10
                },
                CancellationToken.None);

                logger.LogDebug($"Container {containerId} was stopped.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error calling Docker API!");
                throw;
            }
        }

    }
}
