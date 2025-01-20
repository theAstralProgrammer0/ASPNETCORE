# Learning to Deploy an ASP.NET Core Web App Using Docker 🚀  

## Introduction  

Deploying an ASP.NET Core application is an enriching experience, especially
when leveraging **Docker** for containerization. This post chronicles my
journey of building and deploying a web application using the ASP.NET Core SDK
9.0 Docker container image. Along the way, I navigated Docker, container
networking, persistent storage, and application development—all while learning
the intricacies of ASP.NET Core.  

---

## Step 1: Starting the Journey 🚢  

The journey began with a simple goal:  
- **Use the ASP.NET Core SDK 9.0 Docker container image** to build an ASP.NET
Core application without installing .NET SDK locally.  

This involved a dive into Docker, containerization, and understanding how
Docker applications can be spun up in various ways.  

---

## Step 2: Pulling the ASP.NET Core SDK Docker Image 🐳  

To build the app, I needed the **ASP.NET Core SDK image** from Docker Hub:  
```bash  
docker pull mcr.microsoft.com/dotnet/sdk:9.0  
```  
This image contains all the tools required to build .NET applications. However, 
accessing image layer metadata directly in Docker's filesystem
(`/var/lib/docker`) can corrupt the installation—lesson learned! Instead, I
used:  
- `docker image inspect <container_url>`  
- `docker history <container_url>`  

---

## Step 3: Spinning Up the SDK Container 🌟  

My goals:  
1. Use the SDK in a shell environment for app development.  
2. Configure ports for container-to-host communication.  
3. Persist app data and filesystem changes.  

Initially, I used the direct `docker run` method:  
```bash  
docker run -it -v <host_path>:<container_path> -p <host_port>:<container_port> mcr.microsoft.com/dotnet/sdk:9.0  
```  
But this approach quickly became repetitive and error-prone.  

---

## Step 4: Streamlining with Docker Compose 📜  

I transitioned to using a `docker-compose.yml` file, which automates configurations:  

### My `docker-compose.yml` file:  
```yaml  
services:  
  tutorialapp:  
    image: mcr.microsoft.com/dotnet/sdk:9.0  
    working_dir: /app/aspnetcoreapp  
    ports:  
      - "5070:5070"  
    volumes:  
      - ./:/app  
    environment:  
      - ASPNETCORE_ENVIRONMENT=Development  
      - ASPNETCORE_URLS=http://0.0.0.0:5070  
    command: dotnet run --urls "http://0.0.0.0:5070"  
```  

### Key Elements:  
- **services:** Defines app services.  
- **tutorialapp:** The service name.  
- **image:** The container image to use.  
- **ports:** Maps container ports to host ports.  
- **volumes:** Mounts host directories to the container for persistence.  
- **environment:** Sets environment variables for the container.  
- **command:** Runs commands when the container starts.  

To spin up the container:  
```bash  
docker-compose up --build  
```  

---

## Step 5: Building the Web App 🛠  

With the container ready, I scaffolded an ASP.NET Core web app:  
```bash  
dotnet new webapp --output aspnetcoreapp --no-https  
```  
Next, I navigated to the project directory:  
```bash  
cd aspnetcoreapp  
```  
Finally, I ran the development server:  
```bash  
dotnet run --urls "http://0.0.0.0:5070"  
```  

---

## Step 6: Network Configuration Explained 🌐  

By default, the web app binds to `localhost:5070`, making it inaccessible outside the container. Here's why:  
- `localhost` refers to the container's loopback interface, isolating it from the host.  
- Adding `--urls "http://0.0.0.0:5070"` ensures the server listens on all network interfaces, including the Docker bridge network.  
- Port mapping (`5070:5070`) connects the container's port to the host's port.  

This configuration allows seamless communication between the host and container.  

---

## Final Thoughts 💡  

This learning journey taught me the power of containerization with Docker and the flexibility of ASP.NET Core. By combining `docker-compose` with ASP.NET Core SDK images, I achieved a streamlined workflow for building and running web applications without installing .NET locally.  

For anyone embarking on a similar journey, this approach simplifies deployment while offering opportunities to explore advanced Docker features.  

---

### Stay Tuned!  

This is just the beginning. Follow along as I continue building enterprise-grade applications with ASP.NET Core and Docker.  

Happy coding! 🚀  

--- 
