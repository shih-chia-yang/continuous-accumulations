BeforeAll{
. $PSScriptRoot/Confirm-ProjectReference.ps1
$testPackage=@('xunit','xunit.runner.visualstudio','XunitXml.TestLogger')
$FakeUnitTestsProjectPath="E:\Git\continuous-accumulations\learning\practices\ci-testservice\testservice.unittests\testservice.unittests.csproj"
}

Describe "before invoke project,confirm Reference" {
    Context "unit test project reference" {
        It "given xunit reference ,if project not contain should be add reference" {
            Confirm-ProjectReference -Path $FakeUnitTestsProjectPath -Package $testPackage
            $LASTEXITCODE |Should -Be 0
        }

        AfterAll{
            dotnet remove $FakeUnitTestsProjectPath package 'XunitXml.TestLogger'
        }
    }
}