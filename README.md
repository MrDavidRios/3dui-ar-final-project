# SmashIT VR

## Introduction

Rage rooms offer an adrenaline-filled way for people to relieve stress, but they can also be costly, dangerous, and have restrictions on facilities and locations. To address these concerns, SmashIT VR provides a safe and immersive alternative. This virtual reality experience lets users relieve stress by ‘smashing’ a variety of virtual objects, simulating the thrill of a rage room without the associated hazards. The application offers users a wide range of hand-object and object-object interactions with immersive haptics and realistic breaking mechanics. It features 5 levels (including a tutorial), each presenting unique game objects to shatter, ensuring a diverse and engaging experience. 

## Project Information
**Team:** 
- Kevin Li (kl3285)
- David Rios (dar2209)
- Tina Tsai (tyt2111)
- Kei Asano (ka2836)

**Date of submission:** May 7, 2024

**Development Platform and OS Version:** Unity (Version: 2022.3.17f1)

**Platforms:** Meta Quest 2 and 3

**Project Title:** SmashIT VR

## Project Directory Overview
'Final Project' contains all files for the Unity project. Some of the important folders Within this directory: 

- **Assets/Audio:** Contains audio files used for sound haptics.
- **Assets/Baseball Bats:** Contains baseball bat prefab.
- **Assets/Deconstruct:** Contains breakable objects used in the different levels of the game.
- **Assets/Editor:** Contains code for tracking object destruction.
- **Assets/Haptic:** Contains code for vibration haptic feedback.
- **Assets/Lunar Landscape 3D:** Contains prefabs and scenes from Unity Asset store.
- **Assets/Material:** Contains material used in game levels.
- **Assets/Oculus:** Contains Oculus Project configuration
- **Assets/Prefab:** Contains various prefabs used in scenes.
- **Assets/Rocks and Boulders 2:** Contains rock prefabs from Unity asset store.
- **Assets/Scenes:** Contains level selector and levels 0, 1, 2, 3, and 4.
- **Assets/Scripts:** Contains all scripts organized in individual folders.
- **Assets/Sprites:** Contains sprites used in UI.
- **Assets/Teleportation Pad** (NOT USED)
- **Assets/TextMesh Pro:** Contains fonts, including custom font.
- **Assets/WALL-E:** Contains various files related to Level 3. 

## Special Instructions
Due to the complexity of Level 2 and Level 3, we recommend deploying those using Quest Link for best experience. All other levels can be run using a Quest 2 (standalone) without compromising experinece. 

For deployment, please build and run all 6 scenes as shown below in the same order:

- Scenes/Level Selector (Scene Index: 0)
- Scenes/Level0 Tutorial (Scene Index: 1)
- Scenes/Level1 (Scene Index: 2)
- Scenes/Level2-TimeCrunch (Scene Index: 3)
- Scenes/Level3-Moon-Flattened_Tina_0504 (Scene Index: 4)
- Scenes/Level 4_Smash (Scene Index: 5)

<img width="708" alt="Screenshot 2024-05-07 at 12 32 32 PM" src="https://github.com/MrDavidRios/3dui-ar-final-project/assets/56395320/ecec9c8a-a16b-4239-9059-258164592807">



## Demo Video URL
[https://youtu.be/Vi-sld9ctEw]

## References
The following assets were used for this project:
- https://assetstore.unity.com/packages/3d/props/weapons/baseball-bats-pack-102171
- https://assetstore.unity.com/packages/3d/environments/landscapes/lunar-landscape-3d-132614
- https://assetstore.unity.com/packages/3d/props/exterior/rock-and-boulders-2-6947
- https://assetstore.unity.com/packages/2d/textures-materials/brick/old-urban-brick-texture-59256
- https://pixabay.com/sound-effects/search/glass/ (Glass Breaking by Charlie_Raven)
- https://sketchfab.com/3d-models/japanese-traditional-wall-scroll-d4a38adefee64cbebf0c2ccad72e8ea7
- https://sketchfab.com/3d-models/china-vase-da1a1ebb640a42e3b70a32fdf7117dc1
- https://sketchfab.com/3d-models/hammer-stylized-af52617fec3a45579df87a1963405b38
- https://sketchfab.com/3d-models/eve-from-wall-e-c3acb81e214f4fc78bb3251339c2adc9


- https://assetstore.unity.com/packages/tools/custom-teleporter-pad-script-20098 （NOT USED IN FINAL APPLICATION）

The following scripts were referenced:
- https://gist.github.com/ditzel/73f4d1c9028cc3477bb921974f84ed56



