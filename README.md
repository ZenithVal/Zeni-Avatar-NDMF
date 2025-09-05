# Zeni Avatar NDMF

A collection of niche, non-destructive tools for modular avatar creation, primarily geared towards VRChat.  
Designed to be used alongside [Modular Avatar](https://github.com/bdunderscore/modular-avatar).  

💡 *Name ideas welcome — I couldn’t think of something as catchy as “Modular Avatar” or “Prefabulous” starting with Z.*  

📥 **VCC Repo**: [https://zenithval.github.io/vpm/](https://zenithval.github.io/vpm/)

---

## ✨ Components

### 🟦 ZA Remap VRChat Collider
A tool to remap avatar descriptor colliders to target bones and define custom shapes for them. 

**Example Use cases:**  
- Making a non-destruvtive prefab for setting up your standard Colliders.
- Reassigning colliders for weapon/props to interact with PhysBones. (Melee, guns, ect)
- Moving the hand collider on a constraint object to the head, allowing biting by holding trigger.
   - (Example Prefab for this soon)

---

### 🟦 ZA Set View Position
A component to set the avatar descriptor view position based on the position of a GameObject in the world.  
- Works best on a **child of a Modular Avatar bone proxy** targeting the head.
- Great for non-destructivly setting the viewball with a prefab setup. (Adjust height with heels? Already adjusted view)

---

## Credits
This project was built by learning from:  
- [Modular Avatar](https://github.com/bdunderscore/modular-avatar)  
- [Prefabulous Avatar](https://github.com/hai-vr/prefabulous-avatar)  

---
