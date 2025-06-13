using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingEvan : MonoBehaviour
{

    [Header("Shooting")]
    public float shootingDelay = 1f;
    public float elementalType = 0f;
    public int elementalAmmo = 0;
    private float shootingTimer;
    private bool puedeDisparar = true;
    public GameObject bala;
    public GameObject balaFuego;
    public GameObject balaHielo;

    public Animator mAnimator;

    public cargadorBalas cargadorBalas;

    public AudioSource AudioS;
    [SerializeField] private AudioClip shotSound;


    void Start()
    {
        mAnimator = GetComponent<Animator>();
        AudioS = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (!puedeDisparar)
        {
            shootingTimer += Time.deltaTime;
            if (shootingTimer >= shootingDelay)
            {
                puedeDisparar = true;
            }
        }
        if (elementalAmmo == 0)
        {
            elementalType = 0f;
        }
        if(elementalAmmo > 0)
        {
            for (int i = 0; i < elementalAmmo; i++)
            {
                if (cargadorBalas != null)
                {
                    cargadorBalas.ActivarBala(i, elementalType);
                }
            }
        }
    }


    public void Shoot(InputAction.CallbackContext context)
    {

        if (puedeDisparar)
        {
            mAnimator.SetTrigger("Shoot");
            if (AudioS != null)
            {
                AudioS.PlayOneShot(shotSound);
            }
            Debug.Log("Disparo, elemento: " + elementalType);
            puedeDisparar = false;
            shootingTimer = 0;
            Vector3 offset = new Vector3((transform.localScale.x > 0 ? 1 : -1) * 1.1f, 0, 0);
            if (elementalType == 0)
            {
                GameObject temp = Instantiate(bala, transform.position + offset, transform.rotation);
                Debug.Log("Se ha creado: " + temp);
                Destroy(temp, 3);
            }
            if (elementalType == 1 && elementalAmmo > 0)
            {
                GameObject temp = Instantiate(balaFuego, transform.position + offset, transform.rotation);
                Destroy(temp, 3);
                elementalAmmo--;
                cargadorBalas.DesactivarBala(elementalAmmo);
            }
            if (elementalType == 2 && elementalAmmo > 0)
            {
                GameObject temp = Instantiate(balaHielo, transform.position + offset, transform.rotation);
                Destroy(temp, 3);
                elementalAmmo--;
                cargadorBalas.DesactivarBala(elementalAmmo);
            }
        }
    }

    public void SetElement(float type, int ammo)
    {
        elementalType = type;
        elementalAmmo = ammo;
    }
}
