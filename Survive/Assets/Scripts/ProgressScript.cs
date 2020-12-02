using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ProgressScript : MonoBehaviour
{
    [SerializeField] private GameObject panelObj = null;
    [SerializeField] private Image imgObject = null;
    [SerializeField] private Sprite foodSprite = null;
    [SerializeField] private Sprite broomSprite = null;
    [SerializeField] private Sprite ballSprite = null;
    [SerializeField] private Sprite syringeSprite = null;    
    public void FoodActive()
    {
        panelObj.SetActive(true);
        StartCoroutine(FillingImg(foodSprite));
    }
    public void BroomActive()
    {
        panelObj.SetActive(true);
        StartCoroutine(FillingImg(broomSprite));
    }
    public void BallActive()
    {
        panelObj.SetActive(true);
        StartCoroutine(FillingImg(ballSprite));
    }
    public void SyringeActive()
    {
        panelObj.SetActive(true);
        StartCoroutine(FillingImg(syringeSprite));
    }    
    private IEnumerator FillingImg(Sprite spr)
    {
        while (true)
        {
            imgObject.sprite = spr;
            imgObject.fillAmount -= 0.05f;
            if (imgObject.fillAmount == 0)
            {
                panelObj.SetActive(false);
                imgObject.fillAmount = 1;
                break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}