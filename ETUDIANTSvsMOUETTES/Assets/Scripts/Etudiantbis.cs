using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etudiantbis : MonoBehaviour
{
    //L'Etudiant peut être dans 3 états, un seul à la fois
    enum State
    {
        //Il cherche une cible
        WANDERING,
        //Il poursuit sa cible
        CHASING,
        //Il fait demi-tour (Pour rester dans la zone de jeu)
        TURNINGBACK
    }
    
    State state;

    //Distance du champ de vision
    float VisionRange = 20.0f;
    //Vitesse à laquelle l'étudiant poursuit sa cible
    public float chasingSpeed;
    //Vitesse à laquelle l'étudiant marche 
    public float wanderingSpeed;

    //Liste des mouettes
    GameObject[] mouettes;

    //Compteur avant la rotation de l'étudiant (lorsqu'il cherche une cible) 
    float turningTimer = 0;

    Quaternion rotation;

    Animator animator;
    
    //Lorsque l'étudiant entre en contact avec une mouette, cette dernière est détruite
    void OnTriggerEnter(Collider other) {  
        if(other.gameObject.tag=="Mouette"){
            Destroy(other.gameObject);
            state = State.WANDERING;
        }
    } 
  
    void Start()
    {
        animator = GetComponent<Animator>();
        state = State.WANDERING;
        chasingSpeed = Random.Range(10, 15); 
        wanderingSpeed = Random.Range(1,5);
    }

        // Update is called once per frame
    void Update () { 
        if(state == State.CHASING)
            animator.SetBool("Chasing",true);
        else{
            animator.SetBool("Chasing",false);
        }
    }


    void FixedUpdate()
    {
        bool found = false;
        turningTimer -= Time.fixedDeltaTime;

        float wanderingStep = wanderingSpeed * Time.deltaTime;

        //Vecteur de direction vers l'avant
        Vector3 forward = transform.TransformDirection(Vector3.forward) * VisionRange*2;
        Debug.DrawRay(transform.position, forward, Color.red);

        mouettes = GameObject.FindGameObjectsWithTag("Mouette");


        foreach (GameObject mouette in mouettes){
            Vector3 toMouette = mouette.transform.position - transform.position;
            //Si une mouette se trouve devant l'étudiant et à une distance inférieur à la portée de l'étudiant 
            if (Vector3.Dot(forward, toMouette) > 0 && Vector3.Distance(mouette.transform.position, transform.position) < VisionRange*2 && mouette.transform.position.y < 2)
            {   //Alors l'étudiant la poursuit 
                found = true;
                state = State.CHASING;
                float chasingStep = chasingSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, mouette.transform.position, chasingStep);
            }
        }

        //Sinon il cherche
        if(found == false)
            state=State.WANDERING;

        if(state!=State.CHASING){
            //Si l'étudiant cherche une cible
            if(state==State.WANDERING){
                RaycastHit hit;
                //Si l'étudiant fait face à un mur (limite du terrain)
                if (Physics.Raycast(transform.position,  forward * VisionRange, out hit,VisionRange) && hit.transform.tag == "Wall"){
                    //Il se retourne
                    state = State.TURNINGBACK;
                    rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
                }else{
                    //Sinon il cherche, il change ponctuellement de direction
                    state = State.WANDERING;
                    if(turningTimer<0){
                        turningTimer += Random.Range(1, 3); 
                        rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + Random.Range(-180, 180), transform.rotation.z);
                    }
                }
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime);
            }
            //S'il n'est pas en train de faire demi-tour, l'étudiant avance 
            if(state!=State.TURNINGBACK)
                transform.position += transform.forward * Time.fixedDeltaTime * wanderingSpeed;
        }
        
        
    }
}
