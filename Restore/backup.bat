@echo Backup database  %PG_PATH%%PG_FILENAME%
@echo off
SET PG_BIN="C:\Program Files\PostgreSQL\13\bin\pg_dump.exe"
SET PG_HOST=localhost
SET PG_PORT=5433
SET PG_DATABASE=gestor_casos
SET PG_USER=postgres
SET PGPASSWORD=123456
SET PG_PATH=C:\shared\Restore\backup
SET FECHAYHORA=%date:/=%-%time:-0,8%
SET FECHAYHORA=%FECHAYHORA::=-%
SET FECHAYHORA=%FECHAYHORA: =0%
SET PG_FILENAME=%PG_PATH%\%PG_DATABASE%_%d%_%t%.sql

%PG_BIN% -h %PG_HOST% -p %PG_PORT% -U %PG_USER% %PG_DATABASE% > %PG_FILENAME%

@echo Backup Taken Complete %PG_PATH%%PG_FILENAME%