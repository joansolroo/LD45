using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject introduction;

    public float gameOverDuration;
    public GameObject respawn;
    public Controller playerController;
    public GameObject resetables;

    private GameObject instanciatedScene;
    private GameObject instanciatedPlayer;

    public bool initialized = false;
    public bool death;

    protected virtual void Start()
    {
        death = false;
        if (introduction != null)
            introduction.SetActive(true);
        resetables.SetActive(false);
        playerController.gameObject.SetActive(true);
    }

    protected virtual void LateUpdate()
    {
        // keep intro running if we have an intro, launch game if not or key pressed
        if (introduction != null && introduction.activeSelf)
        {
            if (Input.anyKey)
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
                Debug.Log("toto");
                StartCoroutine(Die());
            }
        }
    }

    private void Initialize()
    {
        instanciatedScene = Instantiate(resetables);
        instanciatedScene.SetActive(true);
        initialized = true;
    }

    protected IEnumerator Die()
    {
        death = true;
        yield return new WaitForSeconds(gameOverDuration);
        
        DestroyImmediate(instanciatedScene);
        DestroyImmediate(instanciatedPlayer);
        Initialize();
        playerController.transform.position = respawn.transform.position;
        playerController.Reset();

        death = false;
    }
}
