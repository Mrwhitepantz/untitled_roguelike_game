using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName ="Item", menuName ="Item/baseItem")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public string itemDescription = "New Description";
    public int attack = 0;
    public int defense = 0;
    public int speed = 0;
    public Sprite icon;
    public enum Type { Default, Consumable, Weapon}
    public Type type = Type.Default;

}
