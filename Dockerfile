FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS builder
WORKDIR /app1
# COPY Sat.Recruitment.Api/Sat.Recruitment.Api.csproj .
# COPY Application/Application.csproj .
# COPY Domain/Domain.csproj .
# COPY Infraestructure/Infraestructure.csproj .
COPY ./ ./

WORKDIR /app1/Sat.Recruitment.Api/

RUN dotnet restore


RUN dotnet publish -c Release -o ./../build

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app1
COPY --from=builder /app1/build/ ./
ENTRYPOINT ["dotnet", "Sat.Recruitment.Api.dll"]
