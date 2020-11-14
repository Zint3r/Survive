using UnityEngine;
using UnityEngine.UI;
using System;
public class PetStatsScript : MonoBehaviour
{
    [SerializeField] private Image hungryImg = null;
    [SerializeField] private Image happyImg = null;
    [SerializeField] private Image healthImg = null;
    [SerializeField] private Image sleepImg = null;
    private float hungry = 1;
    //private float hungryMultiplicator = 1;
    private float health = 1;
    //private float healthMultiplicator = 1;
    private float happy = 1;
    //private float happyMultiplicator = 1;
    private float sleep = 1;
    private bool sleeng = false;
    private DateTime currentData;
    void Start()
    {
        hungryImg.color = Color.green;
        Debug.Log(DateTime.Now.Minute + DateTime.Now.Hour * 60);
    }
    void Update()
    {
        //Lerp(happyImg.color, Color.yellow, Mathf.PingPong(Time.time, 1));        
        ChangeAllStats();
    }
    private void ChangeAllStats()
    {
        ChangeHungryStatAndColor();
        ChangeHappyStatAndColor();
        ChangeHealthStatAndColor();
        ChangeSleepStatAndColor();
    }
    public void ChangeHungryStatAndColor()
    {
        if (hungry <= 1 && hungry > 0.5f)
        {
            hungryImg.color = new Color((1 - hungry) * 2, 1, 0, 1);
            hungry -= Time.deltaTime / 100;
        }
        else if (hungry <= 0.5f && hungry > 0)
        {
            hungryImg.color = new Color(1, hungry * 2, 0, 1);
            hungry -= Time.deltaTime / 100;
        }
    }
    public void ChangeHappyStatAndColor()
    {
        if (happy <= 1 && happy > 0.5f)
        {
            happyImg.color = new Color((1 - happy) * 2, 1, 0, 1);
            happy -= Time.deltaTime / 50;
        }
        else if (happy <= 0.5f && happy > 0)
        {
            happyImg.color = new Color(1, happy * 2, 0, 1);
            happy -= Time.deltaTime / 50;
        }
    }
    public void ChangeHealthStatAndColor()
    {
        if (health <= 1 && health > 0.5f)
        {
            healthImg.color = new Color((1 - health) * 2, 1, 0, 1);
            health -= Time.deltaTime / 300;
        }
        else if (health <= 0.5f && health > 0)
        {
            healthImg.color = new Color(1, health * 2, 0, 1);
            health -= Time.deltaTime / 300;
        }
    }
    public void ChangeSleepStatAndColor()
    {
        if (sleep <= 1 && sleep > 0.5f)
        {
            sleepImg.color = new Color((1 - sleep) * 2, 1, 0, 1);
            sleep -= Time.deltaTime / 10;
        }
        else if (sleep <= 0.5f && sleep > 0)
        {
            sleepImg.color = new Color(1, sleep * 2, 0, 1);
            sleep -= Time.deltaTime / 10;
        }
    }
    public void HungryUp(float add)
    {
        if (hungry >= hungry - add)
        {
            hungry = 1;
        }
        else
        {
            hungry += add;
        }
    }
    public void HealthUp(float add)
    {
        if (health >= health - add)
        {
            health = 1;
        }
        else
        {
            health += add;
        }
    }
    public void HappyUp(float add)
    {
        if (happy >= happy - add)
        {
            happy = 1;
        }
        else
        {
            happy += add;
        }
    }
    public void SleepUp()
    {
        if (sleeng == false)
        {
            sleeng = true;
        }
    }
}