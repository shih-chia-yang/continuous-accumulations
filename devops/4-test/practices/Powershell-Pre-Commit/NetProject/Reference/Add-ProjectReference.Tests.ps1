BeforeAll{
. $PSScriptRoot/Add-ProjectReference.ps1
$fakePath="E:\Git\continuous-accumulations\learning\practices\ci-testservice\testservice.unittests\testservice.unittests.csproj"
$fakePackageName="XunitXml.TestLogger"
}
Describe "Add package" {
    Context "add exist package" {
        It "given correct package name , exitCode should be return 0" -TestCases(
            @{
                fakePath="E:\Git\continuous-accumulations\learning\practices\ci-testservice\testservice.unittests\testservice.unittests.csproj"
                fakePackageName="XunitXml.TestLogger"
                expected=0
            }
        ){
            Add-ProjectReference -path $fakePath -name $fakePackageName
            $lastExitCode | Should -be $expected
        }
        AfterAll{
            dotnet remove $fakePath package $fakePackageName
        }
    }
}
