using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class M1ProjectTest : MonoBehaviour
{
    [SerializeField] Hero a;
    [SerializeField] Hero b;

    float damage;

    // Start is called before the first frame update
    void Start()
    {
        // Le stats e i parametri degli Hero si impostano da ISPECTOR !!!!


    }

    // Update is called once per frame
    void Update()
    {
        // ogni frame ......

        if (a.IsAlive() && b.IsAlive()) // se i due eroi sono vivi esegui la battaglia !!!!
        {
            if (CalculateFirstAttacker(a, b) == 1) // parte ad attaccare a
            {
                Debug.Log($"L'eroe che ATTACCA si chiama : {a.GetName()}");
                Debug.Log($"L'eroe che DIFENDE si chiama : {b.GetName()}");



                damage = GameFormulas.CalculateDamage(a, b);

                b.TakeDamage(damage);






            }
            else
            { // parte ad attaccare b
                Debug.Log($"L'eroe che ATTACCA si chiama : {b.GetName()}");
                Debug.Log($"L'eroe che DIFENDE si chiama : {a.GetName()}");

            }
        }
        else
        {
            //Debug.Log("La battaglia NON si può fare xchè uno dei due eroi è DECEDUTO !!!!");
            return;
        }
    }

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
            randomNumber = Random.Range(0, 99); 
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


}
