# webapp-core  
Modules for common features for a .Net Core application

## 1. Vision
Provide .Net Core developers with common functionality, which is often used in (web)-applications, so they can focus on the important business logic of their application they develop

## 2. What is it?
 * set of modules, which include **base functionality** often used in (web)-applications
 * built using **.Net Core 2.0** framework
 * **modular** structure: use only the modules you need
 * easily **extendable**: based on interfaces
 * based on a **layered architecture** (data, service, web)
 * available as **nuget packages** for easy integration


## 3. Modules
Following modules are planned/implemented:

| Module |   Status    |    Version/Downloads    | Description | 
| ---------- | ----------------- | -------------------- | -------------------- |
|[**WaCore.Data.Ef**](xref:UnitOfWork_Module) | implemented |[![NuGet Version](https://img.shields.io/nuget/v/WaCore.Data.Ef.svg)](https://www.nuget.org/packages/WaCore.Data.Ef/) [![NuGet Downloads](https://img.shields.io/nuget/dt/WaCore.Data.Ef.svg)](https://www.nuget.org/packages/WaCore.Data.Ef/)  | Unit of work implementation for Ef 2.0  |
| **WaCore.Crud.Services** | in progress | - | CRUD Service |
| **WaCore.TemplateMgmt.Services** | planned | - | Template management services

