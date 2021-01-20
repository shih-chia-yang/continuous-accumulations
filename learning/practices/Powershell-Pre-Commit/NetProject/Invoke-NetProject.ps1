function Invoke-NetProject ([string]$Path,[string[]]$File) {
    . $PSScriptRoot/Find-NetProject.ps1
    . $PSScriptRoot/Build-NetProject.ps1
    . $PSScriptRoot/Reference/Confirm-ProjectReference.ps1
    . $PSScriptRoot/Test-NetProject.ps1

    Write-Information -MessageData "getting all net project in $Path"
    $projectList=Find-NetProject -path $Path -files $File
    $executeResult=@()
    foreach($project in $projectList)
    {
        $projectName=$project.Name
        $projectPath=$project.path
        Write-Verbose $project.Name -Verbose
        Build-NetProject -Path $projectPath
        Write-Verbose "type:$($project.IsTestProject) `n execute result:$LastExitCode" -Verbose
        if(($project.IsTestProject -eq $true) -and ($LastExitCode -eq 0))
        {

            Write-Host "test project build success, begin to testing"
            $ProjectFullPath=Join-Path -Path $projectPath -ChildPath $projectName
            $testPackage=@('xunit','xunit.runner.visualstudio','XunitXml.TestLogger')
            Confirm-ProjectReference -Path $ProjectFullPath -Package $testPackage
            Test-NetProject -Path $projectPath
        }
    }
    return $LastExitCode
}