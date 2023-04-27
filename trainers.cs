namespace Trainers
{
    public class Trainer
    {
        private Guid trainerId;

        private string trainerName;

        private string address;

        private string email;


        public Trainer (Guid trainerId, string trainerName, string address, string email) {     // mainly for converting from .txt to actual object
            this.trainerId = trainerId;
            this.trainerName = trainerName;
            this.address = address;
            this.email = email;
        }

        public Guid GetTrainerId() {
            return this.trainerId;
        }

        public void SetTrainerName(string trainerName) {
            this.trainerName = trainerName;
        }

        public string GetTrainerName() {
            return this.trainerName;
        }

        public void SetAddress(string address) {
            this.address = address;
        }

        public void SetEmail(string email) {
            this.email = email;
        }

        public string ToFile () {
            return $"{this.trainerId}#{this.trainerName}#{this.address}#{this.email}";
        }
    }

    public class TrainerUtility
    {

        public int GetTrainerCount() {                                  // mainly for use within class
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

        private Trainer[] trainers;

        public TrainerUtility(Trainer[] trainers) {
            this.trainers = trainers;
        }

        public Trainer[] GetTrainersFromFile () {                   // shows all trainers in file
            StreamReader inFile = new StreamReader("trainers.txt");
            string input = inFile.ReadLine();
            int count = 0;

            while (input != null) {
                string[] temp = input.Split("#");
                Trainer newTrainer = new Trainer(Guid.Parse(temp[0]),temp[1],temp[2],temp[3]);
                trainers[count] = newTrainer;
                count++;
                input = inFile.ReadLine();
            } 

            inFile.Close();
            return trainers;
        }

        public void SendTrainersToFile() {                              // posts trainer array to trainers.txt overwrites
            int trainerCount = GetTrainerCount();
            StreamWriter outFile = new StreamWriter("trainers.txt");
            for (int i = 0; i < trainerCount; i++) {
                outFile.WriteLine(trainers[i].ToFile());
            }

            outFile.Close();
        }

        public void AddTrainerToFile() {                                    // adds trainer to trainers.txt
            Console.Clear();
            StreamWriter outFile = new StreamWriter("trainers.txt",true);

            Console.WriteLine("Enter a name for the new trainer.");
            string trainerName = Console.ReadLine();
            Console.WriteLine("Enter trainer's address.");
            string address = Console.ReadLine();
            Console.WriteLine("Enter trainer's email.");
            string email = Console.ReadLine();

            Trainer newTrainer = new Trainer(Guid.NewGuid(), trainerName, address, email);
            outFile.WriteLine(newTrainer.ToFile());
        
            outFile.Close();
        }

        public void DeleteTrainerFromFile(Trainer[] trainerArray, string deleteName) {              // deletes trainer from database
            Trainer[] trainers = trainerArray;
            int currentTrainers = GetTrainerCount();
            StreamWriter outFile = new StreamWriter("trainers.txt");

            for (int i = 0; i < currentTrainers; i++) {
                if (trainers[i].GetTrainerName() == deleteName) {
                    outFile.Write("");
                }
                else {
                    outFile.WriteLine(trainers[i].ToFile());
                }
            }

            outFile.Close();
        }

        public int FindTrainerLocation(Trainer[] trainerArray, string TrainerName) {            // returns int value of trainer in Trainer[]
            Trainer[] trainers = trainerArray;
            bool found = false;
            for (int i = 0; i < GetTrainerCount(); i++) {
                if (trainers[i].GetTrainerName() == TrainerName) {
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