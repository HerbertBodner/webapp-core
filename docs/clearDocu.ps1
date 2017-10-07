
Remove-Item -recurse -force -ErrorAction SilentlyContinue docs/_site
Remove-Item -recurse -force -ErrorAction SilentlyContinue docs/obj
Remove-Item -recurse -force -ErrorAction SilentlyContinue docfx.console.2.24.0/
Remove-Item -recurse -force -ErrorAction SilentlyContinue api
Remove-Item -recurse -force -ErrorAction SilentlyContinue origin_site/
Remove-Item docs/api/*.yml
Remove-Item docs/api/.manifest -ErrorAction SilentlyContinue
Remove-Item docs/index.md -ErrorAction SilentlyContinue

