#!/usr/bin/env pwsh

$fileName = 'Photon-Server-Plugin-SDK_v5-0-12-24499-RC1.zip'

Start-Process 'https://id.photonengine.com/Account/SignIn'
Write-Host 'Please sign in'
Pause

Start-Process "https://dashboard.photonengine.com/download/$fileName"
Write-Host 'Wait for download'
Pause

while ($true) {
  if (Test-Path $fileName) {
    break
  }
  if (Test-Path "~/Downloads/$fileName") {
    Copy-Item "~/Downloads/$fileName"
    break
  }
  Write-Host "Please copy `"$fileName`" into `"$(git rev-parse --show-toplevel)/`""
  Pause
}

Expand-Archive $fileName 'Photon-Server-Plugin-SDK'
