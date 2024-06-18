using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCinematic : MonoBehaviour
{
    public GameObject target; 
    public GameObject effectLeft; 
    public GameObject effectRight; 

    public RectTransform letsgo;
    private Quaternion initialRotation; 
    private Vector3 initialPosition; 
    private float elapsedTime = 0f; 
    private Vector3 board;
    private bool startCinematic = false; // Flag pour démarrer la cinématique
    public GameObject interfacejeu;
    public AudioSource audioSource; // Référence à l'AudioSource
    public AudioClip cameraSoundClip; // Référence au clip audio pour la phase 1
    public AudioClip letsgoClip; // Référence au clip audio à jouer
    public AudioClip letsgoEffectClip; // Référence au clip audio à jouer

    private bool movementStarted = false;

    void Start()
    {
        // Cacher l'interface de jeu
        interfacejeu.SetActive(false);
        initialRotation = transform.rotation;
        initialPosition = transform.position;
        board = GameObject.Find("Board").transform.position;

    }

    void Update()
    {
        if (startCinematic)
        {
            if (target != null)
            {
                elapsedTime += Time.deltaTime;

                float rotationSpeed = (elapsedTime <= 1.5f) ? 15f : 60f;

                transform.RotateAround(board, board, rotationSpeed * Time.deltaTime);

                transform.LookAt(target.transform);

                // Réinitialisation à la fin de l'animation
                if (elapsedTime >= 7.5f)
                {
                    transform.rotation = initialRotation;
                    transform.position = initialPosition;
                    audioSource.Stop(); // Arrêter le son à la fin de l'animation
                    effectLeft.SetActive(false);
                    effectRight.SetActive(false);
                    interfacejeu.SetActive(true); // Afficher l'interface de jeu

                    if (!movementStarted)
                    {
                        StartCoroutine(MoveElements());
                        movementStarted = true;
                        // return;
                    }

                    startCinematic = false;
                }
            }
        }
    }

    // Méthode pour démarrer la cinématique
    public void BeginCinematic()
    {
        startCinematic = true;
        elapsedTime = 0f;
        // Cacher l'interface de jeu au début de la cinématique
        interfacejeu.SetActive(false);
        effectLeft.SetActive(true);
        effectRight.SetActive(true);
        audioSource.clip = cameraSoundClip;
        audioSource.Play();
    }

    IEnumerator MoveElements()
    {
        Vector2 letsgoPosition = new Vector2(0f, 0f); // Position intermédiaire (0, 0)
        float duration = 1.5f; // Durée du déplacement en secondes

        // Jouer le son
        audioSource.clip = letsgoClip;
        audioSource.Play();

        // Jouer le second son en utilisant PlayOneShot
        audioSource.PlayOneShot(letsgoEffectClip);

        float timer = 0f;

        // Descendre à la position intermédiaire
        while (timer < duration)
        {
            float t = timer / duration;
            letsgo.anchoredPosition = Vector2.Lerp(letsgo.anchoredPosition, letsgoPosition, t);
            timer += Time.deltaTime;
            yield return null;
        }

        // Correction de la position pour s'assurer qu'il soit exactement à la position intermédiaire
        letsgo.anchoredPosition = letsgoPosition;

        Debug.Log("After descending:");
        Debug.Log("letsgo.anchoredPosition: " + letsgo.anchoredPosition);

        letsgo.gameObject.SetActive(false);

    }
}
