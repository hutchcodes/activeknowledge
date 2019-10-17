FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["AKS.Api.Build/AKS.Api.Build.csproj", "AKS.Api.Build/"]
COPY ["AKS.Infrastructure/AKS.Infrastructure.csproj", "AKS.Infrastructure/"]
COPY ["AKS.Common/AKS.Common.csproj", "AKS.Common/"]
RUN dotnet restore "AKS.Api.Build/AKS.Api.Build.csproj"
COPY . .
WORKDIR "/src/AKS.Api.Build"
RUN dotnet build "AKS.Api.Build.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AKS.Api.Build.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AKS.Api.Build.dll"]