function Add-ProjectReference {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory=$true,ValueFromPipeline=$true)]
        [ValidateScript({(Test-Path $_)-and((Get-Item $_).Extension -eq".csproj")})]
        [string]$Path,
        [Parameter(Mandatory=$true,ValueFromPipeline=$true)]
        [string]$Name
    )
    
    begin {
        
    }
    
    process {
        dotnet add $Path package $Name
    }
    
    end {
        return $LASTEXITCODE
    }
}

# $FakeUnitTestsProjectPath="E:\Git\continuous-accumulations\learning\practices\ci-testservice\testservice.unittests\testservice.unittests.csproj"

# Add-ProjectReference -Path $FakeUnitTestsProjectPath -Name "XunitXml.TestLogger"

# write-host $LASTEXITCODE