using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResizeBoardUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject resizePanel;
    public Button openButton;
    public Button confirmButton;
    public TMP_InputField widthInput;
    public TMP_InputField heightInput;

    [Header("Target Drawing Plane")]
    public Transform drawingPlane;
    private const float worldScaleFactor = 50f;
    void Start()
    {
        // Hide the panel at the start
        resizePanel.SetActive(false);

        // Button listeners
        openButton.onClick.AddListener(OpenPanel);
        confirmButton.onClick.AddListener(ApplyResize);
    }

    void OpenPanel()
    {
        resizePanel.SetActive(true);
    }

    void ApplyResize()
    {
        // Validate and parse input
        if (float.TryParse(widthInput.text, out float widthCM) && float.TryParse(heightInput.text, out float heightCM))
        {
            // Convert from centimeters to meters
            float widthM = widthCM / worldScaleFactor;
            float heightM = heightCM / worldScaleFactor;

            // Apply scale directly to quad (which is 1x1 in size)
            drawingPlane.localScale = new Vector3(widthM, heightM, 1f);

            // Optionally hide the panel
            resizePanel.SetActive(false);
            Debug.LogWarning("Drawing Plane");
            GridRenderer grid = drawingPlane.GetComponent<GridRenderer>();
            if (grid != null)
            {
                grid.GenerateGrid(heightM, widthM);

            }
            else
            {
                Debug.LogWarning("GridRenderer not found on drawingPlane.");
            }
            heightCM = 0;
            widthCM = 0;
        }
        else
        {
            Debug.LogWarning("Invalid width or height input. Please enter valid numbers.");
        }
    }

}
