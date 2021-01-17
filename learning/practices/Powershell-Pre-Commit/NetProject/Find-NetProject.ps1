function Find-NetProject([string]$path,[string[]] $files)
{
    Write-Information -MessageData "getting .net project from:$path" -InformationAction Continue
    $Projects = Get-ChildItem -Path $path -Filter *.csproj -Recurse
    foreach($project in $Projects){Write-Verbose "$($project)" -Verbose}
    $changedCsProjectList=@()
    $testProjectPattern="(xunit|nunit|mstest)"
    foreach($project in $Projects)
    {
        Write-Information -MessageData "checking if .net project directory exists changed file" -InformationAction Continue
        $changedFile=Get-ChildItem -Path $project.Directory -File -Recurse | Where-Object {$files -contains $_.Name }
        if($changedFile.Exists -eq  $true)
        {
            Write-Verbose "$($changedFile.Name) exist is $($changedFile.Exists)" -Verbose
            $projectInfo=[PSCustomObject]@{
                    Name =$project.Name
                    IsTestProject=(Get-Content -Path $project.FullName | select-string -pattern $testProjectPattern).Length -ne 0
                    path=$project.Directory
                }
            Write-Verbose "changedProjectList add .net project path {$($project.Directory)}" -Verbose
            $changedCsProjectList+=$projectInfo
        }
    }
    Write-Information -MessageData "finish checking .net project directory" -InformationAction Continue
    return $changedCsProjectList
}