using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Map : MonoBehaviour
{
    public enum Direction
    {
        North,
        South,
        East,
        West
    }

    public enum Tile
    {
        Player,
        Wizzards,
        Ranged_Units,
        Melee_Units,
        Factories,
        Resources,
        Obstruction,
        OpenArea
    }
    public int areaSize;//Making the play area bigger
    public GameObject OpenArea;
    public GameObject Player;
    public GameObject Obstruction;
    public GameObject Resources;
    public GameObject[] enemies;//[]Indicating numerous enemies
    public TextMeshProUGUI txtResources;//The TextMeshProUGUI is the UI of the game
    public TextMeshProUGUI txtHP;
    public Camera camera;
    Tile[,] Area;
    int posX;
    int posZ;
    int resources = 0;
    int HP = 100;
    bool Destroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        InitializeArea();
        LayObstructions(20);//Placing and initializing game and game objects
        LayEnemies(10);
        PlacePoints(15);
        LayPlayer();
        Display();
    }

    // Update is called once per frame
    void Update()
    {
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
        txtHP.text = HP + "HP";//Gives HP textbox a value

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
        GameObject[] Tiles = GameObject.FindGameObjectsWithTag("tile");
        foreach (GameObject g in Tiles)
        {
            Destroy(g);
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
                        Instantiate(Obstruction, new Vector3(x, 2f, z), Quaternion.identity); break;
                    case Tile.Resources:
                        Instantiate(Resources, new Vector3(x, 1.5f, z), Quaternion.identity);break;
                    case Tile.Player:
                        GameObject h = Instantiate(Player, new Vector3(x, 2f, z), Quaternion.identity);break;
                    case Tile.Melee_Units:
                        Instantiate(enemies[0], new Vector3(x, 1f, z), Quaternion.identity);break;
                    case Tile.Ranged_Units:
                        Instantiate(enemies[1], new Vector3(x, 2f, z),Quaternion.identity);break;
                    case Tile.Wizzards:
                       
                        Instantiate(enemies[3], new Vector3(x, 1f, z),Quaternion.identity);break;
                    case Tile.Factories:
                        Instantiate(enemies[5], new Vector3(x, 2f, z), Quaternion.identity);break;
                   
                }
            }
        }
    }
    private void LayObstructions(int ObsNums)
    {
        for(int i = 0; i < ObsNums; i++)
        {
            int x = Random.Range(1, areaSize - 1);
            int z = Random.Range(1, areaSize - 1);
            if(Area[x, z] == Tile.OpenArea)
            {
                Area[x, z] = Tile.Obstruction;
            }
            else
            {
                i--;
            }
        }
    }

    private void PlacePoints(int CashNum)
    {
        for(int i =0; i < CashNum;i++)
        {
            int x = Random.Range(1, areaSize - 1);
            int z = Random.Range(1, areaSize - 1);
            if(Area[x, z] == Tile.OpenArea)
            {
                Area[x, z] = Tile.Resources;
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
        else if(Area[newX,newZ] == Tile.Obstruction)
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
    public bool Attack(Tile ti)
    {
        int dmg = 0;
        switch(ti)
        {
            case Tile.Melee_Units: dmg = 10; break;
            case Tile.Ranged_Units: dmg = 15; break;
            case Tile.Wizzards: dmg = 16; break;
        }
        if(HP - dmg > 0)
        {
            HP -= dmg;
            return true;
        }
        else
        {
            HP = 0;
            return false;
        }
    }
      
}
