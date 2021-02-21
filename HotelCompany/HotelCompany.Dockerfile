FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY . .
RUN dotnet restore HotelCompany.Api/HotelCompany.Api.csproj
WORKDIR /src/HotelCompany.Api
RUN dotnet publish -c Release -o ./output

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /src
COPY --from=build /src/HotelCompany.Api/output .

EXPOSE 80
ENTRYPOINT ["dotnet", "HotelCompany.Api.dll"]