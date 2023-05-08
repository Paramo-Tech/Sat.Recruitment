# Sat.Recruitment

Code challenge from Paramo Technologies.

## Refactors
The API now supports both TextFile and SQLServer to persist the information.

To configure this, at the `appsettings.json` file, the `PersistenceOptions`must be configured as it follow for:

- TextFile
```json
  "PersistenceOptions": {
    "Type": 0
  }
```

- SQLServer
```json
{
  "PersistenceOptions": {
    "Type": 1
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_connection_string"
  }
}
```

## Docker support
A dockerfile and docker-compose were added to run the application with docker. To run this, Docker Desktop is required (in Windows). 

### How to run in Docker

1. Open a new terminal (Powershell, WSL2 or Gitbash preferrably).
2. Go to the root of the project where both `dockerfile` and `docker-compose.yml` are located.
3. Run `docker compose up`


