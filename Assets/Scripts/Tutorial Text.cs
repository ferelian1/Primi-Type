using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    [Header("Waktu dalam detik sebelum GameObject ini di-disable")]
    public float delay = 3f;

    private Coroutine disableCoroutine;

    void OnEnable()
    {
        // Jika coroutine lama masih jalan (misalnya OnEnable dipanggil ulang sebelum selesai), hentikan dulu
        if (disableCoroutine != null)
        {
            StopCoroutine(disableCoroutine);
        }
        disableCoroutine = StartCoroutine(DisableAfterDelay());
    }

    void OnDisable()
    {
        // Jika di-disable manual sebelum delay habis, hentikan coroutine agar tidak error
        if (disableCoroutine != null)
        {
            StopCoroutine(disableCoroutine);
            disableCoroutine = null;
        }
    }

    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        disableCoroutine = null;
    }
}