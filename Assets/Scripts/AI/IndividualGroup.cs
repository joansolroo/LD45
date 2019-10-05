using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualGroup : MonoBehaviour
{
    [SerializeField] List<IIndividual> individuals = new List<IIndividual>();


    private void Start()
    {
        individuals.AddRange(GetComponentsInChildren<IIndividual>());
    }
    private void Update()
    {
        for(int c = individuals.Count-1; c >=0;--c)
        {
            if (!individuals[c].Alive())
            {
                Destroy(individuals[c].GetGameObject());
                individuals.RemoveAt(c);
            }
        }
        foreach (IIndividual person in individuals)
        {
            person.Sense();
        }
        foreach (IIndividual person in individuals)
        {
            person.Think();
        }
        foreach (IIndividual person in individuals)
        {
            person.Act();
        }
        foreach (IIndividual person in individuals)
        {
            person.Display();
        }
    }
}
