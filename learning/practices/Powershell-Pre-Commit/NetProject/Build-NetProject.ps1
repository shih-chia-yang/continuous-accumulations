function Build-NetProject
{
    param (
        [string]$Path
    )
    Write-Information -MessageData "getting .net project from:$Path" -InformationAction Continue
    $result=dotnet build $Path --nologo -v q 
    Write-Verbose "$result" -Verbose
    return $lastExitCode
}