#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Product/Product-API/Product-API.csproj", "Product/Product-API/"]
COPY ["Product/Product.Application/Product.Application.csproj", "Product/Product.Application/"]
COPY ["Product/Product.domain/Product.domain.csproj", "Product/Product.domain/"]
COPY ["Product/Product.Infrastructure/Product.Infrastructure.csproj", "Product/Product.Infrastructure/"]
RUN dotnet restore "Product/Product-API/Product-API.csproj"
COPY . .
WORKDIR "/src/Product/Product-API"
RUN dotnet build "Product-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product-API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product-API.dll"]