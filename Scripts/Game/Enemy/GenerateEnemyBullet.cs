using UnityEngine;
using System.Collections;

public class GenerateEnemyBullet : MonoBehaviour
{
    private float Cooling;

    public GameObject bullet_ship;
    public SpriteRenderer bullet;
    public Rigidbody2D GravityBullet;
    [SerializeField] private Generate_bullet Generate_bullet;
    [SerializeField] private Skins Skins;
    private EnemyControl EnemyControl;

    private void Start()
    {
        EnemyControl = GameObject.Find("enemy_spaceship").GetComponentInChildren<EnemyControl>();
        SetCooling(Random.Range(0.5f, 1.7f));
        SetGravityBullet(Random.Range(0.8f, 1.5f));
        SetSkinLaser(Random.Range(0, Skins.skin_laseru.Length));
    }

    private void SetCooling(float cooling)
    {
        this.Cooling = cooling;
    }

    private float GetCooling()
    {
        return Cooling;
    }

    private void SetGravityBullet(float gravityBullet)
    {
        GravityBullet.gravityScale = gravityBullet;
    }

    private void SetSkinLaser(int ID)
    {
        bullet.sprite = Skins.skin_laseru[ID];
    }

    private IEnumerator CountToGenerate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Cooling);
            SetCooling(Random.Range(0.5f, 1.7f));
            ShowBullet(Random.Range(0, Generate_bullet.laser_sound.Length)); 
            StopCoroutine("CountToGenerate");
        }
    }

    private void ShowBullet(int ID)
    {
        if (!EnemyControl.GetEnemyMoving())
        {
            Vector2 bulletPosition = new Vector2(transform.position.x - 0.02f, transform.position.y - 0.5f);
            Generate_bullet.CreateBullet(bullet_ship, bulletPosition, Generate_bullet.laser_sound[ID], transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("CountToGenerate");
            EnemyControl.MovementEnemy();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine("CountToGenerate");
    }
}
