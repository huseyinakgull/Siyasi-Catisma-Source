using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

    public class OyunYonetimi : MonoBehaviour
    {
        public static OyunYonetimi instance;
        public OyunVerileri oyunVerileri;

        private string dosyaYolu;

       void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            dosyaYolu = Path.Combine(Application.persistentDataPath, "oyunVerileri.json");
            VerileriYukle();
        }

        void Start()
        {
            if (oyunVerileri == null)
            {
                oyunVerileri = new OyunVerileri();
                Kaydet();
            }

            Baslat();
        }

        void OnApplicationQuit()
        {
            Kaydet();
        }

        void Baslat()
        {
            
        }

        public void Kaydet()
        {
            string jsonVeri = JsonUtility.ToJson(oyunVerileri);
            File.WriteAllText(dosyaYolu, jsonVeri);
            Debug.Log("Kayýt Alýndý.");
    }

        public void VerileriYukle()
        {
            if (File.Exists(dosyaYolu))
            {
                string jsonVeri = File.ReadAllText(dosyaYolu);
                oyunVerileri = JsonUtility.FromJson<OyunVerileri>(jsonVeri);
                Debug.Log("Veriler yüklendi.");
            }
        }

         public bool OyunVerileriVarMi()
     {
           return oyunVerileri != null && !string.IsNullOrEmpty(oyunVerileri.KullaniciIsmi)
            && !string.IsNullOrEmpty(oyunVerileri.PartiIsmi) && !string.IsNullOrEmpty(oyunVerileri.PartiSlogan);
     }

    public void SaveSifirla()
    {
        oyunVerileri = new OyunVerileri();
        Kaydet();
        Debug.Log("Save Sýfýrlandý.");
    }

}
