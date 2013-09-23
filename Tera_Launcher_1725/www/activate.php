<?php 
require 'core/init.php';
$general->logged_in_protect();

?>
<!doctype html>
<html lang="en">
<head>
	<meta charset="UTF-8">
	<link rel="stylesheet" type="text/css" href="css/style.css" >
	<title>Activate</title>
</head>
<body>	
	<div id="container">
	<?php include 'includes/menu.php'; ?>
		<h1>Activate your account</h1>

    	<?php
        
        if (isset($_GET['success']) === true && empty ($_GET['success']) === true)
		{
	        ?>
	        <h3>Thank you, we've activated your account. You're free to log in!</h3>
	        <?php
        } 
		else if (isset ($_GET['email'], $_GET['emailverify']) === true) 
		{
            
            $email			=trim($_GET['email']);
            $emailverify	=trim($_GET['emailverify']);	
            
            if ($users->email_exists($email) === false) 
			{
                $errors[] = 'Sorry, we couldn\'t find that email address';
            } 
			else if ($users->activate($email, $emailverify) === false) 
			{
                $errors[] = 'Sorry, we have failed to activate your account';
            }
			if(empty($errors) === false)
			{
				echo '<p>' . implode('</p><p>', $errors) . '</p>';	
		
			} 
			else 
			{
			
                header('Location: activate.php?success');
                exit();
            }
        
        } else {
            header('Location: index.php');
            exit();
        }
        ?>

	</div>
</body>
</html>