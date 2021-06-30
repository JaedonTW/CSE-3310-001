using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableCharacter : Hurtable
{
    // Input Params:
    // animation
    public AnimationClip idleClip;
    /// <summary>
    /// Required
    /// </summary>
    public AnimationClip walkUpClip;
    /// <summary>
    /// Required
    /// </summary>
    public AnimationClip walkLeftClip;
    /// <summary>
    /// Required
    /// </summary>
    public AnimationClip walkRightClip;
    /// <summary>
    /// Required
    /// </summary>
    public AnimationClip walkDownClip;
    public AnimationClip sitClip;
    public AnimationClip dieClip;
    public float walkingSpeed;
    // Prefab input params:
    public Animator anim;
    // private variables
    // max magnetude should be walkingSpeed.
    private Vector2 MovingVector { get; set; }
    public bool IsMoving { get; private set; }
    //
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
    public void SetIdle()
    {
        IsMoving = false;
        body.velocity = Vector2.zero;
        ChangeAnimation(AnimationTypes.IDLE);
    }
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
    private void ChangeAnimation(AnimationTypes type) =>
        anim.SetInteger("AnimationType", (int)type);
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
        var oldNameFilters = new string[]
        {
            "IDLE",
            "RIGHT",
            "UP",
            "LEFT",
            "DOWN",
            "SIT",
            "DIE"
        };
        AnimatorOverrideController aoc = new AnimatorOverrideController(anim.runtimeAnimatorController);
        // Getting old (generic) clips in the correct order.
        AnimationClip[] oldClips = new AnimationClip[clips.Length];
        
        foreach(AnimationClip old in aoc.animationClips)
            for(int i = 0; i < oldNameFilters.Length; i++)
                if(old.name.ToUpper().Contains(oldNameFilters[i]))
                {
                    oldClips[i] = old;
                    break;
                }
        // building replacement pairs
        var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>(oldClips.Length);
        for (int i = 0; i < clips.Length; i++)
            if (clips[i] != null)
            {
                clips[i].legacy = false;
                print("Replacing: " + ((AnimationTypes)i).ToString());
                overrides.Add(new KeyValuePair<AnimationClip, AnimationClip>(oldClips[i], clips[i]));
            }
        // replacing old clips and pushing.
        aoc.ApplyOverrides(overrides);
        anim.runtimeAnimatorController = aoc;
        //
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (IsMoving)
            body.velocity = MovingVector;
    }
}
