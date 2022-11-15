#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Infrastructure/AttributeService/AttributeService.Api/AttributeService.Api.csproj", "src/Infrastructure/AttributeService/AttributeService.Api/"]
COPY ["src/Infrastructure/AttributeService/AttributeService.Application/AttributeService.Application.csproj", "src/Infrastructure/AttributeService/AttributeService.Application/"]
COPY ["src/Infrastructure/AttributeService/AttributeService.Domain/AttributeService.Domain.csproj", "src/Infrastructure/AttributeService/AttributeService.Domain/"]
RUN dotnet restore "src/Infrastructure/AttributeService/AttributeService.Api/AttributeService.Api.csproj"
COPY . .
WORKDIR "/src/src/Infrastructure/AttributeService/AttributeService.Api"
RUN dotnet build "AttributeService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AttributeService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AttributeService.Api.dll"]