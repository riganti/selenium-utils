param([String]$key)

del ./Riganti.Utils.Testing.Selenium.*.nupkg

& .\NuGet.exe pack "./Riganti.Utils.Testing.Selenium.Dotvvm.nuspec"

$file = dir ./Riganti.Utils.Testing.Selenium.*.nupkg
$file = $file.Name

& .\NuGet.exe push $file -apikey $key  -source "https://api.nuget.org/v3/index.json" 