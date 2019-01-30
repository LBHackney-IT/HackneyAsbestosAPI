FROM microsoft/dotnet:2.1-sdk AS base
WORKDIR /app
EXPOSE ${PORT:-80}

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY LBHAsbestosAPI.sln ./
COPY LBHAsbestosAPI/LBHAsbestosAPI.csproj LBHAsbestosAPI/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/LBHAsbestosAPI
RUN dotnet build LBHAsbestosAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish LBHAsbestosAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD ASPNETCORE_URLS=http://+:${PORT:-80} dotnet LBHAsbestosAPI.dll
