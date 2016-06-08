
@echo off

:: Checks if there's already a ~\bin\ directory.
if exist "%cd%\bin\" (
    
    if exist "%cd%\bin\log\" (
        
        for %%f in (bin\log\*.txt) do (
            
            xcopy /s /y %%f src\log\ > nul
        )
    )
    
    if exist "%cd%\bin\data\" (
        
        for %%f in (bin\data\*.txt) do (
            
            xcopy /s /y %%f src\data\ > nul
        )
    )
    
    rd "%cd%\bin\" /s /q
)

:: Creates the bin directory hierarchy.
mkdir bin
mkdir bin\log
mkdir bin\login

:: Compiles the base executable with the default parser.
csc /out:bin\base.exe "src\base\*.cs" "src\module\*.cs" "src\system\*.cs"

:: Moves all login files to the ~\bin\login\ directory.
for %%f in (src\login\*.txt) do (
    
    xcopy /s /y %%f bin\login\ > nul
)

:: Moves all login files to the ~\bin\login\ directory.
for %%f in (src\data\*.txt) do (
    
    xcopy /s /y %%f bin\data\ > nul
)

pause

