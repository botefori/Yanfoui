FROM microsoft/dotnet:sdk AS build-env
ENV DOTNET_USE_POLLING_FILE_WATCHER 1
RUN export ASPNETCORE_ENVIRONMENT=development
WORKDIR /app

COPY ./app/*.csproj ./
COPY ./app/NuGet.config ./
COPY ./app/Directory.Build.props ./
RUN dotnet restore

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
COPY docker/default /etc/nginx/sites-enabled/
COPY docker/run.sh /usr/local/bin/runner
RUN chmod +x /usr/local/bin/runner
ENTRYPOINT dotnet watch run --urls=http://+:5000