using System.Collections;
using UnityEngine;

public class Game : GameController
{
    public GameObject introduction;
    public float introductionDuration = 16;
    public float gameOverDuration;
    public GameObject respawn;
    public Controller player;
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
        ResetFlags();
        StartCoroutine(RunIntroduction());
        resetables.SetActive(false);
        player.gameObject.SetActive(true);
    }
    private void ResetFlags()
    {
        death = false;
        conversion = false;
        won = false;
    }
    /*IEnumerator RunIntroduction()
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
     }*/
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
            if (!death && !player.alive && !player.invincible)
            {
                StartCoroutine(Die());
            }
        }

        if(!boss.alive)
        {
            won = true;
            player.invincible = true;
        }

        bossLife.SetActive(false);
        if (!won && bosstriger.trigger)
        {
            bossLife.SetActive(true);
            bossLifeBar.value = (float)boss.hp / boss.maxHp;

            /*if(playerController.equipement.GetAlignment() >= 3)
            {
                conversion = true;
                playerController.gameObject.GetComponent<PlayerController>().enabled = false;
                playerController.gameObject.GetComponent<EnemyController>().enabled = true;
                playerController.gameObject.layer = 10;
                playerAutogun.aimRange = 0;
                playerAutogun.shootRange = 0;
            }*/
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

    public virtual void ResetLevel()
    {
        if (instanciatedScene)
        {
            DestroyImmediate(instanciatedScene);
        }
        instanciatedScene = Instantiate(resetables);
        instanciatedScene.SetActive(true);
    }


    protected IEnumerator Die()
    {
        AS.PlayOneShot(explode);
        death = true;
        BSOD.SetActive(true);
        yield return new WaitForSeconds(gameOverDuration);
        
        DestroyImmediate(instanciatedScene);
        initialized = false;
        Initialize();

        player.transform.position = respawn.transform.position;
        player.ResetChanges();
        player.gameObject.SetActive(true);

        BSOD.SetActive(false);
        death = false;
    }
}
