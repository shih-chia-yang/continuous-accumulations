  function Get-CommittedFiles([string]$Name = '*')
  {
    return git diff --name-only --cached
  }