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
    public AnimationClip sitDownClip;
    public AnimationClip dieClip;
    public float walkingSpeed;
    // Prefab input params:
    public Animation anim;
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
        SIT_DOWN,
        DIE
    }

    // Public animation methods
    public void StopWalking()
    {
        IsMoving = false;
        if (!ChangeAnimation(AnimationTypes.IDLE))
            ChangeAnimation(AnimationTypes.SIT_DOWN);
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
        if (!ChangeAnimation(index))
            throw new System.MissingFieldException("Error, animation for \"" + index.ToString() + "\" not found.  Ensure you are passing it into the prefab.");
    }
    // private Methods
    private bool ChangeAnimation(AnimationTypes type)
    {
        
        //print("ClipCount: " + anim.GetClipCount());
        var str = type.ToString();
        print("Setting Animation: " + str);
        if (anim.GetClip(str) == null)
            return false;
        anim.Play(str, PlayMode.StopAll);
        print("Animation is playing: " + anim.isPlaying);
        return true;
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
            sitDownClip,
            dieClip,
        };
        for (int i = 0; i < clips.Length; i++)
            if (clips[i] != null)
            {
                //clips[i].legacy = false;
                anim.AddClip(clips[i], ((AnimationTypes)i).ToString());
            }
        Rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving)
            Rigid.velocity = new Vector2(Mathf.Cos(MovingDirection) * walkingSpeed, Mathf.Sin(MovingDirection) * walkingSpeed);
        else
            WalkInDirection(Mathf.PI);
    }
}
