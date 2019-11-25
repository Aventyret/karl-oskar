FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env


ARG CONNECTION_STRING
ENV CONNECTION_STRING="${CONNECTION_STRING}"

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY karl-oskar.csproj ./
RUN dotnet restore

# Copy source files and build
COPY *.cs ./
RUN dotnet publish -c Release -o out

# Prepare runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT dotnet karl-oskar.dll -dir=/app/src/script -after=/app/src/after -before=/app/src/before --connectionstring="${CONNECTION_STRING}"

