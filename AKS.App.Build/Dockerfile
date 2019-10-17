FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["AKS.Api.Build.Client/AKS.Api.Build.Client.csproj", "AKS.Api.Build.Client/"]
COPY ["AKS.App.Core/AKS.App.Core.csproj", "AKS.App.Core/"]
COPY ["AKS.App.Build/AKS.App.Build.csproj", "AKS.App.Build/"]
RUN dotnet restore "AKS.App.Build/AKS.App.Build.csproj"
COPY . .
WORKDIR "/src/AKS.App.Build"
RUN dotnet build "AKS.App.Build.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AKS.App.Build.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AKS.App.Build.dll"]
