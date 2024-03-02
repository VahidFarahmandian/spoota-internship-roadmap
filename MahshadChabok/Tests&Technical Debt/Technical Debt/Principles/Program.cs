
using Principles.Dry;
using Principles.Kiss;

Client customer = new Client
{
    Name = "Ali Ahmadi",
    Email = "johndoe@example.com",
   
};

ClientRepository repository = new ClientRepository();
repository.SaveCustomer(customer);


Player player1 = new Player
{
    Age = 16,
    Name = "TOM",
    IsOnline = true,
    

};

Player player2 = new Player
{
    Age = 22,
    Name = "Jac",
    IsOnline = false,
    CurrentGame = "evening 6 pm",

};
CheckingPlayer chechp = new CheckingPlayer();
string status1= chechp.GetPlayerStatus(player1);
Console.WriteLine(status1);

string status2 = chechp.GetPlayerStatusBetter(player2);
Console.WriteLine(status2);
