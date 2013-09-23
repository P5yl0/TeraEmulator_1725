<?php
require 'core/init.php';
$general->logged_in_protect();
?>
<!doctype html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <link rel="stylesheet" type="text/css" href="css/style.css" >
    <title>Recover Password</title>
</head>
<body>
    <div id="container">
        <?php include 'includes/menu.php'; ?>
    	<?php
        if (isset($_GET['success']) === true && empty ($_GET['success']) === true) 
		{
            ?>
            <h3>Thank you, we've send you a randomly generated password in your email.</h3>
            <?php
            
        } 
		else 
		if (isset ($_GET['email'], $_GET['password_recovery']) === true) 
		{
            
            $email		=trim($_GET['email']);
            $string	    =trim($_GET['password_recovery']);	
            
            if ($users->email_exists($email) === false || $users->recover($email, $string) === false) 
			{
                $errors[] = 'Sorry, something went wrong and we couldn\'t recover your password.';
            }
            if (empty($errors) === false) 
			{    		
        		echo '<p>' . implode('</p><p>', $errors) . '</p>';
    			
            } 
			else 
			{
                header('Location: recover.php?success');
                exit();
            }
        }
		else 
		{
            header('Location: index.php'); // If the required parameters are not there in the url. redirect to index.php
            exit();
        }
        ?>
    </div>
</body>
</html>