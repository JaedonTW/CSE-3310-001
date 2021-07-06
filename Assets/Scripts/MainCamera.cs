using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    MainCharacter mainCharacter;
    Vector3 main_Camera_Position;
    Vector3 main_Character_Position;
    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = FindObjectOfType<MainCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = new Vector3(mainCharacter.transform.position.x,mainCharacter.transform.position.y,Camera.main.transform.position.z);
    }
}
