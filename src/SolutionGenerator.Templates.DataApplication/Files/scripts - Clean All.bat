REM Deleting packages
for /d %%p in (".\lib\*.*") do rmdir "%%p" /s /q
for /F "tokens=*" %%G IN ('DIR /B /AD /S bin') DO RMDIR /S /Q "%%G"
for /F "tokens=*" %%G IN ('DIR /B /AD /S obj') DO RMDIR /S /Q "%%G"

REM Deleting output
rmdir .\output /s /q

pause