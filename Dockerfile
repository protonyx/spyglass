###########################################################
## Build Server
###########################################################
FROM microsoft/dotnet:2.1-sdk as build-api

WORKDIR /app

COPY . ./
RUN ./build.sh --target=Publish-Solution --verbosity=Diagnostic

###########################################################
## Build Angular App
###########################################################
FROM node:8.12.0 as build-ng

WORKDIR /src
COPY ./src/Spyglass.Web/package.json ./
RUN yarn install

COPY ./src/Spyglass.Web ./
RUN npm run build -- --prod --progress false

###########################################################
## Build Final Image
###########################################################
FROM microsoft/dotnet:2.1-aspnetcore-runtime

LABEL author="Protonyx"

WORKDIR /app
COPY --from=build-api /app/dist .
COPY --from=build-ng /src/dist ./wwwroot
ENV ASPNETCORE_URLS http://*:5000

EXPOSE 5000
ENTRYPOINT ["dotnet", "Spyglass.Server.dll"]
