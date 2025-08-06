using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Material,
    Weapon, // can expand to types of weapons
    Armor, // cna expand to type like boots, chestplates, gloves, headgear, etc.
    Accessory, // can break down more like rings, belts, amulets, etc.
    Consumable, // should this be a type or a config? and then food or potion could be a item type
    Equipment, // covered by Armor?
    Furniture,
    Treasure,
    Junk,
    Gift // should be a config type if giftable 
    //same iwth colectable
}
