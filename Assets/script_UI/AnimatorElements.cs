using System.Collections;
using UnityEngine;

public class AnimatorElements : MonoBehaviour
{
    public RectTransform element1;
    public RectTransform element2;
    public RectTransform element3;
    public RectTransform element4;
    public RectTransform element5;

    public GameObject effectLeft; 
    public GameObject effectRight; 
    
    public RectTransform versusEffect;

    public GameObject interfacejeu;
    public GameObject startBackground;


    public AudioSource audioSource; // Référence à l'AudioSource
    public AudioClip player1Clip; // Référence au clip audio à jouer
    public AudioClip player2Clip; // Référence au clip audio à jouer
    public AudioClip versusClip; // Référence au clip audio à jouer
    public AudioClip playerEffectClip; // Référence au clip audio à jouer
    public AudioClip versusEffectClip; // Référence au clip audio à jouer


    private bool movementStarted = false;


    // Ajoutez une référence à StartCinematic
    public StartCinematic startCinematic;


    void Update()
    {
        if (!movementStarted)
        {
            StartCoroutine(MoveElements());
            movementStarted = true;
        }
    }

    IEnumerator MoveElements()
    {
        effectLeft.SetActive(false);
        effectRight.SetActive(false);

        // Cacher le GameObject
        interfacejeu.SetActive(false);
        
        // Déplacement des éléments 1 et 2
        Vector2 targetPosition1 = new Vector2(-565f, 90f);
        Vector2 targetPosition2 = new Vector2(-565f, -325f);
        float duration = 2f; // Durée du déplacement en secondes

        // Debug.Log("Starting positions:");
        // Debug.Log("element1.anchoredPosition: " + element1.anchoredPosition);
        // Debug.Log("element2.anchoredPosition: " + element2.anchoredPosition);

        // Jouer le son
        audioSource.clip = player1Clip;
        audioSource.Play();

        // Jouer le second son en utilisant PlayOneShot
        audioSource.PlayOneShot(playerEffectClip);

        float timer = 0f;
        while (timer < duration)
        {
            float t = timer / duration;
            element1.anchoredPosition = Vector2.Lerp(element1.anchoredPosition, targetPosition1, t);
            element2.anchoredPosition = Vector2.Lerp(element2.anchoredPosition, targetPosition2, t);
            timer += Time.deltaTime;
            yield return null;
        }

        // Correction des positions pour s'assurer qu'ils soient exactement à la position finale
        element1.anchoredPosition = targetPosition1;
        element2.anchoredPosition = targetPosition2;

        // Debug.Log("After movement:");
        // Debug.Log("element1.anchoredPosition: " + element1.anchoredPosition);
        // Debug.Log("element2.anchoredPosition: " + element2.anchoredPosition);

        // Déplacement de l'élément 3 et versusEffect
        Vector2 targetPosition3 = new Vector2(0f, 0f);
        Vector2 versusEffectPosition = new Vector2(0f, 0f);
        duration = 2f; // Durée du déplacement en secondes

        // Jouer le son
        audioSource.clip = versusClip;
        audioSource.Play();

        // Jouer le second son en utilisant PlayOneShot
        audioSource.PlayOneShot(versusEffectClip);

        timer = 0f;
        while (timer < duration)
        {
            float t = timer / duration;
            element3.anchoredPosition = Vector2.Lerp(element3.anchoredPosition, targetPosition3, t);
            versusEffect.anchoredPosition = Vector2.Lerp(versusEffect.anchoredPosition, versusEffectPosition, t);
            timer += Time.deltaTime;
            yield return null;
        }

        // Correction de la position pour s'assurer qu'il soit exactement à la position finale
        element3.anchoredPosition = targetPosition3;
        versusEffect.anchoredPosition = versusEffectPosition;


        // Debug.Log("After element3 movement:");
        // Debug.Log("element3.anchoredPosition: " + element3.anchoredPosition);

        // Déplacement des éléments 4 et 5
        Vector2 targetPosition4 = new Vector2(565f, 90f);
        Vector2 targetPosition5 = new Vector2(565f, -325f);
        duration = 2f; // Durée du déplacement en secondes

        // Jouer le son
        audioSource.clip = player2Clip;
        audioSource.Play();

        // Jouer le second son en utilisant PlayOneShot
        audioSource.PlayOneShot(playerEffectClip);

        timer = 0f;
        while (timer < duration)
        {
            float t = timer / duration;
            element4.anchoredPosition = Vector2.Lerp(element4.anchoredPosition, targetPosition4, t);
            element5.anchoredPosition = Vector2.Lerp(element5.anchoredPosition, targetPosition5, t);
            timer += Time.deltaTime;
            yield return null;
        }

        // Correction des positions pour s'assurer qu'ils soient exactement à la position finale
        element4.anchoredPosition = targetPosition4;
        element5.anchoredPosition = targetPosition5;

        // Debug.Log("After elements 4 and 5 movement:");
        // Debug.Log("element4.anchoredPosition: " + element4.anchoredPosition);
        // Debug.Log("element5.anchoredPosition: " + element5.anchoredPosition);

        // Cacher les RectTransforms à la fin de la coroutine
        element1.gameObject.SetActive(false);
        element2.gameObject.SetActive(false);
        element3.gameObject.SetActive(false);
        element4.gameObject.SetActive(false);
        element5.gameObject.SetActive(false);

        versusEffect.gameObject.SetActive(false);

        startBackground.gameObject.SetActive(false);

        // Réactiver le GameObject
        interfacejeu.SetActive(true);

        // Appeler la méthode de début de la cinématique
        if (startCinematic != null)
        {
            startCinematic.BeginCinematic();
        }
    }
}