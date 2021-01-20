BeforeAll {

  function Get-CommittedFiles([string]$Name = '*')
  {
    return git diff --name-only --cached
  }

  function Get-RootDirectory([string[]]$fileNames)
  {
    Write-Verbose -Message "changedFiles:$fileNames" -Verbose
    Write-information -MessageData "----get script path----" -InformationAction Continue
    $rootDirectory =Split-Path -Parent $PSScriptRoot
    Write-Verbose -Message "project path:$rootDirectory" -Verbose
    $changedProjects=@()
    Write-information -MessageData "----search file in root directory ----" -InformationAction Continue
    foreach($file in $fileNames)
    {
        $filePath=Get-ChildItem -path $rootDirectory -Filter $file -Recurse
        Write-Verbose -Message "find file path:$($filePath.FullName)" -Verbose
        Write-information -MessageData "----add changedFile directory to array ----" -InformationAction Continue
        Write-Verbose -Message "changedFile directory path:$($filePath.Directory)" -Verbose
        $changedProjects+=$filePath.Directory
        Write-Verbose -Message "changedProjects count :$($changedProjects.Count)" -Verbose
    }
    return $changedProjects
  }

  function Find-CsProject([string]$path,[string[]] $files)
  {
    $Projects = Get-ChildItem -Path $path -Filter *.csproj -Recurse
    $changedCsProject=@()
    foreach($project in $Projects)
    {
        $changedFile=Get-ChildItem -path $project.Directory -Filter $file -Recurse
        
        if($changedFile -ne $null)
        {
            $projectInfo=[PSCustomObject]
                @{
                    Name = $project.DirectoryName
                    path=$project.Directory
                }
            
            $changedCsProject+=$projectInfo
        }
    }
    return $changedCsProject
  }
  $FakeCommittedFiles=@('TestService.cs','TestServiceTests.cs')
  $FakeTestProjectPath="E:\Git\continuous-accumulations\learning\practices\ci-testservice\testservice"
  $FakeUnitTestsProjectPath="E:\Git\continuous-accumulations\learning\practices\ci-testservice\testservice.unittests"
}



Describe 'Get-CommittedFiles'{
  It 'Given git add 2 files, it lists all 2 files' {
    $allCommittedFiles = Get-CommittedFiles
    $allCommittedFiles.Count | Should -Be 2
  }
}

Describe 'Get-RootDirectory'{
    It 'Given array files ,it Return 2 Directory path'{
        $allCommittedCsProjects = Get-RootDirectory -FileName $FakeCommittedFiles
        write-host $allCommittedCsProjects
        $allCommittedCsProjects.Count | Should -Be 2 
    }
  It 'Given array files, it Return this file full path' {

    $allCommittedCsProjects = Get-RootDirectory -FileName $FakeCommittedFiles
    $allCommittedCsProjects[0] | Should -Be $FakeTestProjectPath
  }
}

Describe 'Find-CsProject'{
    It 'Given rootPath and changed .cs files ,it Return 2 *.csproject projectInfo object'{
        $allCommittedCsProjects = Find-CsProject -FileName $FakeCommittedFiles
        write-host $allCommittedCsProjects
        $allCommittedCsProjects.Count | Should -Be 2 
    }
}


   