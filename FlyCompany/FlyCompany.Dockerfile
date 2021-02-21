FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY . .
RUN dotnet restore FlyCompany.Api/FlyCompany.Api.csproj
WORKDIR /src/FlyCompany.Api
RUN dotnet publish -c Release -o ./output

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /src
COPY --from=build /src/FlyCompany.Api/output .

EXPOSE 80
ENTRYPOINT ["dotnet", "FlyCompany.Api.dll"]