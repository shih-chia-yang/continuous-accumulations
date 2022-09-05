BeforeAll{
    . $PSScriptRoot/Test-NetProject.ps1
    $FakeUnitTestsProjectPath="E:\Git\continuous-accumulations\learning\practices\ci-testservice\testservice.unittests\"
}

Describe "Test Project" {
    BeforeDiscovery{
        & dotnet build $FakeUnitTestsProjectPath -v q
    }
    It "given xunit project ,it return test result" {
        $result = Test-NetProject -Path $FakeUnitTestsProjectPath 
        $result | Should -Be 0 
    }

    AfterAll{
        $LogResultFilePath=Join-Path -Path $FakeUnitTestsProjectPath -ChildPath "Logs"
        Get-ChildItem $LogResultFilePath -Include *.xml -Recurse |Remove-Item
    }
}