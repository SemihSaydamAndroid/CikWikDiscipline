
#region namespace
// namespace someNamespace {} şeklinde bir namespace kendin de oluşturabilirsin.
// bunu yaptıktan sonra içine aşağıdaki class'ı komple de alabilirsin mesela.
// methodları filan da public yaptıktan sonra başka bir c# dosyasında en üste
// using someNamespace; yazıp bu class'ı kullanabilirsin. 
// büyük çaplı projelerde namespace kullanmak kodları organize etmek için çok önemli.
#endregion

//bunlara namespace deniyor // kütüphane
using UnityEngine; // unity namespace'i
using System.Collections.Generic;  //c# namespace'i // List<T> ve diğer collection'lar için

/*
 * UNITY ATTRIBUTE'LARI VE ÖZEL YAPILAR AÇIKLAMALARI:
 * 
 * [Header("Başlık")] - Inspector'da görsel başlık oluşturur
 * [Space] - Inspector'da boşluk ekler
 * [Range(min, max)] - int/float değerler için slider oluşturur
 * [Tooltip("açıklama")] - Inspector'da fare ile üzerine gelindiğinde açıklama gösterir
 * [SerializeField] - private değişkenleri Inspector'da görünür yapar
 * [HideInInspector] - public değişkenleri Inspector'da gizler
 * 
 * UNITY LIFECYCLE METHODS:
 * Start() - İlk frame'de bir kez çağrılır
 * Update() - Her frame'de çağrılır (60+ fps)
 * FixedUpdate() - Sabit aralıklarla çağrılır (fizik için)
 * LateUpdate() - Tüm Update'ler bitince çağrılır
 * 
 * UNITY INPUT SİSTEMİ:
 * Input.GetKeyDown() - Tuşa basıldığı an
 * Input.GetKey() - Tuş basılı tutulduğu sürece
 * Input.GetKeyUp() - Tuş bırakıldığı an
 */

#region monoBehaviour
// MonoBehaviour - Unity'nin temel sınıfı- oyun objeleri için temel davranışları sağlar
// Bu sınıfı miras alarak Unity'de script yazabiliriz. Oyun ile kodlar arasında iletişim sağlar.
// Bundan kalıtım alınarak script yazılır.
#endregion

#region public private protected
// public - Her yerden erişilebilir (başka scriptlerden de) başka class'lar erişebilir.
// private - Sadece bu class içinde erişilebilir, başka class'lar erişemez. Eğer direkt int x = 5 dersen başına bir şey yazmazsan o da private olur.
// protected - Sadece bu class ve miras alan class'lar erişebilir. Yani bu class'ın child'ları erişebilir.

// public'ler inspector'da görünür ve değiştirilebilir. 
// her şeyi public yapmak iyi bir uygulama değildir. Çünkü başka yerlerden erişilebilir ve değiştirilebilir.
// buda karmaşaya neden olabilir. Dikkatli olmak gerekir.
[SerializeField]    // private değişkeni Inspector'da görünür yapar

# endregion

public class MethodsIfElseEtc : MonoBehaviour
{
    // [Header] attribute'u: Unity Inspector'da değişkenleri gruplar halinde organize eder
    // Inspector'da "Oyuncu Durumu" başlığı altında bu değişkenler görünür
    // Sadece görsel düzenleme içindir, kod işlevselliğini etkilemez
    [Header("Oyuncu Durumu")]

    // public değişkenler Unity Inspector'da görünür ve değiştirilebilir
    // Inspector'dan verilen değerler kod'daki varsayılan değerleri geçersiz kılar
    public int playerHealth = 100;        // Inspector'da Health değeri ayarlanabilir
    public int playerScore = 0;           // Skor değeri Inspector'dan değiştirilebilir
    public float playerSpeed = 5f;        // Hız değeri Inspector'da slider olarak görünür
    public bool isGameActive = true;      // Inspector'da checkbox olarak görünür

    // İkinci Header grubu - Inspector'da yeni bir bölüm oluşturur
    [Header("Enum Örnekleri")]

    // Enum'lar Inspector'da dropdown menu olarak görünür
    // Kullanıcı sadece tanımlı değerler arasından seçim yapabilir
    public PlayerState currentState = PlayerState.Idle;    // Dropdown: Idle, Walking, Running, etc.
    public WeaponType currentWeapon = WeaponType.Sword;     // Dropdown: Sword, Bow, Magic, Fist

    // Diğer Unity Attribute örnekleri:
    [Header("Unity Attribute Örnekleri")]

    [Range(0, 100)]  // Inspector'da 0-100 arası slider oluşturur
    [Tooltip("Oyuncunun saldırı gücü (0-100 arası)")]  // Fare ile üzerine gelindiğinde açıklama gösterir
    public int attackPower = 50;

    [Space(10)]  // Inspector'da 10 pixel boşluk bırakır

    [SerializeField]  // private değişkeni Inspector'da görünür yapar
    private string secretMessage = "Bu private ama Inspector'da görünür!";

    [HideInInspector]  // public değişkeni Inspector'da gizler
    public int hiddenValue = 999;

    // Enum tanımlamaları - Unity'de dropdown menüler için kullanılır
    // public enum'lar Inspector'da otomatik olarak dropdown olarak görünür
    // Kod'da daha okunabilir ve hata yapmayı önler (magic number yerine)
    public enum PlayerState
    {
        Idle,       // 0 değeri (varsayılan)
        Walking,    // 1 değeri
        Running,    // 2 değeri
        Jumping,    // 3 değeri
        Attacking,  // 4 değeri
        Dead        // 5 değeri
    }

    public enum WeaponType
    {
        Sword,      // 0 değeri
        Bow,        // 1 değeri
        Magic,      // 2 değeri
        Fist        // 3 değeri
    }

    // Start() - Unity'nin MonoBehaviour lifecycle method'u
    // GameObject ilk aktif edildiğinde ve ilk Update()'ten önce bir kez çağrılır
    // Başlangıç kurulumları için kullanılır
    void Start()
    {
        Debug.Log("=== C# Kontrol Yapıları Örnekleri ===");

        // Tüm örnekleri çalıştır
        IfElseExamples();
        SwitchCaseExamples();
        ForLoopExamples();
        WhileLoopExamples();
        ForeachLoopExamples();
        ArrayAndListExamples();  // Yeni eklenen method
        MethodExamples();

        Debug.Log("=== Örnekler Tamamlandı ===");
    }

    // Update() - Unity'nin MonoBehaviour lifecycle method'u
    // Her frame'de (saniyede 60+ kez) çağrılır
    // Kullanıcı input'u, hareket, sürekli kontrol gereken işlemler için kullanılır
    void Update()
    {
        // Oyun aktifse input kontrolü yap
        if (isGameActive)
        {
            HandleInput();  // Her frame input kontrolü
        }
    }

    #region IF-ELSE Örnekleri
    void IfElseExamples()
    {
        // Debug.Log() - Unity'ye özel console output method'u
        // Unity Editor'ın Console penceresinde görünür
        // Oyun çalışırken debug bilgileri için kullanılır
        Debug.Log("\n--- IF-ELSE Örnekleri ---");

        // Basit if-else
        if (playerHealth > 50)
        {
            Debug.Log("Oyuncu sağlıklı!");
        }
        else
        {
            Debug.Log("Oyuncu yaralı!");
        }

        // if-else if-else
        if (playerHealth <= 0)
        {
            Debug.Log("Oyuncu öldü!");
            currentState = PlayerState.Dead;
        }
        else if (playerHealth < 30)
        {
            Debug.Log("Oyuncu kritik durumda!");
        }
        else if (playerHealth < 70)
        {
            Debug.Log("Oyuncu orta durumda");
        }
        else
        {
            Debug.Log("Oyuncu sağlıklı!");
        }

        // Mantıksal operatörler
        if (playerHealth > 0 && isGameActive)
        {
            Debug.Log("Oyun devam ediyor!");
        }

        if (playerHealth <= 0 || !isGameActive)
        {
            Debug.Log("Oyun durdu!");
        }

        // Ternary operator (koşul ? doğruysa : yanlışsa)
        string healthStatus = playerHealth > 50 ? "İyi" : "Kötü";
        Debug.Log($"Sağlık durumu: {healthStatus}");

        // Nested if (iç içe if)
        if (isGameActive)
        {
            if (playerHealth > 0)
            {
                if (playerScore > 100)
                {
                    Debug.Log("Yüksek skorlu aktif oyuncu!");
                }
            }
        }
    }
    #endregion

    #region SWITCH-CASE Örnekleri
    void SwitchCaseExamples()
    {
        Debug.Log("\n--- SWITCH-CASE Örnekleri ---");

        // Enum ile switch-case
        switch (currentState)
        {
            case PlayerState.Idle:
                Debug.Log("Oyuncu boşta bekliyor");
                playerSpeed = 0f;
                break;

            case PlayerState.Walking:
                Debug.Log("Oyuncu yürüyor");
                playerSpeed = 3f;
                break;

            case PlayerState.Running:
                Debug.Log("Oyuncu koşuyor");
                playerSpeed = 8f;
                break;

            case PlayerState.Jumping:
                Debug.Log("Oyuncu zıplıyor");
                break;

            case PlayerState.Attacking:
                Debug.Log("Oyuncu saldırıyor");
                AttackWithCurrentWeapon();
                break;

            case PlayerState.Dead:
                Debug.Log("Oyuncu öldü - Oyun bitti");
                isGameActive = false;
                break;

            default:
                Debug.Log("Bilinmeyen oyuncu durumu!");
                break;
        }

        // Silah türü switch-case
        switch (currentWeapon)
        {
            case WeaponType.Sword:
                Debug.Log("Kılıç seçildi - Yakın mesafe saldırısı");
                break;

            case WeaponType.Bow:
                Debug.Log("Yay seçildi - Uzak mesafe saldırısı");
                break;

            case WeaponType.Magic:
                Debug.Log("Büyü seçildi - Sihirli saldırı");
                break;

            case WeaponType.Fist:
                Debug.Log("Yumruk seçildi - Temel saldırı");
                break;
        }

        // Sayı ile switch-case
        int dayOfWeek = 3;
        switch (dayOfWeek)
        {
            case 1:
                Debug.Log("Pazartesi - Hafta başı!");
                break;
            case 2:
            case 3:
            case 4:
                Debug.Log("Haftanın ortası");
                break;
            case 5:
                Debug.Log("Cuma - Hafta sonu yaklaşıyor!");
                break;
            case 6:
            case 7:
                Debug.Log("Hafta sonu - Dinlenme zamanı!");
                break;
            default:
                Debug.Log("Geçersiz gün!");
                break;
        }
    }
    #endregion

    #region FOR Döngüsü Örnekleri
    void ForLoopExamples()
    {
        Debug.Log("\n--- FOR Döngüsü Örnekleri ---");

        // Basit for döngüsü
        Debug.Log("1'den 5'e kadar sayma:");
        for (int i = 1; i <= 5; i++)
        {
            Debug.Log($"Sayı: {i}");
        }

        // Geriye doğru for döngüsü
        Debug.Log("5'ten 1'e kadar geriye sayma:");
        for (int i = 5; i >= 1; i--)
        {
            Debug.Log($"Geri sayım: {i}");
        }

        // Çift sayılar
        Debug.Log("0'dan 10'a kadar çift sayılar:");
        for (int i = 0; i <= 10; i += 2)
        {
            Debug.Log($"Çift sayı: {i}");
        }

        // Dizi ile for döngüsü
        string[] playerNames = { "Ali", "Ayşe", "Mehmet", "Fatma", "Ahmet" };
        Debug.Log("Oyuncu listesi:");
        for (int i = 0; i < playerNames.Length; i++)
        {
            Debug.Log($"Oyuncu {i + 1}: {playerNames[i]}");
        }

        // İç içe for döngüsü (2D grid)
        Debug.Log("3x3 Grid oluşturma:");
        for (int row = 0; row < 3; row++)
        {
            string line = "";
            for (int col = 0; col < 3; col++)
            {
                line += $"[{row},{col}] ";
            }
            Debug.Log(line);
        }

        // Break ve continue kullanımı
        Debug.Log("Break ve Continue örneği:");
        for (int i = 1; i <= 10; i++)
        {
            if (i == 5)
            {
                Debug.Log($"Sayı {i} atlandı (continue)");
                continue; // 5'i atla
            }

            if (i == 8)
            {
                Debug.Log($"Sayı {i}'de döngü durdu (break)");
                break; // 8'de dur
            }

            Debug.Log($"İşlenen sayı: {i}");
        }
    }
    #endregion

    #region WHILE Döngüsü Örnekleri
    void WhileLoopExamples()
    {
        Debug.Log("\n--- WHILE Döngüsü Örnekleri ---");

        // Basit while döngüsü
        int counter = 1;
        Debug.Log("While döngüsü ile 1'den 5'e sayma:");
        while (counter <= 5)
        {
            Debug.Log($"Counter: {counter}");
            counter++;
        }

        // Do-while döngüsü
        int number = 10;
        Debug.Log("Do-while döngüsü (en az bir kez çalışır):");
        do
        {
            Debug.Log($"Sayı: {number}");
            number--;
        } while (number > 7);

        // Oyun döngüsü simülasyonu
        int playerLives = 3;
        int currentLevel = 1;

        Debug.Log("Oyun simülasyonu:");
        while (playerLives > 0 && currentLevel <= 3)
        {
            Debug.Log($"Level {currentLevel} - Kalan can: {playerLives}");

            // Random.Range() - Unity'ye özel rastgele sayı üretici
            // Random.Range(0, 2) -> 0 veya 1 değeri döner
            // Random.Range(min, max) -> min dahil, max hariç
            bool levelCompleted = Random.Range(0, 2) == 1;

            if (levelCompleted)
            {
                Debug.Log($"Level {currentLevel} tamamlandı!");
                currentLevel++;
            }
            else
            {
                Debug.Log("Level başarısız! Bir can kaybedildi.");
                playerLives--;
            }
        }

        if (playerLives <= 0)
        {
            Debug.Log("Oyun bitti - Tüm canlar tükendi!");
        }
        else
        {
            Debug.Log("Tebrikler! Tüm leveller tamamlandı!");
        }
    }
    #endregion

    #region FOREACH Döngüsü Örnekleri
    void ForeachLoopExamples()
    {
        Debug.Log("\n--- FOREACH Döngüsü Örnekleri ---");

        // Dizi ile foreach
        int[] scores = { 85, 92, 78, 96, 88 };
        Debug.Log("Oyuncu skorları:");
        foreach (int score in scores)
        {
            Debug.Log($"Skor: {score}");
        }

        // String array ile foreach
        string[] items = { "Kılıç", "Kalkan", "İksir", "Anahtar", "Harita" };
        Debug.Log("Envanter eşyaları:");
        foreach (string item in items)
        {
            Debug.Log($"Eşya: {item}");
        }

        // List<T> - .NET Framework collection'u
        // using System.Collections.Generic; eklediğimiz için sadece List<T> yazabiliyoruz
        // List<T> dinamik dizi gibi çalışır (boyutu değişebilir)
        List<string> enemies = new List<string>
        {
            "Goblin", "Ork", "Ejder", "Büyücü"
        };

        Debug.Log("Düşmanlar:");
        foreach (string enemy in enemies)
        {
            Debug.Log($"Düşman: {enemy}");
        }
    }
    #endregion

    #region ARRAY (Dizi) ve LIST Detaylı Örnekleri
    void ArrayAndListExamples()
    {
        Debug.Log("\n--- ARRAY ve LIST Detaylı Örnekleri ---");

        // ===================
        // ARRAY (DİZİ) ÖRNEKLERİ
        // ===================

        Debug.Log("\n=== ARRAY (Dizi) Örnekleri ===");

        // 1. Array tanımlama yöntemleri
        // Yöntem 1: Boyut belirterek boş array oluşturma
        int[] numbers = new int[5];  // 5 elemanlı int dizisi (varsayılan değerler: 0)
        string[] names = new string[3];  // 3 elemanlı string dizisi (varsayılan değerler: null)

        // Yöntem 2: Değerlerle birlikte tanımlama
        int[] scores = { 100, 85, 92, 78, 96 };  // Kısa yazım
        int[] altScores = new int[] { 100, 85, 92, 78, 96 };  // Uzun yazım

        // Yöntem 3: Boyut belirtip sonra değer atama
        float[] speeds = new float[4];
        speeds[0] = 5.5f;
        speeds[1] = 3.2f;
        speeds[2] = 8.7f;
        speeds[3] = 2.1f;

        // 2. Array elemanlarına erişim
        Debug.Log($"İlk skor: {scores[0]}");  // İndeks 0'dan başlar
        Debug.Log($"Son skor: {scores[scores.Length - 1]}");  // Length özelliği
        Debug.Log($"Array uzunluğu: {scores.Length}");

        // 3. Array'i döngü ile yazdırma
        Debug.Log("Skorları yazdırma:");
        for (int i = 0; i < scores.Length; i++)
        {
            Debug.Log($"Skor[{i}]: {scores[i]}");
        }

        // 4. Array manipülasyonu
        Debug.Log("\nArray manipülasyon örnekleri:");

        // Elemanları değiştirme
        scores[0] = 95;  // İlk elemanı değiştir
        Debug.Log($"Değiştirilen ilk skor: {scores[0]}");

        // Array'in belirli bir değeri içerip içermediğini kontrol etme
        bool contains85 = System.Array.Exists(scores, x => x == 85);
        Debug.Log($"85 skoru var mı? {contains85}");

        // Array'i sıralama
        int[] originalScores = { 100, 85, 92, 78, 96 };
        int[] sortedScores = new int[originalScores.Length];
        System.Array.Copy(originalScores, sortedScores, originalScores.Length);
        System.Array.Sort(sortedScores);

        Debug.Log("Orijinal skorlar:");
        foreach (int score in originalScores)
        {
            Debug.Log($"Skor: {score}");
        }

        Debug.Log("Sıralanmış skorlar:");
        foreach (int score in sortedScores)
        {
            Debug.Log($"Skor: {score}");
        }

        // 5. Çok boyutlu array'ler
        Debug.Log("\n=== Çok Boyutlu Array'ler ===");

        // 2D Array (Matrix)
        int[,] matrix = new int[3, 3]  // 3x3 matrix
        {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9}
        };

        Debug.Log("3x3 Matrix:");
        for (int row = 0; row < 3; row++)
        {
            string line = "";
            for (int col = 0; col < 3; col++)
            {
                line += matrix[row, col] + " ";
            }
            Debug.Log(line);
        }

        // Jagged Array (Düzensiz dizi)
        int[][] jaggedArray = new int[3][];
        jaggedArray[0] = new int[2] { 1, 2 };
        jaggedArray[1] = new int[4] { 3, 4, 5, 6 };
        jaggedArray[2] = new int[3] { 7, 8, 9 };

        Debug.Log("Jagged Array:");
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            string line = $"Satır {i}: ";
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                line += jaggedArray[i][j] + " ";
            }
            Debug.Log(line);
        }

        // ===================
        // LIST ÖRNEKLERİ
        // ===================

        Debug.Log("\n=== LIST Örnekleri ===");

        // 1. List tanımlama yöntemleri
        // Her türlü new'liyorsun bunu.
        List<int> scoreList = new List<int>();  // Boş liste - using sayesinde kısa yazım
        List<string> playerNames = new List<string> { "Ali", "Ayşe", "Mehmet" };  // Değerlerle başlatma

        // 2. List'e eleman ekleme
        scoreList.Add(100);  // Tek eleman ekleme
        scoreList.Add(85);
        scoreList.Add(92);

        // Birden fazla eleman ekleme
        scoreList.AddRange(new int[] { 78, 96, 88 });

        // Array'de Length, List'te Count
        Debug.Log($"Liste uzunluğu: {scoreList.Count}");

        // 3. List elemanlarına erişim
        Debug.Log($"İlk skor: {scoreList[0]}");
        Debug.Log($"Son skor: {scoreList[scoreList.Count - 1]}");

        // 4. List'i yazdırma
        Debug.Log("Liste skorları:");
        for (int i = 0; i < scoreList.Count; i++)
        {
            Debug.Log($"Skor[{i}]: {scoreList[i]}");
        }

        // 5. List manipülasyonu
        Debug.Log("\nList manipülasyon örnekleri:");

        // Belirli indekse eleman ekleme
        scoreList.Insert(1, 99);  // İndeks 1'e 99 ekle
        Debug.Log("İndeks 1'e 99 eklendi");

        // Eleman silme
        scoreList.Remove(85);  // İlk 85'i sil
        Debug.Log("85 değeri silindi");

        scoreList.RemoveAt(0);  // İndeks 0'daki elemanı sil
        Debug.Log("İndeks 0'daki eleman silindi");

        // Eleman arama
        bool contains100 = scoreList.Contains(100);
        Debug.Log($"100 skoru var mı? {contains100}");

        int indexOf92 = scoreList.IndexOf(92);
        Debug.Log($"92 skorunun indeksi: {indexOf92}");

        // Liste sıralama
        scoreList.Sort();
        Debug.Log("Liste sıralandı");

        // Liste ters çevirme
        scoreList.Reverse();
        Debug.Log("Liste ters çevrildi");

        Debug.Log("Final liste:");
        foreach (int score in scoreList)
        {
            Debug.Log($"Skor: {score}");
        }

        // 6. List'ten Array'e, Array'den List'e dönüşüm
        Debug.Log("\n=== Dönüşüm Örnekleri ===");

        // List'ten Array'e
        int[] arrayFromList = scoreList.ToArray();
        Debug.Log($"List'ten Array'e dönüştürüldü. Array uzunluğu: {arrayFromList.Length}");

        // Array'den List'e
        int[] originalArray = { 1, 2, 3, 4, 5 };
        List<int> listFromArray = new List<int>(originalArray);  // Constructor ile dönüşüm
        Debug.Log($"Array'den List'e dönüştürüldü. List uzunluğu: {listFromArray.Count}");

        // 7. Özel List methodları
        Debug.Log("\n=== Özel List Methodları ===");

        List<int> numbers1 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // Find - Koşula uyan ilk elemanı bulur
        int firstEven = numbers1.Find(x => x % 2 == 0);
        Debug.Log($"İlk çift sayı: {firstEven}");

        // FindAll - Koşula uyan tüm elemanları bulur
        List<int> evenNumbers = numbers1.FindAll(x => x % 2 == 0);
        Debug.Log("Çift sayılar:");
        foreach (int num in evenNumbers)
        {
            Debug.Log($"Çift: {num}");
        }

        // RemoveAll - Koşula uyan tüm elemanları siler
        List<int> testList = new List<int> { 1, 2, 3, 4, 5, 6 };
        int removedCount = testList.RemoveAll(x => x % 2 == 0);  // Çift sayıları sil
        Debug.Log($"{removedCount} çift sayı silindi");
        Debug.Log("Kalan tek sayılar:");
        foreach (int num in testList)
        {
            Debug.Log($"Tek: {num}");
        }

        // 8. Unity'de List kullanımı
        Debug.Log("\n=== Unity'de List Kullanımı ===");

        // GameObject'leri tutmak için
        List<string> gameObjectNames = new List<string>
        {
            "Player", "Enemy", "Coin", "PowerUp"
        };

        Debug.Log("GameObject isimleri:");
        foreach (string objName in gameObjectNames)
        {
            Debug.Log($"GameObject: {objName}");
        }

        // Oyuncu envanteri simülasyonu
        List<string> inventory = new List<string>();
        inventory.Add("Kılıç");
        inventory.Add("Kalkan");
        inventory.Add("İksir");

        Debug.Log("Oyuncu envanteri:");
        for (int i = 0; i < inventory.Count; i++)
        {
            Debug.Log($"Slot {i + 1}: {inventory[i]}");
        }

        // Envanterde eşya arama
        if (inventory.Contains("Kılıç"))
        {
            Debug.Log("Oyuncunun kılıcı var!");
        }

        // ===================
        // ARRAY vs LIST KARŞILAŞTIRMASI
        // ===================

        Debug.Log("\n=== ARRAY vs LIST Karşılaştırması ===");
        Debug.Log("ARRAY:");
        Debug.Log("+ Daha hızlı (performance)");
        Debug.Log("+ Bellek kullanımı daha az");
        Debug.Log("- Boyut sabittir (değiştirilemez)");
        Debug.Log("- Eleman ekleme/silme zor");

        Debug.Log("\nLIST:");
        Debug.Log("+ Dinamik boyut (eleman ekleyip silebilirsiniz)");
        Debug.Log("+ Çok sayıda kullanışlı method");
        Debug.Log("+ Esneklik");
        Debug.Log("- Array'e göre biraz daha yavaş");
        Debug.Log("- Biraz daha fazla bellek kullanır");

        Debug.Log("\nNe zaman hangisini kullanmalı?");
        Debug.Log("ARRAY: Sabit boyutlu veriler, yüksek performance gerekli");
        Debug.Log("LIST: Dinamik veriler, sık ekleme/silme işlemleri");
    }
    #endregion

    #region Method Örnekleri
    void MethodExamples()
    {
        Debug.Log("\n--- Method Örnekleri ---");

        // Parametresiz method
        SayHello();

        // Parametreli method
        int result = AddNumbers(15, 25);
        Debug.Log($"15 + 25 = {result}");

        // Birden fazla parametre
        DisplayPlayerInfo("Ahmet", 100, 250);

        // Ref ve out parametreleri
        int originalValue = 10;
        MultiplyByTwo(ref originalValue);
        Debug.Log($"Ref ile değişen değer: {originalValue}");

        int quotient, remainder;
        DivideNumbers(17, 5, out quotient, out remainder);
        Debug.Log($"17 ÷ 5 = {quotient}, kalan: {remainder}");

        // Overloaded methods
        Debug.Log($"Damage hesaplama (sadece güç): {CalculateDamage(25)}");
        Debug.Log($"Damage hesaplama (güç + kritik): {CalculateDamage(25, true)}");
        Debug.Log($"Damage hesaplama (güç + çarpan): {CalculateDamage(25, 1.5f)}");
    }

    // Parametresiz method
    void SayHello()
    {
        Debug.Log("Merhaba Unity!");
    }

    // Return değeri olan method
    int AddNumbers(int a, int b)
    {
        return a + b;
    }

    // Birden fazla parametre alan method
    void DisplayPlayerInfo(string name, int health, int score)
    {
        Debug.Log($"Oyuncu: {name}, Can: {health}, Skor: {score}");
    }

    // Ref parameter (referans ile geçiş)
    void MultiplyByTwo(ref int number)
    {
        number *= 2;
    }

    // Out parameter (çıkış parametresi)
    void DivideNumbers(int dividend, int divisor, out int quotient, out int remainder)
    {
        quotient = dividend / divisor;
        remainder = dividend % divisor;
    }

    // Method overloading (aynı isimli farklı parametreli methodlar)
    int CalculateDamage(int baseDamage)
    {
        return baseDamage;
    }

    int CalculateDamage(int baseDamage, bool isCritical)
    {
        return isCritical ? baseDamage * 2 : baseDamage;
    }

    float CalculateDamage(int baseDamage, float multiplier)
    {
        return baseDamage * multiplier;
    }

    // Silah saldırısı method'u
    void AttackWithCurrentWeapon()
    {
        int damage = 0;

        switch (currentWeapon)
        {
            case WeaponType.Sword:
                damage = 30;
                break;
            case WeaponType.Bow:
                damage = 25;
                break;
            case WeaponType.Magic:
                damage = 40;
                break;
            case WeaponType.Fist:
                damage = 15;
                break;
        }

        Debug.Log($"{currentWeapon} ile {damage} hasar verildi!");
    }

    // Input kontrolü method'u - Unity'ye özel Input sistemi
    void HandleInput()
    {
        // Input.GetKeyDown() - Tuşa basıldığı frame'de true döner (sadece bir frame)
        // Input.GetKey() - Tuş basılı tutulduğu sürece true döner
        // Input.GetKeyUp() - Tuş bırakıldığı frame'de true döner

        if (Input.GetKeyDown(KeyCode.Space))    // KeyCode enum'u Unity'ye özel
        {
            Debug.Log("Space tuşuna basıldı!");
            currentState = PlayerState.Jumping;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))    // Sol Shift tuşu
        {
            Debug.Log("Shift tuşuna basıldı - Koşma moduna geçildi!");
            currentState = PlayerState.Running;
        }

        // Mouse input kontrolü - Unity'ye özel
        // Mouse0 = Sol tık, Mouse1 = Sağ tık, Mouse2 = Orta tık (scroll wheel)
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Sol mouse tuşuna basıldı - Saldırı!");
            currentState = PlayerState.Attacking;
        }
    }
    #endregion
}
