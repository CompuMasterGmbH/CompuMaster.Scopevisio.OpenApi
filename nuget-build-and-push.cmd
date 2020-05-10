::REM -UpdateNuGetExecutable not required since it's updated by VS.NET mechanisms
PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& 'src\CompuMaster.Scopevisio.OpenApi\_CreateNewNuGetPackage\DoNotModify\New-NuGetPackage.ps1' -ProjectFilePath '.\src\CompuMaster.Scopevisio.OpenApi\CompuMaster.Scopevisio.OpenApi.csproj' -verbose -NoPrompt -PushPackageToNuGetGallery"
pause