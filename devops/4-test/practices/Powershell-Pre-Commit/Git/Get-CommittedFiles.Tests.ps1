BeforeAll {
  . $PSScriptRoot/Get-CommittedFiles.ps1
}

Describe 'Get-CommittedFiles'{
  It 'Given git add 2 files, it lists all 2 files' {
    $allCommittedFiles = Get-CommittedFiles
    $allCommittedFiles.Count | Should -Be 2
  }
}



