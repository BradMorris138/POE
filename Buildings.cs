using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public abstract class Buildings
{
    
    
        // Update is called once per frame
        protected float health;
        protected float maxhealth;
        protected float team;
        protected GameObject gameobject;
        protected string name;
        protected bool death;
        protected string symbol;
        public abstract void Dead();
        public abstract override string ToString();


       


}


