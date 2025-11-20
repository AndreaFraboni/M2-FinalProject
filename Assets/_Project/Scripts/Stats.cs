//using UnityEngine;

[System.Serializable]
public struct Stats
{
    public int atk;
    public int def;
    public int res;
    public int spd;
    public int crt;
    public int aim;
    public int eva;

    // Costruttore
    public Stats(int atkval, int defval, int resval, int spdval, int crtval, int aimval, int evaval)
    {
        atk = atkval;
        def = defval;
        res = resval;
        spd = spdval;
        crt = crtval;
        aim = aimval;
        eva = evaval;
    }

    // Funzione statica somma tra due Stats
    public static Stats Sum(Stats statA, Stats statB)
    {
        Stats StatsSum;
        StatsSum.atk = statA.atk + statB.atk;
        StatsSum.def = statA.def + statB.def;
        StatsSum.res = statA.res + statB.res;
        StatsSum.spd = statA.spd + statB.spd;
        StatsSum.crt = statA.crt + statB.crt;
        StatsSum.aim = statA.aim + statB.aim;
        StatsSum.eva = statA.eva + statB.eva;
        return StatsSum;
    }
}

