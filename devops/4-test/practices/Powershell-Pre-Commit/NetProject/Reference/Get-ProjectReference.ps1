function Get-ProjectReference{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true,ValueFromPipeline=$true)]
        [ValidateScript({(Test-Path $_)-and((Get-Item $_).Extension -eq".csproj")})]
        [string]$Path,
        [Parameter(Mandatory=$false,ValueFromPipeline=$false)]
        [string]$PackageName
    )
    Begin{
        $xml=New-Object XML
        $packageList=@()
    }
    Process{
        $xml.Load($Path)
        foreach($ref in $xml.Project.ItemGroup.PackageReference)
        {
            if($null -ne $ref)
            {
                if($PackageName.Length -gt 0)
                {
                    if(-not ($ref.Include |Select-String -Pattern $PackageName -AllMatches).Matches)
                    {
                        Write-Verbose "$($ref.Include) not equal $PackageName" -Verbose
                        continue
                    }
                }
                $package=[PSCustomObject]@{
                        packageName = $ref.Include
                        Version= $ref.Version
                    }
                Write-Verbose "`n packageName:$($ref.Include) `n version:$($ref.Version)" -Verbose
                $packageList+=$package
            }
        }
    }
    End{
        return $packageList
    }
}
