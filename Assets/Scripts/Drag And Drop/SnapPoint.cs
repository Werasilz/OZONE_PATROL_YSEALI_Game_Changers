using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    [SerializeField] private float snapRange = 3f;

    public bool SnapObject(GameObject dragableObject)
    {
        // Distance from dragable object to this snap point
        float currentDistance = Vector2.Distance(dragableObject.transform.localPosition, transform.localPosition);

        // Distance less than snap range
        if (currentDistance <= snapRange)
        {
            // Set dragable object position to snap point position
            dragableObject.transform.localPosition = transform.localPosition;
            return true;
        }

        // Distance more than snap range
        return false;
    }
}
