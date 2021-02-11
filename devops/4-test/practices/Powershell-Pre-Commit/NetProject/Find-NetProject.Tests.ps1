BeforeAll  {
    . $PSScriptRoot/Find-NetProject.ps1
    $fakeCommittedFiles=@('TestService.cs','TestServiceTests.cs')
    $fakeRootPath="E:\Git\continuous-accumulations\learning\practices"
}

Describe 'Find-CsProject'{
    It 'Given rootPath and changed .cs files ,it Return 2 *.csproject projectInfo object'{

        $allChangedCsProjects = Find-NetProject -path $fakeRootPath -files $fakeCommittedFiles
        $allChangedCsProjects.Count | Should -Be 2 
    }

    It 'if Contain unit test project then projectInfo.IsTestProject,  it Return true'{
        $allChangedCsProjects = Find-NetProject -path $fakeRootPath -files $fakeCommittedFiles
        $allChangedCsProjects[1].IsTestProject | Should -Be $true 
    }
}