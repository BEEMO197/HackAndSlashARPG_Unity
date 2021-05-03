# HackAndSlashARPG_Unity

Simple Hack and Slash Action Role-Playing Game

Custom abilities, enemies, items, and a dungeon generation in progress.

Abilities can be custom made in the Unity editor through Unity's Scriptable Objects.
  Abilities that the player has access too are as follows:
    1. Damage boost, increases damage by a small amount
    2. Max health Boost, increases max health by a small amount
    3. Speed boost, increases movement speed
    4. Dash, cooldown is set to 0 just for debugging and testing purspoes
    5. Ranged damaging ability, shoots out a projectile, and then explodes after it reaches a set distance
    6. Aoe Ability around the player, and gives a small speed boost in the aoe

Enemies are placed in the world by hand, and could be spawned in through a simple script, but just to showcase them they're just hand placed.

Items are made in a similar fashion to Abilities, they can be made through Unity's Scriptable Objects, and even have custom UI on them,
that allow you to press a button on a Item object, and generate a new set a stats for it, that takes Rarity of the item, and Level of the item into account.

Dungeon generation is in the works, and currently just need to get dynamic nav meshes on them, and for the most part they should be working, they include randomized rooms,
and connection to other rooms, hoping to make them have levels, but thats for future development.

