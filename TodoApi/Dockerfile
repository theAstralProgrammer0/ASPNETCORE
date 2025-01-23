FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet add package Microsoft.EntityFrameworkCore --version 9.0.1
RUN dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.1
RUN dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.1
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "TodoApi.dll"]
