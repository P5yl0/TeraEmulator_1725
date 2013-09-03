<?php
include "config.php"; // the database configuration file. Update this to connect to your databse

//Gets Input from launcher
$username = $_POST['username']; //username
$password = $_POST['password']; //password
$email = $_POST['email']; //email

//check if the form was submitted
if((isset($_POST['username']) && $_POST['password'] != ""))
{
        //check whether the username and e-mail exist or not
        $sql="select Name,Email from accounts where Name='".$_POST['username']."'or Email='".$_POST['email']."'";
        $results=mysql_query($sql) or die(mysql_error());
        if(mysql_num_rows($results)>0)
		{
                $row=mysql_fetch_row($results);
                //check the username
				if($_POST['username']==$row['0'])
				{
					echo 'username='.$_POST['username'].'&password='.$_POST['password'];
                }
				//checks the e-mail
				elseif($_POST['email']==$row['1'])
				{
                    echo 'username='.$_POST['username'].'&password='.$_POST['password'];
                }
				else
				{
				//something wrong return empty string
				echo ("username=&password=");
                }
        }
}
?> 