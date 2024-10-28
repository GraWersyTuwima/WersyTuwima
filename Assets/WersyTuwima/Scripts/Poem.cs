using System.Collections;
using UnityEngine;

public class Poem : MonoBehaviour
{
    [SerializeField]
    private AudioClip _poemSound;

    private Material _material;
    private Animator _animator;

    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
        _animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AleksCollider"))
        {
            GetComponent<Collider2D>().enabled = false;
            _animator.ResetTrigger("Show");
            _animator.SetTrigger("Hide");

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
