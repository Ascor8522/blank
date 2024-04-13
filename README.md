# Blank

Small program to (temporarily) black out the screen.

See [OP's request](https://discord.com/channels/1120414216352960642/1225664355232907274/1225664358932152350)
on the [r/ProgrammingRequests Discord server](https://discord.gg/h9mJmn8Vpj).

## Usage

To make the program fullscreen, maximize the window.
This also works when
snapping the window to the top of the screen,
double-clicking the title bar,
using the <kbd>Win</kbd> + <kbd>Up</kbd> keyboard shortcut,
or pressing <kbd>F11</kbd>.

To exit fullscreen, click anywhere on the window, and start dragging the window.

## Building

Run

```sh
dotnet publish -c Release --self-contained -r win-x64 -p:PublishSingleFile=true
```
