#!/bin/bash
dotnet tool restore

dotnet cake setup.cake --bootstrap

dotnet cake setup.cake "$@"
