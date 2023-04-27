#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Sat.Recruitment.Api/Sat.Recruitment.Api.csproj", "src/Sat.Recruitment.Api/"]
COPY ["src/Sat.Recruitment.Application/Sat.Recruitment.Application.csproj", "src/Sat.Recruitment.Application/"]
COPY ["src/Sat.Recruitment.Domain/Sat.Recruitment.Domain.csproj", "src/Sat.Recruitment.Domain/"]
COPY ["src/Sat.Recruitment.Infrastructure/Sat.Recruitment.Infrastructure.csproj", "src/Sat.Recruitment.Infrastructure/"]
RUN dotnet restore "src/Sat.Recruitment.Api/Sat.Recruitment.Api.csproj"
COPY . .
WORKDIR "/src/src/Sat.Recruitment.Api"
RUN dotnet build "Sat.Recruitment.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sat.Recruitment.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sat.Recruitment.Api.dll"]