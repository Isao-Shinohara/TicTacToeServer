FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY TicTacToeServer/*.csproj ./TicTacToeServer/
RUN dotnet restore

# copy everything else and build app
COPY TicTacToeServer/. ./TicTacToeServer/
WORKDIR /app/TicTacToeServer
RUN dotnet publish -c Debug -o out


FROM microsoft/dotnet:2.1-runtime AS runtime
WORKDIR /app
COPY --from=build /app/TicTacToeServer/server.pfx ./
COPY --from=build /app/TicTacToeServer/out ./
ENTRYPOINT ["dotnet", "TicTacToeServer.dll"]