FROM mcr.microsoft.com/dotnet/aspnet:3.1

WORKDIR /app

COPY bin/Debug/netcoreapp3.1/publish .

EXPOSE 4040
ENV ASPNETCORE_URLS=http://*:4040

ENTRYPOINT ["dotnet", "Banking.Api.dll", "--environment=Production"]