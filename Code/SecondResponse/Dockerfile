# Use official .NET runtime image as a base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .

# Restore dependencies
RUN dotnet restore "SecondResponse.csproj"

# Build the application
RUN dotnet publish "SecondResponse.csproj" -c Release -o /app/publish

# Final stage / runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ArithmeticApi.dll"]
