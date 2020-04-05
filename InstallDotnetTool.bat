dotnet pack -c Release UpdatePackagesWithSuffix\UpdatePackagesWithSuffix.csproj -o ".\artifacts"

dotnet tool uninstall dotnet-update-packages-with-suffix --global

dotnet tool install dotnet-update-packages-with-suffix --global --add-source ".\artifacts" --version "1.0.1-alpha"