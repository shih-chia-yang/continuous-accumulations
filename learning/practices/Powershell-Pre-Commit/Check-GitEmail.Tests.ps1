BeforeAll{
    . $PSScriptRoot/Check-GitEmail.ps1
    $fakeEmail="test@yuntech.edu.tw"
}

Describe "Pre-Commit" {

    It 'given empty string , it return 1'{
        $exitCode = Check-GitEmail 
        $exitCode | Should -Be 1 
    }
    It 'given yuntech email, it return 0 '{
        $exitCode = Check-GitEmail -Email $fakeEmail
        $exitCode | Should -Be 0
    }
}