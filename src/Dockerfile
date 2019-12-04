FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env

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

ARG CONNECTION_STRING
ENV CONNECTION_STRING="${CONNECTION_STRING}"

ARG SCRIPT_PATH="/app/src"
ENV SCRIPT_PATH="${SCRIPT_PATH}"

COPY --from=build-env /app/out .
ENTRYPOINT dotnet karl-oskar.dll -dir="${SCRIPT_PATH}/script" -after="${SCRIPT_PATH}/after" -before="${SCRIPT_PATH}/before" --connectionstring="${CONNECTION_STRING}"

