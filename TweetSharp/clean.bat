@echo off
echo Deleting all bin and obj folders...
rmdir /s /q bin\net35
rmdir /s /q bin\net40
pushd src
for /f "tokens=*" %%i in ('DIR /B /AD /S obj') do rmdir /s /q %%i 
for /f "tokens=*" %%i in ('DIR /B /AD /S bin') do rmdir /s /q %%i 
popd
