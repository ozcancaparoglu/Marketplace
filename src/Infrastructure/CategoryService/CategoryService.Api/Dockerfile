#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Infrastructure/CategoryService/CategoryService.Api/CategoryService.Api.csproj", "src/Infrastructure/CategoryService/CategoryService.Api/"]
COPY ["src/Infrastructure/CategoryService/CategoryService.Application/CategoryService.Application.csproj", "src/Infrastructure/CategoryService/CategoryService.Application/"]
COPY ["src/Infrastructure/CategoryService/CategoryService.Domain/CategoryService.Domain.csproj", "src/Infrastructure/CategoryService/CategoryService.Domain/"]
COPY ["src/Infrastructure/CommonOperations/CommonOperations.csproj", "src/Infrastructure/CommonOperations/"]
RUN dotnet restore "src/Infrastructure/CategoryService/CategoryService.Api/CategoryService.Api.csproj"
COPY . .
WORKDIR "/src/src/Infrastructure/CategoryService/CategoryService.Api"
RUN dotnet build "CategoryService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CategoryService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CategoryService.Api.dll"]