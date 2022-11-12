FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT="Production"
ENV DB_CONNECTION="Server=ec2-54-174-31-7.compute-1.amazonaws.com;Port=5432;User Id=kcunzosvtnujsg;Password=5e63f8fa70728874154585c3a05711c0a64a21a42d44e7f07e56e2730d383dd4;Database=d5t47vnnron29e;Sslmode=Require;Trust Server Certificate=true;"
ENV PassKey="@Km@1sN3xt"
ENV Key="dd%88*377f6d8fE$$E$FdddFF33fssDG^13"
ENV Audience="KmaisNextAud"
ENV Issuer="KmaisIssuerIss"
ENV Seconds="86400"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./", "."]
RUN dotnet restore "./Api.Application/unitcredit.api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Api.Application/unitcredit.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Api.Application/unitcredit.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#ENTRYPOINT ["dotnet", "unitcredit.api.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet unitcredit.api.dll