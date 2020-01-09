if (!$PSScriptRoot) {
    $PSScriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Definition
}

Push-Location $PSScriptRoot

try {
    $SCRIPT_DIR = Split-Path -Parent $MyInvocation.MyCommand.Definition
    $TOOLS_DIR = "$SCRIPT_DIR/tools"
    if ($IsMacOS -or $IsLinux) {
        $CAKE_EXE = "$TOOLS_DIR/dotnet-cake"
    }
    else {
        $CAKE_EXE = "$TOOLS_DIR/dotnet-cake.exe"
    }

    $DOTNET_EXE = "$(Get-Command dotnet -ea 0 | select -Expand Source)"
    $INSTALL_NETCORE = $false
    [string]$DOTNET_VERSION = ""
    [string]$CAKE_VERSION = ""
    foreach ($line in Get-Content "$SCRIPT_DIR/build.config" -Encoding utf8) {
        if ($line -like "CAKE_VERSION=*") {
            $CAKE_VERSION = $line.Substring($line.IndexOf('=') + 1)
        }
        elseif ($line -like "DOTNET_VERSION=*") {
            $DOTNET_VERSION = $line.Substring($line.IndexOf('=') + 1)
        }
    }

    if ([string]::IsNullOrWhiteSpace($CAKE_VERSION) -or [string]::IsNullOrEmpty($DOTNET_VERSION)) {
        "An errer occured while parsing Cake / .NET Core SDK version."
        exit 1
    }

    if ([string]::IsNullOrWhiteSpace($DOTNET_EXE) -or !(. dotnet --list-sdks)) {
        $INSTALL_NETCORE = $true
    }
    elseif ("$DOTNET_VERSION" -ne "ANY") {
        $DOTNET_INSTALLED_VERSION = . $DOTNET_EXE --version
        if ("$DOTNET_VERSION" -ne "$DOTNET_INSTALLED_VERSION") {
            $INSTALL_NETCORE = $true
        }
    }

    if ($true -eq $INSTALL_NETCORE) {
        if (!(Test-Path "$SCRIPT_DIR/.dotnet")) {
            New-Item -Path "$SCRIPT_DIR/.dotnet" -ItemType Directory -Force | Out-Null
        }

        $arguments = @()
        $ScriptPath = ""
        $LaunchUrl = ""
        $ScriptUrl = ""
        $PathSep = ';'
        if ($IsMacOS -or $IsLinux) {
            $ScriptPath = "$SCRIPT_DIR/.dotnet/dotnet-install.sh"
            $ScriptUrl = "https://dot.net/v1/dotnet-install.sh"
            $LaunchUrl = "$(Get-Command bash)"
            $PathSep = ":"
            $arguments = @(
                $ScriptPath
                "--install-dir"
                "$SCRIPT_DIR/.dotnet"
                "--no-path"
            )
            if ($DOTNET_VERSION -ne "ANY") {
                $arguments += @(
                    "--version"
                    "$DOTNET_VERSION"
                )
            }
        }
        else {
            $ScriptPath = "$SCRIPT_DIR/.dotnet/dotnet-install.ps1"
            $ScriptUrl = "https://dot.net/v1/dotnet-install.ps1"
            $LaunchUrl = "$ScriptPath"
            $arguments = @(
                "-InstallDir"
                "$SCRIPT_DIR/.dotnet"
                "-NoPath"
            )
            if ($DOTNET_VERSION -ne "ANY") {
                $arguments += @(
                    "-Version"
                    "$DOTNET_VERSION"
                )
            }
        }

        (New-Object System.Net.WebClient).DownloadFile($ScriptUrl, $ScriptPath)

        & $LaunchUrl @arguments

        $env:PATH = "$SCRIPT_DIR/.dotnet${PathSep}${env:PATH}"
        $env:DOTNET_ROOT = "$SCRIPT_DIR/.dotnet"

        $DOTNET_EXE = Get-ChildItem -Path "$SCRIPT_DIR/.dotnet" -Filter "dotnet*" | Select-Object -First 1 -Expand FullName

    }
    elseif (Test-Path "/opt/dotnet/sdk" -ea 0) {
        $env:DOTNET_ROOT = "/opt/dotnet"
    }

    $env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 1
    $env:DOTNET_CLI_TELEMETRY_OPTOUT = 1
    $env:DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER = 0

    $CAKE_INSTALLED_VERSION = Get-Command dotnet-cake -ea 0 | ForEach-Object { & $_.Source --version }

    if ($CAKE_INSTALLED_VERSION -eq $CAKE_VERSION) {
        $CAKE_EXE = Get-Command dotnet-cake | ForEach-Object Source
    }
    else {
        $CakePath = "$TOOLS_DIR/.store/cake.tool/$CAKE_VERSION"
        $CAKE_EXE = (Get-ChildItem -Path $TOOLS_DIR -Filter "dotnet-cake*" -File -ea 0 | Select-Object -First 1 -Expand FullName)

        if (!(Test-Path -Path $CakePath -PathType Container) -or !(Test-Path $CAKE_EXE -PathType Leaf)) {
            if (!([string]::IsNullOrWhiteSpace($CAKE_EXE)) -and (Test-Path $CAKE_EXE -PathType Leaf)) {
                & $DOTNET_EXE tool uninstall --tool-path $TOOLS_DIR Cake.Tool
            }

            & $DOTNET_EXE tool install --tool-path $TOOLS_DIR --version $CAKE_VERSION Cake.Tool
            if ($LASTEXITCODE -ne 0) {
                "An error occured while installing Cake."
                exit 1
            }

            $CAKE_EXE = (Get-ChildItem -Path $TOOLS_DIR -Filter "dotnet-cake*" -File | Select-Object -First 1 -Expand FullName)
        }
    }

    & "$CAKE_EXE" setup.cake --bootstrap
    $exitCode = $LASTEXITCODE
    if ($exitCode -eq 0) {
        & "$CAKE_EXE" setup.cake $args
        $exitCode = $LASTEXITCODE
    }
}
catch {
    Pop-Location
    $exitCode = 1
}

exit $exitCode
