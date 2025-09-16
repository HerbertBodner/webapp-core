# webapp-core

webapp-core is a .NET Core 2.0 modular library providing common functionality for web applications including Entity Framework unit of work patterns, CRUD services, web middlewares, and template management.

**Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.**

## Working Effectively

### Dependencies and Prerequisites
- .NET Core SDK 8.0+ is available via `dotnet` command
- The codebase targets .NET Core 2.0 but builds successfully with .NET 8.0 SDK
- Expect security vulnerability warnings for .NET Core 2.0 packages - these are expected and can be ignored

### Build Commands - NEVER CANCEL
- **Restore packages**: `dotnet restore ./src` -- takes ~45 seconds. **NEVER CANCEL**. Set timeout to 2+ minutes.
- **Build all projects**: `dotnet build -c Release ./src` -- takes ~10 seconds. **NEVER CANCEL**. Set timeout to 2+ minutes.
- **Build documentation**: 
  - Install DocFX: `dotnet tool install -g docfx` -- takes ~6 seconds
  - Copy README: `cp README.md docs/index.md`
  - Generate docs: `docfx docs/docfx.json` -- takes ~17 seconds. **NEVER CANCEL**. Set timeout to 2+ minutes.
- **Create NuGet packages**: `cd src/WaCore.Data.Ef && dotnet pack -c Release -o ../../artifacts` -- takes ~2 seconds per project

### Unit Tests - RUNTIME LIMITATION
- **Test command**: `dotnet test ./src/WaCore.Crud.UnitTests/WaCore.Crud.UnitTests.csproj`
- **IMPORTANT**: Tests cannot run due to missing .NET Core 2.0 runtime (only .NET 8.0 is available)
- Tests build successfully but fail at runtime with framework version error
- Document this limitation but do not attempt to fix the runtime issue

### Sample Applications - RUNTIME LIMITATION  
- **Web applications**: Located in `src/WaCore.Crud.ListSample1/`, `src/WaCore.Sample/`, `src/WaCore.Sample.Middlewares/`
- **Run command**: `cd src/WaCore.Crud.ListSample1 && dotnet run`
- **IMPORTANT**: Applications cannot run due to missing .NET Core 2.0 runtime
- Applications build successfully but fail at runtime with framework version error
- Document this limitation but do not attempt to fix the runtime issue

## Repository Structure

### Key Directories
```
├── src/                              # All source code
│   ├── webapp-core.sln              # Main solution file with all projects
│   ├── WaCore.Contracts/            # Core contracts and interfaces
│   ├── WaCore.Data.Ef/             # Entity Framework unit of work implementation
│   ├── WaCore.Web/                 # Web middleware components
│   ├── WaCore.Crud.*/              # CRUD-related modules (services, DTOs, contracts, etc.)
│   └── WaCore.Sample*/             # Sample applications demonstrating usage
├── docs/                           # Documentation source files
│   ├── docfx.json                 # DocFX configuration
│   ├── buildDocu.ps1             # PowerShell documentation build script
│   └── articles/                  # Documentation articles
├── .travis.yml                    # Travis CI configuration
├── appveyor.yml                   # AppVeyor CI configuration
└── Build.ps1                     # Main PowerShell build script
```

### Main Projects
- **WaCore.Data.Ef**: Entity Framework Core 2.0 unit of work implementation (main package)
- **WaCore.Web**: OWASP security headers and exception handling middleware
- **WaCore.Crud.Services**: CRUD service implementations
- **WaCore.Crud.Data.Ef**: Entity Framework CRUD data layer
- **WaCore.Crud.ListSample1**: Sample web application demonstrating CRUD list functionality

## Validation Steps

### Build Validation - Always Required
1. **Always run these commands to validate changes**:
   ```bash
   cd /path/to/webapp-core
   dotnet restore ./src     # ~45 seconds, timeout 2+ minutes
   dotnet build -c Release ./src    # ~10 seconds, timeout 2+ minutes
   ```

2. **Test documentation generation**:
   ```bash
   dotnet tool install -g docfx  # Only if not already installed
   cp README.md docs/index.md
   docfx docs/docfx.json        # ~17 seconds, timeout 2+ minutes
   ```

3. **Test package creation** (for main libraries):
   ```bash
   cd src/WaCore.Data.Ef
   dotnet pack -c Release -o ../../artifacts  # ~2 seconds
   ```

### Manual Testing Scenarios
- **Build verification**: Ensure all projects compile without errors (warnings about .NET Core 2.0 are expected)
- **Package creation**: Verify NuGet packages can be created for the main libraries
- **Documentation**: Verify documentation generates successfully with DocFX

### Known Limitations
- **Runtime execution**: Applications and tests cannot run due to .NET Core 2.0 runtime requirement
- **Security warnings**: Multiple vulnerability warnings for .NET Core 2.0 packages are expected
- **Framework compatibility**: Code targets .NET Core 2.0 but builds with .NET 8.0 SDK

## CI/CD Integration
- **Travis CI**: Builds on Linux using .NET Core 2.0
- **AppVeyor**: Builds on Windows, creates NuGet packages, deploys documentation to GitHub Pages
- **NuGet deployment**: Automatic for tagged releases on master branch

## Common Tasks

### Adding New Features
- Follow the modular pattern: create contracts, implementation, and tests
- Update the main solution file `src/webapp-core.sln` to include new projects
- Add appropriate project references following existing patterns

### Documentation Updates
- Edit files in `docs/articles/` for module-specific documentation
- The `README.md` is automatically copied to `docs/index.md` during builds
- Documentation is deployed to GitHub Pages automatically via AppVeyor

### Package Management
- All packages target .NET Core 2.0 for maximum compatibility
- Main deployable packages: WaCore.Data.Ef, WaCore.Web, WaCore.Crud.Data.Ef, WaCore.Crud.Services
- Version numbers are managed through Git tags in CI/CD pipeline

## Expected Command Timing
- `dotnet restore ./src`: ~45 seconds
- `dotnet build -c Release ./src`: ~10 seconds  
- `docfx docs/docfx.json`: ~17 seconds
- `dotnet pack`: ~2 seconds per project
- Full build cycle: ~1-2 minutes total

**CRITICAL**: Always use timeout values of 2+ minutes for build commands. **NEVER CANCEL** commands that appear to be taking time - the build process includes package downloads and compilation that can take time to complete.