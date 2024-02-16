# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Quiz.sln", "./"]
COPY ["src/Quiz.Api/Quiz.Api.csproj", "src/Quiz.Api/"]
COPY ["src/Quiz.Bll/Quiz.Bll.csproj", "src/Quiz.Bll/"]
COPY ["src/Quiz.Dal/Quiz.Dal.csproj", "src/Quiz.Dal/"]
COPY ["src/Quiz.UnitTest/Quiz.UnitTests.csproj", "src/Quiz.UnitTest/"]


RUN dotnet restore "Quiz.sln"

COPY . .

WORKDIR "/src/src/Quiz.Api"
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 5075
ENV ASPNETCORE_URLS=http://+:5075

ENTRYPOINT ["dotnet", "Quiz.Api.dll"]
