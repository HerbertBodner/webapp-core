Param(
	[Switch]$Deploy
)

$git_user = "OpenPublishBuild"
$git_email = "info@wacore.com"
$git_access_token = "4vngnALEVBWUWd4tuLHADxd5vF26geMyGw0dGjun494fq/W0sLiQAJxhXXYsdD39"
$target_branch = "gh-pages"

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
	Add-Content "$env:USERPROFILE\.git-credentials" "https://$git_access_token:x-oauth-basic@github.com`n"

	& git config --global user.email "$git_email"
	& git config --global user.name "$git_user"

	# Checkout gh-pages
	Write-Host "`n[Checkout $target_branch]" -ForegroundColor Green
	git clone --quiet --no-checkout --branch=$target_branch https://github.com/HerbertBodner/webapp-core.git docs/_site
}

# Build our docs
Write-Host "`n[Build our docs]" -ForegroundColor Green

& .\docfx.console.$docfxVersion\tools\docfx docs/docfx.json

if($Deploy){
	git -C docs/_site status
	$pendingChanges = git -C docs/_site status | select-string -pattern "Changes not staged for commit:","Untracked files:" -simplematch
	if ($pendingChanges -ne $null) { 
		# Committing changes
		Write-host "`n[Committing changes]" -ForegroundColor Green
		Write-host "`n[git -C docs/_site add -A]" -ForegroundColor Green
		git -C docs/_site add -A
		Write-host "`n[git -C docs/_site status]" -ForegroundColor Green
		git -C docs/_site status
		Write-host "`n[Committing changes]" -ForegroundColor Green
		git -C docs/_site commit -m "static site regeneration"
		# Pushing changes
		Write-host "`n[Pushing changes]" -ForegroundColor Green
		git -C docs/_site push origin $target_branch
		Write-Host "`n[Success!]" -ForegroundColor Green
	} 
	else { 
		write-host "`nNo changes to documentation" -ForegroundColor Yellow
    }
}