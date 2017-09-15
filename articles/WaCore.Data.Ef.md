---
uid: UnitOfWork_Module
---

## 1. What is it?
* Implementation of UnitOfWork pattern
* While using the Repository pattern
* Using Entity Framework Core 2.0


## 2. Features
* UoW is responsible for creating repositories
* Not depending on any specific DbContextCan, you can create your own


## 3. How to find it
You can find it in the official nuget package server by searching for: ``WaCore.Data.Ef``


## 4. How to use it
Follwo this step-by-step guide to make use of this module or look at the ``WaCore.Sample`` project in the source code.

### 4.1 Create your own DbContext
For example you create a ``LibraryDbContext`` class, which derives from ``DbContext``:
[!code-csharp[Main](..\src\WaCore.Sample\Data\LibraryDbContext.cs?name=LibraryDbcontextDocu)]
 
### 4.2 Add ``WaCore.Data.Ef`` module
In your project, which represents the data-layer, add a reference to the module ``WaCore.Data.Ef``.

### 4.3 Implement your repository
You create your Repository class by implementing the interface ``IWacRepository<TEntity>``. Again, there is already an abstract class ``WacRepository<TEntity, TDbContext``, which implements that interface and uses EF Core 2.0 under the hood.
[!code-csharp[Main](..\src\WaCore.Sample\Data\Repositories\BooksRepository.cs?name=BookRepositoryDocu)]

### 4.4 Implement your UnitOfWork
You create your ``UnitOfWork`` class, by implementing the interface ``IWacUnitOfWork``. You can actually derive from the abstract class ``WacEfUnitOfWork``, which already implements that interface using EF Core 2.0 under the hood. If you want to use any other ORM, then you just need to implement before mentioned interface.
[!code-csharp[Main](..\src\WaCore.Sample\Data\UnitOfWork.cs?name=UnitOfWorkDocu)]

### 4.5 Register repositories in IoC container
In the ``Startup.cs`` class of your application you register your repositories by using the extension method ``AddUnitOfWork``, which expects a ``RepositoryConfiguration`` object as parameter. 
That class has multiple methods to register repositories: Either you register each repository manually (using method ``AddRepository<TRepoInterface, TRepoImplementation>``) or you use the method ``AddRepositoriesFromAssemblyOf<TAssemblySelector>`` (to register all classes, which names end with 'Repository') (see example).
[!code-csharp[Main](..\src\WaCore.Sample\Startup.cs?name=RegisterRepositoriesDocu)]

### 4.6 Use UoW in your service layer
For example, you can use the ``UnitOfwork`` as follows in your controller:
[!code-csharp[Main](..\src\WaCore.Sample\Controllers\BooksController.cs?name=UseUoWDocu)]