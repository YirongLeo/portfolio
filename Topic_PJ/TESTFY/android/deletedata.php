<?php
  
    $link = new mysqli("localhost", "root", "", "test");

    if (!$link) {
        die("Connection failed: " . mysqli_connect_error());
      }

    $link -> set_charset("UTF8"); 

    $PlateNumber = $_GET["CarNumber"];
    $PhoneNumber = $_GET['PhoneNumber'];
    $query ="UPDATE `user` SET user.PocketMoney=( SELECT user.PocketMoney-demotable665.ParkingFee as 'CheckOutPayment' FROM demotable665,user WHERE demotable665.PlateNumber='$PlateNumber' AND user.UserPhoneNumber='$PhoneNumber');";
    $query .= "INSERT INTO `paycheck`(`PlateNumber`, `UserPhoneNumber`, `MoneyPaid`) SELECT demotable665.PlateNumber,user.UserPhoneNumber,demotable665.ParkingFee FROM demotable665,user WHERE demotable665.PlateNumber='$PlateNumber' AND user.UserPhoneNumber='$PhoneNumber';";
    $query .="DELETE FROM `demotable665` WHERE PlateNumber='$PlateNumber';";
  
    if($link->multi_query($query)){
        echo ("Successful Payment");
    }else{
        echo"Failed";
    }

    $link -> close(); // 關閉資料庫連線

?>