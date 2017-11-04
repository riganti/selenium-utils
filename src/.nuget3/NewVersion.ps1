param([String]$version)

if ($version.Contains("-")) {
	$versionWithoutPre = $version.Substring(0, $version.IndexOf("-"))
} 
else {
	$versionWithoutPre = $version
}

$nuspecPath = ".\Riganti.Utils.Testing.dotvvm.nuspec"
$assemblyInfoPath = "..\Riganti.Selenium.Dotvvm\Properties\AssemblyInfo.cs"



# change the nuspec
$nuspec = [System.IO.File]::ReadAllText($nuspecPath, [System.Text.Encoding]::UTF8)
$nuspec = [System.Text.RegularExpressions.Regex]::Replace($nuspec, "\<version\>([^""]*)\</version\>", "<version>" + $version + "</version>")
[System.IO.File]::WriteAllText($nuspecPath, $nuspec, [System.Text.Encoding]::UTF8)


# change the AssemblyInfo project file
$assemblyInfo = [System.IO.File]::ReadAllText($assemblyInfoPath, [System.Text.Encoding]::UTF8)
$assemblyInfo = [System.Text.RegularExpressions.Regex]::Replace($assemblyInfo, "\[assembly: AssemblyVersion\(""([^""]*)""\)\]", "[assembly: AssemblyVersion(""" + $versionWithoutPre + """)]")
$assemblyInfo = [System.Text.RegularExpressions.Regex]::Replace($assemblyInfo, "\[assembly: AssemblyFileVersion\(""([^""]*)""\)]", "[assembly: AssemblyFileVersion(""" + $versionWithoutPre + """)]")
[System.IO.File]::WriteAllText($assemblyInfoPath, $assemblyInfo, [System.Text.Encoding]::UTF8)
