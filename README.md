# XBee Multi Terminal
Connect to multiple XBee® clients configured as transparent routers and communicate concurrently via their serial terminal connections, all from a single coordinator.

[Latest Release v0.2.0.0](https://github.com/reasyrf/XBeeMultiTerminal/blob/master/Installers/v0.2.0.0/XBMTSetupv.0.2.0.0.exe?raw=true)

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

![Software Screenshot](MultiTerminal.png?raw=true)

![Typical Hardware Configuration](docs/media/Hardware.png?raw=true)

# Help
[Click here for HTML help](https://reasyrf.github.io/XBeeMultiTerminal)

# TODO
- Release sources (after NLog builds correctly).
- Fix problem with duplicate received packets.
- Fix API enable (cannot connect to host modem when in transparent mode). Must set in XCTU.

# Prerequisites
Requires .NET 4.5 Runtime - https://www.microsoft.com/net/download
