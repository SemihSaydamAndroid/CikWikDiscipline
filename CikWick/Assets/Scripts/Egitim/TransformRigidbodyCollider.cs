using UnityEngine;

/// <summary>
/// Unity'nin En Temel 3 Component'i: Transform, Rigidbody, Collider Detaylı Eğitimi
/// 
/// === TRANSFORM NEDİR? ===
/// Transform = Her GameObject'in ZORUNLU component'i
/// • Position (Pozisyon) - Nerede?
/// • Rotation (Rotasyon) - Nasıl döndürülmüş?
/// • Scale (Ölçek) - Ne kadar büyük?
/// • Parent-Child ilişkileri
/// 
/// === RIGIDBODY NEDİR? ===
/// Rigidbody = Fizik simülasyonu component'i
/// • Gravity (Yerçekimi) - Düşme efekti
/// • Mass (Kütle) - Ağırlık
/// • Drag (Hava direnci) - Yavaşlama
/// • Velocity (Hız) - Hareket hızı
/// • Forces (Kuvvetler) - İtme, çekme
/// 
/// === COLLIDER NEDİR? ===
/// Collider = Çarpışma algılama component'i --> iki nesnenin etkileşimi için asıl bu lazım; collider'lar birbirine değince etkileşim oluyor. Top plane'e düştüğünde düştüğünü anlaması için hem topta hem plane'de collider olmalı.
/// • BoxCollider - Kutu şeklinde çarpışma
/// • SphereCollider - Küre şeklinde çarpışma
/// • CapsuleCollider - Kapsül şeklinde çarpışma
/// • MeshCollider - Özel şekil çarpışma
/// • Trigger - Geçilebilir çarpışma sensörü
/// • IsTrigger - Fiziksel çarpışma vs sensör ---> COIN toplama gibi düşün; direct çarpışma yok, sadece geçiş algılama gerekiyor.
/// </summary>
public class TransformRigidbodyCollider : MonoBehaviour
{
    #region Public Variables - Inspector'da Görünen Değişkenler
    
    [Header("Test Nesneleri")]
    [Tooltip("Transform testleri için hedef nesne")]
    public Transform targetTransform;
    
    [Tooltip("Rigidbody testleri için nesne")]
    public Rigidbody targetRigidbody;
    
    [Tooltip("Collider testleri için nesne")]
    public Collider targetCollider;
    
    [Header("Fizik Ayarları")]
    [Tooltip("Uygulanacak kuvvet miktarı")]
    public float forceAmount = 10f;
    
    [Tooltip("Hareket hızı")]
    public float moveSpeed = 5f;
    
    [Tooltip("Döndürme hızı")]
    public float rotationSpeed = 90f;
    
    #endregion
    
    #region Private Variables - Gizli Değişkenler
    
    /// <summary>
    /// Test için dinamik olarak oluşturulan nesneler
    /// </summary>
    private GameObject testCube;
    private GameObject testSphere;
    private GameObject testCapsule;
    private GameObject triggerZone;
    
    /// <summary>
    /// Çarpışma sayaçları
    /// </summary>
    private int collisionCount = 0;
    private int triggerCount = 0;
    
    #endregion
    
    #region Unity Lifecycle Methods - Unity Yaşam Döngüsü
    
    void Start()
    {
        Debug.Log("=== TRANSFORM, RIGIDBODY, COLLIDER EĞİTİMİ ===");
        
        // 1. Transform Eğitimi
        ExplainTransform();
        
        // 2. Rigidbody Eğitimi
        ExplainRigidbody();
        
        // 3. Collider Eğitimi
        ExplainCollider();
        
        // 4. Test nesneleri oluştur
        CreateTestObjects();
        
        // 5. Component kombinasyonları
        CombineComponents();
    }
    
    void Update()
    {
        // Interaktif kontroller
        HandleInput();
        
        // Transform animasyonları
        AnimateTransforms();
    }
    
    void FixedUpdate()
    {
        // Fizik işlemleri FixedUpdate'te yapılır
        HandlePhysicsMovement();
    }
    
    #endregion
    
    #region 1. Transform Component Detaylı Eğitimi
    
    /// <summary>
    /// Transform component'inin tüm özelliklerini açıklar
    /// </summary>
    void ExplainTransform()
    {
        Debug.Log("--- 1. TRANSFORM COMPONENT (DETAYLI) ---");
        Debug.Log("📐 Transform = Her GameObject'in TEMELİ");
        Debug.Log("");
        
        Debug.Log("🎯 TRANSFORM'UN 3 ANA ÖZELLİĞİ:");
        Debug.Log("   1️⃣ POSITION (Pozisyon) - Nerede?");
        Debug.Log("   2️⃣ ROTATION (Rotasyon) - Nasıl döndürülmüş?");
        Debug.Log("   3️⃣ SCALE (Ölçek) - Ne kadar büyük?");
        Debug.Log("");
        
        // Bu GameObject'in Transform bilgileri
        Debug.Log("🔍 BU GAMEOBJECT'İN TRANSFORM BİLGİLERİ:");
        Debug.Log("   • Pozisyon: " + transform.position);
        Debug.Log("   • Rotasyon: " + transform.rotation.eulerAngles);
        Debug.Log("   • Ölçek: " + transform.localScale);
        Debug.Log("");
        
        Debug.Log("📍 POZİSYON (POSITION) ÖRNEKLERİ:");
        Vector3 originalPos = transform.position;
        
        // Pozisyon değiştirme
        transform.position = new Vector3(5, 2, 3);
        Debug.Log("   ✅ Yeni pozisyon: " + transform.position);
        Debug.Log("   • X=5 (sağ), Y=2 (yukarı), Z=3 (ileri)");
        
        // Pozisyon ekleme
        transform.position += Vector3.up * 2; // 2 birim yukarı
        Debug.Log("   ⬆️ 2 birim yukarı hareket: " + transform.position);
        
        // Translate ile hareket
        transform.Translate(Vector3.right * 3); // 3 birim sağa
        Debug.Log("   ➡️ 3 birim sağa Translate: " + transform.position);
        
        // Local vs World pozisyon
        Debug.Log("   🌍 World Position: " + transform.position);
        Debug.Log("   🏠 Local Position: " + transform.localPosition);
        Debug.Log("   • World = Dünya koordinatları");
        Debug.Log("   • Local = Parent'a göre koordinatlar");
        
        // Pozisyonu eski haline getir
        transform.position = originalPos;
        Debug.Log("   🔄 Pozisyon sıfırlandı");
        Debug.Log("");
        
        Debug.Log("🔄 ROTASYON (ROTATION) ÖRNEKLERİ:");
        Quaternion originalRot = transform.rotation;
        
        // Euler açıları ile rotasyon
        transform.rotation = Quaternion.Euler(0, 45, 0);
        Debug.Log("   ✅ Y ekseninde 45° döndürüldü: " + transform.rotation.eulerAngles);
        
        // Rotate ile döndürme
        transform.Rotate(0, 90, 0);
        Debug.Log("   🔄 90° daha döndürüldü: " + transform.rotation.eulerAngles);
        
        // Bir yöne bakma
        if (targetTransform != null)
        {
            transform.LookAt(targetTransform);
            Debug.Log("   👁️ Target'a bakıyor");
        }
        
        // Rotasyonu sıfırla
        transform.rotation = originalRot;
        Debug.Log("   🔄 Rotasyon sıfırlandı");
        Debug.Log("");
        
        Debug.Log("📏 ÖLÇEK (SCALE) ÖRNEKLERİ:");
        Vector3 originalScale = transform.localScale;
        
        // Ölçek değiştirme
        transform.localScale = new Vector3(2, 2, 2);
        Debug.Log("   ✅ 2 katına büyütüldü: " + transform.localScale);
        
        // Sadece Y ekseninde büyütme
        transform.localScale = new Vector3(1, 3, 1);
        Debug.Log("   ⬆️ Sadece boyda 3 katına: " + transform.localScale);
        
        // Ölçeği sıfırla
        transform.localScale = originalScale;
        Debug.Log("   🔄 Ölçek sıfırlandı");
        Debug.Log("");
        
        Debug.Log("🎯 TRANSFORM İPUÇLARI:");
        Debug.Log("   • Transform.position = Dünya koordinatları");
        Debug.Log("   • Transform.localPosition = Parent'a göre");
        Debug.Log("   • Quaternion.identity = Sıfır rotasyon");
        Debug.Log("   • Vector3.one = (1,1,1) normal ölçek");
        Debug.Log("   • Vector3.zero = (0,0,0) sıfır pozisyon");
        Debug.Log("   • Translate() = Mevcut pozisyona ekler");
        Debug.Log("   • Rotate() = Mevcut rotasyona ekler");
    }
    
    #endregion
    
    #region 2. Rigidbody Component Detaylı Eğitimi
    
    /// <summary>
    /// Rigidbody component'inin tüm özelliklerini açıklar
    /// </summary>
    void ExplainRigidbody()
    {
        Debug.Log("--- 2. RIGIDBODY COMPONENT (DETAYLI) ---");
        Debug.Log("⚖️ Rigidbody = FİZİK SİMÜLASYONU");
        Debug.Log("");
        
        Debug.Log("🎯 RIGIDBODY NEDİR?");
        Debug.Log("   • GameObject'e fizik kuralları kazandırır");
        Debug.Log("   • Gravity (yerçekimi) etkisi");
        Debug.Log("   • Mass (kütle) - ağırlık");
        Debug.Log("   • Velocity (hız) - hareket");
        Debug.Log("   • Forces (kuvvetler) - itme, çekme");
        Debug.Log("   • Constraints (kısıtlamalar) - hareket sınırlama");
        Debug.Log("");
        
        // Dinamik test nesnesi oluştur
        GameObject rbTestObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rbTestObject.name = "Rigidbody Test Küpü";
        rbTestObject.transform.position = new Vector3(10, 5, 0);
        
        // Rigidbody ekle
        Rigidbody rb = rbTestObject.AddComponent<Rigidbody>();
        Debug.Log("✅ Test nesnesi oluşturuldu ve Rigidbody eklendi");
        Debug.Log("");
        
        Debug.Log("⚖️ KÜTLE (MASS) ÖRNEKLERİ:");
        rb.mass = 1f;
        Debug.Log("   • Varsayılan kütle: " + rb.mass + " kg");
        rb.mass = 10f;
        Debug.Log("   • Ağır nesne: " + rb.mass + " kg (daha yavaş hareket eder)");
        rb.mass = 0.1f;
        Debug.Log("   • Hafif nesne: " + rb.mass + " kg (daha hızlı hareket eder)");
        rb.mass = 1f; // Normale döndür
        Debug.Log("");
        
        Debug.Log("🌍 YERÇEKİMİ (GRAVITY) ÖRNEKLERİ:");
        Debug.Log("   • useGravity = true: Nesne düşer");
        Debug.Log("   • useGravity = false: Uzayda yüzer");
        rb.useGravity = false;
        Debug.Log("   ✅ Gravity kapatıldı - nesne artık düşmez");
        rb.useGravity = true;
        Debug.Log("   ✅ Gravity açıldı - nesne düşecek");
        Debug.Log("");
        
        Debug.Log("💨 HAVA DİRENCİ (DRAG) ÖRNEKLERİ:");
        rb.linearDamping = 0f;
        Debug.Log("   • Drag = 0: Sürtünme yok, sonsuza kadar hareket");
        rb.linearDamping = 5f;
        Debug.Log("   • Drag = 5: Yüksek sürtünme, çabuk durur");
        rb.linearDamping = 1f; // Normale döndür
        Debug.Log("   • Drag = 1: Normal sürtünme");
        Debug.Log("");
        
        Debug.Log("🔄 AÇISAL DRAG (ANGULAR DRAG) ÖRNEKLERİ:");
        rb.angularDamping = 0.05f;
        Debug.Log("   • Angular Drag = " + rb.angularDamping + ": Dönerken yavaşlama");
        Debug.Log("");
        
        Debug.Log("🚀 KUVVET (FORCE) ÖRNEKLERİ:");
        Debug.Log("   • AddForce(): Sürekli kuvvet (roket motoru gibi)");
        Debug.Log("   • AddForce(ForceMode.Impulse): Anlık darbe (mermi gibi)");
        Debug.Log("   • AddForce(ForceMode.VelocityChange): Hız değişimi");
        Debug.Log("   • AddForce(ForceMode.Acceleration): İvme");
        
        // Örnekler
        rb.AddForce(Vector3.up * 100f); // Yukarı itme
        Debug.Log("   ✅ Yukarı doğru 100N kuvvet uygulandı");
        
        rb.AddForce(Vector3.forward * 50f, ForceMode.Impulse); // İleri darbe
        Debug.Log("   ✅ İleri doğru 50N darbe uygulandı");
        Debug.Log("");
        
        Debug.Log("⚡ HIZ (VELOCITY) ÖRNEKLERİ:");
        rb.linearVelocity = Vector3.right * 5f; // Sağa 5 m/s
        Debug.Log("   • Velocity: " + rb.linearVelocity + " m/s");
        Debug.Log("   • Sağa doğru 5 m/s hız verildi");
        
        rb.angularVelocity = Vector3.up * 2f; // Y ekseninde 2 rad/s
        Debug.Log("   • Angular Velocity: " + rb.angularVelocity + " rad/s");
        Debug.Log("   • Y ekseninde 2 rad/s döndürme hızı");
        Debug.Log("");
        
        Debug.Log("🔒 KISITLAMALAR (CONSTRAINTS) ÖRNEKLERİ:");
        Debug.Log("   • FreezePosition: X, Y, Z pozisyon kilitleme");
        Debug.Log("   • FreezeRotation: X, Y, Z rotasyon kilitleme");
        
        // Y pozisyonunu kilitle (düşmesin)
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        Debug.Log("   ✅ Y pozisyonu kilitlendi - nesne düşemez");
        
        // Tüm rotasyonu kilitle
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        Debug.Log("   ✅ Rotasyon kilitlendi - nesne dönemez");
        
        // Kısıtlamaları kaldır
        rb.constraints = RigidbodyConstraints.None;
        Debug.Log("   ✅ Tüm kısıtlamalar kaldırıldı");
        Debug.Log("");
        
        Debug.Log("🎯 RIGIDBODY İPUÇLARI:");
        Debug.Log("   • Rigidbody olmadan fizik yok");
        Debug.Log("   • Mass arttıkça nesne ağırlaşır");
        Debug.Log("   • Drag arttıkça nesne yavaşlar");
        Debug.Log("   • Forces ile gerçekçi hareket");
        Debug.Log("   • Velocity ile direkt hız kontrolü");
        Debug.Log("   • Constraints ile hareket sınırlama");
        Debug.Log("   • FixedUpdate'te fizik işlemleri yapın");
        Debug.Log("   • Transform.position yerine rb.MovePosition kullanın");
    }
    
    #endregion
    
    #region 3. Collider Component Detaylı Eğitimi
    
    /// <summary>
    /// Collider component'inin tüm özelliklerini açıklar
    /// </summary>
    void ExplainCollider()
    {
        Debug.Log("--- 3. COLLIDER COMPONENT (DETAYLI) ---");
        Debug.Log("🚧 Collider = ÇARPILMA VE ALGILAMA");
        Debug.Log("");
        
        Debug.Log("🎯 COLLIDER NEDİR?");
        Debug.Log("   • GameObject'in fiziksel sınırlarını belirler");
        Debug.Log("   • Diğer nesnelerle çarpışma algılar");
        Debug.Log("   • Çarpışma sonrası tepkileri belirler");
        Debug.Log("   • Trigger ile geçiş algılama");
        Debug.Log("");
        
        Debug.Log("🔍 COLLIDER TİPLERİ:");
        Debug.Log("   • BoxCollider: Kutu şeklinde çarpışma");
        Debug.Log("   • SphereCollider: Küre şeklinde çarpışma");
        Debug.Log("   • CapsuleCollider: Kapsül şeklinde çarpışma");
        Debug.Log("   • MeshCollider: Özel şekil çarpışma");
        Debug.Log("   • Trigger: Geçilebilir çarpışma sensörü");
        Debug.Log("   • IsTrigger: Fiziksel çarpışma vs sensör");
        Debug.Log("");
        
        // Test nesnesine BoxCollider ekle
        if (targetCollider != null)
        {
            targetCollider.isTrigger = false; // Önceki ayarları sıfırla
            Debug.Log("✅ Mevcut Collider sıfırlandı");
        }
        
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        Debug.Log("✅ Yeni BoxCollider eklendi");
        
        // Collider ayarları
        Debug.Log("⚙️ COLLIDER AYARLARI:");
        boxCollider.center = Vector3.zero;
        Debug.Log("   • Center: " + boxCollider.center);
        boxCollider.size = new Vector3(1, 2, 1);
        Debug.Log("   • Size: " + boxCollider.size);
        boxCollider.isTrigger = true;
        Debug.Log("   • IsTrigger: " + boxCollider.isTrigger);
        Debug.Log("");
        
        Debug.Log("🔄 COLLIDER ÖZELLİKLERİ:");
        Debug.Log("   • enabled: Collider'ı açıp kapatır");
        Debug.Log("   • isTrigger: Geçiş algılama (çarpışma değil)");
        Debug.Log("   • material: Fiziksel malzeme (sürtünme, yaylanma)");
        Debug.Log("");
        
        // Collider özelliklerini değiştir
        boxCollider.enabled = false;
        Debug.Log("   ✅ Collider devre dışı bırakıldı");
        boxCollider.enabled = true;
        Debug.Log("   ✅ Collider tekrar etkinleştirildi");
        Debug.Log("");
        
        // Rigidbody ile etkileşim
        if (targetRigidbody != null)
        {
            targetRigidbody.isKinematic = true;
            Debug.Log("   ✅ Rigidbody kinematik yapıldı (fizik etkisi yok)");
        }
        
        // Trigger ile etkileşim
        if (boxCollider.isTrigger)
        {
            Debug.Log("   ✅ Trigger olarak ayarlandı");
        }
        
        // MeshCollider örneği (karmaşık şekil)
        GameObject meshTestObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        meshTestObject.name = "MeshCollider Test Küre";
        meshTestObject.transform.position = new Vector3(15, 5, 0);
        
        MeshCollider meshCollider = meshTestObject.AddComponent<MeshCollider>();
        meshCollider.convex = true; // İç içe geçmişse düzleştir
        Debug.Log("✅ Yeni MeshCollider eklendi (konveks)");
        Debug.Log("");
        
        Debug.Log("🎯 COLLIDER İPUÇLARI:");
        Debug.Log("   • Collider olmadan çarpışma algılanamaz");
        Debug.Log("   • IsTrigger = true ise fiziksel çarpışma yok");
        Debug.Log("   • Rigidbody ile birlikte kullanın");
        Debug.Log("   • MeshCollider için 'convex' ayarını kontrol edin");
    }
    
    #endregion
    
    #region Test Nesneleri ve Component Kombinasyonları
    
    void CreateTestObjects()
    {
        Debug.Log("--- TEST NESNELERİ OLUŞTURULUYOR ---");
        
        // 1. Test Küpü (Box)
        testCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        testCube.name = "Test Küpü";
        testCube.transform.position = new Vector3(0, 1, 0);
        testCube.transform.localScale = new Vector3(1, 1, 1);
        Debug.Log("✅ Test Küpü oluşturuldu");
        
        // 2. Test Küresi (Sphere)
        testSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        testSphere.name = "Test Küresi";
        testSphere.transform.position = new Vector3(2, 1, 0);
        testSphere.transform.localScale = new Vector3(1, 1, 1);
        Debug.Log("✅ Test Küresi oluşturuldu");
        
        // 3. Test Kapsülü (Capsule)
        testCapsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        testCapsule.name = "Test Kapsülü";
        testCapsule.transform.position = new Vector3(4, 1, 0);
        testCapsule.transform.localScale = new Vector3(1, 1, 1);
        Debug.Log("✅ Test Kapsülü oluşturuldu");
        
        // 4. Trigger Zone
        triggerZone = GameObject.CreatePrimitive(PrimitiveType.Cube);
        triggerZone.name = "Trigger Zone";
        triggerZone.transform.position = new Vector3(6, 1, 0);
        triggerZone.transform.localScale = new Vector3(1, 1, 1);
        Debug.Log("✅ Trigger Zone oluşturuldu");
        
        // Test nesnelerine Rigidbody ekle
        Rigidbody rb = testCube.AddComponent<Rigidbody>();
        rb.mass = 1;
        rb.useGravity = true;
        Debug.Log("✅ Test Küpü'ne Rigidbody eklendi");
        
        rb = testSphere.AddComponent<Rigidbody>();
        rb.mass = 0.5f;
        rb.useGravity = true;
        Debug.Log("✅ Test Küresi'ne Rigidbody eklendi");
        
        rb = testCapsule.AddComponent<Rigidbody>();
        rb.mass = 1.5f;
        rb.useGravity = true;
        Debug.Log("✅ Test Kapsülü'ne Rigidbody eklendi");
        
        // Trigger Zone için özel ayarlar
        triggerZone.GetComponent<Collider>().isTrigger = true;
        Debug.Log("✅ Trigger Zone için IsTrigger ayarlandı");
    }
    
    void CombineComponents()
    {
        Debug.Log("--- COMPONENT KOMBINASYONLARI ---");
        
        // Transform + Rigidbody
        Debug.Log("1. Transform + Rigidbody");
        transform.position = new Vector3(0, 5, 0);
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = 1;
        rb.useGravity = true;
        Debug.Log("   ✅ Rigidbody eklendi");
        
        // Transform + Collider
        Debug.Log("2. Transform + Collider");
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(1, 1, 1);
        Debug.Log("   ✅ BoxCollider eklendi");
        
        // Rigidbody + Collider
        Debug.Log("3. Rigidbody + Collider");
        rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = 1;
        rb.useGravity = true;
        Debug.Log("   ✅ Rigidbody eklendi");
        
        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = 0.5f;
        Debug.Log("   ✅ SphereCollider eklendi");
        
        // Transform + Rigidbody + Collider
        Debug.Log("4. Transform + Rigidbody + Collider");
        transform.position = new Vector3(0, 0, 0);
        rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = 1;
        rb.useGravity = true;
        Debug.Log("   ✅ Rigidbody eklendi");
        
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.convex = true;
        Debug.Log("   ✅ MeshCollider eklendi (konveks)");
        
        // Özel kombinasyon örneği
        Debug.Log("5. Özel Kombinasyon");
        GameObject customObject = new GameObject("Özel Nesne");
        customObject.transform.position = new Vector3(10, 0, 0);
        
        // Transform bileşeni ekle
        Transform customTransform = customObject.AddComponent<Transform>();
        customTransform.position = new Vector3(10, 1, 0);
        Debug.Log("   ✅ Transform eklendi");
        
        // Rigidbody ekle
        rb = customObject.AddComponent<Rigidbody>();
        rb.mass = 2;
        rb.useGravity = false;
        Debug.Log("   ✅ Rigidbody eklendi");
        
        // BoxCollider ekle
        boxCollider = customObject.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(1, 2, 1);
        Debug.Log("   ✅ BoxCollider eklendi");
        
        // SphereCollider ekle
        sphereCollider = customObject.AddComponent<SphereCollider>();
        sphereCollider.radius = 0.5f;
        Debug.Log("   ✅ SphereCollider eklendi");
        
        // MeshCollider ekle
        meshCollider = customObject.AddComponent<MeshCollider>();
        meshCollider.convex = true;
        Debug.Log("   ✅ MeshCollider eklendi (konveks)");
        
        // Tag ve Layer ayarları
        customObject.tag = "Untagged";
        customObject.layer = LayerMask.NameToLayer("Default");
        Debug.Log("   ✅ Tag ve Layer ayarlandı");
    }
    
    #endregion
    
    #region Interaktif Kontroller ve Animasyonlar
    
    void HandleInput()
    {
        // Pozisyon kontrolü
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= Vector3.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        
        // Rotasyon kontrolü
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
    
    void AnimateTransforms()
    {
        // Basit bir döngüsel hareket
        float newY = Mathf.Sin(Time.time) * 2;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    
    void HandlePhysicsMovement()
    {
        // Rigidbody ile hareket
        if (targetRigidbody != null)
        {
            targetRigidbody.AddForce(Vector3.forward * forceAmount);
        }
    }
    
    #endregion
    
    #region Çarpışma ve Trigger Olayları
    
    void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
        Debug.Log("Çarpışma! Toplam çarpışma sayısı: " + collisionCount);
    }
    
    void OnTriggerEnter(Collider other)
    {
        triggerCount++;
        Debug.Log("Trigger! Toplam trigger sayısı: " + triggerCount);
    }
    
    #endregion
}
