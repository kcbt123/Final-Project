using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredicateUtil : MonoBehaviour
{
    // Start is called before the first frame update    private static bool EndsWithSaurus(String s)
    private static bool All(string s)
    {
        return s.ToLower().EndsWith("saurus");
    }
}
