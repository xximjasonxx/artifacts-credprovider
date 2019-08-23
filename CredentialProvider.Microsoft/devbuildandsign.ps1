dotnet publish -f netcoreapp2.1
dotnet publish -f net461
signtool sign bin\Debug\netcoreapp2.1\publish\CredentialProvider.Microsoft.dll
signtool sign bin\Debug\net461\publish\CredentialProvider.Microsoft.exe
copy -Recurse -Force bin\Debug\netcoreapp2.1\publish\* $env:UserProfile\.nuget\plugins\netcore\CredentialProvider.Microsoft\
copy -Recurse -Force bin\Debug\net461\publish\* $env:UserProfile\.nuget\plugins\netfx\CredentialProvider.Microsoft\