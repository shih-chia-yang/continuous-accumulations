function Read-Xlsx {
        param (
            [string]$Path,
            [string]$Output
        )

        . $PSScriptRoot/Confirm-WebSite
        $objExcel = New-Object -ComObject Excel.Application  
        $WorkBook = $objExcel.Workbooks.Open($Path)
        # $sheetName=$WorkBook.sheets | Select-Object -Property Name 
        $WorkSheet = $WorkBook.Sheets.Item(1)
        $totalNoOfRecords = ($WorkSheet.UsedRange.Rows).Count
        $totalNoOfItems = $totalNoOfRecords - 1
        $rowNo,$colUniversityId=1,3
        $rowNo,$colUniversityName = 1,4
        $rowNo,$colWebsite = 1,5
        $detectResults=@()
        if($totalNoOfRecords -gt 1)
        {
            for($i=2;$i -le $totalNoOfItems ;$i++)
            {
                if(($null -eq $WorkSheet.Cells.Item($rowNo + $i, $colUniversityId)) -or ($WorkSheet.Cells.Item($rowNo + $i, $colUniversityId).text.Length -eq 0))
                {
                    continue
                }
                $txtUniversityId=$WorkSheet.Cells.Item($rowNo + $i, $colUniversityId).Value()
                $txtUniversityName=$WorkSheet.Cells.Item($rowNo + $i, $colUniversityName).Value()
                $txtWebSite=$WorkSheet.Cells.Item($rowNo + $i, $colWebsite).Value()
                $testResult=Confirm-WebSite -Url $txtWebSite
                $detectResult=[PSCustomObject]@{
                    UniversityId = [string]$txtUniversityId
                    UniversityName=$txtUniversityName
                    WebSite= $txtWebSite
                    StatusCode= $testResult[1]
                    IsAccess=$testResult[0]
                }
                $detectResults+=$detectResult
                
                Write-information -MessageData "$($detectResult.UniversityName) is tested" -InformationAction Continue 
            }
            $detectResults | select-object UniversityId,UniversityName,WebSite ,StatusCode,IsAccess| Export-Csv -Path $Output -Encoding utf8BOM  -Append
        }
        $objExcel.Quit()
        return $totalNoOfRecords
    }