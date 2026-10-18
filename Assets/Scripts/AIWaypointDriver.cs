using UnityEngine;

public class AIWaypointDriver : MonoBehaviour
{
    public Transform[] waypoints;
    public CarScript playerCar;
    public RaceResultsUI raceResultsUI;

    public Transform generatedWaypointContainer;
    public bool useGeneratedRoute = false;

    public float speed = 5f;
    public float turnSpeed = 3f;
    public float waypointDistance = 4f;

    public int totalLaps = 2;

    private Transform[] activeWaypoints;
    private int currentWaypoint = 0;
    private int completedLaps = 0;
    private Rigidbody rb;
    private bool routeFinished = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("AI Competitor needs a Rigidbody!");
            enabled = false;
            return;
        }

        activeWaypoints = waypoints;

        if (useGeneratedRoute)
        {
            if (generatedWaypointContainer == null ||
                generatedWaypointContainer.childCount == 0)
            {
                
                enabled = false;
                return;
            }

            int count = generatedWaypointContainer.childCount;
            activeWaypoints = new Transform[count];

            for (int i = 0; i < count; i++)
            {
                activeWaypoints[i] =
                    generatedWaypointContainer.GetChild(i);
            }

            
        }

        if (activeWaypoints == null ||
            activeWaypoints.Length == 0)
        {
            
            enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (rb == null || playerCar == null ||
            !playerCar.canMove || routeFinished ||
            activeWaypoints == null ||
            activeWaypoints.Length == 0)
            return;

        Transform target = activeWaypoints[currentWaypoint];

        if (target == null)
            return;

        Vector3 direction = target.position - rb.position;
        direction.y = 0f;

        if (direction.magnitude < waypointDistance)
        {
            if (currentWaypoint < activeWaypoints.Length - 1)
            {
                currentWaypoint++;
            }
            else
            {
                completedLaps++;

                Debug.Log(
                    "AI completed route pass " +
                    completedLaps + " / " + totalLaps
                );

                if (completedLaps >= totalLaps)
                {
                    routeFinished = true;

                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;

                    

                    if (raceResultsUI != null)
                    {
                        raceResultsUI.ShowDefeat();
                    }
                    else
                    {
                        Debug.LogWarning(
                            "Race Results UI is not assigned!"
                        );
                    }
                }
                else
                {
                    
                    currentWaypoint = 0;

                    
                }
            }

            return;
        }

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        Quaternion newRotation = Quaternion.Slerp(
            rb.rotation,
            targetRotation,
            turnSpeed * Time.fixedDeltaTime
        );

        rb.MoveRotation(newRotation);

        Vector3 movement =
            newRotation * Vector3.forward *
            speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);
    }
}