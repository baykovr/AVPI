![GAVPI](https://raw.githubusercontent.com/baykovr/AVPI/master/img/gavpi-logo.png)

### [ Latest Version ] (https://github.com/baykovr/AVPI/releases/tag/v0.08)  : Alpha v0.08 :03/11/16
Updates
* Added ability to press a key for selection in addition to using the drop down for most common keys (Special keys and combination not supported yet).
* Added ability to select sound device for audio playback (you can also use different audio files on different devices simultaneously).
* Fixed missing keys from input selection, namely the alt and oem keys.
* Fixed an issue with profile sequence ordering wrapping around during editing (moving an action up would wrap around to the end of the list).
* Refactored and organized source code.

@AdamJamesNaylor
* Added ability to double click items in profile in order to edit them.
* Added balloon tool tip to notify the user the program is still running in the background.


Download link for latest build [direct](https://github.com/baykovr/AVPI/releases/download/v0.08/GAVPI_0.08.zip)

You should run gavpi as administrator for it work properly in most games. (right click run as...)

#### About

GAVPI is an open source voice command software / speech recognition key bind tool. Using gavpi you can execute repetitive or complex tasks using your voice. It can be used with any application, since it's equivilent to pressing your mouse/keyboard.

You can:
+ Bind a set of action sequences (mouse/keyboard presses or text-to-speech events) to arbitrary phrases.
+ Create profiles for different games or applications.
+ Enjoy the program for free.

***

#### Installation

Just extract and run the exe.

***

#### Usage

![main](https://raw.githubusercontent.com/baykovr/AVPI/master/img/main.PNG)

##### Profile
The profile is broken down into three sections.

##### 1. Action Sequence

An action sequence is composed of sequential "actions", one of keyboard or mouse press/down/up, wait or speak. The key or mouse presses are carried out at direct input level which is equivalent to your physical key presses. Action sequences are carried out very quickly, in some cases near instantly so it is helpful to use the wait (milliseconds) command if an application is failing to registered presses.

![action_sequence](https://raw.githubusercontent.com/baykovr/AVPI/master/img/actionsequenceeditor.PNG)

##### 2. Trigger 

Triggers are composed of three parts type, name and value.

Currently only one type of trigger is implemented, VI_Phrase. This is the speech recognition trigger type its value being a spoken word or phrase.
When the speech value is recognized the trigger will execute all of its corresponding events[3] in order.

![trigger](https://cloud.githubusercontent.com/assets/6128886/3487779/f40bece6-04a1-11e4-9142-adba700010e8.PNG)

##### 3. Trigger Events

Trigger events are carried out when a trigger executes. 

The events can be multiple Action Sequences or other Triggers, in this way an you can reuse action sequences in multiple triggers or create a template trigger of sorts.

##### Saving/Loading

Profiles are stored in XML format and can be edited by hand in any text editor which supports it. You can also load profiles made by other people. Additionally you can store simple variables usint the database. Today you can use gavpi's speech to read them out, saving you some time. But in the future you'll have access to boolean logic and updating at run time, watch out.

### Settings

The settings allow you to change which voice pack and localization gavpi will use. Microsoft fully supports recognition in several languages, English is tested the most however.

Additionally you can configure push to talk, useful if you're dealing with chatter or ambient noise.

![settings](https://raw.githubusercontent.com/baykovr/AVPI/master/img/settings.png)

###### Thanks and have fun, if it works that is.
