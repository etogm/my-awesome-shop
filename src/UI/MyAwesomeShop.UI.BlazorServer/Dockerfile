#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/UI/MyAwesomeShop.UI.BlazorServer/MyAwesomeShop.UI.BlazorServer.csproj", "src/UI/MyAwesomeShop.UI.BlazorServer/"]
COPY ["src/Shared/MyAwesomeShop.Shared/MyAwesomeShop.Shared.csproj", "src/Shared/MyAwesomeShop.Shared/"]
RUN dotnet restore "src/UI/MyAwesomeShop.UI.BlazorServer/MyAwesomeShop.UI.BlazorServer.csproj"
COPY . .
WORKDIR "/src/src/UI/MyAwesomeShop.UI.BlazorServer"
RUN dotnet build "MyAwesomeShop.UI.BlazorServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyAwesomeShop.UI.BlazorServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyAwesomeShop.UI.BlazorServer.dll"]