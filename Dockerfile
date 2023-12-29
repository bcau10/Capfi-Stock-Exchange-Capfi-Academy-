#
## === Build step ===
#
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Prepare sources
WORKDIR /App
COPY . ./

# Compile
RUN dotnet test
RUN dotnet restore && dotnet publish -c Release -o out

#
## === Packaging step ===
#
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Retreive compiled application from previous step
WORKDIR /App
COPY --from=build-env /App/out .

EXPOSE 80

# Configure image default behaviour when started
ENTRYPOINT ["dotnet", "api.dll"]

