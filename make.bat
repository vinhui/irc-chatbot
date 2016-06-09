
:: @project >> Internet Relay Chat
:: @authors >> DeeQuation
:: @version >> 04.00.00
:: @release >> 08.06.16
:: @licence >> MIT

@echo off

:: Checks if there's already a ~\bin\ directory and recover files.
if exist "bin\" (
    
    if exist "bin\logs\" (
        
		for %%f in ("bin\logs\*.txt") do (
			
			xcopy /s /y %%f "src\logs\" > nul
		)
    )
    
    if exist "bin\data\" (
		
		for %%f in ("bin\data\*.txt") do (
			
			xcopy /s /y %%f "src\data\" > nul
		)
    )
	
	rd "bin\" /s /q
)

:: Recreates the ~\bin\ directory and hierarchy.
mkdir bin
mkdir bin\logs
mkdir bin\logins

:: Compiles the base executable and packages in the ~\bin\ directory.
csc /out:bin\base.exe "src\kernels\*.cs" "src\modules\*.cs"

:: Moves all login files to the ~\bin\login\ directory.
for %%f in ("src\logins\*.txt") do (
	
	xcopy /s /y %%f "bin\logins\" > nul
)

:: Moves all login files to the ~\bin\login\ directory.
for %%f in ("src\data\*.txt") do (
	
	xcopy /s /y %%f "bin\data\" > nul
)

pause
