using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class MovementScript : MonoBehaviour
{
    private GameObject _currentCamera;
    public List<string> requirements = new List<string>();
    public List<GameObject> combatRequirements = new List<GameObject>();
    
    private GameObject items;

    public GameObject nextCamera;

    private bool _cameraZoom = false;
    private float _originalRotation;

    void Start()
    {
        GetComponent<Button>().interactable = true;
        _currentCamera = transform.parent.transform.parent.gameObject;
        items = GameObject.FindGameObjectWithTag("Items");
        
        if (nextCamera == null) 
        {
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (_cameraZoom) 
        {
            if (transform.gameObject.name == "Up") 
            {
                if (_currentCamera.GetComponent<CinemachineCamera>().Lens.FieldOfView >= 40) {
                    _currentCamera.GetComponent<CinemachineCamera>().Lens.FieldOfView -= Time.deltaTime * 7.5f;

                    if (transform.gameObject.name == "Right") {
                        _currentCamera.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,
                            _currentCamera.GetComponent<RectTransform>().rotation.eulerAngles.y + Time.deltaTime * 15, 0);
                    }

                    else if (transform.gameObject.name == "Left") {
                        _currentCamera.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,
                            _currentCamera.GetComponent<RectTransform>().rotation.eulerAngles.y - Time.deltaTime * 15, 0);
                    }

                    if (_currentCamera.GetComponent<CinemachineCamera>().Lens.FieldOfView >= 60) {
                        _cameraZoom = false;
                    }
                }
            }
            
            else if (transform.gameObject.name == "Down") 
            {
                if (_currentCamera.GetComponent<CinemachineCamera>().Lens.FieldOfView >= 60) {
                    _currentCamera.GetComponent<CinemachineCamera>().Lens.FieldOfView += Time.deltaTime * 7.5f;

                    if (transform.gameObject.name == "Right") {
                        _currentCamera.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,
                            _currentCamera.GetComponent<RectTransform>().rotation.eulerAngles.y + Time.deltaTime * 15, 0);
                    }

                    else if (transform.gameObject.name == "Left") {
                        _currentCamera.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,
                            _currentCamera.GetComponent<RectTransform>().rotation.eulerAngles.y - Time.deltaTime * 15, 0);
                    }

                    if (_currentCamera.GetComponent<CinemachineCamera>().Lens.FieldOfView >= 80) {
                        _cameraZoom = false;
                    }
                }
            }
            
            else if (transform.gameObject.name == "Left") 
            {
                if (_currentCamera.GetComponent<CinemachineCamera>().Lens.FieldOfView >= 40) {
                    _currentCamera.GetComponent<CinemachineCamera>().Lens.FieldOfView -= Time.deltaTime * 15;

                    if (transform.gameObject.name == "Right") {
                        _currentCamera.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,
                            _currentCamera.GetComponent<RectTransform>().rotation.eulerAngles.y + Time.deltaTime * 15, 0);
                    }

                    else if (transform.gameObject.name == "Left") {
                        _currentCamera.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,
                            _currentCamera.GetComponent<RectTransform>().rotation.eulerAngles.y - Time.deltaTime * 15, 0);
                    }

                    if (_currentCamera.GetComponent<CinemachineCamera>().Lens.FieldOfView >= 60) {
                        _cameraZoom = false;
                    }
                }    
            } 
            
            else if (transform.gameObject.name == "Right") 
            {
                if (_currentCamera.GetComponent<CinemachineCamera>().Lens.FieldOfView >= 40) {
                    _currentCamera.GetComponent<CinemachineCamera>().Lens.FieldOfView -= Time.deltaTime * 15;

                    if (transform.gameObject.name == "Right") {
                        _currentCamera.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,
                            _currentCamera.GetComponent<RectTransform>().rotation.eulerAngles.y + Time.deltaTime * 15, 0);
                    }

                    else if (transform.gameObject.name == "Left") {
                        _currentCamera.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,
                            _currentCamera.GetComponent<RectTransform>().rotation.eulerAngles.y - Time.deltaTime * 15, 0);
                    }

                    if (_currentCamera.GetComponent<CinemachineCamera>().Lens.FieldOfView >= 60) {
                        _cameraZoom = false;
                    }
                }   
            } 
        }
    }
    
    public void Movement()
    {
        if (GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>().fadeComplete) {
            // GetComponent<Button>().interactable = false;
            _originalRotation = _currentCamera.GetComponent<RectTransform>().rotation.eulerAngles.y;

            if (requirements.Count > 0) {
                // Fade to black, instant transition to next camera
                foreach (string requirement in requirements) {
                    if (items.GetComponent<Items>().items.Contains(requirement)) {
                        Debug.Log("Requirement conditions met");

                        GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>().StartFade();
                        _cameraZoom = true;
                        Invoke(nameof(NextCamera), 1f);
                    }

                    else {
                        Debug.Log("Requirement conditions not met");
                        break;
                    }
                }
            }

            else if (combatRequirements.Count > 0) {
                foreach (GameObject combatEncounter in combatRequirements) {
                    if (combatEncounter == null) {
                        // Debug.Log("Combat requirement conditions met");

                        GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>().StartFade();
                        _cameraZoom = true;

                        //_mainCamera.GetComponent<CinemachineBrain>().
                        Invoke(nameof(NextCamera), 1f);
                    }

                    else {
                        Debug.Log("Combat requirement conditions not met");
                        break;
                    }
                }
            }

            else {
                // Debug.Log("No Requirements");

                GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>().StartFade();
                _cameraZoom = true;
                Invoke(nameof(NextCamera), 1f);
            }
        }

    }

    void NextCamera()
    {
        _currentCamera.GetComponent<CinemachineCamera>().Lens.FieldOfView = 60;
        _currentCamera.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, _originalRotation, 0);
        _cameraZoom = false;
        transform.parent.parent.gameObject.SetActive(false);
        nextCamera.SetActive(true);
        GetComponent<Button>().interactable = true;
        //nextCamera.GetComponent<CinemachineCamera>().ForceCameraPosition(nextCamera.transform.position);
        GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().FindCurrentActiveScreen();
    }
}
