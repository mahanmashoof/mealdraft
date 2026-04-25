# Stage 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY MealDraft.API/ MealDraft.API/

RUN dotnet restore MealDraft.API/MealDraft.API.csproj
RUN dotnet publish MealDraft.API/MealDraft.API.csproj -c Release -o /app/publish

# Stage 2 — Run
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "MealDraft.API.dll"]