To enable USB debugging:
	1. On your ANDROID DEVICE, go to Settings > About phone
	2. Tap Build number 7 times
	3. Return to the previous screen to find Developer options at the bottom
	4. Under Developer Options, enable USB debugging

To run this game on your ANDROID DEVICE with USB debugging enabled:
	1. Open project in Unity 2018
	2. Connect Android device to computer via USB
	3. In Unity, Select File > Build Settings
	4. Android should be selected on the left. Select your personal Android device from the dropdown next to Run Device on the right
	5. Click Build and Run (you may need to save a file on your local machine)
	6. Wait for build to finish and game will run automatically

Alternative way to run the game on Android Device or Emulator:
	1. On your command line execute command "adb devices" from the android_sdk/platform-tools/ to verify your device or emulator is connected.
	2. Install packaged apk by using the command "adb install /path/to/Team1_Code_3310_Summer2021/CthulhusMansion.apk"