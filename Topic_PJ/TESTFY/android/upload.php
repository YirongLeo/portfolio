<?php
    $connect = mysqli_connect ("localhost","root","","dbupload");
	
	if (isset($_POST['image']))
	{
		$target_dir = "Images/";
		$image = $_POST['image'];
		$imageStore = rand()."_".time().".jpeg";
		$target_dir = $target_dir."/".$imageStore;
		file_put_contents($target_dir,base64_decode($image));
		
		$result = array();
		$select = "INSERT into imagedata(image) VALUES ('$imageStore')";
		$response = mysqli_query($connect,$select);
		
		if($response)
		{
			echo "Image Uploaded";
			mysqli_close($connect);
		}
		else
		{
			echo "Failed";
		}
	}

?>
