<?php 
session_start();
require 'connect/database.php';
require 'classes/users.php';
require 'classes/general.php';

// error_reporting(0);

$users 		= new Users($db);
$general 	= new General();

$errors = array();

if ($general->logged_in() === true)  
{
	$user_id 	= $_SESSION['Id'];
	$user 		= $users->userdata($user_id);
}

ob_start(); // Added to avoid a common error of 'header already sent'