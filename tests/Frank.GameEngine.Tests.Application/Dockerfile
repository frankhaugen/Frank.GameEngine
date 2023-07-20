FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Frank.GameEngine.Tests.Application/Frank.GameEngine.Tests.Application.csproj", "Frank.GameEngine.Tests.Application/"]
RUN dotnet restore "Frank.GameEngine.Tests.Application/Frank.GameEngine.Tests.Application.csproj"
COPY . .
WORKDIR "/src/Frank.GameEngine.Tests.Application"
RUN dotnet build "Frank.GameEngine.Tests.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Frank.GameEngine.Tests.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Frank.GameEngine.Tests.Application.dll"]
