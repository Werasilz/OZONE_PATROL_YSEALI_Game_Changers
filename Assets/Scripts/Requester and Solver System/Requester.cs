using UnityEngine;

public class Requester : MonoBehaviour, IDraggable
{
    [SerializeField] private float closestDistance = 0.5f;
    public bool IsReadyToDrag => true;
    private Vector3 originPosition;

    private void Start()
    {
        originPosition = transform.position;
    }

    public void OnStartDrag()
    {
        // print("Start Drag : " + gameObject.name);
    }

    public void OnEndDrag()
    {
        // print("End Drag : " + gameObject.name);

        for (int i = 0; i < SolveManager.Instance.GetSolvers.Length; i++)
        {
            var distance = Vector2.Distance(this.transform.position, SolveManager.Instance.GetSolversTransform[i].position);

            // Drag to solver unit
            if (distance < closestDistance)
            {
                // Solver is available
                if (SolveManager.Instance.GetSolverUnits[i].isCooldownActive == false)
                {
                    print("Drag end at :" + SolveManager.Instance.GetSolvers[i].name);
                    SolveManager.Instance.AddRequester(i);
                    Destroy(gameObject);
                    return;
                }
                // Solver is not available
                else
                {
                    // Set back to spawn point
                    transform.position = originPosition;
                    return;
                }
            }
        }

        // Set back to spawn point
        transform.position = originPosition;
    }
}

