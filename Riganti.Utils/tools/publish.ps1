param([String]$source, [String]$key)

& dir $PWD\.. -Recurse | where {$_.Name.EndsWith(".nupkg")} | where {!$_.FullName.Contains("packages")} | Remove-Item -Force | Write-Host

dotnet restore ..\Riganti.Utils.Testing.Selenium.WithoutCoordinator.sln
dotnet build ..\Riganti.Utils.Testing.Selenium.WithoutCoordinator.sln -c Release

dotnet pack -c  Release --no-build ..\Coordinator\Riganti.Utils.Testing.Selenium.Coordinator.Client\Riganti.Utils.Testing.Selenium.Coordinator.Client.csproj            
dotnet pack -c  Release --no-build ..\Core\Riganti.Utils.Testing.Selenium.AssertApi\Riganti.Utils.Testing.Selenium.AssertApi.csproj                                     
dotnet pack -c  Release --no-build ..\Core\Riganti.Utils.Testing.Selenium.Core\Riganti.Utils.Testing.Selenium.Core.csproj                                               
dotnet pack -c  Release --no-build ..\Core\Riganti.Utils.Testing.Selenium.Core.Abstractions\Riganti.Utils.Testing.Selenium.Core.Abstractions.csproj                     
dotnet pack -c  Release --no-build ..\Core\Riganti.Utils.Testing.Selenium.LambdaApi\Riganti.Utils.Testing.Selenium.LambdaApi.csproj                                     
dotnet pack -c  Release --no-build ..\Core\Riganti.Utils.Testing.Selenium.PseudoFluentApi\Riganti.Utils.Testing.Selenium.FluentApi.csproj                         
dotnet pack -c  Release --no-build ..\Core\Riganti.Utils.Testing.Selenium.Validators\Riganti.Utils.Testing.Selenium.Validators.csproj                                   
dotnet pack -c  Release --no-build ..\Integrations\Riganti.Utils.Testing.Selenium.MSTest2Integration\Riganti.Utils.Testing.Selenium.MSTest2Integration.csproj           
dotnet pack -c  Release --no-build ..\Integrations\Riganti.Utils.Testing.Selenium.MSTestIntegration\Riganti.Utils.Testing.Selenium.MSTestIntegration.csproj             
dotnet pack -c  Release --no-build ..\Integrations\Riganti.Utils.Testing.Selenium.xUnit\XUnitTestProject1\Riganti.Utils.Testing.Selenium.xUnitIntegration.csproj        

$files = dir $PWD\.. -Recurse | where {$_.Name.EndsWith(".nupkg")} | where {!$_.FullName.Contains("packages")} | where {!$_.FullName.Contains("symbols.nupkg")}

foreach($file in $files)
{
    echo "Uploading package: " + $file.FullName 
    & .\nuget.exe push $file.FullName -source $source -apiKey $key
}

