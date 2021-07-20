using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.AI
{
    public class Friendly : NPC
    {
        /*
            Below are the floating point values used 
            to store the main character's position, as
            well as the friednly NPC's position. x_Val
            and y_Val represent the displacement between
            the friendly and the main character in each
            direction.
        */
        float friendly_XPos;
        float friendly_YPOS;
        float mainCharacter_XPos;
        float mainCharacter_YPos;
        float x_Val;
        float y_Val;

        /*
            instantiation_Distance is the furthest distance
            the main character can be from a friendly before 
            a pop-up box is instantiated.
        */
        [SerializeField] float instantiation_Distance;

        protected MainCharacter mainCharacter;
        private Instantiate instantiate;

        void Start()
        {
            // initialize instantiation_Distance
            instantiation_Distance = 1.75f;

            // Find the reference to the instantiate object
            instantiate = FindObjectOfType<Instantiate>();

            // Find the reference to the mainCharacter object
            mainCharacter = FindObjectOfType<MainCharacter>();

            DamageGroup = DamegeGroups.Friendly;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnMouseDown()
        {
        }


    }
}
