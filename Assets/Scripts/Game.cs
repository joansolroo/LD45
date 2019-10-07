using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject introduction;
    public float introductionDuration = 16;
    public float gameOverDuration;
    public GameObject respawn;
    public Controller playerController;
    public AutoGun playerAutogun;
    public GameObject resetables;
    public Controller boss;
    public BossTrigger bosstriger;
    public GameObject bossLife;
    public UIBar bossLifeBar;

    private GameObject instanciatedScene;
    private GameObject instanciatedPlayer;

    public GameObject BSOD;
    public AudioClip explode;
    public AudioSource AS;


    public bool initialized = false;
    public bool death, conversion, won;

    protected virtual void Start()
    {
        death = false;
        conversion = false;
        won = false;
        StartCoroutine(RunIntroduction());
        resetables.SetActive(false);
        playerController.gameObject.SetActive(true);
    }
    IEnumerator RunIntroduction()
    {
        if (introduction != null)
        {
            introduction.SetActive(true);
            yield return new WaitForSeconds(introductionDuration-1);
            if(!initialized)
            {
                Initialize();
            }
            yield return new WaitForSeconds(1);
            introduction.SetActive(false);
        }
    }
    protected virtual void LateUpdate()
    {
        // keep intro running if we have an intro, launch game if not or key pressed
        if (introduction != null && introduction.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                introduction.SetActive(false);
                Initialize();
            }
        }
        else if(!initialized && introduction == null)
        {
            Initialize();
        }

        // standard game update
        if (introduction == null || !introduction.activeSelf)
        {
            if (!death && !playerController.alive && !playerController.invincible)
            {
                StartCoroutine(Die());
            }
        }

        if(!boss.alive)
        {
            won = true;
            playerController.invincible = true;
        }

        bossLife.SetActive(false);
        if (!won && bosstriger.trigger)
        {
            bossLife.SetActive(true);
            bossLifeBar.value = (float)boss.hp / boss.maxHp;

            if(playerController.equipement.GetAlignment() >= 3)
            {
                conversion = true;
                playerController.gameObject.GetComponent<PlayerController>().enabled = false;
                playerController.gameObject.GetComponent<EnemyController>().enabled = true;
                playerController.gameObject.layer = 10;
                playerAutogun.aimRange = 0;
                playerAutogun.shootRange = 0;
            }
        }
    }

    private void Initialize()
    {
        if (!initialized)
        {
            instanciatedScene = Instantiate(resetables);
            instanciatedScene.SetActive(true);
            initialized = true;
        }
    }

    protected IEnumerator Die()
    {
        AS.PlayOneShot(explode);
        death = true;
        BSOD.SetActive(true);
        yield return new WaitForSeconds(gameOverDuration);
        
        DestroyImmediate(instanciatedScene);
        DestroyImmediate(instanciatedPlayer);
        Initialize();
        playerController.transform.position = respawn.transform.position;
        playerController.Reset();
        playerController.gameObject.SetActive(true);

        BSOD.SetActive(false);
        death = false;
    }
}
