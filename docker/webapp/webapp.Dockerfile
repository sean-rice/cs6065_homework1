FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src/
# Copy csproj and restore as distinct layers
COPY "Cs6065_Homework1/Cs6065_Homework1.csproj" "./"
RUN dotnet restore
# Copy everything else and build
COPY "Cs6065_Homework1" "./"
RUN dotnet publish --no-self-contained -c Release -o publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS final
WORKDIR /app
COPY --from=build /src/publish .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "Cs6065_Homework1.dll"]
