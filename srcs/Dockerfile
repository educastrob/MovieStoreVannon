FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MovieStore.Web/MovieStore.Web.csproj", "MovieStore.Web/"]
COPY ["MovieStore.Data/MovieStore.Data.csproj", "MovieStore.Data/"]
COPY ["MovieStore.Services/MovieStore.Services.csproj", "MovieStore.Services/"]
RUN dotnet restore "MovieStore.Web/MovieStore.Web.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "MovieStore.Web/MovieStore.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieStore.Web/MovieStore.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "MovieStore.Web.dll"]
