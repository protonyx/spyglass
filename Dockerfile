ARG DOTNET_SDK=6.0

###########################################################
## Build Server
###########################################################
FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_SDK as build-api

WORKDIR /app

RUN dotnet tool install Cake.Tool --version 2.0.0 --tool-path ./tools

COPY . ./

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages ./tools/dotnet-cake --target=Publish-Solution --verbosity=Diagnostic

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
FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_SDK

LABEL author="Protonyx"

WORKDIR /app
COPY --from=build-api /app/dist .
COPY --from=build-ng /src/dist ./wwwroot

ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000

VOLUME /data

ENTRYPOINT ["dotnet", "Spyglass.Server.dll"]
