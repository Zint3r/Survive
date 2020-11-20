using UnityEngine;
public class PetScript : MonoBehaviour
{
    [SerializeField] private Transform movePoint = null;
    [SerializeField] private float timeToNextPoint = 2f;
    [SerializeField] private float timeToWait = 10f;
    [SerializeField] private float moveSpeed = 3f;
    private Transform[] points = null;
    private Transform currentPoint = null;
    private float currentTimer;
    private PetStatsScript petStats = null;
    void Start()
    {
        petStats = GetComponent<PetStatsScript>();
        GetPoints();
        currentPoint = RandomizedNextPoint();
    }
    void Update()
    {
        MoveToNextPoint();
    }
    private void GetPoints()
    {
        int count = movePoint.childCount;
        points = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            points[i] = movePoint.GetChild(i);
        }
    }
    private Transform RandomizedNextPoint()
    {
        return points[Random.Range(0, points.Length)];
    }
    private void MoveToNextPoint()
    {
        if (currentTimer > timeToWait && currentTimer < timeToWait + timeToNextPoint && petStats.Sleeping == false)
        {
            currentTimer += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, currentPoint.position) <= 0.1f)
            {
                currentPoint = RandomizedNextPoint();
            }
        }
        else if (currentTimer >= timeToWait + timeToNextPoint)
        {            
            currentTimer = 0;
        }
        else
        {
            currentTimer += Time.deltaTime;
        }
    }
}