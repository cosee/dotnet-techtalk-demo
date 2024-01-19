FROM mcr.microsoft.com/dotnet/aspnet:8.0

COPY CandyBackend/bin/Release/net8.0/publish/ App/

WORKDIR /App
ENTRYPOINT ["dotnet", "CandyBackend.dll"]

EXPOSE 80