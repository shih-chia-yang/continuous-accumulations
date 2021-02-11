BeforeAll{
    . $PSScriptRoot/Get-ChangedFileRootDirectory.ps1
      $FakeCommittedFiles=@('TestService.cs','TestServiceTests.cs')
      $fakeRootDirectory="E:\Git\continuous-accumulations"
      $FakeTestProjectPath="E:\Git\continuous-accumulations\learning\practices\ci-testservice\testservice"
      $FakeUnitTestsProjectPath="E:\Git\continuous-accumulations\learning\practices\ci-testservice\testservice.unittests"
}

Describe 'Get-ChangedFileRootDirectory'{
    It 'Given array files ,it Return 2 Directory path'{
        $allCommittedCsProjects = Get-ChangedFileRootDirectory -FileName $FakeCommittedFiles -RootDirectory $fakeRootDirectory
        write-host $allCommittedCsProjects
        $allCommittedCsProjects.Count | Should -Be 2 
    }
  It 'Given array files, it Return this file full path' {

    $allCommittedCsProjects = Get-ChangedFileRootDirectory -FileName $FakeCommittedFiles -RootDirectory $fakeRootDirectory
    $allCommittedCsProjects[0].Name | Should -Be 'TestService.cs'
    $allCommittedCsProjects[0].DirectoryPath | Should -Be $FakeTestProjectPath
  }
}