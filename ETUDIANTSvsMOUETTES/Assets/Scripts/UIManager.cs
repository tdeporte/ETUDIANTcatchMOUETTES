using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject[] allObjects;
    GameObject[] pauseObjects;
    GameObject[] LaunchObjects;
    GameObject[] mouettes;
    GameObject[] miettes;

    Text mouetteScore;
    Text mietteScore;

    Text numberMouettesText;
    Text numberEtudiantsText;
    Text numberMiettesText;

    

    public Slider numberMouettesSlider;
    public Slider numberEtudiantsSlider;
    public Slider numberMiettesSlider;

	void Start () {
		Time.timeScale = 1;
        if(SceneManager.GetActiveScene().name=="SampleScene"){
            pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		    hidePaused();
        }

        if(SceneManager.GetActiveScene().name=="Options"){
            Setup();
        }
	}

	void Update () {
        if(SceneManager.GetActiveScene().name=="SampleScene"){
            updateScores();
        }
        if(SceneManager.GetActiveScene().name=="Options"){
            updateSliderText();
        }
		//La touche escape met le jeu en pause
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				showPaused();
			} else if (Time.timeScale == 0){
				Debug.Log ("high");
				Time.timeScale = 1;
				hidePaused();
			}
		}
	}

    public void Setup(){
        numberMouettesSlider.value =Data.numberMouettes;
        numberMouettesSlider.onValueChanged.AddListener( ( value ) => Data.numberMouettes = value );

        numberEtudiantsSlider.value = Data.numberEtudiants;
        numberEtudiantsSlider.onValueChanged.AddListener( ( value ) => Data.numberEtudiants = value );

        numberMiettesSlider.value = Data.numberMiettes;
        numberMiettesSlider.onValueChanged.AddListener( ( value ) => Data.numberMiettes = value );
        
    }

    //Met à jour les variables statiques indiquant le nombre d'entitées à spawn
    public void updateSliderText()  
    {
        numberMouettesText=GameObject.Find("numberMouettes").GetComponent<Text>(); 
        numberMiettesText=GameObject.Find("numberMiettes").GetComponent<Text>(); 
        numberEtudiantsText=GameObject.Find("numberEtudiants").GetComponent<Text>(); 

        numberMouettesText.text="Mouettes : " + Data.numberMouettes;    
        numberMiettesText.text="Miettes : " + Data.numberMiettes;  
        numberEtudiantsText.text="Etudiants : " + Data.numberEtudiants; 
        
    } 

    //Met à jour les compteurs de mouettes et de miette sur la scène principale
    public void updateScores()  
    {
        mouettes = GameObject.FindGameObjectsWithTag("Mouette");
        miettes = GameObject.FindGameObjectsWithTag("Miette");

        mouetteScore=GameObject.Find("MouettesRemaining").GetComponent<Text>(); 
        mietteScore=GameObject.Find("MiettesRemaining ").GetComponent<Text>(); 

        mouetteScore.text="Mouettes restantes : " + (mouettes.Length-1);    
        mietteScore.text="Miettes restantes : " + (miettes.Length-1);  

        if(mouettes.Length==1 || miettes.Length == 1){
            LoadLevel("End");
        }
        
    } 


	//Recharge la scène actuelle
	public void Reload(){
		Application.LoadLevel(Application.loadedLevel);
	}

	//Controle la pause
	public void pauseControl(){
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				showPaused();
			} else if (Time.timeScale == 0){
				Time.timeScale = 1;
				hidePaused();
			}
	}

	//Affiche les objets avec le tag "ShowOnPause"
	public void showPaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
	}

	//Cache les objets avec le tag "ShowOnPause"
	public void hidePaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
	}

	//Charge le niveau donné en entrée
	public void LoadLevel(string level){
		SceneManager.LoadScene(level, LoadSceneMode.Single);
	}

    //Quitte l'application
    public void Quit(){
		Application.Quit();
	}
}
