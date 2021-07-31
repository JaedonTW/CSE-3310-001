using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    /// <summary>
    /// Health and Insanity gauges are defined first.
    /// </summary>
    public RectTransform health_Gauge;
    public RectTransform insanity_Gauge;
    
    /// <summary>
    /// update_Gauge will update both the health gauge,
    /// as well as the insanity gauge by referencing the
    /// integers in the Hurtable class which mainCharacter
    /// inherits from.
    /// </summary>    
    private void update_Gauge() 
    {
        health_Gauge.sizeDelta = new Vector2(mainCharacter.Health, health_Gauge.rect.height);
        insanity_Gauge.sizeDelta = new Vector2(mainCharacter.Sanity, insanity_Gauge.rect.height);
    }
    
    /// <summary>
    /// mainCharacter is present so we have a reference to
    /// access the health, and insanity values.
    /// </summary>
    MainCharacter mainCharacter;

    private void Start()
    {
        mainCharacter = FindObjectOfType<MainCharacter>();
    }

    private void Update()
    {
        update_Gauge();
    }
    
    public void setData(MainCharacter mainCharacter)
    {
         
    }
}
