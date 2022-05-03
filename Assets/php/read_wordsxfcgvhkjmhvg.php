<?php


$today = date("Ymd") - 20220420; // begin from 420
$hour = date('H');
$dayTerm = ($hour > 12) ? "pm" : "am";

$array = explode(",", file_get_contents("words_xhcgjgkhlfchgjvbjk.txt"));

$arrLength = count($array);
$index = 0; 


$index = (($today * 24) + $hour) % $arrLength;

	
// word, thisSessionsKey
echo $array [$index] . ',' . $today . '' . $hour;
?> 
                            

