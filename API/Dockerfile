FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 9000

ENV PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin
ENV ASPNETCORE_URLS=http://*:9000/
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_VERSION=6.0.3
ENV ASPNET_VERSION=6.0.3
ENV Logging__Console__FormatterName=
ENV DOTNET_GENERATE_ASPNET_CERTIFICATE=false
ENV DOTNET_NOLOGO=true
ENV DOTNET_SDK_VERSION=6.0.201
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV NUGET_XMLDOC_MODE=skip
ENV POWERSHELL_DISTRIBUTION_CHANNEL=PSDocker-DotnetSDK-Debian-11
ENV ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true
ENV ASPNETCORE_ENVIRONMENT=Development
ENV MONGODB_CONNECTION_STRING=mongodb://inboxapi-mongodb:27017
ENV MONGODB_DATABASE_NAME=EventBusInbox

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EventBusInbox.Api/EventBusInbox.Api.csproj", "EventBusInbox.Api/"]
RUN dotnet restore "./EventBusInbox.Api/EventBusInbox.Api.csproj"
COPY . .
WORKDIR "/src/EventBusInbox.Api"
RUN dotnet build "./EventBusInbox.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EventBusInbox.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app # IMPORTANT!
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventBusInbox.Api.dll"]