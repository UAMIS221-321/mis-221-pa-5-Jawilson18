namespace Bookings
{
    public class Booking
    {
        public Guid sessionId;

        public string customerName;

        public string customerEmail;

        public string trainingDate;

        public string trainingTime;

        public int trainingCost;

        public Guid trainerId;

        public string trainerName;

        public bool taken = true;               // in this case taken = true implies session is booked

        public Booking (Guid sessionid, string customerName, string customerEmail, Guid trainerId, string trainerName, string trainingDate, string trainingTime, int trainingCost, bool taken) {
            this.sessionId = sessionid;
            this.customerName= customerName;
            this.customerEmail = customerEmail;
            this.trainingDate = trainingDate;
            this.trainingTime = trainingTime;
            this.trainingCost = trainingCost;
            this.trainerId = trainerId;
            this.trainerName = trainerName;
        }

        public string ToFile() {
            return $"{this.sessionId}#{this.customerName}#{this.customerEmail}#{this.trainerId}#{this.trainerName}#{this.trainingDate}#{this.trainingTime}#{this.trainingCost}#{this.taken}";
        }

        public string GetTrainerName() {
            return this.trainerName;
        }

        public string GetTrainingDate() {
            return this.trainingDate;
        }

        public int GetTrainingMonth() {
            string dateWhole = GetTrainingDate();
            string[] dateComponents = dateWhole.Split("/");
            return int.Parse(dateComponents[0]);
        }

        public string GetTrainingTime() {
            return this.trainingTime;
        }

        public int GetTrainingCost() {
            return this.trainingCost;
        }

        public string GetCustomerEmail() {
            return this.customerEmail;
        }

        public string GetCustomerName() {
            return this.customerName;
        }

        public void Complete() {
            this.taken = false;
        }
    }

    public class BookingUtility {

        static int GetBookingCount() {                                  // returns # of bookings in bookings.txt
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

        public Booking[] bookings;

        public BookingUtility(Booking[] bookings) {
            this.bookings = bookings;
        }

        public Booking[] GetBookingsFromFile () {                   // shows all trainers in database
            StreamReader inFile = new StreamReader("transactions.txt");
            string input = inFile.ReadLine();
            int count = 0;

            while (input != null) {
                string[] temp = input.Split("#");
                Booking newBooking = new Booking(Guid.Parse(temp[0]),temp[1],temp[2],Guid.Parse(temp[3]),temp[4],temp[5],temp[6],int.Parse(temp[7]),bool.Parse(temp[8]));
                bookings[count] = newBooking;
                count++;
                input = inFile.ReadLine();
            } 

            inFile.Close();
            return bookings;
        }

        public void SendBookingsToFile() {                              // posts booking array to transactions.txt overwrites
            int bookingCount = GetBookingCount();
            StreamWriter outFile = new StreamWriter("transactions.txt");
            for (int i = 0; i < bookingCount; i++) {
                outFile.WriteLine(bookings[i].ToFile());
            }

            outFile.Close();
        }

        public void AddBookingToFile(string trainerName, Guid trainerId, string trainingDate, string trainingTime, int trainingCost) {          // adds booking to transactions.txt
            StreamWriter outFile = new StreamWriter("transactions.txt",true);

            Console.WriteLine("\nEnter customer's name.");
            string customerName = Console.ReadLine();
            Console.WriteLine("\nEnter customer's email.");
            string customerEmail = Console.ReadLine();

            Booking newBooking = new Booking(Guid.NewGuid(), customerName, customerEmail, trainerId, trainerName, trainingDate, trainingTime, trainingCost, true);
            outFile.WriteLine(newBooking.ToFile());
        
            outFile.Close();
        }

        public void DeleteBookingFromFile(Booking[] bookingArray, string trainerName, string trainingDate, string trainingTime) {              // deletes listing from database
            Booking[] bookings = bookingArray;
            int currentTrainers = GetBookingCount();
            StreamWriter outFile = new StreamWriter("transactions.txt");

            for (int i = 0; i < currentTrainers; i++) {
                if (bookings[i].GetTrainerName() == trainerName && bookings[i].GetTrainingDate() == trainingDate && bookings[i].GetTrainingTime() == trainingTime) {
                    outFile.Write("");
                }
                else {
                    outFile.WriteLine(bookings[i].ToFile());
                }
            }

            outFile.Close();
        }

        public int FindBookingLocation(Booking[] bookingArray, string trainerName, string trainingDate, string trainingTime) {         // returns index in array based on trainer, date, and time
            Booking[] bookings = bookingArray;
            bool found = false;
            for (int i = 0; i < GetBookingCount(); i++) {
                if (bookings[i].GetTrainerName() == trainerName && bookings[i].GetTrainingDate() == trainingDate && bookings[i].GetTrainingTime() == trainingTime) {
                    return i;
                    found = true;
                }
            }

            if (!found) {
                return -1;
            }
            else {
                return -9;
            }
        }

        public int GetCustomerSessions(Booking[] bookings, string customerEmail, Booking[] customerBookings){           //returns int value of sessions booked by a customer
            int count = 0;
            for (int i = 0; i < GetBookingCount(); i++) {
                if (bookings[i].GetCustomerEmail() == customerEmail) {
                    System.Console.WriteLine(bookings[i].ToFile());
                    customerBookings[count] = bookings[i];
                    count++;
                }   
            }
            return count;
        }

        public void GenerateRevenueReport(Booking[] bookings) {                     // creates monthly revenue report
            int[] month = new int[12];
            int[] revenues = {0,0,0,0,0,0,0,0,0,0,0,0};
            string[] months = {"January","February","March","April","May","June","July","August","September","October","November","December"};
            for (int i = 0; i < GetBookingCount(); i++) {
                revenues[bookings[i].GetTrainingMonth() - 1] += bookings[i].GetTrainingCost();
            }

            for (int i = 0; i < 12; i++) {
                if (revenues[i] != 0) {
                    System.Console.WriteLine($"{months[i]}:     ${revenues[i]}");
                }
            }
        }

        public void GenerateRevenueReportToFile(Booking[] bookings, string fileName) {          // sorts by customer, then by date, then writes customer, date groups and number of sessions per customer
            int[] month = new int[12];
            int[] revenues = {0,0,0,0,0,0,0,0,0,0,0,0};
            string[] months = {"January","February","March","April","May","June","July","August","September","October","November","December"};
            for (int i = 0; i < GetBookingCount(); i++) {
                revenues[bookings[i].GetTrainingMonth() - 1] += bookings[i].GetTrainingCost();
            }

            StreamWriter outFile = new StreamWriter(fileName);
            for (int i = 0; i < 12; i++) {
                if (revenues[i] != 0) {
                    outFile.WriteLine($"{months[i]}:     ${revenues[i]}");
                }
            }
            outFile.Close();
        }

        public Booking[] SortBookingData(Booking[] bookings) {
            Booking[] temps = new Booking[100];
            Booking[] sortedBookings = bookings;
            int bookingCount = GetBookingCount();
            
            
            for(int i = 0; i < bookingCount - 1; i++) {
                for (int j = i + 1; j < bookingCount; j++) {
                    if (sortedBookings[i].GetCustomerName()[0] > sortedBookings[j].GetCustomerName()[0]) {
                        Booking temp = sortedBookings[i];
                        sortedBookings[i] = sortedBookings[j];
                        sortedBookings[j] = temp;
                    }
                    else if (sortedBookings[i].GetCustomerName()[0] == sortedBookings[j].GetCustomerName()[0]) {
                        if (sortedBookings[i].GetCustomerName()[1] > sortedBookings[j].GetCustomerName()[1]) {
                            Booking temp = sortedBookings[i];
                            sortedBookings[i] = sortedBookings[j];
                            sortedBookings[j] = temp;
                        }
                    }   
                }
            }

            for (int i = 0; i < bookingCount - 1; i++) {
                for (int j = i + 1; j < bookingCount; j++) {
                    if (sortedBookings[i].GetCustomerName() == sortedBookings[j].GetCustomerName()) {
                        if (sortedBookings[i].GetTrainingDate()[0] > sortedBookings[j].GetTrainingDate()[0]) {
                            Booking temp = sortedBookings[i];
                            sortedBookings[i] = sortedBookings[j];
                            sortedBookings[j] = temp;
                        }
                        
                        else if (sortedBookings[i].GetTrainingDate()[0] == sortedBookings[j].GetTrainingDate()[0]) {
                            if (sortedBookings[i].GetTrainingDate()[2] > sortedBookings[j].GetTrainingDate()[2]) {
                                Booking temp = sortedBookings[i];
                                sortedBookings[i] = sortedBookings[j];
                                sortedBookings[j] = temp;
                            }

                            else if (sortedBookings[i].GetTrainingDate()[2] == sortedBookings[j].GetTrainingDate()[2]) {
                                if (sortedBookings[i].GetTrainingDate()[3] > sortedBookings[j].GetTrainingDate()[3]) {
                                    Booking temp = sortedBookings[i];
                                    sortedBookings[i] = sortedBookings[j];
                                    sortedBookings[j] = temp;
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < bookingCount; i++) {
                System.Console.WriteLine(sortedBookings[i].ToFile());
            }

            System.Console.WriteLine();
            int count;
            for (int i = 0; i < bookingCount - 1; i += count) {
                count = 1;
                for (int j = i + 1; j < bookingCount; j++) {
                    if (sortedBookings[j].GetCustomerName() == sortedBookings[i].GetCustomerName()) {
                        count++;
                    }
                }
                System.Console.WriteLine($"{sortedBookings[i].GetCustomerName()}:   {count}");
            }
            
            return sortedBookings;
        }
    }
}