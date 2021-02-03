FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY . .
RUN dotnet restore Application.Api/Application.Api.csproj

# Copy everything else and build
WORKDIR /src/Application.Api
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /src
COPY --from=build /src/Application.Api/out .
ENTRYPOINT ["dotnet", "Application.Api.dll"]