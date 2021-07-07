$Module = 'PSTemperature'

Push-Location $PSScriptRoot

dotnet build $PSScriptRoot\src -o $PSScriptRoot\Output\$module\bin
Copy-Item "$PSScriptRoot\$module\*" "$PSScriptRoot\Output\$module" -Recurse -Force

Compress-Archive -Path "$PSScriptRoot\Output\$module" -DestinationPath "$PSScriptRoot\Output\$module.zip" -Force