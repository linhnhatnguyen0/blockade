using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCinematic : MonoBehaviour
{

    public GameObject target; // Le GameObject autour duquel la caméra va tourner
    public float rotationSpeed = 30f; // Vitesse de rotation (en degrés par seconde)
    public float duration = 6f; // Durée d'un tour complet (en secondes)
    private Quaternion initialRotation; // Rotation initiale de la caméra

    private Vector3 initialPosition; // Position initiale de la caméra

    private float elapsedTime = 0f; // Temps écoulé depuis le début du mouvement
    private Vector3 board;

    private bool startCinematic = false; // Flag pour démarrer la cinématique

    public GameObject interfacejeu;


    void Start()
    {
        // Cacher l'interface de jeu
        interfacejeu.SetActive(false);
        // Enregistrez la rotation initiale de la caméra
        initialRotation = transform.rotation;
        // Enregistrez la position initiale de la caméra
        initialPosition = transform.position;

        board = GameObject.Find("Board").transform.position;
    }

    void Update()
    {
        if (startCinematic)
        {
            // Assurez-vous que le GameObject cible est défini
            if (target != null)
            {
                // Calculer le nombre de tours complets effectués
                float completedTurns = elapsedTime / duration;

                // Vérifier si 3 tours complets ont été effectués
                if (completedTurns >= 1f)
                {
                    // Revenir à la rotation initiale
                    transform.rotation = initialRotation;
                    // Revenir à la position initiale
                    transform.position = initialPosition;
                    // Arrêter le mouvement de la caméra
                    startCinematic = false;
                    interfacejeu.SetActive(true); // Afficher l'interface de jeu
                    return;
                }

                // Rotation autour du centre du GameObject cible (horizontalement)
                transform.RotateAround(board, board, rotationSpeed * Time.deltaTime);

                // Gardez la caméra tournée vers le GameObject cible
                transform.LookAt(target.transform);

                // Mettre à jour le temps écoulé
                elapsedTime += Time.deltaTime;

                Debug.Log(elapsedTime);
                
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
    }
}


