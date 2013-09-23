<?php
require 'core/init.php';
$general->logged_in_protect();

if (empty($_POST) === false) {

	$username = trim($_POST['username']);
	$password = trim($_POST['password']);

	if (empty($username) === true || empty($password) === true) 
	{
		$errors[] = 'Sorry, but we need your username and password.';
	}
	else if ($users->user_exists($username) === false) 
	{
		$errors[] = 'Sorry that username doesn\'t exists.';
	} 
	else if ($users->email_confirmed($username) === false) 
	{
		$errors[] = 'Sorry, but you need to activate your account. 
					 Please check your email.';
	}
	else 
	{
		if (strlen($password) > 32) 
		{
			$errors[] = 'The password should be less than 32 characters, without spacing.';
		}
		$login = $users->login($username, $password);
		if ($login === false) 
		{
			$errors[] = 'Sorry, that username/password is invalid';
		}
		else 
		{
			session_regenerate_id(true);// destroying the old session id and creating a new one
			$_SESSION['Id'] =  $login;
			header('Location: home.php');
			exit();
		}
	}
} 
?>
<!doctype html>
<html lang="en">
<head>
	<meta charset="UTF-8">
	<link rel="stylesheet" type="text/css" href="css/style.css" >
	<title>Login</title>
</head>
<body>	
	<div id="container">
	<?php include 'includes/menu.php'; ?>

		<h1>Login</h1>

		<?php 
		if(empty($errors) === false)
		{
			echo '<p>' . implode('</p><p>', $errors) . '</p>';	
		}
		?>

		<form method="post" action="">
			<h4>Username:</h4>
			<input type="text" name="username" value="<?php if(isset($_POST['username'])) echo htmlentities($_POST['username']); ?>" />
			<h4>Password:</h4>
			<input type="password" name="password" />
			<br>
			<input type="submit" name="submit" />
		</form>
		<br>
		<a href="confirm-recover.php">Forgot your username/password?</a>

	</div>
</body>
</html>