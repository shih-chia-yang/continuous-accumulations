function Test-NetProject {
    param (
        [string]$Path
    )
    Write-Information -MessageData "getting test project from:$Path" -InformationAction Continue
    #check project test package have installed
    #check project xunit.runner have installed
    #check project xunit.xmllog have installed
    #coverlet have installed
    
    $currentDateTime= Get-Date -Format "yyyyMMdd"
    $result=& dotnet test $Path --no-restore --logger:"xunit;logfilepath=.\Logs\$($currentDateTime)Test-Result.xml"
    #get xml result
    #save log to logs directory
    Write-Verbose -Message "$result" -Verbose
    #xunit xml format
    # assemblies
    # assembly
    # collection
    # error
    # errors
    # failure
    # test
    # trait
    # traits
    # $testResult=[PSCustomObject]@{
    #     Name = Value
    # }

    #return reference pscustomobject
    return $lastExitCode
}