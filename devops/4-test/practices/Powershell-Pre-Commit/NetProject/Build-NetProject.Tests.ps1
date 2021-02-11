BeforeAll{
      $FakeTestProjectPath="E:\Git\continuous-accumulations\learning\practices\ci-testservice\testservice"
      . $PSScriptRoot/Build-NetProject.ps1
}

Describe "dotnet build" {
    It "given project path , it return build success" {
        Build-NetProject -path $FakeTestProjectPath 
        $lastExitCode | Should -Be 0 
    }
    
}