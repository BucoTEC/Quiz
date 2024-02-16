FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5075

ENV ASPNETCORE_URLS=http://+:5075

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Quiz.sln", "Quiz.sln"]
COPY ["src/Quiz.Api/Quiz.Api.csproj", "src/Quiz.Api/"]
COPY ["src/Quiz.Bll/Quiz.Bll.csproj", "src/Quiz.Bll/"]
COPY ["src/Quiz.Dal/Quiz.Dal.csproj", "src/Quiz.Dal/"]

RUN dotnet restore "Quiz.sln"
COPY . .
WORKDIR "/src/src/Quiz.Api"
RUN dotnet build -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Quiz.Api.dll"]
