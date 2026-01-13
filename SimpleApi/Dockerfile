FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out
WORKDIR /app/out
EXPOSE 10000
ENTRYPOINT ["dotnet", "SimpleApi.dll"]
