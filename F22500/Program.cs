using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace F22500
{
	internal class Program
	{

		static void Main(string[] args)
		{
			List<List<string>> lista = Beolvas("penztar.txt");

            Console.WriteLine($"2. feladat\na fizetések száma: {lista.Count}");


			//for (int i = 0; i < lista.Count; i++)
			//{
			//             Console.WriteLine($"Az {i+1}. kosár tartalma: {string.Join(", ", lista[i])}");
			//         }
			Console.WriteLine("első kosárban ennyi cucc van:");
			Console.WriteLine(lista[0].Count);
            Console.WriteLine("Adjon meg egy sorszámot!");
            int sorszam = int.Parse(Console.ReadLine());
			Console.WriteLine("Adjon meg egy árucikket!");
			string arucikk = Console.ReadLine();
			Console.WriteLine("Adjon meg egy darabszámot!");
			int db = int.Parse(Console.ReadLine());

			Console.WriteLine($"Először vettek az árucikkből? {lista.FindIndex(k => k.Contains(arucikk)) + 1}");
			Console.WriteLine($"Utoljára vettek az árucikkből? {lista.FindLastIndex(k => k.Contains(arucikk)) + 1}");
			Console.WriteLine($"Összesen vettek az árucikkből? {lista.Count(k => k.Contains(arucikk))} db");
			Console.WriteLine($"A {sorszam}. vásárláskor így alakultak a bevásárlások:");

            Dictionary<string, int> csoportositas = Csoportositas(lista[sorszam-1]);
			/** /
			"toll"			1
			"szatyor"		1
			"doboz"			1
			"csavarkulcs"	1
			"colostok"		2
			"HB ceruza"		2
			/**/
			foreach (string item in csoportositas.Keys)
			{
				Console.WriteLine($"{csoportositas[item]} {item}");
			}

			using (StreamWriter w = new StreamWriter("osszeg.txt"))
			{
				for (int i = 0; i < lista.Count; i++)
				{
					w.WriteLine($"{i+1}: {Mennyiazannyi(lista[i])}");
				}

			}
		}

		private static int Mennyiazannyi(List<string> kosar)
		{
			Dictionary<string, int> szotar = Csoportositas(kosar);
			/** /
			"toll"			1
			"szatyor"		1
			"doboz"			1
			"csavarkulcs"	1
			"colostok"		2
			"HB ceruza"		2
			/**/
			int sum = 0;
			foreach (string kulcs in szotar.Keys)
			{
				sum += ertek(szotar[kulcs]);
			}
			return sum;
		}

		private static Dictionary<string, int> Csoportositas(List<string> list)
		{
			Dictionary<string, int> szotar = new Dictionary<string, int>();
			foreach(string s in list)
			{
				if (szotar.ContainsKey(s))
				{
					szotar[s] += 1;
				}
				else
				{
					szotar[s]  = 1;
				}
			}
			return szotar;
		}

		static int ertek(int db)
		{
			if (db == 0) return 0;
			if (db == 1) return 500;
			if (db == 2) return 500 + 450;
			if (db >= 3) return 500 + 450 + 400 * (db - 2);
			return -1;

			// aki switchet szereti...
			switch (db)
			{
				case 0:
					return 0;
				case 1:
					return 500;
				case 2:
					return 500 + 450;
				default:
					return 500 + 450 + 400 * (db - 2);
			}
		}

		private static List<List<string>> Beolvas(string path)
		{
			List<List<string>> lista = new List<List<string>>();

			using (StreamReader f = new StreamReader(path, Encoding.Default))
			{
				List<string> kosár = new List<string>();
				while (!f.EndOfStream)
				{
					string sor = f.ReadLine();
					if (sor=="F")
					{
						lista.Add(kosár);
						kosár = new List<string>();
					}
					else
					{
						kosár.Add(sor);
					}
				}
			}

			return lista;
		}
	}
}
