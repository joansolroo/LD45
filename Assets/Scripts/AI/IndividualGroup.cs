using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualGroup : MonoBehaviour
{
    [SerializeField] List<IIndividual> individuals = new List<IIndividual>();
    public Transform player;
    [Range(0, 100)] public float activationRadius;

    private void Start()
    {
        individuals.AddRange(GetComponentsInChildren<IIndividual>());
        if (player == null)
            player = transform;
    }
    private void Update()
    {
        for (int c = individuals.Count-1; c >=0;--c)
        {
            if (!individuals[c].Alive())
            {
                Destroy(individuals[c].GetGameObject());
                individuals.RemoveAt(c);
            }
        }
        foreach (IIndividual person in individuals)
        {
            person.GetGameObject().SetActive((player.position - transform.position).magnitude < activationRadius);
            if (person.GetGameObject().activeSelf)
            {
                person.Sense();
                person.Act();
            }
        }
    }
}
