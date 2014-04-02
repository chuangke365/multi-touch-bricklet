#!/usr/bin/perl  

use Tinkerforge::IPConnection;
use Tinkerforge::BrickletMultiTouch;

use constant HOST => 'localhost';
use constant PORT => 4223;
use constant UID => 'jSN'; # Change to your UID

my $ipcon = Tinkerforge::IPConnection->new(); # Create IP connection
my $mt = Tinkerforge::BrickletMultiTouch->new(&UID, $ipcon); # Create device object

$ipcon->connect(&HOST, &PORT); # Connect to brickd
# Don't use device before ipcon is connected

# Get current touch state
my $touch_state = $mt->get_touch_state();
my $s = '';
my $left_shift_1_12_bits = (1 << 12);

if (($touch_state) & ($left_shift_1_12_bits))
{
    $s .= 'In proximity, ';
}

if (($touch_state & 0xFFF) == 0)
{
    $s .= 'No electrodes touched';
} 
else
{
    $s .= 'Electrodes ';
        
    for (my $i = 0; $i < 12; $i++)
    {
        my $left_shift_1_i_bits = 1 << $i;

        if ($touch_state & $left_shift_1_i_bits)
        {
            $s .= "$i ";
        }
    }

    $s .= 'touched';
}

print "$s\n";

print "Press any key to exit...\n";
<STDIN>;
$ipcon->disconnect();

