<?php 
require 'core/init.php';
$general->logged_in_protect();

if (isset($_POST['submit'])) 
{
	if(empty($_POST['username']) || empty($_POST['password']) || empty($_POST['email']) || empty($_POST['first_name']) || empty($_POST['last_name']))
	{
		$errors[] = 'All fields are required.';
	}
	else
	{
        if ($users->user_exists($_POST['username']) === true) 
		{
            $errors[] = 'That username already exists';
        }
        if(!ctype_alnum($_POST['username']))
		{
            $errors[] = 'Please enter a username with only alphabets and numbers';	
        }
        if (strlen($_POST['password']) <4)
		{
            $errors[] = 'Your password must be atleast 4 characters';
        } 
		else if (strlen($_POST['password']) >32)
		{
            $errors[] = 'Your password cannot be more than 32 characters long';
        }
        if (filter_var($_POST['email'], FILTER_VALIDATE_EMAIL) === false)
		{
            $errors[] = 'Please enter a valid email address';
        }
		else if ($users->email_exists($_POST['email']) === true)
		{
            $errors[] = 'That email already exists.';
        }
	}

	if(empty($errors) === true)
	{
		$username 	= htmlentities($_POST['username']);
		$first_name = htmlentities($_POST['first_name']);
		$last_name 	= htmlentities($_POST['last_name']);
		
		$password 	= $_POST['password'];
		$email 		= htmlentities($_POST['email']);

		$users->register($username, $first_name, $last_name, $password, $email );
		header('Location: register.php?success');
		exit();
	}
}

?>
<!doctype html>
<html lang="en">
<head>
	<meta charset="UTF-8">
	<link rel="stylesheet" type="text/css" href="css/style.css" >
	<title>Register</title>
</head>
<body>	
	<div id="container">
		<?php include 'includes/menu.php'; ?>
		<h1>Register</h1>
		
		<?php
		if (isset($_GET['success']) && empty($_GET['success'])) {
		  echo 'Thank you for registering. Please check your email.';
		}
		?>

		<form method="post" action="">
			<h4>Username:</h4>
			<input type="text" name="username" value="<?php if(isset($_POST['username'])) echo htmlentities($_POST['username']); ?>" >
			<h4>FirstName:</h4>
			<input type="text" name="first_name" value="<?php if(isset($_POST['first_name'])) echo htmlentities($_POST['first_name']); ?>" >
			<h4>LastName:</h4>
			<input type="text" name="last_name" value="<?php if(isset($_POST['last_name'])) echo htmlentities($_POST['last_name']); ?>" >
			<h4>Password:</h4>
			<input type="password" name="password" />
			<h4>Email:</h4>
			<input type="text" name="email" value="<?php if(isset($_POST['email'])) echo htmlentities($_POST['email']); ?>"/>	
			<br>
			<input type="submit" name="submit" />
		</form>

		<?php 
		if(empty($errors) === false)
		{
			echo '<p>' . implode('</p><p>', $errors) . '</p>';	
		}
		?>
	</div>
</body>
</html>



























