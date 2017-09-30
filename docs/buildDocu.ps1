Param(
	[Switch]$deploy,
    [string]$git_email,
    [string]$git_user,
    [string]$git_access_token
)

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


$docfxVersion = "2.24.0"
$VisualStudioVersion = "15.0";
$DotnetSDKVersion = "2.0.0";

# Get dotnet paths
$MSBuildExtensionsPath = "C:\Program Files\dotnet\sdk\" + $DotnetSDKVersion;
$MSBuildSDKsPath = $MSBuildExtensionsPath + "\SDKs";

# Get Visual Studio install path
$VSINSTALLDIR =  $(Get-ItemProperty "Registry::HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\VisualStudio\SxS\VS7").$VisualStudioVersion;

# Add Visual Studio environment variables
$env:VisualStudioVersion = $VisualStudioVersion;
$env:VSINSTALLDIR = $VSINSTALLDIR;

# Add dotnet environment variables
$env:MSBuildExtensionsPath = $MSBuildExtensionsPath;
$env:MSBuildSDKsPath = $MSBuildSDKsPath;

# Install docfx
Exec { & nuget install docfx.console -Version $docfxVersion }


# Build our docs
Write-Host "`n[Build our docs]" -ForegroundColor Green
Exec { & .\docfx.console.$docfxVersion\tools\docfx docs/docfx.json }


if(!$deploy){
    return
}

# Checkout gh-pages to folder origin_site
Write-Host "`n[Checkout gh-pages]" -ForegroundColor Green
Exec { & git clone --quiet --no-checkout https://github.com/HerbertBodner/webapp-core.git -b gh-pages origin_site }

          
# Copy origin_site/.git to docs/_site
Write-Host "`n[Copy origin_site/.git to docs/_site]" -ForegroundColor Green
Exec { & Copy-Item origin_site/.git docs/_site -recurse }


Push-Location docs/_site

    # Configuring git credentials
    Write-Host "`n[Configuring git credentials in docs/_site]" -ForegroundColor Green
    Exec { & git config --local credential.helper store }
    #Exec { & Add-Content ".git-credentials" "https://$($env:git_access_token):x-oauth-basic@github.com`n" }
    Exec { & git config --local user.email $git_email }
    Exec { & git config --local user.name $git_user }


    # Add changes
    Write-Host "`n[Add changes]" -ForegroundColor Green
    Exec { & git add -A 2>&1 }
          
    # Show changes
    Write-host "`n[git status --porcelain]" -ForegroundColor Green

    $status = git status --porcelain

    if (![string]::IsNullOrEmpty($status)) {  
          
        # Committing changes
        Write-host "`n[Committing changes]" -ForegroundColor Green
        Exec { & git commit -m "CI Updates" -q }
        Exec { & git push https://$($git_access_token):x-oauth-basic@github.com/HerbertBodner/webapp-core.git gh-pages -q }
     }
    else {
        Write-host "`n[No changes to commit]" -ForegroundColor Green
    }
Pop-Location