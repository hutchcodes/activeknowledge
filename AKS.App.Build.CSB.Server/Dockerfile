#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AKS.App.Build.CSB.Server/AKS.App.Build.CSB.Server.csproj", "AKS.App.Build.CSB.Server"]
COPY ["AKS.App.Build.CSB/AKS.App.Build.CSB.csproj", "AKS.App.Build.CSB"]
COPY ["AKS.CKEditor/AKS.CKEditor.csproj", "AKS.CKEditor/"]
COPY ["AKS.Api.Build.Client/AKS.Api.Build.Client.csproj", "AKS.Api.Build.Client/"]
COPY ["AKS.Common/AKS.Common.csproj", "AKS.Common/"]
COPY ["AKS.App.Build/AKS.App.Build.csproj", "AKS.App.Build/"]
COPY ["AKS.App.Core/AKS.App.Core.csproj", "AKS.App.Core/"]
RUN dotnet restore "AKS.App.Build.Wasm/Server/AKS.App.Build.CSB.Server.csproj"
COPY . .
WORKDIR "/src/AKS.App.Build.CSB.Server"
RUN dotnet build "AKS.App.Build.CSB.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AKS.App.Build.CSB.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AKS.App.Build.CSB.Server.dll"]