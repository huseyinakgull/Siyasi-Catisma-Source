using UnityEngine;
using TMPro;
using System.Security.Cryptography;
using System.Collections.Generic;


public class EtkinlikYonetimi : MonoBehaviour
{
    private OyunVerileri oyunVerileri;
    public TMP_Text ilText;
    public TMP_Text gunText;
    private int oncekiIlIndex = -1;
    private string[] iller = {
        "ADANA", "ADIYAMAN", "AFYONKARAH�SAR", "A�RI", "AMASYA", "ANKARA", "ANTALYA", "ARTV�N", "AYDIN",
        "BALIKES�R", "B�LEC�K", "B�NG�L", "B�TL�S", "BOLU", "BURDUR", "BURSA", "�ANAKKALE", "�ANKIRI", "�ORUM",
        "DEN�ZL�", "D�YARBAKIR", "ED�RNE", "ELAZI�", "ERZ�NCAN", "ERZURUM", "ESK��EH�R", "GAZ�ANTEP", "G�RESUN",
        "G�M��HANE", "HAKKAR�", "HATAY", "ISPARTA", "MERS�N", "�STANBUL", "�ZM�R", "KARS", "KASTAMONU", "KAYSER�",
        "KIRKLAREL�", "KIR�EH�R", "KOCAEL�", "KONYA", "K�TAHYA", "MALATYA", "MAN�SA", "KAHRAMANMARA�", "MARD�N",
        "MU�LA", "MU�", "NEV�EH�R", "N��DE", "ORDU", "R�ZE", "SAKARYA", "SAMSUN", "S��RT", "S�NOP", "S�VAS",
        "TEK�RDA�", "TOKAT", "TRABZON", "TUNCEL�", "�ANLIURFA", "U�AK", "VAN", "YOZGAT", "ZONGULDAK", "AKSARAY",
        "BAYBURT", "KARAMAN", "KIRIKKALE", "BATMAN", "�IRNAK", "BARTIN", "ARDAHAN", "I�DIR", "YALOVA", "KARAB�K",
        "K�L�S", "OSMAN�YE", "D�ZCE"
    };

    void Start()
    {
        oyunVerileri = OyunYonetimi.instance.oyunVerileri;
        UpdateUI();
        gunText.text = "G�n: " + oyunVerileri.GunSayisi.ToString();
        ilText.text = "Bulundu�un �l: " + oyunVerileri.GecilenIl;
        OyunYonetimi.instance.Kaydet();
    }

    public void GuneGec()
    {
        oyunVerileri.KisiselParaDegistir(4500);
        oyunVerileri.PartiKasasiDegistir(10000);
        oyunVerileri.GunDegistir(1);
        gunText.text = "G�n: " + oyunVerileri.GunSayisi.ToString();
        SecilenIlGoreGorevleriBelirle();
        OyunYonetimi.instance.Kaydet();
    }

    public void MitingDuzenle()
    {
        if (oyunVerileri.MitingSayisi < 1)
        {
            int mitingMaliyeti = 2000;

            if (oyunVerileri.KisiselPara >= mitingMaliyeti)
            {
                Debug.Log($"Ba�ar�l� miting d�zenlendi! Destek�i say�s�: 2000, Etkilenen il: {oyunVerileri.GecilenIl}");
                oyunVerileri.DestekciVatandasSayisi += 2000;
                oyunVerileri.KisiselPara -= mitingMaliyeti;
                oyunVerileri.MitingSayisiniArtir(1);
            }
            else
            {
                Debug.Log("Yetersiz para! Miting d�zenleme i�lemi ba�ar�s�z.");
            }
            SecilenIlGoreGorevleriBelirle();
        }
        else
        {
            Debug.Log("Bu ilde zaten miting yap�ld�!");
        }
    }

    public void PankartAs()
    {
        if (oyunVerileri.BasariliMiting >= 1 && oyunVerileri.BasariliPankartSayisi < 2)
        {
            int pankartMaliyeti = 4000;
            if (oyunVerileri.KisiselPara >= pankartMaliyeti)
            {
                Debug.Log($"Ba�ar�l� pankart asma! Destek�i say�s�: 3500, Etkilenen il: {oyunVerileri.GecilenIl}");
                oyunVerileri.DestekciVatandasSayisi += 3500;
                oyunVerileri.KisiselPara -= pankartMaliyeti;
                oyunVerileri.BasariliPankartSayisiniArtir(1);
            }
            else
            {
                Debug.Log("Yetersiz para! Pankart asma i�lemi ba�ar�s�z.");
            }
        }
        else
        {
            if (oyunVerileri.BasariliMiting < 1)
            {
                Debug.Log("Bu ilde �nce miting d�zenleyin!");
            }
            else
            {
                Debug.Log("Bu ilde zaten iki kez pankart as�ld�!");
            }
        }
    }

    void UpdateUI()
    {
        ilText.text = "Bulundu�un �l:" + oyunVerileri.GecilenIl;
    }

    public void SecilenIlGoreGorevleriBelirle()
    {
        int secilenIlIndex;
        do
        {
            secilenIlIndex = Random.Range(0, iller.Length);
        } while (secilenIlIndex == oncekiIlIndex);

        oncekiIlIndex = secilenIlIndex;

        oyunVerileri.GecilenIlIndeks = secilenIlIndex;
        oyunVerileri.GecilenIl = iller[secilenIlIndex];
        oyunVerileri.BasariliMiting = 0;
        oyunVerileri.BasariliPankartSayisi = 0;
        UpdateUI();
    }
}