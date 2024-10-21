FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["SkillProfi.WebApi/SkillProfi.WebApi.csproj", "SkillProfi.WebApi/"]
COPY ["SkillProfi.Application/SkillProfi.Application.csproj", "SkillProfi.Application/"]
COPY ["SkillProfi.Domain/SkillProfi.Domain.csproj", "SkillProfi.Domain/"]
COPY ["SkillProfi.Persistence/SkillProfi.Persistence.csproj", "SkillProfi.Persistence/"]

RUN dotnet restore "SkillProfi.WebApi/SkillProfi.WebApi.csproj"

COPY SkillProfi.Application SkillProfi.Application
COPY SkillProfi.Domain SkillProfi.Domain
COPY SkillProfi.Persistence SkillProfi.Persistence
COPY SkillProfi.WebApi SkillProfi.WebApi

WORKDIR "/src/SkillProfi.WebApi"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 500

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SkillProfi.WebApi.dll"]

