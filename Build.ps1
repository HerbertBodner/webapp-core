function Exec
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    echo "Exec: $cmd"
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

echo "build: Build started"
$projectsToDeploy = @("src/WaCore.Data.Ef")

if(Test-Path .\artifacts) { Remove-Item .\artifacts -Force -Recurse }

echo "build: Restore packages"
exec { & dotnet restore .\src }
exec { & dotnet build -c Release .\src }

if ($env:APPVEYOR_REPO_TAG -eq "true") {
    $buildVersion = $env:APPVEYOR_REPO_TAG_NAME
    echo "NuGet pack version: $buildVersion"

    foreach ($src in $projectsToDeploy) {
        Push-Location $src
    
        echo "build: Pack project in $src"
        exec { & dotnet pack -c Release --include-symbols -o ..\..\artifacts /p:PackageVersion=$buildVersion }    

        Pop-Location
    }
}

