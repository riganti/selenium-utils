dir $PWD\.. -Recurse | where {$_.Name.EndsWith(".nupkg")} | where {!$_.FullName.Contains("packages")} | Remove-Item -Force | Write-Host
& dotnet build ..\Riganti.Utils.Testing.Selenium.WithoutCoordinator.sln -c release | Write-Host
& dotnet pack ..\Core\Riganti.Utils.Testing.Selenium.Core.Abstractions  -c release | Write-Host

$files = dir $PWD\.. -Recurse | where {$_.Name.EndsWith(".nupkg")} | where {!$_.FullName.Contains("packages")} | where {!$_.FullName.Contains("symbols.nupkg")}

dir -Directory "publish*" | rmdir -Recurse -Force | Write-Host
mkdir .\publish | Write-Host

foreach($file in $files)
{
$name = ".\publish\" + $file.Name 
Copy-Item -Destination $name -Path $file.FullName -Force | Write-Host
}