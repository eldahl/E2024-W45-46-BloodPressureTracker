# BloodPressureTracker
[![build and test](https://github.com/eldahl/E2024-W45-46-BloodPressureTracker/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/eldahl/E2024-W45-46-BloodPressureTracker/actions/workflows/build-and-test.yml)

## Migrations
### Adding migrations
```
MySQLDbConnection="Server=localhost;Port=3306;Database=[DB_NAME];User=[USER];Password=[PASSWORD]" dotnet ef migrations add "[MIGRATION_NAME]" --context BtpDbContext --project Models/Models.csproj
```
### Removing previous migration
Requires an active database. Start one with:
```
$ docker run --name mysql -e MYSQL_ROOT_PASSWORD=[ROOT_PASSWORD] -e MYSQL_USER=[USER] -e MYSQL_PASSWORD=[USER_PASSWORD] -e MYSQL_DATABASE=[DB_NAME] -d mysql
```
Then:
```
MySQLDbConnection="Server=localhost;Port=3306;Database=[DB_NAME];User=[USER];Password=[PASSWORD]" dotnet ef migrations remove --context BtpDbContext --project Models/Models.csproj
```
In case this proves troublesome, delete the folder `Models/Migrations/` and make a new migration. (Warning... You'll lose previous migrations.)

### Running migrations on system
A docker compose profile is provided that runs a migration runner.    
Run the docker compose with the profile:
```
docker-compose --profile run-migrations up
```
