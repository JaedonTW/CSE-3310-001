using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MovableCharacter
{
    protected Joystick joystick;

    protected override void Start()
    {
        base.Start();
        joystick = FindObjectOfType<Joystick>();
    }
    protected override void Update()
    {
        base.Update();
        joystick.SnapX = true;
        joystick.SnapY = true;
        Vector2 traveling = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (traveling.sqrMagnitude != 0)
            WalkInDirection(traveling);
        else SetIdle();
    }
}
