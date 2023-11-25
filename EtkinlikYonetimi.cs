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
        "ADANA", "ADIYAMAN", "AFYONKARAHÝSAR", "AÐRI", "AMASYA", "ANKARA", "ANTALYA", "ARTVÝN", "AYDIN",
        "BALIKESÝR", "BÝLECÝK", "BÝNGÖL", "BÝTLÝS", "BOLU", "BURDUR", "BURSA", "ÇANAKKALE", "ÇANKIRI", "ÇORUM",
        "DENÝZLÝ", "DÝYARBAKIR", "EDÝRNE", "ELAZIÐ", "ERZÝNCAN", "ERZURUM", "ESKÝÞEHÝR", "GAZÝANTEP", "GÝRESUN",
        "GÜMÜÞHANE", "HAKKARÝ", "HATAY", "ISPARTA", "MERSÝN", "ÝSTANBUL", "ÝZMÝR", "KARS", "KASTAMONU", "KAYSERÝ",
        "KIRKLARELÝ", "KIRÞEHÝR", "KOCAELÝ", "KONYA", "KÜTAHYA", "MALATYA", "MANÝSA", "KAHRAMANMARAÞ", "MARDÝN",
        "MUÐLA", "MUÞ", "NEVÞEHÝR", "NÝÐDE", "ORDU", "RÝZE", "SAKARYA", "SAMSUN", "SÝÝRT", "SÝNOP", "SÝVAS",
        "TEKÝRDAÐ", "TOKAT", "TRABZON", "TUNCELÝ", "ÞANLIURFA", "UÞAK", "VAN", "YOZGAT", "ZONGULDAK", "AKSARAY",
        "BAYBURT", "KARAMAN", "KIRIKKALE", "BATMAN", "ÞIRNAK", "BARTIN", "ARDAHAN", "IÐDIR", "YALOVA", "KARABÜK",
        "KÝLÝS", "OSMANÝYE", "DÜZCE"
    };

    void Start()
    {
        oyunVerileri = OyunYonetimi.instance.oyunVerileri;
        UpdateUI();
        gunText.text = "Gün: " + oyunVerileri.GunSayisi.ToString();
        ilText.text = "Bulunduðun Ýl: " + oyunVerileri.GecilenIl;
        OyunYonetimi.instance.Kaydet();
    }

    public void GuneGec()
    {
        oyunVerileri.KisiselParaDegistir(4500);
        oyunVerileri.PartiKasasiDegistir(10000);
        oyunVerileri.GunDegistir(1);
        gunText.text = "Gün: " + oyunVerileri.GunSayisi.ToString();
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
                Debug.Log($"Baþarýlý miting düzenlendi! Destekçi sayýsý: 2000, Etkilenen il: {oyunVerileri.GecilenIl}");
                oyunVerileri.DestekciVatandasSayisi += 2000;
                oyunVerileri.KisiselPara -= mitingMaliyeti;
                oyunVerileri.MitingSayisiniArtir(1);
            }
            else
            {
                Debug.Log("Yetersiz para! Miting düzenleme iþlemi baþarýsýz.");
            }
            SecilenIlGoreGorevleriBelirle();
        }
        else
        {
            Debug.Log("Bu ilde zaten miting yapýldý!");
        }
    }

    public void PankartAs()
    {
        if (oyunVerileri.BasariliMiting >= 1 && oyunVerileri.BasariliPankartSayisi < 2)
        {
            int pankartMaliyeti = 4000;
            if (oyunVerileri.KisiselPara >= pankartMaliyeti)
            {
                Debug.Log($"Baþarýlý pankart asma! Destekçi sayýsý: 3500, Etkilenen il: {oyunVerileri.GecilenIl}");
                oyunVerileri.DestekciVatandasSayisi += 3500;
                oyunVerileri.KisiselPara -= pankartMaliyeti;
                oyunVerileri.BasariliPankartSayisiniArtir(1);
            }
            else
            {
                Debug.Log("Yetersiz para! Pankart asma iþlemi baþarýsýz.");
            }
        }
        else
        {
            if (oyunVerileri.BasariliMiting < 1)
            {
                Debug.Log("Bu ilde önce miting düzenleyin!");
            }
            else
            {
                Debug.Log("Bu ilde zaten iki kez pankart asýldý!");
            }
        }
    }

    void UpdateUI()
    {
        ilText.text = "Bulunduðun Ýl:" + oyunVerileri.GecilenIl;
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