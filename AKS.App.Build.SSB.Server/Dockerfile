#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AKS.App.Build.SSB.Host/AKS.App.Build.SSB.Host.csproj", "AKS.App.Build.SSB.Host/"]
RUN dotnet restore "AKS.App.Build.SSB.Host/AKS.App.Build.SSB.Host.csproj"
COPY . .
WORKDIR "/src/AKS.App.Build.SSB.Host"
RUN dotnet build "AKS.App.Build.SSB.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AKS.App.Build.SSB.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AKS.App.Build.SSB.Host.dll"]