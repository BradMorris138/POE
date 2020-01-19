using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



     public abstract class Units
     {
        protected float xpos;
        protected float ypos;
        protected float Health;
        protected float MaxHealth;
        protected float RSpeed; //Round speed
        protected float Attack;
        protected float AckRange;
        protected float Team;
        protected float direction;
        protected string Symbol;
        protected bool attacking;
        protected string name;
        protected bool death;
        protected GameObject gameobject;
        public abstract void Move(float dir);
        public abstract void Conflict(Units conflict);
        public abstract bool ConflictRange(Units other);
        public abstract (Units, float) Nearest(List<Units>units);
        public abstract void Dead();
        public abstract override string ToString();
     }





