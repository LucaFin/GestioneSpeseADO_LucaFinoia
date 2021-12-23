using GestioneSpeseADO_LucaFinoia;

string connectionStringSQL = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestioneSpese;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
Console.WriteLine("=== Esercitazione  ===");

DisconnectedModeClient.ConnectionString = connectionStringSQL;

ConnectedModeClient.ConnectionString = connectionStringSQL;

bool quit = false;
do
{
    Console.WriteLine($"============= Menu =============");
    Console.WriteLine();
    Console.WriteLine("[ 1 ] - Aggiungi Spesa\n");
    Console.WriteLine("[ 2 ] - Approva Spesa\n");
    Console.WriteLine("[ 3 ] - Cancella Spesa\n");
    Console.WriteLine("[ 4 ] - Mostra Spese Approvate\n");
    Console.WriteLine("[ 5 ] - Mostra Spese di un Utente\n");
    Console.WriteLine("[ 6 ] - Totale Spese per Categoria\n");
    Console.WriteLine("[ q ] - QUIT\n");


    // scelta utente
    Console.Write("> ");
    string scelta = Console.ReadLine();
    Console.WriteLine();

    switch (scelta)
    {
        case "1":
            // add new spesa
            ConnectedModeClient.AddSpesa();
            break;
        case "2":
            // approve spesa
            ConnectedModeClient.ApproveSpesa();
            break;
        case "3":
            // delete spesa
            ConnectedModeClient.DeleteSpesa();
            break;
        case "4":
            ConnectedModeClient.ListSpesaApproved();
            break;
        case "5":
            DisconnectedModeClient.ListSpesaUser();
            break;
        case "6":
            ConnectedModeClient.TotalSpesaCategoria();
            break;
        case "q":
            quit = true;
            break;
        default:
            Console.WriteLine("Comando sconosciuto.");
            break;
    }

} while (!quit);
