<?php
$user_name = "prabeesh";
$user_pass = "password";
$host_name = "localhost";
$db_name = "dbupload";

$con = mysqli_connect($host_name,$user_name,$user_pass,$db_name);

if ($con)
{
	$image = $_post["image"];
	$name = $_POST["name"];
	$sql = "insert into imageinfo(name) values('$namge')";
	$upload_path = "uploads/$name.jpg";
	
	if(mysql_query($con,$sql))
	{
		file_put_contents($upload_path,base64_decode($image));
		echo json_encode(array('response'=>'Image Uploaded Successfully'));
	}
	else
	{
		echo json_encode(array('response'=>'Image upload failed'));
	}	
}
else
{
	echo json_encode(array('response'=>'Image Upload Failed'));
}

 mysqli_close($con);
 
?>