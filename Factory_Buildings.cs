using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_Buildings : Buildings
{
    public string Name//Addition of properties
    {
        get { return base.name; }
        set { name = value; }
    }

    public float Health
    {
        get { return base.health; }
        set { health = value; }
    }



    public float MaxHealth//Max Health
    {
        get { return base.maxhealth; }
    }


    public float Team
    {
        get { return base.team; }
    }

    public string Symbol
    {
        get { return base.symbol; }
        set { symbol = value; }
    }



    public bool Death
    {
        get { return base.death; }
        set { base.death = value; }
    }

    public GameObject GameObject
    {
        get { return base.gameobject; }
        set { base.gameobject = value; }
    }
    private string resourceStats;
    public float resourcesRound { get; set; }
    public float resourcesMade { get; set; }
    public float resourcesLeft { get; set; }

    System.Random r = new System.Random();

    public Factory_Buildings(string name, float health, float team, GameObject gameObject)//Declaring constructor
    {
        Name = name;
        Health = health;
        base.team = Team;
        base.maxhealth = health;
        Death = false;
        GameObject = gameObject;
       

    }

   
    public override void Dead()
    {
        Death = true;
        Health = 0;
    }

    public override string ToString()
    {
        string temp = "";
        temp += "Factory Building";
        temp += "(" + GameObject.transform.position.x + "," + GameObject.transform.position.y + "," + GameObject.transform.position.z + ")";
        temp += Health;
        temp += (Death ? ", (Unit is dead)" : ", (Operating)");
        temp += resourceStats;
        return temp;
    }
}
