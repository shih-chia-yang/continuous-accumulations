function Confirm-WebSite([string]$Url)
    {
        $statusInfo=@()
        if($Url -match "(http:|https:)")
        {
                $job = Start-Job -ScriptBlock {  
                    Param($Url)
                    try{
                        Invoke-WebRequest -Uri $Url -UseBasicParsing -ErrorAction SilentlyContinue -TimeoutSec 5 -Method Get
                    }
                    catch
                    {
                        $_.Exception.Response.StatusCode.Value__
                    }
                    
                } -ArgumentList $Url |Wait-Job|Receive-Job
                if ($job.StatusCode -ne 200) {
                    $statusInfo+= $false
                    $statusInfo+= 404
                } else {
                    $statusInfo+= $true
                    $statusInfo+=$job.StatusCode
                }
        }
        return $statusInfo
    }