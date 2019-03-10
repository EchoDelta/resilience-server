FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ResilienceServer.Web.csproj", "."]
RUN dotnet restore "ResilienceServer.Web.csproj"
COPY . .
RUN dotnet build "ResilienceServer.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ResilienceServer.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ResilienceServer.Web.dll"]