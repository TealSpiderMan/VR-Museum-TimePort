using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class OutlineSquareEffect : MonoBehaviour
{
    public float outlineWidth = 2f; // Width of the outline in pixels
    public Color outlineColor = Color.white; // Color of the outline

    private Image image;
    private Sprite outlineSprite;

    void Start()
    {
        image = GetComponent<Image>();

        // Create or load a sprite with just the outline
        CreateOutlineSprite();

        // Apply the outline sprite to the Image
        if (outlineSprite != null)
        {
            image.sprite = outlineSprite;
            image.type = Image.Type.Simple; // Ensure the image renders as a simple sprite
            image.color = outlineColor; // Set the outline color
        }

        // Ensure the Image uses the correct material (optional, depending on your setup)
        if (image.material == null || image.material.name.Contains("BA_Glow_White_01"))
        {
            // If using the BA_Glow_White_01 material, you might need to adjust its properties
            // or create a new material for transparency + outline
            Material outlineMaterial = new Material(Shader.Find("Universal Render Pipeline/2D/Sprite-Unlit")); // URP shader
            if (outlineMaterial != null)
            {
                outlineMaterial.color = outlineColor;
                image.material = outlineMaterial;
            }
        }

        // Ensure the Image is set to preserve its aspect ratio and fill appropriately
        image.preserveAspect = true;
    }

    void CreateOutlineSprite()
    {
        // Get the current RectTransform size (width and height from your screenshot: 1x1 units)
        RectTransform rectTransform = GetComponent<RectTransform>();
        float width = rectTransform.sizeDelta.x;
        float height = rectTransform.sizeDelta.y;

        // Create a new texture for the outline (simple 32x32 pixels for demonstration, scale as needed)
        int textureSize = 32; // You can adjust this for resolution
        Texture2D texture = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);
        Color[] pixels = new Color[textureSize * textureSize];

        // Fill the texture with transparency (alpha = 0)
        for (int y = 0; y < textureSize; y++)
        {
            for (int x = 0; x < textureSize; x++)
            {
                pixels[y * textureSize + x] = new Color(0, 0, 0, 0); // Transparent by default
            }
        }

        // Draw the outline (border) of the square
        int borderWidth = (int)(outlineWidth * (textureSize / Mathf.Max(width, height))); // Scale outline width
        for (int y = 0; y < textureSize; y++)
        {
            for (int x = 0; x < textureSize; x++)
            {
                // Check if this pixel is on the border (outline)
                bool isBorder = (x < borderWidth || x >= textureSize - borderWidth || 
                                y < borderWidth || y >= textureSize - borderWidth);
                if (isBorder)
                {
                    pixels[y * textureSize + x] = outlineColor; // Set outline color
                }
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();

        // Create a sprite from the texture
        outlineSprite = Sprite.Create(texture, new Rect(0, 0, textureSize, textureSize), 
                                    new Vector2(0.5f, 0.5f), 100f);
    }

    void OnDestroy()
    {
        // Clean up the sprite and texture to avoid memory leaks
        if (outlineSprite != null)
        {
            Destroy(outlineSprite.texture);
            Destroy(outlineSprite);
        }
    }
}