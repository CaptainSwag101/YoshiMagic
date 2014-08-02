*******************************************************************************

 Yoshi Magic - Version 0.3 Alpha - Developer Version
 Mario and Luigi : Superstar Saga editor
 Last Updated:  March 23, 2012

*******************************************************************************

Index Table


I    - What's New!
II   - Introduction
III  - Intructions on use
i)   - Before use
ii)  - During/After use
IV   - Text Editor
V    - Enemy Editor
VI   - Battle Editor
VII  - Character Editor
VIII - Room Properties
IX   - Sprite Viewer (Alpha)
X    - Copyright
XI   - Thanks

Note:  Most of the things below are to do with Superstar Saga, as this readme
has not been fully updated yet.


*******************************************************************************

I: What's New!


-A bunch of stuff has been added to the editor to allow you to view information
from the DS games.  Mostly Partners In Time because of how Bowser's Inside
Story seems to compress more stuff.
-PIT Map Viewer and BIS Map Viewer - You should click on an item in the listbox
before attempting anything, really.  Before you use the Script Viewer in PIT,
it should be noted that you should first generate the map.
-PIT Enemy Viewer - Again, to view enemy data, use the listbox. I have not
labeled the second textbox, but it can be helpful when using the BAI Script
Viewer.
-PIT BMap Viewer - Ofcourse, we have battle maps! Not as much here as in the
original Map Viewer.
-PIT Sprite Viewer - Yup, we have this. Please note that the graphics are not
perfect.  If you take a look at the battle sprites that should be quite
obvious. While the pixel data is indeed correct, I have not found a way to
detect which layers are the double width ones.
-PIT BAI Script Viewer - It is in progress. There are a ton of commands we need
to find out more about. However, we already know a few and have listed
descriptions beside some of them.
-BIS Item Viewer - We only have the layout up right now, so it is technically
not a "viewer" yet.
-Tools - Some stuff to help me with the developement progress. You can ignore
this if you don't understand any of it. You more than likely won't if you've
never opened up a hex editor before.


*******************************************************************************

II: Introduction


Thanks for using Yoshi Magic, brought to you by Salanewt and charleysdrpepper.


This Readme file should contain information about Yoshi Magic, the Mario and
Luigi series editor. We first started locating data from Superstar Saga in
June, 2009. Since then, we have produced the editor, and have enabled it to
edit the data of Mario and Luigi: Superstar Saga. It wasn't until Nov 30 2009
that we started looking for data in Mario and Luigi: Partners In Time. We have
also started looking into Bowser's Inside Story. We hope that you have fun
using the editor, and also for it to continue helping you for many of your
Mario and Luigi hacking needs. 


*******************************************************************************

III: Instructions on Use


i) Before use


There are some things that you MUST do before you do anything with the editor.

1. You must make sure that the folder which was given to you includes the
version of Yoshi Magic which you wanted to download, and this readme. If any of
these are missing, you should redownload the editor from Yoshi's Lighthouse.

We've also added two folders that hold images. The editor uses these, so do not
delete or rename the folder and/or images. The folders are Battle Backgrounds,
which currently only supports PNG; and Sprites, which only supports GIF.

2. The ROM has to be the North American version, but we can not tell you where
to find this. We are also working on compatibility for the European version,
but the North American version is the most compatible.

3. Once these have been done/made sure of, you may enjoy the editor!


*******************************************************************************

ii) During/After use


All that you have to do when you have completed the steps above is to report
any errors that you find. Errors include changes not being saved, unexpected
closing of the editor, etc. You should not report anything if it has nothing to
do with editing the game (like how the editor looks, or how things are
organised), unless you have problems with the editor (this should only include
things like health problems or it being hard to see (but there should be few
health problems)).


*******************************************************************************

IV: Text Viewer


Toolbar - File - Save

Saving is now possible. This saves only the text group you have loaded. This
also overwrites foreign text. It repoints all pointers to a word aligned
address after the end of the previous line.


Toolbar - Edit - File and Replace

This is a feature I have added to make replacing all instances of a word
easier. For example, if you wanted to change Beanbean to Mushroom. If you press
Ctrl + an item in the listbox, it will select that item in the Text Editor so
you can make more specific changes much easily if desired. (Depending on your
changes, you may want to press Find again in the Find and Replace form.)


Toolbar - Help - Help

Basically a summary of the most important things to note: 

-------------------------------------------------------------------------------
If you are viewing this, then we must assume that you are having some
difficulty with Yoshi Magic. Most of the text editor is quite simple to use,
however, there are some things you should note below.

First comes the line number. When editing the text, it doesn't matter what you
have before the <, since the code will first look for the < before really doing
anything.

<Width, Height> = The size of the conversation box. Every line should have
this. Any other uses of < or > later in the line will be read as regular
characters.  They don't appear in-game, though.

[##] or [##=##]  = These are used as the commands/arguments.

{##} = These are used to replace the "characters" that do not match any
original font. (Like the A/B/L/R/Control Pad characters.)  Currently, these
only show up for numbers from 0 h - 1F h.

\ = You may use this if you want to surround your text in brackets/braces.
You only need it behind {, or [. You don't have to worry about }, or ].
Ex: \[Braked text], or \{Braced text}
If you want to use a back-slash in your text, you should put in \\.
(Two backslashes would be \\\\.) 

If you need further assistance,  you could try visiting Yoshi's Lighthouse.

Website Home Page: Yoshi's Lighthouse Front Page
http://s3.zetaboards.com/Lighthouse_of_Yoshi/index/
-------------------------------------------------------------------------------


Combo Box - Selecting a Text Group

The combobox in the upperhand corner is where you select the Text Group to
show. This will load from the ROM of all the text in that group, so it is
important to save before changing this group, if you have made any changes. 
(An alert will pop up when you click this box, if you have any unsaved 
changes.) On load, the editor loads the Story Text group which takes about 2
seconds to load. The other two groups load right away.

Story Text    - 2434 lines
Battle Text   -  205 lines
Suitcase Text -   13 lines


Textbox & Go button - Go to line number

To the left of the combo box is a textbox and the "Go" button. This is used to
go to a line number. (It moves that line number to the top of the listbox.)


Find Textbox & Previous!/Next! - Quick Find the next matching instance.

Type what you are searching for into the textbox. If what you are searching for
breaks into two lines, changes in color, or anything else, you may need to
include [#], {#}, etc. Replacing # with the necessary number(s). When you click
one of the buttons to the right, it will search until it finds a match, in
which case, it will go to it in the listbox. If there is no mtch, an alert of
"No matches." will pop up.


Listbox - Display text group.

This displays all the text of the selected group. Selecting one will allow you
to edit the line in the textbox below.


Textbox & Edit button - Edit a text line.

You may edit the text anyway you like, but it is recommended that you test one
line at a time in-game. For example, if you dont use the line-break ([0]), when
you have enough text for 2+ lines, the text in the combo box might not look so
pretty.


Statusbar - Location in a Hex Editor

This is here in case you want to locate the text in a Hex Editor.


*******************************************************************************

V: Enemy Editor


The Enemy editor is extremely fun and easy to use. You can edit any of the 189
enemies in the game (some enemies are made of more than one enemy slot), and
have several neat things with them. We, even to this day, are still finding
things here. Please inform us of anything you can do with enemies, especially
with Attributes and Resistances.


On the left, you see the list of enemies. Some enemies are combined to form one
enemy. (Like Rex, which holds three enemy slots. one for normal, the next for
smashed, and the next for even more smashed, as seen in the Sprites tab.) When
selecting Enemies, you may notice the Enemy Offset at the bottom changes, this
is to help people know where the Enemy Data is in the binary. If you don't use
a hex editor, then you don't need to worry about it.

In the Enemy Data Tab, on the left side, you see a line of textboxes.

Name - You may edit the name, try not to make the name too long, or else it
won't display correctly in Battle. Either way, we will only save the first 17
characters, which should be enough space for you.

Level - This seems to affect how often you fall when running away. Perhaps
calculated with coins? This should be any value from 1 and 99. So far, we have
found that this gets the average of Stache. (The sum of all the stache for both
the Mario and Luigi Level Up Databases up to this level, and divides by 2.) 

HP - The Enemy's HP, but note that Enemies like Bowser, should have a script
for ending the battle.

Defense - Self-explanatory, but its counterpart, Power, is done through
abilities.

Speed - This helps to determine who goes first in battle. This also affects how
quickly Mario and Luigi can run away. This should not be 0.

Experience - This ammount is given to Mario and Luigi. It is not divided up.

Coins - I'm surprised Bowser has coins. Though, you don't get them, since a
script ends the battle. Also, Tolstar may have a script for giving you back
your coins as well.


On the right side, you see the Enemy Flags.

The first four should consist of Status Effect chances for Bros. Attacks.

Unknown - This one just happened to be included in with the byte for status
effects, so if you can figure out what this does, then you can have a
watermelon. Note: They probably won't be in percents, unless they either, use
one of the percent databases of the belows, or they are different.


Cyclone Bros. - Cyclone Bros's Status Effect is Stun. This seems to affect the
chance of stunning enemies when you hammer them from the start, as well.
(Percents Database: 083BA960)

Fire Bros. - The status effect here is Burn. We looked into assembly and found
out that the four options index to a byte that holds the Chance percentage,
(Percents Database: 08201123)

Thunder Bros. - Using this in Non-Advanced, the status effect is Def Down, The
advanced version is Pow Down.
(Percents Database: 08201127)

Each Percents Database is as follows: 0%, 30%, 60%, and 100%.




The next three are the Solo attacks. This is for which Solo attacks the enemies
are strong or weak to.

Jump - This is the only one you can get hurt with, if you choose spiny.
Hammer - Unlike Jump, you have a selection to "Miss" the enemy.
Hand Power - Same choices as Hammer.

The chooses are as follows:
Normal - No repelence to the solo attack, everything is normal.
Spiny - Only for Jump, if you jump on the enemy you lose HP.
Miss - You miss the Enemy dealing no HP. (Used for Flying Enemies)
Weak - Deals less HP than you could with Normal.
Immune - The game doesn't use this one, but it works the same as miss, except
no "Miss" label.


The last two options in this collumn are for which elements the enemy are weak
or strong to.

Fire - This is from Mario's Hand Power.
Thunder - This is from Luigi's Hand Power.

These are the only elements this game seems to focus on, along with 3 choices:

Normal - No weakness or strength to the element, the element acts as normal.
Critical - Enemy's Weakness. The element when used deals double damage.
Heal - Enemy's Strength. This ofcourse, heals the enemy.


At the bottom, are the Item Rewards.

The first is the regular Item Reward, which you can obtain if you do not have
the Gameboy Horror SP equipped. Rarity is how likely you are to get the item.

The next one is the Rare Item Reward, which you can recieve in battle by using
Swing Bros, or on the Rewards Screen when you have the Gameboy Horror SP
equipped, in which case rarity is ignored.


In the Sprites Tab, you can change the Enemy's Sprite.


In the Abilities Tab, you can edit, well, abilities. The most abilities an
enemy has is 8. When enemies do their abilities, they can often be countered.
Sadly, Hand Powers cannot be used for countering. Power affects how much damage
Mario or Luigi recieves when hit. At the bottom are the Attack Arguments. These
are values that are used with the Enemy's Attack. Not all abilities use these,
so you may be better off modifying the ones that aren't 0.


Due to the intensity of coding a Script Editor, the Enemy Scripts have not been
coded yet.


When you are done here, feel free to click the Save Block to Save.


*******************************************************************************

VI: Battle Editor


In the Battles Editor, we have two tabs, the first is the Battles Tab.

On the left, we see the list of Battles, each show the name of the first listed
enemy in a battle. You may notice Battle Offset at the bottom, as well. Same
purpose as with the Enemy Editor. Only there if you want to HEX hack.

What we've decided to do was make a nice display of what it might look like in
battle in the upper-left hand corner. Underneath this is the Backgrounds and
Arrangements. Altaring those will update what you see in the display, to help
you get the perfect Background and arrangement you may want. If the Battle
Background is 0, the background of the room in which you initiated the battle
will be used. Please be aware that the arrangement is mostly approximate, as
you can notice that Bowser in one battle appears a pixel or two above it's
actual location.

On the upper-right hand side, you see Disable Run. This is mainly used for 
required battles, like Boss Battles or tutorials, for example.

Underneath that, you see Chance for battle, and Possible Battles. If the
percent is for example, 90%, and the RNG generates like 95. 90 subtracts from
95, giving you 5, and if the next battle is 5 - 10%, that one will be
initiated. (I think? We have not looked into assembly here, though.)

In the next section, there is a large combo box which is used as Sprite Stack
sizes. What this is, is how much space is available to store the images pixel
data in. What doesn't make sense to me is why won't the game just use the
"Automatic Stack" and be done with? (The one I believe makes the image fit in
its own stack without overflows which may cause graphical glitches.)

Below this, the 6 combo boxes you see on the left determine which Sprite Stack
the enemy's sprite is put in.

To the right, you will notice a list. These are the 6 different enemies for
the battle. Enemies that are not in the battle are 0, but what determines if
they are in the battle or not is whether they are hidden or not. Though, there
are also some enemies that are hidden that also display in battle. Like Troopea
in Battle A4, which has a paratroopea. What is strange is that Battle B4, has
Paratroopeas but no troopeas.

Lastly, the column of unknowns is completely unknown.


On the Next tab, you see the Room Battle Backgrounds. Like I said earlier,
Battles refer to these if the Battle's Background is 0. This is pretty simple.
On the left is the list of rooms, and on the right is the Battle Background
which can be changed by the numeric underneath it.


When you are done here, you may also save by clicking the Save Block.


*******************************************************************************

VII: Party Editor


The Part Editor is where you can edit Mario and Luigi's starting stats. We have
not yet included all the stats, but you can have fun editing the ones we have
included. The two tabs will probably be combined in the future. Also, the third
tab is where you can edit Level Up stats.

Starting Stats
Level - This can be between 1 and 99.
Experience - This is suppose to be 24-bit.
Current - Your Current HP
Base - Your max without Bonuses.
Max - Your absolute max.
HP, BP, Power, Defense, Speed, and Stache are the editable stats.


*******************************************************************************

VIII: Room Properties


Room Properties is where you will be able to edit attributes of the rooms.
Currently, you cannot edit anything but the Underwater flag and the X, Y, ???
coordinates in the Map Locations tab.

Room Properties tab
This is in Beta stages, what you see disabled shall eventually be editable, but
for now, it is here for view. This is the main tab, so you should click a list
item in the listbox. After that, you may view the Tile Viewer and Map Viewer
for what the Room has in store.

Names tab
This is currently for display, but I'm hoping to making it editable as well in
the future.

Map Locations tab
Everything that this tab serves a purpose for should be editable, so have some
fun with it.

Tile Viewer tab
Here, there are three different things. On the right is the Rooms Palette Set.
On the left is the compressed images. Each room can use 1, 2, or 3 different
compressed images to be used to put into the tilesets. At the bottom are tile
tilesets. Each room can either have 1 or 2 of these. Also, each tile in the
tileset can choose between 16 different palettes in the Room's palette set.
Which is by row. So I have included another numeric box, that if you change,
you can see the 16 different palette choices in the compressed images area.
Each palette row is a pallete choice in a palette set.

You can now right click Image 1, and Import/Export the image, this is not
savable to the ROM, so enjoy using it for experimental purposes for now. If you
want to see the changes in the Tilesets after importing a picture, you may need
to change the palette. If you change the Image's 1 numeric box, you will have
to re-import the image again. (Since the new data would replace it.) I have not
made this available for Image 2 and 3, so it should be obvious that its still
being worked on.

Map Editor tab
I have coded this so that once you click this tab, that's when the Map
generates. I did this to save time if you are not trying to view the map, since
it can take time to load. They are also done in three layers individually.
To the right, you see a checkbox for whether to show warps or not. They have
their own data, but I have not made them editable or viewable yet. Next, you
see a button for Blending. Later, I plan on removing the button and get the 
code to look at the blending data to see how it should blend.Below, are  the
checkboxes are for what layer to display. The bullets are for which layer to
edit. The Beta box below is about selecting a tile from the tileset (by X and
Y) and putting it in the map (by X and Y, also.) If you press the button, the
changes will take place in the ROM directly. 

Clicking the tilesets will update the tileset coords. (Ctrl-clicking updates
the width/height from the coords.) Clicking the Map updates the map coords
(Ctrl-clicking automatically presses the Save to ROM button.)


*******************************************************************************

IX: Sprite Viewer (Alpha)

The Sprite Viewer has a lot of work to be done. There are compressed sprites
included here, as well as uncompressed sprites. When you change the upper-most
numeric, you can do #*** for the sprite groups.


*******************************************************************************

X: Copyright


All rights reserved to Nintendo for Mario and Luigi: Superstar Saga. The editor
is 100% free. If you have purchased this program, then you should report the
merchant who sold it to you. Please ask either myself (Salanewt) or
charleysdrpepper for questions about distribution. We will most likely not
allow any other website but Yoshi's Lighthouse to host it until we have
finished the editor for good, or at least a public version which everyone can
access. There is NO way that we will allow someone to host a beta version. If
you do find a beta version on any website (other than Yoshi's Lighthouse), then
there can be no guarantee that it has not been tampered with.


*******************************************************************************

XI: Thanks


We would like to thank you, for letting us, be ourselves! Lol, but I would like
to thank you for downloading Yoshi Magic, and using it. It means so much to us,
considering the fact that it is a work in progress, we can use all of the
support that we can get.

Also, we would like to thank you for actually reading to the end, because you
are suppose to at least read up to and including the Instructions on Use.


*******************************************************************************