# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 1. Copiar solo los archivos de proyecto y solución primero (esto se cachea bien)
COPY AppTarea.sln .
COPY src/Domain/Domain.csproj ./src/Domain/
COPY src/Application/Application.csproj ./src/Application/
COPY src/Infrastructure/Infrastructure.csproj ./src/Infrastructure/
COPY src/Presentation/Presentation.csproj ./src/Presentation/
COPY src/Tests/Tests.csproj ./src/Tests/


RUN dotnet restore


COPY src/ ./src/


RUN dotnet publish src/Presentation/Presentation.csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80


ENTRYPOINT ["dotnet", "Presentation.dll"]
