@echo off
if "%1"=="--reset" (
    echo Resetting database...
    del app.db 2>nul
    echo Database reset complete.
    echo Running migrations...
    dotnet ef database update
    echo Migrations complete.
)
echo Starting application...
dotnet run