BeforeAll{
    . $PSScriptRoot/Confirm-GitEmail.ps1
    $fakeEmail="test@yuntech.edu.tw"
}

Describe "Validate Email" {

    It 'given empty string , it return 1'{
        $exitCode = Confirm-GitEmail 
        $exitCode | Should -Be 1 
    }
    It 'given yuntech email, it return 0 '{
        $exitCode = Confirm-GitEmail -Email $fakeEmail
        $exitCode | Should -Be 0
    }
}