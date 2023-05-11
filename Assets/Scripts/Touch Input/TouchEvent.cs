using UnityEngine;

public class TouchEvent : MonoBehaviour
{
    private void OnEnable()
    {
        TouchInputManager.OnStartTouch += Touch;
    }

    private void OnDisable()
    {
        TouchInputManager.OnEndTouch -= Touch;
    }

    private void Touch(Vector2 touchPosition, float time)
    {
        print("Touch Position " + touchPosition);

        // Set Position To Touch Position
        // Vector3 screenCoordinates = new Vector3(touchPosition.x, touchPosition.y, mainCamera.nearClipPlane);
        // Vector3 worldCoordinates = mainCamera.ScreenToWorldPoint(screenCoordinates);
        // worldCoordinates.z = 0;
        // transform.position = worldCoordinates;
    }
}
