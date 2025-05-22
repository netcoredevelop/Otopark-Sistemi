FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY WebUI/bin/Release/net8.0 .
VOLUME ["/app/wwwroot"]
ENTRYPOINT ["dotnet", "WebUI.dll"]