param (
        [string]$email
    )
Write-Information "begin load powershell script" -InformationAction Continue
. $PSScriptRoot/Git/Confirm-GitEmail.ps1
. $PSScriptRoot/Git/Get-CommittedFiles.ps1
. $PSScriptRoot/Git/Get-ChangedFileRootDirectory
. $PSScriptRoot/NetProject/Invoke-NetProject
$email =git config user.email
if((Confirm-GitEmail -Email $email) -eq 1)
{
    exit 1
}
Write-Information "get committed files" -InformationAction Continue
$repositoryDirectory =Split-Path -Parent $PSScriptRoot|Split-Path
Write-host "root path:$repositoryDirectory" 
$changedFile=Get-CommittedFiles
$changedFileInfos=Get-ChangedFileRootDirectory -fileNames $changedFile -RootDirectory $repositoryDirectory | Where-Object {$_.Name -like "*.cs"}
$executeResult=@()
foreach($file in $changedFileInfos)
{
    Write-Information "execute net project" -InformationAction Continue
    $result=Invoke-NetProject -Path $repositoryDirectory -File $changedFile
    $executeResult+=$result
}
if($executeResult -contains 1)
{
    exit 1
}
exit 0