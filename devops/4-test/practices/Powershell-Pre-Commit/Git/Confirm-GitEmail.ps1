function Confirm-GitEmail ( 
    [ValidateNotNullOrEmpty()]
    [string]$Email
    )
    {
        if($Email.Trim().Length -eq 0)
        {
            return 1
        }
        # Verify user's Git config has appropriate email address
        if ($email -notmatch '@(non\.)?yuntech.edu\.tw$') {
	
            Write-Warning "Your Git email address '$email' is not configured correctly."
            Write-Warning "It should end with '@yuntech.edu.tw'."
            Write-Warning "Use the command: 'git config --global user.email <name@yuntech.edu.tw>' to set it correctly."
            return 1
        }
    return 0
}