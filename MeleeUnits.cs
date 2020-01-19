using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class MeleeUnits : Units
{
    public float xPos//Adding in of properties
    {
        get { return base.xpos; }
        set { xpos = value; }
    }



    public float yPos
    {
        get { return base.ypos; }
        set { ypos = value; }
    }

    public string Name
    {
        get { return base.name; }
        set { name = value;  }
    }

    public float health
    {
        get { return base.Health; }
        set { Health = value; }
    }



    public float maxhealth//Max Health
    {
        get { return base.MaxHealth; }
    }

    public float rspeed
    {
        get { return base.RSpeed; }
        set { RSpeed = value; }
    }



    public float attack
    {
        get { return base.Attack; }
        set { Attack = value; }
    }

    public float ackrange
    {
        get { return base.AckRange; }
        set { AckRange = value; }
    }



    public float team
    {
        get { return base.Team; }
    }

    public string symbol
    {
        get { return base.Symbol; }
        set { Symbol = value; }
    }

    public bool Attacking
    {
        get { return base.attacking; }
        set { attacking = value; }
    }

    public float Directions
    {
        get { return base.direction; }
        set { base.direction = value; }
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

    public int count = 0;
    public MeleeUnits(string name, float xpos, float ypos, float health, float rspeed, float attack, float ackrange, float team, string symbol, bool atck, float direction, GameObject gameobject)//Declaring constructor
    {
        Name = name;
        xPos = xpos;
        yPos = ypos;
        Health = health;
        base.MaxHealth = health;
        RSpeed = rspeed;
        Attack = attack;
        AckRange = 1f;
        base.Team = team;
        Symbol = symbol;
        atck = false;
        Death = false;
        Directions = direction;
        GameObject = gameobject;
        count++;

    }
     
    public override void Conflict(Units conflict)
    {
        if (conflict is MeleeUnits)
        {
            health = health - ((MeleeUnits)conflict).attack;
        }
        else if (conflict is Ranged_Units)
        {
            Ranged_Units ru = (Ranged_Units)conflict;
            health = health - (ru.attack - ru.ackrange);
        }
        else if (conflict is Wizard_Unit) conflict;
        {
            Wizard_Unit wu = (Wizard_Unit)conflict;
            health = health - (wu.attack - wu.ConflictRange);
        }
        if(health <= 0)//if health of unit = 0, the unit will die
        {
            Dead();
        }
    }

    public override bool ConflictRange(Units other)//If units are in range they begin conflict
    {
        float distance;
        GameObject otherObjects = new GameObject();
        if(other is MeleeUnits)
        {
            otherObjects = ((MeleeUnits)other).GameObject;
        }
        else if (other is Ranged_Units)
        {
            otherObjects = ((Ranged_Units)other).GameObject;
        }
        else if (other is Wizard_Unit)
        {
            otherObjects = ((Wizard_Unit)other).GameObject;
        }

        distance = Math.Abs(GameObject.transform.position.x - otherObjects.transform.position.x) + Math.Abs(GameObject.transform.position.z - otherObjects.transform.position.z);

        if(distance <= ackrange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public override/*Overrides Reset in unit class*/ (Units, float) Nearest(List<Units> units)//Calculating where the players will return to Accessing actual damage in  unit class
    {
        float nearest = 100f;//Battle Area
        Units closest = this;
        //Closest Unit and Distance                    
        foreach (Units u in units)
        {
            if (u is MeleeUnits && u != this)
            {
                MeleeUnits otherMu = (MeleeUnits)u;
                float distance = Math.Abs(this.GameObject.transform.position.xPos - otherMu.GameObject.transform.positiono.xPos) +
                                 Math.Abs(this.GameObject.transform.position.zPos - otherMu.GameObject.transform.positiono.zPos);
                          
                if (distance < nearest)
                {
                    nearest = distance;
                    closest = otherMu;
                }
            }
            else if (u is Ranged_Units && u !=this)
            {
                Ranged_Units otherRu = (Ranged_Units)u;
                float distance = Math.Abs(this.GameObject.transform.position.xPos - otherMu.GameObject.transform.position.xPos) +
                                 Math.Abs(this.GameObject.transform.position.zPos - otherMu.GameObject.transform.position.zPos);
                if (distance < nearest)
                {
                    nearest = distance;
                    closest = otherRu;
                }
            }
            else if (u is Wizard_Unit && u != this)
            {
                Wizard_Unit otherWu = (Wizard_Unit)u;
                float distance = Math.Abs(this.GameObject.transform.position.xPos - otherMu.GameObject.transform.position.xPos) +
                                 Math.Abs(this.GameObject.transform.position.zPos - otherMu.GameObject.transform.position.zPos);
                if (distance < nearest)
                {
                    nearest = distance;
                    closest = otherWu;
                }
            }
        }
        return (closest, nearest);
    }
    public override void Dead()
    {
        Death = true;
        health = 0f;
        Attacking = false;
    }
    public override void Move(float dire)
    {
        Vector3 temp = new Vector3();
        switch (dir)
        {
            case 0:
                {
                    temp = GameObject.transform.position + new Vector3(0f, 0f, 10f);
                    GameObject.transform.position = temp;
                    GameObject.transform.Translate(Vector3.up * Speed);
                    break;//Northern direction
                }
            case 1:
                {
                    temp = GameObject.transform.position + new Vector3(10f, 0f, 10f);
                    GameObject.transform.position = temp;
                    GameObject.transform.Translate(Vector3.right * Speed);
                    break;//Eastern direction
                }
            case 2:
                {
                    temp = GameObject.transform.position + new Vector3(f, 0f, 10f);
                    GameObject.transform.position = temp;
                    GameObject.transform.Translate(Vector3.down * Speed);
                    break;//Southern direction
                }
            case 3:
                {
                    temp = GameObject.transform.position + new Vector3(10f, 0f, 10f);
                    GameObject.transform.position = temp;
                    GameObject.transform.Translate(Vector3.left * Speed);
                    break;//Western direction
                }
            default: break;
        }

    }
    public override string ToString()
    {
        string temp = "";
        temp += "Melee:";
        temp += "(" + GameObject.transform.position.x + "," + GameObject.transform.position.y + "," + GameObject.transform.position.z + ")";
        temp += health + " ," + Attack + " ," + AckRange + " ," + RSpeed;
        temp += (Death ? "Dead!" : "Alive");
        return temp;
    }
}
