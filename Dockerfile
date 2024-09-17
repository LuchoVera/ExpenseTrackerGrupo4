FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5183

ENV ASPNETCORE_URLS=http://+:5183

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["ExpenseTrackerGrupo4.csproj", "./"]
RUN dotnet restore "ExpenseTrackerGrupo4.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ExpenseTrackerGrupo4.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "ExpenseTrackerGrupo4.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpenseTrackerGrupo4.dll"]
