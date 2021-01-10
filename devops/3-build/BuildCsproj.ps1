$rootDirectory = Join-Path (Split-Path -Parent $MyInvocation.MyCommand.Path) "\..\..\"
$exitCode = 0;

Function Get-CsProj([string] $rootDirectory)
{
    $files = Get-ChildItem -Path $rootDirectory -Filter *.csproj -Recurse
    $modifiedfiles = @()

    foreach($file in $files)
    {
         $modifiedfiles += $file.FullName
    }
}

return $modifiedfiles