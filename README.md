# HiveMine Unity Project
[Demo Video Not Available Yet]

## Team Members
- Felipe Zavala
- Ivana Banderas

## How to Run
1. Clone repository: `git clone https://github.com/fezavala/HiveMine`
2. Open in Unity Version 2022.3.50f1 (note this version is not secure for now)
3. Run the project in the Unity Editor

## Features Implemented
- Feature 1: Player Character with health, inventory, and attacks
- Feature 2: Crafting System
- Feature 3: Basic Post-Processing effects such as shadows, vignette, light color correction, and light SSAO
- Feature 4: Destructible tiles with visual shake effect when hit (no effects when destroyed unfortunately)
- Feature 5: Collectible ores and gems
- Feature 6: HUD Display with ore count and currently equipped tool
- Feature 7: Some optimization in static wall batching

## Controls
Note: Controller input currently incomplete
- WASD / Left Joystick: Move
- Left Click: Use tool/weapon
- Mouse Scroll: Swap tools
- E: Interact with crafting table (must be up close next to it)
- T: Take 1/4 heart damage (here only for testing purposes)

## Known Issues
- Issue 1: Game may visually stutter if system is not powerful
- Issue 2: Rendering inefficiencies, project is not fully optimized (tiles have many unnecessary tri's and may not be culled properly)
- Issue 3: The wall obfuscation tiles do not update when tiles are destroyed
- Issue 4: No inventory screen to show current tools in inventory
- Issue 5: Not much visual feedback
- Issue 6: No SFX
- Issue 7: No challenge or end state
