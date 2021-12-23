using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpeseADO_LucaFinoia
{
    public class ConnectedModeClient
    {
        public static string ConnectionString { get; set; }

        internal static void AddSpesa()
        {
            DataSet dataSet = new DataSet();
            using SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                    Console.WriteLine("Connessi al DB");
                else
                    Console.WriteLine("NON connessi al DB");
                SqlCommand insertCommand = connection.CreateCommand();
                insertCommand.CommandType = System.Data.CommandType.Text;
                insertCommand.CommandText = "INSERT INTO Spese VALUES(@data,@categoriaId,@descrizione,@utente,@importo,@approvato)";

                Console.Clear();
                Console.WriteLine("---- Inserire una nuova Spesa ----");


                insertCommand.Parameters.AddWithValue("@data", DateTime.Now);
                Console.WriteLine("ID Categoria:");
                int categoriaID;
                while (!int.TryParse(Console.ReadLine(), out categoriaID))
                {
                    Console.WriteLine("inserire un numero intero");
                }
                insertCommand.Parameters.AddWithValue("@categoriaId", categoriaID);
                Console.Write("Descrizione: ");
                string descrizione = Console.ReadLine();

                insertCommand.Parameters.AddWithValue("@descrizione", descrizione);

                Console.Write("Utente: ");
                string utente = Console.ReadLine();

                insertCommand.Parameters.AddWithValue("@utente", utente);

                Console.WriteLine("Importo:");
                double importo;
                while (!double.TryParse(Console.ReadLine(), out importo))
                {
                    Console.WriteLine("inserire un numero:");
                }


                insertCommand.Parameters.AddWithValue("@importo", importo);

                insertCommand.Parameters.AddWithValue("@approvato", 0);

                int result = insertCommand.ExecuteNonQuery();

                if (result != 1)
                    Console.WriteLine("Si è verificato un problema nell'inserimento della spesa.");
                else
                    Console.WriteLine("Spesa aggiunta.");
            


            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        internal static void ListSpesaApproved()
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
            SqlCommand selectCommand = connection.CreateCommand();
            selectCommand.CommandType = System.Data.CommandType.Text;
            selectCommand.CommandText = "Select * from Spese where approvato=1";
            SqlDataReader reader = selectCommand.ExecuteReader();
            Console.Clear();
            Console.WriteLine("---- Elenco Tickets ----");
            Console.WriteLine();
            Console.WriteLine("ID \t Data \t\t CategoriaId \t Descrizione \t Utente \t Importo \t Approvato");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
            while (reader.Read())
            {
                string formattedDate = ((DateTime)reader["Data"]).ToString("dd-MMM-yyyy");
                Console.WriteLine(reader["Id"] + "\t" + formattedDate+ "\t \t" + reader["CategoriaId"] + "\t     " + (string)reader["Descrizione"] + "\t   " + (string)reader["Utente"] + "\t \t  " + reader["Importo"] + "\t   " + reader["Approvato"]);
                }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            }
            catch (SqlException ex)
            {
                Console.Write($"SQL Error: {ex.Message}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.Write($"Error: {ex.Message}");
                Console.WriteLine();
            }
            finally
            {
                connection.Close();
            }
        }

        internal static void ListSpesaUser()
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand selectCommand = connection.CreateCommand();
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "Select * from Spese where user=@utente";
                Console.Write("Utente: ");
                string utente = Console.ReadLine();

                selectCommand.Parameters.AddWithValue("@utente", utente);
                SqlDataReader reader = selectCommand.ExecuteReader();
                Console.Clear();
                Console.WriteLine("---- Elenco Tickets ----");
                Console.WriteLine();
                Console.WriteLine("ID \t Data \t\t CategoriaId \t Descrizione \t Utente \t Importo \t Approvato");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
                while (reader.Read())
                {
                    string formattedDate = ((DateTime)reader["Data"]).ToString("dd-MMM-yyyy");
                    Console.WriteLine(reader["Id"] + "\t" + formattedDate + "\t \t" + reader["CategoriaId"] + "\t     " + (string)reader["Descrizione"] + "\t   " + (string)reader["Utente"] + "\t \t  " + reader["Importo"] + "\t   " + reader["Approvato"]);
                }
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            }
            catch (SqlException ex)
            {
                Console.Write($"SQL Error: {ex.Message}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.Write($"Error: {ex.Message}");
                Console.WriteLine();
            }
            finally
            {
                connection.Close();
            }
        }

        internal static void TotalSpesaCategoria()
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand selectCommand = connection.CreateCommand();
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "select CategoriaId, sum(Importo) as ImportoTotale from spese group by CategoriaId";
                                SqlDataReader reader = selectCommand.ExecuteReader();
                Console.Clear();
                Console.WriteLine("---- Elenco Tickets ----");
                Console.WriteLine();
                Console.WriteLine("  CategoriaId \t Importo Totale");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
                while (reader.Read())
                {
                    Console.WriteLine("\t"+reader["CategoriaId"] + "\t     " + reader["ImportoTotale"]);
                }
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            }
            catch (SqlException ex)
            {
                Console.Write($"SQL Error: {ex.Message}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.Write($"Error: {ex.Message}");
                Console.WriteLine();
            }
            finally
            {
                connection.Close();
            }
        }

        internal static void ApproveSpesa()
        {
            DataSet dataSet = new DataSet();
            using SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                    Console.WriteLine("Connessi al DB");
                else
                    Console.WriteLine("NON connessi al DB");
                SqlCommand updateCommand = connection.CreateCommand();
                updateCommand.CommandType = System.Data.CommandType.Text;
                updateCommand.CommandText = "update Spese set Approvato=1 Where Id=@id";

                Console.Clear();
                Console.Write("ID della spesa da approvare: ");
                string idValue = Console.ReadLine();

                updateCommand.Parameters.AddWithValue("@id", idValue);

                int result = updateCommand.ExecuteNonQuery();

                if (result != 1)
                    Console.WriteLine("Si è verificato un problema nella cancellazione della spesa.");
                else
                    Console.WriteLine("Spesa approvata.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL ERROR{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        internal static void DeleteSpesa()
        {
            DataSet dataSet = new DataSet();
            using SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                    Console.WriteLine("Connessi al DB");
                else
                    Console.WriteLine("NON connessi al DB");
                SqlCommand deleteCommand = connection.CreateCommand();
                deleteCommand.CommandType = System.Data.CommandType.Text;
                deleteCommand.CommandText = "DELETE FROM Spese Where Id=@id";

                Console.Clear();
                Console.Write("ID della spesa da cancellare: ");
                string idValue = Console.ReadLine();

                deleteCommand.Parameters.AddWithValue("@id", idValue);

                int result = deleteCommand.ExecuteNonQuery();

                if (result != 1)
                    Console.WriteLine("Si è verificato un problema nella cancellazione della spesa.");
                else
                    Console.WriteLine("Spesa rimossa.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL ERROR{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
