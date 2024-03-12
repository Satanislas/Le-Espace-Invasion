using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public RectTransform grid;
    private Vector3 initialPos;
    public float timeBeforeLoad;
    public float timeAfterLoad;
    private bool isMoving;
    public float speedOfGrid;
    private string scene;
    public static SceneFader sceneFader; //singleton
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        initialPos = grid.localPosition;
        sceneFader ??= this;
    }

    private void Update()
    {
        if (isMoving)
        {
            grid.localPosition += grid.right * (speedOfGrid * Time.deltaTime * -1);
        }
    }

    public void LoadScene(string sceneToLoad)
    {
        isMoving = true;
        scene = sceneToLoad;
        StartCoroutine(StartAnimation());
    }

    public IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(timeBeforeLoad);
        SceneManager.LoadScene(scene);
        yield return new WaitForSeconds(timeAfterLoad);
        isMoving = false;
        grid.localPosition = initialPos;
    }
}
