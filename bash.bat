@echo off
chcp 65001 >nul
setlocal

REM ==== НАСТРОЙКИ ====
set REPO_URL=https://github.com/Shishakkk/Razzor.git
set BRANCH=main
set COMMIT_MSG=%~1
if "%COMMIT_MSG%"=="" set COMMIT_MSG=Auto commit %date% %time%
REM ====================

echo [1/5] Проверка git...
where git >nul 2>&1 || (echo Git не установлен! & pause & exit /b 1)

if not exist ".git" (
    echo [2/5] Инициализация репозитория...
    git init
    git remote add origin %REPO_URL%
    git branch -M %BRANCH%
) else (
    echo [2/5] Репозиторий уже инициализирован.
    git remote set-url origin %REPO_URL%
)

echo [3/5] Добавление файлов...
git add .

echo [4/5] Коммит: %COMMIT_MSG%
git commit -m "%COMMIT_MSG%"

echo [5/5] Отправка на GitHub (%BRANCH%)...
git push -u origin %BRANCH%

echo.
echo Готово! Открыть репозиторий: %REPO_URL%
pause