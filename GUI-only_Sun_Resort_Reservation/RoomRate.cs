using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum BedType 
{ 
    King,
    Queen,
    Double
}

namespace GUI_only_Sun_Resort_Reservation
{
    internal class RoomRate
    {
        // Some of these data holders may just be excessive
        private BedType bed;
        private double bedCost;
        const double KING_COST = 200;
        const double QUEEN_COST = 150;
        const double DOUBLE_COST = 100;
        const double TAX_RATE = 0.07;
        const double WINTER_CHARGE = 30;
        private DateTime checkIn;
        private DateTime checkOut;
        private bool member = false;
        private double bedDiscount;
        private double roomTotal;
        private double days;
        private string name;
        private double taxAmount;


        public BedType BedType
        {
            set { bed = value; }
        }

        public double BedCharge
        {
            get
            {
                GetBedCharge();
                return bedCost;
            }
        }

        private void GetBedCharge() // Calculate bed cost
        {
            switch(bed) 
            {
                case BedType.King:
                    bedCost = KING_COST;
                    break;
                case BedType.Queen:
                    bedCost = QUEEN_COST;
                    break;
                case BedType.Double:
                    bedCost = DOUBLE_COST;
                    break;
            }
        }

        public DateTime CheckIn
        { 
            set { checkIn = value; }
        }

        public DateTime CheckOut
        {
            set { checkOut = value;
                if (checkOut <= checkIn) {
                    throw (new Exception("Dates not correct"));
                }
            }
        }

        public string Name 
        {
            set { name = value; }
        }

        public bool Member 
        {
            set { member = value; }
        }

        public void GetTotal() {

            GetBedCharge();
            //Total days is actually a TimeSpan property. thanks to visual studio I just scrolled thorugh methods.
            days = (checkOut.Date - checkIn.Date).TotalDays;
            
            if (checkIn.Month >= 1 && checkIn.Month <= 4) 
            {
                bedCost += WINTER_CHARGE; 
            }
            
            roomTotal = bedCost * days;

            if (member) 
            {
                bedDiscount = 0.1 * roomTotal;
            }

            taxAmount = roomTotal * TAX_RATE;
        }

        public override string ToString()
        {
            return $"Reservation\r\n" +
                $"Name {name}\r\n" +
                $"Type of room {bed}\r\n" +
                $"{days} Nights at {bedCost:c} is {roomTotal:c}\r\n" +
                $"Discount {bedDiscount:c}\r\n" +
                $"Tax Amount {taxAmount:c}\r\n" +
                $"Total amount due {(roomTotal + taxAmount - bedDiscount):c}\r\n" +
                $"Thank you";
        }
    }
}
