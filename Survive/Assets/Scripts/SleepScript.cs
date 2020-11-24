using System.Collections;
using UnityEngine;
public class SleepScript : MonoBehaviour
{
    [SerializeField] private GameObject buttonSleepOn = null;
    [SerializeField] private GameObject buttonSleepOff = null;
    private SpriteRenderer sleepImg = null;
    private PetStatsScript petStat = null;
    void Start()
    {
        petStat = FindObjectOfType<PetStatsScript>();
        sleepImg = GetComponent<SpriteRenderer>();
        sleepImg.enabled = petStat.Sleeping;
        if (petStat.Sleeping == true)
        {
            buttonSleepOff.SetActive(true);
            buttonSleepOn.SetActive(false);
        }
        else
        {
            buttonSleepOff.SetActive(false);
            buttonSleepOn.SetActive(true);
        }        
    }
    public void StartSleep()
    {
        sleepImg.enabled = true;
        StartCoroutine(Animation());
    }
    public void StopSleep()
    {
        sleepImg.enabled = false;
        StopCoroutine(Animation());
    }
    IEnumerator Animation()
    {
        while (true)
        {
            if (sleepImg.flipX == true)
            {
                sleepImg.flipX = false;
            }
            else
            {
                sleepImg.flipX = true;
            }
            yield return new WaitForSeconds(2f);
        }
    }
}