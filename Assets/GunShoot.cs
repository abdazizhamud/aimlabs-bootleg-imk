using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Camera cam;
    public float range = 100f;

    // 1. UBAH DI SINI: Kita ganti ParticleSystem menjadi GameObject
    public GameObject muzzleFlashPrefab; 
    public AudioSource gunSound;
    public LineRenderer lineRenderer;
    
    public Transform firePoint; 

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 2. UBAH DI SINI: Spawn efek ledakan Muzzle Flash tepat di posisi FirePoint
        if (muzzleFlashPrefab != null && firePoint != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            // Jadikan child dari firepoint sebentar agar efeknya ikut bergerak jika player menembak sambil berlari
            flash.transform.SetParent(firePoint); 
        }

        gunSound.Play();

        RaycastHit hit;

        Vector3 rayStart = cam.transform.position;
        Vector3 rayDirection = cam.transform.forward;

        lineRenderer.SetPosition(0, firePoint.position);

        if (Physics.Raycast(rayStart, rayDirection, out hit, range))
        {
            lineRenderer.SetPosition(1, hit.point);

            // JIKA MENABRAK TARGET:
            if (hit.collider.CompareTag("Target")) 
            {
                // 1. Tambah skor 10 poin
                GameManager.instance.TambahSkor(10);
                
                // 2. Munculkan 1 target baru untuk menggantikan yang hancur
                GameManager.instance.MunculkanTarget();

                // 3. Hancurkan target yang ini
                Destroy(hit.transform.gameObject);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, rayStart + rayDirection * range);
        }

        StartCoroutine(DisableLine());
    }

    System.Collections.IEnumerator DisableLine()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;
    }
}