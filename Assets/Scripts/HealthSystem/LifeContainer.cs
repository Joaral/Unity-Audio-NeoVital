using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UI.Image;

public class LifeContainer : LifeTaker
{
    [Header("Health Settings")]
    public int healthPoints;
    public bool isInvencible;
    public bool isInvulnerable;
    public float maxHp;
    public enum onDie { Destroy, RestartMenu, DestroyBoss};
    public onDie onKilled;
    public GameObject dropReward;
    public barraVidas barraVidas;

    protected SpriteRenderer sr;
    protected Animator mAnimator;
    public Color damageA = new Color(1f, 1f, 0.5f);
    public Color damageB = new Color(1f, 1f, 0.25f);
    private Color damageFire = new Color(255f, 85f, 0f);
    private Color damageIce = new Color(0.5f, 0.5f, 0.5f);
    private Color originalColor;


    public AudioSource AudioS;
    [SerializeField] private AudioClip hitEnemySound;
    [SerializeField] private AudioClip hitPlayerSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip gameOverSound;



    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr)
        {
            originalColor = sr.color;
        }
        AudioS = GetComponent<AudioSource>();
        mAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void Damage(int damage, DamageType type)
    {
        if (!isInvencible || !isInvulnerable) 
        {
            healthPoints -= damage;
            if (gameObject.tag == "Player")
            {
                StartCoroutine(Damage_Corutine());
                barraVidas.DesactivarVida(healthPoints);
                barraVidas.ActivarVidaVacia(healthPoints);
            }
            else
            {
                switch (type)
                {
                    case DamageType.None:
                        StartCoroutine(Damage_Corutine_E());
                        break;
                    case DamageType.Fire:
                        StartCoroutine(Damage_Corutine_F());
                        break;
                    case DamageType.Ice:
                        StartCoroutine(Damage_Corutine_I());
                        break;
                    default:
                        StartCoroutine(Damage_Corutine_E());
                        break;
                }
                
            }

            Debug.Log(this + " Tiene: " + healthPoints + " de vida.");
            if (healthPoints <= 0 )
            {
                Kill();
            }
        }
    }

    IEnumerator Damage_Corutine()
    {
        isInvulnerable = true;
        if (AudioS != null)
        {
            AudioS.PlayOneShot(hitPlayerSound);
        }
        if (sr)
        {
            sr.color = damageA;
        }
        yield return new WaitForSeconds(0.25f);
        if (sr)
        {
            sr.color = damageB;
        }
        yield return new WaitForSeconds(0.25f);

        if (sr)
        {
            sr.color = damageA;
        }
        yield return new WaitForSeconds(0.25f);
        if (sr)
        {
            sr.color = damageB;
        }
        yield return new WaitForSeconds(0.25f);
        if (sr)
        {
            sr.color = originalColor;
        }
        isInvulnerable = false;
    }


    IEnumerator Damage_Corutine_E()
    {
        isInvulnerable = true;
        if (AudioS != null)
        {
            AudioS.PlayOneShot(hitEnemySound);
        }
        if (sr)
        {
            sr.color = damageA;
        }
        yield return new WaitForSeconds(0.1f);
        if (sr)
        {
            sr.color = damageB;
        }
        yield return new WaitForSeconds(0.1f);

        if (sr)
        {
            sr.color = damageA;
        }
        yield return new WaitForSeconds(0.1f);
        if (sr)
        {
            sr.color = damageB;
        }
        yield return new WaitForSeconds(0.1f);
        if (sr)
        {
            sr.color = originalColor;
        }
        isInvulnerable = false;
    }


    IEnumerator Damage_Corutine_F()
    {
        isInvulnerable = true;
        if (AudioS != null)
        {
            AudioS.PlayOneShot(hitEnemySound);
        }
        if (sr)
        {
            sr.color = damageFire;
            
        }
        yield return new WaitForSeconds(1f);
        if (sr)
        {
            sr.color = originalColor;
            
        }
        yield return new WaitForSeconds(1f);
        if (sr)
        {
            sr.color = damageFire;
            healthPoints -= 5;
        }
        yield return new WaitForSeconds(1f);
        if (sr)
        {
            sr.color = originalColor;

        }
        isInvulnerable = false;
    }

    IEnumerator Damage_Corutine_I()
    {
        if (sr != null)
        {
            sr.color = damageIce;
        }
        if (AudioS != null)
        {
            AudioS.PlayOneShot(hitEnemySound);
        }
        

        MovimientoPatrulla patrulla = GetComponent<MovimientoPatrulla>();
        MovimientoZombie zombie = GetComponent<MovimientoZombie>();

        if(patrulla != null)
        {
            Debug.Log("Patrulla");
            float speed = patrulla.velocidadDefault;
            patrulla.SetSpeed(speed / 2);
            patrulla.SetPuedeDisparar(false);
            Debug.Log(patrulla.puedeDispararGlobal);
            yield return new WaitForSeconds(3f);
            if (sr != null)
            {
                sr.color = originalColor;
            }
            float newSpeed = patrulla.velocidadDefault; //por si se gira
            patrulla.SetSpeed(newSpeed);

            yield return new WaitForSeconds(1f);
            patrulla.SetPuedeDisparar(true);
            Debug.Log(patrulla.puedeDispararGlobal);
        }
        else if(zombie != null)
        {
            Debug.Log("Zombie reduce");
            float speed = zombie.velocidadDefault;
            zombie.SetSpeed(speed / 2);
            yield return new WaitForSeconds(3f);
            if (sr != null)
            {
              sr.color = originalColor;
            }
            float newSpeed = zombie.velocidadDefault; //por si se gira
            zombie.SetSpeed(newSpeed);
            Debug.Log("Zombie recupera");
        }
        isInvulnerable = false;
    }

    public void Kill()
    {
        switch (onKilled)
        {
            case onDie.Destroy:
                StartCoroutine(Death_Corrutine());
                break;
            case onDie.DestroyBoss:
                StartCoroutine(Boss_Corrutine());
                break;
            case onDie.RestartMenu:
                StartCoroutine(Restart_Corrutine());
                break;
            default:
                StartCoroutine(Restart_Corrutine());
                break;
        }
    }

    IEnumerator Death_Corrutine()
    {
        mAnimator.SetBool("isDead", true);
        MovimientoPatrulla patrulla = GetComponent<MovimientoPatrulla>();
        if (patrulla != null)
        {
            patrulla.puedeDispararGlobal = false;
        }
        if (AudioS != null)
        {
            AudioS.PlayOneShot(deathSound);
        }
        yield return new WaitForSeconds(1f);
        if (dropReward != null)
        {
            GameObject tmpDropReward = Instantiate(dropReward, transform.position, transform.rotation);
            tmpDropReward.transform.position += new Vector3(0, 1);
        }
        
        Destroy(gameObject);
    }

    IEnumerator Restart_Corrutine()
    {
        mAnimator.SetBool("isDead", true);

        if (AudioS != null)
        {
            AudioS.PlayOneShot(gameOverSound);
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }


    IEnumerator Boss_Corrutine()
    {
        mAnimator.SetBool("isDead", true);
        if (AudioS != null)
        {
            AudioS.PlayOneShot(gameOverSound);
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        SceneManager.LoadScene("Main_Menu");

    }
}
