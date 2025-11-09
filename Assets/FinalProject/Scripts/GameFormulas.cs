using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

[System.Serializable] 
public static class GameFormulas
{
    public static bool elementalWeaknessHitted = false;
    public static bool elementalResistnaceHitted = false;

    // l'eore che attacca ha un vantaggio rispetto all'eroe che si difende ??
    public static bool HasElementAdvantage(ELEMENT attackElement, Hero defender)
    {
        if (attackElement == defender.GetWeakness())
        {
            elementalWeaknessHitted = true;
            return true;
        }
        else
            return false;
    }

    // L'eroe che attacca ha uno svantaggio contro l'eroe che si difende ?
    public static bool HasElementDisadvantage(ELEMENT attackElement, Hero defender)
    {
        if (attackElement == defender.GetResistance())
        {
            elementalResistnaceHitted = true;
            return true;
        }
        else
            return false;
    }

    public static float EvaluateElementalModifier(ELEMENT attackElement, Hero defender)
    {
        if (HasElementAdvantage(attackElement, defender)) return 1.5f;
        else if
           (HasElementDisadvantage(attackElement, defender)) return 0.5f;
        else
            return 1.0f;
    }

    public static bool HasHit(Stats attacker, Stats defender)
    {
        int hitChance;
        hitChance = (attacker.aim - defender.eva);

        int randomNumber;
        randomNumber = Random.Range(0, 99);

        if (randomNumber > hitChance)
        {
            Debug.Log("MISS");
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool IsCrit(float critValue)
    {
        int randomNumber;
        randomNumber = Random.Range(0, 99);

        Debug.Log("CRIT");

        if (randomNumber < critValue)
        {
            return true;
        }
        else
            return false;
    }

    public static float CalculateDamage(Hero attacker, Hero defender)
    {
        Stats attackerStatsSum;
        Stats defenderStatsSum;

        attackerStatsSum = Stats.Sum(attacker.GetBaseStats(), attacker.GetWeapon().GetBonusStats());
        defenderStatsSum = Stats.Sum(defender.GetBaseStats(), defender.GetWeapon().GetBonusStats());

        int baseDamage;
        float damageMul;      
        float damage = 1.0f; // conterrà il valore del danno da restituire

        if (attacker.GetWeapon().GetDamageType() == Weapon.DAMAGE_TYPE.PHYSICAL)
        { // def    
            baseDamage = attackerStatsSum.atk - defenderStatsSum.def;
            damageMul = EvaluateElementalModifier(attacker.GetWeapon().GetElement(), defender);
            if (elementalWeaknessHitted) Debug.Log("WEAKNESS");
            if (elementalResistnaceHitted) Debug.Log("RESIST");
            damage = baseDamage * damageMul;
        }
        else if (attacker.GetWeapon().GetDamageType() == Weapon.DAMAGE_TYPE.MAGICAL)
        { // res 
            baseDamage = attackerStatsSum.atk - defenderStatsSum.res;
            damageMul = EvaluateElementalModifier(attacker.GetWeapon().GetElement(), defender);
            if (elementalWeaknessHitted) Debug.Log("WEAKNESS");
            if (elementalResistnaceHitted) Debug.Log("RESIST");
            damage = baseDamage * damageMul;
        }

        elementalWeaknessHitted = false;
        elementalResistnaceHitted = false;
 
        if (IsCrit(damage))
        {
            damage = damage * 2;
        }

        if (damage < 0)
        {
            damage = 0;
            return damage;
        }
        else
            return damage;
    }

}