using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _poemPrefab;

    public void SpawnPoem()
    {
        GameObject poem = Instantiate(_poemPrefab, transform.position, Quaternion.identity);
        poem.transform.SetParent(transform);
        poem.GetComponent<BoxCollider2D>().enabled = false;

        Animator poemAnimator = poem.GetComponent<Animator>();
        poemAnimator.SetTrigger("Show");

        StartCoroutine(EnableCollider(poem));
    }

    private IEnumerator EnableCollider(GameObject poem)
    {
        yield return new WaitForSeconds(1f);
        poem.GetComponent<BoxCollider2D>().enabled = true;
    }
}
