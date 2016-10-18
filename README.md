# XBee Multi Terminal
Connect to multiple XBee® clients configured as transparent routers and communicate concurrently via their serial terminal connections, all from a single coordinator.

[Latest Release v0.3.2.0](https://github.com/reasyrf/XBeeMultiTerminal/blob/master/installers/v0.3.2.0/XBMTSetupv0.3.2.0.exe?raw=true)

Current Features:
- Multiplexed terminal windows.
- Create named pipes for external terminal applications.
- Save and load session state.
- XBee coordinator device status.
- Run simple XBee commands.
- Detect clients.
- Packet debug output.
- Copy and paste to terminals.
- Label terminal windows (right click title bar of terminal window).
- Fragmented/Large packet support.
- Asynchronous message handling.
- RSSI indicator.

![Software Screenshot](MultiTerminal.png?raw=true)

![Typical Hardware Configuration](docs/media/Hardware.png?raw=true)

# Version History 
[Version History](https://reasyrf.github.io/XBeeMultiTerminal/html/90b7f806-433d-4171-8d80-4b98f4eafdba.htm)

# Help
[Click here for HTML help](https://reasyrf.github.io/XBeeMultiTerminal)

# TODO
- Release sources (after NLog builds correctly).
- Fix API enable (cannot connect to host modem when in transparent mode). Must set in XCTU.
- Change line ending transmission/receive type (for each terminal).
- Update help with new features (copy/paste, labels).
- Write current settings to non-volatile storage on host modem.
- Log terminal output to file (for the moment you can use the named pipe and an external program like Putty to do this).

# Prerequisites
Requires .NET 4.5 Runtime - https://www.microsoft.com/net/download
