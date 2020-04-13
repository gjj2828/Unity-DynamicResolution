using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class DynamicResolutionTest : MonoBehaviour
{
    public Slider slider;
    public Text infoText;
    public Text scaleText;

    //private const float SCALE_MIN = 0.2f;
    //private const float SCALE_MAX = 1.0f;
    private const float ESP = 0.01f;

    private Camera mCamera;
    private int mPixelWidth, mPixelHeight, mScaledPixelWidth, mScaledPixelHeight;
    private float mScale = 1.0f;

    // Start is called before the first frame update
    void Start() {
        mCamera = GetComponent<Camera>();
        mScale = slider.value;
        ScalableBufferManager.ResizeBuffers(mScale, mScale);
        slider.onValueChanged.AddListener(OnScaleChanged);
    }

    // Update is called once per frame
    void Update() {
        bool resize = false;
        if(resize
            || mCamera.pixelWidth != mPixelWidth
            || mCamera.pixelHeight != mPixelHeight
            || mCamera.scaledPixelWidth != mScaledPixelWidth
            || mCamera.scaledPixelHeight != mScaledPixelHeight) {
            mPixelWidth = mCamera.pixelWidth;
            mPixelHeight = mCamera.pixelHeight;
            mScaledPixelWidth = mCamera.scaledPixelWidth;
            mScaledPixelHeight = mCamera.scaledPixelHeight;
            infoText.text = string.Format("pixel: {0}x{1}\nscaledPixel: {2}x{3}" +
                "\nscale: {4:F2}\nscaleFactor: {5:F2}x{6:F2}"
                , mPixelWidth, mPixelHeight, mScaledPixelWidth, mScaledPixelHeight, mScale
                , ScalableBufferManager.widthScaleFactor, ScalableBufferManager.heightScaleFactor);
            scaleText.text = mScale.ToString("F2");
        }
    }

    void OnScaleChanged(float scale) {
        if(Mathf.Abs(scale - mScale) > ESP) {
            mScale = scale;
            ScalableBufferManager.ResizeBuffers(mScale, mScale);
        }
    }
}
