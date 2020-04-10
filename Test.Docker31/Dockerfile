#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Test.Docker31/Test.Docker31.csproj", "Test.Docker31/"]
RUN dotnet restore "Test.Docker31/Test.Docker31.csproj"
COPY . .
WORKDIR "/src/Test.Docker31"
RUN dotnet build "Test.Docker31.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Test.Docker31.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "Test.Docker31.dll"]