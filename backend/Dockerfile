FROM --platform=${BUILDPLATFORM} mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM --platform=${BUILDPLATFORM} mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
WORKDIR /src

COPY ["src/Management.Service.Domain/Management.Service.Domain.csproj", "Management.Service.Domain/"]
COPY ["src/Management.Service.Api/Management.Service.Api.csproj", "Management.Service.Api/"]
COPY ["src/Management.Service.Infrastructure/Management.Service.Infrastructure.csproj", "Management.Service.Infrastructure/"]

RUN dotnet restore "Management.Service.Api/Management.Service.Api.csproj" --arch ${TARGETARCH}
COPY src/. .
RUN dotnet build "Management.Service.Api/Management.Service.Api.csproj" -c Release -o /app/build

WORKDIR "/src/Management.Service.Api"
FROM build AS publish
RUN dotnet publish "Management.Service.Api.csproj" --arch ${TARGETARCH}  -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Management.Service.Api.dll"]