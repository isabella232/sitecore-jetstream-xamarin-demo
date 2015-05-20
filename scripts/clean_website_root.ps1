# Get-ChildItem -Path "C:\dev\jetstream_doc" -Recurse -Exclude "changes.txt" | foreach ($_) {
Get-ChildItem -Path $Args[0] -Recurse -Exclude $Args[1] | foreach ($_) {
    "CLEANING :" + $_.fullname
    Remove-Item $_.fullname -Force -Recurse
    "CLEANED... :" + $_.fullname
}