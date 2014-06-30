###Coming Soon: GAVPI

Graphical Artificial Virtual Pilot Interface, an open source alternative to VoiceAttack

![cap](https://baykovr.github.io/img/main.png)

###AVPI
Artificial Virtual Pilot Interface, an open source alternative to VoiceAttack

#### Version .02 
Release Page https://github.com/baykovr/AVPI/releases/tag/v0.2 

Direct Link: https://github.com/baykovr/AVPI/releases/download/v0.2/AVPI_v02.7z

### Introduction

![cap](https://cloud.githubusercontent.com/assets/6128886/3350745/ed95144a-f9d0-11e3-8b3c-561f2c7674b7.PNG)

AVPI is a voice command utility for Star Citizen. It lets you easily issue commands which are currently pretty complicated, like power distribution. It can do some(for now) things that you can achieve with VoiceAttack, but for free and is pre-configured. You can compile it from source using visual studio or download the latest release.

It uses windows speech recognition just like VoiceAttack and direct keyboard/mouse input which is really fast.

AVPI is currently in early stages of development so not everything works, but I am working hard on add more features so bookmark the releases page for updates there's going to be many.

Email me at baykovr@gmail.com if you would like to contribute, have a suggestion or have a problem with AVPI. If you would like to donate to AVPI there is a paypal link on my page https://baykovr.github.io/

Thanks, enjoy the program!


### (From Release Readme v.02) Instructions

###### Usage

Extract the contents of the zip file anywhere on your computer.

Double click on AVPI.exe, if star citizen is not running it will warn you. 

This is because unlike VoiceAttack AVPI does not distinguish between windows key commands are
pressed as if you pressed them yourself on the keyboard, so watch out if you like to tab out.

This is an initial release and there may be many problems, you can submit a bug report or email me if you'd like.

###### Extra Sounds
AVPI currently supports windows TTS and  [Tradewind's glados audioset](https://forums.robertsspaceindustries.com/discussion/142698/voice-attack-glados-audio-set-v1) which I cannot redistribute without asking so you'll need to download it yourself.

To use it, create a folder names 'vaglad' inside of the AVPI folder. (The same folder where avpi.exe is located)
Extract the contents of VA_GLaDOS_Set.rar into vaglad/ such that all wav files reside on that folder. (example vaglad/firing missile.wav)

You may replace the wav files with your own, provided it has the exact same name, "firing missile.wav"

## AVPI

### Supported Commands

###### Power Management

Say one of 
"G0", "G1", "G2", "G3" to change power distribution. (G0 is center)


"status" VI will state what it thinks the current power distribution is, in case you are on another screen
and need a reminder. 

 "scan" or "target nearest" equivalent to the 'r' key

"next target" equivalent to the 't' key

 "pin target" equivalent to the 'g' key

 "missile fire", "missile lock"  equivalent to middle mouse
