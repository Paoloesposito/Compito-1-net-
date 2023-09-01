using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compito_1__net_
{
    class Program
    {
        static List<Contribuente> contribuenti = new List<Contribuente>();

        static void Main(string[] args)
        {
            // Alcuni contribuenti di esempio per popolare di base la nostra applicazione
            contribuenti.Add(new Contribuente("Frodo", "Baggins", "1368-04-06", "FRODGB13680406", "M", "Hobbiton, Contea", 12000));
            contribuenti.Add(new Contribuente("Gandalf", "Il Grigio", "Anno sconosciuto", "GNDLGRXXXXXX", "M", "Errante", 55000));
            contribuenti.Add(new Contribuente("Sauron", "Il Signore Oscuro", "Anno sconosciuto", "SRNDRKXXXXXX", "M", "Barad-dûr, Mordor", 100000));
            contribuenti.Add(new Contribuente("Aragorn", "Figlio di Arathorn", "2931-03-01", "ARGRFR29310301", "M", "Rivendell", 60000));
            contribuenti.Add(new Contribuente("Legolas", "Figlio di Thranduil", "Anno sconosciuto", "LGLSTHXXXXXX", "M", "Bosco Atro, Mirkwood", 50000));
            contribuenti.Add(new Contribuente("Gimli", "Figlio di Glóin", "2879-11-11", "GMLIGL28791111", "M", "Erebor", 45000));
            contribuenti.Add(new Contribuente("Samwise", "Gamgee", "1380-04-06", "SAMWGM13800406", "M", "Hobbiton, Contea", 13000));
            while (true)
            {
                Console.WriteLine("Scegli una funzione:");
                Console.WriteLine("1) Inserimento di una nuova dichiarazione di un contribuente");
                Console.WriteLine("2) Lista completa di tutti i contribuenti");
                Console.WriteLine("3) Esci");
                string scelta = Console.ReadLine();

                switch (scelta)
                {
                    case "1":
                        InserisciContribuente();
                        break;
                    case "2":
                        VisualizzaContribuenti();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Scelta non valida");
                        break;
                }
            }
        }

        static void InserisciContribuente()
        {
            Console.WriteLine("Inserisci nome:");
            string nome = Console.ReadLine();
            Console.WriteLine("Inserisci cognome:");
            string cognome = Console.ReadLine();
            Console.WriteLine("Inserisci data di nascita (DD-MM-YYYY):");
            string dataNascita;

            do
            {
                
                dataNascita = Console.ReadLine();

                if (DateTime.TryParseExact(dataNascita, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    break; // IL METODO PER LA VERIFICA DELLA DATA MI HA AIUTATO GOOGLE
                }
                else
                {
                    Console.WriteLine("Data non valida. Riprova.");
                }
            } while (true);
            Console.WriteLine("Inserisci codice fiscale:");
            string codiceFiscale;
            do
            {
                
                codiceFiscale = Console.ReadLine();
                if (codiceFiscale.Length != 16)
                {
                    Console.WriteLine("Il codice fiscale deve essere di 16 caratteri. Per favore, reinseriscilo.");
                }
            } while (codiceFiscale.Length != 16);
            Console.WriteLine("Inserisci sesso (M/F):");
            string sesso;
            do
            {
                Console.WriteLine("1. Maschile (M)");
                Console.WriteLine("2. Femminile (F)");
                string scelta = Console.ReadLine();

                if (scelta == "1")
                {
                    sesso = "M";
                    break;
                }
                else if (scelta == "2")
                {
                    sesso = "F";
                    break;
                }
                else
                {
                    Console.WriteLine("Scelta non valida. Per favore, inserisci 1 per 'Maschile' o 2 per 'Femminile'.");
                }
            } while (true);
            Console.WriteLine("Inserisci comune di residenza:");
            string comuneResidenza = Console.ReadLine();
            Console.WriteLine("Inserisci reddito annuale:");
            double redditoAnnuale = Convert.ToDouble(Console.ReadLine());

            Contribuente c = new Contribuente(nome, cognome, dataNascita, codiceFiscale, sesso, comuneResidenza, redditoAnnuale);
            contribuenti.Add(c);

            Console.WriteLine($"Imposta dovuta: {c.CalcolaImposta():F2}");
        }

        static void VisualizzaContribuenti()
        {
            int count = 1;
            foreach (Contribuente c in contribuenti)
            {
                Console.WriteLine($"Contribuente {count}:");
                Console.WriteLine(c);
                Console.WriteLine("---------------");  // Separatore tra i contribuenti
                count++;
            }
        }
    }

    class Contribuente
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string DataNascita { get; set; }
        public string CodiceFiscale { get; set; }
        public string Sesso { get; set; }
        public string ComuneResidenza { get; set; }
        public double RedditoAnnuale { get; set; }

        public Contribuente(string nome, string cognome, string dataNascita, string codiceFiscale, string sesso, string comuneResidenza, double redditoAnnuale)
        {
            Nome = nome;
            Cognome = cognome;
            DataNascita = dataNascita;
            CodiceFiscale = codiceFiscale;
            Sesso = sesso;
            ComuneResidenza = comuneResidenza;
            RedditoAnnuale = redditoAnnuale;
        }

        public double CalcolaImposta()
        {
            double imposta = 0;

            if (RedditoAnnuale <= 15000)
            {
                imposta = RedditoAnnuale * 0.23;
            }
            else if (RedditoAnnuale <= 28000)
            {
                imposta = 3450 + (RedditoAnnuale - 15000) * 0.27;
            }
            else if (RedditoAnnuale <= 55000)
            {
                imposta = 6960 + (RedditoAnnuale - 28000) * 0.38;
            }
            else if (RedditoAnnuale <= 75000)
            {
                imposta = 17220 + (RedditoAnnuale - 55000) * 0.41;
            }
            else
            {
                imposta = 25420 + (RedditoAnnuale - 75000) * 0.43;
            }

            return imposta;
        }

        public override string ToString()
        {
            return $"Nome: {Nome}\n" +
          $"Cognome: {Cognome}\n" +
          $"Data di nascita: {DataNascita}\n" +
          $"Codice fiscale: {CodiceFiscale}\n" +
          $"Sesso: {Sesso}\n" +
          $"Comune di residenza: {ComuneResidenza}\n" +
          $"Reddito annuale: {RedditoAnnuale}\n" +
          $"Imposta: {CalcolaImposta():F2}";
        }
    }
}

