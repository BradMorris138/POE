using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Map : MonoBehaviour
{
    List<Units> unit = new List<Units>();
    List<Buildings> building = new List<Buildings>();
    public enum Direction
    {
        North,
        South,
        East,
        West
    }

    /*public enum Tile
    {
        Player,
        Wizzards,
        Ranged_Units,
        Melee_Units,
        Factory,
        Resources,
        Obstruction,
        OpenArea
    }*/
    public int areaSize;//Making the play area bigger
    public static System.Random rnd = new System.Random();
    public GameObject OpenArea;
    public GameObject Player;
    public GameObject Wizzard1;
    public GameObject Ranged_Units1;
    public GameObject Melee_Units1;
    public GameObject Obstruction;
    public GameObject Resources1;
    public GameObject Factory1;

    public GameObject Wizzard2;
    public GameObject Ranged_Units2;
    public GameObject Melee_Units2;
    public GameObject Wizzard3;
    public GameObject Resources2;
    public GameObject Factory2;

    public TextMeshProUGUI txtResources;//The TextMeshProUGUI is the UI of the game
    public TextMeshProUGUI txtHP;
    public TextMeshProUGUI txtFactory;
    public TextMeshProUGUI txtMelee_Unit;
    public TextMeshProUGUI txtMelee_Unit2;
    public TextMeshProUGUI txtWizard_Unit;
    public TextMeshProUGUI txtWizard_Units2;
    public TextMeshProUGUI txtRanged_Unit1;
    public TextMeshProUGUI txtRanged_Units2;


    //GameObject[,] Area;
    Vector3 Position;
    public float resourcesMade { get; set; } = 0f;
    public float mCost { get; set; } = 10f;
    public float rCost { get; set; } = 10f;
    public float wCost { get; set; } = 20f;
    //int resources = 0;
    /*int factory = 100;
    float melee = 100;
    int hp = 120;
    int wizard= 120;
    float ranged = 150;
    bool Destroyed = false;
    */
    // Start is called before the first frame update



    void Start()
    {
        Position.y = 1;
        //InitializeArea();
        CreateBuildings(2, 1);

        Display();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Buildings b in building)
        {
            if (b is Resource_Buildings)
            {
                Resource_Buildings rb = (Resource_Buildings)b;
                resourcesMade = rb.CreateBuildings();
            }
        }
        CreateUnits();//Calling method
        Display();
        for (int i = 0; i < unit.Count; i++)
        {
            if (unit[i] is MeleeUnits)
            {
                MeleeUnits mu = (MeleeUnits)unit[i];
                if (mu.health <= mu.maxhealth * 0.25)
                {
                    mu.Move(rnd.Next(0, 4));
                }
                else
                {
                    (Units nearest, float distanceTo) = mu.Nearest(unit);

                    if (distanceTo <= mu.ackrange)
                    {
                        mu.Attacking = true;
                        mu.Conflict(nearest);
                    }
                    else
                    {
                        if (nearest is MeleeUnits)
                        {
                            MeleeUnits nearestMu = (MeleeUnits)nearest;
                            if (mu.GameObject.transform.position.x > nearestMu.GameObject.transform.position.x)//North movement
                            {
                                mu.Move(0);
                            }
                            else if (mu.GameObject.transform.position.x > nearestMu.GameObject.transform.position.x)//South Movement
                            {
                                mu.Move(2);
                            }
                            else if (mu.GameObject.transform.position.z > nearestMu.GameObject.transform.position.z)//West movement
                            {
                                mu.Move(3);
                            }
                            else if (mu.GameObject.transform.position.z > nearestMu.GameObject.transform.position.z)//East movement
                            {
                                mu.Move(1);
                            }
                        }
                        else if (nearest is Ranged_Units)
                        {

                            Ranged_Units nearestRu = (Ranged_Units)nearest;
                            if (mu.GameObject.transform.position.x > nearestRu.GameObject.transform.position.x)
                            {
                                mu.Move(0);
                            }
                            else if (mu.GameObject.transform.position.x > nearestRu.GameObject.transform.position.x)//South Movement
                            {
                                mu.Move(2);
                            }
                            else if (mu.GameObject.transform.position.z > nearestRu.GameObject.transform.position.z)//West movement
                            {
                                mu.Move(3);
                            }
                            else if (mu.GameObject.transform.position.z > nearestRu.GameObject.transform.position.z)//East movement
                            {
                                mu.Move(1);
                            }
                        }
                        else if (nearest is Wizard_Units)
                        {

                            Wizard_Units nearestWu = (Wizard_Units)nearest;
                            if (mu.GameObject.transform.position.x > nearestWu.GameObject.transform.position.x)
                            {
                                mu.Move(0);
                            }
                            else if (mu.GameObject.transform.position.x > nearestWu.GameObject.transform.position.x)//South Movement
                            {
                                mu.Move(2);
                            }
                            else if (mu.GameObject.transform.position.z > nearestWu.GameObject.transform.position.z)//West movement
                            {
                                mu.Move(3);
                            }
                            else if (mu.GameObject.transform.position.z > nearestWu.GameObject.transform.position.z)//East movement
                            {
                                mu.Move(1);
                            }
                        }
                    }
                }
            }
            else if (unit[i] is Ranged_Units)
            {
                Ranged_Units ru = (Ranged_Units)unit[i];
                (Units nearest, float distanceTo) = ru.Nearest(unit);


                if (distanceTo <= ru.ackrange)
                {
                    ru.Attacking = true;
                    ru.Conflict(nearest);
                }
                else
                {
                    if (nearest is MeleeUnits)
                    {
                        MeleeUnits nearestMu = (MeleeUnits)nearest;
                        if (ru.GameObject.transform.position.x > nearestMu.GameObject.transform.position.x)
                        {
                            ru.Move(0);
                        }
                        else if (ru.GameObject.transform.position.x < nearestMu.GameObject.transform.position.x)
                        {
                            ru.Move(2);
                        }
                        else if (ru.GameObject.transform.position.z > nearestMu.GameObject.transform.position.z)
                        {
                            ru.Move(3);
                        }
                        else if (ru.GameObject.transform.position.z < nearestMu.GameObject.transform.position.z)
                        {
                            ru.Move(1);
                        }
                    }
                    else if (nearest is Ranged_Units)
                    {
                        Ranged_Units nearestRu = (Ranged_Units)nearest;
                        if (ru.GameObject.transform.position.x > nearestRu.GameObject.transform.position.x)
                        {
                            ru.Move(0);
                        }
                        else if (ru.GameObject.transform.position.x < nearestRu.GameObject.transform.position.x)
                        {
                            ru.Move(2);
                        }
                        else if (ru.GameObject.transform.position.z < nearestRu.GameObject.transform.position.z)
                        {
                            ru.Move(3);
                        }
                        else if (ru.GameObject.transform.position.z < nearestRu.GameObject.transform.position.z)
                        {
                            ru.Move(1);
                        }
                    }
                    else if (nearest is Wizard_Units)
                    {
                        Wizard_Units nearestWu = (Wizard_Units)nearest;
                        if (ru.GameObject.transform.position.x > nearestWu.GameObject.transform.position.x)
                        {
                            ru.Move(0);
                        }
                        else if (ru.GameObject.transform.position.x < nearestWu.GameObject.transform.position.x)
                        {
                            ru.Move(2);
                        }
                        else if (ru.GameObject.transform.position.z > nearestWu.GameObject.transform.position.x)
                        {
                            ru.Move(3);
                        }
                        else if (ru.GameObject.transform.position.z < nearestWu.GameObject.transform.position.z)
                        {
                            ru.Move(1);
                        }
                    }
                }
            }
            else if (unit[i] is Wizard_Units)
            {
                Wizard_Units wu = (Wizard_Units)unit[i];
                if (wu.health <= wu.maxhealth * 0.5)
                {
                    wu.Move(rnd.Next(0, 4));
                }
                else
                {
                    (Units nearest, float distanceTo) = wu.Nearest(unit);

                    if (distanceTo <= wu.ackrange)
                    {
                        wu.Attacking = true;
                        wu.Conflict(nearest);
                    }
                    else
                    {
                        if (nearest is Ranged_Units)
                        {
                            Ranged_Units nearestRu = (Ranged_Units)nearest;
                            if (wu.GameObject.transform.position.x > nearestRu.GameObject.transform.position.x)
                            {
                                wu.Move(0);
                            }
                            else if (wu.GameObject.transform.position.x < nearestRu.GameObject.transform.position.x)
                            {
                                wu.Move(2);
                            }
                            else if (wu.GameObject.transform.position.z > nearestRu.GameObject.transform.position.z)
                            {
                                wu.Move(3);
                            }
                            else if (wu.GameObject.transform.position.z < nearestRu.GameObject.transform.position.z)
                            {
                                wu.Move(1);
                            }
                        }
                        else if (nearest is Wizard_Units)
                        {
                            Wizard_Units nearestWu = (Wizard_Units)nearest;
                            if (wu.GameObject.transform.position.x > nearestWu.GameObject.transform.position.x)
                            {
                                wu.Move(0);
                            }
                            else if (wu.GameObject.transform.position.x < nearestWu.GameObject.transform.position.x)
                            {
                                wu.Move(2);
                            }
                            else if (wu.GameObject.transform.position.z > nearestWu.GameObject.transform.position.z)
                            {
                                wu.Move(3);
                            }
                            else if (wu.GameObject.transform.position.z < nearestWu.GameObject.transform.position.z)
                            {
                                wu.Move(1);
                            }
                        }
                    }
                }
            }

        }
        Display(); //Calling display method


        foreach (Units u in unit)
        {
            if (u is MeleeUnits)
            {

                MeleeUnits mu = (MeleeUnits)u;
                if (mu.team == 0)
                {
                    txtMelee_Unit.text = "Melee 1" + Convert.ToString(mu.health) + "Health";
                }
                if (mu.team == 1)
                {
                    txtMelee_Unit.text = "Melee 2" + Convert.ToString(mu.health) + "Health";
                }
                mu.Move(rnd.Next(0, 4));
                if (mu.health <= 0)
                {
                    unit.Remove(u);
                }
            }
            else if (u is Ranged_Units)
            {
                Ranged_Units ru = (Ranged_Units)u;
                if (ru.team == 0)
                {
                    txtRanged_Unit1.text = "Ranged1" + Convert.ToString(ru.health) + "Health";
                }
                if (ru.team == 1)
                {
                    txtRanged_Units2.text = "Ranged2" + Convert.ToString(ru.health) + "Health";
                }
                ru.Move(rnd.Next(0, 4));
                if (ru.health <= 0)
                {
                    unit.Remove(u);
                }
            }
            else if (u is Wizard_Units)
            {
                Wizard_Units wu = (Wizard_Units)u;
                if (wu.team == 0)
                {
                    txtWizard_Unit.text = "Wizard 1" + Convert.ToString(wu.health) + "Health";
                }
                if (wu.team == 1)
                {
                    txtWizard_Units2.text = "Wizard 2" + Convert.ToString(wu.health) + "Health";
                }
                wu.Move(rnd.Next(0, 4));
                if (wu.health <= 0)
                {
                    unit.Remove(u);
                }
            }
        }
    }
    private void ShowBuildings()
    {
        foreach(Buildings b in building)
        {
            if(b is Resource_Buildings)
            {
                Resource_Buildings r = (Resource_Buildings)b;
                Instantiate(r.GameObject, r.GameObject.transform.position, Quaternion.identity);
            }
            else if (b is Factory_Buildings)
            {
                Factory_Buildings fb = (Factory_Buildings)b;
                Instantiate(fb.GameObject, fb.GameObject.transform.position, Quaternion.identity);
            }
        }

        Vector3 temp = new Vector3(0f, 1f, 0f);
        for(int i = 0; i <= 60; i++)
        {
            {
                temp.x = i;
                Instantiate(Obstruction, temp, Quaternion.identity);
            }
        }
        for (int k = 0; k <= 60; k++)
        {
            {
                temp.z = k;
                Instantiate(Obstruction, temp, Quaternion.identity);
            }
        }
        for (int l = 0; l <= 60; l--)
        {
            {
                temp.x = l;
                Instantiate(Obstruction, temp, Quaternion.identity);
            }
        }
        for (int p = 0; p <= 60; p--)
        {
            {
                temp.x = p;
                Instantiate(Obstruction, temp, Quaternion.identity);
            }
        }
    }




    private void Display()
    {

        foreach (Units u in unit)
        {
            if (u is MeleeUnits)
            {
                MeleeUnits mu = (MeleeUnits)u;
                Instantiate(mu.GameObject, mu.GameObject.Tansform.position, Quaternion.identity);
            }
            else if (u is Ranged_Units)
            {
                Ranged_Units ru = (Ranged_Units)u;
                Instantiate(ru.GameObject, ru.GameObject.transform.position, Quaternion.identity);
            }
            else if (u is Wizard_Units)
            {
                Wizard_Units wu = (Wizard_Units)u;
                Instantiate(wu.GameObject, wu.GameObject.transform.position, Quaternion.identity);
            }
        }
       
    }
    public void CreateBuildings(int r, int fb)
    {
        for (int i = 0; i < r; i++)
        {
            {
                if (Position.x == rnd.Next(1, 40) || (Position.z == rnd.Next(1, 40)))
                {
                    Position.x = rnd.Next(1, 40);
                    Position.z = rnd.Next(1, 40);
                    Resources1.transform.position = Position;
                }
                else
                {
                    Position.x = rnd.Next(1, 40);
                    Position.z = rnd.Next(1, 40);
                    Resources1.transform.position = Position;
                }
            }
            Resource_Buildings r1 = new Resource_Buildings(Resources1, "Resources1" + Convert.ToString(i), 100, 0, 10, 100);
            building.Add(r1);//Calls list buildings

            if (Position.x == rnd.Next(1, 40) || (Position.z == rnd.Next(1, 40)))
            {
                Position.x = rnd.Next(1, 40);
                Position.z = rnd.Next(1, 40);
                Resources2.transfom.position = Position;
            }
            else
            {
                Position.x = rnd.Next(1, 40);
                Position.z = rnd.Next(1, 40);
                Resources2.transform.position = Position;
            }
            Resource_Buildings r2 = new Resources(Resources2, "Resource 2" + Convert.ToString(i), 100, 1, 10, 1000);
            building.Add(r2);

        }
        for (int k = 0; k < fb; k++)
        {
            {
                Position.x = rnd.Next(1, 40);
                Position.z = rnd.Next(1, 40);
                Factory1.transform.position = Position;
            }
            Factory_Buildings f1 = new Factory_Buildings(Factory1, "Factory 1", Convert.ToString(k), 100, 0);

            {

                Position.x = rnd.Next(1, 40);
                Position.z = rnd.Next(1, 40);
                Factory2.transform.position = Position;
            }
            building.Add(f1);
            Factory_Buildings f2 = new Factory_Buildings(Factory2, "Factory 2", Convert.ToString(k), 100, 1);

            building.Add(f2);
        }
    }
   
   

   

    public void CreateUnits()
    {
        Vector3 pos = new Vector3();
        foreach (Buildings b in building)
        {
            if (((((resourcesMade - mCost) >= 0) || (resourcesMade - rCost) >= 0) || (resourcesMade - wCost) >= 0) && (rnd.Next(0, 3) == 0))
            {
                Resource_Buildings r = (Resource_Buildings)b;
                if (r.Team == 0)
                {
                    foreach (Buildings b1 in building)
                    {
                        if (b1 is Factory_Buildings)
                        {
                            Factory_Buildings fb = (Factory_Buildings)b1;
                            if (fb.Team == 0)
                            {
                                Position = fb.GameObject.transform.position;
                                Position.x += rnd.Next(1, 30);
                                Position.z += rnd.Next(1, 30);
                                Melee_Units1.transform.position = pos;
                                MeleeUnits mu = new MeleeUnits(Melee_Units1, "Ranged1", 20, 15, 5, 1, 0);
                                mu.Name += mu.count;
                                unit.Add(mu);
                            }

                        }
                    }
                }
            }
            else if (r.Team == 1)
            {
                foreach (Buildings b1 in building)
                {
                    if (b1 is Factory_Buildings)
                    {
                        Factory_Buildings fb = (Factory_Buildings)b1;
                        if (fb.Team == 0)
                        {
                            Position = fb.GameObject.transform.position;
                            Position.x += rnd.Next(1, 40);
                            Position.z += rnd.Next(1, 40);
                            Melee_Units2.transform.position = pos;
                            MeleeUnits mu = new MeleeUnits("Melee 2", 30, 10, 1, 1, 1, Melee_Units2);
                            mu.Name += mu.count;
                            unit.Add(mu);
                        }
                    }
                }
            }
            else if (((((resourcesMade - mCost) >= 0) || (resourcesMade - rCost) >= 0) || (resourcesMade - wCost) >= 0) && (rnd.Next(0, 3) == 1))
            {
                Resource_Buildings r = (Resource_Buildings)b;
                if (r.Team == 0)
                {
                    foreach (Buildings b1 in building)
                    {
                        if (b1 is Factory_Buildings)
                        {
                            Factory_Buildings fb = (Factory_Buildings)b1;
                            if (fb.Team == 0)
                            {
                                pos = fb.GameObject.transform.position;
                                Position.x += rnd.Next(1, 40);
                                Position.z += rnd.Next(1, 40);
                                Ranged_Units1.transform.position = pos;
                                Ranged_Units ru = new Ranged_Units(Ranged_Units1, "Ranged 1 ", 40, 15, 5, 1, 1);
                                ru.Name += ru.count;
                                unit.Add(ru);
                            }
                        }
                    }
                }
                else if (r.Team == 1)
                {
                    foreach (Buildings b1 in building)
                    {
                        if (b is Factory_Buildings)
                        {
                            Factory_Buildings f = (Factory_Buildings)b;
                            if(f.Team == 0)
                            {
                                 pos = f.GameObject.transform.position;
                                 Position.x += rnd.Next(1, 40);
                                 Position.z += rnd.Next(1, 40);
                                 Ranged_Units2.transform.position = pos;
                                 Ranged_Units ru = new Ranged_Units(Ranged_Units2, "Ranged 2 ", 40, 15, 5, 1, 1);
                                 ru.Name += ru.count;
                                 unit.Add(ru);

                            }
                        }
                    }
                }
            }
            else if (((((resourcesMade - mCost) >= 0) || (resourcesMade - rCost) >= 0) || (resourcesMade - wCost) >= 0) && (rnd.Next(0, 3) == 2))
            {
                Resource_Buildings r = (Resource_Buildings)b;
                if (r.Team == 0)
                {
                    foreach (Buildings b1 in building)
                    {
                        if (b is Factory_Buildings)
                        {
                            Factory_Buildings f = (Factory_Buildings)b;
                            if (f.Team == 0)
                            {
                                pos = f.GameObject.transform.position;
                                Position.x += rnd.Next(1, 30);
                                Position.z += rnd.Next(1, 40);
                                Wizzard1.transform.position = pos;
                                Wizard_Units wu = new Wizard_Units(Wizzard1, "Wizzard 1 ", 35, 20, 4, 1, 1);
                                wu.Name += wu.count;
                                unit.Add(wu);
                            }
                        }
                    }
                }
                else if (r.Team == 1)
                {
                    foreach (Buildings b1 in building)
                    {
                        
                        if (b1 is Factory_Buildings)
                        {
                            Factory_Buildings fb = (Factory_Buildings)b;
                            if (fb.Team == 0)
                            {
                            pos = fb.GameObject.transform.position;
                            Position.x += rnd.Next(1, 30);
                            Position.z += rnd.Next(1, 30);
                            Wizzard2.transform.position = pos;
                            Wizard_Units wu = new Wizard_Units(Wizzard2, "Wizzard 2 ", 35, 20, 4, 1, 1);
                            wu.Name += wu.count;
                            unit.Add(wu);

                            }
                        }
                    }
                }
            }

        }
    }
}
   
   


  
      

