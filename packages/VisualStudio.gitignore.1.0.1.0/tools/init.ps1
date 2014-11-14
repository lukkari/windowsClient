param($installPath, $toolsPath, $package, $project)
Write-Host installing...
Invoke-Webrequest https://github.com/github/gitignore/raw/master/VisualStudio.gitignore -OutFile .\.gitignore
Write-Host installed
