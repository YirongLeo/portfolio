<?php

    header('Content-type:bitmap:charset=uft-8');

    if(isset($_POST["encoded_string"])){
        $encoded_string = $_POST['encoded_string'];
        $image_name = $_POST["image_name"];

        $decode_string = base64_decode($encoded_string);

        $path = "picture/".$image_name;

        $file = fopen($path,'wb');

        $is_written = fwrite($file,$decode_string);
        fclose($file);

        if($is_written > 0){
            $connection = mysqli_connect('localhost','root','','placeforphoto');
            $query = " INSERT INTO photos(name,path) values('$image_name','$path');";

            $result = mysqli_query($connection,$query);

            if($result){
                echo "succesed";
            }else{
                echo "fail";
            }

            mysqli_close($connection);
        }
    }
?>