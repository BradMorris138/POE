using System.Collections;
using System.Collections.Generic;
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
    public TextMeshProUGUI txtWizard_Unit;
    public TextMeshProUGUI txtRanged_Unit;
   

    //GameObject[,] Area;
    Vector3 Position;
    public float resourcesMade { get; set; } = 0f;
    public float meleeC { get; set; } = 10f;
    public float rangedC { get; set; } = 10f;
    public float wizardC { get; set; } = 15f;
    //int resources = 0;
    /*int factory = 100;
    float melee = 100;
    int hp = 120;
    int wizard= 120;
    float ranged = 150;
    bool Destroyed = false;
    */
    // Start is called before the first frame update

        public Map()
    {

    }

    void Start()
    {
        Position.y = 1;
        //InitializeArea();
        CreateBuildings(2, 1);
        LayObstructions(20);//Placing and initializing game and game objects
        LayEnemies(10);
        PlaceResources(15);
        PlaceFactory(15);
        LayPlayer();
        
        Display();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Buildings b in building)
        {
            if(b is Resources)
            {
                Resources r = (Resources)b;
                resourcesMade = r.CreateBuildings();
            }
        }
        CreateUnits();//Calling method
        Display();
        for(int i = 0; i < unit.Count;i++)
        {
            if(unit[i] is MeleeUnits)
            {
                MeleeUnits mu = (MeleeUnits)unit[i];
                if(mu.health <= mu.MaxHealth * 0.25)
                {
                    mu.Move(rnd.Next(0, 4));
                }
                else
                {
                    (Units Nearest, float distanceTo) = mu.Nearest(unit);
                    
                    if(distanceTo <= mu.ackrange)
                    {
                        mu.Attacking = true;
                        mu.Conflict(nearest);
                    }
                    else
                    {
                        if(nearest is MeleeUnits)
                        {
                            MeleeUnits nearestMu = (MeleeUnits)nearest;
                            if(mu.gameobject.transform.position.x > nearestMu.gameobject.transform.position.x)//North movement
                            {
                                mu.Move(0);
                            }
                            else if (mu.gameobject.transform.position.x > nearestMu.gameobject.transform.position.x)//South Movement
                            {
                                mu.Move(2);
                            }
                            else if (mu.gameobject.transform.position.z > nearestMu.gameobject.transform.position.z)//West movement
                            {
                                mu.Move(3);
                            }
                            else if (mu.gameobject.transform.position.z > nearestMu.gameobject.transform.position.z)//East movement
                            {
                                mu.Move(1);
                            }
                        }
                    }
                }
            }
        }

      if(!Destroyed)
      {

        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerMoves(Direction.North);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerMoves(Direction.West);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerMoves(Direction.South);
        }
           else if (Input.GetKeyDown(KeyCode.D))
           {
               PlayerMoves(Direction.East);
           }
      }

        txtResources.text = resources.ToString() + "$";//Gives the resources textbox  a value
        txtHP.text = hp.ToString() + "  Resource Buidling HP";//Gives HP textbox a value
        txtFactory.text = factory.ToString() + "  Factory Building HP";//Prints out unit health
        txtMelee_Unit.text = melee.ToString() + "  Melee Unit HP";
        txtWizard_Unit.text = wizard.ToString() + "  Wizard Unit HP";
        txtRanged_Unit.text = ranged.ToString() + "  Ranged Unit HP";

    }

    public void InitializeArea()
    {
        Area = new Tile[areaSize, areaSize]; //This creates open spaces in the area
        for (int x = 0; x < areaSize; x++)
        {
            for (int z = 0; z < areaSize; z++)
            {
                Area[x, z] = Tile.OpenArea;//Assigning tiles to an open area
            }
        }
        //This generates the walls specifically the Northside wall
        for (int x = 0; x < areaSize; x++)
        {
            Area[x, areaSize - 1] = Tile.Obstruction;
        }

        //This generates the walls specifically the Southside wall
        for (int x = 0; x < areaSize; x++)
        {
            Area[x, 0] = Tile.Obstruction;
        }

        //This generates the walls specifically the Eastside wall
        for (int z = 0; z < areaSize; z++)
        {
            Area[areaSize - 1, z] = Tile.Obstruction;
        }

        //This generates the walls specifically the Eastside wall
        for (int z = 0; z < areaSize; z++)
        {
            Area[0, z] = Tile.Obstruction;
        }
    }

    private void Display()
    {
        
        foreach (Units u in unit)
        {
            if (u is MeleeUnits)
            {
                MeleeUnits mu = (MeleeUnits)u;
                Instantiate(mu.GameObject, mu.GameObject.tansform.position, Quaternion.identity);
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
        for (int x = 0; x < areaSize; x++)
        {
            for (int z = 0; z < areaSize; z++)
            {

                switch (Area[x, z])
                {
                    case Tile.OpenArea:
                        Instantiate(OpenArea, new Vector3(x, 1f, z), Quaternion.identity); break;
                    case Tile.Obstruction:      
                        Instantiate(Obstruction, new Vector3(x, 1f, z), Quaternion.identity); break;
                    case Tile.Resources:
                        Instantiate(Resources, new Vector3(x, 1.5f, z), Quaternion.identity);break;
                    case Tile.Melee_Units:
                        Instantiate(Melee_Units, new Vector3(x, 2f, z), Quaternion.identity); break;
                    case Tile.Ranged_Units:
                        Instantiate(Ranged_Units, new Vector3(x, 2f, z), Quaternion.identity); break;
                    case Tile.Wizzards:
                        Instantiate(Wizzard, new Vector3(x, 2f, z), Quaternion.identity); break;
                    case Tile.Factory:
                        Instantiate(Factory, new Vector3(x, 1.5f, z), Quaternion.identity);break;
                   
                    case Tile.Player:
                        GameObject h = Instantiate(Player, new Vector3(x, 2f, z), Quaternion.identity);break;
                    
                   
                }
            }

        }
    }
    public void CreateBuildings(int r, int f)
    {
        for(int i = 0; i < r; i++)
        {
            {
                if(Position.x == rnd.Next(1, 40) || (Position.z == rnd.Next(1,40)))
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
            Resources r1 = new Resources(Resources1, "Resources1" + Convert.ToString(i), 100, 0, 10, 100);//Faction 0
            building.Add(r1);//Calls list buildings

            if(Position.x == rnd.Next(1,40) || (Position.z == rnd.Next(1, 40)))
            {
                Position.x = rnd.Next(1, 40);
                Position.z = rnd.Next(1, 40);
                Resources2.transfomr.position = Position;
            }
            else
            {
                Position.x = rnd.Next(1, 40);
                Position.z = rnd.Next(1, 40);
                Resources2.transform.position = Position;
            }
            Resources r2 = new Resources(Resources2, "Resource 2" + Convert.ToString(i), 100, 1, 10, 1000);
            building.Add(r2);
            
        }
        for(int k = 0; j < f; k++)
        {
            {
                Position.x = rnd.Next(1, 40);
                Position.z = rnd.Next(1, 40);
                Factory1.transform.position = Position;
            }
            Factories f1 = new Factories(Factory1, "Factory 1", Convert.ToString(k), 100, 0);

            {
                
                Position.x = rnd.Next(1, 40);
                Position.z = rnd.Next(1, 40);
                Factory2.transform.position = Position;
                building.Add(f1);
                Factories f2 = new Factories(Factory2, "Factory 2", Convert.ToString(k), 100, 1);

                building.Add(f2);
            }
        }
    }
    private void LayObstructions(int ObsNums)
    {
        for(int i = 0; i < ObsNums; i++)
        {
            int x = Random.Range(1, areaSize - 1);
            int z = Random.Range(1, areaSize - 1);
            if(Area[x, z] == GameObject.OpenArea)
            {
                Area[x, z] = GameObject.Obstruction;
            }
            else
            {
                i--;
            }
        }
    }

    private void PlaceResources(int CashNum)
    {
        for(int i =0; i < CashNum;i++)
        {
            int x = Random.Range(1, areaSize - 1);
            int z = Random.Range(1, areaSize - 1);
            if(Area[x, z] == GameObject.OpenArea)
            {
                Area[x, z] = GameObject.Resources;
            }
            else
            {
                i--;
            }

        }
    }

    private void PlaceFactory(int FactNum)
    {
        for(int i = 0; i < FactNum; i++)
        {
            int x = Random.Range(1, areaSize - 1);
            int z = Random.Range(1, areaSize - 1);
            if(Area[x ,z] == Tile.OpenArea)
            {
                Area[x, z] = Tile.Factory;
            }
            else
            {
                i--;
            }
        }
    }

    private void LayPlayer()
    {
        for(int i = 0; i < 1; i++)
        {
            int x = Random.Range(1, areaSize - 1);
            int z = Random.Range(1, areaSize - 1);
            if(Area[x, z] == Tile.OpenArea)
            {
                Area[x, z] = Tile.Player;
                posX = x;
                posZ = z;
            }
            else
            {
                i--;
            }
        }
    }
    private void LayEnemies(int numbEnemy)
    {
        for (int i = 0; i < numbEnemy; i++)
        {
             int x = Random.Range(1, areaSize - 1);
             int z = Random.Range(1, areaSize - 1);
             if (Area[x, z] == Tile.OpenArea)
             {
                Area[x, z] = (Tile)Random.Range(0, 4);
             }
             else
             {
               i--;
             }

        }
    }

    public void CreateUnits()
    {
        Vector3 po = new Vector3();
        foreach(Building b in buildings)
        {
            if (((((resources - mCost) >= 0) || (resources - rangedCost) >= 0) || (resources - wizCost) >= 0) && (rnd.Next(0, 3) == 0))
            {
                Resources r = (Resources)b;
                if (r.Team == 0)
                {
                    foreach (Building b1 in buildings)
                    {
                        if (b1 is Factory)
                        {
                            Factory f = (Factory)b1;
                            if (b.Team == 0)
                            {
                                pos = fb.GameObject.transform.position;
                                posX.x += rnd.Next(1, 30);
                                posX.z += rnd.Next(1, 30);
                                Ranged1.transform.position = pos;
                                Ranged_Units ru = new Ranged_Units(Ranged1, "Ranged1", 20, 15, 5, 1, 0);
                                ru.Name += ru.count;
                                units.Add(ru);
                            }

                        }
                    }
                }
            }
            else if (r.Team == 1)
            {
                foreach (Building b1 in buildings)
                {
                    if (b1 is Fctory)
                    {
                        Factory f = (Factory)b1;
                        if (f.Team == 0)
                        {
                            pos = f.gameobject.transform.position;
                            Position.x += rnd.Next(1, 40);
                            Position.z += rnd.Next(1, 40);
                            Melee_Units2.Transform.position = pos;
                            MeleeUnits mu = new MeleeUnits("Melee 2", 30, 10, 1, 1, 1, Melee_Units2);
                            mu.Name += mu.count;
                            unit.Add(mu);
                        }
                    }
                }
            }
            else if (((((resources - meleeC) >= 0) || (resources - rangedC) >= 0) || (resources - wizardC) >= 0) && (rnd.Next(0, 3) == 1))
            {
                Resources r = (Resources)b;
                if (r.Team == 0)
                {
                    foreach(Buildings b in building)
                    {
                        if (b is Factory)
                        {
                            Factory f = (Factory)b;
                            if(f.Team == 0)
                            {
                                pos = f.gameobject.transform.position;
                                Position.x += rnd.Next(1, 40);
                                Position.z += rnd.Next(1, 40);
                                Ranged_Units1.Transform.position = pos;
                                Ranged_Units ru = new Ranged_Units(Ranged_Units1, "Ranged 1 ", 40, 15, 5, 1, 1);
                                ru.Name += ru.count;
                                unit.Add(mu);
                            }
                        }
                    }
                }
                else if  (r.Team == 1)
                {
                    foreach (Buildings b in building)
                    {
                        if(b is Factory)
                        {
                            pos = f.gameobject.transform.position;
                            Position.x += rnd.Next(1, 40);
                            Position.z += rnd.Next(1, 40);
                            Ranged_Units2.Transform.position = pos;
                            Ranged_Units ru = new Ranged_Units(Ranged_Units2, "Ranged 2 ", 40, 15, 5, 1, 1);
                            ru.Name += ru.count;
                            unit.Add(mu);
                        }
                    }
                }
            }
            else if (((((Resources - meleeC) >= 0) || (Resources - rangedC) >= 0) || (Resources - wizardC) >= 0) && (rnd.Next(0, 3) == 2))
            {
                Resources r = (Resources)b;
                if (r.Team == 0)
                {
                    foreach (Buildings b in building)
                    {
                        if (b is Factory)
                        {
                            Factory f = (Factory)b;
                            if (f.Team == 0)
                            {
                                pos = f.gameobject.transform.position;
                                Position.x += rnd.Next(1, 40);
                                Position.z += rnd.Next(1, 40);
                                Wizzard1.Transform.position = pos;
                                Wizard_Units wu = new Wizard_Units(Wizzard1, "Wizzard 1 ", 35, 20, 4, 1, 1);
                                wu.Name += wu.count;
                                unit.Add(wu);
                            }
                        }
                    }
                }
                else if (r.Team == 1)
                {
                    foreach (Buildings b in building)
                    {
                        if (b is Factory)
                        {
                            pos = f.gameobject.transform.position;
                            Position.x += rnd.Next(1, 40);
                            Position.z += rnd.Next(1, 40);
                            Wizzard2.Transform.position = pos;
                            Wizard_Units ru = new Wizard_Units(Wizzard2, "Wizzard 2 ", 35, 20, 4, 1, 1);
                            wu.Name += wu.count;
                            unit.Add(wu);
                        }
                    }
                }
            }

        }
    }
    private void PlayerMoves(Direction dir)
    {
        int newX = posX;
        int newZ = posZ;
        switch(dir)
        {
            case Direction.North: newZ = posZ + 1; break;
            case Direction.South: newZ = posZ - 1;break;
            case Direction.East: newX = posX + 1;break;
            case Direction.West: newX = posX - 1;break;
        }

        if(Area[newX,newZ] == Tile.OpenArea)
        {
            Area[posX, posZ] = Tile.OpenArea;
            posX = newX;
            posZ = newZ;
            Area[posX, posZ] = Tile.Player;
            Display();
        }
        else if(Area[newX,newZ] ==  Tile.Resources)
        {
            resources++;
            Area[posX, posZ] = Tile.OpenArea;//Spawns cash on the open map
            posX = newX;
            posZ = newZ;
            Area[posX, posZ] = Tile.Player;//Allowing the hero character to collect the cash
            Display();
        }
        else if(Area[newX,newZ] == GameObject.Obstruction)
        {

        }
        else
        {
            if(Attack(Area[newX,newZ]))
            {
                Area[newX, newZ] = Tile.Resources;
            }
            else
            {
                Area[posX, posZ] = Tile.OpenArea;
                Destroyed = true;
            }
            Display();
        }

    }
   


  
      
}
