*** Version 2 ***

ED!! I AM DELETING OLD GUIDELINES AS YOU PRETTY MUCH KNOW HOW THINGS WORK NOW. I DON’T FEEL LIKE GOING THROUGH AND DELETING ALL THE STARS, SO I SIMPLY ADDED ALL THE NEW SFX TO THE END. I’LL CLEARLY MARK WHERE THEY START. ALL NEW SHIT IS AT THE END OF THE LIST EXCEPT FOR ‘ABE ROAR’ WHICH I JUST CHANGED THE SOUND ON, YOU OBVIOUSLY DON’T NEED TO MESS WITH THAT.

NOTE: As SFX creator, I am simply creating sounds as loud as possible without clipping, as Unity’s volume function is shit. Therefore, it will be up to the dev implementing sounds to test for volume as new sounds are put in.

In the order they appear (to me at least):

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


************************************************************* OK ED HERE’S WHERE THE NEW, FINAL SFX STUFF STARTS*********************************************************

-BushWhacker Hit (6 of them)
This is the sound to be played when a BushWhacker takes damage. Obviously you only need wire it up once since these (and many others to follow) all have the same name

-BushWhacker StabOnGround
Sound that is played when, after Abe is successfully tripped, a BushWhacker jumps on him to stab 

-Officer Hit (8 of them)
This is the sound to be played when an officer takes damage.

-Officer Death (5 of them)
This is the sound to be played when an officer dies

-Death (two more, which go with the 3 above from last time)
Don’t worry about these, just wanted more variation in melee enemy death sounds

-Woman Hit (9 of them)
This is the sound to be played when a Woman takes damage

** Note: you should notice Officer and Melee and Lee and others have specific death sounds but Women and Bushwhackers and some others do not. For those that don’t have specific death sounds, just have their random hit sounds play when they die as well.***

-Battlefield Boss Hit
This is the sound to be played when the Battlefield boss (big officer) takes damage / dies.

-Battlefield Boss Intro
This is the sound to be played when the Battlefield boss first appears, much like how the bear does his long growl on appearing

-Lee hit (8 of them)
This is the sound to be played when Lee takes damage

-Lee Death
This is the sound for Lee dying. Only one.

-DT Restoration (4 of them)
So, when an execution is performed, the DT bar is raised, but there’s no audio feedback for that. So when that happens, 1 of these 4 sounds should randomly play.

-JumpLand Grass
This is the sound to be played when Abe lands from a jump in the Forest and Battlefield levels. Which means you need to take the original “Jump Land” sound from way up above and make it only play when Abe lands a jump in the Ballroom.

-Abe Gets Tripped
This is the sound to be played the moment when a BushWhacker SUCCESSFULLY trips Abe

-Axe Throw
Sound to be played when the Axe is thrown

-Axe Throw Hits Target
Sound to be played when a thrown Axe hits its target

** Many of these new sounds are louder than before. So I tried to set their volumes mostly at .2, thinking that’d have them mesh best with the previous ones. However, it’s going to take  a play through, hearing them play in-game, to tell where the volume’s should really be. I don’t mind doing this but I can’t do so of course until they’re wired in.
**