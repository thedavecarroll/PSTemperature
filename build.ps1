$Module = 'PSTemperature'

Push-Location $PSScriptRoot

dotnet build $PSScriptRoot\src -o $PSScriptRoot\Output\$module\bin
Copy-Item "$PSScriptRoot\$module\*" "$PSScriptRoot\Output\$module" -Recurse -Force

Copy-Item "$PSScriptRoot\Output\$module" "C:\Program Files\PowerShell\Modules" -Recurse -Force