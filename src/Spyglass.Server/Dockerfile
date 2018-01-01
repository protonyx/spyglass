FROM microsoft/aspnetcore:2
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Spyglass.Server.dll"]
COPY . /app
