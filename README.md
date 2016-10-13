# XBee Multi Terminal
Connect to multiple XBee® clients configured as transparent routers and communicate concurrently via their serial terminal connections, all from a single coordinator.

[Latest Release v0.3.0.0](https://github.com/reasyrf/XBeeMultiTerminal/blob/master/Installers/v0.3.0.0/XBMTSetupv.0.3.0.0.exe?raw=true)

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

# Help
[Click here for HTML help](https://reasyrf.github.io/XBeeMultiTerminal)

# TODO
- Release sources (after NLog builds correctly).
- Fix API enable (cannot connect to host modem when in transparent mode). Must set in XCTU.
- Change line ending transmission/receive type (for each terminal).
- Update help with new features (copy/paste, labels).
- Write current settings to non-volatile storage on host modem.

# Prerequisites
Requires .NET 4.5 Runtime - https://www.microsoft.com/net/download
