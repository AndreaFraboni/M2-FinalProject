using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Burst.Intrinsics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class M1ProjectTest : MonoBehaviour
{
    [SerializeField] Hero a;
    [SerializeField] Hero b;

    float damage;   
    int firstAttacker = 1; // con 1 partirà l'hero A mentre con 2 partirà ad attaccare B - per iniziare lo inizializzo a 1.

    // Start is called before the first frame update
    void Start()
    {
        //-----------------------------------------------------------------------------------------------------------------
        //                         SETTAGGIO MANUALE ma non necessario si può dall'INSPECTOR
        //-----------------------------------------------------------------------------------------------------------------
        //Stats SpadaStats = new Stats(20, 20, 20, 20, 10, 20, 20);
        //Weapon Spada = new Weapon("Spada", Weapon.DAMAGE_TYPE.PHYSICAL, ELEMENT.ICE, SpadaStats);
        //Stats BastoneMagicoStats = new Stats(100, 10, 20, 50, 20, 50, 50);
        //Weapon Bastonemagico = new Weapon("Bastonemagico", Weapon.DAMAGE_TYPE.MAGICAL, ELEMENT.FIRE, BastoneMagicoStats);
        //a.SetName("Mago");
        //a.SetHp(500.0f);
        //Stats astats = new Stats(10, 20, 20, 20, 20, 20, 20);
        //a.SetStats(astats);
        //a.SetResistance(ELEMENT.FIRE);
        //a.SetWeakness(ELEMENT.ICE);
        //a.SetWeapon(Bastonemagico);
        //b.SetName("Orco");
        //b.SetHp(1000.0f);
        //Stats bstats = new Stats(20, 20, 20, 20, 10, 20, 20);
        //b.SetStats(bstats);
        //b.SetResistance(ELEMENT.LIGHTNING);
        //b.SetWeakness(ELEMENT.FIRE);
        //b.SetWeapon(Spada);
        //-----------------------------------------------------------------------------------------------------------------
    }

    // Update is called once per frame
    void Update()
    {
        // ogni frame ......
        if (a.IsAlive() && b.IsAlive()) // se i due eroi sono entrambi vivi possiamo procedere con la battaglia !!
        {
            firstAttacker = CalculateFirstAttacker(a, b); // Determino chi deve partire con il primo attacco se ottengo 1 attaccherà per primo a altrimenti attaccherà per primo b

            if (firstAttacker == 1) // a attaccherà per primo ....
            {
                SimulateFight(a, b);
                if (!b.IsAlive())
                {      
                    // b è morto !!!! Ha vinto a !
                    Debug.Log($"L'eroe di nome {b.GetName()} è morto !!");
                    Debug.Log($"L'eroe di nome {a.GetName()} è il VINCITORE !!");
                    return;
                }
                else
                {
                    // adesso b è ancora vivo e attacca lui .....
                    SimulateFight(b, a);
                    if (!a.IsAlive())
                    {
                        // è morto a !!!! Ha vinto b
                        Debug.Log($"L'eroe di nome {a.GetName()} è morto !!");
                        Debug.Log($"L'eroe di nome {b.GetName()} è il VINCITORE !!");
                        return;
                    }
                }
            }
            else
            {
                // attacca per primo b
                SimulateFight(b, a);
                if (!a.IsAlive())
                {    
                    // a è morto !!!!
                    Debug.Log($"L'eroe di nome {a.GetName()} è morto !!");
                    Debug.Log($"L'eroe di nome {b.GetName()} è il VINCITORE !!");
                    return;
                }
                else
                {
                    // a è ancora vivo e attacca lui .......
                    SimulateFight(a, b);
                    if (!b.IsAlive())
                    {
                        // b è morto !!!!
                        Debug.Log($"L'eroe di nome {b.GetName()} è morto !!");
                        Debug.Log($"L'eroe di nome {a.GetName()} è il VINCIRTORE !!");
                        return;
                    }
                }
            }
        }
        else
        {
            //Debug.Log("La battaglia NON si può più fare xchè uno dei due eroi è DECEDUTO !!!!");
            return;
        }
    }

    // Determino quale dei due eroi parte per primo ad attaccare
    public int CalculateFirstAttacker(Hero a, Hero b)
    {
        Stats aStatsSum;
        Stats bStatsSum;

        aStatsSum = Stats.Sum(a.GetBaseStats(), a.GetWeapon().GetBonusStats());
        bStatsSum = Stats.Sum(b.GetBaseStats(), b.GetWeapon().GetBonusStats());

        int heroSelected = 0;

        if (aStatsSum.spd > bStatsSum.spd)
        {
            heroSelected = 1;
        }
        else if (bStatsSum.spd > aStatsSum.spd)
        {
            heroSelected = 2;
        }
        else
        {
            // tiriamo a sorte ...........
            int randomNumber;
            randomNumber = Random.Range(0, 100); // metto da 0 a 100 anche qua !
            if (randomNumber <= 50)
            {
                heroSelected = 1;
            }
            else if (randomNumber > 50)
            {
                heroSelected = 2;
            }
        }
        return heroSelected;
    }

    // Simula calcolando la battaglia tra due Hero
    void SimulateFight(Hero Attacker, Hero Defender)
    {
        Debug.Log($"L'eroe che ATTACCA si chiama : {Attacker.GetName()}");
        Debug.Log($"L'eroe che DIFENDE si chiama : {Defender.GetName()}");
        damage = GameFormulas.CalculateDamage(Attacker, Defender);
        Debug.Log($"Il valore del danno calcolato provocato da {Attacker.GetName()} verso {Defender.GetName()} è : {damage}");
        Defender.TakeDamage(damage);
    }


}
