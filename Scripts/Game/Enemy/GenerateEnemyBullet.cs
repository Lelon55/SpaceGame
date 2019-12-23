using UnityEngine;
using System.Collections;

public class GenerateEnemyBullet : MonoBehaviour
{
    private float Cooling;

    public GameObject bullet_ship;
    public SpriteRenderer bullet;
    [SerializeField] private Generate_bullet Generate_bullet;
    [SerializeField] private Skins Skins;
    private EnemyControl EnemyControl;


    private void Start()
    {
        EnemyControl = GameObject.Find("enemy_spaceship").GetComponentInChildren<EnemyControl>();
        SetCooling(Random.Range(0.5f, 1.7f));
        SetRandomLaser(Random.Range(0, Skins.skin_laseru.Length));
    }

    private void SetCooling(float Cooling)
    {
        this.Cooling = Cooling;
    }

    private float GetCooling()
    {
        return Cooling;
    }

    private void SetRandomLaser(int value)
    {
        bullet.sprite = Skins.skin_laseru[value];
    }

    private IEnumerator CountToGenerate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Cooling);
            SetCooling(Random.Range(0.5f, 1.7f));
            GenerateBullet(Random.Range(0, 3)); 
            StopCoroutine("CountToGenerate");
        }
    }

    private void GenerateBullet(int nr)
    {
        if (!EnemyControl.GetEnemyMoving())
        {
            Vector2 bulletPosition = new Vector2(transform.position.x - 0.02f, transform.position.y - 0.5f);
            Generate_bullet.CreateBullet(bullet_ship, bulletPosition, Generate_bullet.laser_sound[nr], transform.position);
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
        //brak strzalu, ale ma szukac wroga od ściany do ściany
        StopCoroutine("CountToGenerate");
    }
}
