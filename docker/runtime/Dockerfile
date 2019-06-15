FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./app/*.csproj ./

COPY ./app/NuGet.config ./
COPY ./app/Directory.Build.props ./
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
        net-tools \
        vim \
        curl \
        iputils-ping \
        wget \
        mysql-client
RUN wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb
COPY --from=build-env /app/app/out .
COPY docker/default /etc/nginx/sites-enabled/
COPY docker/run.sh /usr/local/bin/runner
RUN chmod +x /usr/local/bin/runner
ENTRYPOINT ["dotnet", "app.dll"]
#RUN runner
