using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MenuInvader : MonoBehaviour
{
    public float cycleTime;
    public Sprite firstSprite;
    public Sprite secondSprite;
    private Image img;
    
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        StartCoroutine(MyAnimation());
        cycleTime += Random.Range(-cycleTime / 2, cycleTime / 2);
    }

    public IEnumerator MyAnimation()
    {
        bool b = false;
        while (true)
        {
            if (b)
                img.sprite = firstSprite;
            else
                img.sprite = secondSprite;
            b = !b;
            yield return new WaitForSeconds(cycleTime);
        }
    }
}
