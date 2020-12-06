param([String]$source, [String]$key)

echo "Working directory:" 
$PWD | Write-host  

& dir $PWD\.. -Recurse | where {$_.Name.EndsWith(".nupkg")} | where {!$_.FullName.Contains("packages")} | Remove-Item -Force | Write-Host

dotnet restore ..\Riganti.Selenium.WithoutCoordinator.sln
dotnet build ..\Riganti.Selenium.WithoutCoordinator.sln -c Release

dotnet pack -c  Release --no-build ..\Coordinator\Riganti.Selenium.Coordinator.Client\Riganti.Selenium.Coordinator.Client.csproj            
dotnet pack -c  Release --no-build ..\Core\Riganti.Selenium.AssertApi\Riganti.Selenium.AssertApi.csproj                                     
dotnet pack -c  Release --no-build ..\Core\Riganti.Selenium.Core\Riganti.Selenium.Core.csproj                                               
dotnet pack -c  Release --no-build ..\Core\Riganti.Selenium.Core.Abstractions\Riganti.Selenium.Core.Abstractions.csproj                     
dotnet pack -c  Release --no-build ..\Core\Riganti.Selenium.LambdaApi\Riganti.Selenium.LambdaApi.csproj                                     
dotnet pack -c  Release --no-build ..\Core\Riganti.Selenium.PseudoFluentApi\Riganti.Selenium.FluentApi.csproj                         
dotnet pack -c  Release --no-build ..\Core\Riganti.Selenium.Validators\Riganti.Selenium.Validators.csproj                                   
dotnet pack -c  Release --no-build ..\Integrations\Riganti.Selenium.DotVVM.MSTest2\Riganti.Selenium.DotVVM.csproj
dotnet pack -c  Release --no-build ..\Integrations\Riganti.Selenium.MSTest2Integration\Riganti.Selenium.MSTest2Integration.csproj           
dotnet pack -c  Release --no-build ..\Integrations\Riganti.Selenium.MSTestIntegration\Riganti.Selenium.MSTestIntegration.csproj             
dotnet pack -c  Release --no-build ..\Integrations\Riganti.Selenium.xUnit\Riganti.Selenium.xUnitIntegration.csproj        

$files = dir $PWD\.. -Recurse | where {$_.Name.EndsWith(".nupkg")} | where {!$_.FullName.Contains("packages")} | where {!$_.FullName.Contains(".symbols.nupkg")}

foreach($file in $files)
{
    echo "Uploading package: " + $file.FullName 
    & .\nuget.exe push $file.FullName -source $source -apiKey $key
}

$files = dir $PWD\.. -Recurse | where {$_.Name.EndsWith(".snupkg")} | where {!$_.FullName.Contains("packages")} | where {!$_.FullName.Contains(".symbols.snupkg")}

foreach($file in $files)
{
    echo "Uploading package: " + $file.FullName 
    & .\nuget.exe push $file.FullName -source $source -apiKey $key
}

