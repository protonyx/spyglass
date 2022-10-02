FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app

COPY --from=build /app/out .

ENV ASPNETCORE_URLS http://*:5000

EXPOSE 5000
VOLUME /data

ENTRYPOINT ["dotnet", "Spyglass.Server.dll"]

