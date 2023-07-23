using UnityEngine;

public class ColorController : MonoBehaviour
{
    // Reference to the material attached to the game object
    public Material targetMaterial;

    // Customizable colors
    public Color defaultColor = Color.white;
    public Color highlightColor = Color.green;
    public Color clickColor = Color.blue;

    // Boolean to enable/disable mouse interaction
    public bool useMouseInteraction = true;

    private Renderer objectRenderer;
    private Material clonedMaterial; // The new material instance

    private void Start()
    {
        // Get the Renderer component that displays the material
        objectRenderer = GetComponent<Renderer>();

        // Make sure the target material is set (use the sharedMaterial to avoid changing the material asset)
        if (targetMaterial == null)
        {
            targetMaterial = objectRenderer.sharedMaterial;
        }

        // Clone the original material to create a new instance
        clonedMaterial = Instantiate(targetMaterial);

        // Apply the default color to the new material
        clonedMaterial.color = defaultColor;

        // Assign the new material to the renderer
        objectRenderer.material = clonedMaterial;
    }

    // Mouse Interaction Methods
    #region Mouse Interaction

    private void OnMouseEnter()
    {
        if (useMouseInteraction)
        {
            // Highlight the material when the mouse enters the object
            clonedMaterial.color = highlightColor;
        }
    }

    private void OnMouseExit()
    {
        if (useMouseInteraction)
        {
            // Reset the material color when the mouse exits the object
            clonedMaterial.color = defaultColor;
        }
    }

    private void OnMouseDown()
    {
        if (useMouseInteraction)
        {
            // Change the material color when the mouse button is pressed down
            clonedMaterial.color = clickColor;
        }
    }

    private void OnMouseUp()
    {
        if (useMouseInteraction)
        {
            // Reset the material color when the mouse button is released
            clonedMaterial.color = defaultColor;
        }
    }

    #endregion

    // Method to set the material color externally
    public void SetColor(Color color)
    {
        clonedMaterial.color = color;
    }
}
