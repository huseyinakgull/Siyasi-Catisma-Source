using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OyunVerileri
{
    public string KullaniciIsmi;
    public string PartiIsmi;
    public string PartiSlogan;
    public int IlSayisi = 81;
    public int MitingSayisi = 0;
    public int BasariliMiting = 0;
    public int BasarisizMiting = 0;
    public bool Yasiyor = true;
    public int SaglikDurumu = 0;
    public int KorumaSayisi = 0;
    public string GecilenIl;
    public int HalkTarafindanSevilme = 20;
    public int KisiselPara = 15000;
    public int PartiKasasi = 70000;
    public int DestekciVatandasSayisi = 25;
    public int MilletVekiliSayisi = 5;
    public int GenclikKollariNufus = 5;
    public int ÝllegalEylemGeliri = 0;
    public int CurrentQuestionIndex = 0;
    public int KacSoruCevapladi = 0;

    public int GecilenIlIndeks { get; set; }
    public int BasariliPankartSayisi{ get; set; }
    public bool MitingYapildi { get; set; }
    public bool PankartAsildi { get; set; }
    public int GunSayisi = 0;
    public string UlkeAdi = "Türkiye";

    public UnityEvent<int> MitingSayisiDegisti = new UnityEvent<int>();
    public UnityEvent<bool> YasiyorDurumuDegisti = new UnityEvent<bool>();
    public UnityEvent<int> SaglikDurumuDegisti = new UnityEvent<int>();
    public UnityEvent<int> KorumaSayisiDegisti = new UnityEvent<int>();
    public UnityEvent<int> KisiselParaDegisti = new UnityEvent<int>();
    public UnityEvent<int> PartiKasasiDegisti = new UnityEvent<int>();
    public UnityEvent<int> DestekciVatandasSayisiDegisti = new UnityEvent<int>();
    public UnityEvent<int> MilletVekiliSayisiDegisti = new UnityEvent<int>();
    public UnityEvent<int> GenclikKollariNufusDegisti = new UnityEvent<int>();
    public UnityEvent<int> GunSayisiDegisti = new UnityEvent<int>();
    public UnityEvent<int> BasariliPankartSayisiDegisti = new UnityEvent<int>();
    public UnityEvent<int> ÝllegalEylemGeliriDegisti = new UnityEvent<int>();

    public void YeniGuneHazirla()
    {
        MitingYapildi = false;
        PankartAsildi = false;
    }

    public void MitingSayisiniArtir(int miktar)
    {
        MitingSayisi += miktar;
        MitingSayisiDegisti.Invoke(MitingSayisi);
    }

    public void ÝllegalEylemGeliriArttir(int miktar)
    {
        ÝllegalEylemGeliri += miktar;
        ÝllegalEylemGeliriDegisti.Invoke(ÝllegalEylemGeliri);
    }

    public void BasariliPankartSayisiniArtir(int miktar)
    {
        BasariliPankartSayisi += miktar;
        BasariliPankartSayisiDegisti.Invoke(BasariliPankartSayisi);
    }
    public void GunDegistir(int miktar)
    {
        GunSayisi += miktar;
        GunSayisiDegisti.Invoke(GunSayisi);
    }

    public void YasiyorDurumuDegistir(bool yeniDurum)
    {
        Yasiyor = yeniDurum;
        YasiyorDurumuDegisti.Invoke(Yasiyor);
    }

    public void PartiKasasiDegistir(int miktar)
    {
        PartiKasasi += miktar;
        PartiKasasiDegisti.Invoke(PartiKasasi);
    }


    public void SaglikDurumuDegistir(int yeniDurum)
    {
        SaglikDurumu = Mathf.Clamp(yeniDurum, 0, 1);
        SaglikDurumuDegisti.Invoke(SaglikDurumu);
    }

    public void KorumaSayisiniArtir(int miktar)
    {
        KorumaSayisi += miktar;
        KorumaSayisiDegisti.Invoke(KorumaSayisi);
    }

    public void KisiselParaDegistir(int miktar)
    {
        KisiselPara += miktar;
        KisiselParaDegisti.Invoke(KisiselPara);
    }

    public void PartiKasasiniDegistir(int miktar)
    {
        PartiKasasi += miktar;
        PartiKasasiDegisti.Invoke(PartiKasasi);
    }

    public void DestekciVatandasSayisiniDegistir(int miktar)
    {
        DestekciVatandasSayisi += miktar;
        DestekciVatandasSayisiDegisti.Invoke(DestekciVatandasSayisi);
    }

    public void MilletVekiliSayisiniDegistir(int miktar)
    {
        MilletVekiliSayisi += miktar;
        MilletVekiliSayisiDegisti.Invoke(MilletVekiliSayisi);
    }

    public void GenclikKollariNufusunuDegistir(int miktar)
    {
        GenclikKollariNufus += miktar;
        GenclikKollariNufusDegisti.Invoke(GenclikKollariNufus);
    }
}