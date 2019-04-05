FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./app/*.csproj ./
#RUN dotnet restore
COPY ./ ./
RUN ls 

# Copy everything else and build
#COPY . ./
#RUN ls ./
#RUN dotnet publish -c Release -o out
# Build runtime image
#FROM microsoft/dotnet:aspetcore-runtime
#WORKDIR /ap
#COPY --from=build-env /app/out .
#ENTRYPOINT ["dotnet", "yanoui.dll"]