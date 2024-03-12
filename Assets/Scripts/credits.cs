using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
    public string mainMenuName;
    public void CallSceneFader()
    {
        SceneFader.sceneFader.LoadScene(mainMenuName);
    }
}
