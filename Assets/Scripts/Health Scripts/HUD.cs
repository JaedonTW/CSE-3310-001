﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthGauge;
    [SerializeField]
    private RectTransform sanityGauge;
    
    static RectTransform HealthBar { get; set; }
    static RectTransform SanityBar { get; set; }
    /// <summary>
    /// Sets the onscreen value for the health meter where 100 is max and 0 is none.
    /// </summary>
    /// <param name="health">Desired health value.</param>
    public static void SetHealth(int health) => HealthBar.transform.localScale = new Vector3(1f * health / 100, 1, 1);
    /// <summary>
    /// Sets the onscreen value for the sanity meter where 100 is max and 0 is none.
    /// </summary>
    /// <param name="health">Desired sanity value.</param>
    public static void SetSanity(int health) => SanityBar.transform.localScale = new Vector3(1f * health / 100, 1, 1);
    public void Start()
    {
        HealthBar = healthGauge;
        SanityBar = sanityGauge;
    }
}
