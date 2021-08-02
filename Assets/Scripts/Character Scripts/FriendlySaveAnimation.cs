using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlySaveAnimation : MonoBehaviour
{
    public Transform spriteTransform;
    int CurrentTick { get; set; } = 0;
    const int TotalRotations = 40;
    const int TotalTicks = 60;
    /// <summary>
    /// a constant multiplier for the angular acceleration, given in degrees/ticks^2.
    /// NOTE: When changing 'TotalRotations' and 'TotalTicks', it is necessary 
    ///   to check if this value should be a float.
    /// </summary>
    const int AngularAcceleration = 360 * TotalRotations / (TotalTicks * TotalTicks);

    // Update is called once per frame
    void Update()
    {
        if (CurrentTick < TotalTicks)
        {
            CurrentTick++;
            // the rotation speed increases quadratically, following this formula:
            var angle = CurrentTick * CurrentTick * AngularAcceleration;
            spriteTransform.eulerAngles = new Vector3(0, 0, angle);
            var scale = (float)(TotalTicks - CurrentTick) / TotalTicks;
            spriteTransform.localScale = new Vector3(scale, scale, scale);
        }
        else
            Destroy(gameObject);
    }
}
