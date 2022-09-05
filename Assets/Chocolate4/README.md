# Top down shooter game

Fun zombie shooter game with nice static environment. Zombies use [NavMeshAgent](https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent.html).
Player bullets and zombies are handled with a pooling system. Zombies are pooled throught the entire game, while player bullets only exist in game.
Theres no reason for the above choice, I just wanted to show off I can do both.

For the character animations I mostly got them from [mixamo](https://www.mixamo.com/#/), but it's worth mentioning I used the animation rigging package
to aim the gun and get player hands to use IK.

I used the LeanTween package to tween the UI.

Some things I'm guilty of in this project is that sometimes I've been hardcoding some values here and there. I'm especially guilty of this in [GameManager.cs](Scripts/PersistantThroughLevels/GameManager.cs).

I applied some postprocessing to get the extra polished feel to the game. 

One more thing worth mentioning is that I've decided to use ragdolls in this prototype. To be completely honest this has been the first time I've used them so I'm absolutely sure
I've made alot of mistakes while trying to make them work.

On the final note I'd like to point out that I've not used cinemachine in this game. But I definitely could've done that just to get a virtual camera in front of the player
when he's performing the dance moves.

# Controls

WASD movement, QE to rotate camera, scroolwheel to zoom in out, mouse to shoot

# Things to implement

1. Camera
2. Save & Load
3. UI (score, ammo, hp)
4. Enemies move towards player (circle chaining range?)
5. player wasd + mouse rotation movement
6. player reload and shooting
7. editor tool to spawn enemies with preview (ctrl + LMB in the editor scene view)
8. shader hpbar, getting damaged

# Actually implemented

1. Camera
2. UI (main screen, enemy hp, player hp)
3. Enemies move towards player with simple range
4. player wasd + mouse rotation movement
5. player shooting
6. editor tool to spawn enemies with preview (ctrl + LMB in the editor scene view)
7. shader hpbar

# Preview

![](/Assets/Chocolate4/Art/MainMenu/1.png)
![](/Assets/Chocolate4/Art/MainMenu/2.png)
![](/Assets/Chocolate4/Art/MainMenu/3.png)
![](/Assets/Chocolate4/Art/MainMenu/4.png)
![](/Assets/Chocolate4/Art/MainMenu/5.png)
![](/Assets/Chocolate4/Art/MainMenu/6.png)