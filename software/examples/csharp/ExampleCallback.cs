using Tinkerforge;

class Example
{
	private static string HOST = "localhost";
	private static int PORT = 4223;
	private static string UID = "XYZ"; // Change to your UID

	// Callback function for touch state
	static void TouchStateCB(BrickletMultiTouch sender, int touchState)
	{
		string str = "";

		if((touchState & (1 << 12)) == (1 << 12)) {
			str += "In proximity, ";
		}

		if((touchState & 0xfff) == 0) {
			str += "No electrodes touched" + System.Environment.NewLine;
		} else {
			str += "Electrodes ";
			for(int i = 0; i < 12; i++) {
				if((touchState & (1 << i)) == (1 << i)) {
					str += i + " ";
				}
			}
			str += "touched" + System.Environment.NewLine;
		}

		System.Console.WriteLine(str);
	}

	static void Main() 
	{
		IPConnection ipcon = new IPConnection(); // Create IP connection
		BrickletMultiTouch mt = new BrickletMultiTouch(UID, ipcon); // Create device object

		ipcon.Connect(HOST, PORT); // Connect to brickd
		// Don't use device before ipcon is connected

		// Register touchState callback to function TouchStateCB
		mt.TouchState += TouchStateCB;

		System.Console.WriteLine("Press key to exit");
		System.Console.ReadKey();
		ipcon.Disconnect();
	}
}
