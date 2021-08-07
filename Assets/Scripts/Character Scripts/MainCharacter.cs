using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// The class that corresponds to the main character object.
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
    public int Sanity { get; private set; }
    public GameManager Manager { get; set; }
    public void SetActiveWeapon(int ID)
    {
        if (0 <= ID && ID < HasWeapon.Length && HasWeapon[ID])
        {
            weapon = Weapons[ID];
            weapon.body = body;
        }
    }
    /// <summary>
    /// Change function for sanity.
    /// </summary>
    public void ChangeSanity(int amount)
    {
        Sanity += amount;
        if (Sanity > 100)
            Sanity = 100;
        else if (Sanity < 50)
        {
            if (Sanity <= 0)
            {
                HUD.SetSanity(Sanity);
                OnDeath();
                return;
            }
            else
                InkSpawner.Start(this);
        }
        HUD.SetSanity(Sanity);
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
        PlayerPrefs.SetInt("Sanity", Sanity);
        for(int i = 0; i < HasWeapon.Length; i++)
            PlayerPrefs.SetInt("Weapon " + i + " is unlocked", HasWeapon[i]? 1 : 0);
        if(weapon != null)
            PlayerPrefs.SetInt("Equiped Weapon", weapon.ID);
    }
    public override void ChangeHealth(int change)
    {
        base.ChangeHealth(change);
        HUD.SetHealth(Health);
        print("Player health is now " + Health);
    }
    public override void OnDeath()
    {
        Spawner.Spawners.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        base.OnDeath();
    }
    protected override void Start()
    {
        base.Start();
        // Loading data and setting initial values
        Sanity = PlayerPrefs.GetInt("Sanity",75);
        HUD.SetSanity(Sanity);
        for (int i = 0; i < HasWeapon.Length; i++)
            HasWeapon[i] = PlayerPrefs.GetInt("Weapon " + i + " is unlocked",0) == 1;
        int currentWeapon = PlayerPrefs.GetInt("Equiped Weapon", 0);
        SetActiveWeapon(currentWeapon);
        //
        DamageGroup = DamegeGroups.Player;

        // Getting the joystick and camera objects
        MovementJoystick = FindObjectOfType<Joystick>();
        var joysticks = FindObjectsOfType<Joystick>();
        if (joysticks.Length < 2)
            throw new MissingComponentException("Could not find both 'Joystick' objects.");
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
        Vector2 traveling, attacking;
        const bool debugMode = true;
        traveling = new Vector2(MovementJoystick.Horizontal, MovementJoystick.Vertical);
        attacking = new Vector2(CombatJoystick.Horizontal, CombatJoystick.Vertical);
        if (debugMode)
        {
            // NOTE: we do not handle trying to go contrasting directions at the same time very well (e.g. left and right)
            traveling = new Vector2(Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0,
                Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0).normalized;
            attacking = new Vector2(Input.GetKey(KeyCode.RightArrow) ? 1 : Input.GetKey(KeyCode.LeftArrow) ? -1 : 0,
                Input.GetKey(KeyCode.UpArrow) ? 1 : Input.GetKey(KeyCode.DownArrow) ? -1 : 0);
        }
        
        // updating player movement.
        
        if (traveling.x != 0 || traveling.y != 0)
            WalkInDirection(traveling);
        else SetIdle();
        // updating combat
        
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
            // dealing with sanity
            if (Sanity <= 50)
                InkSpawner.Update(this,Manager.Walls,Manager.inkiePrefab);
        }
        // calling update for parent object.
        // this is done after getting user input to improve response time.
        base.Update();
        // moving the camera to keep up with the MainCharacter (player).
        cam.transform.position = new Vector3(body.position.x, body.position.y, cam.transform.position.z);
    }
}
