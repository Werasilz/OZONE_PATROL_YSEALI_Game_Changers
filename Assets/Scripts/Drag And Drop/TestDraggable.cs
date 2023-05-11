using UnityEngine;

public class TestDraggable : MonoBehaviour, IInteractable, IDraggable
{
    public bool IsReadyToDrag => true;

    public void Interact()
    {
        print("Interact : " + gameObject.name);
    }

    public void OnStartDrag()
    {
        print("Start Drag : " + gameObject.name);
    }

    public void OnEndDrag()
    {
        print("End Drag : " + gameObject.name);
    }
}

