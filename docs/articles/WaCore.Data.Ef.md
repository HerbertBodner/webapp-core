---
uid: UnitOfWork_Module
---

## 1. What is it?
* Implementation of UnitOfWork pattern
* While using the Repository pattern
* Using Entity Framework Core 2.0



## 2. Features
* UoW is responsible for creating repositories
* Not depending on any specific DbContext, you can create your own


## 3. How to install
You can find it in the official nuget package server by searching for: ``WaCore.Data.Ef``


## 4. How to use it
Follow this step-by-step guide to make use of this module or look at the ``WaCore.Sample`` project in the source code.

### 4.1 Create your own DbContext
For example you create a ``LibraryDbContext`` class, which derives from ``DbContext``:
[!code-csharp[Main](..\..\src\WaCore.Sample\Data\LibraryDbContext.cs?name=LibraryDbcontextDocu)]
 
### 4.2 Add ``WaCore.Data.Ef`` module
In your project, which represents the data-layer, add a reference to the module ``WaCore.Data.Ef``.

### 4.3 Implement your repository
You create your Repository class by implementing the interface ``IWacRepository<TEntity>``. Again, there is already an abstract class ``WacRepository<TEntity, TDbContext``, which implements that interface and uses EF Core 2.0 under the hood.
[!code-csharp[Main](..\..\src\WaCore.Sample\Data\Repositories\BooksRepository.cs?name=BookRepositoryDocu)]

### 4.4 Implement your UnitOfWork
You create your ``UnitOfWork`` class, by implementing the interface ``IWacUnitOfWork``. You can actually derive from the abstract class ``WacEfUnitOfWork``, which already implements that interface using EF Core 2.0 under the hood. If you want to use any other ORM, then you just need to implement aforementioned interface.
[!code-csharp[Main](..\..\src\WaCore.Sample\Data\UnitOfWork.cs?name=UnitOfWorkDocu)]

### 4.5 Register repositories in IoC container
In the `ConfigureServices` method of your `Startup` class use the extension method `AddUnitOfWork` to register your Unit of Work class and your repositories in the IoC container.

Specify your Unit of Work class in the type parameters of `AddUnitOfWork` and add your repositories using the configuration function argument. You can add specific repositories using `AddRepository<TRepoInterface, TRepoImplementation>` or all repositories defined in specific assembly using `AddRepositoriesFromAssemblyOf<TAssemblySelector>`.
[!code-csharp[Main](..\..\src\WaCore.Sample\Startup.cs?name=RegisterRepositoriesDocu)]

### 4.6 Use UnitOfWork in your service layer
You can get a UnitOfWork instance using dependency injection via constructor arguments.
For instance, you can get an instance in the constructor and then use it as follows in your controller:
[!code-csharp[Main](..\..\src\WaCore.Sample\Controllers\BooksController.cs?name=UseUoWDocu)]