FROM microsoft/aspnetcore:2
LABEL author="Protonyx"
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Spyglass.Server.dll"]
COPY ./bin/Debug/netcoreapp2.0/publish /app
