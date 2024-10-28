FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SecondSonnet3.5Response.csproj", "./"]
RUN dotnet restore "SecondSonnet3.5Response.csproj"
COPY . .
RUN dotnet build "SecondSonnet3.5Response.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SecondSonnet3.5Response.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecondSonnet3.5Response.dll"]