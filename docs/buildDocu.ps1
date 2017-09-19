Param(
	[Switch]$Deploy,
	[Switch]$Serve
)
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
& nuget install docfx.console -Version $docfxVersion

if($Deploy){
	# Configuring git credentials
	Write-Host "`n[Configuring git credentials]" -ForegroundColor Green
	& git config --global credential.helper store
	Add-Content "$env:USERPROFILE\.git-credentials" "https://$($env:git_access_token):x-oauth-basic@github.com`n"

	& git config --global user.email "$env:git_email"
	& git config --global user.name "$env:git_user"

	# Checkout gh-pages
	Write-Host "`n[Checkout gh-pages]" -ForegroundColor Green
	git clone --quiet --no-checkout --branch=gh-pages https://github.com/HerbertBodner/webapp-core.git docs/_site
}

# Build our docs
Write-Host "`n[Build our docs]" -ForegroundColor Green

& .\docfx.console.$docfxVersion\tools\docfx docs/docfx.json (&{If($Serve) {"--serve"}})

if($Deploy){
	git -C docs/_site status
	$pendingChanges = git -C docs/_site status | select-string -pattern "Changes not staged for commit:","Untracked files:" -simplematch
	if ($pendingChanges -ne $null) { 
		# Committing changes
		Write-host "`n[Committing changes]" -ForegroundColor Green
		git -C docs/_site add -A
		git -C docs/_site commit -m "static site regeneration"
		# Pushing changes
		Write-host "`n[Pushing changes]" -ForegroundColor Green
		git -C docs/_site push origin gh-pages --quiet
		Write-Host "`n[Success!]" -ForegroundColor Green
	} 
	else { 
		write-host "`nNo changes to documentation" -ForegroundColor Yellow
    }
}