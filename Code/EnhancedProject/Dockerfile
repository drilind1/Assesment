FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EnhancedProject.csproj", "./"]
RUN dotnet restore "EnhancedProject.csproj"
COPY . .
RUN dotnet build "EnhancedProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EnhancedProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnhancedProject.dll"]