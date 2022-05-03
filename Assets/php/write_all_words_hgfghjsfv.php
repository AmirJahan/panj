<?php




$UserName = $_POST["UserNamePost"];
$Password = $_POST["PasswordPost"];
$Email = $_POST["EmailPost"];


 $fp = fopen('words_xhcgjgkhlfchgjvbjk.txt', 'w');

// $sql = "INSERT INTO User (UserName,Password,Email) 
//         VALUES ('".$UserName."','".$Password."','".$Email."')";
// $result = mysqli_query($connection, $sql);

// if(!result)
// {
//     echo"Wrong";
// }
// else
// echo "Good";



fwrite($fp, $UserName . ', ' . $Password);
fclose($fp);
?>