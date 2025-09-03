dotnet sonarscanner begin /k:"ambev-developer-evaluation-api" /d:sonar.host.url="http://localhost:9000" /d:sonar.token="sqp_b873b23b74221d27d1541fd6325d810fde240dcd"
dotnet build Ambev.DeveloperEvaluation.sln
dotnet sonarscanner end /d:sonar.token="sqp_b873b23b74221d27d1541fd6325d810fde240dcd"

pause
