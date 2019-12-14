ARG DOTNET_SDK=3.0

###########################################################
## Build Server
###########################################################
FROM mcr.microsoft.com/dotnet/core/sdk:$DOTNET_SDK as build-api

WORKDIR /app

# Workaround for the new sdk images being built with debian 10
ENV GITVERSION_VERSION=5.1.2
ENV LD_LIBRARY_PATH=/app/tools/.store/gitversion.tool/${GITVERSION_VERSION}/gitversion.tool/${GITVERSION_VERSION}/tools/netcoreapp3.0/any/runtimes/debian.9-x64/native/

COPY . ./
RUN ./build.sh --target=Publish-Solution --verbosity=Diagnostic

###########################################################
## Build Angular App
###########################################################
FROM node:10 as build-ng

WORKDIR /src
COPY ./src/Spyglass.Web/package.json ./
RUN yarn install

COPY ./src/Spyglass.Web ./
RUN npm run build -- --prod --progress false

###########################################################
## Build Final Image
###########################################################
FROM mcr.microsoft.com/dotnet/core/aspnet:$DOTNET_SDK

LABEL author="Protonyx"

WORKDIR /app
COPY --from=build-api /app/dist .
COPY --from=build-ng /src/dist ./wwwroot
ENV ASPNETCORE_URLS http://*:5000

EXPOSE 5000
ENTRYPOINT ["dotnet", "Spyglass.Server.dll"]
