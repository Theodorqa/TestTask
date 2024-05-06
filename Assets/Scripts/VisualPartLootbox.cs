using AxGrid.Base;
using UnityEngine;
using UnityEngine.UI;

public class VisualPartLootbox : MonoBehaviourExt
{
    public static VisualPartLootbox Instance { get; private set; }

    [SerializeField] private RectTransform imagePrefab;
    [SerializeField] private RectTransform mask;
    [SerializeField] private float maxScrollSpeed = 200f; 
    [SerializeField] private float acceleration = 20f; 
    [SerializeField] private float stopSmoothness = 10f; 

    [SerializeField] private Color[] imageColors;

    private bool isScrolling = false;
    private Image[] images;
    private float maskHeight;
    private float imageHeight;
    private int currentIndex = 0;
    private float currentSpeed = 0f;

    [OnAwake]
    private void AwakeThis()
    {
        Instance = this;
        maskHeight = mask.rect.height;
        imageHeight = imagePrefab.rect.height;
        
        images = new Image[3];
        for (int i = 0; i < images.Length; i++)
        {
            images[i] = Instantiate(imagePrefab, mask).GetComponent<Image>();
            images[i].rectTransform.anchoredPosition = new Vector2(0f, i * imageHeight);
            images[i].color = imageColors[i % imageColors.Length]; 
        }
    }

    [OnUpdate]
    private void UpdateThis()
    {
        if (isScrolling)
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxScrollSpeed);
            
            foreach (var image in images)
            {
                image.rectTransform.anchoredPosition -= new Vector2(0f, currentSpeed * Time.deltaTime);
                
                if (image.rectTransform.anchoredPosition.y <= -imageHeight)
                {
                    image.rectTransform.anchoredPosition += new Vector2(0f, imageHeight * images.Length);
                    currentIndex++;
                    if (currentIndex >= images.Length) currentIndex = 0;
                }
            }
        }
        else
        {
            if (currentSpeed > 0f)
            {
                float deceleration = stopSmoothness * currentSpeed * Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed - deceleration, 0f);
            }
        }
    }
    
    public void StartScrolling()
    {
        isScrolling = true;
    }
    
    public void StopScrolling()
    {
        isScrolling = false;
    }
}
