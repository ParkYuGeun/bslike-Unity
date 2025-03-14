using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName ="Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType {Melee, Range, Glove, Shoe, Heal}

    [Header("# Main Info")]
    public ItemType itemType;   //위에 내가정한 아이템 종류
    public int itemId;          //아이템id
    public string itemName;     //아이템이름
    [TextArea]
    public string itemDesc;     //아이템설명
    public Sprite itemIcon;     //아이템 아이콘


    [Header("# Level Info")]
    public float baseDamage;    //기본데미지
    public int baseCount;       //근접무기개수, 원거리무기관통횟수
    public float[] damages;     //
    public int[] counts;        //


    [Header("# Weapon")]
    public GameObject projectile;
    public Sprite hand;
}
