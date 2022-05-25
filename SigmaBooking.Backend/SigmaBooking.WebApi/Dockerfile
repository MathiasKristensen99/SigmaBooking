FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY ./bin/Debug/net6.0 /app/publish
ENTRYPOINT ["dotnet", "/app/publish/SigmaBooking.WebApi.dll"]