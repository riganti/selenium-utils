param([string] $file, [String]$key)

& .\NuGet.exe push $file -apikey $key  -source "https://api.nuget.org/v3/index.json" 