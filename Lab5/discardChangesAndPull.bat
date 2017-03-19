:choice
set /P c=Are you sure you want to continue[Y/N]?
if /I "%c%" EQU "Y" goto :checkout
if /I "%c%" EQU "N" goto :quit
goto :choice
:checkout
REM discard local changes for this directory
git checkout -- .
REM pull most recent version.
git pull
:quit