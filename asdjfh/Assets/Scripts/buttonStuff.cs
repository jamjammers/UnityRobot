using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonStuff : MonoBehaviour
{
    public GameObject button;
    public GameObject panel;
    public GameObject bot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void trigger()
    {
        Debug.Log("Button clicked!");
        Destroy(panel);
        Destroy(button);
        bot.GetComponent<DriveTrain>().begin();

    }
}
