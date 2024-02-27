#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV MONGODB_CONNECTION_STRING "mongodb://localhost:27017"
ENV MONGODB_DATABASE_NAME EventBusInbox

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
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventBusInbox.Api.dll"]