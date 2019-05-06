using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeImage : MonoBehaviour
{
    private int maxSize = 500;

    public Texture2D ScaleTexture(Texture2D source)
    {
        Vector2 size = ScaleSize(source.width, source.height);
        
        int targetWidth = (int)size.x;
        int targetHeight = (int)size.y;

        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);

        Color[] pixels = result.GetPixels();

        float incX = (1.0f / (float)targetWidth);
        float incY = (1.0f / (float)targetHeight);

        for(int px = 0; px < pixels.Length; px++)
        {
            pixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * ((float)Mathf.Floor(px / targetWidth)));
        }

        result.SetPixels(pixels, 0);

        result.Apply();

        return result;
    }

    private Vector2 ScaleSize(int txtWidth, int txtHeight)
    {
        Vector2 result;
        float einsEntspricht = 0;

        if(txtWidth > txtHeight)
        {
            einsEntspricht = (float)maxSize / txtWidth;
            return result = new Vector2(maxSize, txtHeight * einsEntspricht);
        }
        else if(txtWidth > txtHeight)
        {
            einsEntspricht = (float)maxSize / txtHeight;
            return result = new Vector2(txtWidth * einsEntspricht, maxSize);
        }
        else
        {
            return result = new Vector2(maxSize, maxSize);
        }
    }
}
