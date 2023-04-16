FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HeadHunter.Application/HeadHunter.Application.csproj", "HeadHunter.Application/"]
RUN dotnet restore "HeadHunter.Application/HeadHunter.Application.csproj"
COPY . .
WORKDIR "/src/HeadHunter.Application"
RUN dotnet build "HeadHunter.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HeadHunter.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HeadHunter.Application.dll"]
