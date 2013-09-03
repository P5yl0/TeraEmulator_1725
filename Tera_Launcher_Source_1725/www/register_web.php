<?php
include "config.php"; //the database configuration file. Update this to connect to your databse

//function to regiter
function register($username,$password,$email)
{
        //find the next userid
        $query="select max(Id) as total from accounts";
        $results=mysql_query($query) or die(mysql_error());
        if($results){
                $row=mysql_fetch_row($results);//store the result in $row array
                $user_id=++$row['0'];//increment the currnet user_id by 1
        }

        //encrypt the password using md5
        $enc_password=md5($password);
        mysql_free_result($results);//free the result
        //insert new registration details to database

        $query="insert into accounts(Id,Name,Password,Email,AccessLevel,Membership,LastOnlineUtc,Coins)";
        $query=$query."values('".$user_id."','".$username."','".$enc_password."','".$email."','"."0"."','"."0"."','"."0"."','"."0"."')";

        $results=mysql_query($query) or die(mysql_error());

        if($results)
		{
				echo 'Welcome '.$username.'!';
                echo " ... Account successfully registered!";
                return 1;
        }
		else
		{
                echo "Registration failed";
                return 0;
        }
}
//function to activate the user. Give the user id as the parameter.
function activate_user($user_id){
        $query="update accounts set Membership=1 where Id=".$user_id;
        $results=mysql_query($query) or die(mysql_error());    
        if(!$results)
		{
                echo "The account has been activated!";
        }      
}

//check if the form was submitted
if((isset($_POST['Submit']) && $_POST['Submit']=="Register"))
{

        //check whether the username and e-mail already exist or not
        $sql="select Name,Email from accounts where Name='".$_POST['username']."' or Email='".$_POST['email']."'";
        $results=mysql_query($sql) or die(mysql_error());
        if(mysql_num_rows($results)>0)
		{
                $row=mysql_fetch_row($results);
                //check if username exists
				if($_POST['username']==$row['0'])
				{
                        echo "Username already taken!";
                }
				//check if e-mail exists
				elseif($_POST['email']==$row['1'])
				{
                        echo "The e-mail already used for registration!";
                }
				else
				{
				//if username and e-mail not used, register the new user to database
                        register($_POST['username'],$_POST['password'],$_POST['email']); 
                }
        }
		else
		{
                //register new user to access
                register($_POST['username'],$_POST['password'],$_POST['email']);
        }
}else{
?>

<!---- the form to take input ----!>
<html>
<body>
<form action="<? $_SERVER['PHP_SELF'] ?>" method="post">
<input type="text" value="username" align="LEFT" name="username" />
<br>
<input type="password" value="password" name="password" />
<br>
<input type="text" value="email" name="email" />
<br>
<input type="submit" value="Register" name="Submit" />
</form>
</body>
</html>
<?

}
?>