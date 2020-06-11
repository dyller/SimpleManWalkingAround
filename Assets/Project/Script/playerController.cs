using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.Vehicles.Car;
using Random = UnityEngine.Random;

public class playerController : MonoBehaviour
{
    private CoinModel coinModel;
    private String nameOfCamera = "Camera";
    private Vector3 walkingCameraPostion = new Vector3(-0.65f, 4f, -5.00f);
    private Vector3 carCameraPostion = new Vector3(-0f, 7.21f, -11.25f);
    public int rangeCar = 1;
    public GameObject Player;
    public GameObject Car;
    private Boolean insideCar = false;
    public GameObject mainCamera;
    public CarController m_Car; // the car controller we want to use

    // Start is called before the first frame update
    void Start()
    {
        coinModel = CoinModel.getInstance();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if(!insideCar)
            Player.transform.localEulerAngles = new Vector3(Player.transform.localEulerAngles.x, Player.transform.localEulerAngles.y, 0);
        if (Input.GetKeyDown(KeyCode.Return) && !coinModel.IsGamePaused())
        {
            if (!insideCar)
                EnterCar(Player.transform.position, rangeCar);
            else
                MoveOutSideCar();
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            PauseGame();
        }
        if(coinModel.IsGamePaused())
        {
            ThirdPersonCharacter thirdPersonCharacter = Player.GetComponent<ThirdPersonCharacter>();
            thirdPersonCharacter.Move(new Vector3(0, 0, 0), false, false);
            CarController carController = Car.GetComponent<CarController>();
            if (m_Car.CurrentSpeed > 0.2)
            {
                //decrease car speed
                m_Car.Move(0, 0, 0f, 20f);
            }
        }
    }

   

    private void PauseGame()
    {
        if (!coinModel.IsGamePaused())
        {
            coinModel.PauseGame();
            Player.GetComponent<ThirdPersonUserControl>().enabled = false;
            
            Car.GetComponent<CarUserControl>().enabled = false;
            Car.GetComponent<CarAudio>().enabled = false;
        }
        else 
        {
            Player.GetComponent<ThirdPersonUserControl>().enabled = true;
            coinModel.PauseGame();
            ChangeCarVariables(insideCar);
            // to remove handbreak from car
            m_Car.Move(0, 0, -1f, 0f);
        }
        
    }

    // Move body to car + move camera to player
    private void MoveOutSideCar()
    {
        ChangeCarVariables(!insideCar);
        Vector3 carPostion = Car.transform.position;
        Player.transform.position = new Vector3(carPostion.x-1,carPostion.y,carPostion.z);
        Player.transform.rotation = Car.transform.rotation;
        // Move camer to player
       
        Camera.main.transform.localPosition = walkingCameraPostion;
        Camera.main.transform.localEulerAngles = new Vector3(30, 0, 0);
       
        mainCamera.transform.SetParent(Player.transform);
        insideCar = false;
    }

    // Check if player is able to enter car
    void EnterCar(Vector3 playerPostion, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(playerPostion, radius);
        foreach (Collider hit in hitColliders)
        {

            Collider col = hit.GetComponent<Collider>();
            if (col.gameObject.name.Contains("Car")) {
                ChangeCarVariables(!insideCar);
                insideCar = true;
                // Move camer to car
                
                mainCamera.transform.SetParent(Car.transform);
                Camera.main.transform.localPosition = carCameraPostion;
                Camera.main.transform.localEulerAngles = new Vector3(30,0,0);

                // to remove handbreak from car
                m_Car.Move(0,  0, -1f, 0f);
                break;
               
            }


        }
    }

    private void ChangeCarVariables(Boolean outsideCar)
    {
        Player.SetActive(!outsideCar);
        Car.GetComponent<CarUserControl>().enabled = outsideCar;
        Car.GetComponent<CarAudio>().enabled = outsideCar;
        //Car.transform.Find(nameOfCamera).gameObject.SetActive(outsideCar);
    }
    private void FixedUpdate()
    {
        if (!insideCar)
        {
            if (m_Car.CurrentSpeed > 0.2)
            {
                //decrease car speed
                m_Car.Move(0, 0, 0f, 20f);
            }
        }

    }

}
