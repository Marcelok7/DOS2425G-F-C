# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Definir a pasta de trabalho dentro do container
WORKDIR /app

# Copiar a solução para a pasta de trabalho do container
COPY Task_Management_System.sln ./

# Copiar os ficheiros de projeto (.csproj) para o container
COPY TMS/*.csproj ./TMS/
COPY XunitTest/*.csproj ./XunitTest/

# Restaurar as dependências da solução
RUN dotnet restore Task_Management_System.sln

# Copiar todos os ficheiros do projeto para a pasta de trabalho do container
COPY . ./

# Publicar o projeto em modo Release para a pasta de saída
RUN dotnet publish Task_Management_System.sln -c Release -o /app/publish

# Etapa de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Definir a pasta de trabalho no ambiente de runtime
WORKDIR /app

# Copiar os ficheiros publicados do estágio de build
COPY --from=build /app/publish .

# Definir o comando de entrada para executar o ficheiro TMS.dll
ENTRYPOINT ["dotnet", "TMS.dll"]