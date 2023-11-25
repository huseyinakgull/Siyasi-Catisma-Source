using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class quecy_main : MonoBehaviour
{
    public TMP_Text soruMetni;
    private OyunVerileri oyunVerileri;
    public GameObject BildirimSayfasi;
    public TMP_Text BildirimText;
    public Button Ýlerleme_Butonu;
    public GameObject KrizYonetimiPenceresi;
    public Button[] secimButonlari;
    public static int KacSoruCevapladi;
    public static int currentQuestionIndex = 0;

    [Tooltip("Sorularý ve cevaplarý burada ayarlayýn.")]
    public Soru[] sorular;

    public enum PositiveEvent
    {
        PartiKasasi,
        KisiselPara,
        DestekciVatandas,
        MilletVekili
    }

    public enum NegativeEvent
    {
        Neg_PartiKasasi,
        Neg_KisiselPara,
        Neg_DestekciVatandas,
        Neg_MilletVekili
    }

    [Tooltip("Olumlu olaylarý burada tanýmlayýn.")]
    public List<PositiveEvent> positiveEvents;

    [Tooltip("Olumsuz olaylarý burada tanýmlayýn.")]
    public List<NegativeEvent> negativeEvents;

    private void Update()
    {
        if (KacSoruCevapladi >= 3)
        {
            Ýlerleme_Butonu.interactable = true;
            KrizYonetimiPenceresi.gameObject.SetActive(false);
        }
        else
        {
            Ýlerleme_Butonu.interactable = false;
            KrizYonetimiPenceresi.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        oyunVerileri = OyunYonetimi.instance.oyunVerileri;
        currentQuestionIndex = oyunVerileri.CurrentQuestionIndex;
        KacSoruCevapladi = oyunVerileri.KacSoruCevapladi;
        DisplayQuestion(currentQuestionIndex);
        Ýlerleme_Butonu.interactable = false;
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        bool dogruMu = sorular[currentQuestionIndex].dogruSecenekler[choiceIndex];
        KacSoruCevapladi++;
        currentQuestionIndex++;
        oyunVerileri.CurrentQuestionIndex = currentQuestionIndex;

        if (dogruMu)
        {
            PositiveEvent positiveEvent = positiveEvents[Random.Range(0, positiveEvents.Count)];
            TriggerPositiveEvent(positiveEvent);
            BildirimSayfasi_Acma();
            BildirimText_Duzenleme(sorular[currentQuestionIndex - 1].olumluSonuclar[choiceIndex]);
        }
        else
        {
            NegativeEvent negativeEvent = negativeEvents[Random.Range(0, negativeEvents.Count)];
            TriggerNegativeEvent(negativeEvent);
            BildirimSayfasi_Acma();
            BildirimText_Duzenleme(sorular[currentQuestionIndex - 1].olumsuzSonuclar[choiceIndex]);
        }

        if (currentQuestionIndex < sorular.Length)
        {
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            Debug.Log("Bitti.");
        }
    }

    private void DisplayQuestion(int questionIndex)
    {
        soruMetni.text = sorular[questionIndex].soruMetni;
        for (int i = 0; i < secimButonlari.Length; i++)
        {
            secimButonlari[i].GetComponentInChildren<TMP_Text>().text = sorular[questionIndex].secenekler[i];
        }
    }

    public void SoruSýnýrSýfýrla()
    {
        KacSoruCevapladi = 0;
    }

    private void TriggerPositiveEvent(PositiveEvent positiveEvent)
    {
        switch (positiveEvent)
        {
            case PositiveEvent.PartiKasasi:
                oyunVerileri.PartiKasasiDegistir(15000);
                break;
            case PositiveEvent.KisiselPara:
                oyunVerileri.KisiselParaDegistir(15000);
                break;
            case PositiveEvent.DestekciVatandas:
                oyunVerileri.DestekciVatandasSayisiniDegistir(1000);
                break;
            case PositiveEvent.MilletVekili:
                oyunVerileri.MilletVekiliSayisiniDegistir(700);
                break;
            default:
                break;
        }
    }

    private void TriggerNegativeEvent(NegativeEvent negativeEvent)
    {
        switch (negativeEvent)
        {
            case NegativeEvent.Neg_PartiKasasi:
                oyunVerileri.PartiKasasiDegistir(-5000);
                break;
            case NegativeEvent.Neg_KisiselPara:
                oyunVerileri.KisiselParaDegistir(-10000);
                break;
            case NegativeEvent.Neg_DestekciVatandas:
                oyunVerileri.DestekciVatandasSayisiniDegistir(-300);
                break;
            case NegativeEvent.Neg_MilletVekili:
                oyunVerileri.MilletVekiliSayisiniDegistir(-200);
                break;
            default:
                break;
        }
    }

    public void BildirimSayfasi_Acma()
    {
        BildirimSayfasi.gameObject.SetActive(true);
    }

    public void BildirimText_Duzenleme(string text)
    {
        BildirimText.text = text;
    }

    public void BildirimSayfasi_Kapatma()
    {
        BildirimSayfasi.gameObject.SetActive(false);
    }
}

[System.Serializable]
public class Soru
{
    [TextArea(3, 5)]
    public string soruMetni;
    public string[] secenekler;
    [Tooltip("Hangilerinin doðru olduðunu tikle.")]
    public bool[] dogruSecenekler;
    public string[] olumluSonuclar;
    public string[] olumsuzSonuclar;
}
