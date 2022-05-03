<?php









    include("dbConnection.php");

    //check connection
    if(!$connection)
    {
        die("no connection". mysqli_connect_error());
    }
    else
    echo "<h1>Connected</h1>"."<br>";

    $UserName = $_POST["UserNamePost"];
    $Password = $_POST["PasswordPost"];
    $Email = $_POST["EmailPost"];

    $sql = "INSERT INTO User (UserName,Password,Email) 
            VALUES ('".$UserName."','".$Password."','".$Email."')";
    $result = mysqli_query($connection, $sql);

    if(!result)
    {
        echo"Wrong";
    }
    else
    echo "Good";


?>
