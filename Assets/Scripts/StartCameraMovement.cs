using AxGrid;
using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class StartCameraMovement : MonoBehaviourExt
{
    [Header("Объекты, которые нужно включить при начале игры")]
    [Space]
    [SerializeField] private GameObject fsmObject;
    [SerializeField] private GameObject mainCanvas;
    
    [Space]
    
    [Header("Точки для камеры")]
    [Space]
    [SerializeField] private Transform cameraTransform; 
    [SerializeField] private Transform targetPosition;
    
    private const float TimeAnimation = 2f;
    private CPath cameraPath;
    private UnityEngine.UI.Button button;

    [OnAwake]
    public void Init()
    {
        this.button = this.GetComponent<UnityEngine.UI.Button>();
        this.button.onClick.AddListener(this.OnMouseDown);
        
        cameraPath = new CPath();
    }
    
    private void OnMouseDown()
    {
        Log.Debug("Начал играть");

        cameraPath.Clear();
        
        cameraPath
            .EasingCircEaseIn(TimeAnimation, 0f, 1f, (f) =>
            {
                cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition.position, f);
                
                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetPosition.rotation, f);
            })
            .Action(() =>
            {
                Debug.Log("Камера достигла указанных координат.");
                
                EnableGameobjects();
            });
        
        MoveCamera();
    }

    [OnUpdate]
    private void MoveCamera()
    {
        if(cameraPath != null)
            cameraPath.Update(Time.deltaTime);
    }

    private void EnableGameobjects()
    {
        fsmObject.SetActive(true);
        mainCanvas.SetActive(true);
    }
}