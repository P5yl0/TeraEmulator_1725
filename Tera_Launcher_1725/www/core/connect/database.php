<?php 
$config = array(
	'host'		=> 'localhost',
	'username' 	=> 'root',
	'password' 	=> 'PASSWORD',
	'dbname' 	=> 'tera'
);

$db = new PDO('mysql:host=' . $config['host'] . ';dbname=' . $config['dbname'], $config['username'], $config['password']);
$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);