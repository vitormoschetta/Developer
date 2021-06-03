# Developer
Aplicação para documentar o desenvolvimento de software.


## Migrations

```
dotnet ef migrations add inity --project Developer.csproj
``` 

## Database 

Aplicar migrations (após execução do docker-compose):

```
dotnet ef database update
```