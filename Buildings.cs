using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public abstract class Buildings : MonoBehaviour
{
    
    
        // Update is called once per frame
        public abstract void Update();
        public abstract void Start();// Start is called before the first frame update
        public string Armory, Medic_Center, Bullet_Factory, Engine_Factory, Medical_Vehicle, Tank_Factory, Chemical_Factory;
        protected int xPos, yPos;
        protected float health;
        protected float maxhealth;
        protected float team;
        protected GameObject gameobject;
        protected bool death;
        protected string symbol;


        public abstract void Destroy();
       

        public abstract override string ToString();

}


