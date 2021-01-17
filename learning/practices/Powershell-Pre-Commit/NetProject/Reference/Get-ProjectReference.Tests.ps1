BeforeAll{
. $PSScriptRoot/Get-ProjectReference.ps1
$FakeUnitTestsProjectPath="E:\Git\continuous-accumulations\learning\practices\ci-testservice\testservice.unittests\testservice.unittests.csproj"
}

Describe "Get *.csproj xml structure" {
    Context "read xml file" {
        It "given *,csproj file packageList should Not be null" {
            $xml=Get-ProjectReference -Path $FakeUnitTestsProjectPath 
            $xml |Should -Not -BeNullOrEmpty
        }

        It "given *,csproj file packageList Count should be gt 0" {
            $xml=Get-ProjectReference -Path $FakeUnitTestsProjectPath 
            $xml.Count |Should -gt 0
        }
    }

    Context "Filter packagename"{
        It "given xunit should be return >0" {
            $xml=Get-ProjectReference -Path $FakeUnitTestsProjectPath -PackageName 'xunit'
            $xml.Count |Should -gt 0
        }

        It "given dosen't exist name,should be return 0" {
            $xml=Get-ProjectReference -Path $FakeUnitTestsProjectPath -PackageName 'abcd'
            $xml.Count |Should -eq 0
        }
    }
}