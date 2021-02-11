function Get-ChangedFileRootDirectory([string[]]$fileNames,[string]$RootDirectory)
{
    Write-Verbose -Message "changedFiles:$fileNames" -Verbose
    Write-information -MessageData "----get script path----" -InformationAction Continue
    # $rootDirectory =Split-Path -Parent $PSScriptRoot
    Write-Verbose -Message "project path:$RootDirectory" -Verbose
    $changedFolders=@()
    Write-information -MessageData "----search file in root directory ----" -InformationAction Continue
    foreach($file in $fileNames)
    {
        $filePath=Get-ChildItem -path $RootDirectory -Filter $file -Recurse
        Write-Verbose -Message "find file path:$($filePath.FullName)" -Verbose
        Write-information -MessageData "----add changedFile directory to array ----" -InformationAction Continue
        Write-Verbose -Message "changedFile directory path:$($filePath.Directory)" -Verbose
        $changedFileInfo=[PSCustomObject]@{
                    Name =$file
                    DirectoryPath=$filePath.Directory
                }
        $changedFolders+=$changedFileInfo
        Write-Verbose -Message "changedProjects count :$($changedFolders.Count)" -Verbose
    }
    return $changedFolders
}