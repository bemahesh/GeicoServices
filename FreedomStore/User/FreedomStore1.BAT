
for %%G in (*.sql) do sqlcmd /S DESKTOP-ABU18QC /d master -E -i"%%G"
pause