#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Pessoa.Web/Pessoa.Web.csproj", "src/Pessoa.Web/"]
COPY ["src/Pessoa.Fisica.Infraestructure/Pessoa.Fisica.Infraestructure.csproj", "src/Pessoa.Fisica.Infraestructure/"]
COPY ["src/Pessoa.Fisica.Domain/Pessoa.Fisica.Domain.csproj", "src/Pessoa.Fisica.Domain/"]
COPY ["Pessoa.Core/Pessoa.Core.csproj", "Pessoa.Core/"]
COPY ["src/Pessoa.Fisica.Appliation/Pessoa.Fisica.Appliation.csproj", "src/Pessoa.Fisica.Appliation/"]
RUN dotnet restore "src/Pessoa.Web/Pessoa.Web.csproj"
COPY . .
WORKDIR "/src/src/Pessoa.Web"
RUN dotnet build "Pessoa.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pessoa.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Pessoa.Web.dll