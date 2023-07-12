#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Basket/MyAwesomeShop.Basket.WebApi/MyAwesomeShop.Basket.WebApi.csproj", "src/Basket/MyAwesomeShop.Basket.WebApi/"]
COPY ["src/Shared/MyAwesomeShop.Shared/MyAwesomeShop.Shared.csproj", "src/Shared/MyAwesomeShop.Shared/"]
RUN dotnet restore "src/Basket/MyAwesomeShop.Basket.WebApi/MyAwesomeShop.Basket.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Basket/MyAwesomeShop.Basket.WebApi"
RUN dotnet build "MyAwesomeShop.Basket.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyAwesomeShop.Basket.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyAwesomeShop.Basket.WebApi.dll"]