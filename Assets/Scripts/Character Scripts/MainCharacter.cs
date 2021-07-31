using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// The class that corrosponds to the main character object.
/// </summary>
public class MainCharacter : MovableCharacter
{
    // PLAYER CONFIGURATION VARIABLES

    const float MeleeRange = 0.5f;
    const int MeleeDamage = 1;

    // END PLAYER CONFIGURATION VARIABLES
    public Weapon[] Weapons;
    public bool[] HasWeapon { get; } = new bool[3];
    /// <summary>
    /// mainCharacter will have sanity which depends
    /// on if friendlies are saved/killed.
    /// </summary>
    [SerializeField] protected int insanity;

    /// <summary>
    /// Getter function for insanity.
    /// </summary>
    public int GetInsanity() => insanity;
    /// <summary>
    /// Change function for insanity.
    /// </summary>
    public void ChangeSanity(int amount)
    {
        insanity += amount;
        if (insanity > 100)
            insanity = 100;
        else if (insanity < 0)
            insanity = 0;

    }

    /// <summary>
    /// Joystick to be used to control the movement of the main character.
    /// </summary>
    protected Joystick MovementJoystick { get; set; }
    protected Joystick CombatJoystick { get; set; }
    /// <summary>
    /// Transformation for the main camera.
    /// </summary>
    protected Camera cam;
    public void OnLevelEnd()
    {
        PlayerPrefs.SetInt("Insanity", GetInsanity());
        for(int i = 0; i < HasWeapon.Length; i++)
            PlayerPrefs.SetInt("Weapon " + i + " is unlocked", HasWeapon[i]? 1 : 0);
    }
    public override void ChangeHealth(int change)
    {
        base.ChangeHealth(change);
        print("Player health is now " + health);
    }
    protected override void Start()
    {
        print("MainCharacter started");
        // Loading data and setting initial values
        insanity = PlayerPrefs.GetInt("Insanity",0);
        for (int i = 0; i < HasWeapon.Length; i++)
            HasWeapon[i] = PlayerPrefs.GetInt("Weapon " + i + " is unlocked",0) == 1;
        health = 100;
        //
        base.Start();
        DamageGroup = DamegeGroups.Player;

        // Getting the joystick and camera objects
        MovementJoystick = FindObjectOfType<Joystick>();
        var joysticks = FindObjectsOfType<Joystick>();
        if (joysticks.Length < 2)
            throw new MissingComponentException("Could not find both 'Joystick' objects.");
        print(joysticks[0].name);
        print(joysticks[1].name);
        if (joysticks[0].name == "Movement Joystick")
        {
            MovementJoystick = joysticks[0];
            CombatJoystick = joysticks[1];
        }
        else
        {
            MovementJoystick = joysticks[1];
            CombatJoystick = joysticks[0];
        }
        MovementJoystick.SnapX = true;
        MovementJoystick.SnapY = true;
        cam = FindObjectOfType<Camera>();
        // setting the camera to be focused on the MainCharacter (player)
        cam.transform.position = new Vector3(body.position.x, body.position.y, cam.transform.position.z);
    }
    protected override void Update()
    {
        // updating player movement.
        Vector2 traveling = new Vector2(MovementJoystick.Horizontal, MovementJoystick.Vertical);
        if (traveling.x != 0 || traveling.y != 0)
            WalkInDirection(traveling);
        else SetIdle();
        // updating combat
        Vector2 attacking = new Vector2(CombatJoystick.Horizontal, CombatJoystick.Vertical);
        if(attacking.x != 0 || attacking.y != 0)
        {
            // we know that an attack direction was specified, so we just need to figure out if it was a melee or ranged attack.
            // So, we check if there is an enemy in melee range.
            var hits = Physics2D.RaycastAll(body.position, attacking, MeleeRange);
            bool rangeAttack = true;
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.tag == "View Blocker")
                    break;
                if (hit.collider.gameObject.tag != "Player")
                {
                    rangeAttack = false;
                    // we have found something we can attack!
                    var hurtable = hit.collider.gameObject.GetComponent<Hurtable>();
                    // making sure it can actually be damaged
                    if (hurtable != null)
                        hurtable.ChangeHealth(-MeleeDamage);
                    break;
                }
            }
            if (rangeAttack && weapon != null)
                weapon.AttemptUse(Mathf.Atan2(attacking.y,attacking.x));
        }
        // calling update for parent object.
        // this is done after getting user input to improve response time.
        base.Update();
        // moving the camera to keep up with the MainCharacter (player).
        cam.transform.position = new Vector3(body.position.x, body.position.y, cam.transform.position.z);

        // dealing with user input
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Mouse0 has been pressed.
            if(weapon != null && !EventSystem.current.IsPointerOverGameObject())
            {
                // Mouse is not over a game object such as the joystick.
                var pos = Input.mousePosition;
                var playerScreenPos = cam.WorldToScreenPoint(body.transform.position);
                var angle = Mathf.Atan2(pos.y - playerScreenPos.y, pos.x - playerScreenPos.x);
                weapon.AttemptUse(angle);
            }
        }
        else
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                // checking if is over game object
                if (!EventSystem.current.IsPointerOverGameObject(t.fingerId))
                {
                    print("Is not in a bad place!");
                    break;
                }
                else
                    print("Is in bad place...");
            }
    }
}
