### Graphical AVPI : Test Build 01

https://github.com/baykovr/AVPI/releases/tag/v0.01

Direct Link : https://github.com/baykovr/AVPI/releases/download/v0.01/GAVPI.zip

#####  Disclaimer: This is a very early build version, be kind.

![main](https://cloud.githubusercontent.com/assets/6128886/3487757/17659892-04a0-11e4-9e81-0e9356861113.PNG)
#### Goal

The purpose of this build is to involve user input at the earliest stage of development. There are probably many bugs and untested features, the idea is to get as much feedback as possible. 

Feel free to make suggestions, contributions or bug reports.

You can message me directly on reddit , /u/ Jebediah_Kerman or submit a git report.

#### Usage

##### Profile

![profile](https://cloud.githubusercontent.com/assets/6128886/3487769/37d32a62-04a1-11e4-9719-b9c0f9055bb1.PNG)

The profile is broken down into three sections.
##### 1. Action Sequence

An action sequence is composed of sequential "actions", one of keyboard or mouse press/down/up, wait or speak. The key or mouse presses are carried out at direct input level which can be thought of equivalent to your physical key presses. Action sequences are carried out very quickly, in some cases near instantly so it is helpful to use the wait (milliseconds) command if an application is failing to registered presses.

Note that Key/Mouse Down action's will hold a specific key until an Up or Press action.

![action_sequence](https://cloud.githubusercontent.com/assets/6128886/3487783/8164d18e-04a2-11e4-8f9f-46318d1a06be.PNG)

##### 2. Trigger 

Triggers are composed of three parts type, name and value.

Currently only one type of trigger is implemented, VI_Phrase. This is the speech recognition trigger type.
Given a trigger value (a spoken phrase) a trigger will execute all of its corresponding events[3] in order.

![trigger](https://cloud.githubusercontent.com/assets/6128886/3487779/f40bece6-04a1-11e4-9142-adba700010e8.PNG)

##### 3. Trigger Events

Trigger events are carried out when a trigger runs, for example when a spoken phrase is recognized. 

The events can be multiple Action Sequences or other Triggers, in this way an action sequence can be reused in multiple triggers

##### Saving/Loading

I know its annoying, but you need to load your profile each time the application starts for now. Profiles are stored in XML format and can be edited by hand in any text editor which supports it. You can also load profiles made by other people. I will be making a few profiles for myself which will be available on the main project [page](https://github.com/baykovr/AVPI).

### Settings

Settings are untested, since I only have en-us and one voice synthesizer to choose from. None the less it is included as requested but not verified to function properly. 

![settings](https://cloud.githubusercontent.com/assets/6128886/3487803/750a8976-04a5-11e4-879e-c2393485907e.PNG)

###### Thanks and have fun, if it works that is.




###AVPI

Artificial Virtual Pilot Interface, an open source alternative to VoiceAttack
#### Version .00
Release Page https://github.com/baykovr/AVPI/releases/tag/v0.2 

Direct Link: https://github.com/baykovr/AVPI/releases/download/v0.2/AVPI_v02.7z

### Introduction

![cap](https://cloud.githubusercontent.com/assets/6128886/3350745/ed95144a-f9d0-11e3-8b3c-561f2c7674b7.PNG)

AVPI is a voice command utility for Star Citizen. It lets you easily issue commands which are currently pretty complicated, like power distribution. It can do some(for now) things that you can achieve with VoiceAttack, but for free and is pre-configured. You can compile it from source using visual studio or download the latest release.

It uses windows speech recognition just like VoiceAttack and direct keyboard/mouse input which is really fast.

AVPI is currently in early stages of development so not everything works, but I am working hard on add more features so bookmark the releases page for updates there's going to be many.

Email me at baykovr@gmail.com if you would like to contribute, have a suggestion or have a problem with AVPI. If you would like to donate to AVPI there is a paypal link on my page https://baykovr.github.io/

Thanks, enjoy the program!


### (From Release Readme v.00) Instructions

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
