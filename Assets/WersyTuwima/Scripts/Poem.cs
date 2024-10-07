using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poem : MonoBehaviour
{
    [SerializeField]
    private AudioClip _poemSound;

    private Material _material;

    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound(_poemSound);
            GameObject.FindGameObjectWithTag("PoemCounter").GetComponent<PoemCounter>().IncrementPoemsCount();
            StartCoroutine(FadeOut());
        }
    }

    public IEnumerator FadeOut()
    {
        _material.SetFloat("_Fade", 1);

        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * 1.5f;
            _material.SetFloat("_Fade", alpha);
            yield return null;
        }
        Destroy(gameObject);
    }
}
