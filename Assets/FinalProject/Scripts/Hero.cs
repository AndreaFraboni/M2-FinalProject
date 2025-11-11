using UnityEngine;

[System.Serializable]
public class Hero
{
    // Variabili private
    [SerializeField] private string name;
    [SerializeField] private float hp;      // l'uso di un int è in contrasto con il calcolo del Danno che lavora con EvaluateElementalModifier che restituisce un float 
    [SerializeField] private Stats baseStats;
    [SerializeField] private ELEMENT resistance;
    [SerializeField] private ELEMENT weakness;
    [SerializeField] private Weapon weapon;

    // Costruttore
    public Hero(string name_Hero, float hp_Hero, Stats baseStats_Hero, ELEMENT resistance_Hero, ELEMENT weakness_Hero, Weapon weapon_Hero)
    {
        name = name_Hero;
        hp = hp_Hero;
        baseStats = baseStats_Hero;
        resistance = resistance_Hero;
        weakness = weakness_Hero;
        weapon = weapon_Hero;
    }

    // Getter
    public string GetName() => name;
    public float GetHp() => hp;
    public Stats GetBaseStats() => baseStats;
    public ELEMENT GetResistance() => resistance;
    public ELEMENT GetWeakness() => weakness;
    public Weapon GetWeapon() => weapon;

    // Setter
    public void SetName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            this.name = name;
        }
        else
            Debug.LogWarning("Stai cercando di assegnare un nome non valido. Non puoi non assegnare il nome dall'HERO!);");
    }

    public void SetHp(float hpvalue) // settaggio dell'hp lavorando con float
    {
        if (hpvalue > 0)
        {
            hp = hpvalue;
        }
        else if (hpvalue <= 0)
        {
            hp = 0;
        }
    }

    public void SetStats(Stats baseStats)
    {
        this.baseStats = baseStats;
    }

    public void SetResistance(ELEMENT resistance)
    {
        this.resistance = resistance;
    }

    public void SetWeakness(ELEMENT weakness)
    {
        this.weakness = weakness;
    }

    public void SetWeapon(Weapon weapon)
    {
        if (weapon != null)
            this.weapon = weapon;
        else
            Debug.LogWarning("Weapon is NULL !!!!!");
    }

    // Functions
    public void AddHp(float amount) 
    {
        SetHp(hp + amount);
    }

    public void TakeDamage(float damage) 
    {
        AddHp(-damage);
    }

    public bool IsAlive()
    {
        if (hp > 0)
            return true;
        else
            return false;
    }
}
