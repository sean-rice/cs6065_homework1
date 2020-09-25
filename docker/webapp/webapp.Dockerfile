FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src/
# Copy csproj and restore as distinct layers
COPY "Cs6065_Homework1/Cs6065_Homework1.csproj" "./"
RUN dotnet restore
# Copy everything else
COPY "Cs6065_Homework1" "./"
# install dotnet-ef tool
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef
# create fresh sqlite app.db
RUN dotnet ef database update --configuration Release
# build/publish, release mode
RUN dotnet publish --no-self-contained -c Release -o publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS final
WORKDIR /app
COPY --from=build /src/publish .
ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "Cs6065_Homework1.dll"]
