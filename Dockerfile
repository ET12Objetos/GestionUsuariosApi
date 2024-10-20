FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /source

COPY . ./

RUN dotnet restore

RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0

EXPOSE 5000

ENV ASPNETCORE_URLS="http://0.0.0.0:5000"

WORKDIR /publish

COPY --from=build-env /publish ./

ENTRYPOINT ["dotnet", "Api.dll"]