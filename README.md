# Blank

Small program to (temporarily) black out the screen.

See [OP's request](https://discord.com/channels/1120414216352960642/1225664355232907274/1225664358932152350)
on the [r/ProgrammingRequests Discord server](https://discord.gg/h9mJmn8Vpj).

## Usage

To make the program fullscreen, either:
- maximize the window
- snap the window to the top of the screen
- double-click the title bar
- double-click the window's content
- press <kbd>Win</kbd> + <kbd>Up</kbd>
- press <kbd>F11</kbd>

To exit fullscreen, either:
- drag the window
- double-click the window
- press <kbd>Esc</kbd>
- press <kbd>F11</kbd>
- press <kbd>Win</kbd> + <kbd>Down</kbd>

## Building

Run

```sh
dotnet publish -c Release --self-contained -r win-x64 -p:PublishSingleFile=true
```
