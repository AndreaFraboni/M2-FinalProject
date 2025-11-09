using System.Globalization;
using UnityEngine;


[System.Serializable]
public class Weapon
{
    // enum Damge Type
    public enum DAMAGE_TYPE
    {
        PHYSICAL,
        MAGICAL
    }

    // private variables
    [SerializeField] private string name;
    [SerializeField] private DAMAGE_TYPE dmgType;
    [SerializeField] private ELEMENT elem;
    [SerializeField] private Stats bonusStats;

    // Costruttore Weapon
    public Weapon(string nameWeapon, DAMAGE_TYPE damageType, ELEMENT enumElement, Stats bonusStatistics)
    {
        name        = nameWeapon;
        dmgType     = damageType;
        elem        = enumElement;
        bonusStats  = bonusStatistics;
    }

    //getter
    public string GetName()
    {
        return name;
    }

    public DAMAGE_TYPE GetDamageType()
    {
        return dmgType;
    }

    public ELEMENT GetElement()
    {
        return elem;
    }

    public Stats GetBonusStats()
    {
        return bonusStats;
    }

    //setter
    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetDamageType(DAMAGE_TYPE dmgType)
    {
        this.dmgType = dmgType;
    }

    public void SetElement(ELEMENT elem)
    {
        this.elem = elem;
    }

    public void SetStats(Stats bonusStats)
    {
        this.bonusStats = bonusStats;
    }




}
