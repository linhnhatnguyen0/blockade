using UnityEngine;

public class CamAnim : MonoBehaviour
{
    public Transform target; // Référence vers l'objet autour duquel la caméra doit tourner
    public float rotationSpeed = 20f; // Vitesse de rotation de la caméra
    public float descentSpeed = 2f; // Vitesse de descente de la caméra
    private float totalRotation = 0f; // Stocke le total de rotation effectuée jusqu'à présent
    private float rotationTime = 0f; // Temps écoulé depuis le début de la rotation

    private bool isDescending = true; // Indique si la caméra est en train de descendre

    void Update()
    {
        // Si la caméra est en train de descendre, exécute l'animation de descente
        if (isDescending)
        {
            DescendCamera();
        }
    }

    void RotateCamera()
    {
        // Calcule le pas de rotation en fonction du temps écoulé depuis la dernière frame
        float rotationStep = rotationSpeed * rotationTime;

        // Tourne la caméra autour du GameObject 'Cube'
        transform.RotateAround(target.transform.position, Vector3.up, rotationStep);

        // Met à jour le total de rotation effectuée jusqu'à présent
        totalRotation += rotationStep;

        // Si le total de rotation dépasse 360 degrés, arrête la rotation
        if (totalRotation >= 360f)
        {
            // Réinitialise le total de rotation à 360 degrés pour éviter un débordement
            totalRotation = 360f;

            // Arrête la rotation en désactivant ce script
            enabled = false;
        }
    }

    void DescendCamera()
    {
        // Calcule le pas de descente en fonction du temps écoulé depuis la dernière frame
        float descentStep = descentSpeed * Time.deltaTime;

        // Descend la caméra vers le bas
        transform.Translate(Vector3.down * descentStep, Space.World);

        // Décale la caméra latéralement
        float lateralStep = descentStep * 0.5f; // Ajuste cette valeur selon ton besoin
        transform.Translate(Vector3.right * lateralStep, Space.World);

        // Fait tourner progressivement la caméra vers une vue de face
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2f); // 2f est une vitesse de rotation arbitraire, tu peux ajuster selon tes besoins

        // Si la caméra est presque au niveau de l'objet, arrête la descente
        if (transform.rotation.eulerAngles.x < 30f)
        {
            isDescending = false;
            rotationTime = Time.deltaTime; // Réinitialise le temps de rotation
            RotateCamera(); // Appelle RotateCamera une fois que la caméra a cessé de descendre
        }
    }

    void LookAtTarget()
    {
        // Fait en sorte que la caméra regarde progressivement vers l'objet
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}
