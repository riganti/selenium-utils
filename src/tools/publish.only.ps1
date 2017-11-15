param([String]$source, [String]$key)

$files = dir $PWD -Recurse | where {$_.Name.EndsWith(".nupkg")} | where {!$_.FullName.Contains("packages")}

foreach($file in $files)
{
    echo "Uploading package: " + $file.FullName 
    & .\nuget.exe push $file.FullName -source $source -apiKey $key
}

