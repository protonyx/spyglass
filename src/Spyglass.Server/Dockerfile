FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
LABEL author="Protonyx"
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Spyglass.Server.dll"]
COPY ./bin/Debug/netcoreapp3.0 /app
