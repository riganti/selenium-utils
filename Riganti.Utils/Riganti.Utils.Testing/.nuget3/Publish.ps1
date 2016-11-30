param([String]$key)

del ./Riganti.Utils.Testing.Selenium.Core.*.nupkg

& .\NuGet.exe pack "./Riganti.Utils.Testing.Selenium.Dotvvm.nuspec"

$file = dir ./Riganti.Utils.Testing.Selenium.Dotvvm.*.nupkg
$file = $file.Name

& .\NuGet.exe push $file $key