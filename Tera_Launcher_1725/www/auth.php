<?php
require 'core/init.php';
$general->logged_in_protect();

if (empty($_POST) === false) 
{
	$username = trim($_POST['username']);
	$password = trim($_POST['password']);

	if (empty($username) === true || empty($password) === true) 
	{
		echo ('Login_NoInput');
	}
	else if ($users->user_exists($username) === false) 
	{
		echo ('Login_NoUser');
		exit();
	} 
	else if (strlen($password) >32 || strlen($password) <4)
	{
		echo ('Login_ErrorPassLength');
		exit();
	} 
	else 
	{
		$login = $users->login($username, $password);
		//login success
		if ($login) 
		{
			session_regenerate_id(true);// destroying the old session id and creating a new one
			$_SESSION['Id'] =  $login;
			echo ('1');
			exit();
		}
		//login failed
		else 
		if ($login === false) 
		{
			echo ('0');
			exit();
		}

	}
} 
?>

		
		