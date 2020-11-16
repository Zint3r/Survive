using UnityEngine;
using UnityEngine.UI;
using System;
public class PetStatsScript : MonoBehaviour
{
    [Header("Links to UI")]
    [SerializeField] private Image hungryImg = null;
    [SerializeField] private Image happyImg = null;
    [SerializeField] private Image healthImg = null;
    [SerializeField] private Image sleepImg = null;
    [Header("Hours to stats fall")]
    [SerializeField, Range(1, 24)] private int hourToHungry = 1;
    [SerializeField, Range(1, 24)] private int hourToHappy = 1;
    [SerializeField, Range(1, 24)] private int hourToHealth = 1;
    [SerializeField, Range(1, 24)] private int hourToSleep = 1;
    private float hungry = 1;
    private float happy = 1;
    private float health = 1;      
    private float sleep = 1;
    private bool sleeng = false;    
    private float timer;    
    void Awake()
    {
        //UpdateStatsAfterOffline();
        //Debug.Log(DateTime.Now.Minute + DateTime.Now.Hour * 60);
    }
    void Update()
    {
        if (timer >= 60)
        {
            ChangeAllStats();
            ChangeAllColor();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
    private void OnApplicationQuit()
    {
        //CheckOffline();
    }
    private void OnApplicationPause(bool pause)
    {
        //CheckOffline();
    }
    public void CheckOffline()
    {        
        PlayerPrefs.SetString("LastSession", DateTime.UtcNow.ToString());
    }
    public void UpdateStatsAfterOffline()
    {
        TimeSpan currentData;
        if (PlayerPrefs.HasKey("LastSession") == true)
        {
            currentData = DateTime.UtcNow - DateTime.Parse(PlayerPrefs.GetString("LastSession"));
            int minutesOffline = (int) currentData.TotalMinutes;
            Debug.Log(minutesOffline);
            ChangeAllStats(minutesOffline);
            ChangeAllColor();
            timer = currentData.Seconds;
        }
    }
    private void ChangeAllStats()
    {
        hungry -= 1 / (float)(hourToHungry * 60);
        happy -= 1 / (float)(hourToHappy * 60);
        health -= 1 / (float)(hourToHealth * 60);
        sleep -= 1 / (float)(hourToSleep * 60);
    }
    private void ChangeAllStats(int multiplication)
    {
        hungry -= multiplication / (float)(hourToHungry * 60);
        happy -= multiplication / (float)(hourToHappy * 60);
        health -= multiplication / (float)(hourToHealth * 60);
        sleep -= multiplication / (float)(hourToSleep * 60);
    }
    private void ChangeAllColor()
    {
        ChangeColor(hungry, hungryImg);
        ChangeColor(happy, happyImg);
        ChangeColor(health, healthImg);
        ChangeColor(sleep, sleepImg);        
    }
    public void ChangeColor(float stat, Image imgStat)
    {
        if (stat <= 1 && stat > 0.5f)
        {
            imgStat.color = new Color((1 - stat) * 2, 1, 0, 1);
        }
        else if (stat <= 0.5f && stat > 0)
        {
            imgStat.color = new Color(1, stat * 2, 0, 1);
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