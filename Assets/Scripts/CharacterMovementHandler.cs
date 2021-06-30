using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementHandler : MonoBehaviour
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
    private float MovingDirection { get; set; }
    private bool IsMoving { get; set; }
    private Rigidbody2D Rigid { get; set; }
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
    public void StopWalking()
    {
        IsMoving = false;
        ChangeAnimation(AnimationTypes.IDLE);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="angle">Angle of movement in radians</param>
    public void WalkInDirection(float angle)
    {
        MovingDirection = angle;

        IsMoving = true;
        // ensures the angle falls in the interval [0,2*pi]
        angle %= 2 * Mathf.PI;
        // converts the angle to fall in the interval [0,4] then converts to an AnimationType
        AnimationTypes index = (AnimationTypes)(Mathf.RoundToInt(angle * 2 / Mathf.PI) + (int)AnimationTypes.WALK_RIGHT);

        // animating
        ChangeAnimation(index);
    }
    // private Methods
    private void ChangeAnimation(AnimationTypes type)
    {
        anim.SetInteger("AnimationType", (int)type);
    }
    // Start is called before the first frame update
    void Start()
    {
        var clips = new AnimationClip[]
        {
            idleClip,
            walkUpClip,
            walkLeftClip,
            walkRightClip,
            walkDownClip,
            sitClip,
            dieClip,
        };
        var oldNameFilters = new string[]
        {
            "IDLE",
            "UP",
            "LEFT",
            "RIGHT",
            "UP",
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
                overrides.Add(new KeyValuePair<AnimationClip, AnimationClip>(oldClips[i], clips[i]));
        // replacing old clips and pushing.
        aoc.ApplyOverrides(overrides);
        anim.runtimeAnimatorController = aoc;
        //
        Rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving)
            Rigid.velocity = new Vector2(Mathf.Cos(MovingDirection) * walkingSpeed, Mathf.Sin(MovingDirection) * walkingSpeed);
        if(Random.Range(0f,1f) < 0.1)
            WalkInDirection(Random.Range(0,Mathf.PI*2));
    }
}
