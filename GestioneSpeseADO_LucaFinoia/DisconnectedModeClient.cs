using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpeseADO_LucaFinoia
{
    public class DisconnectedModeClient
    {
        public static string ConnectionString {get;set;}

        internal static void ListSpesaUser()
        {
            DataSet dataset = new DataSet();
            using SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                    Console.WriteLine("Connessi al db");
                else
                    Console.WriteLine("NON connessi al db");

                var Adapter = InizializzaAdapter(conn);

                Adapter.Fill(dataset, "Spese");

                conn.Close();
                Console.WriteLine("Connessione chiusa");
                Console.WriteLine("Lista Utenti");
                List<String> data = new List<String>();
                foreach (DataRow row in dataset.Tables["Spese"].Rows)
                {
                    data.Add(row["Utente"].ToString());
                }
                foreach(String user in data.Distinct())
                {
                    Console.WriteLine(user);
                }
                Console.WriteLine();
                Console.WriteLine("Utente");
                string utente = Console.ReadLine();
                Console.WriteLine("----------------Spese---------------");
                Console.WriteLine("ID \t \t Data \t\t CategoriaId \t Descrizione \t Importo \t Approvato");
                foreach (DataRow item in dataset.Tables["Spese"].Rows)
                {
                    if (item["Utente"].ToString() == utente)
                    {
                        Console.WriteLine($"{ item["ID"]} \t{ item["Data"]} \t \t{ item["CategoriaId"]}\t    { item["Descrizione"]}    \t{ item["Importo"]} \t\t{ item["Approvato"]} ");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Errore SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore generico: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }

        private static SqlDataAdapter InizializzaAdapter(SqlConnection conn)
        {
            SqlDataAdapter spesaAdapter = new SqlDataAdapter();

            //SELECT (serve al metodo FILL)
            spesaAdapter.SelectCommand = new SqlCommand("Select * from Spese", conn);
            spesaAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            return spesaAdapter;
        }

    }
}
