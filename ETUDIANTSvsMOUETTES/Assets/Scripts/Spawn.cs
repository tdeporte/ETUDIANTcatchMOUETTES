using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public int Size;

    public GameObject EntityPrefab;

    private GameObject[] Entities;

    [Range(-10000, 10000)]
    public int randomSeed = 1;
    private System.Random randomGenerator;

    private Vector3[] Positions;
    private Vector3[] Rotations;


    Vector3 getRandomVector3()
    {
        return new Vector3((float)randomGenerator.NextDouble() - .5f,
                           (float)randomGenerator.NextDouble() - .5f,
                           (float)randomGenerator.NextDouble() - .5f);
    }

    void generatePositions(int Size)
    {
        randomGenerator = new System.Random(randomSeed);

        Positions = new Vector3[Size];
        Rotations = new Vector3[Size];

        for (int i = 0; i < Size; i++)
        {
            Positions[i] = getRandomVector3();
            Rotations[i] = getRandomVector3();
        }
    }

    [RuntimeInitializeOnLoadMethod]
    void generateEntities()
    {
        Entities = new GameObject[Size];
        
        for (int i = 0; i < Size; i++)
        {
            GameObject newEntity = Instantiate(EntityPrefab,
                                               Vector3.zero,
                                               Quaternion.identity);

            newEntity.transform.position = this.transform.position + Vector3.Scale(this.transform.localScale, Positions[i]);
            newEntity.transform.rotation = Quaternion.Euler(Rotations[i]);
            Debug.Log(newEntity.transform.position);
            Entities[i] = newEntity;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(this.tag=="Etudiant"){
            Size =(int) Data.numberEtudiants;
        }
        if(this.tag=="Mouette"){
            Size = (int)Data.numberMouettes;
        }
        if(this.tag=="Miette"){
            Size = (int)Data.numberMiettes;
        }
        generatePositions(Size);
        generateEntities();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 pos = this.transform.position;
        Vector3 scale = this.transform.lossyScale;


        /* Draw bounding box */
        /* Gizmos use global world coordinates */
        Gizmos.DrawWireCube(pos, scale);

        Gizmos.DrawRay(pos - scale / 2, scale);
        Gizmos.DrawRay(pos - Vector3.Reflect(scale, Vector3.up) / 2, Vector3.Reflect(scale, Vector3.up));

        scale = Vector3.Reflect(scale, Vector3.left);

        Gizmos.DrawRay(pos - scale / 2, scale);
        Gizmos.DrawRay(pos - Vector3.Reflect(scale, Vector3.up) / 2, Vector3.Reflect(scale, Vector3.up));

        
        /* Draw  entities */
        /*
        for (int i = 0; i < Size; i++)
        {
            Vector3 localPos = Positions[i];
            Vector3 realPos = pos + Vector3.Scale(localPos, this.transform.lossyScale);

            Gizmos.color = Color.Lerp(Color.cyan, Color.magenta, localPos.sqrMagnitude * 2);
            Gizmos.DrawCube(realPos, Vector3.one);
        }*/
    }
    
}
