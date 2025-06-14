using UnityEngine;

public class unityDefaultMethodsAndDebugLogs : MonoBehaviour
{
    // Bu script Unity'de GameObject'lere eklenebilen basit bir MonoBehaviour'dur.
    // Aşağıdaki methodlar Unity yaşam döngüsü methodlarıdır ve özel davranışlar implement etmek için override edilebilir.
    
    private int updateCount = 0;
    private int fixedUpdateCount = 0;
    private int lateUpdateCount = 0;
    private float lastLogTime = 0f;
    private const float LOG_INTERVAL = 1f; // Her saniye log yazdır
    
    // Awake - GameObject oluşturulduğunda, Start'tan önce çağrılır (GameObject aktif olmasa bile)
    // Genellikle referansları ve başlangıç değerlerini ayarlamak için kullanılır
    void Awake()
    {
        Debug.Log($"[{Time.time:F2}s] Awake() çağrıldı - GameObject: {gameObject.name}");
    }
    
    // OnEnable - GameObject aktif hale geldiğinde çağrılır
    // GameObject her aktif edildiğinde tetiklenir
    void OnEnable()
    {
        Debug.Log($"[{Time.time:F2}s] OnEnable() çağrıldı - GameObject: {gameObject.name}");
    }

    // Start - İlk Update çağrılmadan önce bir kez çağrılır (GameObject aktifse)
    // Genellikle başlangıç kurulumları için kullanılır
    void Start()
    {
        Debug.Log($"[{Time.time:F2}s] Start() çağrıldı - GameObject: {gameObject.name}");
        Debug.LogWarning("warning log tipi");
        Debug.LogError("error log tipi");
    }

    // Update - Her frame'de bir kez çağrılır
    // Kullanıcı girişi, hareket ve genel oyun lojiği için kullanılır
    void Update()
    {
        updateCount++;
        
        // Her saniye log yazdır
        if (Time.time - lastLogTime >= LOG_INTERVAL)
        {
            Debug.Log($"[{Time.time:F2}s] Son {LOG_INTERVAL} saniyede -> Update: {updateCount} kez, FixedUpdate: {fixedUpdateCount} kez, LateUpdate: {lateUpdateCount} kez çağrıldı");
            Debug.Log($"[{Time.time:F2}s] FPS Tahmini: {updateCount / LOG_INTERVAL:F1} (Update bazlı)");
            
            updateCount = 0;
            fixedUpdateCount = 0;
            lateUpdateCount = 0;
            lastLogTime = Time.time;
        }
    }

    // FixedUpdate - Sabit zaman aralıklarında çağrılır (genellikle fizik hesaplamaları için)
    // Frame rate'den bağımsız olarak çalışır
    // örneğin kullanıcının w'ye bastığını update'te yakalarken, bastığında ne kadar zıplayacak vb burada yapmak lazım ki fps'e göre zıplama farketmesin.
    void FixedUpdate()
    {
        fixedUpdateCount++;
    }
    
    // LateUpdate - Tüm Update methodları çağrıldıktan sonra çağrılır
    // Kamera takibi gibi işlemler için kullanılır
    void LateUpdate()
    {
        lateUpdateCount++;
    }
    
    // OnGUI - GUI olaylarını işlemek için çağrılır
    // Eski GUI sistemi için kullanılır (artık UI Canvas tercih edilir)
    void OnGUI()
    {
        // Debug.Log($"[{Time.time:F2}s] OnGUI() çağrıldı - GUI eventi işleniyor");
    }
    
    // OnDrawGizmos - Scene view'da her zaman gizmo çizmek için kullanılır
    // Debug ve görselleştirme amaçlı
    void OnDrawGizmos()
    {
        // Bu çok sık çağrıldığı için sadece Scene view açıkken log yazdır
        // if (Application.isEditor)
        // {
        //     // Bu method çok sık çağrıldığı için seyrek log yazdıralım
        //     if (Time.realtimeSinceStartup % 2f < 0.1f) // Yaklaşık her 2 saniyede bir
        //     {
        //         Debug.Log($"[{Time.time:F2}s] OnDrawGizmos() çağrıldı - Scene view'da gizmo çiziliyor");
        //     }
        // }
    }
    
    // OnDrawGizmosSelected - Sadece GameObject seçiliyken gizmo çizer
    // Debug ve görselleştirme amaçlı
    void OnDrawGizmosSelected()
    {
        // Debug.Log($"[{Time.time:F2}s] OnDrawGizmosSelected() çağrıldı - Seçili GameObject için gizmo çiziliyor");
    }
    
    // OnDisable - GameObject deaktif edildiğinde çağrılır
    // Temizlik işlemleri için kullanılır
    void OnDisable()
    {
        // Debug.Log($"[{Time.time:F2}s] OnDisable() çağrıldı - GameObject: {gameObject.name} deaktif edildi");
    }
    
    // OnDestroy - GameObject yok edilmeden önce çağrılır
    // Son temizlik işlemleri ve kaynak serbest bırakma için kullanılır
    void OnDestroy()
    {
        // Debug.Log($"[{Time.time:F2}s] OnDestroy() çağrıldı - GameObject: {gameObject.name} yok ediliyor");
    }
}
