#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 9001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["banco-api.csproj", "."]
COPY ["../banco-dao/banco-dao.csproj", "../banco-dao/"]
COPY ["../banco-dto/banco-dto.csproj", "../banco-dto/"]
RUN dotnet restore "./banco-api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "banco-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "banco-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "banco-api.dll"]
