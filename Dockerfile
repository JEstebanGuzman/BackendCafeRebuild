# Etapa 1: Build del proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar y restaurar el proyecto
COPY *.csproj ./
RUN dotnet restore

# Copiar todo el código y compilar
COPY . ./
RUN dotnet publish -c Release -o /out

# Etapa 2: Imagen de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "BackendCafe.dll"]

