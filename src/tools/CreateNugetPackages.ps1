param([String]$version)

echo "Working directory:" 
$PWD | Write-host  

& dir $PWD\.. -Recurse | where {$_.Name.EndsWith(".nupkg")} | where {!$_.FullName.Contains("packages")} | Remove-Item -Force | Write-Host

dotnet pack -c  Release ..\Coordinator\Riganti.Selenium.Coordinator.Client\Riganti.Selenium.Coordinator.Client.csproj     /p:Version=$version     
dotnet pack -c  Release ..\Core\Riganti.Selenium.AssertApi\Riganti.Selenium.AssertApi.csproj                              /p:Version=$version   
dotnet pack -c  Release ..\Core\Riganti.Selenium.Core\Riganti.Selenium.Core.csproj                                        /p:Version=$version   
dotnet pack -c  Release ..\Core\Riganti.Selenium.Core.Abstractions\Riganti.Selenium.Core.Abstractions.csproj              /p:Version=$version   
dotnet pack -c  Release ..\Core\Riganti.Selenium.LambdaApi\Riganti.Selenium.LambdaApi.csproj                              /p:Version=$version   
dotnet pack -c  Release ..\Core\Riganti.Selenium.PseudoFluentApi\Riganti.Selenium.FluentApi.csproj                        /p:Version=$version
dotnet pack -c  Release ..\Core\Riganti.Selenium.Validators\Riganti.Selenium.Validators.csproj                            /p:Version=$version   
dotnet pack -c  Release ..\Integrations\Riganti.Selenium.DotVVM.MSTest2\Riganti.Selenium.DotVVM.csproj                    /p:Version=$version
dotnet pack -c  Release ..\Integrations\Riganti.Selenium.MSTest2Integration\Riganti.Selenium.MSTest2Integration.csproj    /p:Version=$version   
dotnet pack -c  Release ..\Integrations\Riganti.Selenium.MSTestIntegration\Riganti.Selenium.MSTestIntegration.csproj      /p:Version=$version  
dotnet pack -c  Release ..\Integrations\Riganti.Selenium.xUnit\Riganti.Selenium.xUnitIntegration.csproj                              /p:Version=$version 

$files = dir $PWD\.. -Recurse | where {$_.Name.EndsWith(".nupkg")} | where {!$_.FullName.Contains("packages")} | where {$_.FullName.Contains("Riganti.Selenium.")}
$dir = dir $PWD | where {$_.Name -eq "Artifacts"} 

if ($dir) {
    Remove-Item $dir -Force -Recurse
}
mkdir Artifacts


foreach($file in $files)
{
    echo "Copy file: " $file.FullName 
    echo "to output folder: " "Artifacts\\$file" 
    Copy-Item $file.FullName -Destination "Artifacts\\$file" -Force 
}

