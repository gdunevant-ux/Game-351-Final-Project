using UnityEngine;

public class AIWaypointDriver : MonoBehaviour
{
    // list of waypoints for the ai to follow
    public Transform[] waypoints;

    // reference to the player's car
    public CarScript playerCar;

    // reference to the race results ui
    public RaceResultsUI raceResultsUI;

    // generated waypoint route
    public Transform generatedWaypointContainer;
    public bool useGeneratedRoute = false;

    // ai movement settings
    public float speed = 5f;
    public float turnSpeed = 3f;
    public float waypointDistance = 4f;

    // total laps the ai must complete
    public int totalLaps = 2;

    // active waypoint list
    private Transform[] activeWaypoints;

    // current waypoint index
    private int currentWaypoint = 0;

    // number of completed laps
    private int completedLaps = 0;

    // ai rigidbody
    private Rigidbody rb;

    // checks if the ai finished the race
    private bool routeFinished = false;

    void Start()
    {
        // get the rigidbody
        rb = GetComponent<Rigidbody>();

        // stop if there is no rigidbody
        if (rb == null)
        {
            Debug.LogError("AI Competitor needs a Rigidbody!");
            enabled = false;
            return;
        }

        // use the assigned waypoints by default
        activeWaypoints = waypoints;

        // use generated waypoints if enabled
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

            // copy generated waypoints into the array
            for (int i = 0; i < count; i++)
            {
                activeWaypoints[i] =
                    generatedWaypointContainer.GetChild(i);
            }
        }

        // stop if there are no waypoints
        if (activeWaypoints == null ||
            activeWaypoints.Length == 0)
        {
            enabled = false;
        }
    }

    void FixedUpdate()
    {
        // stop if the ai cannot move
        if (rb == null || playerCar == null ||
            !playerCar.canMove || routeFinished ||
            activeWaypoints == null ||
            activeWaypoints.Length == 0)
            return;

        // get the current waypoint
        Transform target = activeWaypoints[currentWaypoint];

        // stop if the waypoint is missing
        if (target == null)
            return;

        // find the direction to the waypoint
        Vector3 direction = target.position - rb.position;
        direction.y = 0f;

        // move to the next waypoint when close enough
        if (direction.magnitude < waypointDistance)
        {
            if (currentWaypoint < activeWaypoints.Length - 1)
            {
                currentWaypoint++;
            }
            else
            {
                // finish one lap
                completedLaps++;

                Debug.Log(
                    "AI completed route pass " +
                    completedLaps + " / " + totalLaps
                );

                // finish the race
                if (completedLaps >= totalLaps)
                {
                    routeFinished = true;

                    // stop the ai car
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;

                    // show the defeat screen
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
                    // start the next lap
                    currentWaypoint = 0;
                }
            }

            return;
        }

        // rotate toward the waypoint
        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        Quaternion newRotation = Quaternion.Slerp(
            rb.rotation,
            targetRotation,
            turnSpeed * Time.fixedDeltaTime
        );

        rb.MoveRotation(newRotation);

        // move toward the waypoint
        Vector3 movement =
            newRotation * Vector3.forward *
            speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);
    }
}