using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator animator;
    public GameObject player;
    playerController playerController;
    public GameObject boss;
    BossKnight bossKnight;
    private int sceneIndex;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerController = player.GetComponentInParent<playerController>();
        bossKnight = boss.GetComponentInParent<BossKnight>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!playerController.IsAlive)
        {
            sceneIndex = 1;
            FadeOut();
        }
        if (!bossKnight.IsAlive)
        {
            sceneIndex = 2;
            FadeOut();
        }
    }

    public void FadeOut()
    {
        animator.SetTrigger("fadeOut");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
