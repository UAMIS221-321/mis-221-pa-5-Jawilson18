using Trainers;
using Listings;
using Bookings;
Console.Clear();

int userInput;
do {
    Console.WriteLine("Welcome to TLAC Fitness Terminal.");
    System.Console.WriteLine("\nEnter '1' to see Trainer submenu, Enter '2' to see listing submenu, Enter '3' to see customer booking submenu, Enter '4' to see reports submenu, and Enter '-1' to exit.");
    userInput = int.Parse(Console.ReadLine());

    if (userInput == 1) {
        Console.Clear();
        TrainerMenu();                      // functioning
    }
    
    else if (userInput == 2) {
        Console.Clear();
        ListingMenu();                      // functioning
    }

    else if (userInput == 3) {
        Console.Clear();
        BookingMenu();                      // funtioning
    }

    else if (userInput == 4) {
        Console.Clear();
        ReportsMenu();                      // functioning
    }

    else if (userInput == -1) {
        Console.Clear();
        System.Console.WriteLine("Goodbye.");
    }

    else {
        Console.Clear();
        System.Console.WriteLine("Input not recognized please try again.");
    }


} while (userInput != -1);


//**1 method per business funtion

static int GetTrainerCount() {                                  // returns # of trainers in trainers.txt
            int count = 0;
            StreamReader inFile = new StreamReader("trainers.txt");
            string input = inFile.ReadLine();
            while (input != null) {
                count++;
                input = inFile.ReadLine();
            }
            inFile.Close();
            return count;
}

static void TrainerMenu() {

    System.Console.WriteLine("*Now using trainer terminal*");
    int userInput;

    do {                                                            //can hold 100 trainers at most
        Trainer[] trainers = new Trainer[100];
        System.Console.WriteLine("\nEnter '1' to see list of trainers, Enter '2' to add trainer to database, Enter '3' to delete trainer from database, Enter '4' to edit a trainer's information, Enter '-1' to return to main menu.");
        userInput = int.Parse(Console.ReadLine());

        if (userInput == 1) {                                                   // show current trainers in database
            Console.Clear();
            TrainerUtility getTrainers = new TrainerUtility(trainers);          
            trainers = getTrainers.GetTrainersFromFile();                       

            for (int i = 0; i < GetTrainerCount(); i++) {
                System.Console.WriteLine(trainers[i].ToFile());
            }
        }

        else if (userInput == 2) {                                              // add a single trainer to database
            int input;                                                          // can add more than one trainer at a time
            do {
                TrainerUtility addTrainer = new TrainerUtility(trainers);           
                addTrainer.AddTrainerToFile();                                      
                Console.Clear();
                System.Console.WriteLine("Trainer successfully added to file.");

                System.Console.WriteLine("\nWould you like to add another user to the database? Enter '1' for yes and '2' for no.");
                input = int.Parse(Console.ReadLine());
            } while (input == 1);
        }

        else if (userInput == 3) {                                                                  // delete trainer from database 
            int input;                                                                              // can remove multiple
            do {                                             
                TrainerUtility getTrainers = new TrainerUtility(trainers);          
                trainers = getTrainers.GetTrainersFromFile();

                System.Console.WriteLine("\nEnter the name of the trainer you want to delete.");
                string trainerName = Console.ReadLine();

                TrainerUtility deleteTrainer = new TrainerUtility(trainers);
                deleteTrainer.DeleteTrainerFromFile(trainers,trainerName); 
                Console.Clear();
                System.Console.WriteLine($"{trainerName} was deleted from database.");

                System.Console.WriteLine("\nWould you like to delete another user? Enter '1' for yes and '2' for no.");
                input = int.Parse(Console.ReadLine());
            } while (input == 1);
        }
                                                                            // successfully edits trainer in database
        else if (userInput == 4) {                                          // can edit more than one feature at a time
            TrainerUtility getTrainers = new TrainerUtility(trainers);          
            trainers = getTrainers.GetTrainersFromFile();

            System.Console.WriteLine("\nEnter the name of the trainer you want to edit.");
            string trainerName = Console.ReadLine();
            TrainerUtility getLocation = new TrainerUtility(trainers);
            int location = getLocation.FindTrainerLocation(trainers, trainerName);
            if (location == -1) {
                System.Console.WriteLine($"Trainer file for {trainerName} not found, please try again.");
            }
            int input;
            Console.Clear();
            if (location >= 0) {
                System.Console.WriteLine($"Currently editing trainer file: {trainers[location].ToFile()}");
                do {
                    System.Console.WriteLine("\nEnter '1' to change name, Enter '2' to change address, Enter '3' to change email, Enter '-1' to return to trainer menu.");
                    input = int.Parse(Console.ReadLine());
                    if (input == 1) {
                        System.Console.WriteLine("\nEnter the name you want the user to have.");
                        string changeName = Console.ReadLine();
                        trainers[location].SetTrainerName(changeName);
                        Console.Clear();
                        System.Console.WriteLine($"Name successfully changed to {changeName}.");
                    }

                    else if (input == 2) {
                        System.Console.WriteLine("\nEnter the address you want the user to have.");
                        string changeAddress = Console.ReadLine();
                        trainers[location].SetAddress(changeAddress);
                        Console.Clear();
                        System.Console.WriteLine($"Address successfully changed to {changeAddress}.");
                    }

                    else if (input == 3) {
                        System.Console.WriteLine("\nEnter the email you want the user to have.");
                        string changeEmail = Console.ReadLine();
                        trainers[location].SetEmail(changeEmail);
                        Console.Clear();
                        System.Console.WriteLine($"Email successfully changed to {changeEmail}.");
                    }

                    else if (input == -1) {
                        Console.Clear();
                        System.Console.WriteLine("*Now using trainer terminal*");
                    }

                    else {
                        Console.Clear();
                        System.Console.WriteLine("Input not recognized please try again.");
                    }

                } while (input != -1);
            }

            TrainerUtility postTrainers = new TrainerUtility(trainers);         
            postTrainers.SendTrainersToFile(); 
        }

        else if (userInput == -1) {
            Console.Clear();
        }

        else {                                      // error handling
            Console.Clear();
            System.Console.WriteLine("Input not recognized please try again.");
        }

    } while (userInput != -1);

}


static int GetListingCount() {                                  // returns # of listings in listings.txt
            int count = 0;
            StreamReader inFile = new StreamReader("listings.txt");
            string input = inFile.ReadLine();
            while (input != null) {
                count++;
                input = inFile.ReadLine();
            }
            inFile.Close();
            return count;
}

static void ListingMenu() {

    System.Console.WriteLine("*Now using listing terminal*");
    int userInput;

    do {
        Listing[] listings = new Listing[100];
        System.Console.WriteLine("\nEnter '1' to see listings, Enter '2' to add a listing to database, Enter '3' to delete a listing from database, Enter '4' to edit a listing's information, Enter '-1' to return to main menu.");
        userInput = int.Parse(Console.ReadLine());

        if (userInput == 1) {                                   // shows listings from listings.txt
            Console.Clear();
            ListingUtility getListings = new ListingUtility(listings);          
            listings = getListings.GetListingsFromFile();                       

            for (int i = 0; i < GetListingCount(); i++) {
                System.Console.WriteLine(listings[i].ToFile());
            }
        }

        else if (userInput == 2) {
            int input;                                                          // can add more than one listing at a time
            do {
                Console.Clear();
                int userChoice;
                System.Console.WriteLine("Would you like to see a list of available trainers? Enter '1' for yes and '2' for no.");
                userChoice = int.Parse(Console.ReadLine());
                if (userChoice == 1) {
                    int numTrainers = GetTrainerCount();
                    StreamReader inFile = new StreamReader("trainers.txt");
                    string line = inFile.ReadLine();
                    while (line != null) {
                        string[] temp = line.Split("#");
                        System.Console.WriteLine(temp[1]);
                        line = inFile.ReadLine();
                    }
                    inFile.Close();
                }

                else if (userChoice == 2) {                 // can add multiple listings
                    Console.Clear();
                }
                
                ListingUtility addListing = new ListingUtility(listings);           
                addListing.AddListingToFile();                                      
                Console.Clear();
                System.Console.WriteLine("Listing successfully added to file.");

                System.Console.WriteLine("\nWould you like to add another listing to the database? Enter '1' for yes and '2' for no.");
                input = int.Parse(Console.ReadLine());
            } while (input == 1);
        }

        else if (userInput == 3) {
            int input;                                                                              // can remove multiple listings
            do {                                             
                ListingUtility getListings = new ListingUtility(listings);          
                listings = getListings.GetListingsFromFile();

                System.Console.WriteLine("\nEnter the name of the trainer in the listing.");
                string trainerName = Console.ReadLine();
                System.Console.WriteLine("\nEnter the session date for the listing.");
                string sessionDate = Console.ReadLine();
                System.Console.WriteLine("\nEnter the session time for the listing.");
                string sessionTime = Console.ReadLine();

                ListingUtility deleteListing = new ListingUtility(listings);
                deleteListing.DeleteListingFromFile(listings, trainerName, sessionDate, sessionTime); 
                Console.Clear();
                System.Console.WriteLine($"Session on {sessionDate} at {sessionTime} with {trainerName} was removed from database.");

                System.Console.WriteLine("\nWould you like to delete another listing? Enter '1' for yes and '2' for no.");
                input = int.Parse(Console.ReadLine());
            } while (input == 1);
        }

        else if (userInput == 4) {                                               // can edit multiple features of a listing
            ListingUtility getListings = new ListingUtility(listings);           // needs trainer name, session date, and session time to edit
            listings = getListings.GetListingsFromFile();

            System.Console.WriteLine("\nEnter the name of the trainer in the listing that you want to edit.");
            string trainerName = Console.ReadLine();
            System.Console.WriteLine("\nEnter the session date for the listing that you want to edit.");
            string sessionDate = Console.ReadLine();
            System.Console.WriteLine("\nEnter the session time for the listing that you want to edit.");
            string sessionTime = Console.ReadLine();

            ListingUtility getLocation = new ListingUtility(listings);
            int location = getLocation.FindListingLocation(listings, trainerName, sessionDate, sessionTime);
            if (location == -1) {
                Console.Clear();
                System.Console.WriteLine($"\nListing on {sessionDate} for {sessionTime} with {trainerName} not found, please return to menu and try again.");
            }

            if (location >= 0) {
                System.Console.WriteLine($"\nCurrently editing listing: {listings[location].ToFile()}");
                int input;
                do {
                    System.Console.WriteLine("\nEnter '1' to change trainer, Enter '2' to change session date, Enter '3' to change session time, Enter '4' to change session cost, Enter '5' to change session availability, Enter '-1' to return to listing menu.");
                    input = int.Parse(Console.ReadLine());
                    Console.Clear();
                    if (input == 1) {
                        System.Console.WriteLine("Enter the trainer you want the listing to have.");
                        string changeName = Console.ReadLine();
                        listings[location].SetTrainerName(changeName);
                        Console.Clear();
                        System.Console.WriteLine($"Trainer successfully changed to {changeName}.");
                    }

                    else if (input == 2) {
                        System.Console.WriteLine("Enter the session date you want the listing to have.");
                        string changeDate = Console.ReadLine();
                        listings[location].SetSessionDate(changeDate);
                        Console.Clear();
                        System.Console.WriteLine($"Session date successfully changed to {changeDate}.");
                    }

                    else if (input == 3) {
                        System.Console.WriteLine("Enter the session time you want the listing to have.");
                        string changeTime = Console.ReadLine();
                        listings[location].SetSessionTime(changeTime);
                        Console.Clear();
                        System.Console.WriteLine($"Session time successfully changed to {changeTime}.");
                    }

                    else if (input == 4) {
                        System.Console.WriteLine("Enter the session cost you want the listing to have.");
                        int changeCost = int.Parse(Console.ReadLine());
                        listings[location].SetSessionCost(changeCost);
                        Console.Clear();
                        System.Console.WriteLine($"Session time successfully changed to {changeCost}.");
                    }

                    else if (input == 5) {
                        listings[location].ChangeSessionAvailability();
                        Console.Clear();
                        System.Console.WriteLine($"Session availability successfully changed to {listings[location].GetSessionAvailability()}.");
                    }

                    else if (input == -1) {
                        Console.Clear();
                        System.Console.WriteLine("*Now using listing terminal*");
                    }

                    else {
                        Console.Clear();
                        System.Console.WriteLine("Input not recognized please try again.");
                    }

                } while (input != -1);
            }

            ListingUtility postListings = new ListingUtility(listings);         
            postListings.SendListingsToFile(); 
        }

        else if (userInput == -1) {
            Console.Clear();
        }

        else {                              // error handling
            Console.Clear();
            System.Console.WriteLine("Input not recognized please try again.");
        }


    } while (userInput != -1);
}


static int GetBookingCount() {                                  // returns # of bookings in transactions.txt
            int count = 0;
            StreamReader inFile = new StreamReader("transactions.txt");
            string input = inFile.ReadLine();
            while (input != null) {
                count++;
                input = inFile.ReadLine();
            }
            inFile.Close();
            return count;
}

static void BookingMenu() {
    int userInput;

    do {
        Booking[] bookings = new Booking[100];
        Listing[] listings = new Listing[100];
        Trainer[] trainers = new Trainer[100];
        Console.Clear();
        System.Console.WriteLine("*Now using booking terminal*");
        System.Console.WriteLine("\nEnter '1' to see listings/bookings, Enter '2' to add a booking to database, Enter '3' to undo a booking, Enter '4' to mark a booking as completed/withdrawn, Enter '-1' to return to main menu.");
        userInput = int.Parse(Console.ReadLine());

        if (userInput == 1) {
            int input;
            do {
                System.Console.WriteLine("\nWould you like to see active listings or scheduled bookings? Enter '1' for listings, '2' for bookings, and '-1' to return to booking menu.");
                input = int.Parse(Console.ReadLine());
                if (input == 1) {
                    Console.Clear();
                    ListingUtility getListings = new ListingUtility(listings);          
                    listings = getListings.GetListingsFromFile();                       

                    for (int i = 0; i < GetListingCount(); i++) {
                        if (listings[i].GetSessionAvailability()) {
                            System.Console.WriteLine(listings[i].ToFile());
                        }
                    }
                    System.Console.WriteLine("\n*Showing sessions available for booking*");
                }

                else if (input == 2) {
                    Console.Clear();
                    BookingUtility getBookings = new BookingUtility(bookings);          
                    bookings = getBookings.GetBookingsFromFile();                       

                    for (int i = 0; i < GetBookingCount(); i++) {
                        System.Console.WriteLine(bookings[i].ToFile());
                    }

                    System.Console.WriteLine("\n*Sessions marked true are actively booked*");
                }

                else if (input == -1) {
                    Console.Clear();
                    System.Console.WriteLine("*Now using booking terminal*");
                }

                else {
                    Console.Clear();
                    System.Console.WriteLine("Input not recognized please try again.");
                }
            } while (input != -1);
        }

        else if (userInput == 2) {
            int input;
                do {
                    ListingUtility getListings = new ListingUtility(listings);          
                    listings = getListings.GetListingsFromFile();

                    System.Console.WriteLine("\nDo you need to see a list of available sessions? Enter '1' for yes and '2' for no.");
                    int choice = int.Parse(Console.ReadLine());
                        if (choice == 1) {
                            Console.Clear();                     
                            for (int i = 0; i < GetListingCount(); i++) {
                                if (listings[i].GetSessionAvailability()) {
                                    System.Console.WriteLine(listings[i].ToFile());
                                }
                            }
                        }
                    
                    System.Console.WriteLine("\nEnter the name of the trainer you want to book.");
                    string trainerName = Console.ReadLine();
                    System.Console.WriteLine("\nEnter the date of the training you want to book.");
                    string trainingDate = Console.ReadLine();
                    System.Console.WriteLine("\nEnter the time of the training that you want to book.");
                    string trainingTime = Console.ReadLine();

                    int location;
                    int listingCount = GetListingCount();
                    for (int i = 0; i < listingCount; i++) {
                        if (listings[i].GetTrainerName() == trainerName && listings[i].GetSessionDate() == trainingDate && listings[i].GetSessionTime() == trainingTime && !listings[i].GetSessionAvailability()) {
                            System.Console.WriteLine($"Session with {trainerName} on {trainingDate} at {trainingTime} unavailable, please make another selection.");
                            location = -1;
                        }
                        else if (listings[i].GetTrainerName() == trainerName && listings[i].GetSessionDate() == trainingDate && listings[i].GetSessionTime() == trainingTime && listings[i].GetSessionAvailability()) {
                            location = i;
                            
                            listings[i].ChangeSessionAvailability();
                            ListingUtility postListings = new ListingUtility(listings);         
                            postListings.SendListingsToFile(); 

                            TrainerUtility getTrainers = new TrainerUtility(trainers);          
                            trainers = getTrainers.GetTrainersFromFile();
                            TrainerUtility findTrainerLocation = new TrainerUtility(trainers);          
                            int trainerLocation = findTrainerLocation.FindTrainerLocation(trainers,trainerName);    

                            BookingUtility addBooking = new BookingUtility(bookings);          
                            addBooking.AddBookingToFile(trainerName, trainers[trainerLocation].GetTrainerId(), trainingDate, trainingTime, listings[i].GetSessionCost());
                        }
                    }
                    System.Console.WriteLine("Would you like to make another booking? Enter '1' for yes and '2' for no.");
                    input = int.Parse(Console.ReadLine());
            } while (input != 2);
        }

        else if (userInput == 3) {
            BookingUtility getBookings = new BookingUtility(bookings);          
            bookings = getBookings.GetBookingsFromFile();
            ListingUtility getListings = new ListingUtility(listings);          
            listings = getListings.GetListingsFromFile();
            int input;
            do {                                            
                System.Console.WriteLine("\nEnter the name of the trainer in the booking.");
                string trainerName = Console.ReadLine();
                System.Console.WriteLine("\nEnter the training date for the booking.");
                string trainingDate = Console.ReadLine();
                System.Console.WriteLine("\nEnter the training time for the booking.");
                string trainingTime = Console.ReadLine();

                BookingUtility deleteBooking = new BookingUtility(bookings);
                deleteBooking.DeleteBookingFromFile(bookings, trainerName, trainingDate, trainingTime);
                ListingUtility getLocation = new ListingUtility(listings);
                int location = getLocation.FindListingLocation(listings, trainerName, trainingDate, trainingTime);
                listings[location].ChangeSessionAvailability();
                
                Console.Clear();
                System.Console.WriteLine($"Session on {trainingDate} at {trainingTime} with {trainerName} was unbooked.");

                System.Console.WriteLine("\nWould you like to undo another booking? Enter '1' for yes and '2' for no.");
                input = int.Parse(Console.ReadLine());
                } while (input == 1);
            ListingUtility postListings = new ListingUtility(listings);         
            postListings.SendListingsToFile();
        }

        else if (userInput == 4) {
            BookingUtility getBookings = new BookingUtility(bookings);          
            bookings = getBookings.GetBookingsFromFile();
            int choice;
            do {
                System.Console.WriteLine("\nEnter the name of the trainer in the booking.");
                string trainerName = Console.ReadLine();
                System.Console.WriteLine("\nEnter the training date for the booking.");
                string trainingDate = Console.ReadLine();
                System.Console.WriteLine("\nEnter the training time for the booking.");
                string trainingTime = Console.ReadLine();

                BookingUtility getLocation = new BookingUtility(bookings);
                int location = getLocation.FindBookingLocation(bookings, trainerName, trainingDate, trainingTime);
                bookings[location].Complete();
                BookingUtility postBookings = new BookingUtility(bookings);
                postBookings.SendBookingsToFile();

                System.Console.WriteLine("\nWould you like to complete another booking? Enter '1' for yes and '2' for no.");
                choice = int.Parse(Console.ReadLine());
            } while (choice == 1);
        }

        else if (userInput == -1) {
            Console.Clear();
        }

        else {                                  // error handling
            Console.Clear();
            System.Console.WriteLine("Input not recognized please try again.");
        }

    } while (userInput != -1);
}


static void ReportsMenu() {
    int userInput;

    Booking[] bookings = new Booking[100];
    BookingUtility getBookings = new BookingUtility(bookings);
    bookings = getBookings.GetBookingsFromFile();
    Console.Clear();
    System.Console.WriteLine("*Now using reports terminal*");
    
    do {
        System.Console.WriteLine("\nEnter '1' to view a customer's past sessions, Enter '2' to view all customers' past sessions, Enter '3' to view historical revenue, Enter '-1' to return to main menu.");
        userInput = int.Parse(Console.ReadLine());

        if (userInput == 1) {
            int input;
            do {                                                    // generates customer report
                Console.Clear();
                System.Console.WriteLine("Enter customer email for report viewing.");
                string customerEmail = Console.ReadLine();

                Booking[] customerBookings = new Booking[100];
                BookingUtility getCustomerSessions = new BookingUtility(bookings);
                int numBookings = getCustomerSessions.GetCustomerSessions(bookings, customerEmail, customerBookings);

                System.Console.WriteLine("\nWould you like to save this report to a file? Enter '1' for yes and '2' for no.");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1) {
                    System.Console.WriteLine("\nEnter the name of the file you want to save to. (For demonstration purposes 'report1.txt' is recommended)");
                    string fileName = Console.ReadLine();

                    StreamWriter outFile = new StreamWriter(fileName);
                    for (int i = 0; i < numBookings; i++) {
                        outFile.WriteLine(customerBookings[i].ToFile());
                    }
                    outFile.Close();
                    Console.Clear();
                    System.Console.WriteLine($"Report saved to {fileName}.");
                }
                System.Console.WriteLine("\nWould you like to view another customer's past sessions? Enter '1' for yes and '2' for no.");
                input = int.Parse(Console.ReadLine());
            } while (input == 1);
        }

        else if (userInput == 2) {                                              // generates report of all customers
            BookingUtility sortBookings = new BookingUtility(bookings);
            Booking[] sortedBookings = sortBookings.SortBookingData(bookings);

            System.Console.WriteLine("\nWould you like to save this report to a file? Enter '1' for yes and '2' for no.");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1) {
                    System.Console.WriteLine("\nEnter the name of the file you want to save to. (For demonstration purposes 'report2.txt' is recommended)");
                    string fileName = Console.ReadLine();

                    int numBookings = GetBookingCount();
                    StreamWriter outFile = new StreamWriter(fileName);
                    for (int i = 0; i < numBookings; i++) {
                        outFile.WriteLine(sortedBookings[i].ToFile());
                    }

                    outFile.WriteLine();
                    int count;
                    for (int i = 0; i < numBookings - 1; i += count) {
                        count = 1;
                        for (int j = i + 1; j < numBookings; j++) {
                            if (sortedBookings[j].GetCustomerName() == sortedBookings[i].GetCustomerName()) {
                                count++;
                            }
                        }
                        outFile.WriteLine($"{sortedBookings[i].GetCustomerName()}:   {count}");
                    }

                    outFile.Close();
                    Console.Clear();
                    System.Console.WriteLine($"Report saved to {fileName}.");
                }
        }

        else if (userInput == 3) {                                          // generates monthly revenue report
            Console.Clear();
            BookingUtility revenueReport = new BookingUtility(bookings);
            revenueReport.GenerateRevenueReport(bookings);

            System.Console.WriteLine("Would you like to save this report to a file? Enter '1' for yes and '2' for no.");
            int input = int.Parse(Console.ReadLine());
            if (input == 1) {
                System.Console.WriteLine("\nEnter the name of the file you want to save to. (For demonstration purposes 'report3.txt' is recommended)");
                string fileName = Console.ReadLine();

                BookingUtility revenueReportToFile = new BookingUtility(bookings);
                revenueReportToFile.GenerateRevenueReportToFile(bookings, fileName);

                System.Console.WriteLine($"\nReport saved to {fileName}, Press any key to return to report menu.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        else if (userInput == -1) {
            Console.Clear();
        }
            
        else {                                  // error handling
            Console.Clear();
            System.Console.WriteLine("Input not recognized please try again.");
        }

    } while (userInput != -1);
}