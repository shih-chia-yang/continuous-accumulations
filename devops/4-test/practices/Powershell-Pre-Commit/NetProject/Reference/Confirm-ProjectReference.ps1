function Confirm-ProjectReference {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory=$true,ValueFromPipeline=$true)]
        [ValidateScript({(Test-Path $_)-and((Get-Item $_).Extension -eq".csproj")})]
        [string]$Path,
        [Parameter(Mandatory=$false,ValueFromPipeline=$false)]
        [string[]]$Package
    )
    
    begin {
        . $PSScriptRoot/Get-ProjectReference.ps1
        . $PSScriptRoot/Add-ProjectReference.ps1
    }
    
    process {
        foreach($var in $Package)
        {
            $selectedPackage=Get-ProjectReference -path $Path -PackageName $var
            if($selectedPackage.Count -eq 0)
            {
                Add-ProjectReference -Path $Path -Name $var
            }
        }
    }
    
    end {
        
    }
}