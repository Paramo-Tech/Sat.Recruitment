FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /src
COPY src .
RUN dotnet restore ../src/Sat.Recruitment.Api

RUN dotnet publish ../src/Sat.Recruitment.Api -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Sat.Recruitment.Api.dll"]