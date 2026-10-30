using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.Write("Mənbə qovluğunun yolunu daxil edin: ");
        string menbeQovluq = Console.ReadLine();

        Console.Write("Təyinat (Backup) qovluğunun yolunu daxil edin: ");
        string teyinatQovluq = Console.ReadLine();

        
        if (!Directory.Exists(menbeQovluq))
        {
            Console.WriteLine("XƏTA: Göstərilən mənbə qovluğu mövcud deyil! Zəhmət olmasa yolu düzgün daxil edin.");
            Console.ReadLine();
            return;
        }

        try
        {
            
            if (!Directory.Exists(teyinatQovluq))
            {
                Directory.CreateDirectory(teyinatQovluq);
                Console.WriteLine("Təyinat qovluğu mövcud deyildi, yenisi yaradıldı.");
            }

            
            string[] txtFayllar = Directory.GetFiles(menbeQovluq, "*.txt");

            int kopyalananSay = 0;
            long umumiOlcu = 0;
            string bugunkuTarix = DateTime.Now.ToString("yyyy-MM-dd");

            foreach (string faylYolu in txtFayllar)
            {
                FileInfo faylMelumati = new FileInfo(faylYolu);

             
                string yeniFaylAdi = bugunkuTarix + "_" + faylMelumati.Name;
                string yeniTamYol = Path.Combine(teyinatQovluq, yeniFaylAdi);

               
                File.Copy(faylYolu, yeniTamYol, true);

                kopyalananSay++;
                umumiOlcu += faylMelumati.Length;

                Console.WriteLine($"Kopyalandı: {yeniFaylAdi}");
            }

            Console.WriteLine("\n--- Ehtiyat Nüsxələmə Tamamlandı ---");
            Console.WriteLine($"Kopyalanan faylların sayı: {kopyalananSay}");
            Console.WriteLine($"Faylların ümumi ölçüsü: {umumiOlcu} bayt");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Əməliyyat zamanı xəta baş verdi: {ex.Message}");
        }

        Console.ReadLine();
    }
}