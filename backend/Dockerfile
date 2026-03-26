# Multi-stage build for FoodDelivery API
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/FoodDelivery.HttpApi.Host/FoodDelivery.HttpApi.Host.csproj", "src/FoodDelivery.HttpApi.Host/"]
COPY ["src/FoodDelivery.HttpApi/FoodDelivery.HttpApi.csproj", "src/FoodDelivery.HttpApi/"]
COPY ["src/FoodDelivery.Application/FoodDelivery.Application.csproj", "src/FoodDelivery.Application/"]
COPY ["src/FoodDelivery.Application.Contracts/FoodDelivery.Application.Contracts.csproj", "src/FoodDelivery.Application.Contracts/"]
COPY ["src/FoodDelivery.Domain/FoodDelivery.Domain.csproj", "src/FoodDelivery.Domain/"]
COPY ["src/FoodDelivery.Domain.Shared/FoodDelivery.Domain.Shared.csproj", "src/FoodDelivery.Domain.Shared/"]
COPY ["src/FoodDelivery.EntityFrameworkCore/FoodDelivery.EntityFrameworkCore.csproj", "src/FoodDelivery.EntityFrameworkCore/"]
COPY ["common.props", "."]
RUN dotnet restore "src/FoodDelivery.HttpApi.Host/FoodDelivery.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/FoodDelivery.HttpApi.Host"
RUN dotnet build "FoodDelivery.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodDelivery.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodDelivery.HttpApi.Host.dll"]
