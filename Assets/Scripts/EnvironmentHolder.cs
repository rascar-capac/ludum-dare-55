using UnityEngine;

public class EnvironmentHolder : MonoBehaviour
{
    public Transform Origin => _origin;
    public Bounds Platform => _platform.bounds;
    public ButtonBox  ButtonBox => _buttonBox;
    public SpriteRenderer Signal => _signal;
    public SpriteRenderer Fist => _fist;
    public Vector2 FistUpPosition {get; private set;}
    public Vector2 FistDownPosition => _fistDownPosition.position;

    [SerializeField] private Transform _origin;
    [SerializeField] private SpriteRenderer _platform;
    [SerializeField] private ButtonBox _buttonBox;
    [SerializeField] private SpriteRenderer _signal;
    [SerializeField] private RenderTexture _signalRenderTexture;
    [SerializeField] private SpriteRenderer _fist;
    [SerializeField] private Transform _fistDownPosition;

    public float GetSignalIntensity01()
    {
        return GetSignalValue01AtX(_signalRenderTexture, 0.5f);
    }

    public float GetEllipsisFactor()
    {
        return Platform.extents.y / Platform.extents.x;
    }

    public Vector2 GetEllipsisVector(Vector2 originalVector)
    {
        return new Vector2(originalVector.x, originalVector.y * GetEllipsisFactor());
    }

    private void Awake()
    {
        FistUpPosition = _fist.transform.position;
    }

    private void LateUpdate()
    {
        Graphics.Blit(Game.Environment.Signal.sprite.texture, _signalRenderTexture,  Game.Environment.Signal.material);
    }

    private void OnApplicationQuit()
    {
        _signalRenderTexture.Release();
    }

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
