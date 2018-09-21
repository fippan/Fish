using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayAndNightCycle : MonoBehaviour {

    public Text timerText;
    private float startTime;

    private Transform _sunPivotPoint;                   //The rotation pivot for the sun
    private int _centreOfGameWorld = 50;               //A value to define position at the centre of the scene (SET THE CENTRE VALUE OF THE TERRAIN IN THE VALUE)

    [SerializeField] private float SecondMultiplier;

    [Space(20)]

    public int _days;                                   //Defines naming convention for the days
    public int _hours;                                  //Defines naming convention for the hours
    public int _minutes;                                //Defines naming convention for the minutes
    public int _seconds;                                //Defines naming convention for the seconds
    public float _counter;                              //Defines naming convention for the counter

    public int _years;                                  //Defines naming convention for years counter
    public int _leapYearsCounter;                              //Defines naming conention for leap years
    public int _calendarDays;                           //Defines naming conention for leap years

    public bool _january;                               //Defines if we are in the month of _january
    public bool _february;                              //Defines if we are in the month of _febuary
    public bool _march;                                 //Defines if we are in the month of _march
    public bool _april;                                 //Defines if we are in the month of _april
    public bool _may;                                   //Defines if we are in the month of _may
    public bool _june;                                  //Defines if we are in the month of _june
    public bool _july;                                  //Defines if we are in the month of _july
    public bool _august;                                //Defines if we are in the month of _august
    public bool _september;                             //Defines if we are in the month of _september
    public bool _october;                               //Defines if we are in the month of _october
    public bool _november;                              //Defines if we are in the month of _november
    public bool _december;                              //Defines if we are in the month of _december

    public bool _spring;                                //Defines if we are in the month of spring
    public bool _summer;                                //Defines if we are in the month of spring
    public bool _autumn;                                //Defines if we are in the month of spring
    public bool _winter;                                //Defines if we are in the month of spring

    public float _springDayLength;                      //Defines day length during spring
    public float _summerDayLength;                      //Defines day length during summer
    public float _autumnDayLength;                      //Defines day length during autumn
    public float _winterDayLength;                      //Defines day length during winter

    public int _dawnStartTime;                          //Defines dawn start
    public int _dayStartTime;                           //Defines day start
    public int _duskStartTime;                          //Defines dusk start
    public int _nightStartTime;                         //Defines night start

    public float _dayTemp;                              //create variable for day temp
    public float _rotTemp;                              //Create variable for rotation temp

    public int _dawnSpringStartTime = 7;                //Defines Spring dawn start
    public int _daySpringStartTime = 9;                 //Defines Spring day start
    public int _duskSpringStartTime = 18;               //Defines Spring dusk start
    public int _nightSpringStartTime = 20;              //Defines Spring night start

    public int _dawnSummerStartTime = 6;                    //Defines dawn start
    public int _daySummerStartTime = 8;                     //Defines day start
    public int _duskSummerStartTime = 20;                    //Defines dusk start
    public int _nightSummerStartTime = 22;                   //Defines night start

    public int _dawnAutumnStartTime = 7;                    //Defines dawn start
    public int _dayAutumnStartTime = 9;                     //Defines day start
    public int _duskAutumnStartTime = 18;                    //Defines dusk start
    public int _nightAutumnStartTime = 20;                   //Defines night start

    public int _dawnWinterStartTime = 8;                    //Defines dawn start
    public int _dayWinterStartTime = 10;                     //Defines day start
    public int _duskWinterStartTime = 16;                    //Defines dusk start
    public int _nightWinterStartTime = 18;                   //Defines night start

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

        transform.position =                                //position is equal to
            new Vector3(                                    //a new vector 3 position
            (_centreOfGameWorld * 2),0,_centreOfGameWorld); //at this position

        transform.localEulerAngles = new Vector3(0, -90, 0);//Suns rotation is equal to these (x, x, x) values



    }

    // Use this for initialization
    void Start ()
    {
        startTime = Time.time;

        StartCoroutine("TimeOfDayFiniteStateMachine");      //Start TimeOfDayFiniteStateMachine on start up

        _hours = 5;                                         //hours equals five on start up
        _minutes = 59;                                      //minutes equals fifty nine on start up
        _counter = 59;                                      //Counter equals fifty nine on start up

        _days = 1;                                          //Days equals one on start up

        _calendarDays = 1;                                  //calendar days equal one on start up
        _october = true;                                    //start in the month of october on start up
        _autumn = true;                                     //start in the season of autumn on start up

        _dawnStartTime = _dawnAutumnStartTime;              //Dawn start time is equal to Autumn
        _dayStartTime = _dayAutumnStartTime;                //Day start time is equal to Autumn
        _duskStartTime = _duskAutumnStartTime;              //Dusk start time is equal to Autumn
        _nightStartTime = _nightAutumnStartTime;            //Night start time is equal to Autumn

        _autumnDayLength = _nightStartTime - _dawnStartTime; //autumn daylength equals night start time minus the dusk start time

        _years = 2016;                                      //years equal 2016 on startup
        _leapYearsCounter = 4;                              //leap year counter equals 4 on startup

        GameObject _sunPivotGO =                            //sun pivot game object is equal to
            GameObject.FindGameObjectWithTag("SunPivot");   //game object "SunPivot" tag

        _sunPivotPoint = _sunPivotGO.transform;             //Caches sun pivot point position

        _sunPivotPoint.transform.position =                 //Set sun pivot point
            new Vector3(                                    //to a new vector 3
                _centreOfGameWorld,0,_centreOfGameWorld);   //at this position (centre of scene)


        DiverManager.Instance.WaveCount = _days;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;

            SecondsCounter();                               //Call SecondsCounter function
            UpdateSkybox();                                 //Call UpdateSkybox function
        SunRotationManager();                               //Call SunRotationManager function

        //transform.RotateAround(Vector3.zero, Vector3.right, 10f * Time.deltaTime);
        transform.LookAt(Vector3.zero);

    }

    IEnumerator TimeOfDayFiniteStateMachine()
    {
        while (true)
        {
            switch(_dayPhases)
            {
                case DayPhases.Dawn:
                    Dawn();
                    DiverManager.Instance.WaveIsActive = false;
                    break;
                case DayPhases.Day:
                    Day();
                    break;
                case DayPhases.Dusk:
                    Dusk();
                    DiverManager.Instance.WaveCount = _days;
                    DiverManager.Instance.WaveIsActive = true;
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

        if (_counter == 60)                                 //if the counter equal 60
            _counter = 0;                                   //then make counter equal to 0


        _counter += Time.deltaTime * SecondMultiplier;      //counter plus time sync to pc speed

        _seconds = (int)_counter;                           //seconds equals counter cast to an int 

        if (_counter < 60)                                  //if counter is less than 60
            return;

        if (_counter > 60)                                  //if counter is greater than 60
            _counter = 60;                                  //then make counter equal to 60

        if (_counter == 60)                                 //if counter is equal to 60
            MinutesCounter();                               //call MinutesCounter function
    }

    void MinutesCounter()
    {
        Debug.Log("MiniteCounter");

        _minutes++;                                         //increase minutes counter

        if (_minutes == 60)                                 //if minutes counter equals sixty
        {
            HoursCounter();                                 //call Hours counter function
            _minutes = 0;                                   //and then mae minutes equal zero
        }


    }

    void HoursCounter()
    {
        Debug.Log("HoursCounter");

        _hours++;

        if (_hours == 24)                                   //if hours counter equals twentyfour hour
        {
            DaysCounter();                                  //call DayCounter function
            _hours = 0;                                     //and then make hours equal zero
        }

    }

    void DaysCounter()
    {
        Debug.Log("DaysCounter");
        _days++;                                            //increase days counter
        if(DiverManager.Instance.WaveIsActive == false)
        {
            DiverManager.Instance.WaveCount = _days;
        }
        else if(DiverManager.Instance.WaveIsActive == true)
        {
            SecondMultiplier = 0;
        }
        UpdateCalendarDays();                               //call update calendar days
    }

    void UpdateCalendarDays()
    {
        Debug.Log("UpdateCalendarMonth");
        _calendarDays++;                                    //increase calendar days
        UpdateCalendarMonth();                              //call update calendar function
    }

    void UpdateCalendarMonth()
    {
        Debug.Log("UpdateCalendarMonth");

        if (_january == true && _calendarDays > 31)         //if we are in january and calendar days is greater than 31
        {
            _january = false;                               //then set january to false
            _february = true;                               //and set february to true
            _calendarDays = 1;                              //and make calendar days equal one (first of the new month)

        }
        if (_leapYearsCounter == 4 && 
            _february == true && _calendarDays > 29)        //if leap year counter is true
        {                                                   //and we are in february and calendar days is greater than 29
            _february = false;                              //then set february to false
            _march = true;                                  //and set march to true
            _calendarDays = 1;                              //and make calendar days equal one (first of the new month)

        }
        if (_leapYearsCounter < 4 &&
        _february == true && _calendarDays > 28)            //if leap year counter is less than true
        {                                                   //and we are in february and calendar days is greater than 28
            _february = false;                              //then set february to false
            _march = true;                                  //and set march to true
            _calendarDays = 1;                              //and make calendar days equal one (first of the new month)

            SeasonManager();                                //call seasons manager function 

        }
        if (_march == true && _calendarDays > 31)           //if we are in march and calendar days is greater than 31
        {
            _march = false;                                 //then set march to false
            _april = true;                                  //and set april to true
            _calendarDays = 1;                              //and make calendar days equal one (first of the new month)

        }
        if (_april == true && _calendarDays > 30)           //if we are in april and calendar days is greater than 30
        {
            _april = false;                                 //then set april to false
            _may = true;                                    //and set may to true
            _calendarDays = 1;                              //and make calendar days equal one (first of the new month)

        }
        if (_may == true && _calendarDays > 31)             //if we are in may and calendar days is greater than 31
        {
            _may = false;                                   //then set may to false
            _june = true;                                   //and set june to true
            _calendarDays = 1;                              //and make calendar days equal one (first of the new month)

            SeasonManager();                                //call seasons manager function

        }
        if (_june == true && _calendarDays > 30)            //if we are in june and calendar days is greater than 30
        {
            _june = false;                                  //then set june to false
            _august = true;                                 //and set august to true
            _calendarDays = 1;                              //and make calendar days equal one (first of the new month)

        }
        if (_august == true && _calendarDays > 31)           //if we are in august and calendar days is greater than 31
        {
            _august = false;                                //then set august to false
            _september = true;                              //and set september to true
            _calendarDays = 1;                              //and make calendar days equal one (first of the new month)

        }
        if (_september == true && _calendarDays > 30)       //if we are in september and calendar days is greater than 30
        {
            _september = false;                             //then set september to false
            _october = true;                                //and set october to true
            _calendarDays = 1;                              //and make calendar days equal one (first of the new month)

            SeasonManager();                                //call seasons manager function 

        }
        if (_october == true && _calendarDays > 31)         //if we are in october and calendar days is greater than 31
        {
            _october = false;                               //then set october to false
            _november = true;                               //and set november to true
            _calendarDays = 1;                              //and make calendar days equal one (first of the new month)

        }
        if (_november == true && _calendarDays > 30)        //if we are in november and calendar days is greater than 30
        {
            _november = false;                              //then set november to false
            _december = true;                               //and set december to true
            _calendarDays = 1;                              //and make calendar days equal one (first of the new month)

            SeasonManager();                                //call seasons manager function

        }
        if (_december == true && _calendarDays > 31)        //if we are in december and calendar days is greater than 31
        {
            _december = false;                              //then set december to false
            _january = true;                                //and set january to true
            _calendarDays = 1;                              //and make calendar days equal one (first of the new month)

            YearCounter();                                  //call yearcounter function


        }

    }

    void YearCounter()
    {
        Debug.Log("YearCounter");

        _years++;                                           //increase years
        _leapYearsCounter++;                                //increase leap years

        if (_leapYearsCounter > 4)                          //if leapyear counter is greater than 4
            _leapYearsCounter = 1;                          //then make leap year counter equal to one

    }

    void SeasonManager()
    {
        Debug.Log("SeasonManager");

        _spring = false;                                    //Set _spring to be equal to false
        _summer = false;                                    //Set _summer to be equal to false                   
        _autumn = false;                                    //Set _autumn to be equal to false                   
        _winter = false;                                    //Set _winter to be equal to false                   

        if (_march == true && _calendarDays == 1)           //if we are in march and calendar days equal 1
        {           
            _spring = true;                                 //set spring to true

            _dawnStartTime = _dawnSpringStartTime;          //Dawn start time is equal to Spring
            _dayStartTime = _daySpringStartTime;            //Day start time is equal to Spring
            _duskStartTime = _duskSpringStartTime;          //Dusk start time is equal to Spring
            _nightStartTime = _nightSpringStartTime;        //Night start time is equal to Spring

            _springDayLength = _nightStartTime - _dawnStartTime; //spring daylength equals night start time minus the dawn start time

        }
        if (_june == true && _calendarDays == 1)            //if we are in june and calendar days equal 1
        {            
            _summer = true;                                 //set summer to true

            _dawnStartTime = _dawnSummerStartTime;          //Dawn start time is equal to Summer
            _dayStartTime = _daySummerStartTime;            //Day start time is equal to Summer
            _duskStartTime = _duskSummerStartTime;          //Dusk start time is equal to Summer
            _nightStartTime = _nightSummerStartTime;        //Night start time is equal to Summer

            _summerDayLength = _nightStartTime - _dawnStartTime; //summer daylength equals night start time minus the dawn start time


        }
        if (_september == true && _calendarDays == 1)       //if we are in september and calendar days equal 1
        {       
            _autumn = true;                                 //set autumn to true

            _dawnStartTime = _dawnAutumnStartTime;          //Dawn start time is equal to Autumn
            _dayStartTime = _dayAutumnStartTime;            //Day start time is equal to Autumn
            _duskStartTime = _duskAutumnStartTime;          //Dusk start time is equal to Autumn
            _nightStartTime = _nightAutumnStartTime;        //Night start time is equal to Autumn

            _autumnDayLength = _nightStartTime - _dawnStartTime; //autumn daylength equals night start time minus the dawn start time


        }
        if (_december == true && _calendarDays == 1)        //if we are in december and calendar days equal 1
        {       
            _winter = true;                                 //set winter to true

            _dawnStartTime = _dawnWinterStartTime;          //Dawn start time is equal to Winter
            _dayStartTime = _dayWinterStartTime;            //Day start time is equal to Winter
            _duskStartTime = _duskWinterStartTime;          //Dusk start time is equal to Winter
            _nightStartTime = _nightWinterStartTime;        //Night start time is equal to Winter

            _winterDayLength = _nightStartTime - _dawnStartTime; //winter daylength equals night start time minus the dawn start time


        }

    }

    void SunRotationManager()
    {
        Debug.Log("SunRotationManager");
        if (_spring == true)                                //if spring equals true
            _dayTemp = _springDayLength / 10;               //then day temp equals spring day length divided by ten

        if (_summer == true)                                //if summer equals true
            _dayTemp = _summerDayLength / 10;               //then day temp equals summer day length divided by ten

        if (_autumn == true)                                //if autumn equals true
            _dayTemp = _autumnDayLength / 10;               //then day temp equals autumn day length divided by ten

        if (_winter == true)                                //if winter equals true
            _dayTemp = _winterDayLength / 10;               //then day temp equals winter day length divided by ten


        _rotTemp = (_dayTemp / 270);                        //Rotation temp equals the day temp devided by (360 + 180) degrees

        transform.RotateAround (                            //rotate sun around
            _sunPivotPoint.position,                        //the pivot point (axis)
            Vector3.forward,                                //shorthand for writing vector 3 (0,0,1) (point)
            _rotTemp * Time.deltaTime);                     //by rotation temp times time.deltatime

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

    /*
    public float timeSurvived ()
    {
        return _days;
    }
    */
}
