<?php 
include 'core/init.php';
if(isset($_GET['username']) && empty($_GET['username']) === false)
{ // Putting everything in this if block.

 	$username   = htmlentities($_GET['username']); // sanitizing the user inputed data (in the Url)
	if ($users->user_exists($username) === false)
	{ // If the user doesn't exist
		header('Location:index.php'); // redirect to index page. Alternatively you can show a message or 404 error
		die();
	}
	else
	{
		$profile_data 	= array();
		$user_id 		= $users->fetch_info('Id', 'Username', $username); // Getting the user's id from the username in the Url.
		$profile_data	= $users->userdata($user_id);
	} 
	?>
	<!doctype html>
	<html lang="en">
	<head>	
		<meta charset="UTF-8">
		<link rel="stylesheet" type="text/css" href="css/style.css" >
	 	<title><?php echo $username; ?></title>
	</head>
	<body>
	    <div id="container">
			<?php include 'includes/menu.php'; ?>

			<h1><?php echo $profile_data['Username']; ?>'s Profile</h1>

	    	<div id="personal_info">
	    		<?php if(!empty($profile_data['FirstName']) || !empty($profile_data['LastName'])){?>

		    		<span><strong>First Name:</strong></span>
		    		<span><?php if(!empty($profile_data['FirstName'])) echo $profile_data['FirstName'], ' '; ?></span>
					<br>
					<span><strong>Last Name:</strong></span>
		    		<span><?php if(!empty($profile_data['LastName'])) echo $profile_data['LastName']; ?></span>
		    		<br>	
	    		<?php 
	    		} 
	    		?>
	    	</div>
	    	<div class="clear"></div>
	    </div>        
	     
	</body>
	</html>
	<?php  
}
else
{
	header('Location: index.php'); // redirect to index if there is no username in the Url
}