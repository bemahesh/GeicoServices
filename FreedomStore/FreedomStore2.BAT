for %%G in (*.sql) do sqlcmd /S DESKTOP-ABU18QC /d FreedomStore -U FreedomStoreUser -P myFreedom123 -i"%%G"
pause