#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Catalog/MyAwesomeShop.Catalog.WebApi/MyAwesomeShop.Catalog.WebApi.csproj", "src/Catalog/MyAwesomeShop.Catalog.WebApi/"]
COPY ["src/Shared/MyAwesomeShop.Shared.WebApi/MyAwesomeShop.Shared.WebApi.csproj", "src/Shared/MyAwesomeShop.Shared.WebApi/"]
COPY ["src/Shared/MyAwesomeShop.Shared.Application/MyAwesomeShop.Shared.Application.csproj", "src/Shared/MyAwesomeShop.Shared.Application/"]
COPY ["src/Catalog/MyAwesomeShop.Catalog.Application/MyAwesomeShop.Catalog.Application.csproj", "src/Catalog/MyAwesomeShop.Catalog.Application/"]
COPY ["src/Catalog/MyAwesomeShop.Catalog.Domain/MyAwesomeShop.Catalog.Domain.csproj", "src/Catalog/MyAwesomeShop.Catalog.Domain/"]
COPY ["src/Shared/MyAwesomeShop.Shared/MyAwesomeShop.Shared.csproj", "src/Shared/MyAwesomeShop.Shared/"]
COPY ["src/Shared/MyAwesomeShop.Shared.Domain/MyAwesomeShop.Shared.Domain.csproj", "src/Shared/MyAwesomeShop.Shared.Domain/"]
COPY ["src/Catalog/MyAwesomeShop.Catalog.Infrastructure/MyAwesomeShop.Catalog.Infrastructure.csproj", "src/Catalog/MyAwesomeShop.Catalog.Infrastructure/"]
RUN dotnet restore "src/Catalog/MyAwesomeShop.Catalog.WebApi/MyAwesomeShop.Catalog.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Catalog/MyAwesomeShop.Catalog.WebApi"
RUN dotnet build "MyAwesomeShop.Catalog.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyAwesomeShop.Catalog.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyAwesomeShop.Catalog.WebApi.dll"]