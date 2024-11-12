#!/bin/bash
cd /app
dotnet tool install --global dotnet-ef
/root/.dotnet/tools/dotnet-ef database update
