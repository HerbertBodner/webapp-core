# webapp-core  
Modules for common features for a .Net Core application


|            |   Travis Build    |    AppVeyor Build    | 
| ---------- | ----------------- | -------------------- |
| **master** branch |[![Build Status](https://api.travis-ci.org/HerbertBodner/webapp-core.svg?branch=master)](https://travis-ci.org/HerbertBodner/webapp-core) |[![AppVeyor Build](https://ci.appveyor.com/api/projects/status/github/HerbertBodner/webapp-core?branch=master&svg=true)](https://ci.appveyor.com/project/yshchohaleu/webapp-core)

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
| **WaCore.Web** | in progress | - | OWASP headers middleware
| **WaCore.TemplateMgmt.Services** | planned | - | Template management services

