#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Web_API_Prueba.csproj", "."]
RUN dotnet restore "./Web_API_Prueba.csproj" linux-musl-x64
COPY . .
WORKDIR "/src/."
RUN dotnet build "Web_API_Prueba.csproj" -c Release -o /app/build linux-musl-x64

FROM build AS publish
RUN dotnet publish "Web_API_Prueba.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web_API_Prueba.dll"]