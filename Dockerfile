FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY /app/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish ./app -c Release -o out




# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app

RUN apt-get update \
    && apt-get install -y \
        nginx \
        vim \
        curl

COPY --from=build-env /app/app/out .
COPY docker/default /etc/nginx/sites-enabled/
ENTRYPOINT ["dotnet", "app.dll"]