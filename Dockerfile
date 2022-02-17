ARG DOTNET_SDK=3.1

###########################################################
## Build Server
###########################################################
FROM mcr.microsoft.com/dotnet/core/sdk:$DOTNET_SDK-buster as build-api

WORKDIR /app

RUN dotnet tool install Cake.Tool --version 2.0.0 --tool-path ./tools

COPY . ./

RUN ./tools/dotnet-cake --target=Publish-Solution --verbosity=Diagnostic

###########################################################
## Build Angular App
###########################################################
FROM node:16 as build-ng

WORKDIR /src
COPY ./web/package.json ./web/package-lock.json ./
RUN npm install

COPY ./web ./
RUN npm run build -- --progress false

###########################################################
## Build Final Image
###########################################################
FROM mcr.microsoft.com/dotnet/core/aspnet:$DOTNET_SDK-buster-slim

LABEL author="Protonyx"

WORKDIR /app
COPY --from=build-api /app/dist .
COPY --from=build-ng /src/dist ./wwwroot

ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000

VOLUME /etc/spyglass

ENTRYPOINT ["dotnet", "Spyglass.Server.dll"]
