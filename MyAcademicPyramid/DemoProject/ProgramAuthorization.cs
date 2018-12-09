using DataAccessLayer;
using System;
using DemoProject.MockUserManagementNameSpace;

namespace DemoProject
{
    class ProgramAuthorization
    {
        
        static void Main(string[] args)
        {
            //// Create user Trong 
            //User admin = new User("Trong");


            //// Controller that handles any request involving user management
            //UserManagementController userManagementController = new UserManagementController(admin);

            //// Self Created admin
            //userManagementController.CreateUserAction(admin);
            ////Assign claim CanDeleteUserOwnAccount


            ////Create user Krystal 
            //User Krystal = new User("Krystal");
            //userManagementController.CreateUserAction(Krystal);

            //// Delete other account request is received
            //Console.WriteLine("***Let Trong delete other user account***");
            ////Access DeleteOtherAccount method using the controller
            //userManagementController.DeleteOtherAction(Krystal);


            //// Delete own account request is received
            //Console.WriteLine("***Let Trong delete his own user account***");
            ////Access DeleteUserOwnAccount method using the controller
            //userManagementController.DeleteOwnAction(admin);


            ////Assign CanDeleteUserOwnAccount and CanDeleteUserOwnAccount;

            //// Delete own account request is received
            //Console.WriteLine("\n***Let Krystal delete her own user account***");

            //userManagementController.DeleteOwnAction(Krystal);

            //// Delete other account request is received
            //Console.WriteLine("***Let Krystal delete other user account***");
            //userManagementController.DeleteOtherAction(Krystal);

            //// null example
            ////userManagementController.DeleteOwnAction(null);
            //Console.ReadLine();




            // Create user Trong 
            ResetDbandSeed resetDB = new ResetDbandSeed();
            resetDB.Reseed();
            User admin = new User(3, "Trong");


            UserManagementController userManagementController_Admin = new UserManagementController(admin);



            User user = new User("Arturo");
            user.Claims.Add(new Claim("CanDeleteOtherAccount"));
            user.Claims.Add(new Claim("HasPoints"));

            userManagementController_Admin.CreateUserAction(user);
            User User_Arturo = userManagementController_Admin.FindUserAction("Arturo");


            UserManagementController userManagementController_Arturo = new UserManagementController(User_Arturo);


            userManagementController_Arturo.DeleteOtherAction(admin);

            //userManagementController_Admin.DeleteOtherAction(deletedUser);


            //userManagementController_Admin.CreateUserAction(Krystal);
            //User deletedUser = userManagementController_Admin.FindUserAction("Krystal");

            //UserManagementController userManagementController_LowerAdmin = new UserManagementController(Krystal);

            // Let Lower Admin delete Upper Admin 
            //Console.WriteLine("***Let Lower Admin delete Upper Admin ***");
            //Access DeleteUserOwnAccount method using the controller
            //userManagementController_Admin.DeleteOtherAction(Krystal);
            // userManagementController_LowerAdmin.DeleteOtherAction(admin);
            //Access DeleteUserOwnAccount method using the controller


            Console.ReadLine();




        }
    }
}
