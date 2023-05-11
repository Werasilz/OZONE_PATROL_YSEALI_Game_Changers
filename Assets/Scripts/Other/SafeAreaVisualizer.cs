#if UNITY_EDITOR
using UnityEngine;

public class SafeAreaVisualizer : MonoBehaviour
{
    [Range(0f, 50f)]
    [SerializeField] float borderSizePercentage = 7.5f;
    [SerializeField] private Color borderColor = new Color(1, 1, 1, 0.5f);

    [ExecuteInEditMode]
    private void OnGUI()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        float borderSize = Mathf.Min(screenWidth, screenHeight) * (borderSizePercentage / 100f);

        // Set the GUI color to the specified border color with alpha transparency
        GUI.color = borderColor;

        // Draw top line
        GUI.DrawTexture(new Rect(0, 0, screenWidth, borderSize), Texture2D.whiteTexture);

        // Draw left line
        GUI.DrawTexture(new Rect(0, borderSize, borderSize, screenHeight - 2 * borderSize), Texture2D.whiteTexture);

        // Draw bottom line
        GUI.DrawTexture(new Rect(0, screenHeight - borderSize, screenWidth, borderSize), Texture2D.whiteTexture);

        // Draw right line
        GUI.DrawTexture(new Rect(screenWidth - borderSize, borderSize, borderSize, screenHeight - 2 * borderSize), Texture2D.whiteTexture);

        // Reset the GUI color to the default color
        GUI.color = Color.white;
    }
}
#endif
