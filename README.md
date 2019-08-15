# AppMvcCompleta
Aplicação MVC em .NET CORE 2.2

## No projeto DevIO.Data:

## Install
```
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Relational
```


### Comandos para criar migrations:
PowerShell:
```
Add-Migration Initial -Verbose -Context MeuDbContext
```
Console:
```
dotnet ef migrations add Initial --startup-project ..\DevIO.App\ --context MeuDbContext
```
(Para executar este segundo comando, instale também o Microsoft.EntityFrameworkCore.Design)


### Comando para gerar script migration

```
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

PowerShell:
```
Script-Migration -Context MeuDbContext
Script-Migration -Context ApplicationDbContext
```
Console:
```
dotnet ef migrations script --startup-project ..\DevIO.App\ --context MeuDbContext
dotnet ef migrations script --startup-project ..\DevIO.App\ --context ApplicationDbContext
```

### Comando para atualizar o banco de dados
PowerShell:
```
Update-Database -Context ApplicationDbContext
Update-Database -Context MeuDbContext
```
Console:
```
dotnet ef database update --startup-project ..\DevIO.App\ --context ApplicationDbContext
dotnet ef database update --startup-project ..\DevIO.App\ --context MeuDbContext
```
