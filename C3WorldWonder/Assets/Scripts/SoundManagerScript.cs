using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundManagerScript : MonoBehaviour {

    private Vector3 CAMPOSITION = new Vector3(0, 1000, 0);
    private string nameSong;

    /// <summary>
    /// /////////
    /// </summary>
    /// 


    private GameObject carwhistle;
    private GameObject trainwhistle;
    private GameObject boatwhistle;
    private GameObject buswhistle;
    private GameObject motorbikewhistle;
    private GameObject truckwhistle;
    private GameObject spacescraftwhistle;
    private GameObject ambulancewhistle;
    private GameObject firetruckwhistle;
	private GameObject bicyclewhistle;
	private GameObject planewhistle;

    private string toiLaGiaiPhanCach3 = "-------------------------";

    private GameObject bicycleEnglish;
    private GameObject boatEnglish;
    private GameObject busEnglish;
    private GameObject carEnglish;
    private GameObject helicopterEnglish;
    private GameObject motorbikeEnglish;
    private GameObject planeEnglish;
    private GameObject trainEnglish;
    private GameObject truckEnglish;
    private GameObject spacescraftEnglish;
    private GameObject ambulanceEnglish;
    private GameObject firetruckEnglish;

    private string toiLaGiaiPhanCach4 = "-------------------------";

    private GameObject airplaneSong;
    public GameObject bicycleSong;
    private GameObject drivingCarSong;
    private GameObject rowRowBoatSong;
    private GameObject trainSong;
    private GameObject truckSong;
    private GameObject firetruckSong;
    private GameObject wheelBusSong;
    private GameObject motorbikeSong;
	private GameObject helicopterSong;

    public GameObject SongList;
    public GameObject whistleList;
    public GameObject englishList;

    private Vector3 SoundPosition;


    /// <summary>
    /// 
    /// </summary>

    void Start () {
        SoundPosition = CAMPOSITION;
    }

    public void MakeSound(string nameOfSound)
    {
        Debug.Log(nameOfSound);
        switch (nameOfSound)
        {
            case "Spacecraftwhistle":
			if (!spacescraftwhistle)
                {
					IEnumerator loadSound = LoadSound("Prefabs/Whistle/Coi_Launching_space_craft", whistleList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    spacescraftwhistle.GetComponent<AudioSource>().Play();
                }
                break;

            case "carwhistle":
                if (!carwhistle)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Whistle/Coi_oto_car", whistleList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    carwhistle.GetComponent<AudioSource>().Play();
                }
                break;
            case "trainwhistle":
                if (!trainwhistle)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Whistle/Coi_tau_train", whistleList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    trainwhistle.GetComponent<AudioSource>().Play();
                }
                break;
            case "shipwhistle":
                if (!boatwhistle)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Whistle/Coi_tauthuy", whistleList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    boatwhistle.GetComponent<AudioSource>().Play();
                }
                break;
			case "Firetruckwhistle":
				if (!firetruckwhistle) {
					//IEnumerator loadSound = LoadSound("Prefabs/Whistle/Coi_xe_cuuhoa", whistleList);
					//StartCoroutine(loadSound);
					firetruckwhistle = GameObject.Find ("Coi_xe_cuuhoa");
				}
                firetruckwhistle.GetComponent<AudioSource>().Play();
                break;
            case "Ambulancewhistle":
				if (!ambulancewhistle)
	                {
					ambulancewhistle = GameObject.Find ("Coi_xe_cuuthuong");
	                }
                ambulancewhistle.GetComponent<AudioSource>().Play();
                break;
            case "buswhistle":
                if (!buswhistle)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Whistle/Coi_xebus", whistleList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    buswhistle.GetComponent<AudioSource>().Play();
                }
                break;
            case "motorbikewhistle":
                if (!motorbikewhistle)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Whistle/Coi_xemay", whistleList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    motorbikewhistle.GetComponent<AudioSource>().Play();
                } 
                break;
           
            case "truckwhistle":
                if (!truckwhistle)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Whistle/Coi_xetai", whistleList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    truckwhistle.GetComponent<AudioSource>().Play();
                }
                break;
			case  "bicyclewhistle":
				if (!bicyclewhistle) {
					IEnumerator loadSound = LoadSound ("Prefabs/Whistle/Coi_xedap", whistleList);
					StartCoroutine (loadSound);
				} else {
					bicyclewhistle.GetComponent<AudioSource> ().Play ();
				}
				break;
			case  "boatwhistle":
				if (!boatwhistle) {
					IEnumerator loadSound = LoadSound ("Prefabs/Whistle/Coi_tauthuy", whistleList);
					StartCoroutine (loadSound);
				} else {
					boatwhistle.GetComponent<AudioSource> ().Play ();
				}
				break;
			case  "planewhistle":
				if (!planewhistle) {
					IEnumerator loadSound = LoadSound ("Prefabs/Whistle/Coi_maybay", whistleList);
					StartCoroutine (loadSound);
				}
				else {
					planewhistle.GetComponent<AudioSource> ().Play ();
				}
				break;


            ///toi la dai phan cach
            ///

            case "AmbulanceEnglish":
                if (!ambulanceEnglish)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/English/Ambulance", englishList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    ambulanceEnglish.GetComponent<AudioSource>().Play();
                }
                break;
            case "bicycleEnglish":
                if (!bicycleEnglish)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/English/Bicycle", englishList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    bicycleEnglish.GetComponent<AudioSource>().Play();
                }
                break;
            case "boatEnglish":
                if (!boatEnglish)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/English/Boat", englishList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    boatEnglish.GetComponent<AudioSource>().Play();
                }
                break;
            case "busEnglish":
                if (!busEnglish)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/English/BUS", englishList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    busEnglish.GetComponent<AudioSource>().Play();
                }
                break;
            case "carEnglish":
                if (!carEnglish)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/English/Car", englishList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    carEnglish.GetComponent<AudioSource>().Play();
                }
                break;
            case "FiretruckEnglish":
                if (!firetruckEnglish)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/English/Firetruck", englishList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    firetruckEnglish.GetComponent<AudioSource>().Play();
                }
                break;
            case "helicopterEnglish":
                if (!helicopterEnglish)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/English/Helicopter", englishList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    helicopterEnglish.GetComponent<AudioSource>().Play();
                }
                break;
            case "motorbikeEnglish":
                if (!motorbikeEnglish)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/English/motorbike", englishList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    motorbikeEnglish.GetComponent<AudioSource>().Play();
                }
                break;
            case "planeEnglish":
                if (!planeEnglish)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/English/Plane", englishList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    planeEnglish.GetComponent<AudioSource>().Play();
                }
                break;
            case "SpacecraftEnglish":
                if (!spacescraftEnglish)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/English/Spacecraft", englishList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    spacescraftEnglish.GetComponent<AudioSource>().Play();
                }
                break;
            case "trainEnglish":
                if (!trainEnglish)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/English/Train", englishList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    trainEnglish.GetComponent<AudioSource>().Play();
                }
                break;
            case "truckEnglish":
                if (!truckEnglish)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/English/Truck", englishList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    truckEnglish.GetComponent<AudioSource>().Play();
                }
                break;
           
        }
    }
    public void EnableSong(string nameOfSong)
    {
        DisableAllSong();
        this.nameSong = nameOfSong;
        switch (nameOfSong)
        {
            case "planeSong":
                if (airplaneSong == null)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Song/AirplaneSong", SongList);
                    StartCoroutine(loadSound);
                }
                if (airplaneSong)
                {
                    airplaneSong.GetComponent<AudioSource>().UnPause();
                }
                break;
            case "bicycleSong":
                if (bicycleSong == null)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Song/BicycleSong", SongList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    bicycleSong.GetComponent<AudioSource>().UnPause();
                }
                break;
            case "carSong":
                if (drivingCarSong == null)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Song/Driving_CarSong", SongList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    drivingCarSong.GetComponent<AudioSource>().UnPause();
                }
                break;
            case "firetruckSong":
                if (motorbikeSong == null)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Song/Fire_truckSong", SongList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    firetruckSong.GetComponent<AudioSource>().UnPause();
                }
                break;
            case "motorbikeSong":
                if (motorbikeSong == null)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Song/MotorbikeSong", SongList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    motorbikeSong.GetComponent<AudioSource>().UnPause();
                }
                break;
            case "boatSong":
                if (rowRowBoatSong == null)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Song/Row_row_boatSong", SongList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    rowRowBoatSong.GetComponent<AudioSource>().UnPause();
                }
                break;
            case "trainSong":
                if (trainSong == null)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Song/TrainSong", SongList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    trainSong.GetComponent<AudioSource>().UnPause();
                }
                break;
            case "truckSong":
                if (truckSong == null)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Song/TruckSong", SongList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    truckSong.GetComponent<AudioSource>().UnPause();
                }
                break;
            case "busSong":
                if (wheelBusSong == null)
                {
                    IEnumerator loadSound = LoadSound("Prefabs/Song/Wheel_onthe_BusSong", SongList);
                    StartCoroutine(loadSound);
                }
                else
                {
                    wheelBusSong.GetComponent<AudioSource>().UnPause();
                }
                break;
			case "helicopterSong":
				if (helicopterSong == null)
				{
					IEnumerator loadSound = LoadSound("Prefabs/Song/HelicopterSong", SongList);
					StartCoroutine(loadSound);
				}
				else
				{
					helicopterSong.GetComponent<AudioSource>().UnPause();
				}
				break;
	           
        }
    }

    IEnumerator LoadSound(string path, GameObject soundParent)
    {
        ResourceRequest soundRequest = Resources.LoadAsync(path);
        GameObject soundPrefab = null;
        GameObject soundObject = null;
        yield return soundRequest;
        soundPrefab = soundRequest.asset as GameObject;
        soundObject = (GameObject)Instantiate(soundPrefab, CAMPOSITION, Quaternion.identity);
        soundObject.transform.parent = soundParent.transform;
        switch (path)
        {
            case "Prefabs/Whistle/Coi_Launching_space_craft":
                spacescraftwhistle = soundObject;
                break;
            case "Prefabs/Whistle/Coi_oto_car":
                carwhistle = soundObject;
                break;
            case "Prefabs/Whistle/Coi_tau_train":
                trainwhistle = soundObject;
                break;
            case "Prefabs/Whistle/Coi_tauthuy":
                boatwhistle = soundObject;
                break;
            case "Prefabs/Whistle/Coi_xe_cuuhoa":
                firetruckwhistle = soundObject;
                break;
            case "Prefabs/Whistle/Coi_xe_cuuthuong":
                ambulancewhistle = soundObject;
                break;
            case "Prefabs/Whistle/Coi_xebus":
                buswhistle = soundObject;
                break;
            case "Prefabs/Whistle/Coi_xemay":
                motorbikewhistle = soundObject;
                break;
            case "Prefabs/Whistle/Coi_xetai":
                truckwhistle = soundObject;
                break;
			case "Prefabs/Whistle/Coi_maybay":
				planewhistle = soundObject;
				break;
			case "Prefabs/Whistle/Coi_xedap":
				bicyclewhistle = soundObject;
				break;
            
                // toi la giai phan cach
            case "Prefabs/English/Ambulance":
                ambulanceEnglish = soundObject;
                break;
            case "Prefabs/English/Bicycle":
                bicycleEnglish = soundObject;
                break;
            case "Prefabs/English/Boat":
                boatEnglish = soundObject;
                break;
            case "Prefabs/English/BUS":
                busEnglish = soundObject;
                break;
            case "Prefabs/English/Car":
                carEnglish = soundObject;
                break;
            case "Prefabs/English/Firetruck":
                firetruckEnglish = soundObject;
                break;
            case "Prefabs/English/Helicopter":
                helicopterEnglish = soundObject;
                break;
            case "Prefabs/English/motorbike":
                motorbikeEnglish = soundObject;
                break;
            case "Prefabs/English/Plane":
                planeEnglish = soundObject;
                break;
            case "Prefabs/English/Spacecraft":
                spacescraftEnglish = soundObject;
                break;
            case "Prefabs/English/Train":
                trainEnglish = soundObject;
                break;
            case "Prefabs/English/Truck":
                truckEnglish = soundObject;
                break;
            
            
            // toi la giai phan cach
            case "Prefabs/Song/AirplaneSong":
                airplaneSong = soundObject;
                if (nameSong != "planeSong")
                {
                    soundObject.GetComponent<AudioSource>().Pause();
                }
                break;
            case "Prefabs/Song/BicycleSong":
                bicycleSong = soundObject;
                if (nameSong != "bicycleSong")
                {
                    soundObject.GetComponent<AudioSource>().Pause();
                }
                break;
            case "Prefabs/Song/Driving_CarSong":
                drivingCarSong = soundObject;
                if (nameSong != "carSong")
                {
                    soundObject.GetComponent<AudioSource>().Pause();
                }
                break;
            case "Prefabs/Song/Fire_truckSong":
                firetruckSong = soundObject;
                if (nameSong != "firetruckSong")
                {
                    soundObject.GetComponent<AudioSource>().Pause();
                }
                break;
            case "Prefabs/Song/MotorbikeSong":
                motorbikeSong = soundObject;
                if (nameSong != "motorbikeSong")
                {
                    soundObject.GetComponent<AudioSource>().Pause();
                }
                break;
            case "Prefabs/Song/Row_row_boatSong":
                rowRowBoatSong = soundObject;
                if (nameSong != "boatSong")
                {
                    soundObject.GetComponent<AudioSource>().Pause();
                }
                break;
            case "Prefabs/Song/TrainSong":
                trainSong = soundObject;
                if (nameSong != "trainSong")
                {
                    soundObject.GetComponent<AudioSource>().Pause();
                }
                break;
            case "Prefabs/Song/TruckSong":
                truckSong = soundObject;
                if (nameSong != "truckSong")
                {
                    soundObject.GetComponent<AudioSource>().Pause();
                }
                break;
            case "Prefabs/Song/Wheel_onthe_BusSong":
                wheelBusSong = soundObject;
                if (nameSong != "busSong")
                {
                    soundObject.GetComponent<AudioSource>().Pause();
                }
                break;
			case "Prefabs/Song/HelicopterSong":
				helicopterSong = soundObject;
				if (nameSong != "helicopterSong")
				{
					soundObject.GetComponent<AudioSource>().Pause();
				}
				break;



        }
    }

    public void DisableAllSound()
    {
        if (spacescraftwhistle)
        {
            spacescraftwhistle.GetComponent<AudioSource>().Stop();
        }
        if (carwhistle)
        {
            carwhistle.GetComponent<AudioSource>().Stop();
        }
        if (trainwhistle)
        {
            trainwhistle.GetComponent<AudioSource>().Stop();
        }
        if (boatwhistle)
        {
            boatwhistle.GetComponent<AudioSource>().Stop();
        }
        if (firetruckwhistle)
        {
            firetruckwhistle.GetComponent<AudioSource>().Stop();
        }
        if (ambulancewhistle)
        {
            ambulancewhistle.GetComponent<AudioSource>().Stop();
        }
        if (buswhistle)
        {
            buswhistle.GetComponent<AudioSource>().Stop();
        }
        if (motorbikewhistle)
        {
            motorbikewhistle.GetComponent<AudioSource>().Stop();
        }
        if (truckwhistle)
        {
            truckwhistle.GetComponent<AudioSource>().Stop();
        }
		if (planewhistle) 
		{
			planewhistle.GetComponent<AudioSource> ().Stop ();
		}
		if (bicyclewhistle) 
		{
			bicyclewhistle.GetComponent<AudioSource> ().Stop ();
		}
    }

    public void DisableAllSong()
    {
        if (airplaneSong)
        {
            airplaneSong.GetComponent<AudioSource>().Pause();
        }
        if (bicycleSong)
        {
            bicycleSong.GetComponent<AudioSource>().Pause();
			Debug.Log ("diable song");
        }
        if (drivingCarSong)
        {
            drivingCarSong.GetComponent<AudioSource>().Pause();
        }
        if (rowRowBoatSong)
        {
            rowRowBoatSong.GetComponent<AudioSource>().Pause();
        }
        if (trainSong)
        {
            trainSong.GetComponent<AudioSource>().Pause();
        }
        if (truckSong)
        {
            truckSong.GetComponent<AudioSource>().Pause();
        }
        if (wheelBusSong)
        {
            wheelBusSong.GetComponent<AudioSource>().Pause();
        }
        if (motorbikeSong)
        {
            motorbikeSong.GetComponent<AudioSource>().Pause();
        }
        if (firetruckSong)
        {
            firetruckSong.GetComponent<AudioSource>().Pause();
        }
		if (helicopterSong)
		{
			helicopterSong.GetComponent<AudioSource>().Pause();
		}
    }
}
