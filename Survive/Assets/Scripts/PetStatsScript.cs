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
    [SerializeField] private GameObject[] pupuObj = null;
    [Header("Hours to stats fall")]
    [SerializeField, Range(1, 24)] private int hourToHungry = 1;
    [SerializeField, Range(1, 24)] private int hourToHappy = 1;
    [SerializeField, Range(1, 24)] private int hourToHealth = 1;
    [SerializeField, Range(1, 24)] private int hourToSleep = 1;
    private float hungry = 1;
    private float pupu = 0;
    private float happy = 1;
    private float health = 1;      
    private float sleep = 1;
    private bool sleeping = false;    
    private float timer;
    private Animator anim = null;
    public bool Sleeping { get => sleeping; set => sleeping = value; }
    void Awake()
    {
        UpdateStatsAfterOffline();
        anim = GetComponent<Animator>();
        //Debug.Log(DateTime.Now.Minute + DateTime.Now.Hour * 60);
    }
    void Update()
    {
        if (timer >= 60)
        {
            ChangeAllStats(1);
            PupuRespawn();
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
        CheckOffline();
    }
    private void OnApplicationPause(bool pause)
    {
        CheckOffline();
    }
    private void PupuRespawn()
    {
        for (int i = 0; i < pupuObj.Length; i++)
        {
            if (pupu > 0.25f)
            {
                pupuObj[i].SetActive(true);
                pupu -= 0.25f;
            }
        }
    }
    public void CheckOffline()
    {        
        PlayerPrefs.SetString("LastSession", DateTime.UtcNow.ToString());
        PlayerPrefs.SetFloat("Hungry", hungry);
        PlayerPrefs.SetFloat("Happy", happy);
        PlayerPrefs.SetFloat("Health", health);
        PlayerPrefs.SetFloat("Sleep", sleep);
        if (sleeping == true)
        {
            PlayerPrefs.SetInt("Sleeping", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Sleeping", 0);
        }        
    }
    public void UpdateStatsAfterOffline()
    {
        TimeSpan currentData;
        if (PlayerPrefs.HasKey("LastSession") == true)
        {
            currentData = DateTime.UtcNow - DateTime.Parse(PlayerPrefs.GetString("LastSession"));
            int minutesOffline = (int) currentData.TotalMinutes;
            hungry = PlayerPrefs.GetFloat("Hungry");
            happy = PlayerPrefs.GetFloat("Happy");
            health = PlayerPrefs.GetFloat("Health");
            sleep = PlayerPrefs.GetFloat("Sleep");
            sleeping = CheckSleeping();
            ChangeAllStats(minutesOffline);
            PupuRespawn();
            ChangeAllColor();
            timer = currentData.Seconds;
        }
    }
    private bool CheckSleeping()
    {
        int sleepN = PlayerPrefs.GetInt("Sleeping");
        if (sleepN == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }    
    private void ChangeAllStats(int multiplication)
    {
        hungry -= multiplication / (float)(hourToHungry * 60);
        pupu += multiplication / (float)(hourToHungry * 60);
        if (hungry < 0) hungry = 0;
        happy -= multiplication / (float)(hourToHappy * 60);
        if (happy < 0) happy = 0;
        health -= multiplication / (float)(hourToHealth * 60);
        if (health < 0) health = 0;
        if (sleeping == false)
        {
            sleep -= multiplication / (float)(hourToSleep * 60);
            if (sleep < 0) sleep = 0;
        }
        else
        {
            sleep += 10 * multiplication / (float)(hourToSleep * 60);
            if (sleep >= 1)
            {
                sleep = 1;
                sleeping = false;
            }
        }
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
        else if (stat <= 0.5f && stat >= 0)
        {
            imgStat.color = new Color(1, stat * 2, 0, 1);
        }
    }    
    public void HungryUp(float add)
    {
        if (hungry + add >= 1)
        {
            hungry = 1;
            ChangeColor(hungry, hungryImg);
        }
        else
        {
            hungry += add;
            ChangeColor(hungry, hungryImg);
        }
    }
    public void HealthUp(float add)
    {
        if (health + add >= 1)
        {
            health = 1;
            ChangeColor(health, healthImg);
        }
        else
        {
            health += add;
            ChangeColor(health, healthImg);
        }
    }
    public void HappyUp(float add)
    {
        if (happy + add >= 1)
        {
            happy = 1;
            ChangeColor(happy, happyImg);
        }
        else
        {
            happy += add;
            ChangeColor(happy, happyImg);
        }
    }
    public void SleepUp()
    {
        if (sleeping == false)
        {
            sleeping = true;
            anim.SetBool("Sleep", true);
        }
        else
        {
            sleeping = false;
            anim.SetBool("Sleep", false);
        }
    }
    public void ClearPupu()
    {
        for (int i = 0; i < pupuObj.Length; i++)
        {
            if (pupuObj[i].activeSelf == true)
            {
                pupuObj[i].SetActive(false);
                break;
            }
        }    
    }
}