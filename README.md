# AntiStrangulation

![SCP:SL Plugin](https://img.shields.io/badge/SCP--SL%20Plugin-blue?style=for-the-badge)
![Language](https://img.shields.io/badge/Language-C%23-blueviolet?style=for-the-badge)
![Downloads](https://img.shields.io/github/downloads/ui2506/AntiStrangulation/total?label=Downloads&color=333333&style=for-the-badge)

---
AntiStrangelation is a **LabApi** plugin that allows you to control the mechanics of SCP-3114's strangulation and auto-spawn.
---

## Features

- **Disable Strangulation:** Prevent SCP-3114 from strangling players.
- **Flexible Auto-Spawn Control:**  
  - Prevents SCP-3114 from spawning automatically during Halloween (if configured).
  - Allows forced auto-spawn outside of Halloween based on config.

---

## Configuration (`config.yml`)

```yaml
disable_strangulation: true
disable_auto_spawn: false
```
disable_strangulation
true — SCP-3114 cannot strangle players.
false — Strangulation is allowed.

disable_auto_spawn
true — If it’s Halloween, SCP-3114 will NOT auto-spawn.
false — SCP-3114 will always auto-spawn regardless of Halloween.

Installation
Download the latest release from the Releases page.
Place the DLL file in LabApi directory.
Configure config.yml as needed.
