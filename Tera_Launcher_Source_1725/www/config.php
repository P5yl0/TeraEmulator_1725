<?php

//change connection & password to your suits
$conn=mysql_connect('localhost','root','PASSWORD') or die('mysql_error()');

mysql_select_db('tera', $conn);

if (!$conn) 
{
    die('Could not connect: ' . mysql_error());
}

?>