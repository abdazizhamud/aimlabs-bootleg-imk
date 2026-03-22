using UnityEngine;
using UnityEngine.UI; // Wajib ditambahkan untuk memanipulasi UI Text
using TMPro;
public class GameManager : MonoBehaviour
{
    // Ini adalah pola Singleton agar script lain bisa memanggil GameManager dengan mudah
    public static GameManager instance;

    [Header("Pengaturan Target")]
    public GameObject targetPrefab;
    public int maksimalTarget = 8; // Jumlah target yang selalu ada di layar
    
    // Batas area kemunculan target (Silakan sesuaikan angkanya nanti)
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = 1f;
    public float maxY = 6f;
    public float jarakZ = 15f; // Jarak kedalaman target dari pemain

    [Header("Sistem Skor")]
    public TextMeshProUGUI teksSkor; // Gunakan TMPro.TextMeshProUGUI jika memakai TextMeshPro
    private int skorTotal = 0;

    void Awake()
    {
        // Setup Singleton
        if (instance == null) instance = this;
    }

    void Start()
    {
        // Saat game dimulai, langsung buat target sebanyak nilai 'maksimalTarget' (misal: 8)
        for (int i = 0; i < maksimalTarget; i++)
        {
            MunculkanTarget();
        }
        UpdateUI();
    }

    public void MunculkanTarget()
    {
        // Cari titik acak di dalam batas X dan Y yang sudah ditentukan
        float posisiXAcak = Random.Range(minX, maxX);
        float posisiYAcak = Random.Range(minY, maxY);
        
        Vector3 posisiSpawn = new Vector3(posisiXAcak, posisiYAcak, jarakZ);

        // Cetak (Instantiate) target baru di posisi acak tersebut
        Instantiate(targetPrefab, posisiSpawn, Quaternion.identity);
    }

    public void TambahSkor(int poin)
    {
        skorTotal += poin;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (teksSkor != null)
        {
            teksSkor.text = "Score: " + skorTotal;
        }
    }
}