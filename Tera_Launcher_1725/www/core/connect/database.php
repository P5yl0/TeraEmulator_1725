<?php 
$config = array(
	'host'    => '127.0.0.1',  //Must be IP or FQDN. Don't use localhost or it'll use a UNIX Socket. 
	'port'    => '3306', 
	'username' 	=> 'root',
	'password' 	=> 'PASSWORD',
	'dbname' 	=> 'tera'
);

$db = new PDO('mysql:host=' . $config['host'] . ';port=' . $config['port'] .  ';dbname=' . $config['dbname'], $config['username'], $config['password']); 
$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);