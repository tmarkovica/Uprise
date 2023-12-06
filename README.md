# REST API for monitoring and managing solar power plant.

## https://www.youtube.com/watch?v=DBI59f56L5I&t=1075s&ab_channel=ScriptBytes

## .NET Core CLI
https://learn.microsoft.com/en-us/ef/core/cli/dotnet

## Migrations
To create migration. -p to navigate to folder where is solution.
`dotnet ef migrations add MigrationName --context DbContextClass -p .`
Delte Migration
`dotnet ef migrations remove`
Update database. -v to log
`dotnet ef database upddate -v`


## My DbContexts
`dotnet ef migrations add MigrationUpriseSchema --context UpriseDbContext -p . -v`
`dotnet ef migrations add MigrationPowerPlantSchema --context PowerPlantDbContext -p . -v`

`dotnet ef database update --context UpriseDbContext -v`
`dotnet ef database update --context PowerPlantDbContext -v`