BeforeAll{
    . $PSScriptRoot/Invoke-NetProject.ps1
    $fakeCommittedFiles=@('TestService.cs','TestServiceTests.cs')
    $fakeRootPath="E:\Git\continuous-accumulations\learning\practices"
}

Describe "build and test" {
    Context "committed file contains *.cs file" {
        It "given *.cs file ,should be execute build and test" {
            Invoke-NetProject -Path $fakeRootPath -File $fakeCommittedFiles
            $lastExitCode | Should -Be 0
        }
    }
}