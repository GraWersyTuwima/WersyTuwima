using UnityEngine;

public class WszyscyDlaWszystkichLevel : MonoBehaviour
{
    public int PoemFragments { get; private set; } = 0;
    public int PoemFragmentsNeeded { get; private set; } = 1;

    public void AddPoemFragment()
    {
        PoemFragments++;
    }
}
