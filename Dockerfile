FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Task4.MVC/Task4.MVC.csproj", "Task4.MVC/"]
COPY ["Task4.Application/Task4.Application.csproj", "Task4.Application/"]
COPY ["Task4.Domain/Task4.Domain.csproj", "Task4.Domain/"]
COPY ["Task4.Persistence/Task4.Persistence.csproj", "Task4.Persistence/"]
RUN dotnet restore "Task4.MVC/Task4.MVC.csproj"
RUN dotnet restore "Task4.Application/Task4.Application.csproj"
RUN dotnet restore "Task4.Domain/Task4.Domain.csproj"
RUN dotnet restore "Task4.Persistence/Task4.Persistence.csproj"
COPY . .
WORKDIR "/src/Task4.MVC"
RUN dotnet build "Task4.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Task4.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "Task4.MVC.dll"]
