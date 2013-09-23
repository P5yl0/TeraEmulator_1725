<?php 
require 'core/init.php';
$general->logged_out_protect();

$username	= htmlentities($user['Username']); // storing the user's username after clearning for any html tags.

?>
<!doctype html>
<html lang="en">
<head>
	<meta charset="UTF-8">
	<link rel="stylesheet" type="text/css" href="css/style.css" >
	<title>Home</title>
</head>
<body>	
	<div id="container">
		<?php include 'includes/menu.php'; ?>
		<h1>Hello <?php echo $username, '!'; ?></h1>
	</div>
</body>
</html>