<?php 
include_once 'core/init.php';
$general->logged_out_protect();
?>

<!doctype html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <link rel="stylesheet" type="text/css" href="css/style.css" >
    <title>Change Password</title>
</head>
<body>
    <div id="container">        
    	<?php
        include 'includes/menu.php';
        if(empty($_POST) === false)
		{
            if(empty($_POST['current_password']) || empty($_POST['password']) || empty($_POST['password_again']))
			{
                $errors[] = 'All fields are required';

            }
			else if((md5($_POST['current_password']) == $user['Password']) === true)
			{
                if (trim($_POST['password']) != trim($_POST['password_again'])) 
				{
                    $errors[] = 'Your new passwords do not match';
                } 
				else if (strlen($_POST['password']) < 4) 
				{ 
                    $errors[] = 'Your password must be at least 4 characters';
                } 
				else if (strlen($_POST['password']) >32)
				{
                    $errors[] = 'Your password cannot be more than 32 characters long';
                } 
            } 
			else 
			{
                $errors[] = 'Your current password is incorrect';
            }
        }

        if (isset($_GET['success']) === true && empty ($_GET['success']) === true )
		{
    		echo '<p>Your password has been changed!</p>';
        } 
		else
		{?>
            <h2>Change Password</h2>
            <hr />
            <?php
            if (empty($_POST) === false && empty($errors) === true)
			{
                $users->change_password($user['Id'], $_POST['password']);
                header('Location: change-password.php?success');
            }
			else if (empty ($errors) === false)
			{
                echo '<p>' . implode('</p><p>', $errors) . '</p>';  
            }
            ?>
            <form action="" method="post">
                <ul>
                    <li>
                        <h4>Current password:</h4>
                        <input type="password" name="current_password">
                    </li>
                    <li>
                        <h4>New password:</h4>
                        <input type="password" name="password">
                    </li>
                    <li>
                        <h4>New password again:</h4>
                        <input type="password" name="password_again">
                    </li>			    
                    <li>
                        <input type="submit" value="Change password">
                    </li>  
                </ul>
            </form>
    	<?php 
        }
        ?> 
    </div>
</body>
</html>