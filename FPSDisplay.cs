using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    [Header("Configurations")]
    [SerializeField] private bool _isBackgroundActive = true;

    [Header("Configurations - Fonts")]
    [SerializeField] private Color _fontColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
    [SerializeField] private int _fontSize = 50;

    [Header("Configurations - Background")]
    [SerializeField] private Texture _backgroundTexture;
    [SerializeField] private float _heightPercent = 2.5f;
    [SerializeField] private float _widthPercent = 30;
    [SerializeField] private Color _backgroundColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    [Header("Configurations - Position")]
    [SerializeField] private RectPosition rectPosition;

    private float deltaTime = 0.0f;
    
    enum RectPosition
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        float widthRect = w / 100 * _widthPercent;
        float heightRect = h / 100 * _heightPercent;
        float posX = w - widthRect;
        float posy = h - heightRect;

        GUIStyle style = new GUIStyle();
        Rect rect = new Rect();

        // Rect Position
        switch (rectPosition)
        {
            case RectPosition.TopLeft:
                rect = new Rect(0, 0, widthRect, heightRect);
                style.alignment = TextAnchor.UpperLeft;
                break;
            case RectPosition.TopRight:
                rect = new Rect(posX, 0, widthRect, heightRect);
                style.alignment = TextAnchor.UpperRight;
                break;
            case RectPosition.BottomLeft:
                rect = new Rect(0, posy, widthRect, heightRect);
                style.alignment = TextAnchor.UpperLeft;
                break;
            case RectPosition.BottomRight:
                rect = new Rect(posX, posy, widthRect, heightRect);
                style.alignment = TextAnchor.UpperRight;
                break;
            default:
                break;
        }

        // Background
        if (_isBackgroundActive)
        {
            GUI.color = _backgroundColor;
            GUI.DrawTexture(rect, _backgroundTexture);
        }

        // Style
        style.fontSize = _fontSize;
        style.normal.textColor = _fontColor;
        
        // Text
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);

        GUI.Label(rect, text, style);
        
    }
}
