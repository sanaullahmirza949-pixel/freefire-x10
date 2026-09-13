using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int magazineSize = 30;
    public int currentAmmo;
    public float fireRate = 0.15f;
    public float damage = 25f;
    public float range = 100f;
    public Transform muzzlePoint;

    float lastFireTime = 0f;

    void Start()
    {
        currentAmmo = magazineSize;
    }

    public void TryShoot()
    {
        if (Time.time - lastFireTime < fireRate) return;
        if (currentAmmo <= 0) return;

        Shoot();
        lastFireTime = Time.time;
    }

    void Shoot()
    {
        currentAmmo--;

        Ray ray = new Ray(muzzlePoint.position, muzzlePoint.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            var enemy = hit.collider.GetComponent<AIEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // TODO: spawn impact effect (add VFX asset later)
        }

        // TODO: muzzle flash/sound (add assets later)
    }

    public void Reload()
    {
        currentAmmo = magazineSize;
    }
}
