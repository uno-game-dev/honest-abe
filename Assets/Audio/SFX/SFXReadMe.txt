*** Version 2 ***

***
New things will be notated by stars like you can see here 
***

Here is a description of / guideline on how to use each SFX in the Audio Module: (this list will be updated periodically,)

NOTE: As SFX creator, I am simply creating sounds as loud as possible without clipping, as Unity’s volume function is shit. Therefore, it will be up to the dev implementing sounds to test for volume as new sounds are put in.

In the order they appear (to me at least):

*********************************
First, some notes on things that are old, but need changes:
1. Jump Start is in the module, with the proper sound, but it’s not being triggered
2. Menu Click 1 through Menu Click 4 are in the module with proper sounds, but aren’t being triggered. 
3. PLEASE get rid of the constant walk loop that plays even when Abe isn’t walking!
4. Turn off walk sounds for enemies completely. Only Abe will now have footstep sounds.
5. Turn off “swing” (ya know, the swoosh sound) for all enemies EXCEPT Saber officers. Only Abe and Saber officers should have a “swing” sound. All other enemies should keep their IMPACT sounds, but no “swing” sound for melee or knife.
**********************************

*******
Ok, I’m going to keep everything in the order it appears in the module, as before. New things will be highlighted by the stars.
******* 

***
-Abe Roar
This is for the very beginning when Ave throws his arms out and yells. Right now there’s just some placeholder sound for beta, but it needs to be wired up nonetheless.
***

-Axe Pickup
When you pick up the axe, at any point

-HealthPack Pickup
When you pick up (walk over) any health packs

-Trinket Pickup
When you pick up any trinket

-Footsteps
This is a loop of grass footsteps. Only to be used if we do it that way.

-Light Footsteps
Same ^. Ed made two things and I don’t delete

-Grass Footsteps 1 - Grass Footsteps 2
If we go the direction we want to, these are the 7 individual grass footsteps to be played RANDOMLY as the player walks. Needs to be matched up with walk animation timing.

-Light Axe Swing
Sound the Axe makes being swung through the air on every light attack

***
-Light Axe Swing two more times
Just to give variation to the axe move. For things like this Ed, nothing is required of you.
***

-Light Axe Impact 1
Sound the axe makes impacting an enemy on the first swing 

-Light Axe Impact 2
Sound the axe makes impacting an enemy on the second swing

-Light Axe Impact 3
Sound the axe makes impacting an enemy on the third swing

***
-Heavy Axe Lift
The heavy hit is comprised of two parts: lifting up the axe over the head, and coming down with the swing. Given that, there needs to be a sound for each. As of right now, Heavy Axe Swing plays, but it plays when Abe lifts the Axe. So this is a sound for when Abe lifts it, and Heavy Axe Swing can be moved to the second part
***

***
-Heavy Axe Swing
Sound the axe makes being swung through the air on every heavy attack
This was a sound already in place, but I’m putting the stars since it needs to move to a new part of the animation. Lift needs to be where Swing currently is, and new wiring needs to be created for Swing
***

-Heavy Axe Impact 1
Sound to be played at SAME TIME AS ‘Heavy Axe Impact 1.1’ ; sound the axe makes when impacting an enemy on every heavy swing.

-Heavy Axe Impact 1.1
Sound to be played at SAME TIME AS ‘Heavy Axe Impact 1’ ; sound the axe makes when impacting an enemy on every heavy swing.

***
-Saber Swing
Only Abe and Saber Officers have a swing sound. This is for the Saber officers.
***

***
-Saber Impact
Impact sound for when a Saber Officer makes contact with Abe
***

***
-Knife Impact
Knife enemies don’t get a swing sound, but they do still get an impact sound, this is it.
***

-Damage React Light (currently 3, all named same thing)
Vocal Sound enemies make when hit by light attack  (random)

-Damage React Heavy
Vocal sound enemies make when it by heavy attack

***
-Damage React Heavy
Another variation sound, and again, Ed doesn’t need to do anything here
***

***
-Damage React Bear
The bear should make a sound when hit (sometimes, I don’t think he should react to every hit. This is the bear reacting
***

-Jump Start
Sound made by Abe jumping in the air

-Jump Land
Sound made by Abe landing from jump

-Light Punch Swing
*this is named weird, as this is simply the swing sound* Sound made by a punch cutting through the air

-Light Punch Impact
Sound made by a light punch hitting the enemy

-Light Punch Impact Enemy
Sound made when an enemy’s punch hits Abe

-Heavy Punch Swing
Sound made when Abe’s heavy punch impacts an enemy

-Heavy Punch Impact 1.1 - 1.3
Sounds to be played ALL AT SAME TIME when Abe’s heavy punch impacts an enemy

***
-Pistol Fire
Sound the pistol makes when fired
***

***
-Rifle Fire
Sound the rifle makes when fired
***

***
-Pistol Reload
Sound the pistol makes when reloaded
***

***
-Rifle Reload
Sound the rifle makes when reloaded
***

-Introduction Music
Intro music, self explanatory

-Forest Level Music
Self explanatory

***
-Forest Boss
Self explanatory
***

***
-Battlefield Music
Self explanatory
***

***
-Battlefield Boss
Self explanatory
***

***
-Ballroom Music
Self explanatory
***

***
-Ballroom Boss Intro
Music to be played when encountering Ballroom Boss. Should play just ONCE. To be seamlessly and immediately followed by Ballroom Boss Loop (which loops)
***

***
-Ballroom Boss Loop
After Ballroom Boss Intro plays JUST ONCE, this should loop until the boss battle is done.
***

-Death
Sound to be played when Abe dies

***
-Death (two more times)
Again, just variations, as ever, Ed needs do nothing here.
***

***
-Abe Death
Sound that should be made ONLY WHEN ABE DIES
***

-Menu Click 1 - 4
Sounds to be played ALWAYS IN THE SAME ORDER, 1 through 4, upon keying around or clicking on any menu item. They need to be in the same order because they follow the ‘Abe Rising’ motif
