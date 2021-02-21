FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY . .
RUN dotnet restore AccountCompany.Api/AccountCompany.Api.csproj
WORKDIR /src/AccountCompany.Api
RUN dotnet publish -c Release -o ./output

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /src
COPY --from=build /src/AccountCompany.Api/output .

EXPOSE 80
ENTRYPOINT ["dotnet", "AccountCompany.Api.dll"]