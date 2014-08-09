### [GAVPI](https://baykovr.github.io/gavpi.html) Alpha v0.02

08/07/2014 Latest build [available directly here] (https://github.com/baykovr/AVPI/releases/download/v0.02/GAVPI.zip)
[ (Release Notes) ]
(https://github.com/baykovr/AVPI/releases/latest) 

08/08/2014 If you are having issues with gavpi being unresponsive in games, try running it as administrator (right click run as...)

#### About

GAVPI is an open source alternative to VoiceAttack, a popular speech recognition key bind tool. Using gavpi you can execute repetitive or complex tasks using your voice.

You can:
+ Bind a set of action sequences (mouse/keyboard presses or text-to-speech events) to arbitrary phrases.
+ Create profiles for different games or applications.
+ Enjoy the program for free.

#### Installation

Just extract and run the exe.

#### Usage

![main](https://cloud.githubusercontent.com/assets/6128886/3487757/17659892-04a0-11e4-9e81-0e9356861113.PNG)

##### Profile
The profile is broken down into three sections.

##### 1. Action Sequence

An action sequence is composed of sequential "actions", one of keyboard or mouse press/down/up, wait or speak. The key or mouse presses are carried out at direct input level which is equivalent to your physical key presses. Action sequences are carried out very quickly, in some cases near instantly so it is helpful to use the wait (milliseconds) command if an application is failing to registered presses.

Note that Key/Mouse Down action's will hold a specific key until an Up or Press action.

![action_sequence](https://cloud.githubusercontent.com/assets/6128886/3487783/8164d18e-04a2-11e4-8f9f-46318d1a06be.PNG)

##### 2. Trigger 

Triggers are composed of three parts type, name and value.

Currently only one type of trigger is implemented, VI_Phrase. This is the speech recognition trigger type its value being a spoken word or phrase.
When the speech value is recognized the trigger will execute all of its corresponding events[3] in order.

![trigger](https://cloud.githubusercontent.com/assets/6128886/3487779/f40bece6-04a1-11e4-9142-adba700010e8.PNG)

##### 3. Trigger Events

Trigger events are carried out when a trigger executes. 

The events can be multiple Action Sequences or other Triggers, in this way an you can reuse action sequences in multiple triggers or create a template trigger of sorts.

##### Saving/Loading

I know its annoying, but you need to load your profile each time the application starts for now. Profiles are stored in XML format and can be edited by hand in any text editor which supports it. You can also load profiles made by other people. I will be making a few profiles for myself which will be available on the main project [page](https://github.com/baykovr/AVPI).

### Settings

Settings are untested, since I only have en-us and one voice synthesizer to choose from. None the less it is included as requested but not verified to function properly. 

![settings](https://cloud.githubusercontent.com/assets/6128886/3487803/750a8976-04a5-11e4-879e-c2393485907e.PNG)

###### Thanks and have fun, if it works that is.
