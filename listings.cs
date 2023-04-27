namespace Listings
{
    public class Listing
    {
        private Guid listingId;

        private string trainerName;

        private string sessionDate;

        private string sessionTime;

        private int sessionCost;

        private bool available = true;                  // in this case available = true implies listing is available

        public Listing (Guid listingId, string trainerName, string sessionDate, string sessionTime, int sessionCost, bool available) {   // from file to object
            this.listingId = listingId;
            this.trainerName = trainerName;
            this.sessionDate = sessionDate;
            this.sessionTime = sessionTime;
            this.sessionCost = sessionCost;
            this.available = available;
        }

        public string ToFile() {
            return $"{this.listingId}#{this.trainerName}#{this.sessionDate}#{this.sessionTime}#{this.sessionCost}#{this.available}";
        }

        public string GetTrainerName() {
            return this.trainerName;
        }

        public void SetTrainerName(string trainerName) {
            this.trainerName = trainerName;
        }

        public string GetSessionDate() {
            return this.sessionDate;
        }

        public void SetSessionDate(string sessionDate) {
            this.sessionDate = sessionDate;
        }

        public string GetSessionTime() {
            return this.sessionTime;
        }

        public void SetSessionTime(string sessionTime) {
            this.sessionTime = sessionTime;
        }

        public void SetSessionCost(int sessionCost) {
            this.sessionCost = sessionCost;
        }

        public int GetSessionCost() {
            return this.sessionCost;
        }

        public bool GetSessionAvailability() {
            return this.available;
        }

        public void ChangeSessionAvailability() {
            this.available = !this.available;
        }
        
    }

    public class ListingUtility 
    {
        public int GetListingCount() {                                  // mainly for use within class
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

        public Listing[] listings;

        public ListingUtility(Listing[] listings) {
            this.listings = listings;
        }

        public Listing[] GetListingsFromFile () {                   // shows all trainers in database
            StreamReader inFile = new StreamReader("listings.txt");
            string input = inFile.ReadLine();
            int count = 0;

            while (input != null) {
                string[] temp = input.Split("#");
                Listing newListing = new Listing(Guid.Parse(temp[0]),temp[1],temp[2],temp[3],int.Parse(temp[4]),bool.Parse(temp[5]));
                listings[count] = newListing;
                count++;
                input = inFile.ReadLine();
            } 

            inFile.Close();
            return listings;
        }

        public void SendListingsToFile() {                              // posts listing array to listings.txt overwrites
            int listingCount = GetListingCount();
            StreamWriter outFile = new StreamWriter("listings.txt");
            for (int i = 0; i < listingCount; i++) {
                outFile.WriteLine(listings[i].ToFile());
            }

            outFile.Close();
        }

        public void AddListingToFile() {                                    // adds listing to listings.txt
            StreamWriter outFile = new StreamWriter("listings.txt",true);

            Console.WriteLine("\nEnter trainer's name.");
            string trainerName = Console.ReadLine();
            Console.WriteLine("\nEnter sessions's date.");
            string sessionDate = Console.ReadLine();
            Console.WriteLine("\nEnter session's time.");
            string sessionTime = Console.ReadLine();
            Console.WriteLine("\nEnter session's cost.");
            int sessionCost = int.Parse(Console.ReadLine());

            Listing newListing = new Listing(Guid.NewGuid(), trainerName, sessionDate, sessionTime, sessionCost, true);
            outFile.WriteLine(newListing.ToFile());
        
            outFile.Close();
        }

        public void DeleteListingFromFile(Listing[] listingArray, string trainerName, string sessionDate, string sessionTime) {              // deletes listing from database
            Listing[] listings = listingArray;
            int currentTrainers = GetListingCount();
            StreamWriter outFile = new StreamWriter("listings.txt");

            for (int i = 0; i < currentTrainers; i++) {
                if (listings[i].GetTrainerName() == trainerName && listings[i].GetSessionDate() == sessionDate && listings[i].GetSessionTime() == sessionTime) {
                    outFile.Write("");
                }
                else {
                    outFile.WriteLine(listings[i].ToFile());
                }
            }

            outFile.Close();
        }

        public int FindListingLocation(Listing[] listingArray, string trainerName, string sessionDate, string sessionTime) {         // returns index in array based on trainer, date, and time
            Listing[] listings = listingArray;
            bool found = false;
            for (int i = 0; i < GetListingCount(); i++) {
                if (listings[i].GetTrainerName() == trainerName && listings[i].GetSessionDate() == sessionDate && listings[i].GetSessionTime() == sessionTime) {
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
    }
}