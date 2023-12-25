# Get the path of the current script
$projectPath = Split-Path -Parent $MyInvocation.MyCommand.Definition

# Define the path to the artifacts directory
$artifactsPath = Join-Path -Path $projectPath -ChildPath ".artifacts"

# Create the artifacts directory if it doesn't exist
if(!(Test-Path -Path $artifactsPath )){
    New-Item -ItemType Directory -Path $artifactsPath
}

# Build the .NET project and publish the output to the artifacts directory
Set-Location -Path $projectPath
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=false /p:IncludeNativeLibrariesForSelfExtract=true -o $artifactsPath