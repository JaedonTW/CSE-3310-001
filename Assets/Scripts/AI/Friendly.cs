﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.AI
{
    public class Friendly : NPC
    {
        public GameObject despawnAnimation;
        /*
            Below are the floating point values used 
            to store the main character's position, as
            well as the friednly NPC's position. x_Val
            and y_Val represent the displacement between
            the friendly and the main character in each
            direction.
        */


        /*
            instantiation_Distance is the furthest distance
            the main character can be from a friendly before 
            a pop-up box is instantiated.
        */
        [SerializeField] float instantiation_Distance;
        public float probabilityOfGivingGun = 1;
        /// <summary>
        /// The minimal distance to interact with and save this friendly
        /// </summary>
        protected const float MaxInteractionDistanceSqrd = 1.75f*1.75f;
        protected MainCharacter mainCharacter;
        private Instantiate instantiate;

        protected override void Start()
        {
            base.Start();
            // initialize instantiation_Distance
            instantiation_Distance = 1.75f;

            // Find the reference to the instantiate object
            instantiate = FindObjectOfType<Instantiate>();

            // Find the reference to the mainCharacter object
            mainCharacter = FindObjectOfType<MainCharacter>();

            DamageGroup = DamegeGroups.Friendly;
            SetSit();
        }
        private void AttemptGivePlayerWeapon()
        {
            // we handle giving the player a weapon.
            // First, we use a random check to see if the weapon is successfuly given.
            if (probabilityOfGivingGun == 1 || Random.Range(0, 1f) < probabilityOfGivingGun)
                // now that we know that we are given the player our weapon, we make sure the player does not already have it.
                if (!Manager.player.CurrentWeapons.Contains(weapon))
                    // TODO: indicate to the player that they have been given a new weapon here...
                    Manager.player.CurrentWeapons.Add(weapon);
        }
        public override void OnDeath()
        {
            AttemptGivePlayerWeapon();
            base.OnDeath();
        }
        private void OnMouseDown()
        {
            if((Manager.player.body.position - body.position).sqrMagnitude <= MaxInteractionDistanceSqrd)
            {
                AttemptGivePlayerWeapon();
                var despawn = Instantiate(despawnAnimation,transform.parent);
                despawn.transform.position = transform.position;
                Destroy(gameObject);
            }
        }
    }
}
