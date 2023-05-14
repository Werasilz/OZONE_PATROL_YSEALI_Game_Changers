using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DragAndDropManager : Singleton<DragAndDropManager>
{
    private Vector3 velocity = Vector3.zero;

    [Header("Drag Settings")]
    [SerializeField] private float mouseDragSpeed = 0.05f;

    [Header("Drag Status")]
    private IDraggable draggable;
    public bool isDragging => draggable != null;

    [HideInInspector] public bool debugHit;

    private void OnEnable()
    {
        TouchInputManager.OnStartTouch += MousePressed;
    }

    private void OnDisable()
    {
        TouchInputManager.OnEndTouch -= MousePressed;
    }

    private void MousePressed(Vector2 touchPosition, float time)
    {
        // Raycast using touch position
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

        if (hit2D.collider != null)
        {
            // Hit object and there have the tag "Draggable" or have a script with IDraggable interface
            if (/*hit2D.collider.gameObject.CompareTag("Draggable") ||*/ hit2D.collider.gameObject.GetComponent<IDraggable>() != null)
            {
                if (debugHit) print("Hit Draggable Object at position " + touchPosition);

                // Call Drag Update for set position of the object to touch position
                StartCoroutine(DragUpdate(hit2D.collider.gameObject));
            }

            // Hit object with IInteractable interface
            if (hit2D.collider.gameObject.GetComponent<IInteractable>() != null)
            {
                if (debugHit) print("Hit Interactable Object at position " + touchPosition);

                // Get IInteractable interface
                hit2D.collider.gameObject.TryGetComponent<IInteractable>(out var interactable);

                // If IInteractable interface is not null then call Interact()
                interactable?.Interact();
            }
        }
    }

    public void DragUpdateManually(GameObject dragTarget)
    {
        StartCoroutine(DragUpdate(dragTarget));
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        // Get IDraggable interface
        clickedObject.TryGetComponent<IDraggable>(out draggable);

        // Drag object is not ready
        if (draggable?.IsReadyToDrag == false) yield break;

        // If IDraggable interface not null then call OnStartDrag()
        draggable?.OnStartDrag();

        // Distance from clicked object and camera (default is 10)
        float initialDistance = Vector3.Distance(clickedObject.transform.position, Camera.main.transform.position);

        // If has contact with touch value will not zero
        while (TouchInputManager.touchPress != 0)
        {
            // If clicked object was destroyed
            if (clickedObject == null) yield break;

            // Raycast using touch position
            Ray ray = Camera.main.ScreenPointToRay(TouchInputManager.touchPosition);

            // Get position
            Vector3 target = ray.GetPoint(initialDistance);

            // Get Sorting Group Component
            clickedObject.TryGetComponent<SortingGroup>(out var sortingGroup);

            // Set z axis by sorting order
            if (sortingGroup)
            {
                target.z = -sortingGroup.sortingOrder;
            }
            else
            {
                Debug.LogError(string.Format("This {0} is not have Sorting Group Component", clickedObject.name));
            }

            // Set position
            clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, target, ref velocity, mouseDragSpeed);

            yield return null;
        }

        // If IDraggable interface not null then call OnEndDrag()
        draggable?.OnEndDrag();
    }
}
