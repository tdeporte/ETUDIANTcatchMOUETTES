using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouette : MonoBehaviour
{
    //La miette que la mouette veut manger
    GameObject miette;

    float speed;
    
    //Timer durant lequel la mouette reste au sol
    public float timeStay = 0 ;

    //Vitesse à laquelle les mouettes s'envolent
    float getAwaySpeed = 5;

    //Timer durant lequel la mouette s'envole
    float getAwayTime = 0;

    GameObject[] gos;

    //Méthode renvoyant la miette la plus proche de la mouette
    public GameObject FindClosestMiette()
    {
        
        gos = GameObject.FindGameObjectsWithTag("Miette");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    //Lorsque la mouette entre en contact avec une miette, cette derniere disparait et la mouette reste au sol quelques temps avant de s'envoler
    void OnTriggerEnter(Collider other) {  
        if(other.gameObject.tag=="Miette"){
            System.Collections.Generic.List<GameObject> list = new System.Collections.Generic.List<GameObject>(gos);
            list.Remove(other.gameObject);
            gos = list.ToArray();
            timeStay = Random.Range(1, 5);
            getAwayTime=Random.Range(5, 10) + timeStay;
            Destroy(other.gameObject);
            
        }
        
        if(other.gameObject.tag=="Mouette"){
            getAwayTime=Random.Range(5, 10);

        }
    } 

    void Start()
    {
        speed = Random.Range(10, 15);   
    }

    void Update()
    {
        timeStay -= Time.deltaTime;
        getAwayTime -= Time.deltaTime;

        //Trouve la miette la plus proche
        miette = FindClosestMiette(); 
        if (timeStay < 0){
            if(getAwayTime < 0){
                //La mouette aligne son regard vers la miette
                Vector3 relativePos = miette.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1);

                //Se rapproche de la miette
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, miette.transform.position, step);
            }
            else{
                transform.position += Vector3.up * getAwaySpeed * Time.deltaTime;

                //La mouette reprend une rotation neutre
                Quaternion rotation = Quaternion.LookRotation(new Vector3(0,0,0), Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1);
                
            }
        }
        
        

        
    }
}
