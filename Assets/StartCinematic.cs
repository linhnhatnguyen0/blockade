using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCinematic : MonoBehaviour
{
    public GameObject target; 
    private Quaternion initialRotation; 
    private Vector3 initialPosition; 
    private float elapsedTime = 0f; 
    private Vector3 board;
    private bool startCinematic = false; // Flag pour démarrer la cinématique
    public GameObject interfacejeu;
    public AudioSource audioSource; // Référence à l'AudioSource
    public AudioClip cameraSoundClip; // Référence au clip audio pour la phase 1

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
                    // Arrêter le mouvement de la caméra
                    startCinematic = false;
                    interfacejeu.SetActive(true); // Afficher l'interface de jeu
                    return;
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
        audioSource.clip = cameraSoundClip;
        audioSource.Play();
    }
}
