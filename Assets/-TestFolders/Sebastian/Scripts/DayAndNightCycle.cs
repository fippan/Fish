using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightCycle : MonoBehaviour {

    public int _days;                                   //Defines naming convention for the days
    public int _hours;                                  //Defines naming convention for the hours
    public int _minutes;                                //Defines naming convention for the minutes
    public int _seconds;                                //Defines naming convention for the seconds
    public float _counter;                              //Defines naming convention for the counter

    public int _dawnStartTime = 6;                      //Defines dawn start
    public int _dayStartTime = 8;                       //Defines day start
    public int _duskStartTime = 18;                     //Defines dusk start
    public int _nightStartTime = 20;                    //Defines night start

    public float _sunDimTime = 0.01f;                   //speed at which sun dims
    public float _dawnSunIntensity = 0.5f;              //Dawn sun strength
    public float _daySunIntensity = 1f;                 //Day sun strength
    public float _duskSunIntensity = 0.25f;             //Dusk sun strength
    public float _nightSunIntensity = 0f;               //Night sun strength

    public float _ambientDimTime = 0.0001f;             //Defines speed at which ambient light is adjusted
    public float _dawnAmbientIntensity = 0.5f;          //Dawn ambient strength
    public float _dayAmbientIntensity = 1f;             //Day ambient strength
    public float _duskAmbientIntensity = 0.25f;         //Dusk ambient strength
    public float _nightAmbientIntensity = 0f;           //Dawn ambient strength

    public float _dawnSkyboxBlendFactor = 0.5f;         //Defines dawn skybox blend value
    public float _daySkyboxBlendFactor = 1f;            //Defines day skybox blend value
    public float _duskSkyboxBlendFactor = 0.25f;        //Defines dusk skybox blend value
    public float _nightSkyboxBlendFactor = 0f;          //Defines night skybox blend value

    public float _skyboxBlendFactor;                    //Defines the current skybox blend value
    public float _skyboxBlendSpeed = 0.01f;             //Defines Speed at which the skybox will blend

    public int _guiWidth = 100;                         //Defines Gui label width
    public int _guiHeight = 20;                         //Defines Gui label width


    public DayPhases _dayPhases;                        //Defines naming convention for the phases of the day

    public enum DayPhases                               //enums for day phases
    {
        Dawn,
        Day,
        Dusk,
        Night
    }

    void Awake()
    {
        _dayPhases = DayPhases.Night;                   //Set day Phase to night on start up
        RenderSettings.ambientIntensity = _nightAmbientIntensity; //RenderSettings ambient intensity is equal to night startup

        GetComponent<Light>().intensity = _nightSunIntensity; //Set sun intensity to night on start up


    }

    // Use this for initialization
    void Start ()
    {
        StartCoroutine("TimeOfDayFiniteStateMachine");  //Start TimeOfDayFiniteStateMachine on start up



        _hours = 5;                                     //hours equals five on start up
        _minutes = 59;                                  //minutes equals fifty nine on start up
        _counter = 59;                                  //Counter equals fifty nine on start up


        _days = 1;                                      //Days equals one on start up
    }
	
	// Update is called once per frame
	void Update ()
    {
            SecondsCounter();                           //Call SecondsCounter function
            UpdateSkybox();                             //Call UpdateSkybox function
    }

    IEnumerator TimeOfDayFiniteStateMachine()
    {
        while (true)
        {
            switch(_dayPhases)
            {
                case DayPhases.Dawn:
                    Dawn();
                    break;
                case DayPhases.Day:
                    Day();
                    break;
                case DayPhases.Dusk:
                    Dusk();
                    break;
                case DayPhases.Night:
                    Night();
                    break;
            }
            yield return null;
        }

    }

    void SecondsCounter()
    {
        Debug.Log("SecondsCounter");

        if (_counter == 60)                             //if the counter equal 60
            _counter = 0;                               //then make counter equal to 0

        _counter += Time.deltaTime * 1000;                     //counter plus time sync to pc speed

        _seconds = (int)_counter;                       //seconds equals counter cast to an int 

        if (_counter < 60)                              //if counter is less than 60
            return;

        if (_counter > 60)                              //if counter is greater than 60
            _counter = 60;                              //then make counter equal to 60

        if (_counter == 60)                              //if counter is equal to 60
            MinutesCounter();                            //call MinutesCounter function
    }

    void MinutesCounter()
    {
        Debug.Log("MiniteCounter");

        _minutes++;                                     //increase minutes counter

        if (_minutes == 60)                             //if minutes counter equals sixty
        {
            HoursCounter();                             //call Hours counter function
            _minutes = 0;                               //and then mae minutes equal zero
        }


    }

    void HoursCounter()
    {
        Debug.Log("HoursCounter");

        _hours++;

        if (_hours == 24)                              //if hours counter equals twentyfour hour
        {
            DaysCounter();                             //call DayCounter function
            _hours = 0;                                //and then make hours equal zero
        }

    }

    void DaysCounter()
    {
        Debug.Log("DaysCounter");
        _days++;                                        //increase days counter

    }

    void Dawn()
    {
        Debug.Log("Dawn");

        DawnSunLightManager();                                      //Call DawnSunlightManager function
        DawnAmbientLightManager();                                  //Call DawnAmbientLightManager function

        if (_hours == _dayStartTime && _hours < _duskStartTime)//if hours equas day start time AND hours is still less thank dusk start time
        {
            _dayPhases = DayPhases.Day;                 //Set day Phase to day
        }
    }

    void DawnSunLightManager()
    {
        Debug.Log("DawnSunLightManager");

        if (GetComponent<Light>().intensity == _dawnSunIntensity)       //if light intensity is equal to dawn intensity
            return;                                                     //then do nothing


        if (GetComponent<Light>().intensity < _dawnSunIntensity) //if sun intensity is less than dawn
            GetComponent<Light>().intensity += _sunDimTime * Time.deltaTime; //then increase the intensity by the sun dim time

        if (GetComponent<Light>().intensity > _dawnSunIntensity)    //if intensity is greater than dawn
            GetComponent<Light>().intensity = _dawnSunIntensity;    //then make intensity equal to dawn
    }

    void DawnAmbientLightManager()
    {
        Debug.Log("DawnSunLightManager");

        if (RenderSettings.ambientIntensity == _dawnAmbientIntensity)       //if ambient intensity is equal to dawn ambient intensity
            return;

        if (RenderSettings.ambientIntensity < _dawnAmbientIntensity) //if ambient intensity is less than dawn
            RenderSettings.ambientIntensity += _ambientDimTime * Time.deltaTime; //then increase the intensity by the ambient dim time

        if (RenderSettings.ambientIntensity > _dawnAmbientIntensity) //if ambient intensity is greater than dawn
            RenderSettings.ambientIntensity = _dawnAmbientIntensity; //then make ambient intensity equal to dawn


    }

    void Day()
    {
        Debug.Log("Day");

        DaySunLightManager();                                       //Call DaySunLightManager
        DayAmbientLightManager();                                   //Call DayAmbientLightManager

        if (_hours == _duskStartTime && _hours < _nightStartTime)//if hours equas dusk start time AND hours is still less thank night start time
        {
            _dayPhases = DayPhases.Dusk;                 //Set day Phase to dusk
        }
    }

    void DaySunLightManager()
    {
        Debug.Log("DaySunLightManager");

        if (GetComponent<Light>().intensity == _daySunIntensity)       //if light intensity is equal to day intensity
            return;                                                     //then do nothing

        if (GetComponent<Light>().intensity < _daySunIntensity) //if sun intensity is less than dawn
            GetComponent<Light>().intensity += _sunDimTime * Time.deltaTime; //then increase the intensity by the sun dim time

        if (GetComponent<Light>().intensity > _daySunIntensity)     //if intensity is greater than day
            GetComponent<Light>().intensity = _daySunIntensity;     //then make intensity equal to day


    }

    void DayAmbientLightManager()
    {
        Debug.Log("DaySunLightManager");

        if (RenderSettings.ambientIntensity == _dayAmbientIntensity)       //if ambient intensity is equal to dawn ambient intensity
            return;

        if (RenderSettings.ambientIntensity < _dayAmbientIntensity) //if ambient intensity is less than day
            RenderSettings.ambientIntensity += _ambientDimTime * Time.deltaTime; //then increase the intensity by the ambient dim time

        if (RenderSettings.ambientIntensity > _dayAmbientIntensity) //if ambient intensity is greater than day
            RenderSettings.ambientIntensity = _dayAmbientIntensity; //then make ambient intensity equal to day


    }

    void Dusk()
    {
        Debug.Log("Dusk");

        DuskSunLightManager();
        DuskAmbientLightManager();



        if (_hours == _nightStartTime)                  //if hours equas dusk start time
        {
            _dayPhases = DayPhases.Night;                 //Set day Phase to night
        }
    }

    void DuskSunLightManager()
    {
        Debug.Log("DuskSunLightManager");

        if (GetComponent<Light>().intensity == _duskSunIntensity)       //if light intensity is equal to dusk intensity
            return;                                                     //then do nothing

        if (GetComponent<Light>().intensity > _duskSunIntensity) //if sun intensity is greater than dusk
            GetComponent<Light>().intensity -= _sunDimTime * Time.deltaTime; //then increase the intensity by the sun dim time

        if (GetComponent<Light>().intensity > _duskSunIntensity)            //if intensity is greater than dusk
            GetComponent<Light>().intensity = _duskSunIntensity;            //then make intensity equal to dusk


    }

    void DuskAmbientLightManager()
    {
        Debug.Log("DuskSunLightManager");

        if (RenderSettings.ambientIntensity == _duskAmbientIntensity)       //if ambient intensity is equal to dusk ambient intensity
            return;

        if (RenderSettings.ambientIntensity < _duskAmbientIntensity) //if ambient intensity is less than dusk
            RenderSettings.ambientIntensity -= _ambientDimTime * Time.deltaTime; //then increase the intensity by the ambient dim time

        if (RenderSettings.ambientIntensity > _duskAmbientIntensity) //if ambient intensity is greater than dusk
            RenderSettings.ambientIntensity = _duskAmbientIntensity; //then make ambient intensity equal to dusk



    }


    void Night()
    {
        Debug.Log("Night");

        NightSunLightManager();                                         //Call function NightSunManager
        NightAmbientLightManager();                                     //Call function NightAmbientLightManager

        if (_hours == _dawnStartTime && _hours < _dayStartTime)         //if hours equas dawn start time AND hours is still less thank day start time
        {
            _dayPhases = DayPhases.Dawn;                                //Set day Phase to dawn
        }
    }

    void NightSunLightManager()
    {
        Debug.Log("NightSunLightManager");

        if (GetComponent<Light>().intensity == _nightSunIntensity)       //if light intensity is equal to night intensity
            return;                                                      //then do nothing

        if (GetComponent<Light>().intensity > _nightSunIntensity)        //if sun intensity is less than night
            GetComponent<Light>().intensity -= _sunDimTime * Time.deltaTime; //then increase the intensity by the sun dim time

        if (GetComponent<Light>().intensity > _nightSunIntensity)       //if intensity is greater than night
            GetComponent<Light>().intensity = _nightSunIntensity;       //then make intensity equal to night


    }

    void NightAmbientLightManager()
    {
        Debug.Log("NightSunLightManager");

        if (RenderSettings.ambientIntensity == _nightAmbientIntensity)       //if ambient intensity is equal to night ambient intensity
            return;

        if (RenderSettings.ambientIntensity > _nightAmbientIntensity) //if ambient intensity is less than night
            RenderSettings.ambientIntensity -= _ambientDimTime * Time.deltaTime; //then increase the intensity by the ambient dim time

        if (RenderSettings.ambientIntensity > _nightAmbientIntensity)  //if ambient intensity is greater than night
            RenderSettings.ambientIntensity = _nightAmbientIntensity; //then make ambient intensity equal to night



    }


    void OnGUI()
    {
        //Create GUI Label to display number of days
        GUI.Label(new Rect(Screen.width- 50, 5,_guiWidth, _guiHeight), "Day " + _days);

        //if minutes is less than ten display our clock with extra zero
        if(_minutes < 10)
        {
            GUI.Label(new Rect(Screen.width - 50, 25, _guiWidth, _guiHeight), _hours + ":" + 0 + _minutes + ":" + _seconds);
        }

        //else just display our clock
        else
            GUI.Label(new Rect(Screen.width - 50, 25, _guiWidth, _guiHeight), _hours + ":" + _minutes + ":" + _seconds);
    }

    private void UpdateSkybox()
    {
        Debug.Log("UpdateSkybox");

        if(_dayPhases == DayPhases.Dawn)                             //if day phase is equal to dawn
        {
            if (_skyboxBlendFactor == _dawnSkyboxBlendFactor)        //if skybox blend equals dawn
                return;                                              //then do nothing and return

            _skyboxBlendFactor += _skyboxBlendSpeed * Time.deltaTime;//Increase skybox blend by blend speed

            if (_skyboxBlendFactor > _dawnSkyboxBlendFactor)         //if skybox blend factor is greater than dawn
                _skyboxBlendFactor = _dawnSkyboxBlendFactor;         //then make skybox blend factor equal to dawn


        }


        if (_dayPhases == DayPhases.Day)                             //if day phase is equal to day
        {
            if (_skyboxBlendFactor == _daySkyboxBlendFactor)         //if skybox blend equals day
                return;                                              //then do nothing and return

            _skyboxBlendFactor += _skyboxBlendSpeed * Time.deltaTime;//Increase skybox blend by blend speed

            if (_skyboxBlendFactor > _daySkyboxBlendFactor)         //if skybox blend factor is greater than day
                _skyboxBlendFactor = _daySkyboxBlendFactor;         //then make skybox blend factor equal to day



        }


        if (_dayPhases == DayPhases.Dusk)                            //if day phase is equal to dusk
        {
            if (_skyboxBlendFactor == _duskSkyboxBlendFactor)        //if skybox blend equals dusk
                return;                                              //then do nothing and return

            _skyboxBlendFactor -= _skyboxBlendSpeed * Time.deltaTime;//Decrease skybox blend by blend speed

            if (_skyboxBlendFactor < _duskSkyboxBlendFactor)         //if skybox blend factor is less than dusk
                _skyboxBlendFactor = _duskSkyboxBlendFactor;         //then make skybox blend factor equal to dusk



        }


        if (_dayPhases == DayPhases.Night)                            //if day phase is equal to night
        {
            if (_skyboxBlendFactor == _nightSkyboxBlendFactor)        //if skybox blend equals night
                return;                                               //then do nothing and return

            _skyboxBlendFactor -= _skyboxBlendSpeed * Time.deltaTime; //Decrease skybox blend by blend speed

            if (_skyboxBlendFactor < _nightSkyboxBlendFactor)         //if skybox blend factor is greater than night
                _skyboxBlendFactor = _nightSkyboxBlendFactor;         //then make skybox blend factor equal to night



        }
        RenderSettings.skybox.SetFloat("_Blend", _skyboxBlendFactor); //Get render for skybox and set the box bor the blend
    }

}
