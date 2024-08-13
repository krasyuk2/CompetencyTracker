Запустить Docker. В внутреней папке пректа октрыть консоль и выполнить
```
docker-compose up
```
Так же выполнить миграцию бд 
```
dotnet ef migrations add InitialCreate
```
И за ней так же команду
```
dotnet ef database update
```
Если dotnet ef не установлен выволнить 
```
dotnet tool install --global dotnet-ef
```

