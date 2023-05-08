FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env

# Ensure we listen on any IP Address 
WORKDIR /app

# Copy everything.
COPY ./ ./

# Restore as distinct layer.
RUN dotnet restore ./Sat.Recruitment.Api/Sat.Recruitment.Api.csproj

# Build and publish release.
RUN dotnet publish ./Sat.Recruitment.Api/Sat.Recruitment.Api.csproj -c Release -o out

# Build runtime image.
FROM mcr.microsoft.com/dotnet/sdk:3.1
ENV DOTNET_URLS=http://+:5000
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Sat.Recruitment.Api.dll"]
