using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs = new Invader[4];
    public GameObject bossHealthUI;
    public GameObject bossPrefab;

    private bool bossSpawned = false;
    public Projectile missilePrefab;
    public int rows = 4;
    public int columns = 10;
    public int totalInvaders => this.rows * this.columns;
    public int amountLived => this.totalInvaders - this.amountKilled;
    private float speed = 2.0f;
    private Vector3 direction = Vector2.right;
    public int amountKilled { get; private set; } = 0;
    public float missileSpawnRate = 1.0f;

    public void Start()
    {
        SpawnInvaders();
        InvokeRepeating(nameof(MissileAttack), missileSpawnRate, missileSpawnRate);
    }

    private void MissileAttack()
    {
        // Bước 1: gom tất cả invader còn sống vào 1 danh sách
        List<Transform> aliveInvaders = new List<Transform>();

        foreach (Transform invader in this.transform)
        {
            if (invader.gameObject.activeInHierarchy)
            {
                aliveInvaders.Add(invader);
            }
        }

        // Bước 2: nếu còn invader sống thì chọn random
        if (aliveInvaders.Count > 0)
        {
            int randomIndex = Random.Range(0, aliveInvaders.Count);
            Transform randomInvader = aliveInvaders[randomIndex];

            Instantiate(this.missilePrefab, randomInvader.position, this.missilePrefab.transform.rotation);
        }
    }

    public void SpawnInvaders()
    {
        for (int row = 0; row < this.rows; row++)
        {
            float height = this.columns - 1;//36
            float width = 2.0f * (this.rows - 1);//16
            Vector2 center = new Vector2(-width, height / 4f);//18, 8
            Vector3 rowPosition = new Vector3(center.x, row * 2.0f + center.y, 0.0f);

            for (int col = 0; col < this.columns; col++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;

                Vector3 position = rowPosition;
                position.x += col * 3.0f;
                position.y += row * 1.5f;
                invader.transform.position = position;
            }
        }
    }
    void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        // Vector3 bottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        // Vector3 topEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        //bool isGameOver = false;

        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy) continue;

            if (direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f))
            {
                AdvanceRow();
            }
            else if (direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }
    }

    private void AdvanceRow()
    {
        direction.x *= -1;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }
    private void InvaderKilled()
    {
        // Khi một invader bị tiêu diệt, tăng số lượng đã tiêu diệt
        this.amountKilled++;
        if (this.amountKilled >= this.totalInvaders)
        {
            // GameManager.Instance.OnLevelComplete();
            bossSpawned = true;
            StartCoroutine(SpawnBossDelayed());
        }
    }
    private IEnumerator SpawnBossDelayed()
    {
        yield return new WaitForSeconds(3f); // đợi 3 giây
        SpawnBoss();
    }

    private void SpawnBoss()
    {
        // Vector3 spawnPos = new Vector3(0f, Camera.main.orthographicSize - 1.5f, 0f);
        Vector3 spawnPos = new Vector3(0f, 8f, 0f);
        GameObject bossObj = Instantiate(bossPrefab, spawnPos, Quaternion.identity);

        Boss bossScript = bossObj.GetComponent<Boss>();
        bossScript.InitBossUI(bossHealthUI);
    }
}

