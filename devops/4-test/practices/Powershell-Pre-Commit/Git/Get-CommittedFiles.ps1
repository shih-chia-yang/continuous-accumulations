function Get-CommittedFiles {
 [CmdletBinding()]
    param (
      [string]$Name = '*'
    )
    begin {
      $files =git diff --name-only --cached
      $pattern="\/{0}(-|\w)*.(cs|ps1)$"
      $changed=@()
    }
    process {
      foreach($file in $files)
      {
        $regexMatch=$file| select-string -Pattern $pattern
        if($regexMatch.Matches.Length -gt 0)
        {
          $changed+=$regexMatch.Matches.groups[0]
        }
      }
      Write-Host $changed
    }
    end {
      return $changed
    }
}
  