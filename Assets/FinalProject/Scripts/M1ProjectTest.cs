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

        // Le stats e i parametri delle armi weapon e degli Hero si possono comunque impostare e modificare da INSPECTOR !!!!

        Stats SpadaStats = new Stats (10, 20, 10, 10, 20, 20, 20);
        Weapon Spada = new Weapon("Spada", Weapon.DAMAGE_TYPE.PHYSICAL, ELEMENT.ICE, SpadaStats);

        Stats BastoneMagicoStats = new Stats (50, 20, 10, 50, 50, 50, 20);
        Weapon Bastonemagico = new Weapon("Bastonemagico", Weapon.DAMAGE_TYPE.MAGICAL, ELEMENT.FIRE, BastoneMagicoStats);

        a.SetName("Gandalf");
        a.SetHp(100.0f);
        Stats astats = new Stats(20, 20, 20, 20, 20, 20, 10);
        a.SetStats(astats);
        a.SetResistance(ELEMENT.FIRE);
        a.SetWeakness(ELEMENT.ICE);
        a.SetWeapon(Bastonemagico);

        b.SetName("Orco");
        b.SetHp(100.0f);
        Stats bstats = new Stats(20, 20, 20, 20, 20, 20, 10);
        b.SetStats(bstats);
        b.SetResistance(ELEMENT.ICE);
        b.SetWeakness(ELEMENT.FIRE);
        b.SetWeapon(Spada);

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


                damage = GameFormulas.CalculateDamage(b, a);

                a.TakeDamage(damage);


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
