<?php

require_once('Tinkerforge/IPConnection.php');
require_once('Tinkerforge/BrickletMultiTouch.php');

use Tinkerforge\IPConnection;
use Tinkerforge\BrickletMultiTouch;

$host = 'localhost';
$port = 4223;
$uid = 'XYZ'; // Change to your UID

$ipcon = new IPConnection(); // Create IP connection
$mt = new BrickletMultiTouch($uid, $ipcon); // Create device object

$ipcon->connect($host, $port); // Connect to brickd
// Don't use device before ipcon is connected

// Get current touch state
$touchState = $mt->getTouchState();

if($touchState & (1 << 12)) {
	echo "In proximity, ";
}

if(($touchState & 0xfff) == 0) {
	echo "No electrodes touched\n\n";
} else {
	echo "Electrodes ";
	for($i = 0; $i < 12; $i++) {
		if($touchState & (1 << $i)) {
			echo $i . " ";
		}
	}
	echo "touched\n\n";
}

echo "Press key to exit\n";
fgetc(fopen('php://stdin', 'r'));
$ipcon->disconnect();

?>
