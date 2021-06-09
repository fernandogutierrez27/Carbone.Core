##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
RUN apt-get install --yes curl
RUN curl --silent --location https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get install --yes nodejs
RUN apt-get install --yes build-essential
RUN apt-get install --yes wget
WORKDIR /tmp
RUN wget http://downloadarchive.documentfoundation.org/libreoffice/old/6.3.3.1/deb/x86_64/LibreOffice_6.3.3.1_Linux_x86-64_deb.tar.gz -O libo.tar.gz
RUN apt update
RUN apt install -y libxinerama1 libfontconfig1 libdbus-glib-1-2 libcairo2 libcups2 libglu1-mesa libsm6 unzip
RUN tar -zxvf libo.tar.gz
WORKDIR LibreOffice_6.3.3.1_Linux_x86-64_deb/DEBS
RUN dpkg -i *.deb
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Carbone.Core/Carbone.Core.csproj", "Carbone.Core/"]
RUN dotnet restore "Carbone.Core/Carbone.Core.csproj"
COPY . .
WORKDIR "/src/Carbone.Core"
RUN dotnet build "Carbone.Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Carbone.Core.csproj" -c Release -o /app/publish

FROM node:10-alpine as build-node
WORKDIR /app/Script
COPY Carbone.Core/Script/ .
RUN npm install

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build-node /app/Script /app/Script
ENTRYPOINT ["dotnet", "Carbone.Core.dll"]