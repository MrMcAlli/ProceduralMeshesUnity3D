using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int height = 256;
    public int width = 256;

    public float scale = 20f;
    Renderer matRenderer;

    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GeneratedTexture();
    }

    Texture2D GeneratedTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        //Generating perlin noise map

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = GeneratedColor(x,y);
                texture.SetPixel(x,y,color);
            }
        }

        texture.Apply(); //Very important
        return texture;
    }

    Color GeneratedColor(int x, int y)
    {
        //mathf.clamp?
        float xCoord = (float)x /width * scale;
        float yCoord = (float)y / height * scale;

        float samplePos = Mathf.PerlinNoise(xCoord,yCoord);
        return new Color(samplePos,samplePos,samplePos);
    }
}
