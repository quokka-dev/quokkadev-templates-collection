$shortName = "QuokkaDevcoll"
$id = "QuokkaDev.Templates.Collection"

# Check if the template is already installed
$templates = dotnet new -l
$template = $templates | Select-String $shortName
# If installed then uninstall it
if($null -ne $template){
    Write-Host "Removing $id"
    dotnet new -u $id
}
# Clear Template Cache
Write-Host "Clearing Template Cache"
Remove-Item ~/.templateengine -Recurse -Force

#Build a new Package
Write-Host "Building Template Package $id"
.\Build-Template.ps1

# Install Template
Write-Host "Installing new Template $id"
dotnet new -i ".\buildpackages\$id.*.nupkg"