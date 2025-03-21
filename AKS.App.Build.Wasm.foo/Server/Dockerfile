#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AKS.App.Build.Wasm/Server/AKS.App.Build.Wasm.Server.csproj", "AKS.App.Build.Wasm/Server/"]
COPY ["AKS.App.Build.Wasm/Client/AKS.App.Build.Wasm.Client.csproj", "AKS.App.Build.Wasm/Client/"]
COPY ["AKS.CKEditor/AKS.CKEditor.csproj", "AKS.CKEditor/"]
COPY ["AKS.Api.Build.Client/AKS.Api.Build.Client.csproj", "AKS.Api.Build.Client/"]
COPY ["AKS.Common/AKS.Common.csproj", "AKS.Common/"]
COPY ["AKS.App.Core/AKS.App.Core.csproj", "AKS.App.Core/"]
RUN dotnet restore "AKS.App.Build.Wasm/Server/AKS.App.Build.Wasm.Server.csproj"
COPY . .
WORKDIR "/src/AKS.App.Build.Wasm/Server"
RUN dotnet build "AKS.App.Build.Wasm.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AKS.App.Build.Wasm.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AKS.App.Build.Wasm.Server.dll"]