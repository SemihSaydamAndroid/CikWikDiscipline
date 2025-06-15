using UnityEngine;

/// <summary>
/// Unity GameObjects Detaylı Eğitimi
/// 
/// === GAMEOBJECT NEDİR? ===
/// GameObject, Unity'deki HER ŞEYİN temel yapı taşıdır. Unity sahnesindeki her varlık bir GameObject'tir:
/// Her şeyin yapı taşıdır. Camera vb. bir game objectin üzerine eklenen component'ler ile işlevsellik kazanır.
/// 
/// • KAMERA - Main Camera bir GameObject'tir (Camera component'li)
/// • IŞIKLAR - Directional Light, Point Light, Spot Light hepsi GameObject'tir (Light component'li)
/// • KARAKTER - Player, Enemy, NPC hepsi GameObject'tir (çeşitli component'lerle)
/// • NESNELER - Küp, küre, masa, araba, ağaç hepsi GameObject'tir
/// • UI ELEMANLARI - Button, Text, Image hepsi GameObject'tir (Canvas altında)
/// • SES KAYNAKLARI - AudioSource component'li GameObject'ler
/// • PARÇACIK SİSTEMLERİ - Particle System component'li GameObject'ler
/// • BOŞ NESNELER - Sadece Transform'u olan yönetici GameObject'ler
/// 
/// === GAMEOBJECT YAPISI ===
/// GameObject = Container (Kutu)
/// Component = İçeriği (Kutuya koyduğumuz özellikler)
/// 
/// Örnek: Bir araba GameObject'i şunlara sahip olabilir:
/// • Transform (pozisyon, rotasyon, boyut - ZORUNLU)
/// • MeshRenderer (görüntüsü)
/// • Collider (çarpışma)
/// • Rigidbody (fizik)
/// • AudioSource (motor sesi)
/// • CarController (hareket scripti)
/// 
/// === HIERARCHY'DEKİ HER ŞEY GAMEOBJECT ===
/// Unity'de Hierarchy penceresinde gördüğünüz her satır bir GameObject'tir!
/// 
/// === PREFAB NEDİR? ===
/// Prefab = GameObject'lerin hazır kalıbı/şablonu
/// • Bir GameObject'i tüm component'leriyle birlikte kaydeder
/// • Aynı nesneyi defalarca oluşturmak için kullanılır
/// • Project penceresinde .prefab dosyası olarak saklanır
/// • Instantiate() komutuyla çalışma zamanında oluşturulur
/// • Örnek: Mermi, düşman, pickup gibi tekrar eden nesneler
/// Yani 8 obje var her birinde isTrigger açmak yerine 1 obje oluştur prefab kaydet ve her yerde kullan.
/// Bir de önemli bir özellik olarak, prefab'ı değiştirdiğinizde, o prefab'ı kullanan tüm GameObject'ler otomatik olarak güncellenir.
/// Yani merkezi yönetimi sağlar. Global variable gibi. Fakat bunu yapmak için hiyerarcy'den değil projects'teki prefab'tan değişiklik yapman gerekir.
/// Sürükleyip project'e bırakınca prefab oluşur. Tıkladığınızda prefab'ın içeriğini görebilirsiniz. 
/// 6 tane prefab'tan oluşturup, 6ncıyı ana prefab'tan farklı yapabilirsin. Bunu yaptığında sol tarafında değişiklik gibi mavi olur. Hatta eklediğin componenetlerde mavileşir solda. 
/// En üstte de Prefab Override çıkar, Revert veya apply seçenekleri gelir eğer onda değişiklik yaptıysan. Apply all yaparsan ana prefab dahil tüm prefab'ları düzenler buna göre. 

/// </summary>
public class GameObjects : MonoBehaviour
{
    #region Public Variables - Inspector'da Görünen Değişkenler
    
    [Header("GameObject Referansları")]
    [Tooltip("Herhangi bir GameObject referansı - Inspector'dan sürükle")]
    public GameObject exampleGameObject;
    
    [Tooltip("Player'ın Transform component'i - hareket için")]
    public Transform playerTransform; // 
    
    [Header("Prefab Örnekleri")]
    [Tooltip("Mermi prefab'ı - Instantiate için")]
    public GameObject bulletPrefab;
    
    [Tooltip("Düşman prefab'ı - Instantiate için")]
    public GameObject enemyPrefab;
    
    #endregion
    
    #region Private Variables - Gizli Değişkenler
    
    /// <summary>
    /// dynamicObject = Çalışma zamanında oluşturduğumuz test GameObject'i
    /// Bu nesne eğitim sırasında Component ve Transform işlemlerini test etmek için kullanılır
    /// CreateGameObjects() metodunda oluşturulur ve diğer metodlarda örnek olarak kullanılır
    /// </summary>
    private GameObject dynamicObject;

    #endregion
    
    #region Unity Lifecycle Methods - Unity Yaşam Döngüsü
    
    void Start()
    {
        // GameObject Eğitimi Başlangıcı
        Debug.Log("=== UNITY GAMEOBJECTS DETAYLI EĞİTİMİ ===");
        
        // 1. GameObject Nedir?
        ExplainWhatIsGameObject();
        
        // 2. GameObject Oluşturma
        CreateGameObjects();
        
        // 3. GameObject Bulma
        FindGameObjects();
        
        // 4. Component İşlemleri
        WorkWithComponents();
        
        // 5. Transform İşlemleri
        TransformOperations();
        
        // 6. GameObject Durumları
        GameObjectStates();
        
        // 7. Parent-Child İlişkileri
        ParentChildRelationships();
    }
    
    void Update()
    {
        // Her frame'de çalışan örnekler
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InstantiateBullet();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            CreateEnemy();
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            DestroyRandomObject();
        }
    }
    
    #endregion
    
    
    #region 1. GameObject Temel Açıklamalar
    
    /// <summary>
    /// GameObject'in detaylı açıklaması ve örnekleri
    /// </summary>
    void ExplainWhatIsGameObject()
    {
        Debug.Log("--- 1. GAMEOBJECT NEDİR? (DETAYLI) ---");
        Debug.Log("🎯 GameObject = Unity'deki TEMEL YAPITAŞI");
        Debug.Log("");
        
        Debug.Log("📋 SAHNEDEKİ HER ŞEY GAMEOBJECT:");
        Debug.Log("  🎥 Main Camera → GameObject + Camera Component");
        Debug.Log("  💡 Directional Light → GameObject + Light Component");
        Debug.Log("  🎮 Player → GameObject + Multiple Components");
        Debug.Log("  📦 Küp/Küre → GameObject + MeshRenderer + Collider");
        Debug.Log("  🖼️ UI Button → GameObject + Button + Image Components");
        Debug.Log("  🔊 Ses Kaynağı → GameObject + AudioSource Component");
        Debug.Log("  ✨ Parçacık → GameObject + ParticleSystem Component");
        Debug.Log("  📁 Boş Nesne → GameObject + sadece Transform");
        Debug.Log("");
        
        Debug.Log("🏗️ GAMEOBJECT YAPISI:");
        Debug.Log("  GameObject = Kutu (Container)");
        Debug.Log("  Component = İçerik (Özellikler)");
        Debug.Log("  Transform = ZORUNLU (her GameObject'te olmalı)");
        Debug.Log("");
        
        Debug.Log("💡 ARABA ÖRNEĞİ:");
        Debug.Log("  🚗 Araba GameObject'i:");
        Debug.Log("    • Transform (pozisyon, rotasyon, boyut)");
        Debug.Log("    • MeshRenderer (3D modeli gösterir)");
        Debug.Log("    • Collider (çarpışma algılar)");
        Debug.Log("    • Rigidbody (fizik simülasyonu)");
        Debug.Log("    • AudioSource (motor sesi)");
        Debug.Log("    • CarController (Script - hareket kontrolü)");
        Debug.Log("");
        
        Debug.Log("🔍 HIERARCHY'DEKİ HER SATIR = GAMEOBJECT!");
        Debug.Log("  Unity'de Hierarchy penceresinde gördüğünüz her öğe bir GameObject'tir");
    }
    
    #endregion
    
    
    #region 2. GameObject Oluşturma Yöntemleri
    
    /// <summary>
    /// Farklı yollarla GameObject oluşturma örnekleri
    /// </summary>
    void CreateGameObjects()
    {
        Debug.Log("--- 2. GAMEOBJECT OLUŞTURMA YÖNTEMLERİ ---");
        
        // 1. Boş GameObject oluşturma
        GameObject emptyObject = new GameObject("Boş GameObject");
        Debug.Log("✅ Boş GameObject oluşturuldu: " + emptyObject.name);
        Debug.Log("   • Sadece Transform component'i var");
        Debug.Log("   • Diğer component'ler daha sonra eklenebilir");
        
        // 2. Primitive GameObject oluşturma
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "Dinamik Küp";
        cube.transform.position = new Vector3(2, 0, 0);
        Debug.Log("✅ Primitive Küp oluşturuldu: " + cube.name);
        Debug.Log("   • Transform + MeshRenderer + MeshFilter + BoxCollider");
        
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = "Dinamik Küre";
        sphere.transform.position = new Vector3(-2, 0, 0);
        Debug.Log("✅ Primitive Küre oluşturuldu: " + sphere.name);
        
        // 3. Prefab'dan GameObject oluşturma (Instantiate)
        Debug.Log("🎭 PREFAB NEDİR?");
        Debug.Log("   • Prefab = Hazır kalıp/şablon (Template)");
        Debug.Log("   • Bir GameObject'i tüm component'leriyle birlikte kaydeder");
        Debug.Log("   • Aynı nesneyi defalarca oluşturmak için kullanılır");
        Debug.Log("   • Örnek: Mermi, düşman, pickup nesneleri");
        Debug.Log("   • Project penceresinde .prefab dosyası olarak saklanır");
        Debug.Log("");
        Debug.Log("🏭 PREFAB KULLANIM ALANLARI:");
        Debug.Log("   • Mermi sistemi - Aynı mermiyi sürekli oluştur");
        Debug.Log("   • Düşman spawn - Aynı düşmanı farklı yerlerde oluştur");
        Debug.Log("   • UI elemanları - Aynı butonu farklı menülerde kullan");
        Debug.Log("   • Çevre nesneleri - Ağaç, kaya, bina gibi tekrar eden objeler");
        Debug.Log("");
        Debug.Log("🔄 PREFAB NASIL OLUŞTURULUR?");
        Debug.Log("   1. Hierarchy'de GameObject oluştur");
        Debug.Log("   2. Component'lerini ekle ve ayarla");
        Debug.Log("   3. Project'e sürükle (.prefab dosyası oluşur)");
        Debug.Log("   4. Artık Instantiate() ile kullanabilirsin");
        Debug.Log("");
        
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = new Vector3(0, 2, 0);
            Debug.Log("✅ Prefab'dan mermi oluşturuldu");
            Debug.Log("   • Prefab'ın tüm component'leri kopyalandı");
            Debug.Log("   • Transform, Renderer, Collider vs. hepsi geldi");
            Debug.Log("   • Bu nesne artık bağımsız bir GameObject");
        }
        else
        {
            Debug.LogWarning("⚠️ Bullet prefab atanmamış!");
            Debug.LogWarning("   • Inspector'da bulletPrefab alanını doldur");
            Debug.LogWarning("   • Project'ten bir prefab sürükle");
        }
        
        // 4. Component'lerle birlikte oluşturma
        GameObject lightObject = new GameObject("Dinamik Işık");
        Light lightComponent = lightObject.AddComponent<Light>();
        lightComponent.color = Color.red;
        lightComponent.intensity = 2f;
        lightObject.transform.position = new Vector3(0, 3, 0);
        Debug.Log("✅ Dinamik ışık oluşturuldu ve kırmızı yapıldı");
        
        // Dinamik nesne referansını sakla - Eğitim için önemli!
        dynamicObject = emptyObject;
        Debug.Log("📌 dynamicObject referansı kaydedildi");
        Debug.Log("   • Bu nesne artık diğer metodlarda test için kullanılacak");
        Debug.Log("   • Component ekleme/çıkarma örnekleri");
        Debug.Log("   • Transform işlemleri");
        Debug.Log("   • Aktif/Pasif durumu testleri");
        Debug.Log("   • emptyObject = sadece Transform'lu boş GameObject");
        
        Debug.Log("📊 OLUŞTURMA YÖNTEMLERİ ÖZETİ:");
        Debug.Log("   • new GameObject() - Boş nesne (sadece Transform)");
        Debug.Log("   • CreatePrimitive() - Hazır şekiller (Küp, Küre, vb.)");
        Debug.Log("   • Instantiate() - Prefab kopyalama (Hazır kalıplar)");
        Debug.Log("   • new + AddComponent() - Özel kombinasyon");
        Debug.Log("");
        Debug.Log("🎯 PREFAB İPUÇLARI:");
        Debug.Log("   • Prefab = Tekrar kullanılabilir GameObject şablonu");
        Debug.Log("   • Bir kez oluştur, defalarca kullan");
        Debug.Log("   • Memory efficient - Aynı mesh/material'i paylaşır");
        Debug.Log("   • Prefab değişirse, tüm kopyalar güncellenir");
        Debug.Log("   • Oyun geliştirmede vazgeçilmez tool");
        Debug.Log("");
        Debug.Log("🚀 PREFAB ÖRNEKLERI:");
        Debug.Log("   • FPS Oyunu: Mermi, Düşman, Silah prefab'ları");
        Debug.Log("   • RPG Oyunu: Karakter, Item, Spell prefab'ları");
        Debug.Log("   • Puzzle Oyunu: Blok, Particle, UI prefab'ları");
        Debug.Log("   • Racing Oyunu: Araba, Track, Pickup prefab'ları");
    }
    
    #endregion
    
    
    #region 3. GameObject Bulma Teknikleri
    
    /// <summary>
    /// GameObject bulma yöntemleri ve performans notları
    /// </summary>
    void FindGameObjects()
    {
        Debug.Log("--- 3. GAMEOBJECT BULMA TEKNİKLERİ ---");
        
        // 1. İsimle bulma - DİKKAT: Yavaş!
        GameObject foundByName = GameObject.Find("Main Camera");
        if (foundByName != null)
        {
            Debug.Log("✅ İsimle bulundu: " + foundByName.name);
            Debug.Log("   • GameObject.Find() - Aktif nesnelerde arar");
            Debug.Log("   • ⚠️ YAVAŞ - Sürekli kullanmayın!");
        }

        // 2. Tag ile bulma - Daha iyi performans
        // Yani objelere unique tag'ler atayarak hızlıca bulabilirsiniz.
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            Debug.Log("✅ Tag ile bulundu: " + playerObject.name);
            Debug.Log("   • Tag sistemi daha hızlı");
        }
        else
        {
            Debug.Log("⚠️ 'Player' tag'li nesne bulunamadı");
        }
        
        // 3. Type ile bulma - Component'e göre bulma
        Camera mainCamera = FindFirstObjectByType<Camera>();
        if (mainCamera != null)
        {
            Debug.Log("✅ Camera component'li nesne bulundu: " + mainCamera.name);
            Debug.Log("   • Component'e göre arama yapar");
        }
        
        // 4. Tüm nesneleri bulma
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        Debug.Log("📊 Sahnedeki toplam aktif GameObject: " + allObjects.Length);
        
        // 5. Aktif olmayan nesneleri de bulma
        GameObject[] allIncludingInactive = Resources.FindObjectsOfTypeAll<GameObject>();
        Debug.Log("📊 Aktif olmayan dahil toplam: " + allIncludingInactive.Length);
        
        // 6. İsimle arama (StartsWith, Contains)
        GameObject[] cubeObjects = System.Array.FindAll(allObjects, 
            obj => obj.name.Contains("Küp") || obj.name.Contains("Cube"));
        Debug.Log("📦 'Küp' içeren nesneler: " + cubeObjects.Length);
        
        Debug.Log("");
        Debug.Log("🎯 PERFORMANS İPUÇLARI:");
        Debug.Log("   • Find() metodlarını Start()'ta kullanın, Update()'te DEĞİL");
        Debug.Log("   • Referansları cache'leyin (değişkenlerde saklayın)");
        Debug.Log("   • Tag sistemi daha hızlıdır");
        Debug.Log("   • FindObjectsByType Unity 2023+'ta daha hızlı");
    }
    
    #endregion
    
    
    #region 4. Component Yönetimi
    
    /// <summary>
    /// Component ekleme, bulma ve kaldırma işlemleri
    /// </summary>
    void WorkWithComponents()
    {
        Debug.Log("--- 4. COMPONENT YÖNETİMİ ---");
        Debug.Log("💡 Component = GameObject'e işlevsellik katan parçalar");
        Debug.Log("🎯 Bu bölümde dynamicObject kullanarak component işlemlerini test edeceğiz");
        Debug.Log("   • dynamicObject = CreateGameObjects()'te oluşturduğumuz boş GameObject");
        Debug.Log("");
        
        if (dynamicObject != null)
        {
            // Component ekleme
            Rigidbody rb = dynamicObject.AddComponent<Rigidbody>();
            rb.mass = 2f;
            Debug.Log("✅ Rigidbody component'i eklendi (kütle: 2)");
            
            // Component bulma
            Transform objTransform = dynamicObject.GetComponent<Transform>();
            Debug.Log("✅ Transform component'i bulundu: " + (objTransform != null));
            
            // Component varlığını kontrol etme
            bool hasRigidbody = dynamicObject.GetComponent<Rigidbody>() != null;
            Debug.Log("🔍 Rigidbody var mı? " + hasRigidbody);
            
            // TryGetComponent - Daha güvenli yöntem
            if (dynamicObject.TryGetComponent<Rigidbody>(out Rigidbody foundRb))
            {
                Debug.Log("✅ TryGetComponent ile Rigidbody bulundu");
                foundRb.useGravity = false;
                Debug.Log("   • Gravity kapatıldı");
            }
            
            // Component kaldırma
            if (rb != null)
            {
                Destroy(rb);
                Debug.Log("🗑️ Rigidbody component'i kaldırıldı");
            }
            
            // Çoklu component ekleme
            AudioSource audioSource = dynamicObject.AddComponent<AudioSource>();
            audioSource.volume = 0.5f;
            Debug.Log("✅ AudioSource eklendi (ses: 0.5)");
        }
        else
        {
            Debug.LogError("❌ dynamicObject null - Component işlemleri yapılamıyor");
        }
        
        // Bu GameObject'teki tüm component'leri bulma
        Component[] allComponents = GetComponents<Component>();
        Debug.Log("📋 Bu GameObject'teki component sayısı: " + allComponents.Length);
        
        Debug.Log("📝 COMPONENT LİSTESİ:");
        foreach (Component comp in allComponents)
        {
            Debug.Log("   • " + comp.GetType().Name);
        }
        
        Debug.Log("");
        Debug.Log("🎯 COMPONENT İPUÇLARI:");
        Debug.Log("   • AddComponent<T>() - Yeni component ekler");
        Debug.Log("   • GetComponent<T>() - Component bulur (null olabilir)");
        Debug.Log("   • TryGetComponent<T>() - Güvenli bulma yöntemi");
        Debug.Log("   • Destroy(component) - Component'i kaldırır");
        Debug.Log("   • Her GameObject'te en az Transform vardır");
    }
    
    #endregion
    
    
    #region 5. Transform İşlemleri
    
    /// <summary>
    /// Transform ile pozisyon, rotasyon ve ölçek işlemleri
    /// </summary>
    void TransformOperations()
    {
        Debug.Log("--- 5. TRANSFORM İŞLEMLERİ ---");
        Debug.Log("📐 Transform = Her GameObject'in ZORUNLU component'i");
        Debug.Log("   • Position (Pozisyon) - Nerede?");
        Debug.Log("   • Rotation (Rotasyon) - Nasıl döndürülmüş?");
        Debug.Log("   • Scale (Ölçek) - Ne kadar büyük?");
        Debug.Log("");
        
        // Pozisyon işlemleri
        Vector3 originalPos = transform.position;
        transform.position = new Vector3(1, 1, 1);
        Debug.Log("📍 Pozisyon ayarlandı: " + transform.position);
        Debug.Log("   • X=1 (sağ), Y=1 (yukarı), Z=1 (ileri)");
        
        // Rotasyon işlemleri
        transform.rotation = Quaternion.Euler(0, 45, 0);
        Debug.Log("🔄 Rotasyon ayarlandı: " + transform.rotation.eulerAngles);
        Debug.Log("   • Y ekseninde 45 derece döndürüldü");
        
        // Ölçek işlemleri
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Debug.Log("📏 Ölçek ayarlandı: " + transform.localScale);
        Debug.Log("   • Tüm eksenlerde %150 büyütüldü");
        
        // Hareket ettirme (mevcut pozisyona ekleme)
        transform.Translate(Vector3.forward * 2);
        Debug.Log("➡️ İleri hareket ettirildi (2 birim)");
        Debug.Log("   • Translate = mevcut pozisyona ekler");
        
        // Döndürme (mevcut rotasyona ekleme)
        transform.Rotate(0, 90, 0);
        Debug.Log("🔄 Y ekseninde 90 derece daha döndürüldü");
        Debug.Log("   • Rotate = mevcut rotasyona ekler");
        
        // Başka nesneye bakma - dynamicObject'e bakıyoruz
        if (dynamicObject != null)
        {
            transform.LookAt(dynamicObject.transform);
            Debug.Log("👁️ Bu GameObject artık dynamicObject'e bakıyor");
            Debug.Log("   • dynamicObject = Eğitim için oluşturduğumuz test nesnesi");
            Debug.Log("   • LookAt() başka bir nesneye doğru döndürür");
        }
        else
        {
            Debug.LogWarning("❌ dynamicObject bulunamadı - LookAt işlemi yapılamıyor");
        }
        
        // Pozisyonu sıfırla
        transform.position = originalPos;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        Debug.Log("🔄 Transform sıfırlandı");
        
        Debug.Log("");
        Debug.Log("🎯 TRANSFORM İPUÇLARI:");
        Debug.Log("   • position = Dünya koordinatları");
        Debug.Log("   • localPosition = Parent'a göre koordinatlar");
        Debug.Log("   • Quaternion.identity = Rotasyon yok");
        Debug.Log("   • Vector3.one = (1,1,1) ölçek");
        Debug.Log("   • Vector3.forward = (0,0,1) - İleri yön");
    }
    
    #endregion
    
    
    #region 6. GameObject Durumları
    
    /// <summary>
    /// GameObject aktif/pasif durumları
    /// </summary>
    void GameObjectStates()
    {
        Debug.Log("--- 6. GAMEOBJECT DURUMLARI ---");
        Debug.Log("👁️ Aktif/Pasif sistem nesneleri görünür/gizli yapar");
        Debug.Log("");
        
        if (dynamicObject != null)
        {
            // Aktif durumunu kontrol etme
            bool isActive = dynamicObject.activeInHierarchy;
            bool selfActive = dynamicObject.activeSelf;
            Debug.Log("🔍 GameObject Hierarchy'de aktif mi? " + isActive);
            Debug.Log("🔍 GameObject kendisi aktif mi? " + selfActive);
            Debug.Log("   • activeInHierarchy = Parent'lar da dahil");
            Debug.Log("   • activeSelf = Sadece bu nesne");
            
            // GameObject'i pasif yapma
            dynamicObject.SetActive(false);
            Debug.Log("❌ GameObject pasif yapıldı");
            Debug.Log("   • Görünmez oldu");
            Debug.Log("   • Update, FixedUpdate çalışmaz");
            Debug.Log("   • Collider'lar devre dışı");
            
            // 2 saniye bekle (gerçekte bu çalışmaz çünkü Update yoktur)
            // Bu sadece eğitim amaçlı
            
            // Tekrar aktif yapma
            dynamicObject.SetActive(true);
            Debug.Log("✅ GameObject aktif yapıldı");
            Debug.Log("   • Tekrar görünür");
            Debug.Log("   • Tüm component'ler çalışır");
            
            // GameObject ve child'ların durumu
            Debug.Log("👨‍👩‍👧‍👦 Parent-Child aktiflik:");
            Debug.Log("   • Parent pasifse, child'lar da görünmez");
            Debug.Log("   • Child pasifse, parent aktif olsa da görünmez");
        }
        else
        {
            Debug.LogError("❌ dynamicObject null - Durum işlemleri yapılamıyor");
        }
        
        Debug.Log("");
        Debug.Log("🎯 AKTİFLİK İPUÇLARI:");
        Debug.Log("   • SetActive(false) = Nesneyi gizle");
        Debug.Log("   • SetActive(true) = Nesneyi göster");
        Debug.Log("   • Pasif nesneler CPU kullanmaz");
        Debug.Log("   • Destroy yerine SetActive kullanabilirsiniz");
        Debug.Log("   • Menu'ler için ideal");
    }
    
    #endregion
    
    
    #region 7. Parent-Child Hiyerarşisi
    
    /// <summary>
    /// Parent-Child ilişkileri ve hiyerarşi işlemleri
    /// </summary>
    void ParentChildRelationships()
    {
        Debug.Log("--- 7. PARENT-CHILD HİYERAŞİSİ ---");
        Debug.Log("👨‍👩‍👧‍👦 Hierarchy sistemi = Aile ağacı gibi");
        Debug.Log("");
        
        // Yeni bir parent oluştur
        GameObject parent = new GameObject("🏠 Parent Evi");
        parent.transform.position = new Vector3(5, 0, 0);
        
        // Child nesneler oluştur
        GameObject child1 = new GameObject("👶 Çocuk 1");
        GameObject child2 = new GameObject("👶 Çocuk 2");
        GameObject grandChild = new GameObject("🍼 Torun");
        
        // Parent-child ilişkisi kurma
        child1.transform.SetParent(parent.transform);
        child2.transform.parent = parent.transform; // Alternatif yöntem
        grandChild.transform.SetParent(child1.transform); // Torun
        
        Debug.Log("✅ Parent-child ilişkileri kuruldu");
        Debug.Log("📊 Parent'ın direkt child sayısı: " + parent.transform.childCount);
        
        // Child pozisyonları ayarlama
        child1.transform.localPosition = new Vector3(-1, 0, 0);
        child2.transform.localPosition = new Vector3(1, 0, 0);
        grandChild.transform.localPosition = new Vector3(0, 1, 0);
        
        Debug.Log("📍 Local pozisyonlar ayarlandı");
        Debug.Log("   • localPosition = Parent'a göre pozisyon");
        Debug.Log("   • position = Dünya koordinatları");
        
        // Child'lara erişim
        Debug.Log("📋 CHILD LİSTESİ:");
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            Debug.Log("   • Child " + i + ": " + child.name);
            Debug.Log("     Dünya pos: " + child.position);
            Debug.Log("     Local pos: " + child.localPosition);
        }
        
        // Parent hareket ettirince child'lar da hareket eder
        Vector3 originalParentPos = parent.transform.position;
        parent.transform.position = new Vector3(10, 2, 0);
        Debug.Log("🚚 Parent hareket etti, child'lar da takip etti");
        Debug.Log("   • Child 1 yeni dünya pos: " + child1.transform.position);
        
        // Parent döndürünce child'lar da döner
        parent.transform.Rotate(0, 45, 0);
        Debug.Log("🔄 Parent döndü, child'lar da döndü");
        
        // Child'ı parent'tan ayırma
        child1.transform.SetParent(null);
        Debug.Log("✂️ Child 1 parent'tan ayrıldı (artık bağımsız)");
        Debug.Log("📊 Parent'ın kalan child sayısı: " + parent.transform.childCount);
        
        // Tüm child'ları bulma (kendisi + alt seviyeler)
        Transform[] allChildren = parent.GetComponentsInChildren<Transform>();
        Debug.Log("🔍 Parent + tüm alt seviye child'lar: " + allChildren.Length);
        Debug.Log("   • GetComponentsInChildren kendisini de sayar");
        
        // Sadece child'ları bulma (kendisi hariç)
        Transform[] onlyChildren = new Transform[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            onlyChildren[i] = parent.transform.GetChild(i);
        }
        Debug.Log("👶 Sadece direkt child'lar: " + onlyChildren.Length);
        
        // Parent'ı bulma
        if (child2.transform.parent != null)
        {
            Debug.Log("🔍 Child 2'nin parent'ı: " + child2.transform.parent.name);
        }
        
        Debug.Log("");
        Debug.Log("🎯 HİYERAŞİ İPUÇLARI:");
        Debug.Log("   • SetParent() - Parent belirleme");
        Debug.Log("   • localPosition - Parent'a göre pozisyon");
        Debug.Log("   • Parent hareket edince child'lar takip eder");
        Debug.Log("   • Parent pasifse child'lar da pasif");
        Debug.Log("   • UI için çok önemli (Canvas > Panel > Button)");
        Debug.Log("   • Silme: Parent silinirse child'lar da silinir");
    }
    
    #endregion
    
    
    #region 8. Pratik Örnekler - Interaktif Kodlar
    
    /// <summary>
    /// Mermi oluşturma örneği - SPACE tuşu ile
    /// Bu örnek Prefab sisteminin gücünü gösterir
    /// </summary>
    void InstantiateBullet()
    {
        if (bulletPrefab != null)
        {
            Vector3 spawnPosition = transform.position + transform.forward * 2;
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, transform.rotation);
            bullet.name = "🚀 Dinamik Mermi " + Time.time.ToString("F1");
            
            // Mermiyi 3 saniye sonra yok et
            Destroy(bullet, 3f);
            
            Debug.Log("🚀 Mermi oluşturuldu: " + bullet.name);
            Debug.Log("   • Pozisyon: " + spawnPosition);
            Debug.Log("   • 3 saniye sonra yok edilecek");
            Debug.Log("   • Instantiate() prefab'dan kopya oluşturur");
            Debug.Log("   • Prefab'ın tüm özelliklerini miras alır");
            Debug.Log("   • Bu yöntemle binlerce mermi oluşturabilirsiniz");
        }
        else
        {
            Debug.LogWarning("⚠️ Bullet prefab Inspector'da atanmamış!");
            Debug.LogWarning("   • Bu script'i GameObject'e ekleyin");
            Debug.LogWarning("   • Inspector'da Bullet Prefab alanını doldurun");
            Debug.LogWarning("   • Project'ten .prefab dosyası sürükleyin");
            Debug.LogWarning("   • Prefab olmadan mermi oluşturulamaz");
        }
    }
    
    /// <summary>
    /// Düşman oluşturma örneği - E tuşu ile
    /// Prefab kullanarak çeşitli düşmanlar oluşturur
    /// </summary>
    void CreateEnemy()
    {
        if (enemyPrefab != null)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-5f, 5f),
                0,
                Random.Range(-5f, 5f)
            );
            
            GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            enemy.name = "👹 Düşman_" + Random.Range(1000, 9999);
            
            // Düşmana rastgele boyut ver
            float randomScale = Random.Range(0.5f, 2f);
            enemy.transform.localScale = Vector3.one * randomScale;
            
            // Düşmana rastgele renk ver (eğer renderer varsa)
            Renderer enemyRenderer = enemy.GetComponent<Renderer>();
            if (enemyRenderer != null)
            {
                enemyRenderer.material.color = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f)
                );
            }
            
            Debug.Log("👹 Düşman oluşturuldu: " + enemy.name);
            Debug.Log("   • Pozisyon: " + randomPosition);
            Debug.Log("   • Boyut: " + randomScale.ToString("F1"));
            Debug.Log("   • Random.Range() rastgele sayı üretir");
            Debug.Log("   • Prefab'dan oluşturuldu, sonra özelleştirildi");
            Debug.Log("   • Aynı prefab'dan sınırsız düşman oluşturabilirsiniz");
        }
        else
        {
            Debug.LogWarning("⚠️ Enemy prefab Inspector'da atanmamış!");
            Debug.LogWarning("   • Inspector'da Enemy Prefab alanını doldurun");
            Debug.LogWarning("   • Prefab = Tekrar kullanılabilir GameObject şablonu");
            Debug.LogWarning("   • Bir kez oluştur, defalarca kullan mantığı");
        }
    }
    
    /// <summary>
    /// Rastgele nesne yok etme - D tuşu ile
    /// </summary>
    void DestroyRandomObject()
    {
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        
        if (allObjects.Length > 5) // En az 5 nesne varsa
        {
            // Güvenli nesne listesi
            string[] protectedNames = { "Main Camera", "Directional Light", "Canvas", "Player" };
            
            // Yok edilebilir nesneleri filtrele
            System.Collections.Generic.List<GameObject> destroyableObjects = 
                new System.Collections.Generic.List<GameObject>();
            
            foreach (GameObject obj in allObjects)
            {
                bool isProtected = false;
                
                // Korumalı isimleri kontrol et
                foreach (string protectedName in protectedNames)
                {
                    if (obj.name.Contains(protectedName))
                    {
                        isProtected = true;
                        break;
                    }
                }
                
                // Bu script'in bulunduğu nesneyi koru
                if (obj == this.gameObject)
                {
                    isProtected = true;
                }
                
                if (!isProtected)
                {
                    destroyableObjects.Add(obj);
                }
            }
            
            if (destroyableObjects.Count > 0)
            {
                GameObject randomObject = destroyableObjects[Random.Range(0, destroyableObjects.Count)];
                
                Debug.Log("💥 Nesne yok ediliyor: " + randomObject.name);
                Debug.Log("   • Pozisyon: " + randomObject.transform.position);
                Debug.Log("   • Destroy() güvenli silme yöntemidir");
                Debug.Log("   • Bir sonraki frame'de silinir");
                
                Destroy(randomObject);
            }
            else
            {
                Debug.Log("🛡️ Tüm nesneler korumalı - Hiçbiri silinmedi");
            }
        }
        else
        {
            Debug.Log("⚠️ Yok edilecek yeterli nesne yok (en az 5 olmalı)");
            Debug.Log("   • Şu anda " + allObjects.Length + " nesne var");
            Debug.Log("   • Space/E tuşları ile nesne oluşturun");
        }
        
        Debug.Log("📊 Mevcut toplam nesne sayısı: " + allObjects.Length);
    }
    
    #endregion
    
    /// <summary>
    /// GameObject yok edildiğinde çalışır - Temizlik işlemleri
    /// </summary>
    void OnDestroy()
    {
        Debug.Log("=== GAMEOBJECT EĞİTİMİ ÖZET NOTLARI ===");
        Debug.Log("🎯 TEMEL KAVRAMLAR:");
        Debug.Log("   • GameObject'ler sahnedeki HER ŞEYİN temelini oluşturur");
        Debug.Log("   • Kamera, ışık, UI, karakter - hepsi GameObject'tir");
        Debug.Log("   • Component'ler GameObject'lere işlevsellik katar");
        Debug.Log("   • Transform her GameObject'te ZORUNLUDUR");
        Debug.Log("");
        
        Debug.Log("⚡ PERFORMANS İPUÇLARI:");
        Debug.Log("   • Find metodlarını Start()'ta kullanın, Update()'te DEĞİL");
        Debug.Log("   • Referansları cache'leyin (değişkenlerde saklayın)");
        Debug.Log("   • Tag sistemi daha hızlıdır");
        Debug.Log("   • SetActive yerine Destroy kullanmayı düşünün");
        Debug.Log("");
        
        Debug.Log("🛠️ YAŞAM DÖNGÜSÜ:");
        Debug.Log("   • Instantiate ile çalışma zamanında nesne oluşturabilirsiniz");
        Debug.Log("   • Destroy ile nesneleri güvenli şekilde yok edebilirsiniz");
        Debug.Log("   • SetActive ile nesneleri geçici olarak devre dışı bırakabilirsiniz");
        Debug.Log("");
        
        Debug.Log("👨‍👩‍👧‍👦 HİYERAŞİ:");
        Debug.Log("   • Parent-Child ilişkileri hiyerarşi oluşturur");
        Debug.Log("   • Parent hareket edince child'lar takip eder");
        Debug.Log("   • Parent pasifse child'lar da pasif");
        Debug.Log("");
        
        Debug.Log("🎮 KONTROLLER:");
        Debug.Log("   • SPACE - Mermi oluştur");
        Debug.Log("   • E - Düşman oluştur");  
        Debug.Log("   • D - Rastgele nesne sil");
        Debug.Log("");
        
        Debug.Log("Bu eğitim tamamlandı! Unity GameObject'lerini artık daha iyi anlıyorsunuz! 🚀");
    }
}
