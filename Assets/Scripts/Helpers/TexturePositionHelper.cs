using UnityEngine;

public static class TexturePositionHelper
{
    public static float GetSignalValue01AtX(RenderTexture renderTexture, float x01)
    {
        int width = renderTexture.width;
        int height = renderTexture.height;

        Color[] pixels = ToTexture2D(renderTexture).GetPixels();

        int columnIndexToCheck = Mathf.FloorToInt(x01 * width);
        int y = 0;

        for(int rowIndex = height - 1; rowIndex >= 0; rowIndex--)
        {
            Color pixel = pixels[width * rowIndex + columnIndexToCheck];

            if(pixel.r > 0.9f)
            {
                y = rowIndex;

                break;
            }
        }

        float y01 = (float)y / height;

        return y01;
    }

    private static Texture2D ToTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new(rTex.width, rTex.height, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();

        return tex;
    }
}
