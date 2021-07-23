using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A class for encapsulating all characters that move and need animations for doing so.
/// </summary>
public class MovableCharacter : Hurtable
{
    // Parameters that should be supplied in unity.
    /// <summary>
    /// The IDLE animation clip
    /// </summary>
    public AnimationClip idleClip;
    /// <summary>
    /// The walking up animation clip
    /// </summary>
    public AnimationClip walkUpClip;
    /// <summary>
    /// The walking left animation clip
    /// </summary>
    public AnimationClip walkLeftClip;
    /// <summary>
    /// The walking right animation clip
    /// </summary>
    public AnimationClip walkRightClip;
    /// <summary>
    /// The walking down animation clip
    /// </summary>
    public AnimationClip walkDownClip;
    /// <summary>
    /// The sitting animation clip
    /// </summary>
    public AnimationClip sitClip;
    /// <summary>
    /// The dying animation clip
    /// </summary>
    public AnimationClip dieClip;

    /// <summary>
    /// The max walking speed of the MovableCharacter
    /// </summary>
    public float walkingSpeed = 1;
    
    // This marks the end of the inputs to be supplied in unity.
    // properties
    // max magnetude should be walkingSpeed.
    private Animator Anim { get; set; }
    /// <summary>
    /// The MovingVector for the player.  Should have a magnetude less than or equal to walkingSpeed.
    /// </summary>
    private Vector2 MovingVector { get; set; }
    /// <summary>
    /// Set to true when this MovableCharacter is walking
    /// </summary>
    public bool IsMoving { get; private set; }
    /// <summary>
    /// The various types of animations a MovableCharacter can perform.
    /// </summary>
    public enum AnimationTypes : byte
    {
        IDLE = 0,
        WALK_RIGHT,
        WALK_UP,
        WALK_LEFT,
        WALK_DOWN,
        SIT,
        DIE
    }

    // Public animation methods
    /// <summary>
    /// Sets the MovableCharacter to the Idle animation and stops it from moving.
    /// </summary>
    public void SetIdle()
    {
        IsMoving = false;
        body.velocity = Vector2.zero;
        ChangeAnimation(AnimationTypes.IDLE);
    }
    /// <summary>
    /// Sets the MovableCharacter to the Sit animation and stops it from moving.
    /// </summary>
    public void SetSit()
    {
        IsMoving = false;
        body.velocity = Vector2.zero;
        ChangeAnimation(AnimationTypes.SIT);
    }
    /// <summary>
    /// Should have a magnetude of at most 1.  Will be scaled with walking speed.
    /// </summary>
    /// <param name="angle">Angle of movement in radians</param>
    public void WalkInDirection(Vector2 dir)
    {
        if (dir.sqrMagnitude == 0)
            return;
        MovingVector = new Vector2(walkingSpeed*dir.x,walkingSpeed*dir.y);

        IsMoving = true;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            ChangeAnimation(dir.x > 0 ? AnimationTypes.WALK_RIGHT : AnimationTypes.WALK_LEFT);
        else
            ChangeAnimation(dir.y > 0 ? AnimationTypes.WALK_UP: AnimationTypes.WALK_DOWN);
    }
    // private Methods
    /// <summary>
    /// Changes the current animation to the animation type specified.
    /// </summary>
    /// <param name="type">Desired Animation</param>
    private void ChangeAnimation(AnimationTypes type) =>
        Anim.SetInteger("AnimationType", (int)type);
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (walkingSpeed <= 0)
            print("Warn: You have the walking speed for your MovableCharacter at < " + transform.position.x + ", " + transform.position.y + "> set to " + walkingSpeed + ", is this intentional?");
        // Puts the supplied clips into an array to make processing easier.
        var clips = new AnimationClip[]
        {
            idleClip,
            walkRightClip,
            walkUpClip,
            walkLeftClip,
            walkDownClip,
            sitClip,
            dieClip,
        };
        // A set of filters to find the corrosponding generic animations.
        var oldNameFilters = new string[]
        {
            "GenericIdle",
            "GenericWalkRight",
            "GenericWalkUp",
            "GenericWalkLeft",
            "GenericWalkDown",
            "GenericSit",
            "GenericDie"
        };
        // Getting the animator.
        Anim = GetComponent<Animator>();
        
        // replacing old animations
        AnimatorOverrideController aoc = new AnimatorOverrideController(Anim.runtimeAnimatorController);
        // Getting old (generic) clips in the same order as the enum "AnimationTypes".
        AnimationClip[] oldClips = new AnimationClip[clips.Length];
        foreach(AnimationClip old in aoc.animationClips)
            for(int i = 0; i < oldNameFilters.Length; i++)
                if(old.name == oldNameFilters[i])
                {
                    //print("Found \"" + oldNameFilters[i] + "\"");
                    oldClips[i] = old;
                    break;
                }

        // building replacement pairs
        var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>(oldClips.Length);
        //print(136);
        for (int i = 0; i < clips.Length; i++)
            if (clips[i] != null)
            {
                //print(clips[i].name);
                //clips[i].legacy = false;
                //print("Replacing: " + ((AnimationTypes)i).ToString());
                overrides.Add(new KeyValuePair<AnimationClip, AnimationClip>(oldClips[i], clips[i]));
            }
        // replacing old clips and pushing.
        aoc.ApplyOverrides(overrides);
        Anim.runtimeAnimatorController = aoc;
        //
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (IsMoving)
            body.velocity = MovingVector;
        if (weapon != null)
            weapon.Tick();
    }
}
