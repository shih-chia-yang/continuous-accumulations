BeforeDiscovery{
    . $PSScriptRoot/Test-NetProject.ps1
    $FakeUnitTestsProjectPath="E:\Git\continuous-accumulations\learning\practices\ci-testservice\testservice.unittests\"
}

Describe "Test Project" {
    BeforeAll{
        & dotnet build $FakeUnitTestsProjectPath -v q
    }
    It "given xunit project ,it return test result" {
        $result = Test-NetProject -path $FakeUnitTestsProjectPath 
        $result | Should -Be 0 
    }
}