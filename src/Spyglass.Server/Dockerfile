ARG DOTNET_SDK=6.0

FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_SDK AS build
WORKDIR /app

COPY . ./
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_SDK
WORKDIR /app

COPY --from=build /app/out .

ENV ASPNETCORE_URLS http://*:5000

EXPOSE 5000
VOLUME /data

ENTRYPOINT ["dotnet", "Spyglass.Server.dll"]

