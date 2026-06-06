FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore EmployeeManagement.sln

RUN dotnet publish EmployeeManagement.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:TreatWarningsAsErrors=false \
    /warnaserror-

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EmployeeManagement.API.dll"]