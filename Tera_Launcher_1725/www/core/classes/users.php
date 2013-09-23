<?php 

class Users
{


	private $db;

	public function __construct($database) 
	{
	    $this->db = $database;
	}	


	public function sendmail($subject, $message)
	{
	require 'class.phpmailer.php';

	$mail = new PHPMailer;
	$mail->isSMTP();                                      // Set mailer to use SMTP
	$mail->Host = 'smtp.mail.yahoo.com';  // Specify main and backup server
	$mail->SMTPAuth = true;                               // Enable SMTP authentication
	$mail->Username = 'john@doe.com';                            // SMTP username
	$mail->Password = 'john.doe';                           // SMTP password
	$mail->SMTPSecure = '';                            // Enable encryption, 'ssl' also accepted
	$mail->From = 'john@doe.com';
	$mail->FromName = 'Admin';
	$mail->addAddress('john@doe.com', 'John Doe');  // Add a recipient
	$mail->addAddress('john@doe.com');               // Name is optional
	$mail->addReplyTo('john@doe.com', 'Information');
	$mail->addCC('john@doe.com');
	$mail->addBCC('john@doe.com');
	$mail->WordWrap = 50;                                 // Set word wrap to 50 characters
	$mail->addAttachment('/var/tmp/file.tar.gz');         // Add attachments
	$mail->addAttachment('/tmp/image.jpg', 'new.jpg');    // Optional name
	$mail->isHTML(true);                                  // Set email format to HTML

	$mail->Subject = $subject;
	$mail->Body    = $message;
	$mail->AltBody = 'This is the body in plain text for non-HTML mail clients';

	if($mail->send()) 
	{
	echo 'Message has been sent';
	return true;
	}
	else
		{
		echo 'Message could not be sent.';
		echo 'Mailer Error: ' . $mail->ErrorInfo;
		return false;
		exit;
		}
	}

	public function update_user($first_name, $last_name, $id)
	{
		$query = $this->db->prepare("UPDATE `accounts` SET
								`FirstName`	= ?,
								`LastName`		= ?,
								
								WHERE `id` 		= ? 
								");

		$query->bindValue(1, $first_name);
		$query->bindValue(2, $last_name);
		$query->bindValue(6, $id);
		
		try
		{
			$query->execute();
		}
		catch(PDOException $e)
		{
			die($e->getMessage());
		}	
	}

	public function change_password($user_id, $password)
	{
		$password_hash = md5($password);
		$query = $this->db->prepare("UPDATE `accounts` SET `Password` = ? WHERE `Id` = ?");
		$query->bindValue(1, $password_hash);
		$query->bindValue(2, $user_id);
		try
		{
			$query->execute();
			return true;
		} 
		catch(PDOException $e)
		{
			die($e->getMessage());
		}
	}

	public function recover($email, $password_recovery)
	{
		if($password_recovery == 0)
		{
			return false;
		}
		else
		{
			$query = $this->db->prepare("SELECT COUNT(`Id`) FROM `accounts` WHERE `Email` = ? AND `PasswordRecovery` = ?");

			$query->bindValue(1, $email);
			$query->bindValue(2, $password_recovery);

			try
			{
				$query->execute();
				$rows = $query->fetchColumn();

				if($rows == 1)
				{
			
					$username = $this->fetch_info('Username', 'Email', $email); // getting username for the use in the email.
					$user_id  = $this->fetch_info('Id', 'Email', $email);// We want to keep things standard and use the user's id for most of the operations. Therefore, we use id instead of email.
			
					$charset = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
					$generated_password = substr(str_shuffle($charset),0, 10);

					$this->change_password($user_id, $generated_password);
					$query = $this->db->prepare("UPDATE `accounts` SET `PasswordRecovery` = 0 WHERE `Id` = ?");
					$query->bindValue(1, $user_id);
					$query->execute();
				
					$this->sendmail('your recovery request',  "Hello " . $username. " ,<br>Your new password is: " .$generated_password. "<br> Please change it after your login, thnx.-- the team");			

				}
			
			}
			catch(PDOException $e)
			{
				die($e->getMessage());
			}
		}
	}


    public function fetch_info($what, $field, $value)
	{

		$allowed = array('Id', 'Username', 'FirstName', 'LastName', 'Email'); // I have only added few, but you can add more. However do not add 'password' eventhough the parameters will only be given by you and not the user, in our system.
		if (!in_array($what, $allowed, true) || !in_array($field, $allowed, true)) 
		{
		    throw new InvalidArgumentException;
		}
		else
		{
			$query = $this->db->prepare("SELECT $what FROM `accounts` WHERE $field = ?");
			$query->bindValue(1, $value);

			try
			{
				$query->execute();
			} 
			catch(PDOException $e)
			{
				die($e->getMessage());
			}
			return $query->fetchColumn();
		}
	}

	public function confirm_recover($email)
	{

		$username = $this->fetch_info('Username', 'Email', $email);// We want the 'id' WHERE 'email' = user's email ($email)
		$unique = uniqid('',true);
		$random = substr(str_shuffle('ABCDEFGHIJKLMNOPQRSTUVWXYZ'),0, 10);
		$password_recovery = $unique . $random; // a random and unique string
		$query = $this->db->prepare("UPDATE `accounts` SET `PasswordRecovery` = ? WHERE `Email` = ?");
		$query->bindValue(1, $password_recovery);
		$query->bindValue(2, $email);
		try
		{
			$query->execute();

			$this->sendmail('your recovery',  "Hello " . $username. " ,<br>Please click the link below:<br> <a href=http://psylo.dyndns.org/recover.php?email=" .$email. "&password_recovery=" .$password_recovery. ">recovery link</a> recovery link<br> We will generate a new password for you and send it back to your email.-- the team");			
		} 
		catch(PDOException $e)
		{
			die($e->getMessage());
		}
	}

	public function user_exists($username) 
	{
		$query = $this->db->prepare("SELECT COUNT(`Id`) FROM `accounts` WHERE `Username`= ?");
		$query->bindValue(1, $username);
		
		try
		{
			$query->execute();
			$rows = $query->fetchColumn();
			if($rows == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		catch (PDOException $e)
		{
			die($e->getMessage());
		}
	}
	 
	public function email_exists($email) 
	{
		$query = $this->db->prepare("SELECT COUNT(`Id`) FROM `accounts` WHERE `Email`= ?");
		$query->bindValue(1, $email);
	
		try
		{
			$query->execute();
			$rows = $query->fetchColumn();

			if($rows == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		catch (PDOException $e)
		{
			die($e->getMessage());
		}
	}

	public function register($username, $first_name, $last_name, $password, $email)
	{
		$password   = md5($password);
		
		$accesslevel   		= "1"; //0=need email verification; 1=activated
		$membership			= "20001"; //20000=trial; 20001=veteran; 20002=veteran/club?
		$isgm				= "0";
		$lastonlineutc 		= time();
		$coins 				= "0";
		$ip					= $_SERVER['REMOTE_ADDR']; // getting the users IP address
		$emailverify		= $emailverify = uniqid('code_',true); // Creating a unique string. //for email verification
		$first_name			= $first_name;	//empty for registering now
		$last_name			= $last_name;	//empty for registering now

		$query 	= $this->db->prepare("INSERT INTO `accounts` (`Username`, `Password`, `Email`, `AccessLevel`, `Membership`, `LastOnlineUtc`, `Coins`, `Ip`, `EmailVerify`, `FirstName`, `LastName` ) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?) ");

		$query->bindValue(1, $username);
		$query->bindValue(2, $password);
		$query->bindValue(3, $email);
		$query->bindValue(4, $accesslevel);
		$query->bindValue(5, $membership);
		$query->bindValue(6, $lastonlineutc);
		$query->bindValue(7, $coins);
		$query->bindValue(8, $ip);
		$query->bindValue(9, $emailverify);
		$query->bindValue(10, $first_name);
		$query->bindValue(11, $last_name);

		try
		{
			$query->execute();
		
			$this->sendmail('Please activate your account', "Hello " . $username. " ,<br>Thank you for registering...<br> Please visit the link below so we can activate your account:<br> <a href=http://psylo.dyndns.org/activate.php?email=" . $email . "&emailverify=" . $emailverify . ">activation link</a> activation link<br> -- Example team");
			
		}
		catch(PDOException $e)
		{
			die($e->getMessage());
		}	
	}

	public function activate($email, $emailverify)
	{
		$query = $this->db->prepare("SELECT COUNT(`Id`) FROM `accounts` WHERE `Email` = ? AND `EmailVerify` = ? AND `AccessLevel` = ?");

		$query->bindValue(1, $email);
		$query->bindValue(2, $emailverify);
		$query->bindValue(3, 0);

		try
		{
			$query->execute();
			$rows = $query->fetchColumn();

			if($rows == 1)
			{
				$query_2 = $this->db->prepare("UPDATE `accounts` SET `AccessLevel` = ? WHERE `Email` = ?");
				$query_2->bindValue(1, 1);
				$query_2->bindValue(2, $email);				
				$query_2->execute();
				return true;
			}
			else
			{
				return false;
			}
		} 
		catch(PDOException $e)
		{
			die($e->getMessage());
		}
	}

	public function email_confirmed($username)
	{
		$query = $this->db->prepare("SELECT COUNT(`Id`) FROM `accounts` WHERE `Username`= ? AND `AccessLevel` = ?");
		$query->bindValue(1, $username);
		$query->bindValue(2, 1);
		
		try
		{
			$query->execute();
			$rows = $query->fetchColumn();

			if($rows == 1)
			{
				return true;
			}
			else
			{
				return false;
			}

		} 
		catch(PDOException $e)
		{
			die($e->getMessage());
		}
	}

	public function login($username, $password) 
	{
		$query = $this->db->prepare("SELECT `Password`, `Id` FROM `accounts` WHERE `Username` = ?");
		$query->bindValue(1, $username);

		try
		{
			$query->execute();
			$data 				= $query->fetch();
			$stored_password 	= $data['Password']; // stored hashed password
			$id   				= $data['Id']; // id of the user to be returned if the password is verified, below.
			
			if((md5($password) == $stored_password) === true)
			{ // using the verify method to compare the password with the stored hashed password.
				return $id;	// returning the user's id.
			}
			else
			{
				return false;	
			}
		}
		catch(PDOException $e)
		{
			die($e->getMessage());
		}
	}

	public function userdata($id)
	{
		$query = $this->db->prepare("SELECT * FROM `accounts` WHERE `Id`= ?");
		$query->bindValue(1, $id);

		try
		{
			$query->execute();
			return $query->fetch();
		}
		catch(PDOException $e)
		{
			die($e->getMessage());
		}
	}
	  	  	 
	public function get_users()
	{
		$query = $this->db->prepare("SELECT * FROM `accounts` ORDER BY `LastOnlineUtc` DESC");
		try
		{
			$query->execute();
		}
		catch(PDOException $e)
		{
			die($e->getMessage());
		}
		return $query->fetchAll();
	}	
}