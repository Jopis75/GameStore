# GameStore

- .NET 7.
- C# 11.
- EF Core.
- ASP.NET Core.
- Web API Core.
- Onion Architecture.
- Command Query Responsibility Segregation (CQRS) Pattern.
- Mediator Pattern.
- Service Layer Pattern.
- Unit of Work Pattern.
- Repository Pattern.
- DTO Pattern.
- Null Object Pattern.
- AutoMapper.
- FluentValidation.
- Serilog.
- JWT authentication.


ToDo
- Create Entity Framework Dtos for every entity in the GameStore.Domain project.
- Add a TDto generic parameter to the IRepositoryBase interface and RepositoryBase class and all other repository interfaces and classes. That parameter will be used to return the Entity Framework Dto for the actual entity in repository queries and commands instead of the actual entity.
