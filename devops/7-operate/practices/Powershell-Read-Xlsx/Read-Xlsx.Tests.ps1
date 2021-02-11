BeforeAll {
    . $PSScriptRoot/Read-Xlsx.ps1
    . $PSScriptRoot/Confirm-WebSite.ps1
}

Describe "Read-Xlsx" {
    Context "read data from xlsx" {
        It "given financeWebsite.xlsx file ,should be return 57 rowCount" {
            $fakeFilePath="e:\test.csv"
            $rowCount =Read-Xlsx -Path "E:\Test\read-xlsx\FinanceWebSite.xlsx" -OutPut $fakeFilePath
            $rowCount | Should -be 8
        }
    }

    Context "Confirm Website is accessible" {
        It "given a website should be return status" {
            $fakeUrl="http://account.nchu.edu.tw/nchu/index.aspx" 
            $status =Confirm-WebSite -Url $fakeUrl
            $status[0] | Should -be $false
        }
    }

    AfterAll{

    }
}

