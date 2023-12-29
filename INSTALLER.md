# Capfi Stock Exchange Application Installer

This repository contains the source code and configuration files to build and run the Capfi Stock Exchange application using Docker. Follow the steps below to set up and run the application.
## FYI
The project in this repository is used within the framework of the first cohort of the Capfi Academy to enhance the skills of academists in their fields and to understand the process of industrializing IT solutions. Notably, this project embodies good practices to be implemented.

## Prerequisites

- Docker installed on your machine.

## Getting Started

1. Clone the repository:

    ```bash
    git clone <repository_url>
    cd <repository_directory>
    ```

2. Open the `.gitlab-ci.yml` file and ensure that the Docker image version is up-to-date. Update the `image` field under the `docker` service section if necessary.

    ```yaml
    image: docker:latest # Update this line if needed
    ```

3. Make sure you have the necessary Docker credentials configured in your CI/CD environment for the registry specified in the `docker login` command. Replace `$CI_REGISTRY_USER` and `$CI_REGISTRY_PASSWORD` with your GitLab registry credentials.

## Building the Docker Image

The build process is automated using GitLab CI. The following steps will build the Docker image:

1. Push your changes to trigger the CI/CD pipeline.

2. Visit your GitLab project's CI/CD section to monitor the progress of the pipeline.

3. Once the pipeline is successful, the Docker image will be built and pushed to the specified GitLab Container Registry.

## Running the Capfi Stock Exchange Application

1. After a successful build, the Docker image is available in your GitLab Container Registry.

2. Run the application using the following command:

    ```bash
    docker run --rm registry.gitlab.com/capfi-technology/capfi-academy/all/capfi-stock-exchange:<TIMESTAMP>
    ```

    Replace `<TIMESTAMP>` with the timestamp corresponding to the successful build in your GitLab CI/CD pipeline.

## Additional Information

### Dockerfile Structure

The Dockerfile defines two stages: build and packaging. The build stage uses the Microsoft .NET SDK to compile the application, and the packaging stage creates a lightweight runtime image.

### CI/CD Configuration

The `.gitlab-ci.yml` file configures the CI/CD pipeline with two stages: `build` and `run`. The pipeline is triggered on each push, and the build stage automatically builds and pushes the Docker image to the GitLab Container Registry.

Feel free to customize the configuration files based on your requirements.

For any issues or inquiries, please contact the Capfi Technology team.

Happy coding! ?? CAPAC Team
