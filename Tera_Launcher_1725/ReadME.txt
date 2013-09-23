Setup TERA EU/1725 Launcher web files

Launcher:
---------------------
../build folder: includes compiled Launcher & image files
cTlauncher.rar > ready to use launcher, copy to tera and start! 

---------------------
../www wolder:
includes all web based files needed for the launcher....
copy all files to your web host/root... (../xampp/htdocs/ ) 
(dont use any folders, else you have to edit the launcher source code and www/ *.php files!)

---------------------
/tera-database.sql:
includess all SQL tables for launcher & gameserver...
import to your database "tera"
(if you use another database, you have to edit gameserver config files and 
/www/core/connect/database.php

--------------------
for email functionality edit this 2 files, \www\core\classes\users.php & class.phpmailer.php
edit: users.php > function sendmail and add a email/hoster
edit: class.phpmailer.php > line ~260.. add email&pass from your hoster
you dont need this but if you dont do this edits email functions are not working 
