FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY EmployeeManagement.sln .
COPY EmployeeManagement.API.csproj EmployeeManagement.API/
COPY EmployeeManagement.Domain/EmployeeManagement.Domain.csproj EmployeeManagement.Domain/
COPY EmployeeManagement.Application/EmployeeManagement.Application.csproj EmployeeManagement.Application/
COPY EmployeeManagement.Infrastructure/EmployeeManagement.Infrastructure.csproj EmployeeManagement.Infrastructure/

RUN dotnet restore EmployeeManagement.sln

COPY . EmployeeManagement.API/
COPY EmployeeManagement.Domain/ EmployeeManagement.Domain/
COPY EmployeeManagement.Application/ EmployeeManagement.Application/
COPY EmployeeManagement.Infrastructure/ EmployeeManagement.Infrastructure/

RUN dotnet publish EmployeeManagement.API/EmployeeManagement.API.csproj \
    -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeManagement.API.dll"]