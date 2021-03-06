@{

RootModule = 'bin\PSTemperature.dll'
ModuleVersion = '1.0.0'
CompatiblePSEditions = 'Core'
PowerShellVersion = '7.1.3'
GUID = '88fb4cde-8a02-4fce-8ad6-9927ae59b181'
Author = 'Dave Carroll'
CompanyName = 'thedavecarroll'
Copyright = '(c) 2020-2021 Dave Carroll. All rights reserved.'
Description = 'PowerShell binary module used for simple conversion of Celsius, Fahrenheit, Kelvin, and Rankine temperatures.'

PrivateData = @{

    PSData = @{

        ExperimentalFeatures = @(
            @{
                Name = 'PSTemperature.SupportRankine'
                Description = 'Support Rankine Temperature Scale'
            }
        )

    }

}

}
