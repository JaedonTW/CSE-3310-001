using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.AI
{
    public class Friendly : NPC
    {
        private const int SanityDeathChange = -10;
        private const int SanitySaveChange = 2;
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
            float[] probability = { 1f, 0.5f, 0.25f };
            // we handle giving the player a weapon.
            // First, we use a random check to see if the weapon is successfuly given.
            for (int i = 0; i < Manager.player.HasWeapon.Length; i++)
                if (!Manager.player.HasWeapon[i] && Random.Range(0f, 1f) < probability[i])
                {
                    Manager.player.HasWeapon[i] = true;
                    return;
                }
        }
        public override void OnDeath()
        {
            AttemptGivePlayerWeapon();
            Manager.player.ChangeSanity(SanityDeathChange);
            base.OnDeath();
        }
        private void OnMouseDown()
        {
            if((Manager.player.body.position - body.position).sqrMagnitude <= MaxInteractionDistanceSqrd)
            {
                AttemptGivePlayerWeapon();
                var despawn = Instantiate(despawnAnimation,transform.parent);
                Manager.player.ChangeSanity(SanitySaveChange);
                despawn.transform.position = transform.position;
                Destroy(gameObject);
            }
        }
    }
}
